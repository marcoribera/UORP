/* Created By Kingdoms Development Team*/
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DruidCloak : BaseCloak
	{
		
		[Constructable]
		public DruidCloak() : base( 11013 )
		{
                          Weight = 0;
                          Name = "Capa do Druida";
                          Hue = 1270;
           
		}

        public DruidCloak(Serial serial)
            : base(serial)
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}
