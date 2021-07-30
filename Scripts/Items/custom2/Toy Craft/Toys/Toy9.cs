using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class Toy9 : Item
	{
		[Constructable]
		public Toy9() : this( 1 )
		{
		}
		
		[Constructable]
		public Toy9( int amount ) : base( 0x211C )
		{
			Name = "A Dog Toy";
                        Weight = 1.0;
                        Hue = 1162;
			
		}

		public Toy9( Serial serial ) : base( serial )
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