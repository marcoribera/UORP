using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Cosmos
{
	public class DefletirSpell : CosmosSpell
	{

        private static readonly SpellInfo m_Info = new SpellInfo(
          "Defletir", "Deflectere",
          -1,
          9002,
          Reagent.Bloodmoss,
          Reagent.Garlic);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias


        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Sixth;
            }
        }

        public DefletirSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

        public override bool CheckCast()
		{
			if ( !base.CheckCast(  ) )
				return false;

			if ( Caster.MagicDamageAbsorb > 0 )
			{
				Caster.SendMessage( "Sua essência está protegida!" );
				return false;
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendMessage( "Sua essência não consegue defletir mais nesse momento!" );
				return false;
			}

			return true;
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			if ( Caster.MagicDamageAbsorb > 0 )
			{
				Caster.SendMessage( "Sua essência já está protegida!" );
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendMessage("Sua essência não consegue defletir mais nesse momento!");
			}
			else if ( CheckSequence() )
			{
				if ( Caster.BeginAction( typeof( DefensiveSpell ) ) && CheckFizzle() )
				{
					int min = 15;
					int max = (int)( GetCosmosDamage( Caster ) / 4 );
					Caster.MagicDamageAbsorb = Utility.RandomMinMax( min, max );
					Point3D air = new Point3D( ( Caster.X+1 ), ( Caster.Y+1 ), ( Caster.Z+5 ) );
					Effects.SendLocationParticles(EffectItem.Create(air, Caster.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 0xB41, 0, 5022, 0);
					Effects.PlaySound(Caster.Location, Caster.Map, 0x0F9);
				}
				else
				{
					Caster.SendMessage("Sua essência não consegue defletir mais nesse momento!");
				}

				FinishSequence();
			}
		}
	}
}
