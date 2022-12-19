using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Regions;
using Server.Mobiles;

namespace Server.Spells.CosmosLunar
{
	public class RaioSpell : CosmosLunarSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
           "Raio", "Tempestas",
           203,
           0,
          Reagent.NoxCrystal,
           Reagent.Vela);

        public override int EficienciaMagica(Mobile caster) { return 4; } //Servirá para calcular o modificador na eficiência das magias

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.5); } }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Third;
            }
        }

        public RaioSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
        public override double RequiredSkill { get { return 50.0; } }
        public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() && CheckFizzle() )
			{
				if ( p is Item )
					p = ((Item)p).GetWorldLocation();

				ArrayList targets = new ArrayList();

				Map map = Caster.Map;

				if ( map != null )
				{
					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), 5 );

					foreach ( Mobile m in eable )
					{
						Mobile pet = m;

						if ( Caster.Region == m.Region && Caster != m )
						{
							if ( m is BaseCreature )
								pet = ((BaseCreature)m).GetMaster();

							if ( Caster != pet )
							{
								targets.Add( m );
							}
						}
					}

					eable.Free();
				}

				int min = 20;
				int max = (int)(GetCosmosLunarDamage( Caster ) / 5);

				int damage = Utility.RandomMinMax( min, max );

				int foes = (int)(GetCosmosLunarDamage( Caster ) / 50);
					if ( foes < 1 ){ foes = 1; }

				if ( targets.Count > 0 )
				{
					if ( targets.Count == 1 ){ damage = (int)( damage * 1.0 ); }
					else if ( targets.Count == 2 ){ damage = (int)( damage * 0.90 ); }
					else if ( targets.Count == 3 ){ damage = (int)( damage * 0.80 ); }
					else if ( targets.Count == 4 ){ damage = (int)( damage * 0.70 ); }
					else if ( targets.Count == 5 ){ damage = (int)( damage * 0.60 ); }
					else if ( targets.Count == 6 ){ damage = (int)( damage * 0.50 ); }
					else { damage = (int)( damage * 0.40 ); }

					for ( int i = 0; i < targets.Count; ++i )
					{
						if ( foes > 0 )
						{
							foes--;

							Mobile m = (Mobile)targets[i];

							Region house = m.Region;

							if( !(house is Regions.HouseRegion) )
							{
								Caster.DoHarmful( m );
								AOS.Damage( m, Caster, damage, 0, 0, 0, 0, 100 );

								Point3D blast = new Point3D( ( m.X ), ( m.Y ), m.Z+10 );
                                //Effects.SendLocationEffect( blast, m.Map, 0x2A4E, 30, 10, 0xB00, 0 );
                                Effects.SendBoltEffect(m, true, 0, false);
                                m.PlaySound( 0x029 );
							}
						}
					}
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private RaioSpell m_Owner;

			public InternalTarget( RaioSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
