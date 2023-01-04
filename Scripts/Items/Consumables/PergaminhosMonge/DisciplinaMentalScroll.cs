using System;

namespace Server.Items
{
    public class DisciplinaMentalScroll : SpellScroll
    {
        [Constructable]
        public DisciplinaMentalScroll()
            : this(1)
        {
        }

        [Constructable]
        public DisciplinaMentalScroll(int amount)
            : base(862, 0x1F33, amount)
        {
        }

        public DisciplinaMentalScroll(Serial ser)
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
