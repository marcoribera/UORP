using System;

namespace Server.Items
{
    public class sacodeconcreto : Item
    {
        [Constructable]
        public sacodeconcreto()
            : base(0x1039)
        {
            this.Weight = 1.0;
            Name = "Saco de Concreto";
            Stackable = true;
            Hue = 906;
        }

        public sacodeconcreto(Serial serial)
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
