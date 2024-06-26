using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Server.Spells.Bardo
{
	public class PoderDaFlorSpell : BardoSpell
    {
		private static SpellInfo m_Info = new SpellInfo(
                "Poder da Flor", "Cheira a minha florzinha!",
				-1,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Third;
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

        public override double RequiredSkill
        {
            get
            {
                return 30.0;
            }
        }

        public PoderDaFlorSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public override void OnCast()
		{
			switch ( Utility.Random( 3 ))
			{
				case 0: Caster.PlaySound( Caster.Female ? 794 : 1066 ); break;
				case 1: Caster.PlaySound( Caster.Female ? 801 : 1073 ); break;
			}

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
				int damage = 1 + (int)( (Caster.Skills[SkillName.Carisma].Value / 5) + (Caster.Skills[SkillName.PoderMagico].Value / 3) );
				Caster.MovingParticles( m, 0x3818, 10, 0, false, false, 0xB44, 0, 0 );
				Caster.PlaySound( 0x025 );
                //Effects.SendLocationEffect( m.Location, m.Map, 0x122A, 20, 3, 0 );
                Effects.SendLocationEffect(m.Location, m.Map, 0x122A, 20, 4, 0);

                if ( Caster.Skills[SkillName.Carisma].Value >= Utility.RandomMinMax( 50, 300 ) && m != null )
				{
					int goo = 0;

					foreach ( Item splash in m.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter ){ goo++; } }

					if ( goo == 0 )
					{
                        //	Point3D p = m.Location; 296
                        //MonsterSplatter.AddSplatter( p.X, p.Y, p.Z, m.Map, p, Caster, "geleia venenosa", 3, 0 );
                        Point3D p = m.Location;
                        MonsterSplatter.AddSplatter(p.X, p.Y, p.Z, m.Map, p, Caster, "Àgua gelada", 4, 0);

                    }
                }

				AOS.Damage( m, Caster, damage, 50, 0, 0, 50, 0 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private PoderDaFlorSpell m_Owner;

			public InternalTarget(PoderDaFlorSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
