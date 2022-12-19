using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.CosmosLunar
{
	public class ApertoDaMorteSpell : CosmosLunarSpell

    {
        private static readonly SpellInfo m_Info = new SpellInfo(
           "Aperto da Morte", "Mors Tenaci",
           203,
           0,
           Reagent.NoxCrystal,
           Reagent.PenaETinteiro);

        public override int EficienciaMagica(Mobile caster) { return 4; } //Servirá para calcular o modificador na eficiência das magias




        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.5); } }
        public override double RequiredSkill { get { return 10.0; } }


        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
            }
        }


        public ApertoDaMorteSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.SendMessage("Quem você quer estrangular?");
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) && CheckFizzle() )
			{
				TimeSpan duration = TimeSpan.FromSeconds( GetCosmosLunarDamage(Caster) / 5 );

				Point3D grip = new Point3D( m.X+1, m.Y+1, m.Z+30 );
				Effects.SendLocationParticles(EffectItem.Create(grip, m.Map, EffectItem.DefaultDuration), 0x3818, 9, 32, 107 , 0, 5022, 0);
				m.PlaySound( 0x65A );

				int min = 5;
				int max = ( (int)( GetCosmosLunarDamage(Caster) / 25 ) + 5 );
				AOS.Damage( m, Caster, Utility.RandomMinMax( min, max ), true, 100, 0, 0, 0, 0 );

				Caster.DoHarmful( m );

				new GripTimer( m, duration, min, max, Caster ).Start();

				HarmfulSpell( m );
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private ApertoDaMorteSpell m_Owner;

			public InternalTarget( ApertoDaMorteSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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

		public class GripTimer : Timer
		{
			private Mobile m_m;
			private Mobile m_Caster;
			private DateTime m_Expire;
			private int m_Time;
			private int m_Min;
			private int m_Max;

			public GripTimer( Mobile sleeper, TimeSpan duration, int min, int max, Mobile caster ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = sleeper;
				m_Caster = caster;
				m_Expire = DateTime.UtcNow + duration;
				m_Time = 0;
				m_Min = min;
				m_Max = max;
			}

			protected override void OnTick()
			{
				m_Time++;
				if ( m_Time > 60 && m_m != null && m_Caster != null )
				{
					m_Time = 0;
					AOS.Damage( m_m, m_Caster, Utility.RandomMinMax( m_Min, m_Max ), true, 100, 0, 0, 0, 0 );
					m_Caster.DoHarmful( m_m );
					m_Caster.RevealingAction();
					Point3D grip = new Point3D( m_m.X+1, m_m.Y+1, m_m.Z+30 );
					Effects.SendLocationParticles(EffectItem.Create(grip, m_m.Map, EffectItem.DefaultDuration), 0x3818, 9, 32, 107, 0, 5022, 0);
					m_m.PlaySound( 0x65A );
				}

				if ( DateTime.UtcNow >= m_Expire )
				{
					Stop();
				}
			}
		}
	}
}
