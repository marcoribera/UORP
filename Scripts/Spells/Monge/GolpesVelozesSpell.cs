using System;
using Server.Targeting;

namespace Server.Spells.Monge
{
    public class GolpesVelozesSpell : MongeSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Golpes Velozes", "Celeriter Impetus",
            212,
            9061,
            Reagent.Bloodmoss,
            Reagent.MandrakeRoot);

        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public GolpesVelozesSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override double RequiredSkill
        {
            get
            {
                return 20.0;
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
            }
        }

        public override void OnCast()
        {
            Mobile m = this.Caster;
       
            if (this.CheckBSequence(m))
            {
                int oldDex = SpellHelper.GetBuffOffset(m, StatType.Dex);
                int newDex = SpellHelper.GetOffset(this,Caster, m, StatType.Dex, false, true);

                if (newDex < oldDex || newDex == 0)
                {
                    DoHurtFizzle();
                }
                else
                {
                    SpellHelper.Turn(this.Caster, m);

                    SpellHelper.AddStatBonus(this, this.Caster, m, false, StatType.Dex);
                    int percentage = (int)(SpellHelper.GetOffsetScalar(this,this.Caster, m, false) * 100);
                    TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                    BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Agility, 1075841, length, m, percentage.ToString()));

                    m.FixedParticles(0x375A, 10, 15, 5010, EffectLayer.Waist);
                    m.PlaySound(0x1e7);
                }
            }

            this.FinishSequence();
        }

        
    }
}
