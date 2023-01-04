using System;

namespace Server.Items
{
    public class MetabolizarVenenoScroll : SpellScroll
    {
        [Constructable]
        public MetabolizarVenenoScroll()
            : this(1)
        {
        }

        [Constructable]
        public MetabolizarVenenoScroll(int amount)
            : base(872, 0x1F33, amount)
        {
        }

        public MetabolizarVenenoScroll(Serial ser)
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
