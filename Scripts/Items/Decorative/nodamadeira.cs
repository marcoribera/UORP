using System;

namespace Server.Items
{
    public class nodamadeira : Item
    {
        [Constructable]
        public nodamadeira()
            : base(0x019E)
        {
            this.Weight = 1.0;
            Name = "Nó da Madeira";
            Stackable = true;
            Hue = 1359;
        }

        public nodamadeira(Serial serial)
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
