/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\engines\CursedPirate\Items\Special\CursedJugOfRumShower.cs
Lines of code: 99
***********************************************************/


using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class CursedJugOfRumShower : Item
	{

		[Constructable]
		public CursedJugOfRumShower() :  base( 0x1870 )
		{
			Hue = 0x0;
			Name = "Shower of Cursed Rum";
			LootType = LootType.Blessed;
			Visible = false;
			Movable = false;
		}

		public override void OnDoubleClick( Mobile m )
		{
			Map map = this.Map;

			if ( map != null )
			{
				for ( int x = -12; x <= 12; ++x )
				{
					for ( int y = -12; y <= 12; ++y )
					{
						double dist = Math.Sqrt(x*x+y*y);

						if ( dist <= 12 )
							new GoodiesTimer( map, X + x, Y + y ).Start();
					}
				}
			}
		}

		private class GoodiesTimer : Timer
		{
			private Map m_Map;
			private int m_X, m_Y;

			public GoodiesTimer( Map map, int x, int y ) : base( TimeSpan.FromSeconds( Utility.RandomDouble() * 10.0 ) )
			{
				m_Map = map;
				m_X = x;
				m_Y = y;
			}

			protected override void OnTick()
			{
				int z = m_Map.GetAverageZ( m_X, m_Y );
				bool canFit = m_Map.CanFit( m_X, m_Y, z, 6, false, false );

				for ( int i = -3; !canFit && i <= 3; ++i )
				{
					canFit = m_Map.CanFit( m_X, m_Y, z + i, 6, false, false );

					if ( canFit )
						z += i;
				}

				if ( !canFit )
					return;

				CursedJugOfRum g = new CursedJugOfRum( );

				g.MoveToWorld( new Point3D( m_X, m_Y, z ), m_Map );

				if ( 0.5 >= Utility.RandomDouble() )
				{
					switch ( Utility.Random( 3 ) )
					{
						case 0: // Poison Field
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x3914, 10, 30, 5052 );
							Effects.PlaySound( g, g.Map, 0x208 );

							break;
						}
						case 1: // Explosion
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x3914, 20, 10, 5044 );
							Effects.PlaySound( g, g.Map, 0x307 );

							break;
						}
						case 2: // Ball of fire
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x3914, 10, 10, 5052 );

							break;
						}
					}
				}
			}
		}

		public CursedJugOfRumShower( Serial serial ) : base( serial )
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
