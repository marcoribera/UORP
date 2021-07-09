using System;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("corpo de Homem-Lagarto")]
    public class TLSHLagartoDePorrete : BaseCreature
    {
        [Constructable]
        public TLSHLagartoDePorrete()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("lizardman");
            Body = 1722;
            BaseSoundID = 417;

            SetStr(96, 120);
            SetDex(86, 105);
            SetInt(36, 60);

            SetHits(58, 72);

            SetDamage(5, 7);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 30);
            SetResistance(ResistanceType.Fire, 5, 10);
            SetResistance(ResistanceType.Cold, 5, 10);
            SetResistance(ResistanceType.Poison, 10, 20);

            SetSkill(SkillName.ResistenciaMagica, 35.1, 60.0);
            SetSkill(SkillName.Anatomia, 55.1, 80.0);
            SetSkill(SkillName.Briga, 50.1, 70.0);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 28;

            Persuadable = true;
            ControlSlots = 2;
            MinPersuadeSkill = 75;

        }

        public TLSHLagartoDePorrete(Serial serial)
            : base(serial)
        {
        }

		public override int TreasureMapLevel
        {
            get
            {
                return 1;
            }
        }
        public override InhumanSpeech SpeechType
        {
            get
            {
                return InhumanSpeech.Lizardman;
            }
        }
        public override bool CanRummageCorpses
        {
            get
            {
                return true;
            }
        }
        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override int Hides
        {
            get
            {
                return 12;
            }
        }
        public override HideType HideType
        {
            get
            {
                return HideType.Spined;
            }
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
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