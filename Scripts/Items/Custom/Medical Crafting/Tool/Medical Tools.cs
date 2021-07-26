using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[Flipable( 0x1EB8, 0x1EB9 )]
	public class MedicalTools : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefMedicalCrafting.CraftSystem; } }

		[Constructable]
		public MedicalTools() : base( 0x1EB8 )
		{
			Name = "Medical Tools";
			Weight = 1.0;
			Hue = 1366;
		}

		[Constructable]
		public MedicalTools( int uses ) : base( uses, 0x1028 )
		{
			Weight = 1.0;
		}

		public MedicalTools( Serial serial ) : base( serial )
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