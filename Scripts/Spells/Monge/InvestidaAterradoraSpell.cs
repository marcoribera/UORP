using System;
using System.Collections;
using Server.Items;
using Server.Spells.Bushido;
using Server.Spells.Ninjitsu;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Spells.Paladino;
using Server.Spells.Chivalry;
using Server.Spells.SkillMasteries;

namespace Server.Spells.Monge
{
    //public class InvestidaAterradoraSpell : NinjaMove observar se os requisitos dos moves precisam ser adicionados em mongespell.cs
    public class InvestidaAterradoraSpell : MongeSpell
    {

        private static readonly SpellInfo m_Info = new SpellInfo(
           "Investida Aterradora", "Terribili Impetu",
           212,
           9061
        );

        
        private static readonly Hashtable m_Table = new Hashtable();
        public InvestidaAterradoraSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override int EficienciaMagica(Mobile caster) { return 4; } //Servirá para calcular o modificador na eficiência das magias

        public override double RequiredSkill
        {
            get
            {
                return 80.0;
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Ninth;
            }
        }
        public override TextDefinition AbilityMessage
        {
            get
            {
                return new TextDefinition(1063099);
            }
        }// Your Ki Attack must be complete within 2 seconds for the damage bonus!

        public override void OnCast()
        {
            Mobile m = this.Caster;

        }


        public static double GetBonus(Mobile from)
        {
            KiAttackInfo info = m_Table[from] as KiAttackInfo;

            if (info == null)
                return 0.0;

            int xDelta = info.m_Location.X - from.X;
            int yDelta = info.m_Location.Y - from.Y;

            double bonus = Math.Sqrt((xDelta * xDelta) + (yDelta * yDelta));

            if (bonus > 20.0)
                bonus = 20.0;

            return bonus;
        }

        public override void OnUse(Mobile from)
        {
            if (!this.Validate(from))
                return;

            KiAttackInfo info = new KiAttackInfo(from);
            info.m_Timer = Timer.DelayCall(TimeSpan.FromSeconds(2.0), new TimerStateCallback(EndKiAttack), info);

            m_Table[from] = info;
        }

        public override bool Validate(Mobile from)
        {
            if (from.Hidden && from.AllowedStealthSteps > 0)
            {
                from.SendLocalizedMessage(1063127); // You cannot use this ability while in stealth mode.
                return false;
            }

            if (Core.ML)
            {
                BaseRanged ranged = from.Weapon as BaseRanged;

                if (ranged != null)
                {
                    from.SendLocalizedMessage(1075858); // You can only use this with melee attacks.
                    return false;
                }
            }

            return base.Validate(from);
        }

        public override double GetDamageScalar(Mobile attacker, Mobile defender)
        {
            if (attacker.Hidden)
                return 1.0;

            return 1.0 + GetBonus(attacker) / 10;
        }

        public override void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            if (!this.Validate(attacker) || !this.CheckMana(attacker, true))
                return;

            if (GetBonus(attacker) == 0.0)
            {
                attacker.SendLocalizedMessage(1063101); // You were too close to your target to cause any additional damage.
            }
            else
            {
                attacker.FixedParticles(0x37BE, 1, 5, 0x26BD, 0x0, 0x1, EffectLayer.Waist);
                attacker.PlaySound(0x510);

                attacker.SendLocalizedMessage(1063100); // Your quick flight to your target causes extra damage as you strike!
                defender.FixedParticles(0x37BE, 1, 5, 0x26BD, 0, 0x1, EffectLayer.Waist);

                this.CheckGain(attacker);
            }

            ClearCurrentMove(attacker);
        }

        public override void OnClearMove(Mobile from)
        {
            KiAttackInfo info = m_Table[from] as KiAttackInfo;

            if (info != null)
            {
                if (info.m_Timer != null)
                    info.m_Timer.Stop();

                m_Table.Remove(info.m_Mobile);
            }
        }

        private static void EndKiAttack(object state)
        {
            KiAttackInfo info = (KiAttackInfo)state;

            if (info.m_Timer != null)
                info.m_Timer.Stop();

            ClearCurrentMove(info.m_Mobile);
            info.m_Mobile.SendLocalizedMessage(1063102); // You failed to complete your Ki Attack in time.

            m_Table.Remove(info.m_Mobile);
        }

        private class KiAttackInfo
        {
            public readonly Mobile m_Mobile;
            public readonly Point3D m_Location;
            public Timer m_Timer;
            public KiAttackInfo(Mobile m)
            {
                this.m_Mobile = m;
                this.m_Location = m.Location;
            }
        }

       /* public class InternalTarget : Target
        {
            private readonly InvestidaAterradoraSpell m_Owner;
            public InternalTarget(InvestidaAterradoraSpell owner)
                : base(11, true, TargetFlags.None)
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
        }*/
    }
}
