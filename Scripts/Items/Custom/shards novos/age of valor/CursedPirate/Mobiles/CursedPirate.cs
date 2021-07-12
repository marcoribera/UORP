/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\engines\CursedPirate\Mobiles\CursedPirate.cs
Lines of code: 73
***********************************************************/


using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an undead pirate corpse" )]
	public class CursedPirate : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }

		[Constructable]
		public CursedPirate() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Title = "the Cursed Pirate";

			Hue = Utility.RandomMinMax( 0x8596, 0x8599 );
			Body = 0x190;
			Name = NameList.RandomName( "male" );
			BaseSoundID = 471;

			AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
			AddItem( new Shirt( Utility.RandomNeutralHue() ) );

			switch (Utility.Random(2))
			{
				default:
				case 0: AddItem(new TricorneHat(Utility.RandomRedHue())); break;
				case 1: AddItem(new Bandana(Utility.RandomRedHue())); break;
			}

			BaseWeapon weapon = Loot.RandomWeapon();
			weapon.Movable = false;
			AddItem( weapon );

			SetStr( 114, 140 );
			SetDex( 96, 115 );
			SetInt( 61, 70 );

			SetHits( 120, 167 );

			SetDamage( 9, 23 );

			SetResistance( ResistanceType.Physical, 30, 45 );
			SetResistance( ResistanceType.Fire, 15, 30 );
			SetResistance( ResistanceType.Cold, 35, 55 );
			SetResistance( ResistanceType.Poison, 15, 30 );
			SetResistance( ResistanceType.Energy, 15, 30 );

			SetSkill( SkillName.Cortante, 56.0, 97.5 );
			SetSkill( SkillName.ResistenciaMagica, 63.5, 102.5 );
			SetSkill( SkillName.Anatomia, 75.0, 107.5 );
			SetSkill( SkillName.Medicina, 80.0, 112.5 );
			SetSkill( SkillName.Envenenamento, 70.0, 92.5 );

			Fame = 2500;
			Karma = -3000;
		}

		public override int GetAttackSound()
		{
			return -1;
		}

		public override bool OnBeforeDeath()
		{
            PirateCurse.CursedPirateLoot(this, 20);
			PackItem(new Gold(100));

			return base.OnBeforeDeath();
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public CursedPirate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
