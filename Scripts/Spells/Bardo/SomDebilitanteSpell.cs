/*using System;
using System.Collections.Generic;
using System.Linq;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Spells.Fourth;
using Server.Spells.First;*/

using System;
using System.Collections.Generic;
using System.Linq;
using Server.Items;
using Server.Targeting;
using Server.Spells.Fourth;
using Server.Spells.First;


namespace Server.Spells.Bardo
{
    public class SomDebilitanteSpell : BardoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Som Deblitante", "Parem de ser um bando de molengas",
            218,
            9031,
            false,
            Reagent.Garlic,
            Reagent.Nightshade,
            Reagent.MandrakeRoot,
            Reagent.SulfurousAsh); // o que é false?

        private static readonly Dictionary<Mobile, Timer> m_UnderEffect = new Dictionary<Mobile, Timer>();
        public SomDebilitanteSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Seventh;
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
                return 70.0;
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
            else if (SpellHelper.CheckTown(p, this.Caster) && this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, p);
                SpellHelper.GetSurfaceTop(ref p);

                foreach (var m in AcquireIndirectTargets(p, 2).OfType<Mobile>())
                {
                    DoCurse(this.Caster, m, true);
                }
            }

            this.FinishSequence();
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
            if (!WeakenSpell.IsUnderEffects(m))
                m.RemoveStatMod("[Magic] Str Curse");

            if (!ClumsySpell.IsUnderEffects(m))
                m.RemoveStatMod("[Magic] Dex Curse");

            if (!FeeblemindSpell.IsUnderEffects(m))
                m.RemoveStatMod("[Magic] Int Curse");

            BuffInfo.RemoveBuff(m, BuffIcon.Curse);

            if (m_UnderEffect.ContainsKey(m))
            {
                Timer t = m_UnderEffect[m];

                if (t != null)
                    t.Stop();

                m_UnderEffect.Remove(m);
            }

            m.UpdateResistances();
        }

        public static bool UnderEffect(Mobile m)
        {
            return m_UnderEffect.ContainsKey(m);
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

        private class InternalTarget : Target
        {
            private readonly SomDebilitanteSpell m_Owner;
            public InternalTarget(SomDebilitanteSpell owner)
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
