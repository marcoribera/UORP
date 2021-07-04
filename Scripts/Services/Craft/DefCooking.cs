using System;
using Server.Items;

namespace Server.Engines.Craft
{
    #region Recipes
    public enum CookRecipes
    {
        // magical
        RotWormStew = 500,
        GingerbreadCookie = 599,

        DarkChocolateNutcracker = 600,
        MilkChocolateNutcracker = 601,
        WhiteChocolateNutcracker = 602,

        ThreeTieredCake = 603,
        BlackrockStew = 604,
        Hamburger = 605,
        HotDog = 606,
        Sausage = 607
    }
    #endregion

    public class DefCooking : CraftSystem
    {
        public override SkillName MainSkill
        {
            get
            {
                return SkillName.Culinaria;
            }
        }

        public override int GumpTitleNumber
        {
            get
            {
                return 1044003;
            }// <CENTER>COOKING MENU</CENTER>
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefCooking();

                return m_CraftSystem;
            }
        }

        public override CraftECA ECA
        {
            get
            {
                return CraftECA.ChanceMinusSixtyToFourtyFive;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            if (item.ItemType == typeof(GrapesOfWrath) ||
                item.ItemType == typeof(EnchantedApple))
            {
                return .5;
            }

            return 0.0; // 0%
        }

        private DefCooking()
            : base(1, 1, 1.25)// base( 1, 1, 1.5 )
        {
        }

        public override int CanCraft(Mobile from, ITool tool, Type itemType)
        {
            int num = 0;

            if (tool == null || tool.Deleted || tool.UsesRemaining <= 0)
                return 1044038; // You have worn out your tool!
            else if (!tool.CheckAccessible(from, ref num))
                return num; // The tool must be on your person to use.

            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (failed)
            {
                if (lostMaterial)
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                else
                    return 1044157; // You failed to create the item, but no materials were lost.
            }
            else
            {
                if (quality == 0)
                    return 502785; // You were barely able to make this item.  It's quality is below average.
                else if (makersMark && quality == 2)
                    return 1044156; // You create an exceptional quality item and affix your maker's mark.
                else if (quality == 2)
                    return 1044155; // You create an exceptional quality item.
                else 
                    return 1044154; // You create the item.
            }
        }

        public override void InitCraftList()
        {
            int index = -1;

            #region Ingredients
            index = AddCraft(typeof(SackFlour), 1044495, 1024153, 0.0, 100.0, typeof(WheatSheaf), 1044489, 2, 1044490);
            SetNeedMill(index, true);

            index = AddCraft(typeof(Dough), 1044495, 1024157, 0.0, 100.0, typeof(SackFlourOpen), 1044468, 1, 1151092);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);

            index = AddCraft(typeof(SweetDough), 1044495, 1041340, 0.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(JarHoney), 1044472, 1, 1044253);

            index = AddCraft(typeof(CakeMix), 1044495, 1041002, 0.0, 100.0, typeof(SackFlourOpen), 1044468, 1, 1151092);
            AddRes(index, typeof(SweetDough), 1044475, 1, 1044253);

            index = AddCraft(typeof(CookieMix), 1044495, 1024159, 0.0, 100.0, typeof(JarHoney), 1044472, 1, 1044253);
            AddRes(index, typeof(SweetDough), 1044475, 1, 1044253);

            if (Core.ML)
            {
                index = AddCraft(typeof(CocoaButter), 1044495, 1079998, 10.0, 100.0, typeof(CocoaPulp), 1080530, 1, 1044253);
                SetItemHue(index, 0x457);
                SetNeedOven(index, true);

                index = AddCraft(typeof(CocoaLiquor), 1044495, 1079999, 10.0, 100.0, typeof(CocoaPulp), 1080530, 1, 1044253);
                AddRes(index, typeof(EmptyPewterBowl), 1025629, 1, 1044253);
                SetItemHue(index, 0x46A);
                SetNeedOven(index, true);
            }

            index = AddCraft(typeof(WheatWort), 1044495, 1150275, 15.0, 100.0, typeof(Bottle), 1023854, 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRes(index, typeof(SackFlourOpen), 1044468, 1, 1151092);
            SetItemHue(index, 1281);
            #endregion

            #region Preparations
            index = AddCraft(typeof(UnbakedQuiche), 1044496, 1041339, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Eggs), 1044477, 1, 1044253);

            // TODO: This must also support chicken and lamb legs
            index = AddCraft(typeof(UnbakedMeatPie), 1044496, 1041338, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(RawRibs), 1044482, 1, 1044253);

            index = AddCraft(typeof(UncookedSausagePizza), 1044496, 1041337, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Sausage), 1044483, 1, 1044253);

            index = AddCraft(typeof(UncookedCheesePizza), 1044496, 1041341, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(CheeseWheel), 1044486, 1, 1044253);

            index = AddCraft(typeof(UnbakedFruitPie), 1044496, 1041334, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Pear), 1044481, 1, 1044253);

            index = AddCraft(typeof(UnbakedPeachCobbler), 1044496, 1041335, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Peach), 1044480, 1, 1044253);

            index = AddCraft(typeof(UnbakedApplePie), 1044496, 1041336, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Apple), 1044479, 1, 1044253);

            index = AddCraft(typeof(UnbakedPumpkinPie), 1044496, 1041342, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Pumpkin), 1044484, 1, 1044253);

            if (Core.SE)
            {
                index = AddCraft(typeof(GreenTea), 1044496, 1030316, 60.0, 130.0, typeof(GreenTeaBasket), 1030316, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(WasabiClumps), 1044496, 1029451, 50.0, 120.0, typeof(BaseBeverage), 1046458, 1, 1044253);
                AddRes(index, typeof(WoodenBowlOfPeas), 1025633, 3, 1044253);

                index = AddCraft(typeof(SushiRolls), 1044496, 1030303, 70.0, 120.0, typeof(BaseBeverage), 1046458, 1, 1044253);
                AddRes(index, typeof(RawFishSteak), 1044476, 10, 1044253);

                index = AddCraft(typeof(SushiPlatter), 1044496, 1030305, 70.0, 120.0, typeof(BaseBeverage), 1046458, 1, 1044253);
                AddRes(index, typeof(RawFishSteak), 1044476, 10, 1044253);
            }

            index = AddCraft(typeof(TribalPaint), 1044496, 1040000, Core.ML ? 40.0 : 80.0, Core.ML ? 105.0 : 80.0, typeof(SackFlourOpen), 1044468, 1, 1151092);
            AddRes(index, typeof(TribalBerry), 1046460, 1, 1044253);

            if (Core.SE)
            {
                index = AddCraft(typeof(EggBomb), 1044496, 1030249, 70.0, 120.0, typeof(Eggs), 1044477, 1, 1044253);
                AddRes(index, typeof(SackFlourOpen), 1044468, 3, 1151092);
            }

            #region Mondain's Legacy
            if (Core.ML)
            {
                index = AddCraft(typeof(ParrotWafer), 1044496, 1032246, 30.5, 87.5, typeof(Dough), 1044469, 1, 1044253);
                AddRes(index, typeof(JarHoney), 1044472, 1, 1044253);
                AddRes(index, typeof(RawFishSteak), 1044476, 10, 1044253);
            }
            #endregion

            #region SA
            if (Core.SA)
            {
                index = AddCraft(typeof(PlantPigment), 1044496, 1112132, 60.0, 100.0, typeof(PlantClippings), 1112131, 1, 1044253);
                AddRes(index, typeof(Bottle), 1023854, 1, 1044253);
                SetRequireResTarget(index);

                index = AddCraft(typeof(NaturalDye), 1044496, 1112136, 45.0, 115.0, typeof(PlantPigment), 1112132, 1, 1044253);
                AddRes(index, typeof(ColorFixative), 1112135, 1, 1044253);
                SetRequireResTarget(index);

                index = AddCraft(typeof(ColorFixative), 1044496, 1112135, 55.0, 100.0, typeof(BaseBeverage), 1022503, 1, 1044253);
                AddRes(index, typeof(SilverSerpentVenom), 1112173, 1, 1044253);
                SetBeverageType(index, BeverageType.Wine);

                index = AddCraft(typeof(WoodPulp), 1044496, 1113136, 40.0, 100.0, typeof(BarkFragment), 1032687, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            }
            #endregion

            #region High Seas
            if (Core.HS)
            {
                index = AddCraft(typeof(Charcoal), 1044496, 1116303, 10.0, 50.0, typeof(Board), 1044041, 1, 1044351);
                SetUseAllRes(index, true);
                SetNeedHeat(index, true);
            }
            #endregion
            #endregion

            #region Baking
            index = AddCraft(typeof(BreadLoaf), 1044497, 1024156, 0.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Cookies), 1044497, 1025643, 10.0, 100.0, typeof(CookieMix), 1044474, 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Cake), 1044497, 1022537, 10.0, 100.0, typeof(CakeMix), 1044471, 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Muffins), 1044497, 1022539, 10.0, 100.0, typeof(SweetDough), 1044475, 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Quiche), 1044497, 1041345, 10.0, 100.0, typeof(UnbakedQuiche), 1044518, 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(MeatPie), 1044497, 1041347, 10.0, 100.0, typeof(UnbakedMeatPie), 1044519, 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(SausagePizza), 1044497, 1044517, 10.0, 100.0, typeof(UncookedSausagePizza), 1044520, 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(CheesePizza), 1044497, 1044516, 10.0, 100.0, typeof(UncookedCheesePizza), 1044521, 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(FruitPie), 1044497, 1041346, 10.0, 100.0, typeof(UnbakedFruitPie), 1044522, 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(PeachCobbler), 1044497, 1041344, 10.0, 100.0, typeof(UnbakedPeachCobbler), 1044523, 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(ApplePie), 1044497, 1041343, 10.0, 100.0, typeof(UnbakedApplePie), 1044524, 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(PumpkinPie), 1044497, 1041348, 15.0, 100.0, typeof(UnbakedPumpkinPie), 1046461, 1, 1044253);
            SetNeedOven(index, true);

            if (Core.SE)
            {
                index = AddCraft(typeof(MisoSoup), 1044497, 1030317, 40.0, 110.0, typeof(RawFishSteak), 1044476, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(WhiteMisoSoup), 1044497, 1030318, 40.0, 110.0, typeof(RawFishSteak), 1044476, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(RedMisoSoup), 1044497, 1030319, 45.0, 110.0, typeof(RawFishSteak), 1044476, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(AwaseMisoSoup), 1044497, 1030320, 45.0, 110.0, typeof(RawFishSteak), 1044476, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
                SetNeedOven(index, true);
            }

            index = AddCraft(typeof(GingerBreadCookie), 1044497, 1031233, 25.0, 85.0, typeof(CookieMix), 1044474, 1, 1044253);
            AddRes(index, typeof(FreshGinger), 1031235, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.GingerbreadCookie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(ThreeTieredCake), 1044497, 1154465, 45.0, 110.0, typeof(CakeMix), 1044471, 3, 1044253);
            AddRecipe(index, (int)CookRecipes.ThreeTieredCake);
            SetNeedOven(index, true);
            #endregion

            #region Barbecue
            index = AddCraft(typeof(CookedBird), 1044498, 1022487, 1.0, 100.0, typeof(RawBird), 1044470, 1, 1044253);
            SetNeedHeat(index, true);
            SetUseAllRes(index, true);
            ForceNonExceptional(index);

            index = AddCraft(typeof(ChickenLeg), 1044498, 1025640, 10.0, 100.0, typeof(RawChickenLeg), 1044473, 1, 1044253);
            SetNeedHeat(index, true);
            SetUseAllRes(index, true);
            ForceNonExceptional(index);

            index = AddCraft(typeof(FishSteak), 1044498, 1022427, 10.0, 100.0, typeof(RawFishSteak), 1044476, 1, 1044253);
            SetNeedHeat(index, true);
            SetUseAllRes(index, true);
            ForceNonExceptional(index);

            index = AddCraft(typeof(FriedEggs), 1044498, 1022486, 10.0, 100.0, typeof(Eggs), 1044477, 1, 1044253);
            SetNeedHeat(index, true);
            SetUseAllRes(index, true);
            ForceNonExceptional(index);

            index = AddCraft(typeof(LambLeg), 1044498, 1025642, 10.0, 100.0, typeof(RawLambLeg), 1044478, 1, 1044253);
            SetNeedHeat(index, true);
            SetUseAllRes(index, true);
            ForceNonExceptional(index);

            index = AddCraft(typeof(Ribs), 1044498, 1022546, 10.0, 100.0, typeof(RawRibs), 1044485, 1, 1044253);
            SetNeedHeat(index, true);
            SetUseAllRes(index, true);
            ForceNonExceptional(index);

            if (Core.SA)
            {
                index = AddCraft(typeof(BowlOfRotwormStew), 1044498, 1031706, 15.0, 100.0, typeof(RawRotwormMeat), 1031705, 1, 1044253);
                SetNeedHeat(index, true);
                SetUseAllRes(index, true);
                AddRecipe(index, (int)CookRecipes.RotWormStew);
                ForceNonExceptional(index);

                index = AddCraft(typeof(BowlOfBlackrockStew), 1044498, 1115752, 20.0, 70.0, typeof(BowlOfRotwormStew), 1031706, 1, 1044253);
                AddRes(index, typeof(SmallPieceofBlackrock), 1153836, 1, 1044253);
                SetNeedHeat(index, true);
                SetUseAllRes(index, true);
                SetItemHue(index, 1954);
                AddRecipe(index, (int)CookRecipes.BlackrockStew);
                ForceNonExceptional(index);
            }

            if (Core.EJ)
            {
                index = AddCraft(typeof(KhaldunTastyTreat), 1044498, 1158680, 40.0, 100.0, typeof(RawFishSteak), 1044476, 40, 1044253);
                SetUseAllRes(index, true);
                SetNeedHeat(index, true);
            }

            if (Core.TOL)
            {
                index = AddCraft(typeof(Hamburger), 1044498, 1125202, 20.0, 80.0, typeof(BreadLoaf), 1024155, 1, 1044253);
                AddRes(index, typeof(RawRibs), 1044485, 1, 1044253);
                AddRes(index, typeof(Lettuce), 1023184, 1, 1044253);
                SetNeedHeat(index, true);
                SetUseAllRes(index, true);
                AddRecipe(index, (int)CookRecipes.Hamburger);

                index = AddCraft(typeof(HotDog), 1044498, 1125200, 20.0, 80.0, typeof(BreadLoaf), 1024155, 1, 1044253);
                AddRes(index, typeof(Sausage), 1125198, 1, 1044253);
                SetNeedHeat(index, true);
                SetUseAllRes(index, true);
                AddRecipe(index, (int)CookRecipes.HotDog);

                index = AddCraft(typeof(CookableSausage), 1044498, 1125198, 20.0, 70.0, typeof(Ham), 1022515, 1, 1044253);
                AddRes(index, typeof(DriedHerbs), 1023137, 1, 1044253);
                SetNeedHeat(index, true);
                SetUseAllRes(index, true);
                AddRecipe(index, (int)CookRecipes.Sausage);
            }
            #endregion

            #region Enchanted
            if (Core.ML)
            {
                index = AddCraft(typeof(FoodEngraver), 1073108, 1072951, 50.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
                AddRes(index, typeof(JarHoney), 1044472, 1, 1044253);

                index = AddCraft(typeof(EnchantedApple), 1073108, 1072952, 40.0, 85.0, typeof(Apple), 1044479, 1, 1044253);
                AddRes(index, typeof(GreaterHealPotion), 1073467, 1, 1044253);
                ForceNonExceptional(index);

                index = AddCraft(typeof(GrapesOfWrath), 1073108, 1072953, 55.0, 120.0, typeof(Grapes), 1073468, 1, 1044253);
                AddRes(index, typeof(GreaterStrengthPotion), 1073466, 1, 1044253);
                ForceNonExceptional(index);

                index = AddCraft(typeof(FruitBowl), 1073108, 1072950, 45.0, 105.0, typeof(EmptyWoodenBowl), 1073472, 1, 1044253);
                AddRes(index, typeof(Pear), 1044481, 3, 1044253);
                AddRes(index, typeof(Apple), 1044479, 3, 1044253);
                AddRes(index, typeof(Banana), 1073470, 3, 1044253);
            }
            #endregion

            #region Chocolatiering
            if (Core.ML)
            {
                if (Core.TOL)
                {
                    index = AddCraft(typeof(SweetCocoaButter), 1080001, 1156401, 15.0, 100.0, typeof(SackOfSugar), 1079997, 1, 1044253);
                    AddRes(index, typeof(CocoaButter), 1079998, 1, 1044253);
                    SetItemHue(index, 0x457);
                    SetNeedOven(index, true);
                }

                index = AddCraft(typeof(DarkChocolate), 1080001, 1079994, 15.0, 100.0, typeof(SackOfSugar), 1079997, 1, 1044253);
                AddRes(index, typeof(CocoaButter), 1079998, 1, 1044253);
                AddRes(index, typeof(CocoaLiquor), 1079999, 1, 1044253);
                SetItemHue(index, 0x465);

                index = AddCraft(typeof(MilkChocolate), 1080001, 1079995, 32.5, 107.5, typeof(SackOfSugar), 1079997, 1, 1044253);
                AddRes(index, typeof(CocoaButter), 1079998, 1, 1044253);
                AddRes(index, typeof(CocoaLiquor), 1079999, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1022544, 1, 1044253);
                SetBeverageType(index, BeverageType.Milk);
                SetItemHue(index, 0x461);

                index = AddCraft(typeof(WhiteChocolate), 1080001, 1079996, 42.5, 127.5, typeof(SackOfSugar), 1079997, 1, 1044253);
                AddRes(index, typeof(CocoaButter), 1079998, 1, 1044253);
                AddRes(index, typeof(Vanilla), 1080000, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1022544, 1, 1044253);
                SetBeverageType(index, BeverageType.Milk);
                SetItemHue(index, 0x47E);

                #region TOL
                if (Core.TOL)
                {
                    index = AddCraft(typeof(ChocolateNutcracker), 1080001, 1156390, 15.0, 100.0, typeof(SweetCocoaButter), 1156401, 1, 1044253);
                    AddRes(index, typeof(SweetCocoaButter), 1124032, 1, 1156402);
                    AddRes(index, typeof(CocoaLiquor), 1079999, 1, 1044253);
                    AddRecipe(index, (int)CookRecipes.DarkChocolateNutcracker);
                    SetData(index, ChocolateNutcracker.ChocolateType.Dark);

                    index = AddCraft(typeof(ChocolateNutcracker), 1080001, 1156391, 32.5, 107.5, typeof(SweetCocoaButter), 1156401, 1, 1044253);
                    AddRes(index, typeof(SweetCocoaButter), 1124032, 1, 1156402);
                    AddRes(index, typeof(CocoaLiquor), 1079999, 1, 1044253);
                    AddRecipe(index, (int)CookRecipes.MilkChocolateNutcracker);
                    SetData(index, ChocolateNutcracker.ChocolateType.Milk);

                    index = AddCraft(typeof(ChocolateNutcracker), 1080001, 1156392, 42.5, 127.5, typeof(SweetCocoaButter), 1156401, 1, 1044253);
                    AddRes(index, typeof(SweetCocoaButter), 1124032, 1, 1156402);
                    AddRes(index, typeof(CocoaLiquor), 1079999, 1, 1044253);
                    AddRecipe(index, (int)CookRecipes.WhiteChocolateNutcracker);
                    SetData(index, ChocolateNutcracker.ChocolateType.White);
                }
                #endregion
            }
            #endregion

            #region Fish Pies
            if (Core.SA)
            {
                index = AddCraft(typeof(GreatBarracudaPie), 1116340, 1116214, 51.0, 110.0, typeof(GreatBarracudaSteak), 1116298, 1, 1044253);
                AddRes(index, typeof(MentoSeasoning), 1116299, 1, 1044253);
                AddRes(index, typeof(ZoogiFungus), 1029911, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(GiantKoiPie), 1116340, 1116216, 51.0, 110.0, typeof(GiantKoiSteak), 1044253, 1, 1044253);
                AddRes(index, typeof(MentoSeasoning), 1116299, 1, 1044253);
                AddRes(index, typeof(WoodenBowlOfPeas), 1025628, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(FireFishPie), 1116340, 1116217, 45.0, 105.0, typeof(FireFishSteak), 1116307, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(Carrot), 1023191, 1, 1044253);
                AddRes(index, typeof(SamuelsSecretSauce), 1116338, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(StoneCrabPie), 1116340, 1116227, 45.0, 105.0, typeof(StoneCrabMeat), 1116317, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(Cabbage), 1023195, 1, 1044253);
                AddRes(index, typeof(SamuelsSecretSauce), 1116338, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(BlueLobsterPie), 1116340, 1116228, 45.0, 105.0, typeof(BlueLobsterMeat), 1116318, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(TribalBerry), 1040001, 1, 1044253);
                AddRes(index, typeof(SamuelsSecretSauce), 1116338, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(ReaperFishPie), 1116340, 1116218, 45.0, 105.0, typeof(ReaperFishSteak), 1116308, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(Pumpkin), 1023178, 1, 1044253);
                AddRes(index, typeof(SamuelsSecretSauce), 1116338, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(CrystalFishPie), 1116340, 1116219, 45.0, 105.0, typeof(CrystalFishSteak), 1116309, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(Apple), 1022512, 1, 1044253);
                AddRes(index, typeof(SamuelsSecretSauce), 1116338, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(BullFishPie), 1116340, 1116220, 45.0, 105.0, typeof(BullFishSteak), 1116310, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(Squash), 1023186, 1, 1044253);
                AddRes(index, typeof(MentoSeasoning), 1116299, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(SummerDragonfishPie), 1116340, 1116221, 45.0, 105.0, typeof(SummerDragonfishSteak), 1116311, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(Onion), 1023182, 1, 1044253);
                AddRes(index, typeof(MentoSeasoning), 1116299, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(FairySalmonPie), 1116340, 1116222, 55.0, 105.0, typeof(FairySalmonSteak), 1116312, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(EarOfCorn), 1023199, 1, 1044253);
                AddRes(index, typeof(DarkTruffle), 1116300, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(LavaFishPie), 1116340, 1116223, 65.0, 105.0, typeof(LavaFishSteak), 1116313, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(CheeseWheel), 1044486, 1, 1044253);
                AddRes(index, typeof(DarkTruffle), 1116300, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(AutumnDragonfishPie), 1116340, 1116224, 65.0, 105.0, typeof(AutumnDragonfishSteak), 1116314, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(Pear), 1022452, 1, 1044253);
                AddRes(index, typeof(MentoSeasoning), 1116299, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(SpiderCrabPie), 1116340, 1116229, 65.0, 105.0, typeof(SpiderCrabMeat), 1116320, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(Lettuce), 1023184, 1, 1044253);
                AddRes(index, typeof(MentoSeasoning), 1116299, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(YellowtailBarracudaPie), 1116340, 1116098, 65.0, 105.0, typeof(YellowtailBarracudaSteak), 1116301, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1022503, 1, 1044253);
                AddRes(index, typeof(MentoSeasoning), 1116299, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(HolyMackerelPie), 1116340, 1116225, 65.0, 105.0, typeof(HolyMackerelSteak), 1116315, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(JarHoney), 1022540, 1, 1044253);
                AddRes(index, typeof(MentoSeasoning), 1116299, 1, 1044253);
                SetNeedOven(index, true);

                index = AddCraft(typeof(UnicornFishPie), 1116340, 1116226, 65.0, 105.0, typeof(UnicornFishSteak), 1116316, 1, 1044253);
                AddRes(index, typeof(Dough), 1024157, 1, 1044253);
                AddRes(index, typeof(FreshGinger), 1031235, 1, 1044253);
                AddRes(index, typeof(MentoSeasoning), 1116299, 1, 1044253);
                SetNeedOven(index, true);
            }
            #endregion

            #region Bevereages
            index = AddCraft(typeof(CoffeeMug), 1155736, 1155737, 10.0, 28.58, typeof(CoffeeGrounds), 1155735, 1, 1155734);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            SetBeverageType(index, BeverageType.Water);
            SetNeedMaker(index, true);
            ForceNonExceptional(index);

            index = AddCraft(typeof(BasketOfGreenTeaMug), 1155736, 1030315, 10.0, 28.58, typeof(GreenTeaBasket), 1155735, 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            SetBeverageType(index, BeverageType.Water);
            SetNeedMaker(index, true);
            ForceNonExceptional(index);

            index = AddCraft(typeof(HotCocoaMug), 1155736, 1155738, 10.0, 28.58, typeof(CocoaLiquor), 1080007, 1, 1080006);
            AddRes(index, typeof(SackOfSugar), 1080003, 1, 1080002);
            AddRes(index, typeof(BaseBeverage), 1080011, 1, 1080010);
            SetBeverageType(index, BeverageType.Milk);
            SetNeedMaker(index, true);
            ForceNonExceptional(index);
            #endregion

            index = AddCraft(typeof(SackFlour), 1044495, 1024153, 0.0, 100.0, typeof(WheatSheaf), 1044489, 2, 1044490);
            SetNeedMill(index, true);
            index = AddCraft(typeof(Dough), 1044495, 1024157, 0.0, 100.0, typeof(SackFlour), 1044468, 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            index = AddCraft(typeof(SweetDough), 1044495, 1041340, 0.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(JarHoney), "Jar of Honey", 1, "You need a Jar of Honey");
            index = AddCraft(typeof(Batter), 1044495, "Batter", 0.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Eggs), "Eggs", 1, 1044253);
            AddRes(index, typeof(JarHoney), 1044472, 1, 1044253);
            index = AddCraft(typeof(Butter), 1044495, "Butter", 0.0, 100.0, typeof(Cream), "Cream", 1, 1044253);
            index = AddCraft(typeof(Cream), 1044495, "Cream", 0.0, 100.0, typeof(BaseBeverage), "Milk", 1, 1044253);
            index = AddCraft(typeof(CookingOil), 1044495, "Cooking Oil", 0.0, 100.0, typeof(Peanut), "Peanut", 10, 1044253);
            index = AddCraft(typeof(Vinegar), 1044495, "Vinegar", 0.0, 100.0, typeof(Apple), "apples", 5, 1044253);
            AddRes(index, typeof(BottleOfWine), "Wine", 1, 1044253);
            index = AddCraft(typeof(CakeMix), 1044495, 1041002, 0.0, 100.0, typeof(SackFlour), 1044468, 1, 1044253);
            AddRes(index, typeof(SweetDough), 1044475, 1, 1044253);
            index = AddCraft(typeof(CookieMix), 1044495, 1024159, 0.0, 100.0, typeof(JarHoney), 1044472, 1, 1044253);
            AddRes(index, typeof(SweetDough), 1044475, 1, 1044253);

            index = AddCraft(typeof(GroundBeef), 1044496, "Ground Beef", 0.0, 100.0, typeof(BeefHock), "Beef Hock", 1, 1044253);
            index = AddCraft(typeof(GroundPork), 1044496, "Ground Pork", 0.0, 100.0, typeof(PorkHock), "Pork Hock", 1, 1044253);
            index = AddCraft(typeof(SlicedTurkey), 1044496, "Sliced Turkey", 0.0, 100.0, typeof(TurkeyHock), "Turkey Hock", 1, 1044253);
            index = AddCraft(typeof(PastaNoodles), 1044496, "Pasta Noodles", 0.0, 100.0, typeof(SackFlour), "Sack of Flour", 1, 1044253);
            AddRes(index, typeof(Eggs), "eggs", 5, 1044253);
            index = AddCraft(typeof(PeanutButter), 1044496, "Peanut Butter", 0.0, 100.0, typeof(Peanut), "Peanuts", 30, 1044253);
            index = AddCraft(typeof(Tortilla), 1044496, "Tortilla", 0.0, 100.0, typeof(BagOfCornmeal), "Bag of Cornmeal", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(UnbakedQuiche), 1044496, 1041339, 1.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Eggs), 1044477, 1, 1044253);
            index = AddCraft(typeof(UnbakedMeatPie), 1044496, 1041338, 1.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(RawRibs), 1044482, 1, 1044253);
            index = AddCraft(typeof(UncookedSausagePizza), 1044496, 1041337, 1.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Sausage), 1044483, 1, 1044253);
            index = AddCraft(typeof(UncookedCheesePizza), 1044496, 1041341, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(CheeseWheel), 1044486, 1, 1044253);
            index = AddCraft(typeof(UnbakedFruitPie), 1044496, 1041334, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Pear), 1044481, 1, 1044253);
            index = AddCraft(typeof(UnbakedPeachCobbler), 1044496, 1041335, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Peach), 1044480, 1, 1044253);
            index = AddCraft(typeof(UnbakedApplePie), 1044496, 1041336, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Apple), 1044479, 1, 1044253);
            index = AddCraft(typeof(UnbakedPumpkinPie), 1044496, 1041342, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Pumpkin), 1044484, 1, 1044253);
            index = AddCraft(typeof(GreenTea), 1044496, 1030315, 60.0, 130.0, typeof(GreenTeaBasket), 1030316, 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            //SetNeededExpansion(index, Expansion.SE);
            SetNeedOven(index, true);
            index = AddCraft(typeof(WasabiClumps), 1044496, 1029451, 50.0, 120.0, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRes(index, typeof(WoodenBowlPea), 1025633, 3, 1044253);
            //SetNeededExpansion(index, Expansion.SE);
            index = AddCraft(typeof(SushiRolls), 1044496, 1030303, 70.0, 120.0, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRes(index, typeof(RawFishSteak), 1044476, 5, 1044253);
            AddRes(index, typeof(RawShrimp), "Raw Shrimp", 5, "You need more Raw Shrimp");
            //SetNeededExpansion(index, Expansion.SE);
            index = AddCraft(typeof(SushiPlatter), 1044496, 1030305, 70.0, 120.0, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRes(index, typeof(RawFishSteak), 1044476, 5, 1044253);
            AddRes(index, typeof(RawShrimp), "Raw Shrimp", 5, "You need more Raw Shrimp");
            //SetNeededExpansion(index, Expansion.SE);
            index = AddCraft(typeof(TribalPaint), 1044496, 1040000, 60.0, 80.0, typeof(SackFlour), 1044468, 1, 1044253);
            AddRes(index, typeof(TribalBerry), 1046460, 1, 1044253);
            index = AddCraft(typeof(EggBomb), 1044496, 1030249, 0.0, 120.0, typeof(Eggs), 1044477, 1, 1044253);
            AddRes(index, typeof(SackFlour), 1044468, 3, 1044253);
            //SetNeededExpansion(index, Expansion.SE);
            index = AddCraft(typeof(DriedOnions), 1044496, "Dried Onions", 10.0, 100.0, typeof(Onion), "Onions", 5, 1044253);
            index = AddCraft(typeof(DriedHerbs), 1044496, "Dried Herbs", 10.0, 100.0, typeof(Garlic), "Garlic", 2, 1044253);
            AddRes(index, typeof(Ginseng), "Ginseng", 2, 1044253);
            AddRes(index, typeof(TanGinger), "Tan Ginger", 2, "You need more tan ginger");
            index = AddCraft(typeof(BasketOfHerbs), 1044496, "Basket of Herbs", 10.0, 100.0, typeof(DriedHerbs), "Dried Herbs", 1, 1044253);
            AddRes(index, typeof(DriedOnions), "Dried Onions", 1, 1044253);

            index = AddCraft(typeof(BarbecueSauce), "Sauces", "Barbecue Sauce", 10.0, 100.0, typeof(Tomato), "Tomato", 1, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            AddRes(index, typeof(BasketOfHerbs), "Herbs", 1, 1044253);
            index = AddCraft(typeof(CheeseSauce), "Sauces", "Cheese Sauce", 10.0, 100.0, typeof(Butter), "Butter", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), "Milk", 1, 1044253);
            AddRes(index, typeof(CheeseWheel), "Cheese Wheel", 1, 1044253);
            index = AddCraft(typeof(EnchiladaSauce), "Sauces", "Enchilada Sauce", 10.0, 100.0, typeof(Tomato), "Tomato", 1, 1044253);
            AddRes(index, typeof(ChiliPepper), "Chili Pepper", 1, 1044253);
            AddRes(index, typeof(BasketOfHerbs), "Herbs", 1, 1044253);
            index = AddCraft(typeof(Gravy), "Sauces", "Gravy", 10.0, 100.0, typeof(Dough), 1044469, 2, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRes(index, typeof(BasketOfHerbs), "Herbs", 1, 1044253);
            index = AddCraft(typeof(HotSauce), "Sauces", "Hot Sauce", 10.0, 100.0, typeof(Tomato), "Tomato", 2, 1044253);
            AddRes(index, typeof(ChiliPepper), "Chili Pepper", 3, 1044253);
            AddRes(index, typeof(BasketOfHerbs), "Herbs", 1, 1044253);
            index = AddCraft(typeof(SoySauce), "Sauces", "Soy Sauce", 10.0, 100.0, typeof(BagOfSoy), "Bag of Soy", 1, 1044253);
            AddRes(index, typeof(BagOfSugar), "Bag of Sugar", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            index = AddCraft(typeof(Teriyaki), "Sauces", "Teriyaki", 10.0, 100.0, typeof(SoySauce), "Soy Sauce", 1, 1044253);
            AddRes(index, typeof(BottleOfWine), "Bottle of Wine", 1, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            index = AddCraft(typeof(TomatoSauce), "Sauces", "Tomato Sauce", 10.0, 100.0, typeof(Tomato), "Tomato", 3, 1044253);
            AddRes(index, typeof(BasketOfHerbs), "Herbs", 1, 1044253);

            index = AddCraft(typeof(CakeMix), "Mixes", "Cake Mix", 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(CookingOil), "Cooking Oil", 1, 1044253);
            AddRes(index, typeof(BagOfSugar), "Bag of Sugar", 1, 1044253);
            index = AddCraft(typeof(CookieMix), "Mixes", "Cookie Mix", 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Butter), "Butter", 1, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            index = AddCraft(typeof(AsianVegMix), "Mixes", "Asian Vegetable Mix", 10.0, 100.0, typeof(Cabbage), "Cabbage", 1, 1044253);
            AddRes(index, typeof(Onion), "Onion", 1, 1044253);
            AddRes(index, typeof(RedMushroom), "Red Mushroom", 1, "You need a Red Mushroom");
            AddRes(index, typeof(Carrot), "Carrot", 1, 1044253);
            index = AddCraft(typeof(ChocolateMix), "Mixes", "Chocolate Mix", 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(BagOfCocoa), "Bag of Cocoa", 1, 1044253);
            AddRes(index, typeof(BagOfSugar), "Bag of Sugar", 1, 1044253);
            index = AddCraft(typeof(MixedVegetables), "Mixes", "Mixed Vegetables", 10.0, 100.0, typeof(Potato), "Potato", 2, 1044253);
            AddRes(index, typeof(Carrot), "Carrot", 1, 1044253);
            AddRes(index, typeof(Celery), "Celery", 1, 1044253);
            AddRes(index, typeof(Onion), "Onion", 1, 1044253);
            index = AddCraft(typeof(PieMix), "Mixes", "Pie Mix", 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Butter), "Butter", 1, 1044253);
            index = AddCraft(typeof(PizzaCrust), "Mixes", "Pizza Crust", 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            index = AddCraft(typeof(WaffleMix), "Mixes", "Waffle Mix", 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Eggs), "Eggs", 2, 1044253);
            AddRes(index, typeof(CookingOil), "cooking oil", 1, 1044253);

            index = AddCraft(typeof(BowlCornFlakes), 1044497, "Bowl of Corn Flakes", 15.0, 100.0, typeof(BagOfCornmeal), "Bag of Cornmeal", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            index = AddCraft(typeof(BowlRiceKrisps), 1044497, "Bowl of Rice Krisps", 15.0, 100.0, typeof(BagOfRicemeal), "Bag of Ricemeal", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            index = AddCraft(typeof(FruitBasket), 1044497, "Fruit Basket", 10.0, 100.0, typeof(Apple), "Apple", 5, 1044253);
            AddRes(index, typeof(Peach), "Peach", 5, 1044253);
            AddRes(index, typeof(Pear), "Pear", 5, 1044253);
            AddRes(index, typeof(Cherry), "Cherries", 5, 1044253);
            index = AddCraft(typeof(Tofu), 1044497, "Tofu", 10.0, 100.0, typeof(BagOfSoy), "Bag of Soy", 1, 1044253);

            index = AddCraft(typeof(BreadLoaf), 1044497, 1024156, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(Cookies), 1044497, 1025643, 10.0, 100.0, typeof(CookieMix), 1044474, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(Cake), 1044497, 1022537, 10.0, 100.0, typeof(CakeMix), 1044471, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(Muffins), 1044497, 1022539, 10.0, 100.0, typeof(SweetDough), 1044475, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(Quiche), 1044497, 1041345, 10.0, 100.0, typeof(UnbakedQuiche), 1044518, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(MeatPie), 1044497, 1041347, 10.0, 100.0, typeof(UnbakedMeatPie), 1044519, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(SausagePizza), 1044497, 1044517, 10.0, 100.0, typeof(UncookedSausagePizza), 1044520, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(CheesePizza), 1044497, 1044516, 10.0, 100.0, typeof(UncookedCheesePizza), 1044521, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(FruitPie), 1044497, 1041346, 10.0, 100.0, typeof(UnbakedFruitPie), 1044522, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(PeachCobbler), 1044497, 1041344, 10.0, 100.0, typeof(UnbakedPeachCobbler), 1044523, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(ApplePie), 1044497, 1041343, 10.0, 100.0, typeof(UnbakedApplePie), 1044524, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(PumpkinPie), 1044497, 1041348, 10.0, 100.0, typeof(UnbakedPumpkinPie), 1046461, 1, 1044253);
            SetNeedOven(index, true);
            index = AddCraft(typeof(MisoSoup), 1044497, 1030317, 50.0, 110.0, typeof(RawFishSteak), 1044476, 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            //SetNeededExpansion(index, Expansion.SE);
            SetNeedOven(index, true);
            index = AddCraft(typeof(WhiteMisoSoup), 1044497, 1030318, 50.0, 110.0, typeof(RawFishSteak), 1044476, 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            //SetNeededExpansion(index, Expansion.SE);
            SetNeedOven(index, true);
            index = AddCraft(typeof(RedMisoSoup), 1044497, 1030319, 50.0, 110.0, typeof(RawFishSteak), 1044476, 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            //SetNeededExpansion(index, Expansion.SE);
            SetNeedOven(index, true);
            index = AddCraft(typeof(AwaseMisoSoup), 1044497, 1030320, 50.0, 110.0, typeof(RawFishSteak), 1044476, 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            //SetNeededExpansion(index, Expansion.SE);
            SetNeedOven(index, true);

            index = AddCraft(typeof(CookedBird), 1044498, 1022487, 30.0, 100.0, typeof(RawBird), 1044470, 1, 1044253);
            SetNeedHeat(index, true);
            index = AddCraft(typeof(ChickenLeg), 1044498, 1025640, 10.0, 75.0, typeof(RawChickenLeg), 1044473, 1, 1044253);
            SetNeedHeat(index, true);
            index = AddCraft(typeof(FriedEggs), 1044498, 1022486, 1.0, 50.0, typeof(Eggs), 1044477, 1, 1044253);
            SetNeedHeat(index, true);
            index = AddCraft(typeof(LambLeg), 1044498, 1025642, 30.0, 100.0, typeof(RawLambLeg), 1044478, 1, 1044253);
            SetNeedHeat(index, true);
            index = AddCraft(typeof(Ribs), 1044498, 1022546, 20.0, 100.0, typeof(RawRibs), 1044485, 1, 1044253);
            SetNeedHeat(index, true);
            index = AddCraft(typeof(CookedSteak), 1044498, "Steak", 50.0, 100.0, typeof(RawSteak), "Raw Steak", 1, "You need more Raw Steak");
            SetNeedHeat(index, true);
            index = AddCraft(typeof(HamSlices), 1044498, "Ham Slices", 20.0, 100.0, typeof(RawHamSlices), "Raw Ham Slice", 1, "You need more Raw Ham Slice");
            SetNeedHeat(index, true);
            index = AddCraft(typeof(RoastHam), 1044498, "Roast Ham", 30.0, 100.0, typeof(RawHam), "Raw Ham", 1, "You need more Raw Ham");
            SetNeedHeat(index, true);
            index = AddCraft(typeof(FishSteak), 1044498, 1022427, 30.0, 100.0, typeof(RawFishSteak), 1044476, 1, 1044253);
            SetNeedHeat(index, true);
            index = AddCraft(typeof(HalibutFishSteak), 1044498, "Halibut Fish Steak", 30.0, 120.0, typeof(RawHalibutSteak), "Raw Halibut Fish Steak", 1, "you need more Raw Halibut Fish Steaks");
            SetNeedHeat(index, true);
            index = AddCraft(typeof(FlukeFishSteak), 1044498, "Fluke Fish Steak", 30.0, 120.0, typeof(RawFlukeSteak), "Raw Fluke Fish Steak", 1, "you need more Raw Fluke Fish Steaks");
            SetNeedHeat(index, true);
            index = AddCraft(typeof(MahiFishSteak), 1044498, "Mahi Fish Steak", 30.0, 120.0, typeof(RawMahiSteak), "Raw Mahi Fish Steak", 1, "you need more Raw Mahi Fish Steaks");
            SetNeedHeat(index, true);
            index = AddCraft(typeof(SalmonFishSteak), 1044498, "Salmon Fish Steak", 30.0, 120.0, typeof(RawSalmonSteak), "Raw Salmon Fish Steak", 1, "you need more Raw Salmon Fish Steaks");
            SetNeedHeat(index, true);
            index = AddCraft(typeof(RedSnapperFishSteak), 1044498, "Red Snapper Fish Steak", 30.0, 120.0, typeof(RawRedSnapperSteak), "Raw Red Snapper Fish Steak", 1, "you need more Raw Red Snapper Fish Steaks");
            SetNeedHeat(index, true);
            index = AddCraft(typeof(ParrotFishSteak), 1044498, "Parrot Fish Steak", 30.0, 120.0, typeof(RawParrotFishSteak), "Raw Parrot Fish Steak", 1, "you need more Raw Parrot Fish Steaks");
            SetNeedHeat(index, true);
            index = AddCraft(typeof(TroutFishSteak), 1044498, "Trout Fish Steak", 30.0, 120.0, typeof(RawTroutSteak), "Raw Trout Fish Steak", 1, "you need more Raw Trout Fish Steaks");
            SetNeedHeat(index, true);
            index = AddCraft(typeof(CookedShrimp), 1044498, "Cooked Shrimp", 30.0, 120.0, typeof(RawShrimp), "Raw Shrimp", 1, "you need more Raw Shrimp");
            SetNeedHeat(index, true);

            //index = AddCraft(typeof(Pulp), "Other", "100 Pulp", 50.0, 100.0, typeof(Log), "Log", 200, 1044253);
            //AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            //SetNeedOven(index, true);



        }
    }
}
