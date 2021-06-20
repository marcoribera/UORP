using System;
using System.Collections.Generic;
using Server.Guilds;
using Server.Items;

namespace Server.Mobiles
{
    public class SBProvisioner : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBProvisioner()
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
              //  Add(new GenericBuyInfo("1060834", typeof(Engines.Plants.PlantBowl), 2, 20, 0x15FD, 0));

                Add(new GenericBuyInfo(typeof(Arrow), 7, 200, 0xF3F, 0, true));
                Add(new GenericBuyInfo(typeof(Bolt), 7, 200, 0x1BFB, 0, true));

                Add(new GenericBuyInfo(typeof(Backpack), 100, 5, 0x9B2, 0));
                Add(new GenericBuyInfo(typeof(Pouch), 60, 5, 0xE79, 0));
                Add(new GenericBuyInfo(typeof(Bag), 60, 5, 0xE76, 0));
				
                Add(new GenericBuyInfo(typeof(Candle), 60, 3, 0xA28, 0));
                Add(new GenericBuyInfo(typeof(Torch), 30, 3, 0xF6B, 0));
                Add(new GenericBuyInfo(typeof(Lantern), 100, 3, 0xA25, 0));
                Add(new GenericBuyInfo(typeof(OilFlask), 50, 3, 0x1C18, 0));

                Add(new GenericBuyInfo(typeof(Lockpick), 50, 40, 0x14FC, 0, true));

                Add(new GenericBuyInfo(typeof(FloppyHat), 70, 5, 0x1713, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(WideBrimHat), 80, 5, 0x1714, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(Cap), 100, 5, 0x1715, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(TallStrawHat), 80, 5, 0x1716, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(StrawHat), 70, 5, 0x1717, Utility.RandomDyedHue()));
              //  Add(new GenericBuyInfo(typeof(WizardsHat), 110, 20, 0x1718, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(LeatherCap), 100, 5, 0x1DB9, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(FeatheredHat), 100, 5, 0x171A, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(TricorneHat), 80, 5, 0x171B, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(Bandana), 60, 5, 0x1540, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(SkullCap), 70, 5, 0x1544, Utility.RandomDyedHue()));

                Add(new GenericBuyInfo(typeof(BreadLoaf), 20, 5, 0x103B, 0, true));
                Add(new GenericBuyInfo(typeof(LambLeg), 35, 5, 0x160A, 0, true));
                Add(new GenericBuyInfo(typeof(ChickenLeg), 35, 5, 0x1608, 0, true));
                Add(new GenericBuyInfo(typeof(CookedBird), 25, 5, 0x9B7, 0, true));

              //  Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Ale, 7, 20, 0x99F, 0));
                //Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Wine, 7, 20, 0x9C7, 0));
                Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Liquor, 40, 5, 0x99B, 0));
                //Add(new BeverageBuyInfo(typeof(Jug), BeverageType.Cider, 13, 20, 0x9C8, 0));

                Add(new GenericBuyInfo(typeof(Pear), 15, 10, 0x994, 0, true));
             //   Add(new GenericBuyInfo(typeof(Apple), 3, 20, 0x9D0, 0, true));

             //   Add(new GenericBuyInfo(typeof(Beeswax), 1, 20, 0x1422, 0, true));

                Add(new GenericBuyInfo(typeof(Garlic), 10, 20, 0xF84, 0));
                Add(new GenericBuyInfo(typeof(Ginseng), 10, 20, 0xF85, 0));

                Add(new GenericBuyInfo(typeof(Bottle), 20, 25, 0xF0E, 0, true));

                Add(new GenericBuyInfo(typeof(RedBook), 10, 20, 0xFF1, 0));
                Add(new GenericBuyInfo(typeof(BlueBook), 10, 20, 0xFF2, 0));
                Add(new GenericBuyInfo(typeof(TanBook), 10, 20, 0xFF0, 0));

                Add(new GenericBuyInfo(typeof(WoodenBox), 200, 3, 0xE7D, 0));
              //  Add(new GenericBuyInfo(typeof(Key), 2, 20, 0x100E, 0));

                Add(new GenericBuyInfo(typeof(Bedroll), 120, 2, 0xA59, 0));
                Add(new GenericBuyInfo(typeof(Kindling), 25, 20, 0xDE1, 0, true));

            //    Add(new GenericBuyInfo("1041205", typeof(Multis.SmallBoatDeed), 10177, 20, 0x14F2, 0));

             //   Add(new GenericBuyInfo("1041060", typeof(HairDye), 60, 20, 0xEFF, 0));

            //    Add(new GenericBuyInfo("1016450", typeof(Chessboard), 2, 20, 0xFA6, 0));
            //    Add(new GenericBuyInfo("1016449", typeof(CheckerBoard), 2, 20, 0xFA6, 0));
            //    Add(new GenericBuyInfo(typeof(Backgammon), 2, 20, 0xE1C, 0));
         //       if (Core.AOS)
           //         Add(new GenericBuyInfo(typeof(Engines.Mahjong.MahjongGame), 6, 20, 0xFAA, 0));
             //   Add(new GenericBuyInfo(typeof(Dices), 2, 20, 0xFA7, 0));

            //    if (Core.AOS)
               // {
               //     Add(new GenericBuyInfo(typeof(SmallBagBall), 3, 20, 0x2256, 0));
               //     Add(new GenericBuyInfo(typeof(LargeBagBall), 3, 20, 0x2257, 0));
              //  }

            //    if (!Guild.NewGuildSystem)
            //        Add(new GenericBuyInfo("1041055", typeof(GuildDeed), 12450, 20, 0x14F0, 0));

            //    if (Core.ML)
              //      Add(new GenericBuyInfo("1079931", typeof(SalvageBag), 1255, 20, 0xE76, Utility.RandomBlueHue()));

            //    if (Core.SA)
             //       Add(new GenericBuyInfo("1114770", typeof(SkinTingeingTincture), 1255, 20, 0xEFF, 90));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(Arrow), 1);
                Add(typeof(Bolt), 2);
                Add(typeof(Backpack), 4);
                Add(typeof(Pouch), 3);
                Add(typeof(Bag), 3);
                Add(typeof(Candle), 3);
                Add(typeof(Torch), 4);
                Add(typeof(Lantern), 1);
                Add(typeof(Lockpick), 6);
                Add(typeof(FloppyHat), 3);
                Add(typeof(WideBrimHat), 4);
                Add(typeof(Cap), 5);
                Add(typeof(TallStrawHat), 4);
                Add(typeof(StrawHat), 3);
                Add(typeof(WizardsHat), 5);
                Add(typeof(LeatherCap), 5);
                Add(typeof(FeatheredHat), 5);
                Add(typeof(TricorneHat), 4);
                Add(typeof(Bandana), 3);
                Add(typeof(SkullCap), 3);
                Add(typeof(Bottle), 3);
                Add(typeof(RedBook), 7);
                Add(typeof(BlueBook), 7);
                Add(typeof(TanBook), 7);
                Add(typeof(WoodenBox), 7);
                Add(typeof(Kindling), 1);
                Add(typeof(HairDye), 5);
                Add(typeof(Chessboard), 1);
                Add(typeof(CheckerBoard), 1);
                Add(typeof(Backgammon), 1);
                Add(typeof(Dices), 1);

                Add(typeof(Beeswax), 1);

                Add(typeof(Amber), 5);
                Add(typeof(Amethyst), 5);
                Add(typeof(Citrine), 2);
                Add(typeof(Diamond), 10);
                Add(typeof(Emerald), 5);
                Add(typeof(Ruby), 3);
                Add(typeof(Sapphire), 5);
                Add(typeof(StarSapphire), 2);
                Add(typeof(Tourmaline), 4);
                Add(typeof(GoldRing), 3);
                Add(typeof(SilverRing), 2);
                Add(typeof(Necklace), 2);
                Add(typeof(GoldNecklace), 2);
                Add(typeof(GoldBeadNecklace), 2);
                Add(typeof(SilverNecklace), 2);
                Add(typeof(SilverBeadNecklace), 2);
                Add(typeof(Beads), 2);
                Add(typeof(GoldBracelet), 2);
                Add(typeof(SilverBracelet), 2);
                Add(typeof(GoldEarrings), 2);
                Add(typeof(SilverEarrings), 2);

          //      if (!Guild.NewGuildSystem)
          //          Add(typeof(GuildDeed), 6225);
            }
        }
    }
}
