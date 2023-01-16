using System;
using Server;
using System.Collections;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Monge
{
	public class CorrerNoVentoSpell : MongeSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
                "Correr no Vento", "Currunt Ventum",
				269,
				0,
                Reagent.Garlic,
                Reagent.Bloodmoss
            );

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
        public override double RequiredSkill
        {
            get
            {
                return 90.0;
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Ninth;
            }
        }


        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public CorrerNoVentoSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public static Hashtable TableWindRunning = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableWindRunning[m] != null );
		}

		public static bool UnderEffect( Mobile m )
		{
			return TableWindRunning.Contains( m );
		}

		public static void RemoveEffect( Mobile m )
		{
			m.Send(SpeedControl.Disable);
			TableWindRunning.Remove( m );
			m.EndAction( typeof(CorrerNoVentoSpell) );
		}

		public override void OnCast()
		{
			Item shoes = Caster.FindItemOnLayer( Layer.Shoes );

            if ( Caster.Mounted )
            {
                Caster.SendMessage( "You cannot use this ability while on a mount!" );
            }
			
			else
			{
				if ( !Caster.CanBeginAction( typeof(CorrerNoVentoSpell) ) )
				{
                    CorrerNoVentoSpell.RemoveEffect( Caster );
				}

				int TotalTime = (int)( Caster.Skills[SkillName.Briga].Value * 5 );
				TableWindRunning[Caster] = SpeedControl.MountSpeed;
				Caster.Send(SpeedControl.MountSpeed);
				new InternalTimer( Caster, TimeSpan.FromSeconds( TotalTime ) ).Start();
				Caster.BeginAction( typeof(CorrerNoVentoSpell) );
				Point3D air = new Point3D( ( Caster.X+1 ), ( Caster.Y+1 ), ( Caster.Z+5 ) );
				Effects.SendLocationParticles(EffectItem.Create(air, Caster.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 0, 0, 5022, 0);
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
                    CorrerNoVentoSpell.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}
}
