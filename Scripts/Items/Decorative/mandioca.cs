using System;

namespace Server.Items
{
    public class mandioca : Item
    {
        [Constructable]
        public mandioca()
            : base(0x0C77)
        {
            this.Weight = 1.0;
            Name = "Mandioca";
            Stackable = true;
            Hue = 1141;
        }

        public mandioca(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

}
