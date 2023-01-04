using System;

namespace Server.Items
{
    public class FerimentoInternoScroll : SpellScroll
    {
        [Constructable]
        public FerimentoInternoScroll()
            : this(1)
        {
        }

        [Constructable]
        public FerimentoInternoScroll(int amount)
            : base(864, 0x1F33, amount)
        {
        }

        public FerimentoInternoScroll(Serial ser)
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
