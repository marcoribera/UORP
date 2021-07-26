using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class CheeseMaker : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public CheeseMaker() : base( "the cheese maker" )
		{
			SetSkill( SkillName.Extracao, 36.0, 68.0 );
			SetSkill( SkillName.Culinaria, 36.0, 68.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBCheeseMaker() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return VendorShoeType.ThighBoots; }
		}

		public override int GetShoeHue()
		{
			return 0;
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.WideBrimHat( Utility.RandomNeutralHue() ) );
		}

		public CheeseMaker( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
