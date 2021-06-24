// Nest with Eggs - Spawning.cs    by Alari (alarihyena@gmail.com)
using Server;
using Server.Items;
using Server.Network;
using System;

namespace Server.Items
{
	public abstract class NestWithEggsSpawning : Item
	{
		private short m_SpawnTime = 5;
		private EggsResetTimer m_ResetTimer;

		[CommandProperty( AccessLevel.GameMaster )]
		public short SpawnTime
		{
			get
			{
				return m_SpawnTime;
			}
			set
			{
				m_SpawnTime = value;
			}
		}

		public NestWithEggsSpawning() : base ( 6868 )
		{
			Movable = false;
		}

		public NestWithEggsSpawning( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( m_SpawnTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_SpawnTime = reader.ReadShort();

			if( ItemID == 6869 )
				StartResetTimer();
		}

		private void StartResetTimer()
		{
			if( m_ResetTimer == null )
				m_ResetTimer = new EggsResetTimer( this );
			else
				m_ResetTimer.Delay = TimeSpan.FromMinutes( m_SpawnTime );
				
			m_ResetTimer.Start();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this, 1 ) )
			{
				if ( this.ItemID == 6868 )
				{
					Eggs eggs = new Eggs();

					if ( !from.AddToBackpack( eggs ) )
					{
						eggs.Delete();
					}
					else
					{
						this.ItemID = 6869;
						StartResetTimer();
					}
				}
				else
				{
					from.SendMessage( "The nest is empty." );
				}
			}
			else
			{
				from.SendMessage( "That is too far away for you to reach." );  // 1060178 you are too far away to perform that action
			}
		}

		public void Reset()
		{
			if( m_ResetTimer != null )
			{
				if( m_ResetTimer.Running )
					m_ResetTimer.Stop();
			}

			ItemID = 6868;
		}

		private class EggsResetTimer : Timer
		{
			private NestWithEggsSpawning m_Nest;
			
			public EggsResetTimer( NestWithEggsSpawning nest ) : base ( TimeSpan.FromMinutes( nest.SpawnTime ) )
			{
				m_Nest = nest;
				Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				m_Nest.Reset();
			}
		}
	}
}