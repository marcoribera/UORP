using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;

namespace Server.Spells.Bardo
{
	public class PalhacosSpell : BardoSpell
    {
		private static SpellInfo m_Info = new SpellInfo(
				"Palhaços", "Que comece o show!",
				-1,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3.0 ); } }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Fifth;
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
                return 45.0;
            }
        }

        public PalhacosSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if( (Caster.Followers + 1) > Caster.FollowersMax )
			{
				Caster.SendMessage("Você tem muitos seguidores para fazer essa palhaçada.");
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( Caster.Mounted )
			{
				Caster.SendLocalizedMessage( 1063132 ); // You cannot use this ability while mounted.
			}
			else if ( (Caster.Followers + 1) > Caster.FollowersMax )
			{
				Caster.SendMessage("Você tem muitos seguidores para fazer palhaçadas.");
			}
			else if( TransformationSpellHelper.UnderTransformation( Caster, typeof( HorrificBeastSpell ) ) )
			{
				Caster.SendMessage("Você não pode ficar fazendo palhaçada enquanto está assim.");
			}
			else if ( CheckSequence() )
			{
				Effects.SendLocationParticles( EffectItem.Create( Caster.Location, Caster.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 0, 0, 5042, 0 );

				Caster.PlaySound( Caster.Female ? 780 : 1051 );
				Caster.Say( "*aplaude*" );

				new Palhaco( Caster ).MoveToWorld( Caster.Location, Caster.Map );

				int qty = 0;

				if ( Caster.Skills[SkillName.Caos].Value >= Utility.RandomMinMax( 1, 200 ) ){ qty++; }
				if ( Caster.Skills[SkillName.PoderMagico].Value >= Utility.RandomMinMax( 1, 100 ) ){ qty++; }

				if ( qty > ( ( Caster.FollowersMax - Caster.Followers - 1 ) ) )
					qty = Caster.FollowersMax - Caster.Followers;

				if ( qty > 0 ){ new Palhaco( Caster ).MoveToWorld( Caster.Location, Caster.Map ); }
				if ( qty > 1 ){ new Palhaco( Caster ).MoveToWorld( Caster.Location, Caster.Map ); }
			}

			FinishSequence();
		}
	}
}

namespace Server.Mobiles
{
	public class Palhaco : BaseCreature
	{
		private Mobile m_Caster;

		public Palhaco( Mobile caster ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			m_Caster = caster;

			Body = caster.Body;

			Hue = caster.Hue;
			Female = caster.Female;

			Name = "Bobo da Corte";
			NameHue = caster.NameHue;

			Title = caster.Title;
			Kills = caster.Kills;
            AddItem(new FancyShirt(Utility.RandomGreenHue()));
            AddItem(new LongPants(Utility.RandomYellowHue()));
            AddItem(new JesterHat(Utility.RandomPinkHue()));
            AddItem(new Cloak(Utility.RandomPinkHue()));
            AddItem(new Sandals());

            HairItemID = caster.HairItemID;
			HairHue = caster.HairHue;

			FacialHairItemID = caster.FacialHairItemID;
			FacialHairHue = caster.FacialHairHue;

			for ( int i = 0; i < caster.Skills.Length; ++i )
			{
				Skills[i].Base = caster.Skills[i].Base;
				Skills[i].Cap = caster.Skills[i].Cap;
			}

		/*	for( int i = 0; i < caster.Items.Count; i++ )
			{
				AddItem( ClownItem( caster.Items[i] ) );
			}*/

			Warmode = false;

			Summoned = true;
			SummonMaster = caster;

			ControlOrder = OrderType.Follow;
			ControlTarget = caster;

            int min = 60;
            int max = (int)(Server.Spells.CosmosSolar.CosmosSolarSpell.GetCosmosSolarDamage(m_Caster));
            if (max < min) { max = min; }

            int hits = Utility.RandomMinMax(min, max);

            SetHits(hits);

            SetDamage(1, 1);

            SetDamageType(ResistanceType.Physical, 100);
            SetResistance(ResistanceType.Physical, 20, 40);
            SetResistance(ResistanceType.Fire, 20, 40);
            SetResistance(ResistanceType.Cold, 20, 40);
            SetResistance(ResistanceType.Poison, 20, 40);

            Fame = 0;
            Karma = 0;

            VirtualArmor = 1;

            ControlSlots = 3;

            TimeSpan duration = TimeSpan.FromSeconds(Server.Spells.CosmosSolar.CosmosSolarSpell.GetCosmosSolarDamage(m_Caster) / 2);

            new UnsummonTimer(caster, this, duration).Start();
            SummonEnd = DateTime.UtcNow + duration;

            MirrorImage.AddClone(m_Caster);
        }

        //protected override BaseAI ForcedAI { get { return new ClownAI( this ); } }

        public override bool IsHumanInTown() { return false; }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);
            if (m_Caster != null) { m_Caster.DoHarmful(defender); }
        }

     /*   private Item MiragemCosmicaItem(Item item)
        {
            Item newItem = new Item(item.ItemID);
            newItem.Hue = item.Hue;
            newItem.Layer = item.Layer;

            return newItem;
        }
     */
        public override bool DeleteCorpseOnDeath { get { return true; } }

        public override void OnDelete()
		{
			switch ( Utility.Random( 6 ))
			{
				case 0: PlaySound( Female ? 780 : 1051 ); break;
				case 1: Animate( 32, 5, 1, true, false, 0 ); break;
				case 2: PlaySound( Female ? 794 : 1066 ); break;
				case 3: PlaySound( Female ? 801 : 1073 ); break;
				case 4: PlaySound( 792 ); break;
				case 5: PlaySound( Female ? 783 : 1054 ); break;
			};

			Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3728, 10, 15, 5042 );

			base.OnDelete();
		}

		public override void OnAfterDelete()
		{
			MirrorImage.RemoveClone( m_Caster );
			base.OnAfterDelete();
		}

		public override bool IsDispellable { get { return false; } }
		public override bool Commandable { get { return false; } }

		public Palhaco( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version

			writer.Write( m_Caster );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_Caster = reader.ReadMobile();

			MirrorImage.AddClone( m_Caster );
		}
	}
}

namespace Server.Mobiles
{
	public class ClownAI : MeleeAI


    {
		public ClownAI(Palhaco m ) : base ( m )
		{
			m.CurrentSpeed = m.ActiveSpeed;
		}

		public override bool Think()
		{
			// Clones only follow their owners
			Mobile master = m_Mobile.SummonMaster;

			if ( master != null && master.Map == m_Mobile.Map && master.InRange( m_Mobile, m_Mobile.RangePerception ) )
			{
				int iCurrDist = (int)m_Mobile.GetDistanceToSqrt( master );
				bool bRun = (iCurrDist > 5);

				WalkMobileRange( master, 2, bRun, 0, 1 );
			}
			else
				WalkRandom( 2, 2, 1 );

			if ( Utility.RandomMinMax( 1, 10 ) == 1 )
			{
				switch ( Utility.Random( 6 ) )
				{
					case 0: m_Mobile.PlaySound( m_Mobile.Female ? 780 : 1051 ); break;
					case 1: m_Mobile.Animate( 32, 5, 1, true, false, 0 ); break;
					case 2: m_Mobile.PlaySound( m_Mobile.Female ? 794 : 1066 ); break;
					case 3: m_Mobile.PlaySound( m_Mobile.Female ? 801 : 1073 ); break;
					case 4: m_Mobile.PlaySound( 792 ); break;
					case 5: m_Mobile.PlaySound( m_Mobile.Female ? 783 : 1054 ); break;
				};
			}

			return true;
		}
		
		
	}
}
