using System;

namespace Server.Items
{
    public class glandulaviscosa : Item
    {
        [Constructable]
        public glandulaviscosa()
            : base(0x2DB2)
        {
            this.Weight = 1.0;
            Name = "Glândula Viscosa";
            Stackable = true;
         }

        public glandulaviscosa(Serial serial)
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
