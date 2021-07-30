using System;
using Server;

namespace Server.Items
{

	public class SlantedEyePumpkin : Item
	{

		[Constructable]
		public   SlantedEyePumpkin() : base( 0x4692 )
		{
			Name = "Carved Slanted Eye Pumpkin";
                                      Weight = 0.2;
                	
		}
		

		public  SlantedEyePumpkin( Serial serial ) : base( serial )
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