using System;

namespace Server.Items
{
    public class GolpesVelozesScroll : SpellScroll
    {
        [Constructable]
        public GolpesVelozesScroll()
            : this(1)
        {
        }

        [Constructable]
        public GolpesVelozesScroll(int amount)
            : base(868, 0x1F33, amount)
        {
        }

        public GolpesVelozesScroll(Serial ser)
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
