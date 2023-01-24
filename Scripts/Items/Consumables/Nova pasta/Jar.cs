

using System;

namespace Server.Items
{
    public class Jar : Item, ICommodity
    {
        [Constructable]
        public Jar()
            : this(1)
        {
        }

        [Constructable]
        public Jar(int amount)
            : base(0xA1E8)
        {
            this.Name = "pote";
            this.Stackable = true;
            this.Weight = 1.0;
            this.Amount = amount;
        }

        public Jar(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description
        {
            get
            {
                return this.LabelNumber;
            }
        }
        bool ICommodity.IsDeedable
        {
            get
            {
                return (Core.ML);
            }
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
             ItemID = 0xA1E8;
        }
    }
}
