using System;

namespace Server.Items
{
    public class carvao : Item
    {
        [Constructable]
        public carvao()
            : base(0x09EA)
        {
            this.Weight = 1.0;
            Name = "Carvão";
            Stackable = true;
            Hue = 1797;
        }

        public carvao(Serial serial)
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
