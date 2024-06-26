
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
	public class LongBuffetTableGreenAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {2754, 8, 0, 6}, {2809, 7, 0, 6}, {2756, 6, 0, 6}// 7	8	9	
			, {2757, 8, -1, 6}, {2807, 7, -1, 6}, {2755, 6, -1, 6}// 10	11	12	
			, {4609, 8, 1, 0}, {4611, 8, 0, 0}, {4610, 8, -1, 0}// 35	36	37	
			, {4609, 7, 1, 0}, {4611, 7, 0, 0}, {4610, 7, -1, 0}// 38	39	40	
			, {4609, 6, 1, 0}, {4611, 6, 0, 0}, {4610, 6, -1, 0}// 41	42	43	
			, {4609, 5, 1, 0}, {4611, 5, 0, 0}, {4610, 5, -1, 0}// 44	45	46	
			, {4609, 4, 1, 0}, {4611, 4, 0, 0}, {4610, 4, -1, 0}// 47	48	49	
			, {2754, 2, 0, 6}, {2809, 1, 0, 6}, {2756, 0, 0, 6}// 64	65	66	
			, {2757, 2, -1, 6}, {2807, 1, -1, 6}, {2755, 0, -1, 6}// 67	68	69	
			, {4609, -8, 1, 0}, {4611, -8, 0, 0}, {4610, -8, -1, 0}// 124	125	126	
			, {4609, -7, 1, 0}, {4611, -7, 0, 0}, {4610, -7, -1, 0}// 127	128	129	
			, {4609, 3, 1, 0}, {4611, 3, 0, 0}, {4610, 3, -1, 0}// 130	131	132	
			, {4609, 2, 1, 0}, {4611, 2, 0, 0}, {4610, 2, -1, 0}// 133	134	135	
			, {4609, 1, 1, 0}, {4611, 1, 0, 0}, {4610, 1, -1, 0}// 136	137	138	
			, {4609, 0, 1, 0}, {4611, 0, 0, 0}, {4610, 0, -1, 0}// 139	140	141	
			, {4609, -1, 1, 0}, {4611, -1, 0, 0}, {4610, -1, -1, 0}// 142	143	144	
			, {4609, -2, 1, 0}, {4611, -2, 0, 0}, {4610, -2, -1, 0}// 145	146	147	
			, {4609, -3, 1, 0}, {4611, -3, 0, 0}, {4610, -3, -1, 0}// 148	149	150	
			, {4609, -4, 1, 0}, {4611, -4, 0, 0}, {4610, -4, -1, 0}// 151	152	153	
			, {4609, -5, 1, 0}, {4611, -5, 0, 0}, {4610, -5, -1, 0}// 154	155	156	
			, {4609, -6, 1, 0}, {4611, -6, 0, 0}, {4610, -6, -1, 0}// 157	158	159	
					};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new LongBuffetTableGreenAddonDeed();
			}
		}

		[ Constructable ]
		public LongBuffetTableGreenAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 2754, 8, 1, 6, 175, -1, "", 1);// 1
			AddComplexComponent( (BaseAddon) this, 2809, 7, 1, 6, 175, -1, "", 1);// 2
			AddComplexComponent( (BaseAddon) this, 2756, 6, 1, 6, 175, -1, "", 1);// 3
			AddComplexComponent( (BaseAddon) this, 2757, 8, 0, 6, 175, -1, "", 1);// 4
			AddComplexComponent( (BaseAddon) this, 2807, 7, 0, 6, 175, -1, "", 1);// 5
			AddComplexComponent( (BaseAddon) this, 2755, 6, 0, 6, 175, -1, "", 1);// 6
			AddComplexComponent( (BaseAddon) this, 2754, 8, 0, 6, 175, -1, "", 1);// 13
			AddComplexComponent( (BaseAddon) this, 2809, 7, 0, 6, 175, -1, "", 1);// 14
			AddComplexComponent( (BaseAddon) this, 2756, 6, 0, 6, 175, -1, "", 1);// 15
			AddComplexComponent( (BaseAddon) this, 2757, 8, -1, 6, 175, -1, "", 1);// 16
			AddComplexComponent( (BaseAddon) this, 2807, 7, -1, 6, 175, -1, "", 1);// 17
			AddComplexComponent( (BaseAddon) this, 2755, 6, -1, 6, 175, -1, "", 1);// 18
			AddComplexComponent( (BaseAddon) this, 2754, 6, 0, 6, 175, -1, "", 1);// 19
			AddComplexComponent( (BaseAddon) this, 2809, 5, 0, 6, 175, -1, "", 1);// 20
			AddComplexComponent( (BaseAddon) this, 2756, 4, 0, 6, 175, -1, "", 1);// 21
			AddComplexComponent( (BaseAddon) this, 2757, 6, -1, 6, 175, -1, "", 1);// 22
			AddComplexComponent( (BaseAddon) this, 2807, 5, -1, 6, 175, -1, "", 1);// 23
			AddComplexComponent( (BaseAddon) this, 2755, 4, -1, 6, 175, -1, "", 1);// 24
			AddComplexComponent( (BaseAddon) this, 2754, 6, 1, 6, 175, -1, "", 1);// 25
			AddComplexComponent( (BaseAddon) this, 2809, 5, 1, 6, 175, -1, "", 1);// 26
			AddComplexComponent( (BaseAddon) this, 2756, 4, 1, 6, 175, -1, "", 1);// 27
			AddComplexComponent( (BaseAddon) this, 2757, 6, 0, 6, 175, -1, "", 1);// 28
			AddComplexComponent( (BaseAddon) this, 2807, 5, 0, 6, 175, -1, "", 1);// 29
			AddComplexComponent( (BaseAddon) this, 2755, 4, 0, 6, 175, -1, "", 1);// 30
			AddComplexComponent( (BaseAddon) this, 2754, 4, 0, 6, 175, -1, "", 1);// 31
			AddComplexComponent( (BaseAddon) this, 2757, 4, -1, 6, 175, -1, "", 1);// 32
			AddComplexComponent( (BaseAddon) this, 2754, 4, 1, 6, 175, -1, "", 1);// 33
			AddComplexComponent( (BaseAddon) this, 2757, 4, 0, 6, 175, -1, "", 1);// 34
			AddComplexComponent( (BaseAddon) this, 2809, 3, 0, 6, 175, -1, "", 1);// 50
			AddComplexComponent( (BaseAddon) this, 2756, 2, 0, 6, 175, -1, "", 1);// 51
			AddComplexComponent( (BaseAddon) this, 2807, 3, -1, 6, 175, -1, "", 1);// 52
			AddComplexComponent( (BaseAddon) this, 2755, 2, -1, 6, 175, -1, "", 1);// 53
			AddComplexComponent( (BaseAddon) this, 2809, 3, 1, 6, 175, -1, "", 1);// 54
			AddComplexComponent( (BaseAddon) this, 2756, 2, 1, 6, 175, -1, "", 1);// 55
			AddComplexComponent( (BaseAddon) this, 2807, 3, 0, 6, 175, -1, "", 1);// 56
			AddComplexComponent( (BaseAddon) this, 2755, 2, 0, 6, 175, -1, "", 1);// 57
			AddComplexComponent( (BaseAddon) this, 2754, 2, 1, 6, 175, -1, "", 1);// 58
			AddComplexComponent( (BaseAddon) this, 2809, 1, 1, 6, 175, -1, "", 1);// 59
			AddComplexComponent( (BaseAddon) this, 2756, 0, 1, 6, 175, -1, "", 1);// 60
			AddComplexComponent( (BaseAddon) this, 2757, 2, 0, 6, 175, -1, "", 1);// 61
			AddComplexComponent( (BaseAddon) this, 2807, 1, 0, 6, 175, -1, "", 1);// 62
			AddComplexComponent( (BaseAddon) this, 2755, 0, 0, 6, 175, -1, "", 1);// 63
			AddComplexComponent( (BaseAddon) this, 2754, 2, 0, 6, 175, -1, "", 1);// 70
			AddComplexComponent( (BaseAddon) this, 2809, 1, 0, 6, 175, -1, "", 1);// 71
			AddComplexComponent( (BaseAddon) this, 2756, 0, 0, 6, 175, -1, "", 1);// 72
			AddComplexComponent( (BaseAddon) this, 2757, 2, -1, 6, 175, -1, "", 1);// 73
			AddComplexComponent( (BaseAddon) this, 2807, 1, -1, 6, 175, -1, "", 1);// 74
			AddComplexComponent( (BaseAddon) this, 2755, 0, -1, 6, 175, -1, "", 1);// 75
			AddComplexComponent( (BaseAddon) this, 2754, 0, 1, 6, 175, -1, "", 1);// 76
			AddComplexComponent( (BaseAddon) this, 2809, -1, 1, 6, 175, -1, "", 1);// 77
			AddComplexComponent( (BaseAddon) this, 2756, -2, 1, 6, 175, -1, "", 1);// 78
			AddComplexComponent( (BaseAddon) this, 2757, 0, 0, 6, 175, -1, "", 1);// 79
			AddComplexComponent( (BaseAddon) this, 2807, -1, 0, 6, 175, -1, "", 1);// 80
			AddComplexComponent( (BaseAddon) this, 2755, -2, 0, 6, 175, -1, "", 1);// 81
			AddComplexComponent( (BaseAddon) this, 2754, -2, 1, 6, 175, -1, "", 1);// 82
			AddComplexComponent( (BaseAddon) this, 2809, -3, 1, 6, 175, -1, "", 1);// 83
			AddComplexComponent( (BaseAddon) this, 2756, -4, 1, 6, 175, -1, "", 1);// 84
			AddComplexComponent( (BaseAddon) this, 2757, -2, 0, 6, 175, -1, "", 1);// 85
			AddComplexComponent( (BaseAddon) this, 2807, -3, 0, 6, 175, -1, "", 1);// 86
			AddComplexComponent( (BaseAddon) this, 2755, -4, 0, 6, 175, -1, "", 1);// 87
			AddComplexComponent( (BaseAddon) this, 2754, -2, 0, 6, 175, -1, "", 1);// 88
			AddComplexComponent( (BaseAddon) this, 2809, -3, 0, 6, 175, -1, "", 1);// 89
			AddComplexComponent( (BaseAddon) this, 2756, -4, 0, 6, 175, -1, "", 1);// 90
			AddComplexComponent( (BaseAddon) this, 2757, -2, -1, 6, 175, -1, "", 1);// 91
			AddComplexComponent( (BaseAddon) this, 2807, -3, -1, 6, 175, -1, "", 1);// 92
			AddComplexComponent( (BaseAddon) this, 2755, -4, -1, 6, 175, -1, "", 1);// 93
			AddComplexComponent( (BaseAddon) this, 2754, -4, 1, 6, 175, -1, "", 1);// 94
			AddComplexComponent( (BaseAddon) this, 2809, -5, 1, 6, 175, -1, "", 1);// 95
			AddComplexComponent( (BaseAddon) this, 2756, -6, 1, 6, 175, -1, "", 1);// 96
			AddComplexComponent( (BaseAddon) this, 2757, -4, 0, 6, 175, -1, "", 1);// 97
			AddComplexComponent( (BaseAddon) this, 2807, -5, 0, 6, 175, -1, "", 1);// 98
			AddComplexComponent( (BaseAddon) this, 2755, -6, 0, 6, 175, -1, "", 1);// 99
			AddComplexComponent( (BaseAddon) this, 2754, -6, 0, 6, 175, -1, "", 1);// 100
			AddComplexComponent( (BaseAddon) this, 2809, -7, 0, 6, 175, -1, "", 1);// 101
			AddComplexComponent( (BaseAddon) this, 2756, -8, 0, 6, 175, -1, "", 1);// 102
			AddComplexComponent( (BaseAddon) this, 2757, -6, -1, 6, 175, -1, "", 1);// 103
			AddComplexComponent( (BaseAddon) this, 2807, -7, -1, 6, 175, -1, "", 1);// 104
			AddComplexComponent( (BaseAddon) this, 2755, -8, -1, 6, 175, -1, "", 1);// 105
			AddComplexComponent( (BaseAddon) this, 2754, -4, 0, 6, 175, -1, "", 1);// 106
			AddComplexComponent( (BaseAddon) this, 2809, -5, 0, 6, 175, -1, "", 1);// 107
			AddComplexComponent( (BaseAddon) this, 2756, -6, 0, 6, 175, -1, "", 1);// 108
			AddComplexComponent( (BaseAddon) this, 2757, -4, -1, 6, 175, -1, "", 1);// 109
			AddComplexComponent( (BaseAddon) this, 2807, -5, -1, 6, 175, -1, "", 1);// 110
			AddComplexComponent( (BaseAddon) this, 2755, -6, -1, 6, 175, -1, "", 1);// 111
			AddComplexComponent( (BaseAddon) this, 2754, 0, 0, 6, 175, -1, "", 1);// 112
			AddComplexComponent( (BaseAddon) this, 2809, -1, 0, 6, 175, -1, "", 1);// 113
			AddComplexComponent( (BaseAddon) this, 2756, -2, 0, 6, 175, -1, "", 1);// 114
			AddComplexComponent( (BaseAddon) this, 2757, 0, -1, 6, 175, -1, "", 1);// 115
			AddComplexComponent( (BaseAddon) this, 2807, -1, -1, 6, 175, -1, "", 1);// 116
			AddComplexComponent( (BaseAddon) this, 2755, -2, -1, 6, 175, -1, "", 1);// 117
			AddComplexComponent( (BaseAddon) this, 2754, -6, 1, 6, 175, -1, "", 1);// 118
			AddComplexComponent( (BaseAddon) this, 2809, -7, 1, 6, 175, -1, "", 1);// 119
			AddComplexComponent( (BaseAddon) this, 2756, -8, 1, 6, 175, -1, "", 1);// 120
			AddComplexComponent( (BaseAddon) this, 2757, -6, 0, 6, 175, -1, "", 1);// 121
			AddComplexComponent( (BaseAddon) this, 2807, -7, 0, 6, 175, -1, "", 1);// 122
			AddComplexComponent( (BaseAddon) this, 2755, -8, 0, 6, 175, -1, "", 1);// 123

		}

		public LongBuffetTableGreenAddon( Serial serial ) : base( serial )
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

	public class LongBuffetTableGreenAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new LongBuffetTableGreenAddon();
			}
		}

		[Constructable]
		public LongBuffetTableGreenAddonDeed()
		{
			Name = "LongBuffetTableGreen";
		}

		public LongBuffetTableGreenAddonDeed( Serial serial ) : base( serial )
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