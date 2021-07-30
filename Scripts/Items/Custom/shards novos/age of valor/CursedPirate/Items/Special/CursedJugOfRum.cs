/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\engines\CursedPirate\Items\Special\CursedJugOfRum.cs
Lines of code: 108
***********************************************************/


using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
 

namespace Server.Items 
{ 
	public abstract class BaseCursedJugOfRum : Item 
	{ 
		public Poison m_Poison; 
		public int Highness; 

		[CommandProperty( AccessLevel.GameMaster )] 
		public Poison Poison 
		{ 
			get { return m_Poison; } 
			set { m_Poison = value; } 
		} 

		public BaseCursedJugOfRum( int itemID ) : base( itemID ) 
		{
			LootType = LootType.Cursed;
		} 
		
		public override void OnDoubleClick( Mobile from ) 
		{ 
			if ( !IsChildOf( from.Backpack ) ) 
			{ 
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				
				return;
			} 
			else 
			{ 
				DrinkIt( from );
			} 
		}

		public void DrinkIt( Mobile from )
		{
			// Play a random drinking sound
			from.PlaySound( Utility.Random( 0x30, 2 ) );

			if ( from.Body.IsHuman && !from.Mounted )
				from.Animate( 34, 5, 1, true, false, 0 );

			switch ( Utility.Random( 25 ) )
			{
				default: from.SendMessage("Nothing seems to happen"); break; //No effect
				case 0: GetDrunk( from ); break; //Get Drunk
				case 1: PirateCurse.SummonPirate( from, 1 ); break; //Summon Cursed Pirate
				case 2: PirateCurse.SummonPirate( from, 2 ); break; //Summon Cursed Pirate King
				case 3: PirateCurse.CursePlayer( from ); break; //Turn into Cursed Pirate??
				case 4: PirateCurse.HurtPlayer( from ); break; //DIE!
			}
			
			this.Consume();
		}
		
		public void GetDrunk( Mobile m )
		{
			m.SendMessage("Yo ho, Yo ho, a pirate's life for you");
			int bac = 30;
			m.BAC += bac;
			if ( m.BAC > 60 )
				m.BAC = 60;

			BaseBeverage.CheckHeaveTimer( m );
		}

		public BaseCursedJugOfRum( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version 
			
			Poison.Serialize( m_Poison, writer ); 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 

			switch ( version ) 
			{ 
				case 0: 
				{ 
					m_Poison = Poison.Deserialize( reader ); 
					break; 
				} 
			} 
		} 
	} 

//===================================== 


	[FlipableAttribute( 0x9C8, 0x9C8 )] 
	public class CursedJugOfRum : BaseCursedJugOfRum
	{ 
		[Constructable] 
		public CursedJugOfRum() : base( 0x9C8 ) 
		{ 
			Name = "A Jug of Rum"; 
			Weight = 0.2;
			LootType = LootType.Cursed;
		} 

		public CursedJugOfRum( Serial serial ) : base( serial ) 
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
//===================================== 
} 
