using System;

namespace Server.Items
{
    public class pregosreforcados : Item
    {
        [Constructable]
        public pregosreforcados()
            : base(0x102F)
        {
            this.Weight = 1.0;
            Name = "Pregos Reforçados";
            Stackable = true;
            Hue = 2401;
        }

        public pregosreforcados(Serial serial)
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
