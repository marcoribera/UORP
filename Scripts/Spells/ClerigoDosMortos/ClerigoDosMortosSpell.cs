using System;
using Server.Items;


namespace Server.Spells.ClerigoDosMortos
{
    public abstract class ClerigoDosMortosSpell : Spell
    {
        //                                            Circulo:  1  2  3   4   5   6   7   8   9   10   11
        private static readonly int[] m_ManaTable = new int[] { 4, 6, 9, 13, 19, 28, 42, 63, 94, 141, 211 };
        private const double ChanceOffset = 20.0, ChanceLength = 120.0 / 10.0; //originalmente era: ChanceOffset = 20.0, ChanceLength = 100.0 /7.0
        public ClerigoDosMortosSpell(Mobile caster, Item scroll, SpellInfo info)
            : base(caster, scroll, info)
        {
        }


        public override SkillName CastSkill
        {
            get
            {
                return SkillName.Necromancia;
            }
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

        public static double GetClerigoDosMortosDamage(Mobile from)
        {
            int karma = from.Karma;
            if (karma < 1) { karma = 0; }
            if (karma > 15000) { karma = 15000; }

            double hate = karma / 120; // MAX 125
            hate = hate + from.Skills[SkillName.Cortante].Value + from.Skills[SkillName.Cortante].Value; // MAX 375

            return hate;
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
