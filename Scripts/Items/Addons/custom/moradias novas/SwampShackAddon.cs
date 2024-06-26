
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
	public class SwampShackAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {8296, -1, 5, 1}, {8560, 4, -3, 26}, {8564, 0, 0, 32}// 4	6	7	
			, {1993, 4, -7, 0}, {8560, 3, -1, 29}, {1993, 4, -6, 0}// 8	9	10	
			, {191, 3, -1, 21}, {8560, 5, -6, 23}, {1474, 1, -7, 35}// 11	12	13	
			, {8560, 3, -2, 29}, {8564, 0, -6, 32}, {8560, 3, -6, 29}// 14	15	16	
			, {8560, 3, -3, 29}, {1993, 3, -6, 0}, {8560, 5, -5, 23}// 17	18	19	
			, {8560, 3, -8, 29}, {8560, 5, -7, 23}, {2928, 1, -4, 1}// 20	22	23	
			, {1993, 2, -1, 0}, {1993, 0, -6, 0}, {3247, 5, -4, 0}// 24	25	26	
			, {8560, 2, -5, 32}, {8560, 3, -7, 29}, {3129, 3, -8, 1}// 27	28	29	
			, {8564, 0, -8, 32}, {1993, -1, -3, 0}, {8564, -1, -8, 29}// 30	31	32	
			, {2930, 1, -5, 1}, {1993, 0, -3, 0}, {8564, -1, -2, 29}// 33	34	35	
			, {8564, -1, -1, 29}, {8564, -1, -4, 29}, {8564, -1, -3, 29}// 36	37	38	
			, {8564, -1, -6, 29}, {8564, -1, -5, 29}, {8564, -1, 0, 29}// 39	40	41	
			, {8564, -1, -7, 29}, {3244, 5, -6, 0}, {168, 3, -9, 1}// 42	45	46	
			, {168, 0, -9, 1}, {2597, 1, -4, 7}, {2467, 1, -4, 7}// 47	48	49	
			, {168, 1, -9, 1}, {1993, 2, -8, 0}, {1993, 0, -5, 0}// 50	51	52	
			, {1993, 1, -8, 0}, {183, -1, -1, 21}, {1474, 1, -4, 35}// 53	54	55	
			, {8560, 3, 0, 29}, {1993, 3, -3, 0}, {1993, 3, -8, 0}// 56	57	58	
			, {1718, 1, 0, 1}, {8560, 3, -5, 29}, {1993, 1, -2, 0}// 59	60	61	
			, {1474, 1, 0, 35}, {3249, -1, 0, 0}, {1993, 0, -4, 0}// 62	63	64	
			, {1474, 1, -5, 35}, {8566, -1, -6, 30}, {8564, 0, -1, 32}// 65	66	67	
			, {8662, 4, -4, 1}, {8560, 5, 0, 23}, {1993, 3, -4, 0}// 68	69	70	
			, {1474, 1, -1, 35}, {3893, -1, -7, 2}, {1993, 3, -5, 0}// 71	72	73	
			, {2585, 1, -5, 7}, {183, 2, -1, 21}, {1474, 1, -2, 35}// 74	75	76	
			, {1993, 1, -7, 0}, {2473, 2, -8, 1}, {1993, 1, -6, 0}// 77	79	80	
			, {1993, 4, -3, 0}, {7050, -1, -8, 1}, {3091, 1, -8, 1}// 81	82	84	
			, {8560, 2, -7, 32}, {1993, 0, -2, 0}, {1993, -1, -6, 0}// 85	86	88	
			, {1993, 3, -2, 0}, {1993, 4, -4, 0}, {1993, 4, -5, 0}// 89	93	94	
			, {1993, -1, -7, 0}, {8560, 2, 0, 32}, {1993, 1, -5, 0}// 95	96	98	
			, {1993, 2, -7, 0}, {1993, -1, -8, 0}, {1993, 0, -8, 0}// 99	100	101	
			, {183, 1, -1, 21}, {3250, 2, 0, 0}, {8564, 0, -2, 32}// 102	103	104	
			, {8560, 5, -4, 23}, {167, 4, -5, 1}, {1993, 1, -3, 0}// 105	106	107	
			, {8562, 3, -3, 30}, {1993, 1, -4, 0}, {1474, 1, -3, 35}// 108	109	110	
			, {8560, 4, -6, 26}, {183, 0, -1, 21}, {8560, 4, 0, 26}// 111	112	113	
			, {8560, 4, -7, 26}, {3248, 4, 0, 0}, {1993, 1, -1, 0}// 114	115	116	
			, {8564, 0, -4, 32}, {8560, 4, -5, 26}, {7052, -1, -7, 1}// 117	119	120	
			, {8564, 0, -3, 32}, {8560, 4, -4, 26}, {1993, 2, -2, 0}// 122	123	124	
			, {8560, 5, -1, 23}, {3711, 2, -8, 1}, {8560, 4, -1, 26}// 125	126	127	
			, {3320, 7, 3, 1}, {1993, 2, -3, 0}, {3252, 5, -5, 0}// 128	129	130	
			, {1993, 2, -4, 0}, {1474, 1, -6, 35}, {166, 4, -1, 1}// 131	132	133	
			, {167, 4, -7, 1}, {1725, -1, -1, 1}, {167, 4, -2, 1}// 134	135	136	
			, {1993, 2, -5, 0}, {1993, 3, -7, 0}, {8560, 5, -2, 23}// 137	138	139	
			, {2929, 1, -4, 1}, {1993, -1, -5, 0}, {8564, 0, -5, 32}// 140	141	142	
			, {3245, 3, 0, 0}, {1993, -1, -2, 0}, {1474, 1, -8, 35}// 143	144	145	
			, {1993, -1, -1, 0}, {8560, 2, -3, 32}, {1993, 2, -6, 0}// 146	147	148	
			, {8560, 5, -3, 23}, {1993, -1, -4, 0}, {1993, 4, -1, 0}// 149	150	151	
			, {1993, 3, -1, 0}, {1993, 4, -2, 0}, {8560, 2, -2, 32}// 152	153	154	
			, {1993, 0, -7, 0}, {8560, 2, -6, 32}, {3089, 1, -3, 1}// 155	156	157	
			, {8560, 4, -2, 26}, {1993, 4, -8, 0}, {168, 4, -9, 1}// 158	159	160	
			, {3088, 2, -4, 1}, {8560, 2, -4, 32}, {8560, 5, -8, 23}// 161	162	163	
			, {8560, 2, -1, 32}, {3251, 5, -8, 0}, {167, 4, -8, 1}// 164	165	166	
			, {8564, 0, -7, 32}, {8560, 4, -8, 26}, {7051, -1, -7, 1}// 167	168	169	
			, {8560, 2, -8, 32}, {1993, 0, -1, 0}, {3673, -5, 8, 1}// 170	171	179	
			, {2524, -3, -4, 7}, {8564, -3, -8, 23}, {7046, -3, -7, 1}// 180	181	182	
			, {8662, -4, -5, 1}, {1993, -3, -3, 0}, {3897, -2, 0, 0}// 183	184	185	
			, {8662, -4, -4, 1}, {7047, -2, -7, 1}, {1993, -2, -6, 0}// 186	187	188	
			, {1993, -2, -7, 0}, {1993, -3, -2, 0}, {167, -4, -2, 1}// 189	190	191	
			, {1993, -2, -2, 0}, {1993, -2, -5, 0}, {3718, -2, 0, 0}// 192	193	194	
			, {1993, -3, -6, 0}, {1993, -2, -3, 0}, {168, -2, -9, 1}// 195	196	197	
			, {168, -3, -9, 1}, {8564, -2, -2, 26}, {8564, -2, -1, 26}// 198	199	200	
			, {8564, -3, -7, 23}, {8564, -3, 0, 23}, {8564, -3, -3, 23}// 201	202	203	
			, {8564, -3, -2, 23}, {8564, -3, -1, 23}, {8564, -2, 0, 26}// 204	205	206	
			, {8564, -2, -7, 26}, {8564, -2, -5, 26}, {8564, -2, -4, 26}// 207	208	209	
			, {8564, -2, -3, 26}, {8564, -3, -5, 23}, {8564, -3, -6, 23}// 210	211	212	
			, {8564, -3, -4, 23}, {167, -4, -1, 1}, {167, -4, -8, 1}// 213	214	215	
			, {167, -4, -7, 1}, {169, -4, -9, 0}, {7046, -3, -8, 1}// 216	217	218	
			, {7045, -2, -8, 1}, {1993, -3, -5, 0}, {7045, -3, -6, 1}// 219	220	221	
			, {2880, -3, -3, 1}, {2519, -3, -3, 7}, {7044, -2, -6, 1}// 222	223	224	
			, {7042, -3, -7, 1}, {1993, -2, -8, 0}, {1993, -3, -8, 0}// 225	226	227	
			, {2880, -3, -4, 1}, {8564, -2, -8, 26}, {3246, -3, 0, 0}// 230	231	232	
			, {1993, -3, -1, 0}, {1993, -2, -4, 0}, {1993, -3, -4, 0}// 233	235	236	
			, {3892, -2, -7, 2}, {3898, -2, 1, 0}, {3893, -2, -6, 1}// 237	238	239	
			, {7048, -2, -6, 1}, {7043, -2, -7, 1}, {3717, -3, 0, 0}// 240	241	242	
			, {1993, -3, -7, 0}, {2597, -2, 0, 0}, {168, -3, -1, 1}// 243	244	245	
			, {191, -2, -1, 21}, {1993, -2, -1, 0}, {2519, -3, -3, 8}// 246	247	248	
					};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new SwampShackAddonDeed();
			}
		}

		[ Constructable ]
		public SwampShackAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 4012, -1, 5, 1, 0, 2, "Cauldron fire", 1);// 1
			AddComplexComponent( (BaseAddon) this, 2416, -1, 5, 9, 0, -1, "Gurgling Brew", 1);// 2
			AddComplexComponent( (BaseAddon) this, 13901, -1, 9, 1, 0, -1, "Welcome to the Black Bayou", 1);// 3
			AddComplexComponent( (BaseAddon) this, 7977, 7, 3, 1, 0, -1, "Like sausage on a smokehouse wall", 1);// 5
			AddComplexComponent( (BaseAddon) this, 185, 4, -6, 1, 0, 1, "", 1);// 21
			AddComplexComponent( (BaseAddon) this, 186, -1, -9, 1, 0, 1, "", 1);// 43
			AddComplexComponent( (BaseAddon) this, 186, 2, -9, 1, 0, 1, "", 1);// 44
			AddComplexComponent( (BaseAddon) this, 2965, 1, 0, 16, 0, -1, "Hatties Shack", 1);// 78
			AddComplexComponent( (BaseAddon) this, 185, 4, -3, 1, 0, 1, "", 1);// 83
			AddComplexComponent( (BaseAddon) this, 8444, 5, 3, 1, 177, -1, "", 1);// 87
			AddComplexComponent( (BaseAddon) this, 8444, 6, 3, 6, 567, -1, "", 1);// 90
			AddComplexComponent( (BaseAddon) this, 8444, 7, 2, 5, 262, -1, "", 1);// 91
			AddComplexComponent( (BaseAddon) this, 8444, 7, 1, 0, 82, -1, "", 1);// 92
			AddComplexComponent( (BaseAddon) this, 186, -1, -1, 1, 0, 1, "", 1);// 97
			AddComplexComponent( (BaseAddon) this, 186, 3, -1, 1, 0, 1, "", 1);// 118
			AddComplexComponent( (BaseAddon) this, 186, 2, -1, 1, 0, 1, "", 1);// 121
			AddComplexComponent( (BaseAddon) this, 7977, -6, 5, 1, 0, -1, "Like sausage on a smokehouse wall", 1);// 172
			AddComplexComponent( (BaseAddon) this, 8809, -5, 8, 8, 1150, -1, "Don't come lookin' again!", 1);// 173
			AddComplexComponent( (BaseAddon) this, 8444, -7, 6, 10, 77, -1, "", 1);// 174
			AddComplexComponent( (BaseAddon) this, 8444, -6, 6, 17, 671, -1, "", 1);// 175
			AddComplexComponent( (BaseAddon) this, 8444, -6, 4, 4, 72, -1, "", 1);// 176
			AddComplexComponent( (BaseAddon) this, 8444, -5, 4, 9, 82, -1, "", 1);// 177
			AddComplexComponent( (BaseAddon) this, 3320, -6, 5, 1, 456, -1, "", 1);// 178
			AddComplexComponent( (BaseAddon) this, 185, -4, -6, 1, 0, 1, "", 1);// 228
			AddComplexComponent( (BaseAddon) this, 185, -4, -3, 1, 0, 1, "", 1);// 229
			AddComplexComponent( (BaseAddon) this, 186, -2, -1, 1, 0, 1, "", 1);// 234

		}

		public SwampShackAddon( Serial serial ) : base( serial )
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

	public class SwampShackAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SwampShackAddon();
			}
		}

		[Constructable]
		public SwampShackAddonDeed()
		{
			Name = "SwampShack";
		}

		public SwampShackAddonDeed( Serial serial ) : base( serial )
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