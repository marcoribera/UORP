
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
	public class LakeFishingAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {9967, 2, 8, 1}, {3473, -1, 8, 1}, {9037, -1, 9, 1}// 1	2	3	
			, {6039, 6, 2, 1}, {6046, 9, 1, 1}, {6054, 4, -3, 1}// 4	5	8	
			, {6057, -1, -4, 1}, {6054, 3, -2, 1}, {6039, 4, -1, 1}// 9	10	11	
			, {6060, 4, -2, 1}, {6054, 5, -4, 1}, {6868, 1, 0, 8}// 12	13	14	
			, {6039, 2, 5, 1}, {1872, 2, 3, 1}, {1872, 2, 2, 1}// 15	16	17	
			, {6053, 9, -1, 1}, {6053, 10, 2, 1}, {6055, 11, 5, 1}// 18	19	20	
			, {3517, 6, 3, 1}, {1872, 1, 1, 1}, {6046, 9, 0, 1}// 21	22	23	
			, {6046, 6, -3, 1}, {1875, 2, -1, 1}, {6046, 11, 4, 1}// 24	25	26	
			, {1872, 1, 5, 1}, {1872, 1, 4, 1}, {6039, 7, 2, 1}// 27	28	29	
			, {6039, 6, 1, 1}, {6039, 4, 0, 1}, {6057, 6, 0, 1}// 30	31	32	
			, {6039, 5, 3, 1}, {6039, 5, 4, 1}, {6039, 5, 0, 1}// 33	34	35	
			, {6039, 3, 3, 1}, {6046, 6, -2, 1}, {6039, 5, 1, 1}// 36	37	38	
			, {1875, 1, -1, 1}, {6053, 6, -4, 1}, {4965, 6, -2, 1}// 39	40	41	
			, {6058, 5, 5, 1}, {6024, 6, -2, 1}, {3473, 7, -1, 1}// 42	43	44	
			, {6057, 0, -2, 1}, {6049, 0, 6, 1}, {6049, 6, 5, 1}// 45	46	47	
			, {6049, 9, 6, 1}, {3346, 6, 6, 1}, {6060, 3, -1, 1}// 48	49	50	
			, {1872, 2, 4, 1}, {1872, 2, 5, 1}, {6053, 0, -4, 1}// 51	52	53	
			, {6054, 7, 0, 1}, {6050, 8, 6, 1}, {6046, 0, -3, 1}// 54	55	56	
			, {6055, 10, 6, 1}, {6057, 10, 3, 1}, {6060, 5, -3, 1}// 57	58	59	
			, {6053, 11, 3, 1}, {6054, 8, -1, 1}, {1872, 2, 0, 1}// 60	61	62	
			, {6039, 2, 4, 1}, {6053, -1, -5, 1}, {3253, 6, 5, 1}// 63	64	65	
			, {6057, 9, 2, 1}, {6039, 7, 3, 1}, {6039, 7, 4, 1}// 66	67	68	
			, {6039, 8, 1, 1}, {6056, 7, 6, 1}, {6059, 7, 5, 1}// 69	70	71	
			, {3522, 6, 3, 6}, {1872, 1, 2, 1}, {1872, 1, 0, 1}// 72	73	74	
			, {6058, 10, 5, 1}, {6039, 2, 3, 1}, {3342, -1, -5, 1}// 75	76	77	
			, {6039, 6, 3, 1}, {6039, 5, -1, 1}, {6055, 5, 6, 1}// 78	79	80	
			, {1872, 1, 3, 1}, {6045, 6, -1, 1}, {1872, 2, 1, 1}// 81	82	83	
			, {6039, 9, 3, 1}, {6039, 9, 4, 1}, {6039, 4, 5, 1}// 84	85	86	
			, {6039, 9, 5, 1}, {6039, 8, 5, 1}, {6039, 8, 4, 1}// 87	88	89	
			, {6039, 8, 3, 1}, {6039, 8, 2, 1}, {6039, 7, 1, 1}// 90	91	92	
			, {6039, 6, 4, 1}, {6039, 3, 2, 1}, {6039, 4, 2, 1}// 93	94	95	
			, {6039, 5, -2, 1}, {6039, -1, -3, 1}, {6039, -1, -2, 1}// 96	97	98	
			, {6039, -1, -1, 1}, {6039, -1, 0, 1}, {6039, -1, 1, 1}// 99	100	101	
			, {6039, -1, 2, 1}, {6039, 3, 1, 1}, {6039, 4, 1, 1}// 102	103	104	
			, {6039, 0, -1, 1}, {6039, 5, 2, 1}, {6039, 0, 0, 1}// 105	106	107	
			, {6039, 0, 1, 1}, {6039, 0, 2, 1}, {6039, 10, 4, 1}// 108	109	110	
			, {6039, -1, 5, 1}, {6039, 1, 0, 1}, {6039, 1, 1, 1}// 111	112	113	
			, {6039, 1, 2, 1}, {6053, 7, 0, 1}, {6039, 0, 4, 1}// 114	115	116	
			, {6039, 0, 5, 1}, {6039, -1, 3, 1}, {6039, -1, 4, 1}// 117	118	119	
			, {6039, 0, 3, 1}, {6039, 2, 0, 1}, {6039, 2, 1, 1}// 120	121	122	
			, {6039, 2, 2, 1}, {1873, 1, 6, 1}, {6049, -1, 6, 1}// 123	124	125	
			, {6039, 3, 4, 1}, {6039, 3, 0, 1}, {6039, 4, 3, 1}// 126	127	128	
			, {6039, 4, 4, 1}, {6050, 4, 6, 1}, {3237, 7, 5, 1}// 129	130	131	
			, {6060, 8, 0, 1}, {9327, 3, -1, 1}, {3256, 5, -2, 1}// 132	133	134	
			, {3250, 9, 2, 1}, {3250, 0, -2, 1}, {1873, 2, 6, 1}// 135	136	137	
			, {6059, 3, 5, 1}, {6056, 3, 6, 1}, {3301, -9, 9, 1}// 138	139	140	
			, {6056, -4, 8, 1}, {6055, -3, 8, 1}, {3942, -2, 9, 1}// 141	142	143	
			, {4033, -3, 9, 1}, {6057, -2, -5, 0}, {6039, -3, -5, 1}// 144	145	146	
			, {3715, -8, -6, 0}, {6039, -6, 2, 1}, {6049, -6, 5, 1}// 147	148	149	
			, {6054, -8, -5, 1}, {6054, -9, -2, 1}, {3545, -9, -4, 1}// 150	151	152	
			, {6056, -5, 6, 1}, {6054, -5, -7, 1}, {3304, -8, 6, 21}// 153	154	155	
			, {6049, -9, 2, 1}, {6039, -5, -4, 1}, {6052, -9, -1, 1}// 156	157	158	
			, {6048, -6, -6, 1}, {6059, -8, -3, 1}, {6039, -5, -3, 1}// 159	160	161	
			, {6039, -4, -6, 1}, {6060, -8, -2, 1}, {6059, -5, 5, 1}// 162	163	164	
			, {6060, -8, -4, 1}, {6056, -7, 5, 1}, {6059, -7, 4, 1}// 165	166	167	
			, {6054, -9, -4, 1}, {6056, -9, -3, 1}, {3516, -5, 2, 1}// 168	169	170	
			, {2918, -4, -8, 1}, {3482, -10, 3, 1}, {3482, -10, 4, 1}// 171	172	173	
			, {6054, -10, 1, 1}, {6039, -5, -5, 1}, {3520, -8, -5, 1}// 174	175	176	
			, {6039, -4, 5, 1}, {3303, -7, 7, 1}, {6056, -8, 4, 1}// 177	178	179	
			, {6052, -9, 0, 1}, {6060, -9, 1, 1}, {6059, -4, 6, 1}// 180	181	182	
			, {6048, -4, -7, 1}, {6039, -4, -5, 1}, {6051, -4, 7, 1}// 183	184	185	
			, {6060, -7, -5, 1}, {3304, -4, 3, 1}, {6039, -8, -1, 1}// 186	187	188	
			, {6059, -8, 2, 1}, {6039, -8, 1, 1}, {6060, -5, -6, 1}// 189	190	191	
			, {6039, -5, -1, 1}, {6039, -5, 0, 1}, {6039, -5, 1, 1}// 192	193	194	
			, {6039, -5, 2, 1}, {6039, -5, 3, 1}, {6039, -6, 3, 1}// 195	196	197	
			, {6039, -7, 3, 1}, {6039, -4, -4, 1}, {6039, -7, -4, 1}// 198	199	200	
			, {6039, -7, -3, 1}, {6039, -7, -2, 1}, {6039, -4, -3, 1}// 201	202	203	
			, {6039, -4, 3, 1}, {6039, -4, 4, 1}, {6039, -7, -1, 1}// 204	205	206	
			, {6039, -4, -1, 1}, {6039, -4, 0, 1}, {6039, -4, 1, 1}// 207	208	209	
			, {6039, -4, 2, 1}, {6039, -7, 0, 1}, {6039, -7, 1, 1}// 210	211	212	
			, {6039, -5, 4, 1}, {6039, -7, 2, 1}, {6039, -8, 0, 1}// 213	214	215	
			, {6039, -6, 4, 1}, {6039, -6, -4, 1}, {6039, -6, -3, 1}// 216	217	218	
			, {6039, -6, -2, 1}, {6039, -5, -2, 1}, {6054, -7, -6, 1}// 219	220	221	
			, {6039, -6, -1, 1}, {6039, -6, 0, 1}, {6039, -6, 1, 1}// 222	223	224	
			, {6052, -8, 3, 1}, {6039, -6, -5, 1}, {6056, -10, 2, 1}// 225	226	227	
			, {3347, -9, 4, 1}, {3256, -7, -3, 1}, {6039, -4, -2, 1}// 228	229	230	
			, {6058, -3, 7, 1}, {9035, -3, -7, 1}, {6055, -2, 7, 1}// 231	232	233	
			, {3310, -2, 7, 1}, {6058, -2, 6, 1}, {2917, -3, -8, 1}// 234	235	236	
			, {6053, -3, -7, 1}, {4030, -3, -8, 2}, {6039, -3, 2, 1}// 237	238	239	
			, {6039, -2, -4, 1}, {6039, -2, -3, 1}, {6039, -2, -2, 1}// 240	241	242	
			, {6039, -2, -1, 1}, {6039, -2, 0, 1}, {6039, -2, 1, 1}// 243	244	245	
			, {6039, -2, 2, 1}, {6039, -3, 6, 1}, {6039, -3, -4, 1}// 246	247	248	
			, {6039, -3, -3, 1}, {6039, -3, -2, 1}, {6039, -3, 5, 1}// 249	250	251	
			, {6039, -3, 4, 1}, {6039, -3, 3, 1}, {6039, -2, 5, 1}// 252	253	254	
			, {6039, -2, 4, 1}, {6039, -3, -1, 1}, {6039, -3, 0, 1}// 255	256	257	
			, {6039, -3, 1, 1}, {6039, -2, 3, 1}, {6053, -2, -6, 1}// 258	259	260	
			, {6057, -3, -6, 1}, {3247, -2, -5, 1}// 261	262	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new LakeFishingAddonDeed();
			}
		}

		[ Constructable ]
		public LakeFishingAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 8496, 2, 5, 7, 473, -1, "", 1);// 6
			AddComplexComponent( (BaseAddon) this, 8430, 1, 0, 12, 446, -1, "", 1);// 7

		}

		public LakeFishingAddon( Serial serial ) : base( serial )
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

	public class LakeFishingAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new LakeFishingAddon();
			}
		}

		[Constructable]
		public LakeFishingAddonDeed()
		{
			Name = "LakeFishing";
		}

		public LakeFishingAddonDeed( Serial serial ) : base( serial )
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