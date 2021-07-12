/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\items\ReagentPlants\SilkCocoon.cs
Lines of code: 95
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
	public class SilkCocoon : Item
	{
	
		private int amt = 0;
		private int bonus = 0;
	
		[Constructable]
		public SilkCocoon() : base(4314)
		{
			Name = "Spider's Cocoon";
			Weight = 0;
			Movable = false;
		}

		public SilkCocoon( Serial serial ) : base( serial )
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
			
				from.Say("* You destroy the cocoon and collect some spider's silk *");
				from.AddToBackpack(new SpidersSilk(amt + bonus));
				from.PlaySound(829);
				
				if (from.Alive && 0.2 > Utility.RandomDouble()) {
					Mobile spawn;
					
					switch ( Utility.Random( 10 ) )
					{
						default:
						case 0: 
							spawn = new GiantSpider(); 
							break;
						case 1: 
							spawn = new GiantBlackWidow(); 
							break;
						case 2:
							spawn = new DreadSpider(); 
							break;
						case 3: 
							spawn = new GiantBlackWidow(); 
							break;
						case 4: 
							spawn = new GiantBlackWidow(); 
							break;
						case 5: 
							spawn = new GiantSpider(); 
							break;
						case 6:
							spawn = new GiantSpider(); 
							break;
						case 7: 
							spawn = new GiantSpider(); 
							break;
						case 8:
							spawn = new GiantBlackWidow(); 
							break;
						case 9: 
							spawn = new DreadSpider(); 
							break;
					}
					
					spawn.MoveToWorld( this.Location, this.Map );
					spawn.Combatant = from;
					from.SendMessage( "A spider attacks you for destroying the cocoon!" );
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