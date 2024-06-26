using System;
using System.Collections.Generic;
using Server.Engines.PartySystem;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Bardo
{
    public class SomDoFocoSpell : BardoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Som do Foco", "Observem melhor!",
            Core.AOS ? 239 : 215,
            9011);
        public SomDoFocoSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Sixth;
            }
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

        public override double RequiredSkill
        {
            get
            {
                return 60.0;
            }
        }
        public override int EficienciaMagica(Mobile caster) { return 5; } //Servirá para calcular o modificador na eficiência das magias

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
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), Core.AOS ? 2 : 3);

                    foreach (Mobile m in eable)
                    {
                        if (this.Caster.CanBeBeneficial(m, false))
                            targets.Add(m);
                    }

                    eable.Free();
                }

                if (Core.AOS)
                {
                    Party party = Party.Get(this.Caster);

                    for (int i = 0; i < targets.Count; ++i)
                    {
                        Mobile m = targets[i];

                        if (m == this.Caster || (party != null && party.Contains(m)))
                        {
                            this.Caster.DoBeneficial(m);
                            Spells.Second.ProtectionSpell.Toggle(this.Caster, m, true);
                        }
                    }
                }
                else
                {
                    Effects.PlaySound(p, this.Caster.Map, 0x299);

                    int val = (int)(this.Caster.Skills[SkillName.Arcanismo].Value / 10.0 + 1);

                    if (targets.Count > 0)
                    {
                        for (int i = 0; i < targets.Count; ++i)
                        {
                            Mobile m = targets[i];

                            if (m.BeginAction(typeof(SomDoFocoSpell)))
                            {
                                this.Caster.DoBeneficial(m);
                                m.VirtualArmorMod += val;

                                AddEntry(m, val);
                                new InternalTimer(m, this.Caster).Start();

                                m.FixedParticles(0x375A, 9, 20, 5027, EffectLayer.Waist);
                                m.PlaySound(0x1F7);
                            }
                        }
                    }
                }
            }

            this.FinishSequence();
        }

        private static Dictionary<Mobile, Int32> _Table = new Dictionary<Mobile, Int32>();

        private static void AddEntry(Mobile m, Int32 v)
        {
            _Table[m] = v;
        }

        public static void RemoveEntry(Mobile m)
        {
            if (_Table.ContainsKey(m))
            {
                int v = _Table[m];
                _Table.Remove(m);
                m.EndAction(typeof(SomDoFocoSpell));
                m.VirtualArmorMod -= v;
                if (m.VirtualArmorMod < 0)
                    m.VirtualArmorMod = 0;
            }
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_Owner;

            public InternalTimer(Mobile target, Mobile caster)
                : base(TimeSpan.FromSeconds(0))
            {
                double time = caster.Skills[SkillName.Arcanismo].Value * 1.2;
                if (time > 144)
                    time = 144;
                this.Delay = TimeSpan.FromSeconds(time);
                this.Priority = TimerPriority.OneSecond;

                this.m_Owner = target;
            }

            protected override void OnTick()
            {
                SomDoFocoSpell.RemoveEntry(this.m_Owner);
            }
        }

        private class InternalTarget : Target
        {
            private readonly SomDoFocoSpell m_Owner;
            public InternalTarget(SomDoFocoSpell owner)
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
