
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
	public class GreyEastFirePlaceThinAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {1997, 0, 2, 15}, {1997, 0, 1, 15}, {1997, 0, 0, 15}// 1	2	3	
			, {1997, 0, -1, 15}, {1997, 0, -2, 15}, {1822, 0, 2, 10}// 4	5	6	
			, {1822, 0, 2, 5}, {1822, 0, -2, 10}, {1822, 0, -2, 5}// 7	8	9	
			, {1822, 0, 1, 10}, {1822, 0, 0, 10}, {1822, 0, -1, 10}// 10	11	12	
			, {1822, 0, 2, 0}, {1822, 0, -2, 0}, {1305, 0, 2, 0}// 13	14	18	
			, {1305, 0, 1, 0}, {1305, 0, 0, 0}, {1305, 0, -1, 0}// 19	20	21	
			, {1305, 0, -2, 0}, {1305, 1, 2, 0}, {1305, 1, 1, 0}// 22	23	24	
			, {1305, 1, 0, 0}, {1305, 1, -1, 0}, {1305, 1, -2, 0}// 25	26	27	
					};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new GreyEastFirePlaceThinAddonDeed();
			}
		}

		[ Constructable ]
		public GreyEastFirePlaceThinAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 3633, 0, 1, 0, 0, 1, "", 1);// 15
			AddComplexComponent( (BaseAddon) this, 3633, 0, 0, 0, 0, 1, "", 1);// 16
			AddComplexComponent( (BaseAddon) this, 3633, 0, -1, 0, 0, 1, "", 1);// 17

		}

		public GreyEastFirePlaceThinAddon( Serial serial ) : base( serial )
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

	public class GreyEastFirePlaceThinAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new GreyEastFirePlaceThinAddon();
			}
		}

		[Constructable]
		public GreyEastFirePlaceThinAddonDeed()
		{
			Name = "GreyEastFirePlaceThin";
		}

		public GreyEastFirePlaceThinAddonDeed( Serial serial ) : base( serial )
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