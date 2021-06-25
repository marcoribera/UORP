using System;
using Server;

namespace Server.Items
{

	public class GlowingRoundEyePumpkin : Item
	{

		[Constructable]
		public   GlowingRoundEyePumpkin() : base( 0x4695 )
		{
			Name = "Scary Glowing Round Eye Pumpkin";
                                      Weight = 0.2;
                	
		}
		

		public  GlowingRoundEyePumpkin( Serial serial ) : base( serial )
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