using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("corpo do Elemental de Agua")]
    public class TLSElementalDeAgua : BaseCreature
    {
        private Boolean m_HasDecanter = true;

        [CommandProperty(AccessLevel.GameMaster)]
        public Boolean HasDecanter { get { return m_HasDecanter; } set { m_HasDecanter = value; } }

        [Constructable]
        public TLSElementalDeAgua()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "Elemental de Agua";
            this.Body = 1788;
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
            this.SetSkill(SkillName.Arcanismo, 60.1, 75.0);
            this.SetSkill(SkillName.ResistenciaMagica, 100.1, 115.0);
            this.SetSkill(SkillName.Anatomia, 50.1, 70.0);
            this.SetSkill(SkillName.Briga, 50.1, 70.0);

            this.Fame = 4500;
            this.Karma = -4500;

            this.VirtualArmor = 40;
            this.ControlSlots = 3;
            this.CanSwim = true;

            this.PackItem(new BlackPearl(3));
        }

        public TLSElementalDeAgua(Serial serial)
            : base(serial)
        {
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