using System;
using System.Collections.Generic;
using Server;
using Server.Multis;
using Server.Network;
using Server.Mobiles;
using Server.ContextMenus;

namespace Server.Items
{

        [DynamicFliping]
	[Flipable( 0x194A, 0x1949 )]
	public class BlessedStatue : BaseContainer
	{

		public override int DefaultGumpID{ get{ return 0x109; } }
		public override int DefaultDropSound{ get{ return 0x42; } }


		[Constructable]
		public BlessedStatue() : base( 0x194A )
               {
			Weight = 20.0;
               }

		public BlessedStatue( Serial serial ) : base( serial )
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