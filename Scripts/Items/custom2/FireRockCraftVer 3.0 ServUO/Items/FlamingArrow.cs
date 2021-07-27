/* Created by Hammerhand */
using Server.Engines.Craft;
using System;

namespace Server.Items
{
    public class FlamingArrow : Item, ICommodity, ICraftable
    {
        [Constructable]
        public FlamingArrow()
            : this(1)
        {
        }

        [Constructable]
        public FlamingArrow(int amount)
            : base(0xF3F)
        {
            Name = "FlamingArrow";
            Hue = 1359;
            Stackable = true;
            Amount = amount;
        }

        public FlamingArrow(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description
        {
            get
            {
                return String.Format(Amount == 1 ? "{0} FlamingArrow" : "{0} FlamingArrows", Amount);

                // return 1063506;
            }
        }
        bool ICommodity.IsDeedable
        {
            get
            {
                return false;
            }
        }

       /* public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1072351); // Quest Item
        }*/

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
