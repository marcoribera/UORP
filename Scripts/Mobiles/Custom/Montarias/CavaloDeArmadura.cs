using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("corpo de Cavaldo de armadura")]
    public class CavaloDeArmadura : BaseMount
    {
        [Constructable]
        public CavaloDeArmadura()
            : this("Cavaldo de armadura")
        {
        }

        [Constructable]
        public CavaloDeArmadura(string name)
            : base(name, 1766, 1766, AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0xA8;
            BodyValue = 1766;
            //Hue = 1175;

            SetStr(500, 555);
            SetDex(89, 125);
            SetInt(100, 160);

            SetHits(555, 650);

            SetDamage(20, 26);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Poison, 20);
            SetDamageType(ResistanceType.Energy, 40);

            SetResistance(ResistanceType.Physical, 65, 75);
            SetResistance(ResistanceType.Fire, 20, 40);
            SetResistance(ResistanceType.Cold, 20, 40);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.PoderMagico, 15.2, 19.3);
            SetSkill(SkillName.Arcanismo, 39.5, 49.5);
            SetSkill(SkillName.ResistenciaMagica, 91.4, 93.4);
            SetSkill(SkillName.Anatomia, 108.1, 110.0);
            SetSkill(SkillName.Briga, 97.3, 98.2);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 60;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 108.0;
        }

        public CavaloDeArmadura(Serial serial)
            : base(serial)
        {
        }

        public override int Meat
        {
            get
            {
                return 5;
            }
        }
        public override int Hides
        {
            get
            {
                return 10;
            }
        }
        public override HideType HideType
        {
            get
            {
                return HideType.Barbed;
            }
        }
        public override FoodType FavoriteFood
        {
            get
            {
                return FoodType.Meat;
            }
        }
        public override bool CanAngerOnTame
        {
            get
            {
                return true;
            }
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.Potions);
        }

        public override int GetAngerSound()
        {
            if (!Controlled)
                return 0x16A;

            return base.GetAngerSound();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0)
            {
                SetDamageType(ResistanceType.Physical, 40);
            }
        }
    }
}
