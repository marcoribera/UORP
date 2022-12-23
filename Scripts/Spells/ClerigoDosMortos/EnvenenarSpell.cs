using System;
using Server.Spells.ClerigoDosMortos;
using Server.Targeting;

namespace Server.Spells.ClerigoDosMortos
{
    public class EnvenenarSpell : ClerigoDosMortosSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Envenenar", "Ut Venenum",
            203,
            9051,
            Reagent.Nightshade,
           Reagent.Nightshade);

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Seventh;
            }
        }

        public EnvenenarSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }
        public override int EficienciaMagica(Mobile caster) { return 4; } //Servirá para calcular o modificador na eficiência das magias


        public override double RequiredSkill
        {
            get
            {
                return 65.0;
            }
        }
        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckHSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.CheckReflect((int)Circle, Caster, ref m);

                if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

                if (CheckResisted(m) || Server.Spells.Mysticism.StoneFormSpell.CheckImmunity(m))
                {
                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                }
                else
                {
                    int level;

                    if (Core.AOS)
                    {
                        int total = (Caster.Skills.Arcanismo.Fixed + Caster.Skills.Envenenamento.Fixed) / 2;

                        if (Core.SA && Caster.InRange(m, 8))
                        {
                            int range = (int)Caster.GetDistanceToSqrt(m.Location);

                            if (total >= 1000)
                                level = Utility.RandomDouble() <= .1 ? 4 : 3;
                            else if (total > 850)
                                level = 2;
                            else if (total > 650)
                                level = 1;
                            else
                                level = 0;

                            if (!Caster.InRange(m, 2))
                                level -= range / 2;

                            if (level < 0)
                                level = 0;
                        }
                        else if (Caster.InRange(m, 2))
                        {
                            if (total >= 1000)
                                level = 3;
                            else if (total > 850)
                                level = 2;
                            else if (total > 650)
                                level = 1;
                            else
                                level = 0;
                        }
                        else
                        {
                            level = 0;
                        }
                    }
                    else
                    {
                        double total = Caster.Skills[SkillName.Arcanismo].Value + Caster.Skills[SkillName.Envenenamento].Value;                        
                        double dist = Caster.GetDistanceToSqrt(m);

                        if (dist >= 3.0)
                            total -= (dist - 3.0) * 10.0;

                        if (total >= 200.0 && 1 > Utility.Random(10))
                            level = 3;
                        else if (total > (Core.AOS ? 170.1 : 170.0))
                            level = 2;
                        else if (total > (Core.AOS ? 130.1 : 130.0))
                            level = 1;
                        else
                            level = 0;
                    }

                    m.ApplyPoison(Caster, Poison.GetPoison(level));
                }

                m.FixedParticles(0x374A, 10, 15, 5021, EffectLayer.Waist);
                m.PlaySound(0x205);

                HarmfulSpell(m);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly EnvenenarSpell m_Owner;

            public InternalTarget(EnvenenarSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}
