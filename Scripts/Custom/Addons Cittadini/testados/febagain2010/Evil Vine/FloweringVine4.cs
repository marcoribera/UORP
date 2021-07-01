using System;

namespace Server.Items
{

public class FloweringVine4 : Item
	{
		[Constructable]
		public FloweringVine4() : base( 11515 )
		{
			Name = "Flowering Vine";
			Weight = 1.0;
		}

		public FloweringVine4( Serial serial ) : base( serial )
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
