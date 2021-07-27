/* Created by Hammerhand*/
using Server.Engines.Craft;
using System;

namespace Server.Items
{
    public class CrystalineFire : Item, ICommodity, ICraftable
    {
        [Constructable]
        public CrystalineFire()
            : this(1)
        {
        }

        [Constructable]
        public CrystalineFire(int amount)
            : base(7961)
        {
            Stackable = true;
            Name = "Crystaline Fire";
            Hue = 1260;
            Weight = 1.0;
            Amount = amount;
        }

        public CrystalineFire(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description
        {
            get
            {
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
