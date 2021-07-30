using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefWinecrafting : CraftSystem
	{
		public override SkillName MainSkill { get { return SkillName.Culinaria; } }

		public override int GumpTitleNumber { get { return 0; } }

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>Winecrafting Menu</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null ) m_CraftSystem = new DefWinecrafting();
				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item ) { return 0.5; }

		private DefWinecrafting() : base( 1, 1, 1.25 ){}

		public override int CanCraft( Mobile from, ITool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 ) return 1044038;
			return 0;
		}

		public override void PlayCraftEffect( Mobile from ) { from.PlaySound( 0x241 ); }

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
			string skillNotice = "Você não sabe fazer vinho com essa uva.";

			index = AddCraft( typeof( WineKeg ), "Vinhos", "Barril de Vinho", 50.0, 105.6, typeof( CabernetSauvignonGrapes ), "Uva Cabernet Sauvignon", 50 );
			AddRes( index, typeof( Keg ), "Barril", 1 );
			AddRes( index, typeof ( WinecrafterSugar ), "Açucar", 1 );
			AddRes( index, typeof ( WinecrafterYeast ), "Levedura", 1 );

			SetSubRes( typeof( CabernetSauvignonGrapes ), "Uvas Cabernet Sauvignon" );

			AddSubRes(typeof(CabernetSauvignonGrapes), "Uvas Cabernet Sauvignon", 50.0, skillNotice);
			AddSubRes(typeof(ChardonnayGrapes), "Uvas Chardonnay", 55.0, skillNotice);
			AddSubRes(typeof(CheninBlancGrapes), "Uvas Chenin Blanc", 58.0, skillNotice);
			AddSubRes(typeof(MerlotGrapes), "Uvas Merlots", 60.0, skillNotice);
			AddSubRes(typeof(PinotNoirGrapes), "Uvas Pinot Noir Grapes", 62.0, skillNotice);
			AddSubRes(typeof(RieslingGrapes), "Uvas Riesling Grapes", 65.0, skillNotice);
			AddSubRes(typeof(SangioveseGrapes), "Uvas Sangiovese Grapes", 67.0, skillNotice);
			AddSubRes(typeof(SauvignonBlancGrapes), "Uvas Sauvignon Blanc Grapes", 69.0, skillNotice);
			AddSubRes(typeof(ShirazGrapes), "Uvas Shiraz Grapes", 70.0, skillNotice);
			AddSubRes(typeof(ViognierGrapes), "Uvas Viognier Grapes", 72.0, skillNotice);
			AddSubRes(typeof(ZinfandelGrapes), "Uvas Zinfandel Grapes", 74.0, skillNotice);
			AddSubRes(typeof(Apple), "Maça", 50.0, skillNotice);
			AddSubRes(typeof(Apricot), "Damasco", 55.0, skillNotice);
			AddSubRes(typeof(Cherry), "Cerejas", 58.0, skillNotice);
			AddSubRes(typeof(Mango), "Manga", 60.0, skillNotice);
			AddSubRes(typeof(Orange), "Laranja", 62.0, skillNotice);
			AddSubRes(typeof(Pear), "Pera", 50.0, skillNotice);
			AddSubRes(typeof(Peach), "Pêssego", 50.0, skillNotice);
			AddSubRes(typeof(Blackberry), "Amora Silveste", 70.0, skillNotice);
			AddSubRes(typeof(BlackRaspberry), "Frambroesa Prestas", 72.0, skillNotice);
			AddSubRes(typeof(Blueberry), "Amoras", 75.0, skillNotice);
			AddSubRes(typeof(Cranberry), "Cranberries", 79.0, skillNotice);
			AddSubRes(typeof(RedRaspberry), "Framboesas Vermelhas", 80.0, skillNotice);
			AddSubRes(typeof(Strawberry), "Morangos", 85.0, skillNotice);
			AddSubRes(typeof(Watermelon), "Melância", 90.0, skillNotice);
			AddSubRes(typeof(RiceSheath), "Arroz", 100.0, skillNotice);
			AddSubRes(typeof(Dandelion), "Dente de Leão", 110.0, skillNotice);

			MarkOption = true;
			Repair = false;
		}
	}
}
