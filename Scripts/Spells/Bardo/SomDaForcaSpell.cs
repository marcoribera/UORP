using System;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Bardo
{
    public class SomDaForcaSpell : BardoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Som da Força", "Uau, que musculoso!",
            212,
            9061);
        public SomDaForcaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

     
        public override int EficienciaMagica(Mobile caster) { return 5; } //Servirá para calcular o modificador na eficiência das magias
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Third;
            }
        }

        public override double RequiredSkill
        {
            get
            {
                return 30.0;
            }
        }
        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }
public override bool CheckCast()
        {
            // Check for a musical instrument in the player's backpack
            if (!CheckInstrument())
            {
                Caster.SendMessage("Você precisa ter um instrumento musical na sua mochila para canalizar essa magia.");
                return false;
            }


            return base.CheckCast();
        }


 private bool CheckInstrument()
        {
            return Caster.Backpack.FindItemByType(typeof(BaseInstrument)) != null;
        }


        private BaseInstrument GetInstrument()
        {
            return Caster.Backpack.FindItemByType(typeof(BaseInstrument)) as BaseInstrument;
        }

        public void Target(Mobile m)
        {
            if (!this.Caster.CanSee(m))
            {
                this.Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (this.CheckBSequence(m))
            {
                SpellHelper.Turn(this.Caster, m);

                int oldStr = SpellHelper.GetBuffOffset(m, StatType.Str);
                int newStr = SpellHelper.GetOffset(this,Caster, m, StatType.Str, false, true);

                if (newStr < oldStr || newStr == 0)
                {
                    DoHurtFizzle();
                }
                else
                {
                    SpellHelper.AddStatBonus(this, this.Caster, m, false, StatType.Str);
                    int percentage = (int)(SpellHelper.GetOffsetScalar(this,this.Caster, m, false) * 100);
                    TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                    BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Strength, 1075845, length, m, percentage.ToString()));

                    m.FixedParticles(0x375A, 10, 15, 5017, EffectLayer.Waist);
                    m.PlaySound(0x1EE);
                }
            }

            this.FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly SomDaForcaSpell m_Owner;
            public InternalTarget(SomDaForcaSpell owner)
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
