using System;

namespace Server.Items
{
    public class InvestidaAterradoraScroll : SpellScroll
    {
        [Constructable]
        public InvestidaAterradoraScroll()
            : this(1)
        {
        }

        [Constructable]
        public InvestidaAterradoraScroll(int amount)
            : base(869, 0x1F33, amount)
        {
        }

        public InvestidaAterradoraScroll(Serial ser)
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
