
////////////////////////////////////////
//                                     //
//   Generated by CEO's YAAAG - Ver 2  //
// (Yet Another Arya Addon Generator)  //
//    Modified by Hammerhand for       //
//      SA & High Seas content         //
//       Deco by Hiro 12-30-16         //
////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class GreyEastTallFirePlaceAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {1822, 0, 2, 10}, {1822, 0, 1, 10}, {1822, 0, 0, 10}// 2	3	4	
			, {1822, 0, -1, 10}, {1822, 0, 2, 5}, {1822, 0, -2, 10}// 5	6	7	
			, {1997, 0, 2, 15}, {1997, 0, -2, 15}, {1997, 0, -1, 15}// 8	9	10	
			, {1997, 0, 1, 15}, {1997, 0, 0, 15}, {1822, 0, 2, 0}// 11	12	15	
			, {1822, 0, -2, 5}, {1822, 0, -2, 0}, {1305, 1, -1, 0}// 16	17	18	
			, {1305, 1, 0, 0}, {1305, 1, 1, 0}, {1305, 0, 2, 0}// 19	20	21	
			, {1305, 0, -2, 0}, {1305, 0, 0, 0}, {1305, 0, 1, 0}// 22	23	24	
			, {1305, 0, -1, 0}, {1305, 1, -2, 0}, {1305, 1, 2, 0}// 25	26	27	
			, {1822, -1, 0, 16}, {1822, -1, -2, 16}, {1822, -1, -1, 16}// 28	29	30	
			, {1822, -1, 1, 16}, {1822, -1, 2, 16}, {1822, -1, 2, 10}// 31	32	33	
			, {1822, -1, 1, 10}, {1822, -1, 0, 10}, {1822, -1, -1, 10}// 34	35	36	
			, {1822, -1, -2, 5}, {1822, -1, -1, 5}, {1822, -1, 2, 0}// 37	38	39	
			, {1822, -1, 1, 0}, {1822, -1, 0, 0}, {1822, -1, -1, 0}// 40	41	42	
			, {1822, -1, 2, 5}, {1822, -1, 1, 5}, {1822, -1, 0, 5}// 43	44	45	
			, {1822, -1, -2, 10}, {1997, -1, 2, 15}, {1997, -1, 1, 15}// 46	47	48	
			, {1997, -1, -2, 15}, {1997, -1, -1, 15}, {1997, -1, 0, 15}// 49	50	51	
			, {1822, -1, -2, 0}// 52	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new GreyEastTallFirePlaceAddonDeed();
			}
		}

		[ Constructable ]
		public GreyEastTallFirePlaceAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 3633, 0, -1, 0, 0, 1, "", 1);// 1
			AddComplexComponent( (BaseAddon) this, 3633, 0, 1, 0, 0, 1, "", 1);// 13
			AddComplexComponent( (BaseAddon) this, 3633, 0, 0, 0, 0, 1, "", 1);// 14

		}

		public GreyEastTallFirePlaceAddon( Serial serial ) : base( serial )
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

	public class GreyEastTallFirePlaceAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new GreyEastTallFirePlaceAddon();
			}
		}

		[Constructable]
		public GreyEastTallFirePlaceAddonDeed()
		{
			Name = "GreyEastTallFirePlace";
		}

		public GreyEastTallFirePlaceAddonDeed( Serial serial ) : base( serial )
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
