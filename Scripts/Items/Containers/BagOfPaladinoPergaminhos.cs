using System;

namespace Server.Items
{
    public class BagOfPaladinoPergaminhos : Bag
    {
        [Constructable]
        public BagOfPaladinoPergaminhos()
            : this(1)
        {
        }

        [Constructable]
        public BagOfPaladinoPergaminhos(int amount)
        {
            this.DropItem(new AgilidadeDoDevotoScroll(amount));
            this.DropItem(new ArmaSagradaScroll(amount));
            this.DropItem(new BanimentoCelestialScroll(amount));
            this.DropItem(new BanimentoSagradoScroll(amount));
            this.DropItem(new BencaoSagradaScroll(amount));
            this.DropItem(new DesafioSagradoScroll(amount));
            this.DropItem(new EmanacaoPuraScroll(amount));
            this.DropItem(new EspiritoBenignoScroll(amount));
            this.DropItem(new ForcaDoDevotoScroll(amount));
            this.DropItem(new FuriaSagradaScroll(amount));
            this.DropItem(new HaloDivinoScroll(amount));
            this.DropItem(new IntelectoDoDevotoScroll(amount));
            this.DropItem(new SacrificioSantoScroll(amount));
            this.DropItem(new SaudeDivinaScroll(amount));
            this.DropItem(new ToqueCircatrizanteScroll(amount));
            this.DropItem(new ToqueCurativoScroll(amount));
            this.DropItem(new ToqueRegeneradorScroll(amount));




        }

        public BagOfPaladinoPergaminhos(Serial serial)
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
