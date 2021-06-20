using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles 
{ 
    public class SBCobbler : SBInfo 
    { 
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBCobbler() 
        { 
        }

        public override IShopSellInfo SellInfo
        {
            get
            {
                return m_SellInfo;
            }
        }
        public override List<GenericBuyInfo> BuyInfo
        {
            get
            {
                return m_BuyInfo;
            }
        }

        public class InternalBuyInfo : List<GenericBuyInfo> 
        { 
            public InternalBuyInfo() 
            { 
                Add(new GenericBuyInfo(typeof(ThighBoots), 100, 3, 0x1711, Utility.RandomNeutralHue())); 
                Add(new GenericBuyInfo(typeof(Shoes), 70, 3, 0x170f, Utility.RandomNeutralHue())); 
                Add(new GenericBuyInfo(typeof(Boots), 90, 3, 0x170b, Utility.RandomNeutralHue()));
                Add(new GenericBuyInfo(typeof(Sandals), 50, 3, 0x170d, Utility.RandomNeutralHue())); 
            }
        }

        public class InternalSellInfo : GenericSellInfo 
        { 
            public InternalSellInfo() 
            { 
                Add(typeof(Shoes), 2); 
                Add(typeof(Boots), 2); 
                Add(typeof(ThighBoots), 4); 
                Add(typeof(Sandals), 2); 
            }
        }
    }
}
