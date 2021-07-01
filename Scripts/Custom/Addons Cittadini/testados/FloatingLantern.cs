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

			SetStr( 376, 405 );
			SetDex( 176, 195 );
			SetInt( 201, 225 );

			SetHits( 3000, 9000 );

			SetDamage( 8, 19 );

			SetSkill( SkillName.Feiticaria, 80.1, 90.0 );
			SetSkill( SkillName.Arcanismo, 80.1, 90.0 );
			SetSkill( SkillName.ResistenciaMagica, 75.1, 85.0 );
			SetSkill( SkillName.Anatomia, 80.1, 90.0 );
			SetSkill( SkillName.Briga, 80.1, 100.0 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			Fame = 18000;
			Karma = -18000;

			VirtualArmor = 60;
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

		public override int TreasureMapLevel{ get{ return 4; } }
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
