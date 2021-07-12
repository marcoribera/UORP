/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\items\resources\HealthyRoot.cs
Lines of code: 63
***********************************************************/


using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class HealthyRoot : Item //Unfinished, timer is currently broken
	{
		public virtual int Bonus{ get{ return 1; } }
		public virtual StatType Type{ get{ return StatType.Str; } }

		public override string DefaultName
		{
			get { return "Healthy Root"; }
		}

		[Constructable]
		public HealthyRoot() : base( 6375 )
		{
		Hue = 67;
		}
		
		public HealthyRoot( Serial serial ) : base( serial )
		{
		}
		
		public virtual bool Apply( Mobile from )
		{
			bool applied = Spells.SpellHelper.AddStatOffset( from, Type, Bonus, TimeSpan.FromMinutes( 1.0 ) );

			if ( !applied )
				from.SendMessage( "Eating another root this soon is hazardous to your health" );

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
				from.SendMessage( "Your health is replenished" );
				from.SendMessage( "Your body is purged of all toxins" );
				from.SendMessage( "You no longer feel hungry" );
				
				from.Hits += 2000;
				from.Poison = null;
				from.Hunger = 0; 
				
				from.FixedParticles( 0x376A, 9, 32, 5005, EffectLayer.Waist );
				from.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
				from.PlaySound( 0x202 );
				from.PlaySound( 0x1E0 );
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

			//if ( Hue == 376 )
			//	Hue = 33;
		}
	}
}