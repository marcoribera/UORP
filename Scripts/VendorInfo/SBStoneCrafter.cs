using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBStoneCrafter : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBStoneCrafter()
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
                Add(new GenericBuyInfo(typeof(Nails), 20, 20, 0x102E, 0));
                Add(new GenericBuyInfo(typeof(Axle), 20, 20, 0x105B, 0, true));
           //   Add(new GenericBuyInfo(typeof(Board), 30, 20, 0x1BD7, 0, true));
                Add(new GenericBuyInfo(typeof(DrawKnife), 50, 3, 0x10E4, 0));
                Add(new GenericBuyInfo(typeof(Froe), 50, 3, 0x10E5, 0));
                Add(new GenericBuyInfo(typeof(Scorp), 50, 3, 0x10E7, 0));
                Add(new GenericBuyInfo(typeof(Inshave), 50, 3, 0x10E6, 0));
                Add(new GenericBuyInfo(typeof(DovetailSaw), 3, 20, 0x1028, 0));
                Add(new GenericBuyInfo(typeof(Saw), 50, 3, 0x1034, 0));
                Add(new GenericBuyInfo(typeof(Hammer), 50, 3, 0x102A, 0));
                Add(new GenericBuyInfo(typeof(MouldingPlane), 50, 3, 0x102C, 0));
                Add(new GenericBuyInfo(typeof(SmoothingPlane), 50, 3, 0x1032, 0));
                Add(new GenericBuyInfo(typeof(JointingPlane), 50, 3, 0x1030, 0));

              //  Add(new GenericBuyInfo("Making Valuables With Stonecrafting", typeof(MasonryBook), 10625, 10, 0xFBE, 0));
              //  Add(new GenericBuyInfo("Mining For Quality Stone", typeof(StoneMiningBook), 10625, 10, 0xFBE, 0));
             //   Add(new GenericBuyInfo("1044515", typeof(MalletAndChisel), 3, 50, 0x12B3, 0));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(MasonryBook), 5000);
                Add(typeof(StoneMiningBook), 5000);
                Add(typeof(MalletAndChisel), 1);

                Add(typeof(WoodenBox), 7);
                Add(typeof(SmallCrate), 5);
                Add(typeof(MediumCrate), 6);
                Add(typeof(LargeCrate), 7);
                Add(typeof(WoodenChest), 8);
              
                Add(typeof(LargeTable), 8);
                Add(typeof(Nightstand), 7);
                Add(typeof(YewWoodTable), 8);

                Add(typeof(Throne), 10);
                Add(typeof(WoodenThrone), 6);
                Add(typeof(Stool), 6);
                Add(typeof(FootStool), 6);

                Add(typeof(FancyWoodenChairCushion), 8);
                Add(typeof(WoodenChairCushion), 8);
                Add(typeof(WoodenChair), 8);
                Add(typeof(BambooChair), 6);
                Add(typeof(WoodenBench), 6);

                Add(typeof(Saw), 9);
                Add(typeof(Scorp), 6);
                Add(typeof(SmoothingPlane), 6);
                Add(typeof(DrawKnife), 6);
                Add(typeof(Froe), 6);
                Add(typeof(Hammer), 14);
                Add(typeof(Inshave), 6);
                Add(typeof(JointingPlane), 6);
                Add(typeof(MouldingPlane), 6);
                Add(typeof(DovetailSaw), 7);
                Add(typeof(Board), 2);
                Add(typeof(Axle), 1);

                Add(typeof(WoodenShield), 5);
                Add(typeof(BlackStaff), 5);
                Add(typeof(GnarledStaff), 3);
                Add(typeof(QuarterStaff), 3);
                Add(typeof(ShepherdsCrook), 3);
                Add(typeof(Club), 3);

                Add(typeof(Log), 1);
            }
        }
    }
}
