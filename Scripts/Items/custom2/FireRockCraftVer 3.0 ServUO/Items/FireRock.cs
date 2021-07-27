/* Created by Hammerhand*/
using Server.Engines.Craft;
using System;

namespace Server.Items
{
    public class SmallFireRock : Item, ICommodity, ICraftable
    {
        [Constructable]
        public SmallFireRock()
            : this(1)
        {
        }

        [Constructable]
        public SmallFireRock(int amount)
            : base(0x1366)
        {
            Stackable = true;
            Weight = 1;
            Hue = 1359;
            Name = "SmallFireRock";
            Amount = amount;
        }

        public SmallFireRock(Serial serial)
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



    public class LargeFireRock : Item, ICommodity, ICraftable
    {
        [Constructable]
        public LargeFireRock()
            : this(1)
        {
        }

        [Constructable]
        public LargeFireRock(int amount)
            : base(0x1363)
        {
            Stackable = true;
            Weight = 3;
            Hue = 1359;
            Name = "LargeFireRock";
            Amount = amount;
        }

        public LargeFireRock(Serial serial)
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

