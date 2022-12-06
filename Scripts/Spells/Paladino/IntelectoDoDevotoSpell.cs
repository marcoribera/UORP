using System;
using Server.Targeting;

namespace Server.Spells.Paladino
{
    public class IntelectoDoDevotoSpell : PaladinoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Intelecto do Devoto", "Intelec Devot",
            212,
            9061,
            Reagent.MandrakeRoot,
            Reagent.Nightshade);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias


        public IntelectoDoDevotoSpell(Mobile caster, Item scroll)
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
            this.Caster.Target = new InternalTarget(this);
        }
        
        public override int MantraNumber
        {
            get
            {
                return 1060722;
            }
        }

        public override double RequiredSkill
        {
            get
            {
                return 0.0;
            }
        }

        public override int RequiredMana
        {
            get
            {
                return 15;
            }
        }

        public void Target(Mobile m)
        {
            if (!this.Caster.CanSee(m))
            {
                this.Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (this.CheckBSequence(m))
            {
                int oldInt = SpellHelper.GetBuffOffset(m, StatType.Int);
                int newInt = SpellHelper.GetOffset(Caster, m, StatType.Int, false, true);

                if (newInt < oldInt || newInt == 0)
                {
                    DoHurtFizzle();
                }
                else
                {
                    SpellHelper.Turn(this.Caster, m);

                    SpellHelper.AddStatBonus(this.Caster, m, false, StatType.Int);
                    int percentage = (int)(SpellHelper.GetOffsetScalar(this.Caster, m, false) * 100);
                    TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                    BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Cunning, 1075843, length, m, percentage.ToString()));

                    m.FixedParticles(0x375A, 10, 15, 5011, EffectLayer.Head);
                    m.PlaySound(0x1EB);
                }
            }

            this.FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly IntelectoDoDevotoSpell m_Owner;
            public InternalTarget(IntelectoDoDevotoSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Beneficial)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    this.m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}
