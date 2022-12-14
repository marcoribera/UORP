

using System;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server;
using System.Text;
using Server.Mobiles;
using System.Collections.Generic;
using System.Collections;




namespace Server.Spells.CosmosSolar
{
	public class AceleracaoSpell : CosmosSolarSpell
    {

        private static readonly SpellInfo m_Info = new SpellInfo(
           "Aceleração", "Celeritas Fort",
           -1,
           0,
           Reagent.SulfurousAsh,
           Reagent.PenaETinteiro);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias

      
		
		
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.5 ); } }
		
		

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Third;
            }
        }

        public AceleracaoSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public static Hashtable TableCosmosSolarRunning = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return (TableCosmosSolarRunning[m] != null );
		}

		public static bool UnderEffect( Mobile m )
		{
			return TableCosmosSolarRunning.Contains( m );
		}

		public static void RemoveEffect( Mobile m )
		{
			m.Send(SpeedControl.Disable);
            TableCosmosSolarRunning.Remove( m );
			m.EndAction( typeof(AceleracaoSpell) );
		}

		public override void OnCast()
		{
			Item shoes = Caster.FindItemOnLayer( Layer.Shoes );

            if ( Caster.Mounted )
            {
                Caster.SendMessage( "Você não pode usar seu poder enquanto estiver montado" );
            }
			
			else if ( CheckFizzle() )
			{
				if ( !Caster.CanBeginAction( typeof(AceleracaoSpell) ) )
				{
                    AceleracaoSpell.RemoveEffect( Caster );
				}

				int TotalTime = (int)(GetCosmosSolarDamage( Caster ) * 4 );
					if ( TotalTime < 600 ){ TotalTime = 600; }
                TableCosmosSolarRunning[Caster] = SpeedControl.MountSpeed;
				Caster.Send(SpeedControl.MountSpeed);
				new InternalTimer( Caster, TimeSpan.FromSeconds( TotalTime ) ).Start();
				Caster.BeginAction( typeof(AceleracaoSpell) );
				Point3D air = new Point3D( ( Caster.X+1 ), ( Caster.Y+1 ), ( Caster.Z+5 ) );
				Effects.SendLocationParticles(EffectItem.Create(air, Caster.Map, EffectItem.DefaultDuration), 0x5590, 9, 32, 0, 0, 5022, 0);
				Caster.PlaySound( 0x64F );
			}

            FinishSequence();
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile Caster, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = Caster;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
                    AceleracaoSpell.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}
}
