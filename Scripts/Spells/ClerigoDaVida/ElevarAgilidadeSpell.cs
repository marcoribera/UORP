using System;
using Server.Targeting;

namespace Server.Spells.ClerigoDaVida
{
    public class ElevarAgilidadeSpell : ClerigoDaVidaSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Elevar Agilidade", "Improve Agilitas",
            212,
            9061,
            Reagent.Bloodmoss,
            Reagent.MandrakeRoot);

        public ElevarAgilidadeSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public override double RequiredSkill
        {
            get
            {
                return 10.0;
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
            this.Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!this.Caster.CanSee(m))
            {
                this.Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (this.CheckBSequence(m))
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

        private class InternalTarget : Target
        {
            private readonly ElevarAgilidadeSpell m_Owner;
            public InternalTarget(ElevarAgilidadeSpell owner)
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
