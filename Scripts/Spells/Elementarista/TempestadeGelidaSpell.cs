using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Elementarista
{
	public class TempestadeGelidaSpell : ElementaristaSpell
    {



        private static readonly SpellInfo m_Info = new SpellInfo(
         "Tempestade Gélida", "Torpore Tempestate",
         230,
         9041,

         Reagent.Incenso,
         Reagent.PenaETinteiro);


        public TempestadeGelidaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }
        public int CirclePower = 6;


        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias


        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Sixth;
            }
        }

        public override double RequiredSkill
        {
            get
            {
                return 20.0;
            }
        }





        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(1.75); } }




		

		public override bool DelayedDamageStacking { get { return !Core.AOS; } }

		public override void OnCast()
		{
			Caster.SendMessage( "Escolha onde a tempestade vai cair." );
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage { get { return false; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( Caster.CanBeHarmful( m ) && CheckSequence() )
			{
				Mobile attacker = Caster, defender = m;

				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int) CirclePower, Caster, ref m );

				InternalTimer t = new InternalTimer( this, attacker, defender, m );
				t.Start();
			}

			FinishSequence();
		}

		private class InternalTimer : Timer
		{
			private ElementaristaSpell m_Spell;
			private Mobile m_Target;
			private Mobile m_Attacker, m_Defender;

			public InternalTimer(ElementaristaSpell spell, Mobile attacker, Mobile defender, Mobile target ): base( TimeSpan.FromSeconds( Core.AOS ? 3.0 : 2.5 ) )
			{
				m_Spell = spell;
				m_Attacker = attacker;
				m_Defender = defender;
				m_Target = target;

				if ( m_Spell != null )
					m_Spell.StartDelayedDamageContext( attacker, this );

				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				if ( m_Attacker.HarmfulCheck( m_Defender ) )
				{
					double damage = DamagingSkill( m_Attacker )/2;
						if ( damage > 125 ){ damage = 125.0; }
						if ( damage < 28 ){ damage = 28.0; }

					m_Target.FixedParticles( Utility.RandomList(0x384E,0x3859), 20, 10, 5044, 0, 0, EffectLayer.Head );

					m_Target.PlaySound( 0x64F );

					SpellHelper.Damage( m_Spell, m_Target, damage, 0, 0, 100, 0, 0 );

					if ( m_Spell != null )
						m_Spell.RemoveDelayedDamageContext( m_Attacker );
				}
			}
		}

		private class InternalTarget : Target
		{
			private TempestadeGelidaSpell m_Owner;

			public InternalTarget(TempestadeGelidaSpell owner ): base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile) o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
