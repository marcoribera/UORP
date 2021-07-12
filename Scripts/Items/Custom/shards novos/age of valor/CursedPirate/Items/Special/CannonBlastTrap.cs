/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\engines\CursedPirate\Items\Special\CannonBlastTrap.cs
Lines of code: 53
***********************************************************/


using System;
using Server;
using Server.Network;
using Server.Regions;
using Server.Mobiles;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class CannonBlastTrap : BaseTrap
	{
		[Constructable]
		public CannonBlastTrap() : base(11668)
		{
			Name = "Cannon Blast Trap";
			this.Visible = false; 
		}

		public override bool PassivelyTriggered{ get{ return true; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.Zero; } }
		public override int PassiveTriggerRange{ get{ return 0; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.Zero; } }

		public override void OnTrigger( Mobile from )
		{
		
			if (from == null || !from.Alive || from.AccessLevel > AccessLevel.Player)
				return;

			if(from is BaseCreature)
			{
				BaseCreature bc = (BaseCreature)from;

				if (bc == null || bc is BaseCreature && !bc.Controlled || !bc.Allured)
				return;
			}

			from.SendMessage("You are hit by a cannon blast!");
			from.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
			from.PlaySound( 0x307 );
			Spells.SpellHelper.Damage( TimeSpan.FromTicks( 1 ), from, from, Utility.RandomMinMax( 20, 55 ));
			Delete();
		}

		public CannonBlastTrap( Serial serial ) : base( serial )
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