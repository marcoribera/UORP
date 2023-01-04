using System;

namespace Server.Items
{
    public class GolpeAscendenteScroll : SpellScroll
    {
        [Constructable]
        public GolpeAscendenteScroll()
            : this(1)
        {
        }

        [Constructable]
        public GolpeAscendenteScroll(int amount)
            : base(865, 0x1F33, amount)
        {
        }

        public GolpeAscendenteScroll(Serial ser)
            : base(ser)
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
        }
    }
}
