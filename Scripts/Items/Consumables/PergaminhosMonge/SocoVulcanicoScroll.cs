using System;

namespace Server.Items
{
    public class SocoVulcanicoScroll : SpellScroll
    {
        [Constructable]
        public SocoVulcanicoScroll()
            : this(1)
        {
        }

        [Constructable]
        public SocoVulcanicoScroll(int amount)
            : base(878, 0x1F33, amount)
        {
        }

        public SocoVulcanicoScroll(Serial ser)
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
