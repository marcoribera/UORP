using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Bardo
{
	public class CoelhoNoChapeuSpell : BardoSpell
    {
		private static SpellInfo m_Info = new SpellInfo(
				"Coelho no Chapéu", "Acho que esse coelho não serve para ensopado!?",
				-1,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3.0 ); } }
		


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
                return 10.0;
            }
        }
        public CoelhoNoChapeuSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if( (Caster.Followers + 1) > Caster.FollowersMax )
			{
				Caster.SendMessage("Você tem muitos seguidores para alcançar o chapéu.");
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
				string FoolName = "um coelho";
				int FoolPoisons = 0;
				int FoolHue = 0xBB4;
				int FoolSound = 0xA3;
				int FoolBody = 205;
				int FoolPhys = 100;
				int FoolCold = 0;
				int FoolFire = 0;
				int FoolPois = 0;
				int FoolEngy = 0;

				Server.Mobiles.SummonedJoke.MakeJoker( Caster, p, FoolPoisons, FoolName, FoolBody, FoolHue, FoolSound, FoolPhys, FoolCold, FoolFire, FoolPois, FoolEngy );

				int qty = 0;

				if ( Caster.Skills[SkillName.Begging].Value >= Utility.RandomMinMax( 1, 200 ) ){ qty++; }
				if ( Caster.Skills[SkillName.Begging].Value >= Utility.RandomMinMax( 1, 200 ) ){ qty++; }
				if ( Caster.Skills[SkillName.EvalInt].Value >= Utility.RandomMinMax( 1, 200 ) ){ qty++; }
				if ( Caster.Skills[SkillName.EvalInt].Value >= Utility.RandomMinMax( 1, 200 ) ){ qty++; }

				if ( qty > ( ( Caster.FollowersMax - Caster.Followers - 1 ) ) )
					qty = Caster.FollowersMax - Caster.Followers;

				if ( qty > 0 ){ Server.Mobiles.SummonedJoke.MakeJoker( Caster, p, FoolPoisons, FoolName, FoolBody, FoolHue, FoolSound, FoolPhys, FoolCold, FoolFire, FoolPois, FoolEngy ); }
				if ( qty > 1 ){ Server.Mobiles.SummonedJoke.MakeJoker( Caster, p, FoolPoisons, FoolName, FoolBody, FoolHue, FoolSound, FoolPhys, FoolCold, FoolFire, FoolPois, FoolEngy ); }
				if ( qty > 2 ){ Server.Mobiles.SummonedJoke.MakeJoker( Caster, p, FoolPoisons, FoolName, FoolBody, FoolHue, FoolSound, FoolPhys, FoolCold, FoolFire, FoolPois, FoolEngy ); }
				if ( qty > 3 ){ Server.Mobiles.SummonedJoke.MakeJoker( Caster, p, FoolPoisons, FoolName, FoolBody, FoolHue, FoolSound, FoolPhys, FoolCold, FoolFire, FoolPois, FoolEngy ); }

				Caster.PlaySound( Caster.Female ? 811 : 1085 );
				Caster.Say( "*oooh*" );
			}

			FinishSequence();
		}
	}
}
