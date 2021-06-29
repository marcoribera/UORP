using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBNecromancer : SBInfo
	{
		private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
		public SBNecromancer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BlackPearl ), 7, 140, 0xF7A, 0 ) );
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 10, 140, 0xF7B, 0 ) );
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 7, 140, 0xF86, 0 ) );
				Add( new GenericBuyInfo( typeof( Garlic ), 7, 140, 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 7, 240, 0xF85, 0 ) );
				Add( new GenericBuyInfo( typeof( Nightshade ), 7, 240, 0xF88, 0 ) );
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 7, 140, 0xF8D, 0 ) );
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 7, 140, 0xF8C, 0 ) );

				if ( Core.AOS )
				{
					Add( new GenericBuyInfo( typeof( BatWing ), 7, 340, 0xF78, 0 ) );
					Add( new GenericBuyInfo( typeof( GraveDust ), 7, 340, 0xF8F, 0 ) );
					Add( new GenericBuyInfo( typeof( DaemonBlood ), 7, 340, 0xF7D, 0 ) );
					Add( new GenericBuyInfo( typeof( NoxCrystal ), 7, 340, 0xF8E, 0 ) );
					Add( new GenericBuyInfo( typeof( PigIron ), 7, 340, 0xF8A, 0 ) );

					Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 1000, 2, 0x2253, 0 ) );
				}
				
				//Add(new GenericBuyInfo("1041072", typeof(MagicWizardsHat), 11, 10, 0x1718, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(ScribesPen), 50, 3, 0xFBF, 0));
                Add(new GenericBuyInfo(typeof(BlankScroll), 15, 40, 0x0E34, 0));
				//Add( new GenericBuyInfo( typeof( RecallRune ), 25, 10, 0x1f14, 0 ) );
				Add( new GenericBuyInfo( typeof( Spellbook ), 1000, 3, 0xEFA, 0 ) );
			
				Type[] types = Loot.RegularScrollTypes;
			
				for (int i = 0; i < types.Length && i < 8; ++i)
                {
                    int itemID = 0x1F2E + i;

                    if (i == 6)
                        itemID = 0x1F2D;
                    else if (i > 6)
                        --itemID;

                    Add(new GenericBuyInfo(types[i], 12 + ((i / 8) * 10), 20, itemID, 0, true));
                }
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( WizardsHat ), 5 );
			//	Add( typeof( Runebook ), 1250 );
				Add( typeof( BlackPearl ), 2 ); 
				Add( typeof( Bloodmoss ),2 ); 
				Add( typeof( MandrakeRoot ), 2 ); 
				Add( typeof( Garlic ), 2 ); 
				Add( typeof( Ginseng ), 2 ); 
				Add( typeof( Nightshade ), 2 ); 
				Add( typeof( SpidersSilk ), 2 ); 
				Add( typeof( SulfurousAsh ), 2 );
			//	Add( typeof( RecallRune ), 13 );
				Add( typeof( Spellbook ), 25 );
				
				if ( Core.AOS )
				{
				Add( typeof( PigIron ), 2 );
				Add( typeof( DaemonBlood ), 3 );
				Add( typeof( NoxCrystal ), 3 );
				Add( typeof( BatWing ), 1 );
				Add( typeof( GraveDust ), 1 );
				}

				Type[] types = Loot.RegularScrollTypes;

				for (int i = 0; i < types.Length; ++i)
                    Add(types[i], ((i / 8) + 2) * 2);
			}
		}
	}
}
