using System;
using Server.Targeting;
using System.Collections.Generic;

namespace Server.Spells.ClerigoDosMortos
{
    public class DrenarAgilidadeSpell : ClerigoDosMortosSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Drenar Agilidade", "Exhaurire Agilitas",
            212,
            9031,
            Reagent.Bloodmoss,
            Reagent.Nightshade);
        public DrenarAgilidadeSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public static Dictionary<Mobile, Timer> m_Table = new Dictionary<Mobile, Timer>();

        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public static bool IsUnderEffects(Mobile m)
        {
            return m_Table.ContainsKey(m);
        }

        public override double RequiredSkill
        {
            get
            {
                return 20.0;
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

                BuffInfo.RemoveBuff(m, BuffIcon.Clumsy);

                if (removeMod)
                    m.RemoveStatMod("[Magic] Dex Curse");

                m_Table.Remove(m);
            }
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
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
            else if (this.CheckHSequence(m))
            {
                SpellHelper.Turn(this.Caster, m);
                SpellHelper.CheckReflect((int)this.Circle, this.Caster, ref m);

                if (Mysticism.StoneFormSpell.CheckImmunity(m))
                {
                    Caster.SendLocalizedMessage(1080192); // Your target resists your ability reduction magic.
                    return;
                }

                int oldOffset = SpellHelper.GetCurseOffset(m, StatType.Dex);
                int newOffset = SpellHelper.GetOffset(this,Caster, m, StatType.Dex, true, false);

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
                        SpellHelper.AddStatCurse(this, this.Caster, m, StatType.Dex, false, newOffset);

                        int percentage = (int)(SpellHelper.GetOffsetScalar(this,this.Caster, m, true) * 100);
                        TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                        BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Clumsy, 1075831, length, m, percentage.ToString()));

                        if (m_Table.ContainsKey(m))
                            m_Table[m].Stop();

                        m_Table[m] = Timer.DelayCall(length, () =>
                            {
                                RemoveEffects(m);
                            });
                    }
                }
            }

            this.FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly DrenarAgilidadeSpell m_Owner;
            public InternalTarget(DrenarAgilidadeSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    this.m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}
