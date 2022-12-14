using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Algoz
{
    public class BanirDemonioSpell : AlgozSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Banir Demônio", "Daemon Exsil",
            263,
            9002,
            Reagent.Garlic,
            Reagent.MandrakeRoot,
            Reagent.BlackPearl,
            Reagent.SulfurousAsh);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias

        public BanirDemonioSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Ninth;
            }
        }

        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        public void Target(IPoint3D p)
        {
            if (!this.Caster.CanSee(p))
            {
                this.Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, p);

                SpellHelper.GetSurfaceTop(ref p);

                List<Mobile> targets = new List<Mobile>();

                Map map = this.Caster.Map;

                if (map != null)
                {
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), 8);

                    foreach (Mobile m in eable)
                        if (m is BaseCreature && (m as BaseCreature).IsDispellable && (((BaseCreature)m).SummonMaster == this.Caster || this.Caster.CanBeHarmful(m, false)))
                            targets.Add(m);

                    eable.Free();
                }

                for (int i = 0; i < targets.Count; ++i)
                {
                    Mobile m = targets[i];

                    BaseCreature bc = m as BaseCreature;

                    if (bc == null)
                        continue;

                    double dispelChance = (50.0 + ((100 * (this.Caster.Skills.Ordem.Value - bc.GetDispelDifficulty())) / (bc.DispelFocus * 2))) / 100;
                    
                    // Skill Masteries
                    dispelChance -= ((double)SkillMasteries.MasteryInfo.EnchantedSummoningBonus(bc) / 100);

                    if (dispelChance > Utility.RandomDouble())
                    {
                        Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                        Effects.PlaySound(m, m.Map, 0x201);

                        m.Delete();
                    }
                    else
                    {
                        this.Caster.DoHarmful(m);

                        m.FixedEffect(0x3779, 10, 20);
                    }
                }
            }

            this.FinishSequence();
        }

        public class InternalTarget : Target
        {
            private readonly BanirDemonioSpell m_Owner;
            public InternalTarget(BanirDemonioSpell owner)
                : base(Core.ML ? 10 : 12, true, TargetFlags.None)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                IPoint3D p = o as IPoint3D;

                if (p != null)
                    this.m_Owner.Target(p);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}
