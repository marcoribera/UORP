using System;
using System.Collections.Generic;
using System.Linq;

using Server;
using Server.Targeting;
using Server.Spells.Mysticism;

namespace Server.Spells.Bardo
{
    public class CancaoDeNinarSpell : BardoSpell
    {

        private static SpellInfo m_Info = new SpellInfo(
                "Canção de ninar", "Dorme filhinhos do meu coração...",
                230,
                9022
            );

        public CancaoDeNinarSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }
        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Tenth;
            }
        }

        public override double RequiredSkill
        {
            get
            {
                return 110.0;
            }
        }
        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public class InternalTarget : Target
        {
            private CancaoDeNinarSpell m_Owner;

            public InternalTarget(CancaoDeNinarSpell owner)
                : base(10, true, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is IPoint3D)
                    m_Owner.Target((IPoint3D)o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }

        public void Target(IPoint3D p)
        {
            if (!Caster.CanSee(p))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
            {
                Map map = Caster.Map;

                if (map == null)
                    return;

                foreach (var m in AcquireIndirectTargets(p, 3).OfType<Mobile>())
                {
                    double duration = ((Caster.Skills[CastSkill].Value + Caster.Skills[DamageSkill].Value) / 20) + 3;
                    duration -= GetResistSkill(m) / 10;

                    if (duration > 0)
                    {
                        Caster.DoHarmful(m);

                        SleepSpell.DoSleep(Caster, m, TimeSpan.FromSeconds(duration));
                    }
                }
            }

            FinishSequence();
        }
    }
}
