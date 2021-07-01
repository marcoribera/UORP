using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an evil vine corpse" )]
	public class EvilVine : BaseCreature
	{
		[Constructable]
		public EvilVine() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an evil vine";
			Body = 8;
			Hue = 12;
			BaseSoundID = 352;

			SetStr( 251, 300 );
			SetDex( 76, 100 );
			SetInt( 26, 40 );

			SetMana( 0 );

			SetDamage( 7, 25 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Poison, 30 );

			SetResistance( ResistanceType.Physical, 75, 85 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 75, 85 );
			SetResistance( ResistanceType.Energy, 35, 45 );

			SetSkill( SkillName.ResistenciaMagica, 70.0 );
			SetSkill( SkillName.Anatomia, 70.0 );
			SetSkill( SkillName.Briga, 70.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 45;

			//PackReg( 3 );
			PackItem( new FertileDirt( Utility.RandomMinMax( 1, 10 ) ) );

			//if ( 0.2 >= Utility.RandomDouble() )
				//PackItem( new ExecutionersCap() );

			//PackItem( new Vines() );
		}

		public override bool BardImmune{ get{ return !Core.AOS; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public EvilVine( Serial serial ) : base( serial )
		{
		}


                public override void OnDeath( Container c )
		{
			base.OnDeath( c );	
			
			
			switch ( Utility.Random( 4 ) )
			{
				case 0: c.DropItem( new FloweringVine1() ); break;
				case 1: c.DropItem( new FloweringVine2() ); break;
				case 2: c.DropItem( new FloweringVine3() ); break;
                                case 3: c.DropItem( new FloweringVine4() ); break;
			}
			
			//if ( Utility.RandomDouble() < 0.5 )				
				//c.DropItem( new GardenFlower() );
			
			//if ( Utility.RandomDouble() < 0.1 )
				//c.DropItem( new Seed() );
				
			//if ( Utility.RandomDouble() < 0.05 )
				//c.DropItem( new ArchersSinch() );
				
			//if ( Utility.RandomDouble() < 0.05 )
				//c.DropItem( new SinchoftheMagi() );
				
			
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
