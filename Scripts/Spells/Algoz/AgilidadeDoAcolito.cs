using System;

namespace Server.Spells.Algoz
{
    public class AgilidadeDoAcolitoSpell : AlgozSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Agilidade do Acólito", "Agilis Sup",
            212,
            9061,
            Reagent.Bloodmoss,
            Reagent.MandrakeRoot);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias

        public AgilidadeDoAcolitoSpell(Mobile caster, Item scroll)
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
                int oldDex = SpellHelper.GetBuffOffset(m, StatType.Dex);
                int newDex = EficienciaMagica(this.Caster) * SpellHelper.GetOffset(Caster, m, StatType.Dex, false, true); //multiplica a eficiencia mágica pelo efeito base

                if (newDex < oldDex || newDex == 0)
                {
                    DoHurtFizzle();
                }
                else
                {
                    SpellHelper.Turn(this.Caster, m);

                    SpellHelper.AddStatBonus(this.Caster, m, false, StatType.Dex);
                    int percentage = (int)(SpellHelper.GetOffsetScalar(this.Caster, m, false) * 100);
                    TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                    BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Agility, 1075841, length, m, percentage.ToString()));

                    m.FixedParticles(0x375A, 10, 15, 5010, 31, 3, EffectLayer.Waist);
                    m.PlaySound(0x1e7);
                }
            }

            this.FinishSequence();
        }
    }
}
