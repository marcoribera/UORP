using System;
using Server;

namespace Server.Items
{

	public class DarkRoundEyePumpkin : Item
	{

		[Constructable]
		public   DarkRoundEyePumpkin() : base( 0x4698 )
		{
			Name = "Dark Round Eye Pumpkin";
                                      Weight = 0.2;
                	
		}
		

		public  DarkRoundEyePumpkin( Serial serial ) : base( serial )
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