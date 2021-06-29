
////////////////////////////////////////
//                                    //
//   Generated by CEO's YAAAG - V1.2  //
// (Yet Another Arya Addon Generator) //
//                                    //
////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class snaketowerAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {9020, -1, 2, 0}, {9021, -1, 1, 0}, {9022, -1, 0, 0}// 1	2	3	
			, {9023, -1, -1, 0}, {9024, 0, -1, 0}, {9025, 1, -1, 0}// 4	5	6	
			, {9026, 2, -1, 0}, {9028, 0, 1, 0}, {9032, 1, 0, 0}// 7	8	9	
			, {9029, 0, 0, 0}, {9034, 2, 0, 0}, {9031, 1, 1, 0}// 10	11	12	
			, {9033, 2, 1, 0}, {9030, 1, 2, 0}, {9027, 0, 2, 0}// 13	14	15	
			, {9037, 2, 0, 0}, {9036, 0, 2, 0}, {9037, 1, 1, 0}// 16	17	18	
					};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new snaketowerAddonDeed();
			}
		}

		[ Constructable ]
		public snaketowerAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public snaketowerAddon( Serial serial ) : base( serial )
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

	public class snaketowerAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new snaketowerAddon();
			}
		}

		[Constructable]
		public snaketowerAddonDeed()
		{
			Name = "snaketower";
		}

		public snaketowerAddonDeed( Serial serial ) : base( serial )
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