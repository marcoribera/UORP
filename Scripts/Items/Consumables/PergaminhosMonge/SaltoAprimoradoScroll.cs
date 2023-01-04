using System;

namespace Server.Items
{
    public class SaltoAprimoradoScroll : SpellScroll
    {
        [Constructable]
        public SaltoAprimoradoScroll()
            : this(1)
        {
        }

        [Constructable]
        public SaltoAprimoradoScroll(int amount)
            : base(875, 0x1F33, amount)
        {
        }

        public SaltoAprimoradoScroll(Serial ser)
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
