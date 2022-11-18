using System;

namespace Server.Spells.Algoz
{
    public class IntelectoDoAcolitoSpell : AlgozSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Intelecto do Acólito", "Intelis Sup",
            212,
            9061,
            Reagent.MandrakeRoot,
            Reagent.Nightshade);

        private static int EficienciaMagica = 1;
        public IntelectoDoAcolitoSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Second;
            }
        }
        
        public override void OnCast()
        {
            Mobile m = this.Caster;

            if (this.CheckBSequence(m))
            {
                int oldInt = SpellHelper.GetBuffOffset(m, StatType.Int);
                int newInt = EficienciaMagica * SpellHelper.GetOffset(Caster, m, StatType.Int, false, true);

                if (newInt < oldInt || newInt == 0)
                {
                    DoHurtFizzle();
                }
                else
                {
                    SpellHelper.Turn(this.Caster, m);

                    SpellHelper.AddStatBonus(this.Caster, m, false, StatType.Int);
                    int percentage = (int)(SpellHelper.GetOffsetScalar(this.Caster, m, true) * 100);
                    TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                    BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Cunning, 1075843, length, m, percentage.ToString()));

                    m.FixedParticles(0x3779, 10, 15, 5011, 31, 3, EffectLayer.Head); //é pra ser um vermelho 31
                    m.PlaySound(0x1EB);
                }
            }

            this.FinishSequence();
        }
    }
}
