using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.CosmosLunar
{
    public class ExplosaoPsiquicaSpell : CosmosLunarSpell
    {


        private static readonly SpellInfo m_Info = new SpellInfo(
           "Explosão Psíquica", "Psychicae Fracor",
           -1,
           0,
           Reagent.Bloodmoss,
           Reagent.GraveDust);

        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias




        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.5); } }

        public override double RequiredSkill { get { return 60.0; } }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Fifth;
            }
        }


        public ExplosaoPsiquicaSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		private void AosDelay_Callback( object state )
		{
			object[] states = (object[])state;
			Mobile caster = (Mobile)states[0];
			Mobile target = (Mobile)states[1];
			Mobile defender = (Mobile)states[2];
			int min = (int)states[3];
			int max = (int)states[4];

			if ( caster.HarmfulCheck( defender ) )
			{
				AOS.Damage( target, caster, Utility.RandomMinMax( min, max ), 0, 0, 0, 0, 100 );

				Point3D boom = new Point3D( target.X+1, target.Y+2, target.Z+5);

                target.FixedParticles(0xA7E3, 10, 15, 5038, 308, 2, EffectLayer.Head);
                target.PlaySound(0x213);
                target.PlaySound( 0x658 );
			}
		}

		public override bool DelayedDamage{ get{ return !Core.AOS; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( Caster.CanBeHarmful( m ) && CheckSequence() && CheckFizzle() )
			{
				Mobile from = Caster, target = m;

				int min = 26;
				int max = (int)( GetCosmosLunarDamage( Caster ) / 3 );

				if ( max > 125 )
					max = 125;

				Timer.DelayCall( TimeSpan.FromSeconds( 0.1 ),
					new TimerStateCallback( AosDelay_Callback ),
					new object[]{ Caster, target, m, min, max } );
			}

			FinishSequence();
		}

		public override double GetSlayerDamageScalar( Mobile target )
		{
			return 1.0; //This spell isn't affected by slayer spellbooks
		}

		private class InternalTarget : Target
		{
			private ExplosaoPsiquicaSpell m_Owner;

			public InternalTarget(ExplosaoPsiquicaSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
