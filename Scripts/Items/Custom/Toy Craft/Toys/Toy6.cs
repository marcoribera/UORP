using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class Toy6 : Item
	{
        		[Constructable]
		public Toy6() : this( 1 )
		{
		}
		[Constructable]
		public Toy6( int amount ) : base( 0x20FB )
		{
			Name = "A Serpent Toy";
                        Weight = 1.0;
                        Hue = 1162;
			
		}

		public Toy6( Serial serial ) : base( serial )
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