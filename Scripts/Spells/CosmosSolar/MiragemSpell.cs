using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;
using System.Collections;
using Server.Multis;

using System.Collections.Generic;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;


namespace Server.Spells.CosmosSolar
{
	public class MiragemSpell : CosmosSolarSpell
	{
        private static readonly SpellInfo m_Info = new SpellInfo(
           "Miragem", "Fictus Imago",
           -1,
           0,
           Reagent.Bloodmoss,
           Reagent.DragonBlood);

    

        public MiragemSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias


        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.75); } }

        public override SpellCircle Circle

        {
            get
            {
                return SpellCircle.Third;
            }
        }

        public override bool CheckCast()
		{
			if ( !base.CheckCast(  ) )
				return false;

			if( (Caster.Followers + 3) > Caster.FollowersMax )
			{
				Caster.SendMessage( "Você tem seguidores demais para criar uma miragem." );
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
			else if ( (Caster.Followers + 3) > Caster.FollowersMax )
			{
				Caster.SendMessage("Você tem seguidores demais para criar uma miragem.");
			}
			else if( TransformationSpellHelper.UnderTransformation( Caster, typeof( HorrificBeastSpell ) ) )
			{
				Caster.SendMessage("Você não pode criar uma miragem enquanto estiver assim.");
			}
			else if ( CheckSequence() && CheckFizzle() )
			{
				new MiragemCosmica ( Caster ).MoveToWorld( Caster.Location, Caster.Map );
				Caster.Hidden = false;
               
            }

			FinishSequence();
		}
	}
}

namespace Server.Mobiles
{
	public class MiragemCosmica : BaseCreature
	{
		private Mobile m_Caster;

		public MiragemCosmica( Mobile caster ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			m_Caster = caster;

			Body = caster.Body;

			Hue = caster.Hue;
			Female = caster.Female;

			Name = caster.Name;
			NameHue = caster.NameHue;

			Title = caster.Title;
			Kills = caster.Kills;

			HairItemID = caster.HairItemID;
			HairHue = caster.HairHue;

			FacialHairItemID = caster.FacialHairItemID;
			FacialHairHue = caster.FacialHairHue;

			for ( int i = 0; i < caster.Skills.Length; ++i )
			{
				Skills[i].Base = caster.Skills[i].Base;
				Skills[i].Cap = caster.Skills[i].Cap;
			}

			for( int i = 0; i < caster.Items.Count; i++ )
			{
				AddItem( MiragemCosmicaItem( caster.Items[i] ) );
			}

			Warmode = true;

			Summoned = true;
			SummonMaster = caster;

			int min = 60;
			int max = (int)(Server.Spells.CosmosSolar.CosmosSolarSpell.GetCosmosSolarDamage( m_Caster ) );
				if ( max < min ){ max = min; }

			int hits = Utility.RandomMinMax( min, max );

			SetHits( hits );

			SetDamage( 1, 1 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 20, 40 );
			SetResistance( ResistanceType.Fire, 20, 40 );
			SetResistance( ResistanceType.Cold, 20, 40 );
			SetResistance( ResistanceType.Poison, 20, 40 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 1;

			ControlSlots = 3;

			TimeSpan duration = TimeSpan.FromSeconds( Server.Spells.CosmosSolar.CosmosSolarSpell.GetCosmosSolarDamage( m_Caster ) / 2 );

			new UnsummonTimer( caster, this, duration ).Start();
			SummonEnd = DateTime.UtcNow + duration;

			MirrorImage.AddClone( m_Caster );
		}

		public override bool IsHumanInTown() { return false; }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			if ( m_Caster != null ){ m_Caster.DoHarmful( defender ); }
		}

		private Item MiragemCosmicaItem( Item item )
		{
			Item newItem = new Item( item.ItemID );
			newItem.Hue = item.Hue;
			newItem.Layer = item.Layer;

			return newItem;
		}

		public override bool DeleteCorpseOnDeath { get { return true; } }

		public override void OnDelete()
		{
			PlaySound( 0x0FD );
			this.FixedParticles( 0x375A, 10, 30, 5052, 0xB41, 0, EffectLayer.LeftFoot );
			base.OnDelete();
		}

		public override void OnAfterDelete()
		{
			MirrorImage.RemoveClone( m_Caster );
			base.OnAfterDelete();
		}

		public override bool IsDispellable { get { return false; } }
		public override bool Commandable { get { return false; } }

		public MiragemCosmica( Serial serial ) : base( serial )
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
