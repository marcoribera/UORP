using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class Toy3 : Item
	{
				[Constructable]
		public Toy3() : this( 1 )
		{
		}
		[Constructable]
		public Toy3( int amount ) : base( 0x2107 )
		{
			Name = "A Barbie Doll";
                        Weight = 1.0;
			Hue = 1162;
		}

		public Toy3( Serial serial ) : base( serial )
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