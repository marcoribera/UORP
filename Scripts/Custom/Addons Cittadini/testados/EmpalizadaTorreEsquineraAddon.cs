
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
	public class EmpalizadaTorreEsquineraAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {546, -1, 3, 15}, {1218, 0, -1, 35}, {1848, 2, 3, 10}// 1	2	3	
			, {1848, 2, 1, 10}, {1848, 2, -1, 10}, {1218, -1, 2, 35}// 4	6	7	
			, {546, 3, 3, 15}, {546, -1, 3, 0}, {545, -2, 3, 0}// 8	9	10	
			, {546, -1, -2, 35}, {553, -2, 3, 43}, {9961, 2, 0, 58}// 11	12	13	
			, {1848, 3, 2, 0}, {1218, 3, 3, 35}, {1848, 3, 0, 5}// 14	15	16	
			, {1848, -1, 0, 5}, {546, 2, -2, 15}, {1848, -1, 2, 10}// 17	18	20	
			, {1848, 2, 3, 0}, {1848, 2, 2, 10}, {555, 0, 1, 65}// 21	22	23	
			, {1848, 1, 1, 10}, {1848, 0, 3, 10}, {547, 3, 3, 0}// 24	25	26	
			, {1218, 2, -1, 35}, {1218, -1, 1, 35}, {1218, 3, 3, 35}// 27	28	29	
			, {553, 3, -2, 43}, {1218, -1, -1, 35}, {545, -2, 3, 35}// 30	31	32	
			, {1848, 3, 2, 5}, {1848, 3, 3, 10}, {546, -1, -2, 15}// 33	34	35	
			, {546, 3, -2, 15}, {1848, 2, -1, 10}, {9961, 3, -1, 55}// 36	37	38	
			, {2212, 1, 1, 15}, {2212, 1, 0, 23}, {1848, 3, 0, 10}// 39	40	41	
			, {1848, 0, 3, 5}, {1848, 0, 1, 10}, {546, 0, -2, 15}// 42	43	44	
			, {1218, -1, 0, 35}, {1848, 0, -1, 0}, {545, 3, -1, 35}// 45	46	47	
			, {1848, 3, 0, 0}, {558, 3, 1, 13}, {1848, -1, 0, 0}// 48	49	50	
			, {1848, 0, 1, 10}, {555, 1, 1, 67}, {1848, 0, 2, 10}// 51	52	53	
			, {1250, 3, -1, 55}, {1848, 2, -1, 0}, {554, 1, 0, 65}// 54	55	56	
			, {545, 3, 3, 15}, {1218, 2, 1, 35}, {1848, 3, 2, 10}// 57	58	59	
			, {1218, 0, 2, 35}, {1848, -1, 0, 10}, {1218, 1, 2, 35}// 60	61	62	
			, {545, -2, 3, 15}, {554, 1, 1, 65}, {553, -2, -1, 35}// 63	64	65	
			, {1848, 2, -1, 5}, {1848, 0, 0, 10}, {1218, 0, 1, 35}// 66	67	68	
			, {1848, 0, 3, 10}, {553, 3, 3, 43}, {1848, -1, 2, 0}// 69	70	71	
			, {1848, 0, 3, 10}, {9960, 0, 2, 58}, {546, -1, -2, 0}// 72	73	74	
			, {1218, 0, 0, 35}, {1848, 3, 2, 10}, {546, 2, 3, 13}// 75	76	77	
			, {546, -3, -3, 0}, {1848, 1, 2, 10}, {546, 3, -2, 0}// 78	79	80	
			, {1218, 1, -1, 35}, {1848, 0, -1, 10}, {1848, 0, 3, 0}// 81	82	83	
			, {1848, 2, 3, 5}, {545, 3, 2, 15}, {9958, 2, 2, 58}// 84	85	86	
			, {1848, 0, 2, 10}, {546, 3, 3, 35}, {1848, 0, 0, 10}// 87	88	89	
			, {1848, 0, -1, 10}, {1848, 2, 3, 10}, {1848, 0, -1, 5}// 90	91	93	
			, {545, 3, -1, 15}, {1848, 0, -1, 10}, {545, -2, -1, 35}// 94	95	96	
			, {545, -2, -1, 15}, {1848, -1, 0, 10}, {557, 1, 3, 15}// 97	98	99	
			, {545, -2, -1, 0}, {545, 3, 3, 35}, {1218, -1, 3, 35}// 100	101	102	
			, {1218, 3, -1, 35}, {1218, 2, 2, 35}, {546, 3, -2, 35}// 103	104	105	
			, {1848, 2, 0, 10}, {1848, 1, 0, 10}, {1848, 3, -1, 10}// 106	107	108	
			, {545, 3, -1, 0}, {1848, -1, 3, 10}, {545, -2, 0, 15}// 109	110	111	
			, {1848, -1, 2, 5}, {546, -1, 3, 35}, {1218, 2, 0, 35}// 112	113	114	
			, {1848, -1, -1, 10}, {1848, -1, 2, 10}, {9953, 1, 1, 64}// 115	116	117	
			, {545, -2, 2, 15}, {9959, 0, 0, 58}, {9960, -1, 3, 55}// 118	119	120	
			, {9959, -1, -1, 55}, {558, -2, 1, 13}, {557, 1, -2, 13}// 121	122	123	
			, {9964, 0, -1, 55}, {9964, 1, -1, 55}, {9964, 2, -1, 55}// 125	126	127	
			, {9964, 2, -1, 55}, {9948, -1, 0, 55}, {9948, -1, 1, 55}// 128	129	130	
			, {9948, -1, 2, 55}, {9964, 1, 0, 58}, {9964, 1, 0, 58}// 131	132	133	
			, {9947, 3, 0, 55}, {9947, 3, 1, 55}, {9947, 3, 2, 55}// 134	135	136	
			, {9948, 0, 1, 58}, {9947, 2, 1, 58}, {9963, 1, 2, 58}// 137	138	139	
			, {9963, 1, 2, 58}, {9963, 0, 3, 55}, {9963, 1, 3, 55}// 140	141	142	
			, {9963, 2, 3, 55}, {9958, 3, 3, 55}, {546, 0, 3, 15}// 143	144	145	
			, {545, 3, 0, 15}, {1218, 3, -1, 35}, {1218, 3, 0, 35}// 146	147	148	
			, {1218, 3, 1, 35}, {1218, 3, 2, 35}, {1218, 3, 3, 35}// 149	150	151	
			, {1218, -1, 3, 35}, {1218, 0, 3, 35}, {1218, 1, 3, 35}// 152	153	154	
			, {1218, 2, 3, 35}, {1218, 3, 3, 35}, {541, 0, 3, 35}// 155	156	157	
			, {541, 1, 3, 35}, {541, 2, 3, 35}, {544, 4, 2, 35}// 158	159	160	
			, {544, 4, 1, 35}, {544, 4, 0, 35}, {554, 0, -2, 35}// 161	162	163	
			, {554, 1, -2, 35}, {554, 2, -2, 35}, {554, 0, -2, 38}// 164	165	166	
			, {554, 1, -2, 38}, {554, 2, -2, 38}, {555, -2, 0, 35}// 167	168	169	
			, {555, -2, 1, 35}, {555, -2, 2, 35}, {555, -2, 0, 39}// 170	171	172	
			, {555, -2, 1, 39}, {555, -2, 2, 39}, {1848, 1, 3, 0}// 173	174	175	
			, {1848, 1, 3, 5}, {1848, 3, 1, 0}, {1848, 3, 1, 5}// 176	177	178	
			, {1850, 3, 1, 10}, {1849, 1, 3, 10}, {1848, -1, 1, 0}// 179	180	181	
			, {1848, -1, 1, 5}, {1852, -1, 1, 10}, {1848, 1, -1, 0}// 182	183	184	
			, {1848, 1, -1, 5}, {1851, 1, -1, 10}, {542, 4, -1, 0}// 185	186	187	
			, {542, 4, 0, 0}, {542, 4, 2, 0}, {542, 4, 3, 0}// 188	189	190	
			, {543, -1, 4, 0}, {543, 0, 4, 0}, {543, 2, 4, 0}// 191	192	193	
			, {543, 3, 4, 0}// 194	
		};

 

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new EmpalizadaTorreEsquineraAddonDeed();
			}
		}

		[ Constructable ]
		public EmpalizadaTorreEsquineraAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 3633, 2, 2, 35, 0, 1, "", 1);// 5
			AddComplexComponent( (BaseAddon) this, 3633, -1, -1, 35, 0, 1, "", 1);// 19
			AddComplexComponent( (BaseAddon) this, 3633, 3, 3, 15, 0, 1, "", 1);// 92
			AddComplexComponent( (BaseAddon) this, 3633, -1, -1, 15, 0, 1, "", 1);// 124
			AddComplexComponent( (BaseAddon) this, 2572, -1, 4, 50, 0, 5, "", 1);// 195
			AddComplexComponent( (BaseAddon) this, 2572, 3, 4, 50, 0, 5, "", 1);// 196
			AddComplexComponent( (BaseAddon) this, 2567, 4, 3, 50, 0, 8, "", 1);// 197
			AddComplexComponent( (BaseAddon) this, 2567, 4, -1, 50, 0, 8, "", 1);// 198

		}

		public EmpalizadaTorreEsquineraAddon( Serial serial ) : base( serial )
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

	public class EmpalizadaTorreEsquineraAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new EmpalizadaTorreEsquineraAddon();
			}
		}

		[Constructable]
		public EmpalizadaTorreEsquineraAddonDeed()
		{
			Name = "EmpalizadaTorreEsquinera";
		}

		public EmpalizadaTorreEsquineraAddonDeed( Serial serial ) : base( serial )
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
