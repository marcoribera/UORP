using System;

namespace Server.Items
{
    public class AtaqueElementalScroll : SpellScroll
    {
        [Constructable]
        public AtaqueElementalScroll()
            : this(1)
        {
        }

        [Constructable]
        public AtaqueElementalScroll(int amount)
            : base(860, 0x1F33, amount)
        {
        }

        public AtaqueElementalScroll(Serial ser)
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
