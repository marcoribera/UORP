using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using System.Threading;

namespace Server.Misc
{
    class BardFunctions
    {
        public static void UseBardInstrument(Item instrument, bool succeed, Mobile singer)
        {
            if (instrument == null)
                return;

            if (instrument is BaseInstrument) { } else { return; }

            if (singer == null)
                return;

            BaseInstrument harp = (BaseInstrument)instrument;

            if (succeed == true)
            {
                singer.PlaySound(harp.SuccessSound);
            }
            else
            {
                singer.PlaySound(harp.FailureSound);
            }

            if (harp.UsesRemaining > 1)
            {
                harp.UsesRemaining--;
            }
            else
            {
                if (singer != null)
                    singer.SendLocalizedMessage(502079); // The instrument played its last tune.
                instrument.Delete();
            }
        }
    }
}

namespace Server.Spells.Bardo
{
    public abstract class BardoSpell : Spell
    {
        //                                            Circulo:  1  2  3   4   5   6   7   8   9   10   11
        private static readonly int[] m_ManaTable = new int[] { 4, 6, 9, 13, 19, 28, 42, 63, 94, 141, 211 };
        private const double ChanceOffset = 20.0, ChanceLength = 120.0 / 10.0; //originalmente era: ChanceOffset = 20.0, ChanceLength = 100.0 /7.0
        protected const int SpellEffectHue = 1070;

        public BardoSpell(Mobile caster, Item scroll, SpellInfo info)
            : base(caster, scroll, info)
        {
            ExpiryMessage = "The effect of the musical spell wears off.";
            Duration = 0;
            Targets = new ArrayList();
            IsHkCheck = false;

            IsDamageOverTime = false;
        }

        public static int MusicSkill(Mobile m)
        {
            return (int)(m.Skills[SkillName.Tocar].Value + m.Skills[SkillName.Provocacao].Value + m.Skills[SkillName.Caos].Value);
        }

        public override SkillName CastSkill
        {
            get
            {
                return SkillName.Caos;
            }
        }
        private string m_ExpiryMessage;

        public string ExpiryMessage
        {
            get { return m_ExpiryMessage; }
            set { m_ExpiryMessage = value; }
        }

        public override SkillName DamageSkill
        {
            get
            {
                return SkillName.PoderMagico;
            }
        }
        //public override int CastDelayBase{ get{ return base.CastDelayBase; } } // Reference, 3
        public override bool ClearHandsOnCast
        {
            get
            {
                return false;
            }
        }

        public abstract double RequiredSkill { get; }
        private int m_Duration;
        private ArrayList m_Targets;
        private bool m_isHkCheck;
        private bool m_DamageOverTime;

        public bool IsDamageOverTime
        {
            get { return m_DamageOverTime; }
            set { m_DamageOverTime = value; }
        }

        public abstract SpellCircle Circle { get; }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromMilliseconds(((4 + (int)Circle) * CastDelaySecondsPerTick) * 1000);
            }
        }

        public override bool ConsumeReagents()
        {
            if (base.ConsumeReagents())
                return true;
            else
                return false;
        }

        public ArrayList Targets
        {
            get { return m_Targets; }
            set { m_Targets = value; }
        }

        public int Duration
        {
            get { return m_Duration; }
            set { m_Duration = value; }
        }


        public override void GetCastSkills(out double min, out double max)
        {
            int circle = (int)Circle;

            if (Scroll != null)
                circle -= 2;

            double avg = ChanceLength * circle;

            min = avg - ChanceOffset;
            max = avg + ChanceOffset;
        }

        public override int GetMana()
        {
            if (Scroll is BaseWand)
                return 0;

            return m_ManaTable[(int)Circle];
        }

        public virtual bool CheckResisted(Mobile target)
        {
            double n = GetResistPercent(target);

            n /= 100.0;

            if (n <= 0.0)
                return false;

            if (n >= 1.0)
                return true;

            int maxSkill = (1 + (int)Circle) * 10;
            maxSkill += (1 + ((int)Circle / 6)) * 25;

            if (target.Skills[SkillName.ResistenciaMagica].Value < maxSkill)
                target.CheckSkill(SkillName.ResistenciaMagica, 0.0, target.Skills[SkillName.ResistenciaMagica].Cap);

            return (n >= Utility.RandomDouble());
        }


        public override void DoFizzle()
        {
            Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1115710); // The song fizzles.
                                                                              // 1115737 You feel invigorated by the bard's spellsong
                                                                              // 1115758 The bard's song fills you with resilience
                                                                              // 1115759 The bard's song fills you with invincible
                                                                              // 1115774 You halt your spellsong
                                                                              // 1115938 Your spellsong has finished
                                                                              // 1149722 Your spellsong has ended
        }

        public virtual double GetResistPercentForCircle(Mobile target, SpellCircle circle)
        {
            double value = GetResistSkill(target);
            double firstPercent = value / 5.0;
            double secondPercent = value - (((Caster.Skills[CastSkill].Value - 20.0) / 5.0) + (1 + (int)circle) * 5.0);

            return (firstPercent > secondPercent ? firstPercent : secondPercent) / 2.0; // Seems should be about half of what stratics says.
        }

        
        public virtual double GetResistPercent(Mobile target)
        {
            return GetResistPercentForCircle(target, Circle);
        }

        public static int Buff(Mobile m, string category)
        {
            int value = 10;
            double var = 2.0;
            if (m.Skills[SkillName.PoderMagico].Value >= Utility.RandomMinMax(1, 400)) { var = 1.5; }
            else if (m.Skills[SkillName.PoderMagico].Value >= Utility.RandomMinMax(1, 200)) { var = 1.8; }

            int time = 10;                                                              // MIN 10
            int skill1 = (int)(m.Skills[SkillName.Carisma].Value / 2);                  // MAX 60
            int skill2 = (int)(m.Skills[SkillName.PoderMagico].Value);                      // MAX 120
            int TotalTime = (int)((time + skill1 + skill2));

            int buff_default = 10;                                                      // +10 DEFAULT
            int buff_skill1 = (int)(m.Skills[SkillName.Carisma].Value / 4);             // +25 MAX
            int buff_skill2 = (int)(m.Skills[SkillName.PoderMagico].Value / 2);             // +60 MAX
            int TotalBuff = (buff_default + buff_skill1 + buff_skill2);

            int skill = 20;                                                             // MIN 20
            int skb_skill1 = (int)(m.Skills[SkillName.Carisma].Value / 2);              // MAX 60
            int skb_skill2 = (int)(m.Skills[SkillName.PoderMagico].Value);                  // MAX 120
            int TotalSkill = (int)(skill + skb_skill1 + skb_skill2);

            int damage = 1;                                                             // MIN 1
            int dmg_skill1 = (int)(m.Skills[SkillName.Carisma].Value / 25);                 // MAX 4
            int dmg_skill2 = (int)(m.Skills[SkillName.PoderMagico].Value / 15);                 // MAX 8
            int TotalDamage = (int)(damage + dmg_skill1 + dmg_skill2);

            int TotalPoison = (int)(m.Skills[SkillName.PoderMagico].Value / 25) + 1;            // MAX 5

            if (category == "time") { value = (int)(TotalTime / var); }
            else if (category == "strength") { value = (int)(TotalBuff / var); }
            else if (category == "skills") { value = (int)(TotalSkill / var); }
            else if (category == "damage") { value = (int)(TotalDamage / var); }
            else if (category == "poison") { value = (int)(TotalPoison / var); }
            else if (category == "hurts") { value = TotalBuff; }
            else if (category == "range") { value = TotalPoison; }

            return value;
        }

        public bool IsHkCheck
        {
            get { return m_isHkCheck; }
            set { m_isHkCheck = value; }
        }
        public override TimeSpan GetCastDelay()
        {
            if (!Core.ML && Scroll is BaseWand)
                return TimeSpan.Zero;

            if (!Core.AOS)
                return TimeSpan.FromSeconds(0.5 + (0.25 * (int)Circle));

            return base.GetCastDelay();
        }

      
    }
}


