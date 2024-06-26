using System;
using Server.Factions;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("corpo do arquedemonio")]
    public class TLSArqueDemonio : BaseCreature
    {
        [Constructable]
        public TLSArqueDemonio()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "Arque-demonio";
            this.Body = 1705;
            this.Hue = 0;
            this.BaseSoundID = 357;

            this.SetStr(986, 1185);
            this.SetDex(177, 255);
            this.SetInt(151, 250);

            this.SetHits(592, 711);

            this.SetDamage(22, 29);

            this.SetDamageType(ResistanceType.Physical, 50);
            this.SetDamageType(ResistanceType.Fire, 25);
            this.SetDamageType(ResistanceType.Energy, 25);

            this.SetResistance(ResistanceType.Physical, 65, 80);
            this.SetResistance(ResistanceType.Fire, 60, 80);
            this.SetResistance(ResistanceType.Cold, 50, 60);
            this.SetResistance(ResistanceType.Poison, 100);
            this.SetResistance(ResistanceType.Energy, 40, 50);

            this.SetSkill(SkillName.Anatomia, 25.1, 50.0);
            this.SetSkill(SkillName.PoderMagico, 90.1, 100.0);
            this.SetSkill(SkillName.Arcanismo, 95.5, 100.0);
            this.SetSkill(SkillName.ResistenciaMagica, 100.5, 150.0);
            this.SetSkill(SkillName.Anatomia, 90.1, 100.0);
            this.SetSkill(SkillName.Briga, 90.1, 100.0);

            this.Fame = 24000;
            this.Karma = -24000;

            this.VirtualArmor = 90;
            PackItem(new BottledLightning(Utility.RandomMinMax(0, 1)));
        }

        public TLSArqueDemonio(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty
        {
            get
            {
                return 125.0;
            }
        }
        public override double DispelFocus
        {
            get
            {
                return 45.0;
            }
        }
        public override Faction FactionAllegiance
        {
            get
            {
                return Shadowlords.Instance;
            }
        }
        public override Ethics.Ethic EthicAllegiance
        {
            get
            {
                return Ethics.Ethic.Evil;
            }
        }
        public override bool CanRummageCorpses
        {
            get
            {
                return true;
            }
        }
        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Regular;
            }
        }
        public override int TreasureMapLevel
        {
            get
            {
                return 4;
            }
        }
        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Rich);
            this.AddLoot(LootPack.Average, 2);
            this.AddLoot(LootPack.MedScrolls, 2);
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
