using System;

namespace Server.Items
{
    public class SuperacaoScroll : SpellScroll
    {
        [Constructable]
        public SuperacaoScroll()
            : this(1)
        {
        }

        [Constructable]
        public SuperacaoScroll(int amount)
            : base(879, 0x1F33, amount)
        {
        }

        public SuperacaoScroll(Serial ser)
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
