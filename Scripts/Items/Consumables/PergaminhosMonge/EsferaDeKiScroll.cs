using System;

namespace Server.Items
{
    public class EsferaDeKiScroll : SpellScroll
    {
        [Constructable]
        public EsferaDeKiScroll()
            : this(1)
        {
        }

        [Constructable]
        public EsferaDeKiScroll(int amount)
            : base(863, 0x1F33, amount)
        {
        }

        public EsferaDeKiScroll(Serial ser)
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
