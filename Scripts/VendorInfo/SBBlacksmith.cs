using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles 
{ 
    public class SBBlacksmith : SBInfo 
    { 
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();
        public SBBlacksmith() 
        { 
        }

        public override IShopSellInfo SellInfo
        {
            get
            {
                return this.m_SellInfo;
            }
        }
        public override List<GenericBuyInfo> BuyInfo
        {
            get
            {
                return this.m_BuyInfo;
            }
        }

        public class InternalBuyInfo : List<GenericBuyInfo> 
        { 
            public InternalBuyInfo() 
            {
               // this.Add(new GenericBuyInfo(typeof(IronIngot), 5, 16, 0x1BF2, 0, true));
                this.Add(new GenericBuyInfo(typeof(Tongs), 50, 2, 0xFBB, 0)); 
 
                this.Add(new GenericBuyInfo(typeof(BronzeShield), 150, 5, 0x1B72, 0));
             //   this.Add(new GenericBuyInfo(typeof(Buckler), 50, 20, 0x1B73, 0));
               // this.Add(new GenericBuyInfo(typeof(MetalKiteShield), 123, 20, 0x1B74, 0));
              // this.Add(new GenericBuyInfo(typeof(HeaterShield), 231, 20, 0x1B76, 0));
             //  this.Add(new GenericBuyInfo(typeof(WoodenKiteShield), 70, 20, 0x1B78, 0));
              //  this.Add(new GenericBuyInfo(typeof(MetalShield), 121, 20, 0x1B7B, 0));

               // this.Add(new GenericBuyInfo(typeof(WoodenShield), 100, 5, 0x1B7A, 0));

               // this.Add(new GenericBuyInfo(typeof(PlateGorget), 150, 20, 0x1413, 0));
               // this.Add(new GenericBuyInfo(typeof(PlateChest), 250, 20, 0x1415, 0));
               // this.Add(new GenericBuyInfo(typeof(PlateLegs), 250, 20, 0x1411, 0));
               // this.Add(new GenericBuyInfo(typeof(PlateArms), 190, 20, 0x1410, 0));
              //  this.Add(new GenericBuyInfo(typeof(PlateGloves), 150, 20, 0x1414, 0));

             //   this.Add(new GenericBuyInfo(typeof(PlateHelm), 21, 20, 0x1412, 0));
               // this.Add(new GenericBuyInfo(typeof(CloseHelm), 18, 20, 0x1408, 0));
              //  this.Add(new GenericBuyInfo(typeof(CloseHelm), 18, 20, 0x1409, 0));
               // this.Add(new GenericBuyInfo(typeof(Helmet), 31, 20, 0x140A, 0));
               this.Add(new GenericBuyInfo(typeof(Helmet), 150, 5, 0x140B, 0));
               this.Add(new GenericBuyInfo(typeof(NorseHelm), 150, 5, 0x140E, 0));
               // this.Add(new GenericBuyInfo(typeof(NorseHelm), 18, 20, 0x140F, 0));
               // this.Add(new GenericBuyInfo(typeof(Bascinet), 18, 20, 0x140C, 0));
               // this.Add(new GenericBuyInfo(typeof(PlateHelm), 21, 20, 0x1419, 0));

                this.Add(new GenericBuyInfo(typeof(ChainCoif), 150, 5, 0x13BB, 0));
                this.Add(new GenericBuyInfo(typeof(ChainChest), 300, 5, 0x13BF, 0));
                this.Add(new GenericBuyInfo(typeof(ChainLegs), 300, 5, 0x13BE, 0));

               // this.Add(new GenericBuyInfo(typeof(RingmailChest), 121, 20, 0x13ec, 0));
               // this.Add(new GenericBuyInfo(typeof(RingmailLegs), 90, 20, 0x13F0, 0));
                this.Add(new GenericBuyInfo(typeof(RingmailArms), 150, 5, 0x13EE, 0));
                this.Add(new GenericBuyInfo(typeof(RingmailGloves), 150, 5, 0x13eb, 0));

              //  this.Add(new GenericBuyInfo(typeof(ExecutionersAxe), 30, 20, 0xF45, 0));
              //  this.Add(new GenericBuyInfo(typeof(Bardiche), 60, 20, 0xF4D, 0));
                this.Add(new GenericBuyInfo(typeof(BattleAxe), 200, 3, 0xF47, 0));
              //  this.Add(new GenericBuyInfo(typeof(TwoHandedAxe), 32, 20, 0x1443, 0));
                this.Add(new GenericBuyInfo(typeof(Bow), 150, 3, 0x13B2, 0));
                this.Add(new GenericBuyInfo(typeof(ButcherKnife), 80, 3, 0x13F6, 0));
                this.Add(new GenericBuyInfo(typeof(Crossbow), 200, 3, 0xF50, 0));
              //  this.Add(new GenericBuyInfo(typeof(HeavyCrossbow), 55, 20, 0x13FD, 0));
               //this.Add(new GenericBuyInfo(typeof(Cutlass), 24, 20, 0x1441, 0));
                this.Add(new GenericBuyInfo(typeof(Dagger), 80, 3, 0xF52, 0));
             //   this.Add(new GenericBuyInfo(typeof(Halberd), 42, 20, 0x143E, 0));
                this.Add(new GenericBuyInfo(typeof(HammerPick), 200, 3, 0x143D, 0));
              //  this.Add(new GenericBuyInfo(typeof(Katana), 33, 20, 0x13FF, 0));
              //  this.Add(new GenericBuyInfo(typeof(Kryss), 32, 20, 0x1401, 0));
               // this.Add(new GenericBuyInfo(typeof(Broadsword), 35, 20, 0xF5E, 0));
                this.Add(new GenericBuyInfo(typeof(Longsword), 150, 3, 0xF61, 0));
              //  this.Add(new GenericBuyInfo(typeof(ThinLongsword), 27, 20, 0x13B8, 0));
               // this.Add(new GenericBuyInfo(typeof(VikingSword), 55, 20, 0x13B9, 0));
             //   this.Add(new GenericBuyInfo(typeof(Cleaver), 15, 20, 0xEC3, 0));
                this.Add(new GenericBuyInfo(typeof(Axe), 150, 3, 0xF49, 0));
             //   this.Add(new GenericBuyInfo(typeof(DoubleAxe), 52, 20, 0xF4B, 0));
                this.Add(new GenericBuyInfo(typeof(Pickaxe), 50, 3, 0xE86, 0));
                this.Add(new GenericBuyInfo(typeof(Pitchfork), 100, 3, 0xE87, 0));
              //  this.Add(new GenericBuyInfo(typeof(Scimitar), 36, 20, 0x13B6, 0));
                this.Add(new GenericBuyInfo(typeof(SkinningKnife), 3, 100, 0xEC4, 0));
             //   this.Add(new GenericBuyInfo(typeof(LargeBattleAxe), 33, 20, 0x13FB, 0));
             //   this.Add(new GenericBuyInfo(typeof(WarAxe), 29, 20, 0x13B0, 0));

               // if (Core.AOS)
                //{
                  //  this.Add(new GenericBuyInfo(typeof(BoneHarvester), 35, 20, 0x26BB, 0));
                    //this.Add(new GenericBuyInfo(typeof(CrescentBlade), 37, 20, 0x26C1, 0));
                   // this.Add(new GenericBuyInfo(typeof(DoubleBladedStaff), 35, 20, 0x26BF, 0));
                   // this.Add(new GenericBuyInfo(typeof(Lance), 34, 20, 0x26C0, 0));
                  //  this.Add(new GenericBuyInfo(typeof(Pike), 39, 20, 0x26BE, 0));
                   // this.Add(new GenericBuyInfo(typeof(Scythe), 39, 20, 0x26BA, 0));
                   // this.Add(new GenericBuyInfo(typeof(CompositeBow), 50, 20, 0x26C2, 0));
                   // this.Add(new GenericBuyInfo(typeof(RepeatingCrossbow), 57, 20, 0x26C3, 0));
             //   }

             //   this.Add(new GenericBuyInfo(typeof(BlackStaff), 22, 20, 0xDF1, 0));
                this.Add(new GenericBuyInfo(typeof(Club), 80, 3, 0x13B4, 0));
                this.Add(new GenericBuyInfo(typeof(GnarledStaff), 80, 3, 0x13F8, 0));
                this.Add(new GenericBuyInfo(typeof(Mace), 150, 3, 0xF5C, 0));
               // this.Add(new GenericBuyInfo(typeof(Maul), 21, 20, 0x143B, 0));
              //  this.Add(new GenericBuyInfo(typeof(QuarterStaff), 19, 20, 0xE89, 0));
                this.Add(new GenericBuyInfo(typeof(ShepherdsCrook), 80, 3, 0xE81, 0));
                this.Add(new GenericBuyInfo(typeof(SmithHammer), 50, 3, 0x13E3, 0));
            //    this.Add(new GenericBuyInfo(typeof(ShortSpear), 23, 20, 0x1403, 0));
                this.Add(new GenericBuyInfo(typeof(Spear), 200, 3, 0xF62, 0));
               // this.Add(new GenericBuyInfo(typeof(WarHammer), 25, 20, 0x1439, 0));
             //   this.Add(new GenericBuyInfo(typeof(WarMace), 31, 20, 0x1407, 0));

              //  if (Core.AOS)
              //  {
            //        this.Add(new GenericBuyInfo(typeof(Scepter), 39, 20, 0x26BC, 0));
             //       this.Add(new GenericBuyInfo(typeof(BladedStaff), 40, 20, 0x26BD, 0));
             //   }

           //     Add(new GenericBuyInfo("1154005", typeof(MalleableAlloy), 50, 500, 7139, 2949, true));
            }
        }

        public class InternalSellInfo : GenericSellInfo 
        { 
            public InternalSellInfo() 
            { 
                this.Add(typeof(Tongs), 2); 
                this.Add(typeof(IronIngot), 2); 

                this.Add(typeof(Buckler), 4);
                this.Add(typeof(BronzeShield), 5);
                this.Add(typeof(MetalShield), 7);
                this.Add(typeof(MetalKiteShield), 10);
                this.Add(typeof(HeaterShield), 20);
                this.Add(typeof(WoodenKiteShield), 8);

                this.Add(typeof(WoodenShield), 4);

                this.Add(typeof(PlateArms), 12);
                this.Add(typeof(PlateChest), 20);
                this.Add(typeof(PlateGloves), 10);
                this.Add(typeof(PlateGorget), 12);
                this.Add(typeof(PlateLegs), 20);

                this.Add(typeof(FemalePlateChest), 20);
                this.Add(typeof(FemaleLeatherChest), 4);
                this.Add(typeof(FemaleStuddedChest), 4);
                this.Add(typeof(LeatherShorts), 4);
                this.Add(typeof(LeatherSkirt), 4);
                this.Add(typeof(LeatherBustierArms), 4);
                this.Add(typeof(StuddedBustierArms), 4);

                this.Add(typeof(Bascinet), 4);
                this.Add(typeof(CloseHelm), 4);
                this.Add(typeof(Helmet), 4);
                this.Add(typeof(NorseHelm), 4);
                this.Add(typeof(PlateHelm), 6);

                this.Add(typeof(ChainCoif), 10);
                this.Add(typeof(ChainChest), 10);
                this.Add(typeof(ChainLegs), 10);

                this.Add(typeof(RingmailArms), 8);
                this.Add(typeof(RingmailChest), 8);
                this.Add(typeof(RingmailGloves), 8);
                this.Add(typeof(RingmailLegs), 8);

                this.Add(typeof(BattleAxe), 7);
                this.Add(typeof(DoubleAxe), 8);
                this.Add(typeof(ExecutionersAxe), 8);
                this.Add(typeof(LargeBattleAxe), 8);
                this.Add(typeof(Pickaxe), 2);
                this.Add(typeof(TwoHandedAxe), 8);
                this.Add(typeof(WarAxe), 7);
                this.Add(typeof(Axe), 5);

                this.Add(typeof(Bardiche), 10);
                this.Add(typeof(Halberd), 10);

                this.Add(typeof(ButcherKnife), 2);
                this.Add(typeof(Cleaver), 2);
                this.Add(typeof(Dagger), 2);
                this.Add(typeof(SkinningKnife), 2);

                this.Add(typeof(Club), 2);
                this.Add(typeof(HammerPick), 6);
                this.Add(typeof(Mace), 5);
                this.Add(typeof(Maul), 6);
                this.Add(typeof(WarHammer), 6);
                this.Add(typeof(WarMace), 6);

                this.Add(typeof(HeavyCrossbow), 7);
                this.Add(typeof(Bow), 5);
                this.Add(typeof(Crossbow), 6); 

                if (Core.AOS)
                {
                    this.Add(typeof(CompositeBow), 8);
                    this.Add(typeof(RepeatingCrossbow), 8);
                    this.Add(typeof(Scepter), 8);
                    this.Add(typeof(BladedStaff), 8);
                    this.Add(typeof(Scythe), 8);
                    this.Add(typeof(BoneHarvester), 8);
                    this.Add(typeof(Scepter), 8);
                    this.Add(typeof(BladedStaff), 7);
                    this.Add(typeof(Pike), 7);
                    this.Add(typeof(DoubleBladedStaff), 8);
                    this.Add(typeof(Lance), 8);
                    this.Add(typeof(CrescentBlade), 6);
                }

                this.Add(typeof(Spear), 4);
                this.Add(typeof(Pitchfork), 2);
                this.Add(typeof(ShortSpear), 3);

                this.Add(typeof(BlackStaff), 3);
                this.Add(typeof(GnarledStaff), 2);
                this.Add(typeof(QuarterStaff), 2);
                this.Add(typeof(ShepherdsCrook), 2);

                this.Add(typeof(SmithHammer), 1);

                this.Add(typeof(Broadsword), 7);
                this.Add(typeof(Cutlass), 4);
                this.Add(typeof(Katana), 4);
                this.Add(typeof(Kryss), 4);
                this.Add(typeof(Longsword), 6);
                this.Add(typeof(Scimitar), 6);
                this.Add(typeof(ThinLongsword), 3);
                this.Add(typeof(VikingSword), 7);
            }
        }
    }
}
