using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBBowyer : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBBowyer()
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
                Add(new GenericBuyInfo(typeof(FletcherTools), 50, 3, 0x1022, 0));
                Add(new GenericBuyInfo(typeof(Bow), 150, 3, 0x13B2, 0));
                Add(new GenericBuyInfo(typeof(Crossbow), 200, 3, 0xF50, 0));

            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(FletcherTools), 1);
                Add(typeof(HeavyCrossbow), 7);
                Add(typeof(Bow), 5);
                Add(typeof(Crossbow), 6);
            }
        }
    }
}
