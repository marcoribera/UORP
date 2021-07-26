using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{

	public class ToyMakersKit : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefToy.CraftSystem; } }

		[Constructable]
		public ToyMakersKit() : this( 10 )
       		{
		}

		[Constructable]
		public ToyMakersKit( int uses ) : base( 0x1EBA )
		{
			Weight = 2.0;
                        Name = "Toy Maker's Kit";
                        Hue = 2963;
			UsesRemaining = uses;
			ShowUsesRemaining = true;
		}

        public ToyMakersKit(Serial serial): base(serial)
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

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}