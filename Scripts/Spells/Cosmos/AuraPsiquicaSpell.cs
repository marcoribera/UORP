using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Cosmos
{
    public class AuraPsiquicaSpell : CosmosSpell
	{

        private static readonly SpellInfo m_Info = new SpellInfo(
           "Aceleração", "Psychica  Aureola",
           -1,
           9002,
           Reagent.Bloodmoss,
           Reagent.Garlic);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias


        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.5 ); } }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
            }
        }

        public AuraPsiquicaSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override bool CheckCast( )
		{
			return true;
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				Mobile targ = Caster;

				ResistanceMod[] mods = (ResistanceMod[])m_Table[targ];

				if ( mods == null )
				{
					targ.PlaySound( 0x1E9 );
					targ.FixedParticles( 0x376A, 9, 32, 5008, 0xB41, 0, EffectLayer.Waist );

					int phys = (int)( (targ.Skills[SkillName.Erudicao].Value / 15) + (GetCosmosDamage(Caster) / 50) );
					int engy = (int)( (targ.Skills[SkillName.Erudicao].Value / 25) + (GetCosmosDamage(Caster) / 75) );

					mods = new ResistanceMod[5]
						{
							new ResistanceMod( ResistanceType.Physical, phys ),
							new ResistanceMod( ResistanceType.Fire, -5 ),
							new ResistanceMod( ResistanceType.Cold, -5 ),
							new ResistanceMod( ResistanceType.Poison, -5 ),
							new ResistanceMod( ResistanceType.Energy, engy )
						};

					m_Table[targ] = mods;

					for ( int i = 0; i < mods.Length; ++i )
						targ.AddResistanceMod( mods[i] );
				}
				else
				{
					targ.PlaySound( 0x1ED );
					targ.FixedParticles( 0x376A, 9, 32, 5008, 0xB41, 0, EffectLayer.Waist );

					m_Table.Remove( targ );

					for ( int i = 0; i < mods.Length; ++i )
						targ.RemoveResistanceMod( mods[i] );
				}
			}

			FinishSequence();
		}

		public static void EndArmor( Mobile m )
		{
			if ( m_Table.Contains( m ) )
			{
				ResistanceMod[] mods = (ResistanceMod[]) m_Table[ m ];

				if ( mods != null )
				{
					for ( int i = 0; i < mods.Length; ++i )
						m.RemoveResistanceMod( mods[ i ] );
				}

				m_Table.Remove( m );
			}
		}
	}
}
