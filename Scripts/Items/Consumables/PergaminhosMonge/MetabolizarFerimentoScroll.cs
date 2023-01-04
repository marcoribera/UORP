using System;

namespace Server.Items
{
    public class MetabolizarFerimentoScroll : SpellScroll
    {
        [Constructable]
        public MetabolizarFerimentoScroll()
            : this(1)
        {
        }

        [Constructable]
        public MetabolizarFerimentoScroll(int amount)
            : base(871, 0x1F33, amount)
        {
        }

        public MetabolizarFerimentoScroll(Serial ser)
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
