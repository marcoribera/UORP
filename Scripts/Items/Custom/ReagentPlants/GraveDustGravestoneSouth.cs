/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\items\ReagentPlants\GraveDustGravestoneSouth.cs
Lines of code: 96
***********************************************************/


using System;
using Server;
using Server.Spells;
using Server.Items;
using Server.Network;
using Server.Misc;
using Server.Mobiles;
using System.Collections;

namespace Server.Items
{
	public class GraveDustGravestoneSouth : Item
	{
	
		private int amt = 0;
		private int bonus = 0;
	
		[Constructable]
		public GraveDustGravestoneSouth() : base(Utility.RandomList(4460))
		{
			Name = "Old Gravestone";
			Weight = 0;
			Movable = false;
		}

		public GraveDustGravestoneSouth( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			Map map = from.Map;

			if ( from.Alive && !from.InRange( this, 1 ))
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			}
			else
			{
				if(this.Map == Map.Felucca) amt = 8;
				else amt = 6;
			
				if(from.Race == Race.Human) bonus = 2;
				else bonus = 0;
			
				from.Say("* You smash the old gravestone and retrieve some dust *");
				Misc.Titles.AwardKarma( from, -150, true );
				from.AddToBackpack(new GraveDust(amt + bonus));
				from.PlaySound(829);
				
				if (from.Alive && 0.1 > Utility.RandomDouble()) {
					Mobile spawn;
					
					switch ( Utility.Random( 10 ) )
					{
						default:
						case 0: 
							spawn = new Skeleton(); 
							break;
						case 1: 
							spawn = new SkeletalKnight(); 
							break;
						case 2:
							spawn = new Wraith(); 
							break;
						case 3: 
							spawn = new Wight(); 
							break;
						case 4: 
							spawn = new BoneMagi(); 
							break;
						case 5: 
							spawn = new Lich(); 
							break;
						case 6:
							spawn = new LichLord(); 
							break;
						case 7: 
							spawn = new SkeletalMage(); 
							break;
						case 8:
							spawn = new Zombie(); 
							break;
						case 9: 
							spawn = new Ghoul(); 
							break;
					}
					
					spawn.MoveToWorld( this.Location, this.Map );
					spawn.Combatant = from;
					from.SendMessage( "An undead creature leaps from the grave to seek vengeance!" );
				}
				this.Delete();
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
		}
	}
}