using System;
using Server.Targeting;

namespace Server.Spells.Monge
{
    public class GolpesFortesSpell : MongeSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Golpes Fortes", "Fortis Impetus",
            212,
            9061,
            Reagent.MandrakeRoot,
            Reagent.Bloodmoss);

        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public GolpesFortesSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override double RequiredSkill
        {
            get
            {
                return 30.0;
            }
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
                SpellHelper.Turn(this.Caster, m);

                int oldStr = SpellHelper.GetBuffOffset(m, StatType.Str);
                int newStr = SpellHelper.GetOffset(this, Caster, m, StatType.Str, false, true);

                if (newStr < oldStr || newStr == 0)
                {
                    DoHurtFizzle();
                }
                else
                {
                    SpellHelper.AddStatBonus(this, this.Caster, m, false, StatType.Str);
                    int percentage = (int)(SpellHelper.GetOffsetScalar(this, this.Caster, m, false) * 100);
                    TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                    BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Strength, 1075845, length, m, percentage.ToString()));

                    m.FixedParticles(0x375A, 10, 15, 5017, EffectLayer.Waist);
                    m.PlaySound(0x1EE);
                }
            }

            this.FinishSequence();

        }

        
    }
            
}
