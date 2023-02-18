using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Bardo
{
	public class PoteDeCobrasSpell : BardoSpell
    {
		private static SpellInfo m_Info = new SpellInfo(
                "Pote de Cobras", "Pega aqui na minha cobra!",
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
public override bool CheckCast()
        {
            // Check for a musical instrument in the player's backpack
            if (!CheckInstrument())
            {
                Caster.SendMessage("Você precisa ter um instrumento musical na sua mochila para canalizar essa magia.");
                return false;
            }


            return base.CheckCast();
        }


 private bool CheckInstrument()
        {
            return Caster.Backpack.FindItemByType(typeof(BaseInstrument)) != null;
        }


        private BaseInstrument GetInstrument()
        {
            return Caster.Backpack.FindItemByType(typeof(BaseInstrument)) as BaseInstrument;
        }

        public PoteDeCobrasSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if( (Caster.Followers + 1) > Caster.FollowersMax )
			{
				Caster.SendMessage("Você tem muitos seguidores para abrir essa lata.");
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
				string FoolName = "uma cobra";
				int FoolPoisons = 1;
				int FoolHue = Server.Misc.RandomThings.GetRandomColor(0);
				int FoolSound = 0xDB;
				int FoolBody = 52;
				int FoolPhys = 50;
				int FoolCold = 0;
				int FoolFire = 0;
				int FoolPois = 50;
				int FoolEngy = 0;

				Server.Mobiles.SummonedJoke.MakeJoker( Caster, p, FoolPoisons, FoolName, FoolBody, FoolHue, FoolSound, FoolPhys, FoolCold, FoolFire, FoolPois, FoolEngy );

				int qty = 0;

				if ( Caster.Skills[SkillName.Carisma].Value >= Utility.RandomMinMax( 1, 200 ) ){ qty++; }
				if ( Caster.Skills[SkillName.PoderMagico].Value >= Utility.RandomMinMax( 1, 200 ) ){ qty++; }
				if ( Caster.Skills[SkillName.PoderMagico].Value >= Utility.RandomMinMax( 1, 200 ) ){ qty++; }

				if ( qty > ( ( Caster.FollowersMax - Caster.Followers - 1 ) ) )
					qty = Caster.FollowersMax - Caster.Followers;

				if ( qty > 0 ){ FoolHue = Server.Misc.RandomThings.GetRandomColor(0); Server.Mobiles.SummonedJoke.MakeJoker( Caster, p, FoolPoisons, FoolName, FoolBody, FoolHue, FoolSound, FoolPhys, FoolCold, FoolFire, FoolPois, FoolEngy ); }
				if ( qty > 1 ){ FoolHue = Server.Misc.RandomThings.GetRandomColor(0); Server.Mobiles.SummonedJoke.MakeJoker( Caster, p, FoolPoisons, FoolName, FoolBody, FoolHue, FoolSound, FoolPhys, FoolCold, FoolFire, FoolPois, FoolEngy ); }
				if ( qty > 2 ){ FoolHue = Server.Misc.RandomThings.GetRandomColor(0); Server.Mobiles.SummonedJoke.MakeJoker( Caster, p, FoolPoisons, FoolName, FoolBody, FoolHue, FoolSound, FoolPhys, FoolCold, FoolFire, FoolPois, FoolEngy ); }

				Caster.PlaySound( Caster.Female ? 793 : 1065 );
				Caster.Say( "*gasp!*" );
			}

			FinishSequence();
		}
	}
}
