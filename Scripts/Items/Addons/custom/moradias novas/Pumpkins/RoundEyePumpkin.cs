using System;
using Server;

namespace Server.Items
{

	public class RoundEyePumpkin : Item
	{

		[Constructable]
		public   RoundEyePumpkin() : base( 0x4696 )
		{
			Name = "Carved Round Eye Pumpkin";
                                      Weight = 0.2;
                	
		}
		

		public  RoundEyePumpkin( Serial serial ) : base( serial )
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