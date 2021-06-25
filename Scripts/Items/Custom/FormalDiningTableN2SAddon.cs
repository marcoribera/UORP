
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
	public class FormalDiningTableN2SAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {40306, 1, 3, 0}, {40305, 0, 3, 0}, {40304, 1, 2, 0}// 1	2	3	
			, {40303, 0, 2, 0}, {40302, 1, 1, 0}, {40301, 0, 1, 0}// 4	5	6	
			, {40300, 1, 0, 0}, {40299, 0, 0, 0}, {40298, 1, -1, 0}// 7	8	9	
			, {40297, 0, -1, 0}, {40296, 1, -2, 0}, {40295, 0, -2, 0}// 10	11	12	
					};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new FormalDiningTableN2SAddonDeed();
			}
		}

		[ Constructable ]
		public FormalDiningTableN2SAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public FormalDiningTableN2SAddon( Serial serial ) : base( serial )
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

	public class FormalDiningTableN2SAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new FormalDiningTableN2SAddon();
			}
		}

		[Constructable]
		public FormalDiningTableN2SAddonDeed()
		{
			Name = "Formal Dining Table";
		}

		public FormalDiningTableN2SAddonDeed( Serial serial ) : base( serial )
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
