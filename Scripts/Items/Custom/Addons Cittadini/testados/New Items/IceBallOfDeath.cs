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
         		base.Name = "uma bola de gelo com uma pedra dentro";
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
              				from.SendMessage( "você cuidadosamente coloca gelo ao redor da pedra..." );
            			}
            			else
            			{
               				from.SendMessage( "A rocha está muito quente para colocar gelo ao redor dela.  Espere um pouquinho..." );
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
               				from.SendMessage( "Sem vandalismo, por favor." );
					Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerStateCallback( ReleaseIceBallLock ), from );
				}
				else if ( target == from )
				{
               				from.SendMessage( "Você acertou sua cabeça com a pedra, não parece uma boa ideia..." );
					Timer.DelayCall( TimeSpan.FromSeconds( 10.0 ), new TimerStateCallback( ReleaseIceBallLock ), from );
				}
				else if ( targetIsPM && pm.CheckAlive() )
				{
		   			Item[] targeticodnow = pm.Backpack.FindItemsByType( typeof( IceBallOfDeath ) );

		   			if ( targeticodnow == null || targeticodnow.Length == 0 )
		   			{
               					from.SendMessage( "Aquela pessoa está desarmada, isso não parece justo..." );
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
						from.SendMessage( "Você errou!!!" );
               					pm.SendMessage( "Um objeto pesado, acabou de passar voando pela sua cabeça!" );
						Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( ReleaseIceBallLock ), from );
					}
				}
				else
				{
					if( targetIsPM && !pm.CheckAlive() )
					{
						from.SendMessage( "Por que tentar acertar alguém que já está desmaiado???" );
						Timer.DelayCall( TimeSpan.FromSeconds( 2.0 ), new TimerStateCallback( ReleaseIceBallLock ), from );
					}
					else
					{
						from.SendMessage( "Sem vandalismo, por favor." );
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
               					attacker.SendMessage("Você joga a bola de gelo e, para sua sorte, ela voa e os atinge.");
               					defender.SendMessage("Você acaba de ser atingido por uma rocha que ricocheteou!");
						damage += Utility.RandomMinMax( 1, 3 );
						break;
					}
					case 1:
					{
               					attacker.SendMessage("Você joga a bola de gelo e ela acerta no braço deles.");
               					defender.SendMessage("Você acaba de ser atingido por um golpe de raspão por uma bola de gelo!");
						damage += Utility.RandomMinMax( 2, 6 );
						break;
					}
					case 2:
					{
               					attacker.SendMessage("Você joga a bola de gelo e ela os atinge nas costas!");
               					defender.SendMessage("Você acaba de ser atingido nas costas por uma bola de gelo!");
						damage += Utility.RandomMinMax( 4, 10 );
						break;
					}
					case 3:
					{
               					attacker.SendMessage("Você joga a bola de gelo e ela atinge a rótula!");
               					defender.SendMessage("Você acaba de ser atingido na rótula por uma bola de gelo!");
						damage += Utility.RandomMinMax( 6, 10 );
						break;
					}
					case 4:
					{
               					attacker.SendMessage("Você joga a bola de gelo e ela os acerta em cheio no braço!");
               					defender.SendMessage("Você acaba de ser atingido no braço por uma bola de gelo!");
						damage += Utility.RandomMinMax( 6, 12 );
						break;
					}
					case 5:
					{
               					attacker.SendMessage("Você joga a bola de gelo e ela os atinge no peito, deixando-os sem fôlego!");
               					defender.SendMessage("Você acaba de ser atingido no peito por uma bola de gelo. Você perde o ar!");
						damage += Utility.RandomMinMax( 6, 15 );
						break;
					}
					case 6:
					{
               					attacker.SendMessage("Você joga a bola de gelo e ela atinge o tornozelo. Acho que alguém caius!");
               					defender.SendMessage("Você acaba de ser atingido no tornozelo por uma bola de gelo. Você cai!");
						damage += Utility.RandomMinMax( 6, 18 );
						break;
					}
					case 7:
					{
               					attacker.SendMessage("Você joga a bola de gelo e ela acerta na testa, deixando-os atordoados!");
               					defender.SendMessage("Você acaba de ser atingido na testa por uma bola de gelo, você perde os sentidos por um momento!");
						damage += Utility.RandomMinMax( 7, 21 );
						if( isStunned )
						{
							
               						attacker.SendMessage("Você atordoou seu alvo!!!");
							defender.SendLocalizedMessage( 1004014 ); // You have been stunned!
							defender.Freeze( TimeSpan.FromSeconds( 2.0 ) );
						}
						break;
					}
					case 8:
					{
               					attacker.SendMessage("Você joga a bola de gelo e ela atinge os olhos deles, deixando seus olhos fechados!");
               					defender.SendMessage("Você acaba de ser atingido no olho por uma bola de gelo, ela te cega por alguns momentos!");
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
               					attacker.SendMessage("Você joga a bola de gelo e ela acerta bem no meio das pernas. Você leva seu oponente a nocaute!");
               					defender.SendMessage("Você acaba de ser atingido bem na virilha por uma bola de gelo, Você se dobra e cai de joelhos!");
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
