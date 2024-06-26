using System;
using Server.Mobiles;
using Server.Spells.Chivalry;
using Server.Targeting;

namespace Server.Spells.Monge
{
    public class GolpeAtordoanteSpell : MongeSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Golpe Atordoante", "Impetus Attonitus",
            218,
            9012
            );

        public override int EficienciaMagica(Mobile caster) { return 4; } //Servirá para calcular o modificador na eficiência das magias

        public GolpeAtordoanteSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override double RequiredSkill
        {
            get
            {
                return 50.0;
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Seventh;
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
            else if (Core.AOS && (m.Frozen || m.Paralyzed || (m.Spell != null && m.Spell.IsCasting && !(m.Spell is PaladinSpell))))
            {
                this.Caster.SendLocalizedMessage(1061923); // The target is already frozen.
            }
            else if (this.CheckHSequence(m))
            {
                SpellHelper.Turn(this.Caster, m);

                SpellHelper.CheckReflect((int)this.Circle, this.Caster, ref m);

                double duration;
				
                if (Core.AOS)
                {
                    int secs = (int)((this.GetDamageSkill(this.Caster) / 10) - (this.GetResistSkill(m) / 10));
					
                    if (!Core.SE)
                        secs += 2;

                    if (!m.Player)
                        secs *= 3;

                    if (secs < 0)
                        secs = 0;

                    duration = secs;
                }
                else
                {
                    // Algorithm: ((20% of magery) + 7) seconds [- 50% if resisted]
                    duration = 7.0 + (this.Caster.Skills[SkillName.Arcanismo].Value * 0.2);

                    if (this.CheckResisted(m))
                        duration *= 0.75;
                }

                if (m is PlagueBeastLord)
                {
                    ((PlagueBeastLord)m).OnParalyzed(this.Caster);
                    duration = 120;
                }

                m.Paralyze(TimeSpan.FromSeconds(duration));

                m.PlaySound(0x204);
                m.FixedEffect(0x376A, 6, 1);

                this.HarmfulSpell(m);
            }

            this.FinishSequence();
        }

        public class InternalTarget : Target
        {
            private readonly GolpeAtordoanteSpell m_Owner;
            public InternalTarget(GolpeAtordoanteSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    this.m_Owner.Target((Mobile)o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}
