using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBScribe : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo;
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBScribe(Mobile m)
        {
            if (m != null)
            {
                m_BuyInfo = new InternalBuyInfo(m);
            }
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
            public InternalBuyInfo(Mobile m)
            {
                Add(new GenericBuyInfo(typeof(ScribesPen), 50, 3, 0xFBF, 0));
                Add(new GenericBuyInfo(typeof(BlankScroll), 15, 60, 0x0E34, 0));
                Add(new GenericBuyInfo(typeof(BrownBook), 15, 10, 0xFEF, 0));
                Add(new GenericBuyInfo(typeof(TanBook), 15, 10, 0xFF0, 0));
                Add(new GenericBuyInfo(typeof(BlueBook), 15, 10, 0xFF2, 0));
                Add(new GenericBuyInfo(typeof(BookOfNinjitsu), 1000, 10, 0x23A0, 0));
                Add(new GenericBuyInfo(typeof(BookOfBushido), 1000, 10, 0x238C, 0));
                Add(new GenericBuyInfo(typeof(MysticBook), 1000, 10, 0x2D9D, 0, true));
                Add(new GenericBuyInfo(typeof(NecromancerSpellbook), 1000, 2, 0x2253, 0));
                Add(new GenericBuyInfo(typeof(Spellbook), 1000, 2, 0xEFA, 0));
                Add(new GenericBuyInfo(typeof(SpellweavingBook), 1000, 2, 0x2D50, 0));

                if (m.Map == Map.Tokuno || m.Map == Map.TerMur)
                {
                    Add(new GenericBuyInfo(typeof(BookOfNinjitsu), 1000, 10, 0x23A0, 0));
                    Add(new GenericBuyInfo(typeof(BookOfBushido), 1000, 10, 0x238C, 0));
                }
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(ScribesPen), 4);
                Add(typeof(BrownBook), 1);
                Add(typeof(TanBook), 1);
                Add(typeof(BlueBook), 1);
                Add(typeof(BlankScroll), 3);
            }
        }
    }
}
