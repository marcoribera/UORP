using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("corpo de Cavaldo de armadura")]
    public class TLSCavaloDeArmadura : BaseMount
    {
        [Constructable]
        public TLSCavaloDeArmadura()
            : this("Cavaldo de armadura")
        {
        }

        [Constructable]
        public TLSCavaloDeArmadura(string name)
            : base(name, 1766, 1766, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0xA8;
            BodyValue = 1766;
            //Hue = 1175;

            SetStr(100, 150);
            SetDex(50, 100);
            SetInt(6, 10);

            //SetHits(555, 650);

            SetDamage(5, 11);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 40, 50);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.ResistenciaMagica, 25.1, 30.0);
            SetSkill(SkillName.Anatomia, 29.3, 44.0);
            SetSkill(SkillName.Briga, 29.3, 44.0);

            Fame = 0;
            Karma = 200;

            VirtualArmor = 50;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 29.1;
        }

        public TLSCavaloDeArmadura(Serial serial)
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
        public override FoodType FavoriteFood
        {
            get
            {
                return FoodType.FruitsAndVegies | FoodType.GrainsAndHay;
            }
        }
        public override bool CanAngerOnTame
        {
            get
            {
                return true;
            }
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
