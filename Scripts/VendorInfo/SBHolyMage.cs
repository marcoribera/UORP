using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBHolyMage : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBHolyMage()
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
                Add(new GenericBuyInfo(typeof(Spellbook), 1000, 2, 0xEFA, 0));
                Add(new GenericBuyInfo(typeof(ScribesPen), 50, 3, 0xFBF, 0));
                Add(new GenericBuyInfo(typeof(BlankScroll), 10, 40, 0x0E34, 0));

             //   Add(new GenericBuyInfo("1041072", typeof(MagicWizardsHat), 11, 10, 0x1718, Utility.RandomDyedHue()));

               // Add(new GenericBuyInfo(typeof(RecallRune), 15, 10, 0x1f14, 0));

             //   Add(new GenericBuyInfo(typeof(RefreshPotion), 15, 20, 0xF0B, 0, true));
               // Add(new GenericBuyInfo(typeof(AgilityPotion), 15, 20, 0xF08, 0, true));
              //  Add(new GenericBuyInfo(typeof(NightSightPotion), 15, 20, 0xF06, 0, true));
                Add(new GenericBuyInfo(typeof(LesserHealPotion), 50, 10, 0xF0C, 0, true));
              //  Add(new GenericBuyInfo(typeof(StrengthPotion), 15, 20, 0xF09, 0, true));
                Add(new GenericBuyInfo(typeof(LesserCurePotion), 50, 10, 0xF07, 0, true));

                Add(new GenericBuyInfo(typeof(BlackPearl), 7, 230, 0xF7A, 0));
                Add(new GenericBuyInfo(typeof(Bloodmoss), 7, 230, 0xF7B, 0));
                Add(new GenericBuyInfo(typeof(Garlic), 7, 230, 0xF84, 0));
                Add(new GenericBuyInfo(typeof(Ginseng), 7, 230, 0xF85, 0));
                Add(new GenericBuyInfo(typeof(MandrakeRoot), 7, 230, 0xF86, 0));
                Add(new GenericBuyInfo(typeof(Nightshade), 7, 230, 0xF88, 0));
                Add(new GenericBuyInfo(typeof(SpidersSilk), 7, 230, 0xF8D, 0));
                Add(new GenericBuyInfo(typeof(SulfurousAsh), 7, 230, 0xF8C, 0));

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
                Add(typeof(BlackPearl), 3); 
                Add(typeof(Bloodmoss), 3); 
                Add(typeof(MandrakeRoot), 2); 
                Add(typeof(Garlic), 2); 
                Add(typeof(Ginseng), 2); 
                Add(typeof(Nightshade), 2); 
                Add(typeof(SpidersSilk), 2); 
                Add(typeof(SulfurousAsh), 2); 
                Add(typeof(RecallRune), 8);
                Add(typeof(Spellbook), 9);
                Add(typeof(BlankScroll), 3);

                Add(typeof(NightSightPotion), 7);
                Add(typeof(AgilityPotion), 7);
                Add(typeof(StrengthPotion), 7);
                Add(typeof(RefreshPotion), 7);
                Add(typeof(LesserCurePotion), 7);
                Add(typeof(LesserHealPotion), 7);

                Type[] types = Loot.RegularScrollTypes;

                for (int i = 0; i < types.Length; ++i)
                    Add(types[i], ((i / 8) + 2) * 2);
            }
        }
    }
}
