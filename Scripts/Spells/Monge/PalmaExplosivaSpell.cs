using System;
using Server.Targeting;

namespace Server.Spells.Monge
{
    public class PalmaExplosivaSpell :  MongeSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Palma Explosiva", "Ignis Palma",
            230,
            9041,
            Reagent.Bloodmoss,
            Reagent.MandrakeRoot);

        public override int EficienciaMagica(Mobile caster) { return 4; } //Servirá para calcular o modificador na eficiência das magias

        public PalmaExplosivaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Seventh;
            }
        }
        public override double RequiredSkill
        {
            get
            {
                return 70.0;
            }
        }
       
        public override bool DelayedDamageStacking
        {
            get
            {
                return !Core.AOS;
            }
        }
        public override bool DelayedDamage
        {
            get
            {
                return false;
            }
        }
        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(IDamageable m)
        {
            if (Core.SA && HasDelayContext(m))
            {
                DoHurtFizzle();
                return;
            }

            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (Caster.CanBeHarmful(m) && CheckSequence())
            {
                Mobile attacker = Caster;

                SpellHelper.Turn(Caster, m);

                SpellHelper.CheckReflect((int)Circle, Caster, ref m);

                InternalTimer t = new InternalTimer(this, attacker, m);
                t.Start();
            }

            FinishSequence();
        }

        private class InternalTimer : Timer
        {
            private readonly MongeSpell m_Spell;
            private readonly IDamageable m_Target;
            private readonly Mobile m_Attacker;

            public InternalTimer(MongeSpell spell, Mobile attacker, IDamageable target)
                : base(TimeSpan.FromSeconds(Core.AOS ? 3.0 : 2.5))
            {
                m_Spell = spell;
                m_Attacker = attacker;
                m_Target = target;

                if (m_Spell != null)
                    m_Spell.StartDelayedDamageContext(attacker, this);

                Priority = TimerPriority.FiftyMS;
            }

            protected override void OnTick()
            {
                Mobile defender = m_Target as Mobile;

                if (m_Attacker.HarmfulCheck(m_Target))
                {
                    double damage = 0;

                    if (Core.AOS)
                    {
                        damage = m_Spell.GetNewAosDamage(40, 1, 5, m_Target);
                    }
                    else if (defender != null)
                    {
                        damage = Utility.Random(23, 22);

                        if (m_Spell.CheckResisted(defender))
                        {
                            damage *= 0.75;

                            defender.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                        }

                        damage *= m_Spell.GetDamageScalar(defender);
                    }

                    if (defender != null)
                    {
                        defender.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                        defender.PlaySound(0x307);
                    }
                    else
                    {
                        Effects.SendLocationParticles(m_Target, 0x36BD, 20, 10, 5044);
                        Effects.PlaySound(m_Target.Location, m_Target.Map, 0x307);
                    }

                    if (damage > 0)
                    {
                        SpellHelper.Damage(m_Spell, m_Target, damage, 0, 100, 0, 0, 0);
                    }

                    if (m_Spell != null)
                        m_Spell.RemoveDelayedDamageContext(m_Attacker);
                }
            }
        }

        private class InternalTarget : Target
        {
            private readonly PalmaExplosivaSpell m_Owner;
            public InternalTarget(PalmaExplosivaSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is IDamageable)
                    m_Owner.Target((IDamageable)o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}
