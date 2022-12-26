using System;
using Server.Targeting;
using Server.Network;
using Server;

namespace Server.Spells.Elementarista
{
	public class CongelarSpell : ElementaristaSpell
    {



        private static readonly SpellInfo m_Info = new SpellInfo(
         "Congelar", "Frigidus",
         236,
         9031,

         Reagent.Nightshade,
         Reagent.SpidersSilk);
        public int CirclePower = 1;


        public CongelarSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }


        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias


        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
            }
        }

        public override double RequiredSkill
        {
            get
            {
                return 20.0;
            }
        }





        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.50); } }








        

	

        public override bool DelayedDamageStacking { get { return !Core.AOS; } }

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				Mobile source = Caster;

				SpellHelper.Turn( source, m );

				SpellHelper.CheckReflect( CirclePower, ref source, ref m );

				double damage = DamagingSkill( Caster )/3;
					if ( damage > 80 ){ damage = 80.0; }
					if ( damage < 1 ){ damage = 1.0; }

				source.MovingParticles( m, 0x28EF, 5, 0, false, false, 0xB77 , 0, 3600, 0, 0, 0 );
				source.PlaySound( 0x1E5 );

				SpellHelper.Damage( this, m, damage, 0, 0, 100, 0, 0 );
			
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private CongelarSpell m_Owner;

			public InternalTarget(CongelarSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
