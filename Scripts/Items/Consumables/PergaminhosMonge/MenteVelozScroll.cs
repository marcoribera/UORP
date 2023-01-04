using System;

namespace Server.Items
{
    public class MenteVelozScroll : SpellScroll
    {
        [Constructable]
        public MenteVelozScroll()
            : this(1)
        {
        }

        [Constructable]
        public MenteVelozScroll(int amount)
            : base(870, 0x1F33, amount)
        {
        }

        public MenteVelozScroll(Serial ser)
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
