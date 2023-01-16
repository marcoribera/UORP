using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Monge
{
    public class MetabolizarFeridaSpell : MongeSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Metabolizar Ferida", "Expellere Injuria",
            224,
            9061,
            Reagent.Garlic,
            Reagent.Ginseng,
            Reagent.SpidersSilk);

        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public MetabolizarFeridaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override double RequiredSkill
        {
            get
            {
                return 40.0;
            }
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
                SpellHelper.Turn(this.Caster, m);

                int toHeal;

                if (Core.AOS)
                {
                    toHeal = this.Caster.Skills.Arcanismo.Fixed / 120;
                    toHeal += Utility.RandomMinMax(1, 4);

                    if (Core.SE && this.Caster != m)
                        toHeal = (int)(toHeal * 1.5);
                }
                else
                {
                    toHeal = (int)(this.Caster.Skills[SkillName.Arcanismo].Value * 0.1);
                    toHeal += Utility.Random(1, 5);
                }

                //m.Heal( toHeal, Caster );
                SpellHelper.Heal(toHeal, m, this.Caster);

                m.FixedParticles(0x376A, 9, 32, 5005, EffectLayer.Waist);
                m.PlaySound(0x1F2);
            }

            this.FinishSequence();
        }

       
    }
}
