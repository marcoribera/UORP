/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\engines\CursedPirate\Items\Special\CursedRumBottles.cs
Lines of code: 80
***********************************************************/


using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class CursedRumBottles : Item
	{
		private SpawnTimer m_Timer;

		[Constructable]
		public CursedRumBottles() : base( 0x99D )
		{
			Movable = false;
			Hue = 0x483;
			Name = "Cursed bottles of rum";

			m_Timer = new SpawnTimer( this );
			m_Timer.Start();
		}

		public void Carve( Mobile from, Item item )
		{
			Effects.PlaySound( GetWorldLocation(), Map, 0x48F );
			Effects.SendLocationEffect( GetWorldLocation(), Map, 0x3728, 10, 10, 0, 0 );

			if ( 0.3 > Utility.RandomDouble() )
			{
				if ( ItemID == 0x99D )
					from.SendMessage( "You destroy the bottles." );
				else
					from.SendMessage( "You destroy the bottles." );

				Gold gold = new Gold( 25, 100 );

				gold.MoveToWorld( GetWorldLocation(), Map );

				Delete();

				m_Timer.Stop();
			}
			else
			{
				if ( ItemID == 0x99D )
					from.SendMessage( "You damage the bottles." );
				else
					from.SendMessage( "You damage the bottles." );
			}
		}

		public CursedRumBottles( Serial serial ) : base( serial )
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

			m_Timer = new SpawnTimer( this );
			m_Timer.Start();
		}

		private class SpawnTimer : Timer
		{
			private Item m_Item;

			public SpawnTimer( Item item ) : base( TimeSpan.FromSeconds( Utility.RandomMinMax( 5, 10 ) ) )
			{
				Priority = TimerPriority.FiftyMS;

				m_Item = item;
			}

			protected override void OnTick()
			{
				if ( m_Item.Deleted )
					return;

				Mobile spawn;

				switch ( Utility.Random( 2 ) )
				{
					default:
					case 0: spawn = new SkeletonPirate(); break;
					case 1: spawn = new CursedPirate(); break;
				}

				spawn.MoveToWorld( m_Item.Location, m_Item.Map );

				m_Item.Delete();
			}
		}
	}
}
