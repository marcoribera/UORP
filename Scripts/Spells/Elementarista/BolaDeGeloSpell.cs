using System;
using Server.Targeting;
using Server.Network;
using Server;

namespace Server.Spells.Elementarista
{
	public class BolaDeGeloSpell : ElementaristaSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
          "Bola de Gelo", "Glacies Esfera",
          236,
          9031,
          Reagent.Nightshade,
          Reagent.SpidersSilk);


        public BolaDeGeloSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public int CirclePower = 3;

        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias


        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Third;
            }
        }

        public override double RequiredSkill
        {
            get
            {
                return 20.0;
            }
        }





        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(1.0); } }






     
		
		

		

		

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
					if ( damage < 2 ){ damage = 2.0; }

				source.MovingParticles( m, 0x36E4, 7, 0, false, true, 0x480, 0, 9502, 4019, 0x160, 0 );
				source.PlaySound( 0x650 );

				SpellHelper.Damage( this, m, damage, 0, 0, 100, 0, 0 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private BolaDeGeloSpell m_Owner;

			public InternalTarget(BolaDeGeloSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
