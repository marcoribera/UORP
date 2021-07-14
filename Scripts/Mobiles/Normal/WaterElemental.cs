using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a water elemental corpse")]
    public class WaterElemental : BaseCreature
    {
        private Boolean m_HasDecanter = true;

        [CommandProperty(AccessLevel.GameMaster)]
        public Boolean HasDecanter { get { return m_HasDecanter; } set { m_HasDecanter = value; } }

        [Constructable]
        public WaterElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "elemental da agua";
            this.Body = 16;
            this.BaseSoundID = 278;

            this.SetStr(126, 155);
            this.SetDex(66, 85);
            this.SetInt(101, 125);

            this.SetHits(76, 93);

            this.SetDamage(7, 9);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 35, 45);
            this.SetResistance(ResistanceType.Fire, 10, 25);
            this.SetResistance(ResistanceType.Cold, 10, 25);
            this.SetResistance(ResistanceType.Poison, 60, 70);
            this.SetResistance(ResistanceType.Energy, 5, 10);

            this.SetSkill(SkillName.PoderMagico, 60.1, 75.0);
            this.SetSkill(SkillName.Arcanismo, 70, 80);
            this.SetSkill(SkillName.ResistenciaMagica, 100.1, 115.0);
            this.SetSkill(SkillName.Anatomia, 50.1, 70.0);
            this.SetSkill(SkillName.Briga, 50.1, 70.0);

            this.Fame = 4500;
            this.Karma = -4500;

            this.VirtualArmor = 0;
            this.ControlSlots = 3;
            this.CanSwim = true;

            this.PackItem(new BlackPearl(3));
        }

        public WaterElemental(Serial serial)
            : base(serial)
        {
        }

        public override void OnBeforeDamage(Mobile from, ref int totalDamage, DamageType type)
        {

            if (from == null)
                return;

            if (!this.Alive)
                return;

            if (type != DamageType.Melee)
                return;

            bool bonus = false;
            base.OnBeforeDamage(from, ref totalDamage, type);

            if (!from.Player)
                return;

            var arma = from.FindItemOnLayer(Layer.OneHanded);
            if (arma != null && arma is BaseSword)
            {
                totalDamage += 10;
                bonus = true;
            }

            arma = from.FindItemOnLayer(Layer.TwoHanded);
            if (arma != null && arma is BaseSword)
            {
                bonus = true;
                totalDamage += 10;
            }

            if (!bonus)
            {
                totalDamage /= 3;
                if (!from.IsCooldown("dicabas2"))
                {
                    from.SetCooldown("dicabas2", TimeSpan.FromMinutes(10));
                    from.SendMessage("Seu ataque nao foi muito efetivo contra este ser aquoso por conta do tipo de sua arma");
                }
            }
            else if (bonus)
            {
                if (!from.IsCooldown("dicabas"))
                {
                    from.SetCooldown("dicabas", TimeSpan.FromMinutes(10));
                    from.SendMessage("Sua arma foi muito efetiva contra " + this.Name);
                }
            }
        }

        public override double DispelDifficulty
        {
            get
            {
                return 117.5;
            }
        }
        public override double DispelFocus
        {
            get
            {
                return 45.0;
            }
        }
        public override bool BleedImmune
        {
            get
            {
                return true;
            }
        }
        public override int TreasureMapLevel
        {
            get
            {
                return 2;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Average);
            this.AddLoot(LootPack.Meager);
            this.AddLoot(LootPack.Potions);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);

            writer.Write(m_HasDecanter);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    break;
                case 1:
                    m_HasDecanter = reader.ReadBool();
                    break;
            }
        }
    }
}
