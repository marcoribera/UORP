using System;
using Server.Targeting;
using System.Collections.Generic;

namespace Server.Spells.Algoz
{
    public class BencaoProfanaSpell : AlgozSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Benção Profana", "Benedic Profanus",
            203,
            9061,
            Reagent.Garlic,
            Reagent.MandrakeRoot);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias

        private static Dictionary<Mobile, InternalTimer> _Table;

        public static bool IsBlessed(Mobile m)
        {
            return _Table != null && _Table.ContainsKey(m);
        }

        public static void AddBless(Mobile m, TimeSpan duration)
        {
            if (_Table == null)
                _Table = new Dictionary<Mobile, InternalTimer>();

            if (_Table.ContainsKey(m))
            {
                _Table[m].Stop();
            }

            _Table[m] = new InternalTimer(m, duration);
        }

        public static void RemoveBless(Mobile m, bool early = false)
        {
            if (_Table != null && _Table.ContainsKey(m))
            {
                _Table[m].Stop();
                m.Delta(MobileDelta.Stat);

                _Table.Remove(m);
            }
        }

        public BencaoProfanaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Fourth;
            }
        }

        public override void OnCast()
        {
            Mobile m = this.Caster;

            if (this.CheckBSequence(m))
            {
                int oldStr = SpellHelper.GetBuffOffset(m, StatType.Str);
                int oldDex = SpellHelper.GetBuffOffset(m, StatType.Dex);
                int oldInt = SpellHelper.GetBuffOffset(m, StatType.Int);

                int newStr = SpellHelper.GetOffset(this, Caster, m, StatType.Str, false, true);
                int newDex = SpellHelper.GetOffset(this, Caster, m, StatType.Dex, false, true);
                int newInt = SpellHelper.GetOffset(this, Caster, m, StatType.Int, false, true);

                if ((newStr < oldStr && newDex < oldDex && newInt < oldInt) ||
                    (newStr == 0 && newDex == 0 && newInt == 0))
                {
                    DoHurtFizzle();
                }
                else
                {
                    SpellHelper.AddStatBonus(this, this.Caster, m, false, StatType.Str);
                    SpellHelper.AddStatBonus(this, this.Caster, m, true, StatType.Dex);
                    SpellHelper.AddStatBonus(this, this.Caster, m, true, StatType.Int);

                    int percentage = (int)(SpellHelper.GetOffsetScalar(this, this.Caster, m, false) * 100);
                    TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                    string args = String.Format("{0}\t{1}\t{2}", percentage, percentage, percentage);
                    BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Bless, 1075847, 1075848, length, m, args.ToString()));

                    m.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
                    m.PlaySound(0x1EA);

                    AddBless(Caster, length + TimeSpan.FromMilliseconds(50));
                }
            }

            this.FinishSequence();
        }

        private class InternalTimer : Timer
        {
            public Mobile Mobile { get; set; }

            public InternalTimer(Mobile m, TimeSpan duration)
                : base(duration)
            {
                Mobile = m;
                Start();
            }

            protected override void OnTick()
            {
                BencaoProfanaSpell.RemoveBless(Mobile);
            }
        }
    }
}
