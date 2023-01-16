// coded by ßåяя¥ 2.0
using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Monge
{
    public class SocoDoKiSpell : MongeSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Soco de Ki", "Energo Impetus",
            212,
            9041
            );

        public override int EficienciaMagica(Mobile caster) { return 4; } //Servirá para calcular o modificador na eficiência das magias


        public SocoDoKiSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }


        public override double RequiredSkill
        {
            get
            {
                return 10.0;
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Sixth;
            }
        }


        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.5); } }

        public override bool BlocksMovement { get { return false; } }


       

        public override void OnCast()
        {
            BaseWeapon weapon = Caster.Weapon as BaseWeapon; // precisa fazer nao usar arma

            if (weapon == null || weapon is Fists)
            {
                Caster.SendLocalizedMessage(501078); // You must be holding a weapon.
            }
            else if (CheckSequence())
            {
                /* Temporarily enchants the weapon the caster is currently wielding.
				 * The type of damage the weapon inflicts when hitting a target will
				 * be converted to the target's worst Resistance type.
				 * Duration of the effect is affected by the caster's Karma and lasts for 3 to 11 seconds.
				 */

                int itemID, soundID;

                switch (weapon.Skill)
                {
                    case SkillName.Briga: itemID = 0xFB4; soundID = 0x232; break;
                    default: itemID = 0xF5F; soundID = 0x56; break;
                }

                Caster.PlaySound(0x20C);
                Caster.PlaySound(soundID);
                Caster.FixedParticles(0x3779, 1, 30, 9964, 3, 3, EffectLayer.Waist);
                IEntity from = new Entity(Serial.Zero, new Point3D(Caster.X, Caster.Y, Caster.Z), Caster.Map);

                IEntity to = new Entity(Serial.Zero, new Point3D(Caster.X, Caster.Y, Caster.Z + 50), Caster.Map);
                Effects.SendMovingParticles(from, to, itemID, 1, 0, false, false, 33, 3, 9501, 1, 0, EffectLayer.Head, 0x100);

                double seconds = Caster.Skills[SkillName.Misticismo].Value;

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
