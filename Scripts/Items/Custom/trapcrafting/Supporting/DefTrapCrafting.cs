using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	public class DefTrapCrafting : CraftSystem
	{
		public override SkillName MainSkill
		{
			get{ return SkillName.Mecanica; }
		}

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>MENU DE CONSTRUÇÃO DE ARMADILHAS</CENTER></basefont>"; } 
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTrapCrafting();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefTrapCrafting() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 )
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

        public override void PlayCraftEffect( Mobile from )
		{
            from.PlaySound( 0x241 ); 
		}

		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 0.7 ) )
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.PlaySound( 0x1C6 );
			}
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
				from.PlaySound( 0x1c6 );

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

            #region Components
            index = AddCraft(typeof( TrapFrame ), "Componente", "Armação de armadilhas", 20.0, 40.0, typeof( IronIngot ), "Iron Ingot", 2, "You need more iron ingots.");
            AddRes(index, typeof(Leather), "Leather", 2, "You need more leather");
            AddRes(index, typeof(Board), "Board", 1, "You need a board");

            index = AddCraft(typeof( TrapSpike ), "Componente", "Cravo para armadilhas", 25.0, 45.0, typeof(Bolt), "Crossbow Bolt", 1, "You need a crossbow bolt.");
            AddRes(index, typeof( Springs ), "Springs", 1, "You need some springs");

            index = AddCraft(typeof( TrapCrystalTrigger ), "Componente", "Trap Crystal", 60.0, 80.0, typeof( CrystalisedEnergy ), "Crystalised Energy", 1, "You need a piece of crystalised energy.");
            AddRes(index, typeof( DullCopperIngot ), "Dull Copper Ingot", 2, "You need more dull copper ingots");
            AddRes(index, typeof( Springs ), "Springs", 2, "You need more springs");

            index = AddCraft(typeof( TrapCrystalSensor ), "Componente", "Sensor de cristal", 90.0, 110.0, typeof( GazerEye ), "Gazer Eye", 1, "You need a gazer eye.");
            AddRes(index, typeof( TrapCrystalTrigger ), "Crystal Trigger", 1, "You need a crystal trigger.");
            #endregion

            #region Explosive Traps
            index = AddCraft(typeof(ExplosiveLesserTrap), "Armadilha Explosiva", "Armadilha explosiva menor", 35.0, 55.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(LesserExplosionPotion), "Lesser Explosion Potion", 1, "You need an explosion potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(ExplosiveRegularTrap), "Armadilha Explosiva", "Armadilha explosiva", 50.0, 70.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(ExplosionPotion), "Explosion Potion", 1, "You need an explosion potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(ExplosiveGreaterTrap), "Armadilha Explosiva", "Armadilha explosiva maior", 65.0, 85.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(GreaterExplosionPotion), "Greater Explosion Potion", 1, "You need an explosion potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");
            #endregion

            #region Freezing Traps
            index = AddCraft(typeof(FreezingLesserTrap), "Armadilha de Congelamento", "Armadilha congelamento menor", 50.0, 70.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(DryIce), "Dry Ice", 1, "You need dry ice.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(FreezingRegularTrap), "Armadilha de Congelamento", "Armadilha congelamento", 65.0, 85.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(DryIce), "Dry Ice", 2, "You need more dry ice.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(FreezingGreaterTrap), "Armadilha de Congelamento", "Armadilha congelamento maior", 90.0, 110.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(DryIce), "Dry Ice", 3, "You need more dry ice.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");
            #endregion

            #region Lightning Traps
            index = AddCraft(typeof(LightningLesserTrap), "Armadilha de Raio", "Armadilha raio menor", 45.0, 65.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(BottledLightning), "Bottled Lightning", 1, "You need bottled lightning.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(LightningRegularTrap), "Armadilha de Raio", "Armadilha raio", 60.0, 80.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(BottledLightning), "Bottled Lightning", 2, "You need more bottled lightning.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(LightningGreaterTrap), "Armadilha de Raio", "Armadilha raio maior", 75.0, 95.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(BottledLightning), "Bottled Lightning", 3, "You need more bottled lightning.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");
            #endregion

            #region Paralysis Traps
            index = AddCraft(typeof(ParalysisLesserTrap), "Armadilha de Paralisia", "Armadilha de paralisia menor", 40.0, 60.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(GiantSpiderVenom), "Giant Spider Venom Gland", 1, "You need a giant spider venom sac.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(ParalysisRegularTrap), "Armadilha de Paralisia", "Armadilha de paralisia", 55.0, 75.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(GiantSpiderVenom), "Giant Spider Venom Gland", 2, "You need more giant spider venom sacs.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears.");

            index = AddCraft(typeof(ParalysisGreaterTrap), "Armadilha de Paralisia", "Armadilha de paralisia maior", 70.0, 90.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(GiantSpiderVenom), "Giant Spider Venom Gland", 3, "You need more giant spider venom sacs.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears.");
            #endregion

            #region Poison Dart Traps
            index = AddCraft(typeof(PoisonLesserDartTrap), "Dardos Envenenados", "Armadilha de dardo venenoso menor", 25.0, 45.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(LesserPoisonPotion), "Lesser Poison Potion", 1, "You need a lesser poison potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(PoisonRegularDartTrap), "Dardos Envenenados", "Armadilha de dardo venenoso", 40.0, 60.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(PoisonPotion), "Poison Potion", 1, "You need a poison potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(PoisonGreaterDartTrap), "Dardos Envenenados", "Armadilha de dardo venenoso maior", 55.0, 75.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(GreaterPoisonPotion), "Greater Poison Potion", 1, "You need a greater poison potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(PoisonDeadlyDartTrap), "Dardos Envenenados", "Armadilha de dardo venenoso mortal", 70.0, 90.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(DeadlyPoisonPotion), "Deadly Poison Potion", 1, "You need a deadly poison potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");
            #endregion

            #region
            //Armadilhas para Baús

            // Dart Trap
            index = AddCraft(typeof(DartTrapCraft), "Armadilhas de Baú", 1024396, 30.0, 80.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddRes(index, typeof(Bolt), 1044570, 1, 1044253);

            // Poison Trap
            index = AddCraft(typeof(PoisonTrapCraft), "Armadilhas de Baú", 1044593, 30.0, 80.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddRes(index, typeof(BasePoisonPotion), 1044571, 1, 1044253);

            // Explosion Trap
            index = AddCraft(typeof(ExplosionTrapCraft), "Armadilhas de Baú", 1044597, 55.0, 105.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddRes(index, typeof(BaseExplosionPotion), 1044569, 1, 1044253);
            #endregion

            #region
            //armadilhas para portas
            index = this.AddCraft(typeof(DoorArrowTrapInstaller), "Armadilhas de Portas", "Armadilha de Flechas para Portas", 75.0, 95.0, typeof(IronIngot), "iron ingot", 2, 1044253);
            this.AddRes(index, typeof(Springs), "springs", 1, 1044253);
            this.AddRes(index, typeof(Arrow), "arrows", 8, 1044253);

            index = this.AddCraft(typeof(DoorDartTrapInstaller), "Armadilhas de Portas", "Armadilha de Dardos para Portas", 65.0, 85.0, typeof(IronIngot), "iron ingot", 2, 1044253);
            this.AddRes(index, typeof(Springs), "springs", 1, 1044253);
            this.AddRes(index, typeof(Bolt), "bolts", 8, 1044253);

            index = this.AddCraft(typeof(DoorExplosionTrapInstaller), "Armadilhas de Portas", "Armadilha Explosiva para Portas", 90.0, 110.0, typeof(IronIngot), "iron ingot", 2, 1044253);
            this.AddRes(index, typeof(SulfurousAsh), "sulfurous ash", 4, 1044253);
            this.AddRes(index, typeof(BaseExplosionPotion), "explosion potion", 2, 1044253);

            index = this.AddCraft(typeof(DoorPoisonTrapInstaller), "Armadilhas de Portas", "Armadilha de Veneno para Portas", 80.0, 100.0, typeof(IronIngot), "iron ingot", 2, 1044253);
            this.AddRes(index, typeof(Gears), "gears", 2, 1044253);
            this.AddRes(index, typeof(BasePoisonPotion), "poison potion", 1, 1044253);

            index = this.AddCraft(typeof(DoorGuillotineTrapInstaller), "Armadilhas de Portas", "Armadilha de Lâmina para Portas", 80.0, 100.0, typeof(IronIngot), "iron ingot", 2, 1044253);
            this.AddRes(index, typeof(Gears), "gears", 2, 1044253);
            this.AddRes(index, typeof(Halberd), "katana", 1, 1025183);
            #endregion

            #region Other Traps
            index = AddCraft(typeof(BladeSpiritTrap), "Outros", "Armadilha de Blade Spirit", 35.0, 55.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(TrappedGhost), "Trapped Ghost", 1, "You need a trapped ghost.");
            AddRes(index, typeof(Hammer), "Hammers", 1, "You need some a hammer.");
            AddRes(index, typeof(CrescentBlade), "Crescent Blades", 4, "You need more crescent blades.");

            index = AddCraft(typeof(GhostTrap), "Outros", "Armadilha de Fantasma", 75.0, 95.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(Bottle), "Bottle", 1, "You need a bottle.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Garlic), "Garlic", 4, "You need more garlic.");

            index = AddCraft(typeof(TrapDetector), "Outros", "Detector de Armadilhas", 55.0, 75.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(Springs), "Springs", 4, "You need some more springs");
            AddRes(index, typeof(Hammer), "Hammers", 4, "You need some more hammers");
            AddRes(index, typeof(Buckler), "Buckler", 1, "You need a buckler");

            index = AddCraft(typeof(TrapTest), "Outros", "Almofada", 25.0, 45.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(Leather), "Leather", 2, "You need more leather");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");
            AddRes(index, typeof(Springs), "Springs", 1, "You need some springs");
            #endregion
        }
    }
}
