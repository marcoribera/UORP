using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Spells.Necromancy;


namespace Server.Spells.Algoz
{
    public class EspiritoMalignoSpell : AlgozTransforma
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Espirito Maligno", "Espiritus Malus",
            203,
            9031,
            Reagent.NoxCrystal,
            Reagent.PigIron);
        public EspiritoMalignoSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Eleventh;
            }
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(5.0);
            }
        }
       
        public override int Body
        {
            get
            {
                return this.Caster.Female ? 747 : 748; // TODO: Escolher animação da forma
            }
        }
        public override int Hue
        {
            get
            {
                return this.Caster.Female ? 0 : 0x4001; // TODO: Escolher cor da forma
            }
        }

        //TODO: DEFINIR QUAIS OS BONUS E REDUTORES DESSA FORMA EM ESPECÍFICO
        public override int PhysResistOffset
        {
            get
            {
                return +50;
            }
        }
        public override int FireResistOffset
        {
            get
            {
                return +100;
            }
        }
        public override int ColdResistOffset
        {
            get
            {
                return +100;
            }
        }
        public override int PoisResistOffset
        {
            get
            {
                return +100;
            }
        }
        public override int NrgyResistOffset
        {
            get
            {
                return -100;
            }
        }
        public override void DoEffect(Mobile m)
        {
            if (m is PlayerMobile)
                ((PlayerMobile)m).IgnoreMobiles = true;

            m.PlaySound(0x17F);
            m.FixedParticles(0x374A, 1, 15, 9902, SpellEffectHue, 4, EffectLayer.Waist);

            int manadrain = (int)(m.Skills.PoderMagico.Value / 5);

            BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.WraithForm, 1060524, 1153829, String.Format("15\t5\t5\t{0}", manadrain)));
        }

        public override void RemoveEffect(Mobile m)
        {
            if (m is PlayerMobile && m.IsPlayer())
                ((PlayerMobile)m).IgnoreMobiles = false;

            BuffInfo.RemoveBuff(m, BuffIcon.WraithForm);
        }
    }
}
