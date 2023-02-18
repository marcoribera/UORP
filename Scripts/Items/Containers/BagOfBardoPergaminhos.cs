using System;

namespace Server.Items
{
    public class BagOfBardoPergaminhos : Bag
    {
        [Constructable]
        public BagOfBardoPergaminhos()
            : this(1)
        {
        }

        [Constructable]
        public BagOfBardoPergaminhos(int amount)
        {
            this.DropItem(new AnularBencaosScroll(amount));
            this.DropItem(new AtaqueSonicoScroll(amount));
            this.DropItem(new IlusaoExplosivaScroll(amount));
            this.DropItem(new CancaoDeNinarScroll(amount));
            this.DropItem(new CoelhoNoChapeuScroll(amount));
            this.DropItem(new EncantarCriaturaScroll(amount));
            this.DropItem(new GarrafaDeAguaScroll(amount));
            this.DropItem(new HilarioScroll(amount));
            this.DropItem(new InsultosScroll(amount));
            this.DropItem(new PalhacosScroll(amount));
            this.DropItem(new PoderDaFlorScroll(amount));
            this.DropItem(new PoteDeCobrasScroll(amount));
            this.DropItem(new SaltandoPorAiScroll(amount));
            this.DropItem(new MusicaArdenteScroll(amount));
            this.DropItem(new SomAgonizanteScroll(amount));
            this.DropItem(new SomDaAgilidadeScroll(amount));
            this.DropItem(new SomDaBurradaScroll(amount));
            this.DropItem(new SomDaForcaScroll(amount));
            this.DropItem(new SomDaFraquezaScroll(amount));
            this.DropItem(new SomDaInteligenciaScroll(amount));
            this.DropItem(new SomDaLerdezaScroll(amount));
            this.DropItem(new SomDaMelhoriaScroll(amount));
            this.DropItem(new SomDebilitanteScroll(amount));
            this.DropItem(new SomDeFestaScroll(amount));
            this.DropItem(new SomDoFocoScroll(amount));
            this.DropItem(new SomFascinanteScroll(amount));
            this.DropItem(new SomFatiganteScroll(amount));
         






        }

        public BagOfBardoPergaminhos(Serial serial)
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
