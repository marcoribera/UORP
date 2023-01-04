using System;
using Server.Items;

using System.Collections.Generic;


using Server.Network;
using Server.Spells.Bushido;
using Server.Spells.Ninjitsu;
using Server.Spells.SkillMasteries;

namespace Server.Spells.Monge
{
    public abstract class MongeSpell : Spell
    {
        //                                            Circulo:  1  2  3   4   5   6   7   8   9   10   11
        private static readonly int[] m_ManaTable = new int[] { 4, 6, 9, 13, 19, 28, 42, 63, 94, 141, 211 };
        private const double ChanceOffset = 20.0, ChanceLength = 120.0 / 10.0; //originalmente era: ChanceOffset = 20.0, ChanceLength = 100.0 /7.0
        public MongeSpell(Mobile caster, Item scroll, SpellInfo info)
            : base(caster, scroll, info)
        {
        }
        private static readonly Dictionary<Mobile, SpecialMoveContext> m_PlayersTable = new Dictionary<Mobile, SpecialMoveContext>();
        public override SkillName CastSkill
        {
            get
            {
                return SkillName.Misticismo;
            }
        }
        public virtual int BaseMana
        {
            get
            {
                return 0;
            }
        }

        private static void AddContext(Mobile m, SpecialMoveContext context)
        {
            m_PlayersTable[m] = context;
        }

        private static void RemoveContext(Mobile m)
        {
            SpecialMoveContext context = GetContext(m);

            if (context != null)
            {
                m_PlayersTable.Remove(m);

                context.Timer.Stop();
            }
        }
        private class SpecialMoveTimer : Timer
        {
            private readonly Mobile m_Mobile;

            public SpecialMoveTimer(Mobile from)
                : base(TimeSpan.FromSeconds(3.0))
            {
                this.m_Mobile = from;

                this.Priority = TimerPriority.TwentyFiveMS;
            }

            protected override void OnTick()
            {
                RemoveContext(this.m_Mobile);
            }
        }
        private static SpecialMoveContext GetContext(Mobile m)
        {
            return (m_PlayersTable.ContainsKey(m) ? m_PlayersTable[m] : null);
        }

        public static bool GetContext(Mobile m, Type type)
        {
            SpecialMoveContext context = null;
            m_PlayersTable.TryGetValue(m, out context);

            if (context == null)
                return false;

            return (context.Type == type);
        }
        public virtual void SetContext(Mobile from)
        {
            if (GetContext(from) == null)
            {
                if (this.DelayedContext || from.Skills[this.MoveSkill].Value < 50.0)
                {
                    Timer timer = new SpecialMoveTimer(from);
                    timer.Start();

                    AddContext(from, new SpecialMoveContext(timer, this.GetType()));
                }
            }
        }
        public abstract double RequiredSkill { get; }
        public override SkillName DamageSkill
        {
            get
            {
                return SkillName.PoderMagico;
            }
        }

        public virtual SkillName MoveSkill
        {
            get
            {
                return SkillName.Misticismo;
            }
        }

        //public override int CastDelayBase{ get{ return base.CastDelayBase; } } // Reference, 3
        public override bool ClearHandsOnCast
        {
            get
            {
                return false;
            }
        }
        public virtual TextDefinition AbilityMessage
        {
            get
            {
                return 0;
            }
        }

        public virtual bool BlockedByAnimalForm
        {
            get
            {
                return true;
            }
        }
        public virtual bool DelayedContext
        {
            get
            {
                return false;
            }
        }

        public virtual int GetAccuracyBonus(Mobile attacker)
        {
            return 0;
        }

        public virtual double GetDamageScalar(Mobile attacker, Mobile defender)
        {
            return 1.0;
        }

        public virtual bool OnBeforeSwing(Mobile attacker, Mobile defender)
        {
            return true;
        }

        // Called when a hit connects, but before damage is calculated.
        public virtual bool OnBeforeDamage(Mobile attacker, Mobile defender)
        {
            return true;
        }

        // Called as soon as the ability is used.
        public virtual void OnUse(Mobile from)
        {
        }

        // Called when a hit connects, at the end of the weapon.OnHit() method.
        public virtual void OnHit(Mobile attacker, Mobile defender, int damage)
        {
        }

        // Called when a hit misses.
        public virtual void OnMiss(Mobile attacker, Mobile defender)
        {
        }

        // Called when the move is cleared.
        public virtual void OnClearMove(Mobile from)
        {
        }
        public virtual bool Validate(Mobile from)
        {
            if (!from.Player)
                return true;

            if (Bushido.HonorableExecution.IsUnderPenalty(from))
            {
                from.SendLocalizedMessage(1063024); // You cannot perform this special move right now.
                return false;
            }

            if (Ninjitsu.AnimalForm.UnderTransformation(from))
            {
                from.SendLocalizedMessage(1063024); // You cannot perform this special move right now.
                return false;
            }

            return this.CheckSkills(from) && this.CheckMana(from, false);
        }
        public virtual void CheckGain(Mobile m)
        {
            m.CheckSkill(this.MoveSkill, this.RequiredSkill, this.RequiredSkill + 37.5);
        }

        private static readonly Dictionary<Mobile, SpecialMove> m_Table = new Dictionary<Mobile, SpecialMove>();


        public virtual void SendAbilityMessage(Mobile m)
        {
            TextDefinition.SendMessageTo(m, AbilityMessage);
        }

        public virtual bool IgnoreArmor(Mobile attacker)
        {
            return false;
        }

        public virtual double GetPropertyBonus(Mobile attacker)
        {
            return 1.0;
        }
        public abstract SpellCircle Circle { get; }
        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromMilliseconds(((4 + (int)Circle) * CastDelaySecondsPerTick) * 1000);
            }
        }
        public override bool ConsumeReagents()
        {
            if (base.ConsumeReagents())
                return true;
            else
                return false;
        }

        public override void GetCastSkills(out double min, out double max)
        {
            int circle = (int)Circle;

            if (Scroll != null)
                circle -= 2;

            double avg = ChanceLength * circle;

            min = avg - ChanceOffset;
            max = avg + ChanceOffset;
        }

        public override int GetMana()
        {
            if (Scroll is BaseWand)
                return 0;

            return m_ManaTable[(int)Circle];
        }

        public virtual bool CheckSkills(Mobile m)
        {
            if (m.Skills[this.MoveSkill].Value < this.RequiredSkill)
            {
                string args = String.Format("{0}\t{1}\t ", this.RequiredSkill.ToString("F1"), this.MoveSkill.ToString());
                m.SendLocalizedMessage(1063013, args); // You need at least ~1_SKILL_REQUIREMENT~ ~2_SKILL_NAME~ skill to use that ability.
                return false;
            }

            return true;
        }

        public static SpecialMove GetCurrentMove(Mobile m)
        {
            if (m == null)
                return null;

            if (!Core.SE)
            {
                ClearCurrentMove(m);
                return null;
            }

            SpecialMove move = null;
            m_Table.TryGetValue(m, out move);

            if (move != null && move.ValidatesDuringHit && !move.Validate(m))
            {
                ClearCurrentMove(m);
                return null;
            }

            return move;
        }

        public static bool SetCurrentMove(Mobile m, SpecialMove move)
        {
            if (!Core.SE)
            {
                ClearCurrentMove(m);
                return false;
            }

            if (move != null && !move.Validate(m))
            {
                ClearCurrentMove(m);
                return false;
            }

            bool sameMove = (move == GetCurrentMove(m));

            ClearCurrentMove(m);

            if (sameMove)
                return true;

            if (move != null)
            {
                WeaponAbility.ClearCurrentAbility(m);

                m_Table[m] = move;

                move.OnUse(m);

                int moveID = SpellRegistry.GetRegistryNumber(move);

                if (moveID > 0)
                    m.Send(new ToggleSpecialAbility(moveID + 1, true));

                move.SendAbilityMessage(m);

                SkillMasterySpell.CancelSpecialMove(m);
            }

            return true;
        }
        public virtual int ScaleMana(Mobile m, int mana)
        {
            double scalar = 1.0;

            if (ManaPhasingOrb.IsInManaPhase(m))
            {
                ManaPhasingOrb.RemoveFromTable(m);
                return 0;
            }

            if (!Server.Spells.Necromancy.MindRotSpell.GetMindRotScalar(m, ref scalar))
            {
                scalar = 1.0;
            }

            if (Server.Spells.Mysticism.PurgeMagicSpell.IsUnderCurseEffects(m))
            {
                scalar += .5;
            }

            // Lower Mana Cost = 40%
            int lmc = Math.Min(AosAttributes.GetValue(m, AosAttribute.LowerManaCost), 40);

            lmc += BaseArmor.GetInherentLowerManaCost(m);

            scalar -= (double)lmc / 100;

            int total = (int)(mana * scalar);

            if (m.Skills[this.MoveSkill].Value < 50.0 && GetContext(m) != null)
                total *= 2;

            return total;
        }

       

        public static void ClearCurrentMove(Mobile m)
        {
            SpecialMove move = null;
            m_Table.TryGetValue(m, out move);

            if (move != null)
            {
                move.OnClearMove(m);

                int moveID = SpellRegistry.GetRegistryNumber(move);

                if (moveID > 0)
                    m.Send(new ToggleSpecialAbility(moveID + 1, false));
            }

            m_Table.Remove(m);
        }


        public virtual bool CheckMana(Mobile from, bool consume)
        {
            int mana = this.ScaleMana(from, this.BaseMana);

            if (from.Mana < mana)
            {
                from.SendLocalizedMessage(1060181, mana.ToString()); // You need ~1_MANA_REQUIREMENT~ mana to perform that attack
                return false;
            }

            if (consume)
            {
                if (!this.DelayedContext)
                    this.SetContext(from);

                from.Mana -= mana;
            }

            return true;
        }

        public virtual bool CheckResisted(Mobile target)
        {
            double n = GetResistPercent(target);

            n /= 100.0;

            if (n <= 0.0)
                return false;

            if (n >= 1.0)
                return true;

            int maxSkill = (1 + (int)Circle) * 10;
            maxSkill += (1 + ((int)Circle / 6)) * 25;

            if (target.Skills[SkillName.ResistenciaMagica].Value < maxSkill)
                target.CheckSkill(SkillName.ResistenciaMagica, 0.0, target.Skills[SkillName.ResistenciaMagica].Cap);

            return (n >= Utility.RandomDouble());
        }

        public virtual double GetResistPercentForCircle(Mobile target, SpellCircle circle)
        {
            double value = GetResistSkill(target);
            double firstPercent = value / 5.0;
            double secondPercent = value - (((Caster.Skills[CastSkill].Value - 20.0) / 5.0) + (1 + (int)circle) * 5.0);

            return (firstPercent > secondPercent ? firstPercent : secondPercent) / 2.0; // Seems should be about half of what stratics says.
        }

        public virtual double GetResistPercent(Mobile target)
        {
            return GetResistPercentForCircle(target, Circle);
        }
        public class SpecialMoveContext
        {
            private readonly Timer m_Timer;
            private readonly Type m_Type;

            public Timer Timer
            {
                get
                {
                    return this.m_Timer;
                }
            }
            public Type Type
            {
                get
                {
                    return this.m_Type;
                }
            }

            public SpecialMoveContext(Timer timer, Type type)
            {
                this.m_Timer = timer;
                this.m_Type = type;
            }
        }
        public override TimeSpan GetCastDelay()
        {
            if (!Core.ML && Scroll is BaseWand)
                return TimeSpan.Zero;

            if (!Core.AOS)
                return TimeSpan.FromSeconds(0.5 + (0.25 * (int)Circle));

            return base.GetCastDelay();
        }
    }
}
