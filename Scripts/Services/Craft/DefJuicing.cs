using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefJuicing : CraftSystem
	{
		public override SkillName MainSkill { get { return SkillName.Culinaria; } }

		public override int GumpTitleNumber { get { return 0; } }

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>JUICING MENU</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null ) m_CraftSystem = new DefJuicing();
				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5;
		}

		private DefJuicing() : base( 1, 1, 1.25 ) { }

		public override int CanCraft( Mobile from, ITool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 ) return 1044038;
			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x241 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken ) from.SendLocalizedMessage( 1044038 );
			if ( failed )
			{
				if ( lostMaterial ) return 1044043;
				else return 1044157;
			}
			else
			{
				if ( quality == 0 ) return 502785;
				else if ( makersMark && quality == 2 ) return 1044156;
				else if ( quality == 2 ) return 1044155;
				else return 1044154;
			}
		}

		public override void InitCraftList()
		{
			int index = -1;
			string skillNotice = "Você não sabe como trabalhar com essa fruta.";

			index = AddCraft( typeof( JuiceKeg ), "Sucos", "Barril de Suco", 25.0, 120.0, typeof( Apple ), "Maças", 25 );
			AddRes( index, typeof( BaseBeverage ), "Água", 5 );
			AddRes( index, typeof( Keg ), "Barril", 1 );
			AddRes( index, typeof( BagOfSugar ), "Saco de Açúcar", 1 );

			SetSubRes( typeof( Apple ), "Maça" );

			AddSubRes( typeof( Apple ),		"Maças", 20.0, skillNotice );
			AddSubRes( typeof( Banana ),		"Bananas", 20.0, skillNotice );
			AddSubRes( typeof( Dates ),		"Tâmara", 20.0, skillNotice );
			AddSubRes( typeof( Grapes ),		"Uvas", 25.0, skillNotice );
			AddSubRes( typeof( Lemon ),		"Limões", 32.0, skillNotice );
			AddSubRes( typeof( Lime ),		"Lima", 34.0, skillNotice );
			AddSubRes( typeof( Orange ),		"Laranja", 36.0, skillNotice );
			AddSubRes( typeof( Peach ),		"Pêssego", 38.0, skillNotice );
			AddSubRes( typeof( Pear ),		"Peras", 40.0, skillNotice );
			AddSubRes( typeof( Pumpkin ), "Abóboras", 42.0, skillNotice );
			AddSubRes( typeof( Tomato ),		"Tomates", 44.0, skillNotice );
			AddSubRes( typeof( Watermelon ),	"Melância", 46.0, skillNotice );
			AddSubRes( typeof( Apricot ), "Damascos", 48.0, skillNotice );
			AddSubRes( typeof( Blackberry ), "Amora", 50.0, skillNotice );
			AddSubRes( typeof( Blueberry ), "Mirtilo", 53.0, skillNotice );
			AddSubRes( typeof( Cherry ), "cereja", 54.0, skillNotice );
			AddSubRes( typeof( Cranberry ),	"Uva do Monte", 55.0, skillNotice );
			AddSubRes( typeof( Grapefruit ), "Toranja", 56.0, skillNotice );
			AddSubRes( typeof( Kiwi ),		"Kiwis", 58.0, skillNotice );
			AddSubRes( typeof( Mango ),		"Mangas", 60.0, skillNotice );
			AddSubRes( typeof( Pineapple ),	"Abacaxi", 62.0, skillNotice );
			AddSubRes( typeof( Pomegranate ), "Romãs", 64.0, skillNotice );
			AddSubRes( typeof( Strawberry ), "morango", 66.0, skillNotice );
			AddSubRes( typeof( Almond ), "Amêndoa", 68.0, skillNotice );
			AddSubRes( typeof( Asparagus ),	"Asparago", 70.0, skillNotice );
			AddSubRes( typeof( Avocado ), "Abacate", 72.0, skillNotice );
			AddSubRes( typeof( Beet ), "Beterraba", 74.0, skillNotice );
			AddSubRes( typeof( BlackRaspberry ), "Framboesa preta", 76.0, skillNotice );
			AddSubRes( typeof( Cantaloupe ), "Cantalupo", 78.0, skillNotice );
			AddSubRes( typeof( Carrot ),		"Cenora", 80.0, skillNotice );
			AddSubRes( typeof( Cauliflower ),	"Couve Flor", 82.0, skillNotice );
			AddSubRes( typeof( Celery ), "Salsão", 83.0, skillNotice );
			AddSubRes( typeof( Coconut ),	"Coco", 86.0, skillNotice );
			AddSubRes( typeof( Corn ),		"Milho", 89.0, skillNotice );
			AddSubRes( typeof( Cucumber ),	"Pepino", 91.0, skillNotice );
			AddSubRes( typeof( GreenSquash ), "Abóbora Verde", 94.0, skillNotice );
			AddSubRes( typeof( HoneydewMelon ),	"Melão", 96.0, skillNotice );
			AddSubRes( typeof( Onion ),		"Cebola", 99.0, skillNotice );
			AddSubRes( typeof( Peanut ), "Amendoim", 104.0, skillNotice );
			AddSubRes( typeof( Pistacio ),		"Pistache", 106.0, skillNotice );
			AddSubRes( typeof( Potato ),		"Batata", 108.0, skillNotice );
			AddSubRes( typeof( Radish ), "Rabanete", 110.0, skillNotice );
			AddSubRes( typeof( RedRaspberry ), "Framboesa vermelha", 112.0, skillNotice );
			AddSubRes( typeof( Spinach ),		"Espinafre", 114.0, skillNotice );
			AddSubRes( typeof( Squash ),		"Aboboreia", 116.0, skillNotice );
			AddSubRes( typeof( SweetPotato ),	"Batata Doce", 118.0, skillNotice );
			AddSubRes( typeof( Turnip ), "Nabo", 120.0, skillNotice );

			MarkOption = true;
			Repair = false;
		}
	}
}
