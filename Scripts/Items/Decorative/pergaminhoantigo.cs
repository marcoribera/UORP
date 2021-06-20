using System;

namespace Server.Items
{
    public class pergaminhoantigo : Item
    {
        [Constructable]
        public pergaminhoantigo()
            : base(0x0EF3)
        {
            this.Weight = 1.0;
            Name = "Pergaminho Antigo";
            Stackable = true;
            Hue = 1107;
        }

        public pergaminhoantigo(Serial serial)
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
