using System;
using Server.Items;

using System.Collections.Generic;
using Server.Mobiles;

using Server.Spells.Necromancy;





namespace Server.Spells.Algoz
{
    public class FormaVampiricaSpell : TransformationSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Forma Vampirica", "Sanguis Corpus",
            203,
            9031,
            Reagent.BatWing,
            Reagent.NoxCrystal,
            Reagent.PigIron);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias

        public FormaVampiricaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
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
                return 99.0;
            }
        }
        /*public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Tenth;
            }
        }*/
        public override int RequiredMana
        {
            get
            {
                return 23;
            }
        }
        public override int Body
        {
            get
            {
                if (Caster.Race == Race.Gargoyle)
                {
                    return Caster.Female ? 667 : 666;
                }

                return Caster.Female ? Caster.Race.FemaleBody : Caster.Race.MaleBody;
            }
        }
        public override int Hue
        {
            get
            {
                return 0x847E;
            }
        }
        public override int FireResistOffset
        {
            get
            {
                return -25;
            }
        }
        public override void GetCastSkills(out double min, out double max)
        {
            if (this.Caster.Skills[this.CastSkill].Value >= this.RequiredSkill)
            {
                min = 80.0;
                max = 120.0;
            }
            else
            {
                base.GetCastSkills(out min, out max);
            }
        }

        public override void DoEffect(Mobile m)
        {
            Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x373A, 1, 17, 1108, 7, 9914, 0);
            Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x376A, 1, 22, 67, 7, 9502, 0);
            Effects.PlaySound(m.Location, m.Map, 0x4B1);

            BuffInfo.AddBuff(Caster, new BuffInfo(BuffIcon.VampiricEmbrace, 1028812, 1153768, String.Format("{0}\t{1}\t{2}\t{3}", "20", "15", "3", "25")));

			if (Caster.Skills.Necromancia.Value > 99.0)
                BuffInfo.AddBuff(Caster, new BuffInfo(BuffIcon.PoisonImmunity, 1153785, 1153814));

            m.ResetStatTimers();
		}

		public override void RemoveEffect(Mobile m)
		{
			BuffInfo.RemoveBuff(Caster, BuffIcon.PoisonImmunity);
			BuffInfo.RemoveBuff(Caster, BuffIcon.VampiricEmbrace);
		}
    }
}
