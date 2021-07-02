
////////////////////////////////////////
//                                     //
//   Generated by CEO's YAAAG - Ver 2  //
// (Yet Another Arya Addon Generator)  //
//    Modified by Hammerhand for       //
//      SA & High Seas content         //
//                                     //
////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class Barded_HorseEastAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {4984, 1, 1, 0}, {4985, 1, 0, 0}, {1305, 0, 1, 0}// 1	2	3	
			, {1305, 0, -1, 0}, {1305, 1, -1, 0}, {1305, 1, 0, 0}// 4	5	6	
			, {1305, 1, 1, 0}, {1305, 0, 0, 0}// 7	8	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new Barded_HorseEastAddonDeed();
			}
		}

		[ Constructable ]
		public Barded_HorseEastAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public Barded_HorseEastAddon( Serial serial ) : base( serial )
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

	public class Barded_HorseEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Barded_HorseEastAddon();
			}
		}

		[Constructable]
		public Barded_HorseEastAddonDeed()
		{
			Name = "Barded_HorseEast";
		}

		public Barded_HorseEastAddonDeed( Serial serial ) : base( serial )
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