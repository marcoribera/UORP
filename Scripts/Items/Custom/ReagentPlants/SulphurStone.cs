/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\items\ReagentPlants\SulphurStone.cs
Lines of code: 56
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
	public class SulfurStone : Item
	{
	
		private int amt = 0;
		private int bonus = 0;
	
		[Constructable]
		public SulfurStone() : base(Utility.RandomList(4965, 4966))
		{
			Name = "Rocha Vulcânica";
			Weight = 0;
			Hue = 1175;
			Movable = false;
		}

		public SulfurStone( Serial serial ) : base( serial )
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
				else amt = 3;
			
				if(from.Race == Race.Human) bonus = 2;
				else bonus = 0;
			
				from.Say("* Você quebra a rocha vulcânica e recupera algumas cinzas sulfurosas *");
				from.AddToBackpack(new SulfurousAsh(amt + bonus));
				from.PlaySound(776);
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
