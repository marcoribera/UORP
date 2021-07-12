using System.Collections.Generic;
namespace Server.Mobiles
{
	[CorpseName( "a smoldering corpse" )]
	public class SpiritMinion : BaseCreature
	{
        private static readonly string[] m_Names = new string[]
			{
				"Fire Sprite",
				"Fire Fly"
			};
		public override bool InitialInnocent{ get{ return true; } }
        
		[Constructable]
		public SpiritMinion() : base( AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4 )
		{
            Name = m_Names[Utility.Random(m_Names.Length)];
			Body = 128;
			BaseSoundID = 0x467;
		    Hue = 2519;

			SetStr( 20, 30 );
			SetDex( 31, 40 );
			SetInt( 21, 25 );

			SetHits( 50, 80 );

			SetDamage( 5, 10 );

            SetResistance(ResistanceType.Physical, 10);
            SetResistance(ResistanceType.Fire, 10);
            SetResistance(ResistanceType.Cold, 10);
            SetResistance(ResistanceType.Poison, 10);
            SetResistance(ResistanceType.Energy, 10);

            SetSkill(SkillName.Briga, 50, 60);
            SetSkill(SkillName.Anatomia, 60, 60.0);
            SetSkill(SkillName.ResistenciaMagica, 20.0, 20.0);
            SetSkill(SkillName.Arcanismo, 80.0, 80.0);

            SetSkill(SkillName.Bloqueio, 20.5, 20.0);
            SetSkill(SkillName.Medicina, 20.0, 20.0);

            Fame = 50;

			VirtualArmor = 10;

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

        public override HideType HideType { get { return HideType.Regular; } }

		public override int Hides{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }

		public SpiritMinion( Serial serial ) : base( serial )
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
