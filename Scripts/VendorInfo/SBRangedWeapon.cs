using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBRangedWeapon : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBRangedWeapon()
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
                Add(new GenericBuyInfo(typeof(Crossbow), 250, 3, 0xF50, 0));
             //   Add(new GenericBuyInfo(typeof(HeavyCrossbow), 55, 20, 0x13FD, 0));
                if (Core.AOS)
                {
           //         Add(new GenericBuyInfo(typeof(RepeatingCrossbow), 46, 20, 0x26C3, 0));
            //        Add(new GenericBuyInfo(typeof(CompositeBow), 45, 20, 0x26C2, 0));
                }
                Add(new GenericBuyInfo(typeof(Bolt), 10, 500, 0x1BFB, 0, true));
                Add(new GenericBuyInfo(typeof(Bow), 250, 3, 0x13B2, 0));
                Add(new GenericBuyInfo(typeof(Arrow), 10, 500, 0xF3F, 0, true));
                Add(new GenericBuyInfo(typeof(Feather), 4, 30, 0x1BD1, 0, true));
                Add(new GenericBuyInfo(typeof(Shaft), 3, 30, 0x1BD4, 0, true));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(Bolt), 1);
                Add(typeof(Arrow), 1);
                Add(typeof(Shaft), 1);
                Add(typeof(Feather), 1);			

                Add(typeof(HeavyCrossbow), 10);
                Add(typeof(Bow), 5);
                Add(typeof(Crossbow), 5); 

                if (Core.AOS)
                {
                    Add(typeof(CompositeBow), 12);
                    Add(typeof(RepeatingCrossbow), 12);
                }
            }
        }
    }
}
