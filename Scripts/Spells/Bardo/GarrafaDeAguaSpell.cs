using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;
using Server.Misc;
using Ultima;

namespace Server.Spells.Bardo
{
	public class GarrafaDeAguaSpell : BardoSpell
    {
		private static SpellInfo m_Info = new SpellInfo(
				"Garrafa de Água", "Se molhou??",
				-1,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Second;
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
                return 20.0;
            }
        }
        public GarrafaDeAguaSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
				int damage = 1 + (int)( (Caster.Skills[SkillName.Caos].Value / 5) + (Caster.Skills[SkillName.PoderMagico].Value / 3) );
				//Caster.MovingParticles( m, 0x377C, 7, 0, false, false, 0x84B, 0, 0 );  /// testando as particulas. coloquei animação de agua
                Effects.SendTargetParticles(m, 0x377C, 10, 20, 296, 0, 0x84B, EffectLayer.Head, 0);
                Caster.PlaySound( 0x025 );
				//Effects.SendLocationEffect( m.Location, m.Map, 0x122A, 20 );
                Effects.SendLocationEffect(m.Location, m.Map, 0x122A, 20, 296, 0);

                if ( Caster.Skills[SkillName.Caos].Value >= Utility.RandomMinMax( 50, 300 ) && m != null )
				{
					int goo = 0;

					foreach ( Item splash in m.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter ){ goo++; } }

					if ( goo == 0 )
					{
						Point3D p = m.Location;
						MonsterSplatter.AddSplatter( p.X, p.Y, p.Z, m.Map, p, Caster, "Àgua gelada", 296, 0 );
					}
				}

				AOS.Damage( m, Caster, damage, 50, 0, 50, 0, 0 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private GarrafaDeAguaSpell m_Owner;

			public InternalTarget(GarrafaDeAguaSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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