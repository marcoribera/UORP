using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBCarpenter : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBCarpenter()
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
                Add(new GenericBuyInfo(typeof(Nails), 50, 3, 0x102E, 0));
               // Add(new GenericBuyInfo(typeof(Axle), 2, 20, 0x105B, 0, true));
               // Add(new GenericBuyInfo(typeof(Board), 3, 20, 0x1BD7, 0, true));
                Add(new GenericBuyInfo(typeof(DrawKnife), 50, 3, 0x10E4, 0));
                Add(new GenericBuyInfo(typeof(Froe), 50, 3, 0x10E5, 0));
               Add(new GenericBuyInfo(typeof(Scorp), 50, 3, 0x10E7, 0));
                Add(new GenericBuyInfo(typeof(Inshave), 50, 3, 0x10E6, 0));
               Add(new GenericBuyInfo(typeof(DovetailSaw), 50, 3, 0x1028, 0));
                Add(new GenericBuyInfo(typeof(Saw), 50, 3, 0x1034, 0));
                Add(new GenericBuyInfo(typeof(Hammer), 50, 3, 0x102A, 0));
                Add(new GenericBuyInfo(typeof(MouldingPlane), 50, 3, 0x102C, 0));
                Add(new GenericBuyInfo(typeof(SmoothingPlane), 50, 3, 0x1032, 0));
                Add(new GenericBuyInfo(typeof(JointingPlane), 50, 3, 0x1030, 0));
                Add(new GenericBuyInfo(typeof(Drums), 300, 2, 0xE9C, 0));
                Add(new GenericBuyInfo(typeof(Tambourine), 350, 3, 0xE9D, 0));
                Add(new GenericBuyInfo(typeof(LapHarp), 350, 3, 0xEB2, 0));
                Add(new GenericBuyInfo(typeof(Lute), 350, 3, 0xEB3, 0));

               // Add(new GenericBuyInfo("1154004", typeof(SolventFlask), 50, 500, 7192, 2969, true));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(WoodenBox), 5);
                Add(typeof(SmallCrate), 3);
                Add(typeof(MediumCrate), 6);
                Add(typeof(LargeCrate), 7);
                Add(typeof(WoodenChest), 10);
              
                Add(typeof(LargeTable), 8);
                Add(typeof(Nightstand), 7);
                Add(typeof(YewWoodTable), 8);

                Add(typeof(Throne), 15);
                Add(typeof(WoodenThrone), 6);
                Add(typeof(Stool), 6);
                Add(typeof(FootStool), 6);

                Add(typeof(FancyWoodenChairCushion), 8);
                Add(typeof(WoodenChairCushion), 7);
                Add(typeof(WoodenChair), 5);
                Add(typeof(BambooChair), 4);
                Add(typeof(WoodenBench), 4);

                Add(typeof(Saw), 2);
                Add(typeof(Scorp), 2);
                Add(typeof(SmoothingPlane), 2);
                Add(typeof(DrawKnife), 2);
                Add(typeof(Froe), 2);
                Add(typeof(Hammer), 4);
                Add(typeof(Inshave), 2);
                Add(typeof(JointingPlane), 2);
                Add(typeof(MouldingPlane), 2);
                Add(typeof(DovetailSaw), 2);
                Add(typeof(Board), 2);
              //  Add(typeof(Axle), 1);

                Add(typeof(Club), 4);

                Add(typeof(Lute), 10);
                Add(typeof(LapHarp), 10);
                Add(typeof(Tambourine), 10);
                Add(typeof(Drums), 10);

                Add(typeof(Log), 1);
            }
        }
    }
}
