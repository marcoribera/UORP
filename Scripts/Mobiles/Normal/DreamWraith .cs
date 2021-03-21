using System;

namespace Server.Mobiles
{
    [CorpseName("a dream wraith corpse")]
    public class DreamWraith : BaseCreature
    {
        [Constructable]
        public DreamWraith()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a Dream Wraith";
            Body = 740;
            //Hue = 0;
            BaseSoundID = 0x482;

            SetStr(200, 300);
            SetDex(100, 200);
            SetInt(600, 700);

            SetHits(550, 650);

            SetDamage(18, 25);

            SetDamageType(ResistanceType.Physical, 10);
            SetDamageType(ResistanceType.Cold, 45);
            SetDamageType(ResistanceType.Energy, 45);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 30, 50);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.Necromancia, 100.0, 120.0);
            SetSkill(SkillName.PoderMagico, 100.0, 120.0);
            SetSkill(SkillName.Anatomia, 0.0, 10.0);
            SetSkill(SkillName.PoderMagico, 100.0, 120.0);
            SetSkill(SkillName.Arcanismo, 100.0, 120.0);
            SetSkill(SkillName.ResistenciaMagica, 120.0, 150.0);
            SetSkill(SkillName.Anatomia, 70.0, 80.0);
            SetSkill(SkillName.Briga, 90.0, 100.0);

            Fame = 4000;
            Karma = -4000;

            VirtualArmor = 28;

            PackReg(10);
        }

        public DreamWraith(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmune
        {
            get
            {
                return true;
            }
        }
        public override OppositionGroup OppositionGroup
        {
            get
            {
                return OppositionGroup.FeyAndUndead;
            }
        }
        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Lethal;
            }
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

        public override int GetIdleSound()
        {
            return 0x5F4;
        }

        public override int GetAngerSound()
        {
            return 0x5F1;
        }

        public override int GetDeathSound()
        {
            return 0x5F2;
        }

        public override int GetHurtSound()
        {
            return 0x5F3;
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
