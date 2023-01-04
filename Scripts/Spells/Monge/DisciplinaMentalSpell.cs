using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Monge
{
	public class DisciplinaMentalSpell : MongeSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
                "Disciplina Mental", "Anima Disciplina",
				269,
				0,
                Reagent.Garlic,
                Reagent.MandrakeRoot,
                Reagent.SpidersSilk
            );

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
        public override double RequiredSkill
        {
            get
            {
                return 50.0;
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Eighth;
            }
        }


        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias


        public DisciplinaMentalSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( Core.AOS )
				return true;

			if ( Caster.MagicDamageAbsorb > 0 )
			{
				Caster.SendMessage( "Your mind is already protected!" );
				return false;
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendMessage( "Your mind cannot be shielded at this time!" );
				return false;
			}

			return true;
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			if ( Caster.MagicDamageAbsorb > 0 )
			{
				Caster.SendMessage( "Your mind is already protected!" );
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendMessage( "Your mind cannot be shielded at this time!" );
			}
			else if ( CheckSequence() )
			{
				if ( Caster.BeginAction( typeof( DefensiveSpell ) ) )
				{
					int value = (int)( Caster.Skills[SkillName.Briga].Value / 2 );
					Caster.MagicDamageAbsorb = value;
					Caster.FixedParticles( 0x3039, 10, 15, 5038, 0, 2, EffectLayer.Head );
					Caster.PlaySound( 0x5BC );
				}
				else
				{
					Caster.SendMessage( "Your mind cannot be shielded at this time!" );
				}

				FinishSequence();
			}
		}
	}
}
