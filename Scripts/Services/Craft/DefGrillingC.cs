using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefGrilling : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Culinaria;	}
		}

		public override int GumpTitleNumber
		{
			get { return 0; }
		}

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>GRILLING MENU</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefGrilling();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0;
		}

		private DefGrilling() : base( 1, 1, 1.25 )
		{
		}

		public override int CanCraft( Mobile from, ITool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038;
			else if ( !BaseTool.CheckAccessible((Item)tool, from))
				return 1044263;

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 );

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043;
				else
					return 1044157;
			}
			else
			{
				if ( quality == 0 )
					return 502785;
				else if ( makersMark && quality == 2 )
					return 1044156;
				else if ( quality == 2 )
					return 1044155;
				else
					return 1044154;
			}
		}









		public override void InitCraftList()
		{
			int index = -1;

			index = AddCraft( typeof( Pancakes ), "Desejum", "Massa com mel", 10.0, 100.0, typeof( Batter ), "Massa", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Mel", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Waffles ), "desjejum", "Massa frita com mel", 10.0, 100.0, typeof( WaffleMix ), "Mistura de Massa", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Mel", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( FriedEggs ), "desjejum", "Ovos fritos", 10.0, 100.0, typeof( Eggs ), "Ovo", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Bacon ), "desjejum", "Bacon", 10.0, 100.0, typeof( RawBacon ), "Bacon Cru", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Ribs ), "Churrasco", "Costela", 10.0, 100.0, typeof( RawRibs ), "Costelas Crua", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CookedBird ), "Churrasco", "Peito de codorna", 10.0, 100.0, typeof( RawBird ), "Codorna Crua", 1, 1044253 );
			SetNeedOven( index, true );

            index = AddCraft(typeof(CookedBird), "Churrasco", "Coxa de Frango", 10.0, 100.0, typeof( RawChickenLeg ), "Coxa de Frango Cru", 1, 1044253);
            SetNeedOven(index, true);

          
			index = AddCraft( typeof( FishSteak ), "Churrasco", "Filé de Frango", 10.0, 100.0, typeof( RawFishSteak ), "Filé de Peixe Cru", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( LambLeg ), "Churrasco", "Coxa de Cordeiro", 10.0, 100.0, typeof( RawLambLeg ), "Coxa de Carneiro Crua", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BeefBBQRibs ), "Jantar", "Costelas", 20.0, 100.0, typeof( RawRibs ), "Costelas Cruas", 1, 1044253 );
			AddRes( index, typeof( SoySauce ), "Molho de Soja", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BeefBroccoli ), "Jantar", "Bife com Brócolis", 20.0, 100.0, typeof( GroundBeef ), "Carne Moída", 1, 1044253 );
			AddRes( index, typeof( Broccoli ), "Brócolis", 4, 1044253 );
			AddRes( index, typeof( SoySauce ), "Molho de Soja", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ChoChoBeef ), "Jantar", "Comida picante", 30.0, 100.0, typeof( GroundBeef ), "Carne Moída", 1, 1044253 );
			AddRes( index, typeof( Teriyaki ), "Teriyaki", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BeefSnowpeas ), "Jantar", "Carne com Ervilha", 30.0, 100.0, typeof( GroundBeef ), "Carne Moída", 1, 1044253 );
			AddRes( index, typeof( SnowPeas ), "Ervilha", 4, 1044253 );
			AddRes( index, typeof( SoySauce ), "Molho de Soja", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Hamburger ), "Jantar", "Pedaço de Carne Saborosa", 35.0, 100.0, typeof( GroundBeef ), "Carne Moída", 1, 1044253 );
			AddRes( index, typeof( BreadLoaf ), "Pão", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );




			index = AddCraft( typeof( BeefLoMein ), "Jantar", "Massa com Carne e Vegetais", 40.0, 100.0, typeof( GroundBeef ), "Carne Moída", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Vegetais Mistos Cozidos", 1, 1044253 );
			AddRes( index, typeof( PastaNoodles ), "Massa", 2, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BeefStirfry ), "Jantar", "Carne com Vegetais", 45.0, 100.0, typeof( GroundBeef ), "Carne Moída", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Vegetais Mistos Cozidos", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ChickenStirfry ), "Jantar", "Codorna com Vegetais", 45.0, 100.0, typeof( RawBird ), "Codorna Cru", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Vegetais Mistos Cozidos", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( MooShuPork ), "Jantar", "Porco com Vegetais", 55.0, 100.0, typeof( GroundPork ), "Carne de Porco Moída", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Vegetais Mistos Cozidos", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( MoPoTofu ), "Jantar", "Queijo Vegetal Temperado", 60.0, 100.0, typeof( Tofu ), "Queijo Vegetal", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Vegetais Mistos Cozidos", 1, 1044253 );
			AddRes( index, typeof( ChiliPepper ), "Pimenta Forte", 3, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PorkStirfry ), "Jantar", "Pork Stirfry", 65.0, 100.0, typeof( GroundPork ), "Ground Pork", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Vegetais Mistos Cozidos", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( SweetSourChicken ), "Jantar", "Frango Agridoce", 70.0, 100.0, typeof( RawBird ), "Cordona Cru", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Mel", 1, 1044253 );
			AddRes( index, typeof( SoySauce ), "Molho de Soja", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( SweetSourPork ), "Jantar", "Porco Agridoce", 70.0, 100.0, typeof( GroundPork ), "Carne de Porco Moída", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Mel", 1, 1044253 );
			AddRes( index, typeof( SoySauce ), "Molho de Soja", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BaconAndEgg ), "Jantar", "Bacon com ovos", 75.0, 100.0, typeof( Eggs ), "Ovos", 2, 1044253 );
			AddRes( index, typeof ( RawBacon ), "Bacon Cru", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Prato", 1, "Você precisa de um prato!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( GarlicBread ), "Outras Comidas", "Pão com alho", 75.0, 100.0, typeof( BreadLoaf ), "Pão", 1, 1044253 );
			AddRes( index, typeof( Butter ), "Mantega", 1, 1044253 );
			AddRes( index, typeof( Garlic ), "Alho", 2, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Ervas", 1, 1044253 );

			index = AddCraft( typeof( GrilledHam ), "Outras Comidas", "Presunto Grelhado", 80.0, 100.0, typeof( RawHamSlices ), "Presunto Cru", 1, 1044253 );

			index = AddCraft( typeof( Sausage ), "Outras Comidas", "Salsicha", 90.0, 100.0, typeof( GroundBeef ), "Carne Moída", 1, 1044253 );
			AddRes( index, typeof( GroundPork ), "Carne Moída de Porco", 1, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Ervas", 1, 1044253 );

			index = AddCraft( typeof( Hotwings ), "Outras Comidas", "Frango frito", 100.0, 100.0, typeof ( RawChickenLeg ), "Coxa de Frango Crua", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Mel", 1, 1044253 );
			AddRes( index, typeof( HotSauce ), "Molho Picante", 1, 1044253 );

			index = AddCraft( typeof( PotatoFries ), "Outras Comidas", "Batata Assada", 110.0, 100.0, typeof( Potato ), "Batata", 3, 1044253 );
			AddRes( index, typeof( Onion ), "Cebola", 1, 1044253 );
			AddRes( index, typeof( Butter ), "Mantega", 1, 1044253 );

			index = AddCraft( typeof( Taco ), "Outras Comidas", "Pão de Milho", 120.0, 100.0, typeof( GroundBeef ), "Carne Moída", 1, 1044253 );
			AddRes( index, typeof( Tortilla ), "Carne moída temperada", 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ), "Roda de Queijo", 1, 1044253 );

		}
	}
}
