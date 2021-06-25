
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
	public class SkullRugSouthAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {14441, -3, 2, 0}, {14444, -3, -1, 0}, {14443, -3, 0, 0}// 1	2	3	
			, {14442, -3, 1, 0}, {14468, 3, 2, 0}, {14467, 3, -1, 0}// 4	5	6	
			, {14466, 3, 0, 0}, {14465, 3, 1, 0}, {14464, 2, 2, 0}// 7	8	9	
			, {14463, 2, -1, 0}, {14462, 2, 0, 0}, {14461, 2, 1, 0}// 10	11	12	
			, {14460, 1, 2, 0}, {14459, 1, -1, 0}, {14458, 1, 0, 0}// 13	14	15	
			, {14457, 1, 1, 0}, {14456, 0, 2, 0}, {14455, 0, -1, 0}// 16	17	18	
			, {14454, 0, 0, 0}, {14453, 0, 1, 0}, {14452, -1, 2, 0}// 19	20	21	
			, {14451, -1, -1, 0}, {14450, -1, 0, 0}, {14449, -1, 1, 0}// 22	23	24	
			, {14448, -2, 2, 0}, {14447, -2, -1, 0}, {14446, -2, 0, 0}// 25	26	27	
			, {14445, -2, 1, 0}// 28	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new SkullRugSouthAddonDeed();
			}
		}

		[ Constructable ]
		public SkullRugSouthAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public SkullRugSouthAddon( Serial serial ) : base( serial )
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

	public class SkullRugSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SkullRugSouthAddon();
			}
		}

		[Constructable]
		public SkullRugSouthAddonDeed()
		{
			Name = "SkullRugSouth";
		}

		public SkullRugSouthAddonDeed( Serial serial ) : base( serial )
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