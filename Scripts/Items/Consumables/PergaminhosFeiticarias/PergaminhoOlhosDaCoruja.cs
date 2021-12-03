using System;

namespace Server.Items
{
    public class PergaminhoOlhosDaCoruja : SpellScroll
    {
        [Constructable]
        public PergaminhoOlhosDaCoruja()
            : this(1)
        {
        }

        [Constructable]
        public PergaminhoOlhosDaCoruja(int amount)
            : base(420, 0x1F33, amount)
        {
        }

        public PergaminhoOlhosDaCoruja(Serial ser)
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
