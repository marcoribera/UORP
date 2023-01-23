using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Bardo
{
	public class PresentesSurpresaSpell : BardoSpell
    {
		private static SpellInfo m_Info = new SpellInfo(
                "Presentes Surpesa", "Surpresa!!!!",
				-1,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3.0 ); } }
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
                return 30.0;
            }
        }

        public PresentesSurpresaSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if( (Caster.Followers + 3) > Caster.FollowersMax )
			{
				Caster.SendMessage("Você tem muitos seguidores para conseguir embrulhar um presente.");
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			Map map = Caster.Map;

			Point3D p = Caster.Location;

			if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				string FoolName = "um presente";
				int FoolHue = 0;
				int FoolBody = Utility.RandomList( 1027, 1028, 1029, 1030 );
				int FoolFroze = 1;

				Caster.Hidden = true;
				Server.Mobiles.SummonedPrank.MakePrankster( Caster, p, FoolName, FoolBody, FoolHue, FoolFroze );
				Caster.Hidden = false;

				Caster.PlaySound( Caster.Female ? 794 : 1066 );
				Caster.Say( "*gargalha*" );
			}

			FinishSequence();
		}
	}
}
