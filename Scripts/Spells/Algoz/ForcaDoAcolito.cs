using System;
using Server.Targeting;

namespace Server.Spells.Algoz
{
    public class ForcaDoAcolitoSpell : AlgozSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Força do Acólito", "Forcis Sup",
            212,
            9061,
            Reagent.MandrakeRoot,
            Reagent.Nightshade);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias
        public ForcaDoAcolitoSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Third;
            }
        }
        
        public override void OnCast()
        {
            Mobile m = this.Caster;
            if (this.CheckBSequence(m))
            {
                SpellHelper.Turn(this.Caster, m);

                int oldStr = SpellHelper.GetBuffOffset(m, StatType.Str);
                int newStr = EficienciaMagica(this.Caster) * SpellHelper.GetOffset(Caster, m, StatType.Str, false, true);

                if (newStr < oldStr || newStr == 0)
                {
                    DoHurtFizzle();
                }
                else
                {
                    SpellHelper.AddStatBonus(this.Caster, m, false, StatType.Str);
                    int percentage = (int)(SpellHelper.GetOffsetScalar(this.Caster, m, true) * 100);
                    TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                    BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Strength, 1075845, length, m, percentage.ToString()));

                    m.FixedParticles(0x375A, 10, 15, 5017, 31, 3, EffectLayer.Waist);

                    m.PlaySound(0x1EE);
                }
            }

            this.FinishSequence();
        }
    }
}
