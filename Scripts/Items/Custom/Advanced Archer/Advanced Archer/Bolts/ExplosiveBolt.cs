using System;


namespace Server.Items
{
    public class ExplosiveBolt : Item
	{
		[Constructable]
		public ExplosiveBolt() : this( 1 )
		{
		}

		[Constructable]
		public ExplosiveBolt( int amount ) : base( 0x1BFB )
		{
			Name = "Explosive Bolt";
			Hue = 32;
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
		}

		public ExplosiveBolt( Serial serial ) : base( serial )
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
