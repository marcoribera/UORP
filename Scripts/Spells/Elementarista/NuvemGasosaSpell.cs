using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Elementarista
{
	public class NuvemGasosaSpell : ElementaristaSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
         "Nuvem Gasosa", "Pneuma Nubes",
         260,
         9032,
         Reagent.DragonBlood,
         Reagent.Vela,
         Reagent.SpidersSilk);


        public NuvemGasosaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }


        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias


        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Fifth;
            }
        }

        public override double RequiredSkill
        {
            get
            {
                return 20.0;
            }
        }





        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(2.25); } }




        


		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( (Caster.Followers + 4) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			Caster.SendMessage("Escolha onde você vai liberar a nuvem de gás.");
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			Map map = Caster.Map;

			SpellHelper.GetSurfaceTop( ref p );

			if ( map == null || !map.CanSpawnMobile( p.X, p.Y, p.Z ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				double time = DamagingSkill( Caster )/1.2;
					if ( time > 200 ){ time = 200.0; }
					if ( time < 60 ){ time = 60.0; }

				TimeSpan duration = TimeSpan.FromSeconds( time );

				BaseCreature.Summon( new GasCloud(), false, Caster, new Point3D( p ), 0x231, duration );
				
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private NuvemGasosaSpell m_Owner;

			public InternalTarget(NuvemGasosaSpell owner ) : base( Core.ML ? 10 : 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
					m_Owner.Target( (IPoint3D)o );
			}

			protected override void OnTargetOutOfLOS( Mobile from, object o )
			{
				from.SendLocalizedMessage( 501943 ); // Target cannot be seen. Try again.
				from.Target = new InternalTarget( m_Owner );
				from.Target.BeginTimeout( from, TimeoutTime - DateTime.UtcNow );
				m_Owner = null;
			}

			protected override void OnTargetFinish( Mobile from )
			{
				if ( m_Owner != null )
					m_Owner.FinishSequence();
			}
		}
	}
}
