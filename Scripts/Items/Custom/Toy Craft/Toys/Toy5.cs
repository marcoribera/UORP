using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class Toy5 : Item
	{
				[Constructable]
		public Toy5() : this( 1 )
		{
		}
		[Constructable]
		public Toy5( int amount ) : base( 0x212F )
		{
			Name = "A Toad Toy";
                        Weight = 1.0;
                        Hue = 1162;
			
		}

		public Toy5( Serial serial ) : base( serial )
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