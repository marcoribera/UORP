using System;

namespace Server.Items
{
    public class ovodedino : Item
    {
        [Constructable]
        public ovodedino()
            : base(0x41BF)
        {
            this.Weight = 1.0;
            Name = "Ovo de Dinossauro";
            Stackable = true;
        }

        public ovodedino(Serial serial)
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
