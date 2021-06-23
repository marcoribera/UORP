// Nest with Eggs - Decaying.cs    by Alari (alarihyena@gmail.com)
using System;
using Server.Items;

namespace Server.Items
{
	public class NestWithEggsDecaying : Item
	{
		[Constructable]
		public NestWithEggsDecaying() : base ( 6868 )
		{
			Movable = false;
		}

		public override bool Decays
		{
			get
			{
				if ( this.ItemID == 6868 )
					return false;

				return true;
			}
		}

		// I don't think this works right... (My own customization probably breaks it)
		public override TimeSpan DecayTime
		{
			get
			{
				return TimeSpan.FromMinutes( 1.0 );
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this, 1 ) )
			{
				if ( this.ItemID == 6868 )
				{
					Eggs eggs = new Eggs();

					if ( !from.AddToBackpack( eggs ) )
						eggs.Delete();

					this.ItemID = 6869;
				}
				else
				{
					from.SendMessage( "The nest is empty." );
				}
			}
			else
			{
				from.SendMessage( "That is too far away for you to reach." );  // 1060178 you are too far away to perform that action
			}
		}

		public NestWithEggsDecaying( Serial serial ) : base( serial )
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