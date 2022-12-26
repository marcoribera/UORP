using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Elementarista
{
	public class AvalancheSpell : ElementaristaSpell
    {

        private static readonly SpellInfo m_Info = new SpellInfo(
          "Avalanche", "Lapsus Glaciem",
          233,
          9042,
          Reagent.Nightshade,
          Reagent.SpidersSilk);


        public AvalancheSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }


        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias


        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Seventh;
            }
        }

        public override double RequiredSkill
        {
            get
            {
                return 20.0;
            }
        }





        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(2); } }







       
		
	

		public override void OnCast()
		{
			Caster.SendMessage( "Escolha onde conjurar a magia." );
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );

				if ( p is Item )
					p = ((Item)p).GetWorldLocation();

				List<Mobile> targets = new List<Mobile>();

				Map map = Caster.Map;

				if ( map != null )
				{
					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), 5 );

					foreach ( Mobile m in eable )
					{
						Mobile pet = m;
						if ( m is BaseCreature )
							pet = ((BaseCreature)m).GetMaster();

						if ( Caster.Region == m.Region && Caster != m && Caster != pet && Caster.InLOS( m ) && m.Blessed == false && Caster.CanBeHarmful( m, true ) )
						{
							targets.Add( m );
						}
					}

					eable.Free();
				}

				double damage = DamagingSkill( Caster )/2;
					if ( damage > 125 ){ damage = 125.0; }
					if ( damage < 35 ){ damage = 35.0; }

				if ( targets.Count > 0 )
				{
					Effects.PlaySound( p, Caster.Map, 0x65A );

					damage = (damage * 2) / targets.Count;
						
					double toDeal;
					for ( int i = 0; i < targets.Count; ++i )
					{
						Mobile m = targets[i];

						toDeal  = damage;
						Caster.DoHarmful( m );
						SpellHelper.Damage( this, m, toDeal, 0, 0, 100, 0, 0 );

						Point3D blast1 = new Point3D( ( m.X ), ( m.Y ), m.Z );
						Point3D blast2 = new Point3D( ( m.X-1 ), ( m.Y ), m.Z );
						Point3D blast3 = new Point3D( ( m.X+1 ), ( m.Y ), m.Z );
						Point3D blast4 = new Point3D( ( m.X ), ( m.Y-1 ), m.Z );
						Point3D blast5 = new Point3D( ( m.X ), ( m.Y+1 ), m.Z );

						Effects.SendLocationEffect( blast1, m.Map, Utility.RandomList(0x17CD, 0x1774, 0xA674), 850, 100, 0x480, 0 );
						Effects.SendLocationEffect( blast2, m.Map, Utility.RandomList(0x17CD, 0x1774, 0xA674), 850, 100, 0x480, 0 );
						Effects.SendLocationEffect( blast3, m.Map, Utility.RandomList(0x17CD, 0x1774, 0xA674), 850, 100, 0x480, 0 );
						Effects.SendLocationEffect( blast4, m.Map, Utility.RandomList(0x17CD, 0x1774, 0xA674), 850, 100, 0x480, 0 );
						Effects.SendLocationEffect( blast5, m.Map, Utility.RandomList(0x17CD, 0x1774, 0xA674), 850, 100, 0x480, 0 );
						Effects.PlaySound( m.Location, m.Map, 0x65A );
					}
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private AvalancheSpell m_Owner;

			public InternalTarget(AvalancheSpell owner ) : base( Core.ML ? 10 : 12, true, TargetFlags.None )
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
