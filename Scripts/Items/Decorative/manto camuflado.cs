using System;

namespace Server.Items
{
    public class mantocamuflado : Item
    {
        [Constructable]
        public mantocamuflado()
            : base(0x1515)
        {
            this.Weight = 1.0;
            Name = "Manto Camuflado";
            Stackable = true;
            Hue = 367;
        }

        public mantocamuflado(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

}
