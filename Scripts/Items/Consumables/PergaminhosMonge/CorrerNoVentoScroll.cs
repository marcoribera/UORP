using System;

namespace Server.Items
{
    public class CorrerNoVentoScroll : SpellScroll
    {
        [Constructable]
        public CorrerNoVentoScroll()
            : this(1)
        {
        }

        [Constructable]
        public CorrerNoVentoScroll(int amount)
            : base(861, 0x1F33, amount)
        {
        }

        public CorrerNoVentoScroll(Serial ser)
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
