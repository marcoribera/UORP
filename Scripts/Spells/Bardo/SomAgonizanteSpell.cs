using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server;
using Server.Targeting;
using Server.Spells.SkillMasteries;

namespace Server.Spells.Bardo
{
    public class SomAgonizanteSpell : BardoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Som Agonizante", "O que?! Meu som lhe parece ruim?",
            203,
            9031);

        private static readonly Dictionary<Mobile, InternalTimer> m_Table = new Dictionary<Mobile, InternalTimer>();

        public SomAgonizanteSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }
        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(1.25);
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Ninth;
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
                return 80.0;
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

        public void Target(Mobile m)
        {
            if (CheckHSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                ApplyEffects(m);
                ConduitSpell.CheckAffected(Caster, m, ApplyEffects);
            }

            FinishSequence();
        }

        public void ApplyEffects(Mobile m, double strength = 1.0)
        {
            //SpellHelper.CheckReflect( (int)Circle, Caster, ref m ); //Irrelevent asfter AoS

            /* Temporarily causes intense physical pain to the target, dealing direct damage.
             * After 10 seconds the spell wears off, and if the target is still alive, 
             * some of the Hit Points lost through Pain Spike are restored.
             */

            m.FixedParticles(0x37C4, 1, 8, 9916, 39, 3, EffectLayer.Head);
            m.FixedParticles(0x37C4, 1, 8, 9502, 39, 4, EffectLayer.Head);
            m.PlaySound(0x210);

            double damage = (((GetDamageSkill(Caster) - GetResistSkill(m)) / 10) + (m.Player ? 18 : 30)) * strength;
            m.CheckSkill(SkillName.ResistenciaMagica, 0.0, 120.0);	//Skill check for gain

            if (damage < 1)
                damage = 1;

            TimeSpan buffTime = TimeSpan.FromSeconds(10.0 * strength);
            InternalTimer t;

            if (m_Table.ContainsKey(m))
            {
                damage = Utility.RandomMinMax(3, 7);
                t = m_Table[m];

                if (t != null)
                {
                    t.Expires += TimeSpan.FromSeconds(2);
                }
            }
            else
            {
                t = new InternalTimer(m, damage);
                t.Start();
            }

            BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.PainSpike, 1075667, t.Expires - DateTime.UtcNow, m, Convert.ToString((int)damage)));

            m.DFA = DFAlgorithm.PainSpike;
            AOS.Damage(m, Caster, (int)damage, 0, 0, 0, 0, 0, 0, 100);
            AOS.DoLeech((int)damage, Caster, m);
            m.DFA = DFAlgorithm.Standard;

            HarmfulSpell(m);
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_Mobile;
            private readonly int m_ToRestore;

            public DateTime Expires { get; set; }

            public InternalTimer(Mobile m, double toRestore)
                : base(TimeSpan.FromMilliseconds(250), TimeSpan.FromMilliseconds(250))
            {
                Priority = TimerPriority.FiftyMS;

                m_Mobile = m;
                m_ToRestore = (int)toRestore;

                Expires = DateTime.UtcNow + TimeSpan.FromSeconds(10);

                m_Table[m] = this;
            }

            protected override void OnTick()
            {
                if (DateTime.UtcNow >= Expires)
                {
                    if (m_Table.ContainsKey(m_Mobile))
                        m_Table.Remove(m_Mobile);

                    if (m_Mobile.Alive && !m_Mobile.IsDeadBondedPet)
                        m_Mobile.Hits += m_ToRestore;

                    BuffInfo.RemoveBuff(m_Mobile, BuffIcon.PainSpike);
                    
                    Stop();
                }
            }
        }

        private class InternalTarget : Target
        {
            private readonly SomAgonizanteSpell m_Owner;
            public InternalTarget(SomAgonizanteSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    m_Owner.Target((Mobile)o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}
