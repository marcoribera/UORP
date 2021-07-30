    using Server.Engines.Craft;
    using System;

namespace Server.Items
{
    public class CarneSaborosa : Item, ICommodity, ICraftable
    {
        [Constructable]
        public CarneSaborosa()
            : this(1)
        {
        }

        [Constructable]
        public CarneSaborosa(int amount)
            : base(0x09F2)
        {
            this.Weight = 1.0;
            Name = "Carne Saborosa";
            Stackable = true;
            Hue = 237;
        }

        public CarneSaborosa(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description
        {
            get
            {
                return 1063500;
            }
        }
        bool ICommodity.IsDeedable
        {
            get
            {
                return false;
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1072351); // Quest Item
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            Amount = 1;
            return 1;
        }
    }
}
