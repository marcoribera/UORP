using System;

namespace Server.Items
{

public class FloweringVine2 : Item
	{
		[Constructable]
		public FloweringVine2() : base( 11513 )
		{
			Name = "Flowering Vine";
			Weight = 1.0;
		}

		public FloweringVine2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
