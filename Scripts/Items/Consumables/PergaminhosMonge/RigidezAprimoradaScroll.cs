using System;

namespace Server.Items
{
    public class RigidezAprimoradaScroll : SpellScroll
    {
        [Constructable]
        public RigidezAprimoradaScroll()
            : this(1)
        {
        }

        [Constructable]
        public RigidezAprimoradaScroll(int amount)
            : base(874, 0x1F33, amount)
        {
        }

        public RigidezAprimoradaScroll(Serial ser)
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
