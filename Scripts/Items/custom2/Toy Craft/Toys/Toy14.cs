using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class Toy14 : Item
	{
		[Constructable]
		public Toy14() : this( 1 )
		{
		}
		
		[Constructable]
		public Toy14( int amount ) : base( 0x2581 )
		{
			Name = "A Centaur Toy";
                        Weight = 1.0;
                        Hue = 1162;
			
		}

		public Toy14( Serial serial ) : base( serial )
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