using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class LesserNecromancer : BaseVendor
	{
		private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
		[Constructable]
		public LesserNecromancer()
			: base( "" )
		{
			SetSkill( SkillName.PoderMagico, 40, 52.0);
			SetSkill( SkillName.Erudicao, 40, 52.0);
			SetSkill( SkillName.Necromancia, 40, 52.0 );
			SetSkill( SkillName.ResistenciaMagica, 40, 52.0);

			Persuadable = true;
			ControlSlots = 2;
			MinPersuadeSkill = 55;
            this.SetStr(80, 96);
            this.SetDex(80, 90);
            this.SetInt(26, 40);

            SetHits(150, 180);
            SetMana(80, 100);

            Hue = 0x3C6;
		}

		public LesserNecromancer(Serial serial)
            : base(serial)
        {
        }

		protected override List<SBInfo> SBInfos
        {
            get
            {
                return this.m_SBInfos;
            }
        }

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBNecromancer() );
		}


		public override void InitOutfit()
		{
			base.InitOutfit();
			AddItem( new Server.Items.Shoes( 0x151 ) );
			AddItem( new Server.Items.Robe( 0x455 ) );
			AddItem( new Server.Items.FancyShirt( 0x455 ) );

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = 0x3c6;
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );

			Item beard = new Item( 0x0 );
			beard.Layer = Layer.FacialHair;
			AddItem( beard );
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
