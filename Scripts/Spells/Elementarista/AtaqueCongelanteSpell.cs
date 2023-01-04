using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Elementarista
{
	public class AtaqueCongelanteSpell : ElementaristaSpell
    {

        private static readonly SpellInfo m_Info = new SpellInfo(
           "Ataque Congelante", "Frigore Impetum",
           245,
           9042,
           Reagent.SulfurousAsh,
           Reagent.PenaETinteiro);


        public AtaqueCongelanteSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public int CirclePower = 7;

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





        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2 ); } }
				

		

		

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
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( CirclePower, Caster, ref m );

				double damage = DamagingSkill( Caster )/2;
					if ( damage > 125 ){ damage = 125.0; }
					if ( damage < 35 ){ damage = 35.0; }

				m.FixedParticles( 0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot );
				m.PlaySound( 0x64F );

				SpellHelper.Damage( this, m, damage, 0, 0, 100, 0, 0 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private AtaqueCongelanteSpell m_Owner;

			public InternalTarget(AtaqueCongelanteSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
