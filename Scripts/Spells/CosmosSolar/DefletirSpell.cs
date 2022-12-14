using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.CosmosSolar
{
	public class DefletirSpell : CosmosSolarSpell
	{

        private static readonly SpellInfo m_Info = new SpellInfo(
          "Defletir", "Deflectere",
          203,
          0,
          Reagent.Vela,
          Reagent.Incenso,
          Reagent.PenaETinteiro);

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

        public static void EndReflect(Mobile m)
        {
            if (m_Table.Contains(m))
            {
                ResistanceMod[] mods = (ResistanceMod[])m_Table[m];

                if (mods != null)
                {
                    for (int i = 0; i < mods.Length; ++i)
                        m.RemoveResistanceMod(mods[i]);
                }

                m_Table.Remove(m);
                BuffInfo.RemoveBuff(m, BuffIcon.MagicReflection);
            }
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
					int max = (int)( GetCosmosSolarDamage( Caster ) / 4 );
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
