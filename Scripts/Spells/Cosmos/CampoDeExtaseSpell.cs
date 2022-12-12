using System;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;
using Server.Spells.Chivalry;

namespace Server.Spells.Cosmos
{
    public class CampoDeExtaseSpell : CosmosSpell
	{
        private static readonly SpellInfo m_Info = new SpellInfo(
           "Aceleração", "Campus Immobilio",
           -1,
           9002,
           Reagent.Bloodmoss,
           Reagent.Garlic);
      
        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias
        public int CirclePower = 5;

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.5); } }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Fifth;
            }
        }

        public CampoDeExtaseSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
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
			else if ( m.Frozen || m.Paralyzed || ( m.Spell != null && m.Spell.IsCasting ) )
			{
				Caster.SendLocalizedMessage( 1061923 ); // The target is already frozen.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( CirclePower, Caster, ref m );

				double duration;

				int secs = (int)((GetCosmosDamage( Caster )/25) - (GetResistSkill( m ) / 10) + (Caster.Skills[SkillName.PoderMagico].Value / 2) );
				
				if( !Core.SE )
					secs += 2;

				if ( !m.Player )
					secs *= 3;

				if ( secs < 0 )
					secs = 0;

				duration = secs;

				m.Paralyze( TimeSpan.FromSeconds( duration ) );

				m.PlaySound( 0x204 );
				m.FixedEffect( 0x376A, 6, 1, 0xB41, 0 );

				HarmfulSpell( m );
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private CampoDeExtaseSpell m_Owner;

			public InternalTarget(CampoDeExtaseSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
