using System;

namespace Server.Items
{
    public class BagOfAlgozPergaminhos : Bag
    {
        [Constructable]
        public BagOfAlgozPergaminhos()
            : this(1)
        {
        }

        [Constructable]
        public BagOfAlgozPergaminhos(int amount)
        {
            this.DropItem(new EmbasbacarScroll(amount));
            this.DropItem(new IntelectoDoAcolitoScroll(amount));
            this.DropItem(new EnfraquecerScroll(amount));
            this.DropItem(new ForcaDoAcolitoScroll(amount));
            this.DropItem(new AtrapalharScroll(amount));
            this.DropItem(new AgilidadeDoAcolitoScroll(amount));
            this.DropItem(new ToqueDaDorScroll(amount));
            this.DropItem(new BanimentoProfanoScroll(amount));
            this.DropItem(new ArmaVampiricaScroll(amount));
            this.DropItem(new BanimentoDemoniacoScroll(amount));
            this.DropItem(new BencaoProfanaScroll(amount));
            this.DropItem(new DesafioProfanoScroll(amount));
            this.DropItem(new EspiritoMalignoScroll(amount));
            this.DropItem(new FormaVampiricaScroll(amount));
            this.DropItem(new FuriaProfanaScroll(amount));
            this.DropItem(new HaloProfanoScroll(amount));
            this.DropItem(new PeleCadavericaScroll(amount));
            this.DropItem(new SaudeProfanaScroll(amount));




        }

        public BagOfAlgozPergaminhos(Serial serial)
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
