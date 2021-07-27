/* Created by Hammerhand */
/* Created by Hammerhand*/
using Server.Engines.Craft;
using System;

namespace Server.Items
{
    public class FireBolt : Item, ICommodity, ICraftable
    {
        [Constructable]
        public FireBolt()
            : this(1)
        {
        }

        [Constructable]
        public FireBolt(int amount)
            : base(0x1BFB)
        {
            Name = "FireBolt";
            Hue = 1359;
            Stackable = true;
            Amount = amount;
        }

        public FireBolt(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description
        {
            get
            {
                //return String.Format(Amount == 1 ? "{0} FlamingArrow" : "{0} FlamingArrows", Amount);

                return 1063506;
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
