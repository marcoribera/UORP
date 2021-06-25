
////////////////////////////////////////
//                                     //
//   Generated by CEO's YAAAG - Ver 2  //
// (Yet Another Arya Addon Generator)  //
//    Modified by Hammerhand for       //
//      SA & High Seas content         //
//     Created by Hiro 12-30-2016      //
////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class GreyLowFPSouthAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {1993, 2, 0, 10}, {1993, -2, 0, 10}, {1993, -1, 0, 10}// 7	8	9	
			, {1993, 0, 0, 10}, {1993, 1, 0, 10}, {1822, 2, 0, 5}// 10	11	12	
			, {1822, 2, 0, 0}, {1822, -2, 0, 5}, {1822, -2, 0, 0}// 13	14	15	
			, {1305, -2, 1, 0}, {1305, 2, 0, 0}, {1305, 2, 1, 0}// 16	17	18	
			, {1305, -2, 0, 0}, {1305, -1, 0, 0}, {1305, -1, 1, 0}// 19	20	21	
			, {1305, 1, 0, 0}, {1305, 1, 1, 0}, {1305, 0, 0, 0}// 22	23	24	
			, {1305, 0, 1, 0}// 25	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new GreyLowFPSouthAddonDeed();
			}
		}

		[ Constructable ]
		public GreyLowFPSouthAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 3555, 1, 0, 0, 0, 29, "", 1);// 1
			AddComplexComponent( (BaseAddon) this, 3555, 1, 0, 4, 0, 29, "", 1);// 2
			AddComplexComponent( (BaseAddon) this, 3555, 0, 0, 0, 0, 29, "", 1);// 3
			AddComplexComponent( (BaseAddon) this, 3555, 0, 0, 4, 0, 29, "", 1);// 4
			AddComplexComponent( (BaseAddon) this, 3555, -1, 0, 0, 0, 29, "", 1);// 5
			AddComplexComponent( (BaseAddon) this, 3555, -1, 0, 4, 0, 29, "", 1);// 6

		}

		public GreyLowFPSouthAddon( Serial serial ) : base( serial )
		{
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
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

	public class GreyLowFPSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new GreyLowFPSouthAddon();
			}
		}

		[Constructable]
		public GreyLowFPSouthAddonDeed()
		{
			Name = "GreyLowFPSouth";
		}

		public GreyLowFPSouthAddonDeed( Serial serial ) : base( serial )
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
