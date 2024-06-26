using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a clan scratch scrounger corpse")]
    public class ClanSS : BaseCreature
    {
        [Constructable]
        public ClanSS()
            : base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Clan Scratch Scrounger";
            Body = 0x8E;
            BaseSoundID = 437;

            SetStr(97, 100);
            SetDex(98, 100);
            SetInt(45, 50);

            SetHits(135);

            SetDamage(4, 5);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 30);
            SetResistance(ResistanceType.Fire, 20, 25);
            SetResistance(ResistanceType.Cold, 49, 55);
            SetResistance(ResistanceType.Poison, 10, 20);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Anatomia, 51.5, 55.5);
            SetSkill(SkillName.ResistenciaMagica, 65.1, 90.0);
            SetSkill(SkillName.Anatomia, 59.1, 65.0);
            SetSkill(SkillName.Briga, 72.5, 75.0);

            Fame = 6500;
            Karma = -6500;

            VirtualArmor = 56;

            AddItem(new Bow());
            PackItem(new Arrow(Utility.RandomMinMax(50, 70)));
        }

        public ClanSS(Serial serial)
            : base(serial)
        {
        }

        public override bool CanRummageCorpses
        {
            get
            {
                return true;
            }
        }
        public override int Hides
        {
            get
            {
                return 8;
            }
        }
        public override HideType HideType
        {
            get
            {
                return HideType.Spined;
            }
        }
		public override int TreasureMapLevel
        {
            get
            {
                return 2;
            }
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }
      
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}