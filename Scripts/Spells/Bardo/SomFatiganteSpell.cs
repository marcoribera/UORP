using System;
using System.Collections.Generic;
using Server.Targeting;
using Server.Spells.First;
using Server.Network;
using Server.Items;

namespace Server.Spells.Bardo
{
    public class SomFatiganteSpell : BardoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Som Fatigante", "Estou revelando a sua fraqueza!",
            227,
            9031);

        private static readonly Dictionary<Mobile, Timer> m_UnderEffect = new Dictionary<Mobile, Timer>();

        public SomFatiganteSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }
        public override int EficienciaMagica(Mobile caster) { return 5; } //Servirá para calcular o modificador na eficiência das magias

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Fifth;
            }
        }

        public override double RequiredSkill
        {
            get
            {
                return 50.0;
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

        public static void AddEffect(Mobile m, TimeSpan duration, int strOffset, int dexOffset, int intOffset)
        {
            if (m == null)
                return;

            if (m_UnderEffect.ContainsKey(m))
            {
                m_UnderEffect[m].Stop();
                m_UnderEffect[m] = null;
            }

            // my spell is stronger, so lets remove the lesser spell
            if (WeakenSpell.IsUnderEffects(m) && SpellHelper.GetCurseOffset(m, StatType.Str) <= strOffset)
            {
                WeakenSpell.RemoveEffects(m, false);
            }

            if (ClumsySpell.IsUnderEffects(m) && SpellHelper.GetCurseOffset(m, StatType.Dex) <= dexOffset)
            {
                ClumsySpell.RemoveEffects(m, false);
            }

            if (FeeblemindSpell.IsUnderEffects(m) && SpellHelper.GetCurseOffset(m, StatType.Int) <= intOffset)
            {
                FeeblemindSpell.RemoveEffects(m, false);
            }

            m_UnderEffect[m] = Timer.DelayCall<Mobile>(duration, RemoveEffect, m); //= new CurseTimer(m, duration, strOffset, dexOffset, intOffset);
            m.UpdateResistances();
        }

        public static void RemoveEffect(Mobile m)
        {
            if(!WeakenSpell.IsUnderEffects(m))
                m.RemoveStatMod("[Magic] Str Curse");

            if(!ClumsySpell.IsUnderEffects(m))
                m.RemoveStatMod("[Magic] Dex Curse");

            if(!FeeblemindSpell.IsUnderEffects(m))
                m.RemoveStatMod("[Magic] Int Curse");

            BuffInfo.RemoveBuff(m, BuffIcon.Curse);

            if(m_UnderEffect.ContainsKey(m))
            {
                Timer t = m_UnderEffect[m];
                
                if(t != null)
                    t.Stop();
                
                m_UnderEffect.Remove(m);
            }
            
            m.UpdateResistances();
        }

        public static bool UnderEffect(Mobile m)
        {
            return m_UnderEffect.ContainsKey(m);
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public virtual bool DoCurse(Mobile caster, Mobile m, bool masscurse)
        {
            if (Mysticism.StoneFormSpell.CheckImmunity(m))
            {
                caster.SendLocalizedMessage(1080192); // Your target resists your ability reduction magic.
                return true;
            }

            int oldStr = SpellHelper.GetCurseOffset(m, StatType.Str);
            int oldDex = SpellHelper.GetCurseOffset(m, StatType.Dex);
            int oldInt = SpellHelper.GetCurseOffset(m, StatType.Int);

            int newStr = SpellHelper.GetOffset(this, caster, m, StatType.Str, true, true);
            int newDex = SpellHelper.GetOffset(this, caster, m, StatType.Dex, true, true);
            int newInt = SpellHelper.GetOffset(this, caster, m, StatType.Int, true, true);

            if ((-newStr > oldStr && -newDex > oldDex && -newInt > oldInt) || 
                (newStr == 0 && newDex == 0 && newInt == 0))
            {
                return false;
            }

            SpellHelper.AddStatCurse(this, caster, m, StatType.Str, false);
            SpellHelper.AddStatCurse(this, caster, m, StatType.Dex, true);
            SpellHelper.AddStatCurse(this, caster, m, StatType.Int, true);

            int percentage = (int)(SpellHelper.GetOffsetScalar(this, caster, m, true) * 100);
            TimeSpan length = SpellHelper.GetDuration(caster, m);
            string args;

            if (masscurse)
            {
                args = String.Format("{0}\t{0}\t{0}", percentage);
                BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.MassCurse, 1075839, length, m, args));
            }
            else
            {
                args = String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", percentage, percentage, percentage, 10, 10, 10, 10);
                BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Curse, 1075835, 1075836, length, m, args.ToString()));
            }

            AddEffect(m, SpellHelper.GetDuration(caster, m), oldStr, oldDex, oldInt);

            if (m.Spell != null)
                m.Spell.OnCasterHurt();

            m.Paralyzed = false;

            m.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);
            m.PlaySound(0x1E1);

            return true;
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

                if (DoCurse(Caster, m, false))
                {
                    HarmfulSpell(m);
                }
                else
                {
                    DoHurtFizzle();
                }
			}

			FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly SomFatiganteSpell m_Owner;
            public InternalTarget(SomFatiganteSpell owner)
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
