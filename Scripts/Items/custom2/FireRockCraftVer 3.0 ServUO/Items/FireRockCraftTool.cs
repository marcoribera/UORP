/* Created by Hammerhand*/

using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class FireRockCraftTool : BaseTool
	{
		public override CraftSystem CraftSystem { get { return DefFireRockCraft.CraftSystem; } }


		[Constructable]
		public FireRockCraftTool() : base( 0x1EBC )
		{
			Weight = 1.0;
            Name = "FireRock Crafting Tools";
			Hue = 1358;
		}

		[Constructable]
		public FireRockCraftTool( int uses ) : base( uses, 0x1EBC )
		{
			Weight = 1.0;
			Hue = 1358;
		}

        public FireRockCraftTool(Serial serial)
            : base(serial)
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

			if ( Weight == 2.0 )
				Weight = 1.0;
		}
	}
}
