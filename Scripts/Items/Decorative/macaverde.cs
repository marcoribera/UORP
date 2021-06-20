using System;

namespace Server.Items
{
    public class macaverde : Item
    {
        [Constructable]
        public macaverde()
            : base(0x09D0)
        {
            this.Weight = 1.0;
            Name = "Maça Verde";
            Stackable = true;
            Hue = 264;
        }

        public macaverde(Serial serial)
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
