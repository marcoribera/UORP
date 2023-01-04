using System;

namespace Server.Items
{
    public class SocoTectonicoScroll : SpellScroll
    {
        [Constructable]
        public SocoTectonicoScroll()
            : this(1)
        {
        }

        [Constructable]
        public SocoTectonicoScroll(int amount)
            : base(877, 0x1F33, amount)
        {
        }

        public SocoTectonicoScroll(Serial ser)
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
