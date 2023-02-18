using System;

namespace Server.Items
{
    public class LuteNova : BaseInstrument
    {
        [Constructable]
        public LuteNova()
            : base(0xCA58)
        {
            this.Name = "violao";
            this.Weight = 5.0;
           //this.Layer = Layer.FirstValid;


        }

        public LuteNova(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (this.Weight == 3.0)
                this.Weight = 5.0;
        }
    }
}
