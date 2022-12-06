using System;
using Server.Targeting;

namespace Server.Spells.Algoz
{
    public class ToqueDaDorSpell : AlgozSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Toque da Dor", "Levi Dolore",
            212,
            Core.AOS ? 9001 : 9041,
            Reagent.Nightshade,
            Reagent.SpidersSilk);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias

        public ToqueDaDorSpell(Mobile caster, Item scroll)
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
        public override bool DelayedDamage
        {
            get
            {
                return false;
            }
        }
        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        public override double GetSlayerDamageScalar(Mobile target)
        {
            return 1.0; //This spell isn't affected by slayer spellbooks
        }

        public void Target(IDamageable m)
        {
            Mobile mob = m as Mobile;

            if (!this.Caster.CanSee(m))
            {
                this.Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (this.CheckHSequence(m))
            {
                if (!this.Caster.InRange(m, 1))
                {
                    Caster.SendMessage("Toque da Dor só atinge alvos ao alcance do toque.");
                    this.FinishSequence();
                }
                else
                {
                    SpellHelper.Turn(this.Caster, m);
                    Mobile source = this.Caster;

                    SpellHelper.CheckReflect((int)this.Circle, ref source, ref m);

                    double damage = 0;

                    if (Core.AOS)
                    {
                        damage = GetNewAosDamage(17, 1, 5, m);
                    }
                    else if (mob != null)
                    {
                        damage = Utility.Random(1, 15);

                        if (this.CheckResisted(mob))
                        {
                            damage *= 0.75;

                            mob.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                        }

                        damage *= this.GetDamageScalar(mob);
                    }

                    damage *= EficienciaMagica(this.Caster); //Aplica o multiplicador de eficiência da magia

                    if (Core.AOS)
                    {
                        if (mob != null)
                        {
                            mob.FixedParticles(0x374A, 10, 30, 5013, 31, 2, EffectLayer.Waist); //31 é pra ser um vermelho
                            mob.PlaySound(0x0FC);
                        }
                        else
                        {
                            Effects.SendLocationParticles(m, 0x374A, 10, 30, 31, 2, 5013, 0);
                            Effects.PlaySound(m.Location, m.Map, 0x0FC);
                        }
                    }
                    else if (mob != null)
                    {
                        mob.FixedParticles(0x374A, 10, 15, 5013, 31, 2, EffectLayer.Waist); //31 é pra ser um vermelho
                        mob.PlaySound(0x1F1);
                    }

                    if (damage > 0)
                    {
                        SpellHelper.Damage(this, m, damage, 0, 0, 0, 0, 0, 0, 100);
                    }
                }
            }
            this.FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly ToqueDaDorSpell m_Owner;
            public InternalTarget(ToqueDaDorSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is IDamageable)
                {
                    this.m_Owner.Target((IDamageable)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}
