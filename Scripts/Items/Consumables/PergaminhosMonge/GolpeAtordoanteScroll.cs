using System;

namespace Server.Items
{
    public class GolpeAtordoanteScroll : SpellScroll
    {
        [Constructable]
        public GolpeAtordoanteScroll()
            : this(1)
        {
        }

        [Constructable]
        public GolpeAtordoanteScroll(int amount)
            : base(866, 0x1F33, amount)
        {
        }

        public GolpeAtordoanteScroll(Serial ser)
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
