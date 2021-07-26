using System;


namespace Server.Items
{
    public class PoisonBolt : Item
	{
		[Constructable]
		public PoisonBolt() : this( 1 )
		{
		}

		[Constructable]
		public PoisonBolt( int amount ) : base( 0x1BFB )
		{
			Name = "Poison Bolt";
			Hue = 69;
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
		}

		public PoisonBolt( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
