using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;

namespace Server.Spells.Cosmos
{
    public class ToqueCalmanteSpell : CosmosSpell
	{

        private static readonly SpellInfo m_Info = new SpellInfo(
                  "Toque Calmante", "Tactus Quietantis",
                  204,
                  0,
                  Reagent.Bloodmoss,
                  Reagent.Garlic);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias




        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.5); } }



        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
            }
        }

        public ToqueCalmanteSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }


        public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( m is BaseCreature && ((BaseCreature)m).IsAnimatedDead )
			{
				Caster.SendLocalizedMessage( 1061654 ); // You cannot heal that which is not alive.
			}
			else if ( m.IsDeadBondedPet )
			{
				Caster.SendLocalizedMessage( 1060177 ); // You cannot heal a creature that is already dead!
			}
			else if ( m is Golem )
			{
				Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 500951 ); // You cannot heal that.
			}
			else if ( MortalStrike.IsWounded( m ) )
			{
				if ( GetCosmosDamage( Caster ) > Utility.RandomMinMax( 185, 750 ) )
				{
					MortalStrike.EndWound( m );
					BuffInfo.RemoveBuff( m, BuffIcon.MortalStrike );
				}
				else
				{
					Caster.LocalOverheadMessage( MessageType.Regular, 0x22, (Caster == m) ? 1005000 : 1010398 );
				}
			}
			else if ( m.Poisoned )
			{
				double healing = Caster.Skills[SkillName.PoderMagico].Value;
				double anatomy = (double)( GetCosmosDamage( Caster ) / 2 );
				double chance = ((healing - 30.0) / 50.0) - (m.Poison.Level * 0.1);

				if ( healing >= 60.0 && anatomy >= 60.0 && chance > Utility.RandomDouble() )
				{
					if ( m.CurePoison( Caster ) )
					{
						Caster.SendLocalizedMessage( 1010058 );
						if ( Caster != m ){ m.SendLocalizedMessage( 1010059 ); }
					}
				}
				else
				{
					Caster.LocalOverheadMessage( MessageType.Regular, 0x22, 1010060 );
				}
			}
			else if ( BleedAttack.IsBleeding( m ) )
			{
				if ( GetCosmosDamage( Caster ) > Utility.RandomMinMax( 185, 750 ) )
				{
					BleedAttack.EndBleed( m, false );
				}
				else
				{
					Caster.LocalOverheadMessage( MessageType.Regular, 0x22, 1060159 );
				}
			}
			else if ( CheckBSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				int toHeal = (int)( Caster.Skills[SkillName.PoderMagico].Value * 0.2 ) + (int)( GetCosmosDamage( Caster ) * 0.1 );
				toHeal += Utility.Random( 1, 10 );
			

				SpellHelper.Heal( toHeal, m, Caster );

				m.FixedParticles( 0x376A, 9, 32, 5030, 0xB41, 0, EffectLayer.Waist );
				m.PlaySound( 0x202 );
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private ToqueCalmanteSpell m_Owner;

			public InternalTarget(ToqueCalmanteSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Beneficial )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
