using System;

namespace Server.Items
{
    public class SocoDoKiScroll : SpellScroll
    {
        [Constructable]
        public SocoDoKiScroll()
            : this(1)
        {
        }

        [Constructable]
        public SocoDoKiScroll(int amount)
            : base(876, 0x1F33, amount)
        {
        }

        public SocoDoKiScroll(Serial ser)
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
