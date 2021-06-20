using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBTailor : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBTailor()
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
                Add(new GenericBuyInfo(typeof(SewingKit), 50, 3, 0xF9D, 0)); 
                Add(new GenericBuyInfo(typeof(Scissors), 50, 3, 0xF9F, 0));
            //    Add(new GenericBuyInfo(typeof(DyeTub), 8, 20, 0xFAB, 0)); 
            //    Add(new GenericBuyInfo(typeof(Dyes), 8, 20, 0xFA9, 0)); 

                Add(new GenericBuyInfo(typeof(Shirt), 120, 20, 0x1517, 0));
                Add(new GenericBuyInfo(typeof(ShortPants), 70, 5, 0x152E, 0));
                Add(new GenericBuyInfo(typeof(FancyShirt), 210, 5, 0x1EFD, 0));
                Add(new GenericBuyInfo(typeof(LongPants), 100, 5, 0x1539, 0));
                Add(new GenericBuyInfo(typeof(FancyDress), 260, 5, 0x1EFF, 0));
                Add(new GenericBuyInfo(typeof(PlainDress), 130, 5, 0x1F01, 0));
                Add(new GenericBuyInfo(typeof(Kilt), 110, 5, 0x1537, 0));
                Add(new GenericBuyInfo(typeof(Kilt), 110, 5, 0x1537, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(HalfApron), 100, 5, 0x153b, 0));
                Add(new GenericBuyInfo(typeof(Robe), 180, 5, 0x1F03, 0));
                Add(new GenericBuyInfo(typeof(Cloak), 80, 5, 0x1515, 0));
                Add(new GenericBuyInfo(typeof(Cloak), 80, 5, 0x1515, 0));
                Add(new GenericBuyInfo(typeof(Doublet), 130, 5, 0x1F7B, 0));
                Add(new GenericBuyInfo(typeof(Tunic), 180, 5, 0x1FA1, 0));
                Add(new GenericBuyInfo(typeof(JesterSuit), 260, 5, 0x1F9F, 0));

                Add(new GenericBuyInfo(typeof(JesterHat), 120, 5, 0x171C, 0));
                Add(new GenericBuyInfo(typeof(FloppyHat), 70, 5, 0x1713, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(WideBrimHat), 80, 5, 0x1714, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(Cap), 100, 5, 0x1715, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(TallStrawHat), 80, 5, 0x1716, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(StrawHat), 70, 5, 0x1717, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(WizardsHat), 110, 5, 0x1718, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(LeatherCap), 100, 5, 0x1DB9, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(FeatheredHat), 100, 5, 0x171A, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(TricorneHat), 80, 5, 0x171B, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(Bandana), 60, 5, 0x1540, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(SkullCap), 70, 5, 0x1544, Utility.RandomDyedHue()));

                Add(new GenericBuyInfo(typeof(BoltOfCloth), 1000, 5, 0xf95, Utility.RandomDyedHue(), true));

                Add(new GenericBuyInfo(typeof(Cloth), 20, 5, 0x1766, Utility.RandomDyedHue(), true));
                Add(new GenericBuyInfo(typeof(UncutCloth), 20, 5, 0x1767, Utility.RandomDyedHue(), true));

                Add(new GenericBuyInfo(typeof(Cotton), 1020, 5, 0xDF9, 0, true));
                Add(new GenericBuyInfo(typeof(Wool), 620, 5, 0xDF8, 0, true));
                Add(new GenericBuyInfo(typeof(Flax), 1020, 5, 0x1A9C, 0, true));
                Add(new GenericBuyInfo(typeof(SpoolOfThread), 180, 5, 0xFA0, 0, true));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(Scissors), 4);
                Add(typeof(SewingKit), 1);
                Add(typeof(Dyes), 4);
                Add(typeof(DyeTub), 4);

                Add(typeof(BoltOfCloth), 10);
                Add(typeof(Cloth), 1);
                Add(typeof(UncutCloth), 1);

                Add(typeof(FancyShirt), 5);
                Add(typeof(Shirt), 6);

                Add(typeof(ShortPants), 3);
                Add(typeof(LongPants), 5);

                Add(typeof(Cloak), 4);
                Add(typeof(FancyDress), 8);
                Add(typeof(Robe), 5);
                Add(typeof(PlainDress), 5);

                Add(typeof(Skirt), 5);
                Add(typeof(Kilt), 5);

                Add(typeof(Doublet), 7);
                Add(typeof(Tunic), 9);
                Add(typeof(JesterSuit), 9);

                Add(typeof(FullApron), 5);
                Add(typeof(HalfApron), 5);

                Add(typeof(JesterHat), 6);
                Add(typeof(FloppyHat), 3);
                Add(typeof(WideBrimHat), 4);
                Add(typeof(Cap), 5);
                Add(typeof(SkullCap), 3);
                Add(typeof(Bandana), 3);
                Add(typeof(TallStrawHat), 4);
                Add(typeof(StrawHat), 4);
                Add(typeof(WizardsHat), 5);
                Add(typeof(Bonnet), 4);
                Add(typeof(FeatheredHat), 5);
                Add(typeof(TricorneHat), 4);

                Add(typeof(SpoolOfThread), 9);

                Add(typeof(Flax), 20);
                Add(typeof(Cotton), 20);
                Add(typeof(Wool), 20);
            }
        }
    }
}
