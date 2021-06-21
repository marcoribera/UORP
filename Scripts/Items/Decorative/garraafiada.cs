using System;

namespace Server.Items
{
    public class garraafiada : Item
    {
        [Constructable]
        public garraafiada()
            : base(0x0F7E)
        {
            this.Weight = 1.0;
            Name = "Garra Afiada";
            Stackable = true;
            Hue = 2498;
        }

        public garraafiada(Serial serial)
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
