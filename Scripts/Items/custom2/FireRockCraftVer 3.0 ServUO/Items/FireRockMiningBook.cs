/* Created by Hammerhand*/

using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class FireRockMiningBook : Item
	{
		[Constructable]
		public FireRockMiningBook() : base( 0xFF4 )
		{
            Name = "Mining FireRock";
			Weight = 1.0;
            Hue = 1358;
		}

        public FireRockMiningBook(Serial serial)
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
		}

		public override void OnDoubleClick( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( pm == null || from.Skills[SkillName.Extracao].Base < 100.0 )
			{
				pm.SendMessage( "Only a Grandmaster Miner can learn from this book." );
			}
			// else if ( pm.FireRockMining )
			// {
				// pm.SendMessage( "You have already learned this information." );
			// }
			// else
			// {
				// pm.FireRockMining = true;
                // pm.SendMessage("You have learned how to mine chunks of FireRock. Target lava areas when mining to look for FireRock.");
				// Delete();
			// }
		}
	}
}
