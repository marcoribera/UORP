using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBFeiticeiro : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBFeiticeiro()
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
               
                Add(new GenericBuyInfo(typeof(ThunderstormScroll), 250, 10, 0x0E34, 0, true));
                Add(new GenericBuyInfo(typeof(ArcaneCircleScroll), 250, 10, 0x0E34, 0, true));
                Add(new GenericBuyInfo(typeof(GiftOfRenewalScroll), 250, 10, 0x0E34, 0, true));
                Add(new GenericBuyInfo(typeof(ImmolatingWeaponScroll), 250, 10, 0x0E34, 0, true));
                Add(new GenericBuyInfo(typeof(AttuneWeaponScroll), 250, 10, 0x0E34, 0, true));
                Add(new GenericBuyInfo(typeof(MagicalResidue), 150, 5, 0x2DB1, 0));
                Add(new GenericBuyInfo(typeof(EnchantedEssence), 250, 2, 0x2DB2, 0));
                Add(new GenericBuyInfo(typeof(RelicFragment), 450, 2, 0x2DB3, 0));
               
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(PurgeMagicScroll), 5);
                Add(typeof(EnchantScroll), 5);
                Add(typeof(SleepScroll), 5);
                Add(typeof(EagleStrikeScroll), 5);
                Add(typeof(AnimatedWeaponScroll), 5);
                Add(typeof(StoneFormScroll), 5);
                Add(typeof(MysticBook), 5);
                Add(typeof(RecallRune), 5);

                Add(typeof(BlackPearl), 3); 
                Add(typeof(Bloodmoss), 4); 
                Add(typeof(MandrakeRoot), 2); 
                Add(typeof(Garlic), 2); 
                Add(typeof(Ginseng), 2); 
                Add(typeof(Nightshade), 2); 
                Add(typeof(SpidersSilk), 2); 
                Add(typeof(SulfurousAsh), 2); 
                
                Add(typeof(NetherBoltScroll), 4);
                Add(typeof(HealingStoneScroll), 6);
            }
        }
    }
}
