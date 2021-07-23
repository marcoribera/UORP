using System;
using Server.Items;

namespace Server.Items
{
	public class LightPillowHuge : Item
	{
		[Constructable]
		public LightPillowHuge() : base( 0x163A )
		{
			Weight = 1.0;
			Name = "A Pillow";
			Movable = true;
		}

		public LightPillowHuge( Serial serial ) : base( serial )
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