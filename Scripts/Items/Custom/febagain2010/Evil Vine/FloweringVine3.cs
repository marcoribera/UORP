using System;

namespace Server.Items
{

public class FloweringVine3 : Item
	{
		[Constructable]
		public FloweringVine3() : base( 11514 )
		{
			Name = "Flowering Vine";
			Weight = 1.0;
		}

		public FloweringVine3( Serial serial ) : base( serial )
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
