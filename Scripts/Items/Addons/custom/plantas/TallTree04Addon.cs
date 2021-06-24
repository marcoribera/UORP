/////////////////////////////////////////////////
//
// Automatically generated by the
// AddonGenerator script by Arya
//
/////////////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class TallTree04Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new TallTree04AddonDeed();
			}
		}

		[ Constructable ]
		public TallTree04Addon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 3427 );
			AddComponent( ac, -3, 3, 0 );
			ac = new AddonComponent( 3428 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 3429 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 3430 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 3431 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 3432 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 3433 );
			AddComponent( ac, 3, -3, 0 );
			ac = new AddonComponent( 3418 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 3417 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 3416 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 3419 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 3415 );
			AddComponent( ac, -2, 2, 0 );

		}

		public TallTree04Addon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class TallTree04AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new TallTree04Addon();
			}
		}

		[Constructable]
		public TallTree04AddonDeed()
		{
			Name = "TallTree04";
		}

		public TallTree04AddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}