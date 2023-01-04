using System;

namespace Server.Items
{
    public class PalmaExplosivaScroll : SpellScroll
    {
        [Constructable]
        public PalmaExplosivaScroll()
            : this(1)
        {
        }

        [Constructable]
        public PalmaExplosivaScroll(int amount)
            : base(873, 0x1F33, amount)
        {
        }

        public PalmaExplosivaScroll(Serial ser)
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
