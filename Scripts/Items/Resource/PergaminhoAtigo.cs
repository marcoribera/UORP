using System;

namespace Server.Items
{
    public class PergaminhoAntigo : Item, ICommodity
    {
        [Constructable]
        public PergaminhoAntigo()
            : this(1)
        {
        }

        [Constructable]
        public PergaminhoAntigo(int amount)
            : base(0xEF3)
        {
            this.Weight = 1.0;
            Name = "Pergaminho Antigo";
            Stackable = true;
            Hue = 1107;
        }

        public PergaminhoAntigo(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description
        {
            get
            {
                return 1063503; //Pergaminho Antigo
            }
        }
        bool ICommodity.IsDeedable
        {
            get
            {
                return false;
            }
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
