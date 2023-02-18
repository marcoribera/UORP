using System;
using Server.Targeting;
using System.Collections.Generic;
using Server.Network;
using Server.Items;

namespace Server.Spells.Bardo
{
    public class SomDaFraquezaSpell : BardoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Som da Fraqueza", "Sua mochila ficou pesada, meu amigo?",
            212,
            9031);

        public static Dictionary<Mobile, Timer> m_Table = new Dictionary<Mobile, Timer>();

        public static bool IsUnderEffects(Mobile m)
        {
            return m_Table.ContainsKey(m);
        }
        public override int EficienciaMagica(Mobile caster) { return 5; } //Servirá para calcular o modificador na eficiência das magias
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
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
                return 10.0;
            }
        }
        public static void RemoveEffects(Mobile m, bool removeMod = true)
        {
            if (m_Table.ContainsKey(m))
            {
                Timer t = m_Table[m];

                if (t != null && t.Running)
                {
                    t.Stop();
                }

                BuffInfo.RemoveBuff(m, BuffIcon.Weaken);

                if(removeMod)
                    m.RemoveStatMod("[Magic] Str Curse");

                m_Table.Remove(m);
            }
        }

        public SomDaFraquezaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
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

                if (Mysticism.StoneFormSpell.CheckImmunity(m))
                {
                    Caster.SendLocalizedMessage(1080192); // Your target resists your ability reduction magic.
                    return;
                }

                int oldOffset = SpellHelper.GetCurseOffset(m, StatType.Str);
                int newOffset = SpellHelper.GetOffset(this,Caster, m, StatType.Str, true, false);

                if (-newOffset > oldOffset || newOffset == 0)
                {
                    DoHurtFizzle();
                }
                else
                {
                    if (m.Spell != null)
                        m.Spell.OnCasterHurt();

                    m.Paralyzed = false;

                    m.FixedParticles(0x3779, 10, 15, 5002, EffectLayer.Head);
                    m.PlaySound(0x1DF);

                    HarmfulSpell(m);

                    if (-newOffset < oldOffset)
                    {
                        SpellHelper.AddStatCurse(this, this.Caster, m, StatType.Str, false, newOffset);

                        int percentage = (int)(SpellHelper.GetOffsetScalar(this,this.Caster, m, true) * 100);
                        TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                        BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Weaken, 1075837, length, m, percentage.ToString()));

                        if (m_Table.ContainsKey(m))
                            m_Table[m].Stop();

                        m_Table[m] = Timer.DelayCall(length, () =>
                        {
                            RemoveEffects(m);
                        });
                    }
                }
            }

            FinishSequence();
        }

        public class InternalTarget : Target
        {
            private readonly SomDaFraquezaSpell m_Owner;
            public InternalTarget(SomDaFraquezaSpell owner)
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
