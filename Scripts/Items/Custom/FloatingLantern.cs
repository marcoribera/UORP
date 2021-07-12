/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\events and scenarios\Summer2008\Magincia Mystery\FloatingLantern.cs
Lines of code: 72
***********************************************************/


using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a mysterious corpse" )]
	public class FloatingLantern : BaseCreature
	{
		[Constructable]
		public FloatingLantern () : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "A floating lantern?";
			Body = 500;
			Hue = Utility.RandomList(1157, 1, 32, 1175);
			BaseSoundID = 0x482;

			SetStr( 76, 105 );
			SetDex( 36, 55 );
			SetInt( 61, 100 );

			SetHits( 100, 130 );

			SetDamage( 8, 19 );

			SetSkill( SkillName.Feiticaria, 60.1, 70.0 );
			SetSkill( SkillName.Arcanismo, 40.1, 60.0 );
			SetSkill( SkillName.ResistenciaMagica, 45.1, 55.0 );
			SetSkill( SkillName.Anatomia, 30.1, 40.0 );
			SetSkill( SkillName.Briga, 40.1, 50.0 );

            SetResistance(ResistanceType.Physical, 10);
            SetResistance(ResistanceType.Fire, 10);
            SetResistance(ResistanceType.Cold, 10);
            SetResistance(ResistanceType.Poison, 10);
            SetResistance(ResistanceType.Energy, 10);

            Fame = 180;
			Karma = -180;

			VirtualArmor = 24;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls, 2 );
		}
		
		public override void OnDamage( int amount, Mobile attacker, bool willKill ) 
		{
			if(attacker!=null && attacker.Alive && attacker.InRange( this, 20 ))
			{
				attacker.Kill();
			}
			base.OnDamage(amount, attacker, willKill);	
		}

		public override int TreasureMapLevel{ get{ return 2; } }
		public override int Meat{ get{ return 1; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override bool Uncalmable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool BleedImmune{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		public FloatingLantern( Serial serial ) : base( serial )
		{
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
