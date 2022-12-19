using System;

namespace Server.Items
{
    public class BagOfCosmosLunarPergaminhos : Bag
    {
        [Constructable]
        public BagOfCosmosLunarPergaminhos()
            : this(1)
        {
        }

        [Constructable]
        public BagOfCosmosLunarPergaminhos(int amount)
        {
            this.DropItem(new ApertoDaMorteScroll(amount));
            this.DropItem(new CeleridadeScroll(amount));
            this.DropItem(new DrenarVidaScroll(amount));
            this.DropItem(new ExplosaoPsiquicaScroll(amount));
            this.DropItem(new IlusaoScroll(amount));
            this.DropItem(new LancamentoScroll(amount));
            this.DropItem(new RaioScroll(amount));
           this.DropItem(new RedirecionarScroll(amount));
            




        }

        public BagOfCosmosLunarPergaminhos(Serial serial)
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
