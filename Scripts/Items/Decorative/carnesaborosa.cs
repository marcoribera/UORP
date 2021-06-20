using System;

namespace Server.Items
{
    public class carnesaborosa : Item
    {
        [Constructable]
        public carnesaborosa()
            : base(0x09F2)
        {
            this.Weight = 1.0;
            Name = "Carne Saborosa";
            Stackable = true;
            Hue = 237;
        }

        public carnesaborosa(Serial serial)
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
