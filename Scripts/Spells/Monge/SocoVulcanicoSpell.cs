using System;
using System.Collections.Generic;

namespace Server.Spells.Monge
{
    public class SocoVulcanicoSpell : MongeSpell
    {
        public override DamageType SpellDamageType { get { return DamageType.SpellAOE; } }

        private static readonly SpellInfo m_Info = new SpellInfo(
            "Soco Vulcânico", "Volcanus Impetus",
            233,
            9012,
            false,
            Reagent.SulfurousAsh,
            Reagent.SulfurousAsh,
            Reagent.SulfurousAsh,
            Reagent.SulfurousAsh,
            Reagent.SulfurousAsh);

        public SocoVulcanicoSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override double RequiredSkill
        {
            get
            {
                return 120.0;
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Eleventh;
            }
        }

        public override int EficienciaMagica(Mobile caster) { return 4; } //Servirá para calcular o modificador na eficiência das magias

        public override bool DelayedDamage
        {
            get
            {
                return !Core.AOS;
            }
        }
        public override void OnCast()
        {
            if (SpellHelper.CheckTown(Caster, Caster) && CheckSequence())
            {
                foreach (var id in AcquireIndirectTargets(Caster.Location, 1 + (int)(Caster.Skills[SkillName.Arcanismo].Value / 15.0)))
                {
                    Mobile m = id as Mobile;

                    int damage;

                    if (Core.AOS)
                    {
                        damage = id.Hits / 2;

                        if (m == null || !m.Player)
                            damage = Math.Max(Math.Min(damage, 100), 15);
                        damage += Utility.RandomMinMax(0, 15);
                    }
                    else
                    {
                        damage = (id.Hits * 6) / 10;

                        if ((m == null || !m.Player) && damage < 10)
                            damage = 10;
                        else if (damage > 75)
                            damage = 75;
                    }

                    Caster.DoHarmful(id);
                    SpellHelper.Damage(this, id, damage, 100, 0, 0, 0, 0);
                }
            }

            FinishSequence();
        }
    }
}
