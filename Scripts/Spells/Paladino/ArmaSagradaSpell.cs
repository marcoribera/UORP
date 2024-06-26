using System;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;


namespace Server.Spells.Paladino
{
    public class ArmaSagradaSpell : PaladinoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Arma Sagrada", "Sanct Arm",
            -1,
            9002,
             Reagent.Incenso,
            Reagent.Vela);

        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        private static Dictionary<Mobile, ArmaSagradaContext> m_Table = new Dictionary<Mobile, ArmaSagradaContext>();

        public ArmaSagradaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Fourth;
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

                switch ( weapon.DefSkill )
                {
                    case SkillName.Contusivo:
                    case SkillName.Cortante:
                    case SkillName.Perfurante:
                        itemID = 0xFB4;
                        soundID = 0x232;
                        break;
                    case SkillName.Atirar:
                        itemID = 0x2D2A;
                        soundID = 0x145;
                        break;
                    default:
                        itemID = 0xF5F;
                        soundID = 0x56;
                        break;
                }

                this.Caster.PlaySound(0x20C);
                this.Caster.PlaySound(soundID);
                this.Caster.FixedParticles(0x3779, 1, 30, 9964, SpellEffectHue, 3, EffectLayer.Waist);

                IEntity from = new Entity(Serial.Zero, new Point3D(this.Caster.X, this.Caster.Y, this.Caster.Z), this.Caster.Map);
                IEntity to = new Entity(Serial.Zero, new Point3D(this.Caster.X, this.Caster.Y, this.Caster.Z + 50), this.Caster.Map);
                Effects.SendMovingParticles(from, to, itemID, 1, 0, false, false, 33, 3, 9501, 1, 0, EffectLayer.Head, 0x100);

                double seconds;

                seconds = 5 + (Caster.Skills.PoderMagico.Value / 10);

                // TODO: Should caps be applied?
                /*
                int pkarma = this.Caster.Karma;

                if (pkarma > 5000)
                    seconds = 11.0;
                else if (pkarma >= 4999)
                    seconds = 10.0;
                else if (pkarma >= 3999)
                    seconds = 9.00;
                else if (pkarma >= 2999)
                    seconds = 8.0;
                else if (pkarma >= 1999)
                    seconds = 7.0;
                else if (pkarma >= 999)
                    seconds = 6.0;
                else
                    seconds = 5.0;
                */

                TimeSpan duration = TimeSpan.FromSeconds(seconds);
                ArmaSagradaContext context;

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
                    context = new ArmaSagradaContext(Caster, weapon, new ArmaSagradaSpell(Caster,null));
                }

                weapon.ArmaSagradaContext = context;
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

    public class ArmaSagradaContext
    {
        public Mobile Owner { get; private set; }
        public BaseWeapon Weapon { get; set; }

        public Timer Timer { get; set; }

        public Spell MagiaOrigem { get; set; }

        public int ConsecrateProcChance
        {
            get
            {
                if (!Core.SA || Owner.Skills.Ordem.Value >= 80)
                {
                    return 100;
                }

                return (int)Owner.Skills.Ordem.Value;
            }
        }

        public int ConsecrateDamageBonus
        {
            get
            {
                int valor;
                valor = (int)Math.Truncate(MagiaOrigem.EfeitoValorRelativo(Owner, MagiaOrigem.Circle, 0.0));
                Console.WriteLine("Dano Sagrado da arma: " + valor);
                return valor;
            }
        }

        public ArmaSagradaContext(Mobile owner, BaseWeapon weapon,Spell magia)
        {
            Owner = owner;
            Weapon = weapon;
            MagiaOrigem = magia;
        }

        public void Expire()
        {
            Weapon.ArmaSagradaContext = null;

            Effects.PlaySound(Weapon.GetWorldLocation(), Weapon.Map, 0x1F8);

            if (Timer != null)
            {
                Timer.Stop();
                Timer = null;
            }
        }
    }
}
