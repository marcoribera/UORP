    using Server.Engines.Craft;
    using System;

namespace Server.Items
{
    public class CercasReforcadas : Item, ICommodity, ICraftable
    {
        [Constructable]
        public CercasReforcadas()
            : this(1)
        {
        }

        [Constructable]
        public CercasReforcadas(int amount)
            : base(0x0862)
        {
            this.Weight = 1.0;
            Name = "Cercas Reforçadas";
            Stackable = true;
            Hue = 1359;
        }

        public CercasReforcadas(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description
        {
            get
            {
                return 1063512;
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
