using System;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Monge
{
    public class AtaqueElementalSpell : MongeSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Ataque Elemental", "Elementa Impetus",
            -1,
            9002);


        public override int EficienciaMagica(Mobile caster) { return 4; } //Servirá para calcular o modificador na eficiência das magias

        private static Dictionary<Mobile, AtaqueElementalContext> m_Table = new Dictionary<Mobile, AtaqueElementalContext>();

        public AtaqueElementalSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(0.5);
            }
        }
        public override double RequiredSkill
        {
            get
            {
                return 60.0;
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Ninth;
            }
        }
       
        public override bool BlocksMovement
        {
            get
            {
                return false;
            }
        }
        public override void OnCast()
        {
            BaseWeapon weapon = this.Caster.Weapon as BaseWeapon;

            if (Caster.Player && (weapon == null || weapon is Fists))
            {
                this.Caster.SendLocalizedMessage(501078); // You must be holding a weapon.
            }
            else if (this.CheckSequence())
            {
                /* Temporarily enchants the weapon the caster is currently wielding.
                * The type of damage the weapon inflicts when hitting a target will
                * be converted to the target's worst Resistance type.
                * Duration of the effect is affected by the caster's Karma and lasts for 3 to 11 seconds.
                */
                int itemID, soundID;

                switch ( weapon.Skill )
                {
                    case SkillName.Contusivo:
                        itemID = 0xFB4;
                        soundID = 0x232;
                        break;
                    case SkillName.Atirar:
                        itemID = 0x13B1;
                        soundID = 0x145;
                        break;
                    default:
                        itemID = 0xF5F;
                        soundID = 0x56;
                        break;
                }

                this.Caster.PlaySound(0x20C);
                this.Caster.PlaySound(soundID);
                this.Caster.FixedParticles(0x3779, 1, 30, 9964, 3, 3, EffectLayer.Waist);

                IEntity from = new Entity(Serial.Zero, new Point3D(this.Caster.X, this.Caster.Y, this.Caster.Z), this.Caster.Map);
                IEntity to = new Entity(Serial.Zero, new Point3D(this.Caster.X, this.Caster.Y, this.Caster.Z + 50), this.Caster.Map);
                Effects.SendMovingParticles(from, to, itemID, 1, 0, false, false, 33, 3, 9501, 1, 0, EffectLayer.Head, 0x100);

                double seconds = 5.0;

                // TODO: Should caps be applied?

               

                TimeSpan duration = TimeSpan.FromSeconds(seconds);
                AtaqueElementalContext context;

                if (IsUnderEffects(Caster))
                {
                    context = m_Table[Caster];

                    if (context.Timer != null)
                    {
                        context.Timer.Stop();
                        context.Timer = null;
                    }

                    context.Weapon = weapon;
                }
                else
                {
                    context = new AtaqueElementalContext(Caster, weapon);
                }

                weapon.AtaqueElementalContext = context;
                context.Timer = Timer.DelayCall<Mobile>(duration, RemoveEffects, Caster);

                m_Table[Caster] = context;

                BuffInfo.AddBuff(Caster, new BuffInfo(BuffIcon.ConsecrateWeapon, 1151385, 1151386, duration, Caster, String.Format("{0}\t{1}", context.ConsecrateProcChance, context.ConsecrateDamageBonus)));
            }

            this.FinishSequence();
        }

        public static bool IsUnderEffects(Mobile m)
        {
            return m_Table.ContainsKey(m);
        }

        public static void RemoveEffects(Mobile m)
        {
            if (m_Table.ContainsKey(m))
            {
                var context = m_Table[m];

                context.Expire();

                m_Table.Remove(m);
            }
        }
    }

    public class AtaqueElementalContext
    {
        public Mobile Owner { get; private set; }
        public BaseWeapon Weapon { get; set; }

        public Timer Timer { get; set; }

        public int ConsecrateProcChance
        {
            get
            {
                if (!Core.SA || Owner.Skills.Misticismo.Value >= 80)
                {
                    return 100;
                }

                return (int)Owner.Skills.Misticismo.Value;
            }
        }

        public int ConsecrateDamageBonus
        {
            get
            {
                if (Core.SA)
                {
                    double value = Owner.Skills.Misticismo.Value;

                    if (value >= 90)
                    {
                        return (int)Math.Truncate((value - 90) / 2);
                    }
                }

                return 0;
            }
        }

        public AtaqueElementalContext(Mobile owner, BaseWeapon weapon)
        {
            Owner = owner;
            Weapon = weapon;
        }

        public void Expire()
        {
            Weapon.ConsecratedContext = null;

            Effects.PlaySound(Weapon.GetWorldLocation(), Weapon.Map, 0x1F8);

            if (Timer != null)
            {
                Timer.Stop();
                Timer = null;
            }
        }
    }
}


/*CONSECRATED WEAPON PARA MONGE
using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Spells.Mysticism;

namespace Server.Spells.Monge
{
    public class SocoTectonicoSpell : MongeSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Quivering Palm", "Summ Cah Beh Ra",
                269,
                0
            );

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.5); } }

        
       
        public override bool BlocksMovement { get { return false; } }
        public override double RequiredSkill
        {
            get
            {
                return 50.0;
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Eleventh;
            }
        }

        public override int EficienciaMagica(Mobile caster) { return 4; } //Servirá para calcular o modificador na eficiência das magias


        public SocoTectonicoSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            BaseWeapon weapon = Caster.Weapon as BaseWeapon;

            if (CheckSequence())
            {
                IEntity from = new Entity(Serial.Zero, new Point3D(Caster.X, Caster.Y, Caster.Z), Caster.Map);
                IEntity to = new Entity(Serial.Zero, new Point3D(Caster.X, Caster.Y, Caster.Z + 50), Caster.Map);

                Caster.PlaySound(0x212);
                Effects.SendLocationParticles(EffectItem.Create(Caster.Location, Caster.Map, EffectItem.DefaultDuration), 0x376A, 1, 29, 0x47D, 2, 9962, 0);

                double seconds = Caster.Skills[SkillName.Briga].Value;

                TimeSpan duration = TimeSpan.FromSeconds(seconds);

                Timer t = (Timer)m_Table[weapon];

                if (t != null)
                    t.Stop();

                weapon.Consecrated = true;

                m_Table[weapon] = t = new ExpireTimer(weapon, duration);

                t.Start();
            }

            FinishSequence();
        }

        private static Hashtable m_Table = new Hashtable();

        private class ExpireTimer : Timer
        {
            private BaseWeapon m_Weapon;

            public ExpireTimer(BaseWeapon weapon, TimeSpan delay) : base(delay)
            {
                m_Weapon = weapon;
                Priority = TimerPriority.FiftyMS;
            }

            protected override void OnTick()
            {
                m_Weapon.Consecrated = false;
                Effects.PlaySound(m_Weapon.GetWorldLocation(), m_Weapon.Map, 0x1F8);
                m_Table.Remove(this);
            }
        }
    }
}
*/
