using System;

namespace Server.Items
{
    public class BagOfClerigoBomPergaminhos : Bag
    {
        [Constructable]
        public BagOfClerigoBomPergaminhos()
            : this(1)
        {
        }

        [Constructable]
        public BagOfClerigoBomPergaminhos(int amount)
        {
            this.DropItem(new AlimentoDaVidaScroll(amount));
            this.DropItem(new ElevarAgilidadeScroll(amount));
            this.DropItem(new ElevarForcaScroll(amount));
            this.DropItem(new ElevarInteligenciaScroll(amount));
            this.DropItem(new AscencaoCompletaScroll(amount));
            this.DropItem(new AuraPurificadoraScroll(amount));
            this.DropItem(new ClarearVistaScroll(amount));
            this.DropItem(new CuraLeveScroll(amount));
            this.DropItem(new CuraMinimaScroll(amount));
            this.DropItem(new CuraModeradaScroll(amount));
            this.DropItem(new FogoDeVidaScroll(amount));
            this.DropItem(new ProtecaoDaLuzScroll(amount));
            this.DropItem(new PurificarAreaScroll(amount));
            this.DropItem(new PurificarScroll(amount));
            this.DropItem(new ReanimarMortoScroll(amount));
            this.DropItem(new RecuperacaoBreveScroll(amount));
            this.DropItem(new RemoverMaldicaoScroll(amount));
            this.DropItem(new ReviverScroll(amount));
            this.DropItem(new VidaEternaScroll(amount));
          




        }

        public BagOfClerigoBomPergaminhos(Serial serial)
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
