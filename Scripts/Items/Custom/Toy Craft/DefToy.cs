using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefToy : CraftSystem
	{
		public override SkillName MainSkill{get	{ return SkillName.Carpintaria;	}}

		public override int GumpTitleNumber
		{
			get { return 1044004; } // <CENTER>TOY CRAFT MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefToy();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefToy() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
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
			// no animation
			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 9, 5, 1, true, false, 0 );

			from.PlaySound( 0x23D );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				if ( quality == 0 )
					return 502785; // You were barely able to make this item.  It's quality is below average.
				else if ( makersMark && quality == 2 )
					return 1044156; // You create an exceptional quality item and affix your maker's mark.
				else if ( quality == 2 )
					return 1044155; // You create an exceptional quality item.
				else				
					return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{

            #region Toys

            AddCraft( typeof( Toy1 ), "Toys", "Bear Toy", 50.0, 67.4, typeof( Board ), "Board", 12 );
			            AddCraft( typeof( Toy2 ), "Toys", "Male Doll", 50.0, 67.4, typeof( Board ), "Board", 12 );
                        AddCraft( typeof( Toy3 ), "Toys", "Barbie Doll", 60.0, 77.4, typeof( Board ), "Board", 12 );
                        AddCraft( typeof( Toy4 ), "Toys", "Horse Toy", 50.0, 67.4, typeof( Board ), "Board", 12 );
                        AddCraft( typeof( Toy5 ), "Toys", "Toad Toy", 50.0, 67.4, typeof( Board ), "Board", 12 );
                        AddCraft( typeof( Toy6 ), "Toys", "Serpent Toy", 50.0, 67.4, typeof( Board ), "Board", 12 );
                        AddCraft( typeof( Toy7 ), "Toys", "Lizard Toy", 55.0, 67.4, typeof( Board ), "Board", 12 );
                        AddCraft( typeof( Toy8 ), "Toys", "Scorpion Toy", 65.0, 87.4, typeof( Board ), "Board", 12 );
                        AddCraft( typeof( Toy9 ), "Toys", "Dog Toy", 45.0, 87.4, typeof( Board ), "Board", 12 );
                        AddCraft( typeof( Toy10 ), "Toys", "Crocodile Toy", 45.0, 87.4, typeof( Board ), "Board", 12 );
                        AddCraft( typeof( Toy11 ), "Toys", "Chariot Toy", 75.0, 97.4, typeof(Board), "Board", 12);
                        AddCraft( typeof( Toy12 ), "Toys", "Unicorn Toy", 65.0, 87.4, typeof(Board), "Board", 12);
                        AddCraft( typeof( Toy13 ), "Toys", "Dragon Toy", 55.0, 87.4, typeof(Board), "Board", 12);
                        AddCraft( typeof( Toy14 ), "Toys", "Centaur Toy", 60.0, 92.4, typeof(Board), "Board", 12);
            #endregion

                        MarkOption = true;
			Repair = Core.AOS;
		}
	}
}
