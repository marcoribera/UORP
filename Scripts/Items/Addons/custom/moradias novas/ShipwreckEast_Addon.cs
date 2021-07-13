
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
	public class ShipwreckEast_Addon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {18378, 1, 3, 0}, {18366, 2, 3, 0}, {18367, 2, 2, 0}// 1	2	3	
			, {18374, 1, 1, 0}, {18373, 1, 2, 0}, {18368, 2, 1, 0}// 4	5	6	
			, {18372, 3, -2, 0}, {18376, 1, -1, 0}, {18370, 2, -1, 0}// 7	8	9	
			, {18360, 1, 0, 0}, {18369, 2, 0, 0}, {18379, 0, 2, 0}// 10	11	12	
			, {18384, -1, 1, 0}, {18387, -2, 1, 0}, {18380, 0, 1, 0}// 13	14	15	
			, {18385, -1, 0, 0}// 16	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new ShipwreckEast_AddonDeed();
			}
		}

		[ Constructable ]
		public ShipwreckEast_Addon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public ShipwreckEast_Addon( Serial serial ) : base( serial )
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

	public class ShipwreckEast_AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new ShipwreckEast_Addon();
			}
		}

		[Constructable]
		public ShipwreckEast_AddonDeed()
		{
			Name = "ShipwreckEast_";
		}

		public ShipwreckEast_AddonDeed( Serial serial ) : base( serial )
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