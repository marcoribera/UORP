using System;

namespace Server.Items
{
    public class cercasreforcadas : Item
    {
        [Constructable]
        public cercasreforcadas()
            : base(0x0862)
        {
            this.Weight = 1.0;
            Name = "Cercas Reforçadas";
            Stackable = true;
            Hue = 1359;
        }

        public cercasreforcadas(Serial serial)
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
