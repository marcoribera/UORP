using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
   	public class IceBallOfDeath : Item
   	{
      		[Constructable]
      		public IceBallOfDeath() : base( 0x913 )
      		{
         		base.Name = "a ball of ice with a rock in it";
         		Hue = 0x481;
         		LootType = LootType.Regular;
			base.Weight = 13.0;
      		}

      		public IceBallOfDeath( Serial serial ) : base( serial )
      		{
      		}

      		public override void OnDoubleClick( Mobile from )
     	 	{
         		if (!IsChildOf(from.Backpack))
         		{		
            			from.SendLocalizedMessage( 1042010 ); //You must have the object in your backpack to use it.
         		}
         		else
         		{
				if ( from.BeginAction( typeof( IceBallOfDeath ) ) )
				{
               				from.Target = new IceBallOfDeathTarget( from );
					if( from.Hidden )
						from.RevealingAction();
              				from.SendMessage( "You carefully pack the ice around a big rock..." );
            			}
            			else
            			{
               				from.SendMessage( "The rock is too warm for you to pack ice around.  Wait a bit..." );
					Timer.DelayCall( TimeSpan.FromSeconds( 2.0 ), new TimerStateCallback( ReleaseIceBallLock ), from );
            			}
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

      		private class IceBallOfDeathTarget : Target
      		{
         		private Mobile m_From;

         		public IceBallOfDeathTarget( Mobile from ) : base ( 10, false, TargetFlags.None )
         		{
            			m_From = from;
         		}

         		protected override void OnTarget( Mobile from, object target )
         		{
				bool targetIsPM = false;
				PlayerMobile pm = null;
				if( from.Hidden )
					from.RevealingAction();
				if( target is PlayerMobile )
				{
					targetIsPM = true;					
					pm = (PlayerMobile)target;
				}
				
				if ( target is Item )
				{
               				from.SendMessage( "No vandalism, please." );
					Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerStateCallback( ReleaseIceBallLock ), from );
				}
				else if ( target == from )
				{
               				from.SendMessage( "You hit yourself in the head with your own rock, dumbass..." );
					Timer.DelayCall( TimeSpan.FromSeconds( 10.0 ), new TimerStateCallback( ReleaseIceBallLock ), from );
				}
				else if ( targetIsPM && pm.CheckAlive() )
				{
		   			Item[] targeticodnow = pm.Backpack.FindItemsByType( typeof( IceBallOfDeath ) );

		   			if ( targeticodnow == null || targeticodnow.Length == 0 )
		   			{
               					from.SendMessage( "It's not nice to throw iceballs at unarmed people..." );
						Timer.DelayCall( TimeSpan.FromSeconds( 10.0 ), new TimerStateCallback( ReleaseIceBallLock ), from );
					}
					else if( checkHit() )
					{
               					from.Animate( 9, 1, 1, true, false, 0 );
               					Effects.SendMovingEffect( from, pm, 0x36E4, 7, 0, false, true, 0x480, 0 );
						Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( ReleaseIceBallLock ), from );

						InternalTimer t = new InternalTimer( from );
						t.Start();
						doDamage( from, pm );
					}
					else
					{
						from.SendMessage( "You MISSED!!!" );
               					pm.SendMessage( "A hard, cold object goes flying by your head!" );
						Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( ReleaseIceBallLock ), from );
					}
				}
				else
				{
					if( targetIsPM && !pm.CheckAlive() )
					{
						from.SendMessage( "Why would you try to hit an already dead player???" );
						Timer.DelayCall( TimeSpan.FromSeconds( 2.0 ), new TimerStateCallback( ReleaseIceBallLock ), from );
					}
					else
					{
						from.SendMessage( "No vandalism, please." );
						Timer.DelayCall( TimeSpan.FromSeconds( 2.0 ), new TimerStateCallback( ReleaseIceBallLock ), from );
					}
				}
         		}
			
			protected bool checkHit()
			{
				if( Utility.Random( 100 ) <= 98 )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			
			protected void doDamage( Mobile attacker, Mobile defender )
			{
				int damage = 2;
				int hitType = Utility.Random( 10 );
				bool isStunned = ( Utility.Random( 10 ) <= 2 );
				switch( hitType )
				{
					case 0:
					{
               					attacker.SendMessage( "You throw the iceball and lucky for you, it bounced and hit them." );
               					defender.SendMessage( "You have just been hit by a ricocheting rock!" );
						damage += Utility.RandomMinMax( 1, 3 );
						break;
					}
					case 1:
					{
               					attacker.SendMessage( "You throw the iceball and it glances off their arm." );
               					defender.SendMessage( "You have just been hit a glancing blow by an iceball!" );
						damage += Utility.RandomMinMax( 2, 6 );
						break;
					}
					case 2:
					{
               					attacker.SendMessage( "You throw the iceball and it hits them in the back!" );
               					defender.SendMessage( "You have just been hit in the back by an iceball!" );
						damage += Utility.RandomMinMax( 4, 10 );
						break;
					}
					case 3:
					{
               					attacker.SendMessage( "You throw the iceball and it hits them in the kneecap!" );
               					defender.SendMessage( "You have just been hit in the kneecap by an iceball!" );
						damage += Utility.RandomMinMax( 6, 10 );
						break;
					}
					case 4:
					{
               					attacker.SendMessage( "You throw the iceball and it hits them square in the arm!" );
               					defender.SendMessage( "You have just been hit in the arm by an iceball!" );
						damage += Utility.RandomMinMax( 6, 12 );
						break;
					}
					case 5:
					{
               					attacker.SendMessage( "You throw the iceball and it hits them in the chest, knocking the wind out of them!" );
               					defender.SendMessage( "You have just been hit in the chest by an iceball, knocking the wind out of you!" );
						damage += Utility.RandomMinMax( 6, 15 );
						break;
					}
					case 6:
					{
               					attacker.SendMessage( "You throw the iceball and it hits them in the ankle, knocking them down!" );
               					defender.SendMessage( "You have just been hit in the ankle by an iceball, knocking you down!" );
						damage += Utility.RandomMinMax( 6, 18 );
						break;
					}
					case 7:
					{
               					attacker.SendMessage( "You throw the iceball and it hits smack in the forehead, knocking them senseless!" );
               					defender.SendMessage( "You have just been hit smack in the forehead by an iceball, knocking you senseless!" );
						damage += Utility.RandomMinMax( 7, 21 );
						if( isStunned )
						{
							
               						attacker.SendMessage( "You have stunned your target!!!" );
							defender.SendLocalizedMessage( 1004014 ); // You have been stunned!
							defender.Freeze( TimeSpan.FromSeconds( 2.0 ) );
						}
						break;
					}
					case 8:
					{
               					attacker.SendMessage( "You throw the iceball and it hits them in the eye, swelling their eye shut!" );
               					defender.SendMessage( "You have just been hit in the eye by an iceball, swelling your eye shut!" );
						damage += Utility.RandomMinMax( 8, 25 );
						if( isStunned )
						{
							defender.SendLocalizedMessage( 1004014 ); // You have been stunned!
							defender.Freeze( TimeSpan.FromSeconds( 4.0 ) );
						}
						break;
					}
					case 9:
					{
               					attacker.SendMessage( "You throw the iceball and it hits them right in the crotch. You can see their eyes crossing!" );
               					defender.SendMessage( "You have just been hit right in the crotch by an iceball, You see double as you slump to your knees!" );
						damage += Utility.RandomMinMax( 9, 30 );
						if( isStunned )
						{
							defender.SendLocalizedMessage( 1004014 ); // You have been stunned!
							defender.Freeze( TimeSpan.FromSeconds( 6.0 ) );
						}
						break;
					}
				}

				new Blood().MoveToWorld( defender.Location, defender.Map );

				if ( Utility.RandomBool() )
				{
					new Blood().MoveToWorld( new Point3D(
						defender.X + Utility.RandomMinMax( -1, 1 ),
						defender.Y + Utility.RandomMinMax( -1, 1 ),
						defender.Z ), defender.Map );
				}
				SpellHelper.Damage( TimeSpan.Zero, defender, attacker, (double)damage );
				
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 0.5 ) )
			{
				m_From = from;

				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				m_From.PlaySound( 0x145 );
			}
		}

		private static void ReleaseIceBallLock( object state )
		{
			((Mobile)state).EndAction( typeof( IceBallOfDeath ) );
		}
      	}
}