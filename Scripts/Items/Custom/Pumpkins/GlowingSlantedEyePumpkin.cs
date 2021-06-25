using System;
using Server;

namespace Server.Items
{

	public class GlowingSlantedEyePumpkin : Item
	{

		[Constructable]
		public   GlowingSlantedEyePumpkin() : base( 0x4691 )
		{
			Name = "Scary Glowing Slanted Eye Pumpkin";
                                      Weight = 0.2;
                	
		}
		

		public  GlowingSlantedEyePumpkin( Serial serial ) : base( serial )
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