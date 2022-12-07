using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Spells.Necromancy;

// TODO: reformular essa magia que não está como PaladinoSpell
namespace Server.Spells.Paladino
{
    public class EspiritoBenignoSpell : TransformationSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Espirito Benigno", "Benignus Spirit",
            203,
            9031,
            Reagent.NoxCrystal,
            Reagent.PigIron);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias

        public EspiritoBenignoSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }
        public SpellCircle Circle
        {
            get
            {
                return SpellCircle.Tenth;
            }
        }
        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(2.25);
            }
        }
        public override double RequiredSkill
        {
            get
            {
                return 20.0;
            }
        }
        public override int RequiredMana
        {
            get
            {
                return 17;
            }
        }
        public override int Body
        {
            get
            {
                return this.Caster.Female ? 747 : 748;
            }
        }
        public override int Hue
        {
            get
            {
                return this.Caster.Female ? 0 : 0x4001;
            }
        }
        public override int PhysResistOffset
        {
            get
            {
                return +15;
            }
        }
        public override int FireResistOffset
        {
            get
            {
                return -5;
            }
        }
        public override int ColdResistOffset
        {
            get
            {
                return 0;
            }
        }
        public override int PoisResistOffset
        {
            get
            {
                return 0;
            }
        }
        public override int NrgyResistOffset
        {
            get
            {
                return -5;
            }
        }
        public override void DoEffect(Mobile m)
        {
            if (m is PlayerMobile)
                ((PlayerMobile)m).IgnoreMobiles = true;

            m.PlaySound(0x17F);
            m.FixedParticles(0x374A, 1, 15, 9902, 1108, 4, EffectLayer.Waist);

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
