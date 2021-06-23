using System;

namespace Server.Items
{
    public class provisoesreforcadas : Item
    {
        [Constructable]
        public provisoesreforcadas()
            : base(0x2D4F)
        {
            this.Weight = 1.0;
            Name = "Provisão Reforçada";
            Stackable = true;
        }

        public provisoesreforcadas(Serial serial)
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
