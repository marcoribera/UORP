using System;

namespace Server.Items
{
    public class BagOfMongePergaminhos : Bag
    {
        [Constructable]
        public BagOfMongePergaminhos()
            : this(1)
        {
        }

        [Constructable]
        public BagOfMongePergaminhos(int amount)
        {
            this.DropItem(new AtaqueElementalScroll(amount));
            this.DropItem(new CorrerNoVentoScroll(amount));
            this.DropItem(new DisciplinaMentalScroll(amount));
            this.DropItem(new EsferaDeKiScroll(amount));
            this.DropItem(new FerimentoInternoScroll(amount));
            this.DropItem(new GolpeAscendenteScroll(amount));
            this.DropItem(new GolpeAtordoanteScroll(amount));
            this.DropItem(new GolpesFortesScroll(amount));
            this.DropItem(new GolpesVelozesScroll(amount));
            this.DropItem(new InvestidaFatalScroll(amount));
            this.DropItem(new MenteVelozScroll(amount));
            this.DropItem(new MetabolizarFeridaScroll(amount));
            this.DropItem(new SuprimirVenenoScroll(amount));
            this.DropItem(new PalmaExplosivaScroll(amount));
            this.DropItem(new RigidezPerfeitaScroll(amount));
            this.DropItem(new SaltoAprimoradoScroll(amount));
            this.DropItem(new SocoDoKiScroll(amount));
            this.DropItem(new SocoTectonicoScroll(amount));
            this.DropItem(new SocoVulcanicoScroll(amount));
            this.DropItem(new SuperacaoScroll(amount));
            






        }

        public BagOfMongePergaminhos(Serial serial)
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
