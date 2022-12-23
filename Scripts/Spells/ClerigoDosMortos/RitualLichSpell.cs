using System;
using System.Collections.Generic;
using Server.Spells.ClerigoDosMortos;
using Server.Spells.Necromancy;

namespace Server.Spells.ClerigoDosMortos
{
    public class RitualLichSpell : TransformationSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Ritual Lich", "Mortuis Rituali",
            203,
            9031,
            Reagent.GraveDust,
            Reagent.DaemonBlood,
            Reagent.NoxCrystal);
        public RitualLichSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override int EficienciaMagica(Mobile caster) { return 5; } //Servirá para calcular o modificador na eficiência das magias

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(2.25);
            }
        }

        public override int RequiredMana
        {
            get
            {
                return 211;
            }
        }



        public override double RequiredSkill
        {
            get
            {
                return 120.0;
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Eleventh;
            }
        }
        public override int Body
        {
            get
            {
                return 749;
            }
        }
        public override int FireResistOffset
        {
            get
            {
                return -10;
            }
        }
        public override int ColdResistOffset
        {
            get
            {
                return +10;
            }
        }
        public override int PoisResistOffset
        {
            get
            {
                return +10;
            }
        }
        public override double TickRate
        {
            get
            {
                return 2;
            }
        }
        public override void DoEffect(Mobile m)
        {
            m.PlaySound(0x19C);
            m.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.RightFoot);
            BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.LichForm, 1060515, 1153767, "5\t13\t10\t10\t10"));

            m.ResetStatTimers();
        }
        public override void OnTick(Mobile m)
        {
            --m.Hits;
        }

        public override void RemoveEffect(Mobile m)
        {
            BuffInfo.RemoveBuff(m, BuffIcon.LichForm);
        }
    }
}
