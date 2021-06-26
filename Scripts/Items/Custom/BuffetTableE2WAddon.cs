
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
	public class BuffetTableE2WAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {40274, 2, 1, 0}, {40273, 2, 0, 0}, {40272, 1, 1, 0}// 1	2	3	
			, {40271, 1, 0, 0}, {40270, 0, 1, 0}, {40269, 0, 0, 0}// 4	5	6	
			, {40268, -1, 1, 0}, {40267, -1, 0, 0}, {40266, -2, 1, 0}// 7	8	9	
			, {40265, -2, 0, 0}// 10	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new BuffetTableE2WAddonDeed();
			}
		}

		[ Constructable ]
		public BuffetTableE2WAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public BuffetTableE2WAddon( Serial serial ) : base( serial )
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

	public class BuffetTableE2WAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new BuffetTableE2WAddon();
			}
		}

		[Constructable]
		public BuffetTableE2WAddonDeed()
		{
			Name = "Buffet Table E2W";
		}

		public BuffetTableE2WAddonDeed( Serial serial ) : base( serial )
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