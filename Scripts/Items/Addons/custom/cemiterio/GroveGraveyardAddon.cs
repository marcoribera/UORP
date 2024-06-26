
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
	public class GroveGraveYardAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {3203, -1, -1, 0}, {3203, -2, 1, 0}, {3809, -4, -3, 0}// 1	2	3	
			, {2081, -6, 4, 0}, {2081, -6, 3, 0}, {2081, -6, 6, 0}// 4	5	6	
			, {2081, -6, 5, 0}, {2081, -6, -3, 0}, {2081, -6, -4, 0}// 7	8	9	
			, {2083, -2, -5, 0}, {2081, -6, 0, 0}, {2081, -6, 2, 0}// 10	11	12	
			, {3203, -4, 1, 0}, {2083, -5, 6, 0}, {2081, -6, -2, 0}// 13	14	15	
			, {3795, -2, -2, 0}, {2081, -6, -1, 0}, {1301, -1, 2, 0}// 16	17	18	
			, {3808, -3, 4, 0}, {3809, -2, -3, 0}, {2083, -5, -5, 0}// 19	20	21	
			, {1301, -2, 0, 0}, {2081, -6, 1, 0}, {2083, -4, -5, 0}// 22	23	24	
			, {3203, -1, 5, 0}, {3810, -4, 4, 0}, {1301, -1, 4, 0}// 25	26	27	
			, {1301, -3, 0, 0}, {1301, -2, 0, 0}, {3203, -4, -1, 0}// 28	29	30	
			, {3203, -5, -1, 0}, {3203, -3, -1, 0}, {1301, -1, 3, 0}// 31	32	33	
			, {3203, -2, 5, 0}, {3203, -3, 1, 0}, {3810, -4, 2, 0}// 34	35	36	
			, {3203, -5, 0, 0}, {1301, -4, 0, 0}, {2083, -4, 6, 0}// 37	38	39	
			, {1301, -1, 1, 0}, {2083, -1, -5, 0}, {3203, -2, 2, 0}// 40	41	43	
			, {2083, -3, -5, 0}, {2083, -1, 6, 0}, {3203, -2, -1, 0}// 44	46	47	
			, {3808, -3, 2, 0}, {2083, -3, 6, 0}, {2083, -2, 6, 0}// 48	49	50	
			, {3795, -4, -2, 0}, {3203, -2, 4, 0}, {3203, -2, 3, 0}// 52	53	54	
			, {1301, -1, 0, 0}, {3203, -5, 1, 0}, {3795, 0, -2, 0}// 55	57	58	
			, {2082, 6, 6, 0}, {1301, 2, 2, 0}, {2083, 5, -5, 0}// 59	60	61	
			, {1301, 0, 1, 0}, {2081, 6, -4, 0}, {1301, 0, 3, 0}// 62	63	64	
			, {3795, 2, -2, 0}, {1301, 0, 2, 0}, {3203, 4, 4, 0}// 65	66	67	
			, {3809, 0, -3, 0}, {1301, 2, 3, 0}, {2083, 1, -5, 0}// 68	69	70	
			, {3203, 1, -1, 0}, {2083, 1, 6, 0}, {3203, 2, -1, 0}// 71	73	74	
			, {3203, 1, 5, 0}, {1301, 3, 5, 0}, {1301, 3, 0, 0}// 75	76	77	
			, {3809, 4, -3, 0}, {3203, 1, 4, 0}, {3203, 4, 2, 0}// 78	79	80	
			, {2083, 0, 6, 0}, {3203, 4, 3, 0}, {2083, 5, 6, 0}// 81	82	83	
			, {1301, 3, 4, 0}, {2083, 3, -5, 0}, {1301, 3, 1, 0}// 84	85	86	
			, {3203, 1, 2, 0}, {1301, 2, 4, 0}, {1301, 3, 2, 0}// 87	88	89	
			, {2083, 4, -5, 0}, {3203, 3, -1, 0}, {1301, 0, 0, 0}// 90	91	92	
			, {1301, 2, 1, 0}, {3203, 4, 0, 0}, {2083, 4, 6, 0}// 93	94	95	
			, {3203, 1, 3, 0}, {3203, 4, 1, 0}, {1301, 2, 6, 0}// 96	97	98	
			, {1301, 3, 6, 0}, {1301, 1, 1, 0}, {3795, 4, -2, 0}// 99	100	101	
			, {3203, 4, -1, 0}, {3809, 2, -3, 0}, {3203, 1, 6, 0}// 102	103	104	
			, {1301, 3, 3, 0}, {2083, 0, -5, 0}, {1301, 1, 0, 0}// 107	108	109	
			, {3203, 0, -1, 0}, {1301, 2, 5, 0}, {3203, 4, 6, 0}// 110	111	112	
			, {3203, 4, 5, 0}, {2083, 6, -5, 0}, {1301, 2, 0, 0}// 113	114	115	
			, {1301, 0, 4, 0}, {2083, 2, -5, 0}, {3203, 0, 5, 0}// 116	117	118	
			, {3203, 4, 3, 0}, {2081, 6, -3, 0}, {2081, 6, -2, 0}// 119	120	121	
			, {2081, 6, -1, 0}, {2081, 6, 0, 0}, {2081, 6, 1, 0}// 122	123	124	
			, {2081, 6, 2, 0}, {2081, 6, 3, 0}, {2081, 6, 4, 0}// 125	126	127	
			, {2081, 6, 5, 0}// 128	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new GroveGraveYardAddonDeed();
			}
		}

		[ Constructable ]
		public GroveGraveYardAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 3800, -4, -4, 0, 0, -1, "Aqui Jaz Gaius", 1);// 42
			AddComplexComponent( (BaseAddon) this, 3799, -5, 4, 0, 0, -1, "Aqui Jaz Julia", 1);// 45
			AddComplexComponent( (BaseAddon) this, 3799, -5, 2, 0, 0, -1, "Aqui Jaz Cicero", 1);// 51
			AddComplexComponent( (BaseAddon) this, 3800, -2, -4, 0, 0, -1, "Aqui Jaz Luca, o Belo", 1);// 56
			AddComplexComponent( (BaseAddon) this, 3800, 0, -4, 0, 0, -1, "Aqui Jaz Hersilia", 1);// 72
			AddComplexComponent( (BaseAddon) this, 3800, 2, -4, 0, 0, -1, "Aqui Jaz Livius", 1);// 105
			AddComplexComponent( (BaseAddon) this, 3800, 4, -4, 0, 0, -1, "Aqui Jaz Agrippina", 1);// 106

		}

		public GroveGraveYardAddon( Serial serial ) : base( serial )
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

	public class GroveGraveYardAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new GroveGraveYardAddon();
			}
		}

		[Constructable]
		public GroveGraveYardAddonDeed()
		{
			Name = "GroveGraveYard";
		}

		public GroveGraveYardAddonDeed( Serial serial ) : base( serial )
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
