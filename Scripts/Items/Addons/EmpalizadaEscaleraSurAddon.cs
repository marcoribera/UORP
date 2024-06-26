
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
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
	public class EmpalizadaEscaleraSurAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {546, 2, 1, 14}, {546, 0, 1, 14}, {546, -1, 1, 2}// 1	2	3	
			, {546, 1, 1, 2}, {546, 0, 1, 2}, {546, 2, 1, 2}// 4	5	6	
			, {1221, -1, 0, 14}, {1221, 0, 0, 14}, {1221, 1, 0, 14}// 7	8	9	
			, {1221, 2, 0, 14}, {545, 2, 1, 2}, {2202, 1, -1, 0}// 10	11	12	
			, {2202, 0, -1, 5}, {1221, -1, -1, 13}// 13	14	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new EmpalizadaEscaleraSurAddonDeed();
			}
		}

		[ Constructable ]
		public EmpalizadaEscaleraSurAddon()
		{


            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public EmpalizadaEscaleraSurAddon( Serial serial ) : base( serial )
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

	public class EmpalizadaEscaleraSurAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new EmpalizadaEscaleraSurAddon();
			}
		}

		[Constructable]
		public EmpalizadaEscaleraSurAddonDeed()
		{
			Name = "EmpalizadaEscaleraSur";
		}

		public EmpalizadaEscaleraSurAddonDeed( Serial serial ) : base( serial )
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
