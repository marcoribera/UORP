using System;

namespace Server.Items
{
    public class tijoloreforcado : Item
    {
        [Constructable]
        public tijoloreforcado()
            : base(0x9ACA)
        {
            this.Weight = 1.0;
            Name = "Tijolo Reforçado";
            Stackable = true;
            Hue = 40;
        }

        public tijoloreforcado(Serial serial)
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
