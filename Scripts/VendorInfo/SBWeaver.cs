using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles 
{ 
    public class SBWeaver : SBInfo 
    { 
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBWeaver() 
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
                Add(new GenericBuyInfo(typeof(Dyes), 8, 20, 0xFA9, 0)); 
                Add(new GenericBuyInfo(typeof(DyeTub), 8, 20, 0xFAB, 0));

                Add(new GenericBuyInfo(typeof(UncutCloth), 3, 20, 0x1761, 0, true));
                Add(new GenericBuyInfo(typeof(UncutCloth), 3, 20, 0x1762, 0, true));
                Add(new GenericBuyInfo(typeof(UncutCloth), 3, 20, 0x1763, 0, true));
                Add(new GenericBuyInfo(typeof(UncutCloth), 3, 20, 0x1764, 0, true));

                Add(new GenericBuyInfo(typeof(BoltOfCloth), 100, 20, 0xf9B, 0, true));
                Add(new GenericBuyInfo(typeof(BoltOfCloth), 100, 20, 0xf9C, 0, true));
                Add(new GenericBuyInfo(typeof(BoltOfCloth), 100, 20, 0xf96, 0, true));
                Add(new GenericBuyInfo(typeof(BoltOfCloth), 100, 20, 0xf97, 0, true));

                Add(new GenericBuyInfo(typeof(DarkYarn), 18, 20, 0xE1D, 0, true));
                Add(new GenericBuyInfo(typeof(LightYarn), 18, 20, 0xE1E, 0, true));
                Add(new GenericBuyInfo(typeof(LightYarnUnraveled), 18, 20, 0xE1F, 0, true));

                Add(new GenericBuyInfo(typeof(Scissors), 11, 20, 0xF9F, 0));

                Add(new GenericBuyInfo("1154003", typeof(LeatherBraid), 50, 500, 5152, 2968));
            }
        }

        public class InternalSellInfo : GenericSellInfo 
        { 
            public InternalSellInfo() 
            { 
                Add(typeof(Scissors), 6); 
                Add(typeof(Dyes), 4); 
                Add(typeof(DyeTub), 4); 
                Add(typeof(UncutCloth), 1);
                Add(typeof(BoltOfCloth), 50); 
                Add(typeof(LightYarnUnraveled), 9);
                Add(typeof(LightYarn), 9);
                Add(typeof(DarkYarn), 9);
            }
        }
    }
}
