/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\items\ReagentPlants\NightshadePlant.cs
Lines of code: 60
***********************************************************/


using System;
using Server;
using Server.Spells;
using Server.Items;
using Server.Network;
using Server.Misc;
using Server.Mobiles;
using System.Collections;
using Server.Targeting;

namespace Server.Items
{
	public class NightshadePlant : Item
	{
	
		private int amt = 0;
		private int bonus = 0;
	
		[Constructable]
		public NightshadePlant() : base(Utility.RandomList(6373, 6374))
		{
			Weight = 0;
			Movable = false;
		}

		public NightshadePlant( Serial serial ) : base( serial )
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
			
				from.Say("* You harvest nightshade sprigs from the plant *");
				from.AddToBackpack(new Nightshade(amt + bonus));
				from.PlaySound(89);
				
				if (from.Alive && 0.4 >= Utility.RandomDouble() && from.Skills[SkillName.Envenenamento].Value <= 70)
				{
					from.SendMessage("You become poisoned upon picking the nightshade");
					from.ApplyPoison(from, Poison.Greater );
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
