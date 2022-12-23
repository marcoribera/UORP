using System;

namespace Server.Items
{
    public class BagOfClerigoDosMortosPergaminhos : Bag
    {
        [Constructable]
        public BagOfClerigoDosMortosPergaminhos ()
            : this(1)
        {
        }

        [Constructable]
        public BagOfClerigoDosMortosPergaminhos (int amount)

        {
            this.DropItem(new AbrirFeridasScroll(amount));
            this.DropItem(new AlimentoDaMorteScroll(amount));
            this.DropItem(new CampoVenenosoScroll(amount));
            this.DropItem(new DrenarAgilidadeScroll(amount));
            this.DropItem(new DrenarEssenciaScroll(amount));
            this.DropItem(new DrenarForcaScroll(amount));
            this.DropItem(new DrenarInteligenciaScroll(amount));
            this.DropItem(new DrenarManaScroll(amount));
            this.DropItem(new DrenarQuintessenciaScroll(amount));
            this.DropItem(new EntorpecerScroll(amount));
            this.DropItem(new EnvenenarMenteScroll(amount));
            this.DropItem(new EnvenenarScroll(amount));
            this.DropItem(new ErguerCadaverScroll(amount));
            this.DropItem(new EscurecerVistaScroll(amount));
            this.DropItem(new FogoDaMorteScroll(amount));
            this.DropItem(new PactoDeSangueScroll(amount));
            this.DropItem(new ProtecaoDasTrevasScroll(amount));
            this.DropItem(new RitualLichScroll(amount));
            this.DropItem(new SonolenciaScroll(amount));
          




        }

        public BagOfClerigoDosMortosPergaminhos (Serial serial)

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
