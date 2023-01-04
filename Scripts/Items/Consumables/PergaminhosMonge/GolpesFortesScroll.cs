using System;

namespace Server.Items
{
    public class GolpesFortesScroll : SpellScroll
    {
        [Constructable]
        public GolpesFortesScroll()
            : this(1)
        {
        }

        [Constructable]
        public GolpesFortesScroll(int amount)
            : base(867, 0x1F33, amount)
        {
        }

        public GolpesFortesScroll(Serial ser)
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
