/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\items\resources\GreenMushroom.cs
Lines of code: 58
***********************************************************/


using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class GreenMushroom : Item
	{
		public virtual int Bonus{ get{ return 50; } }
		public virtual StatType Type{ get{ return StatType.Str; } }

		public override string DefaultName
		{
			get { return "Green Mushroom"; }
		}

		[Constructable]
		public GreenMushroom() : base( 0x26B7 )
		{
		Hue = 76;
		}
		
		public GreenMushroom( Serial serial ) : base( serial )
		{
		}
		
		public virtual bool Apply( Mobile from )
		{
			bool applied = Spells.SpellHelper.AddStatOffset( from, Type, Bonus, TimeSpan.FromMinutes( 3.0 ) );

			if ( !applied )
				from.SendLocalizedMessage( 502173 ); // You are already under a similar effect.

			return applied;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( Apply( from ) )
			{
				from.SendMessage( "You feel a lot stronger" );
				from.FixedEffect( 0x375A, 10, 15 );
				from.PlaySound( 0x1E7 );
				Delete();
			}
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

			if ( Hue == 376 )
				Hue = 33;
		}
	}
}