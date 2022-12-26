using System;

namespace Server.Items
{
    public class BagOfElementaristaPergaminhos : Bag
    {
        [Constructable]
        public BagOfElementaristaPergaminhos()
            : this(1)
        {
        }

        [Constructable]
        public BagOfElementaristaPergaminhos(int amount)
        {
            this.DropItem(new AtaqueCongelanteScroll(amount));
            this.DropItem(new AvalancheScroll(amount));
            this.DropItem(new BolaDeGeloScroll(amount));
            this.DropItem(new CampoGelidoScroll(amount));
            this.DropItem(new TempestadeCongelanteScroll(amount));
            this.DropItem(new CongelarScroll(amount));
            this.DropItem(new MorteMassivaScroll(amount));
            this.DropItem(new NuvemGasosaScroll(amount));
            




        }

        public BagOfElementaristaPergaminhos(Serial serial)
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
