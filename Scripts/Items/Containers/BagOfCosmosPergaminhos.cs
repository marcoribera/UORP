using System;

namespace Server.Items
{
    public class BagOfCosmosSolarPergaminhos : Bag
    {
        [Constructable]
        public BagOfCosmosSolarPergaminhos()
            : this(1)
        {
        }

        [Constructable]
        public BagOfCosmosSolarPergaminhos(int amount)
        {
            this.DropItem(new AceleracaoScroll(amount));
            this.DropItem(new ArremessoScroll(amount));
            this.DropItem(new AuraPsiquicaScroll(amount));
            this.DropItem(new CampoDeExtaseScroll(amount));
            this.DropItem(new DefletirScroll(amount));
            this.DropItem(new MaoCosmicaScroll(amount));
            this.DropItem(new MiragemScroll(amount));
           
            this.DropItem(new ToqueCalmanteScroll(amount));
            




        }

        public BagOfCosmosSolarPergaminhos(Serial serial)
            : base(serial)
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
