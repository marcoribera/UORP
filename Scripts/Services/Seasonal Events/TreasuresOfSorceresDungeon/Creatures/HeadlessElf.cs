using System;

using Server.Mobiles;
using Server.Items;

namespace Server.Engines.SorcerersDungeon
{
    [CorpseName("the corpse of a headless elf")]
    public class HeadlessElf : BaseCreature
    {
        [Constructable]
        public HeadlessElf()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a headless elf";
            Race = Race.Elf;
            Body = 31;

            Hue = Race.RandomSkinHue();
            BaseSoundID = 0x39D;

            SetStr(700, 800);
            SetDex(90, 100);
            SetInt(450, 500);

            SetHits(8000);

            SetDamage(21, 27);

            SetDamageType(ResistanceType.Physical, 60);
            SetDamageType(ResistanceType.Fire, 20);
            SetDamageType(ResistanceType.Energy, 20);

            SetResistance(ResistanceType.Physical, 70, 80);
            SetResistance(ResistanceType.Fire, 70, 80);
            SetResistance(ResistanceType.Cold, 60, 70);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 60, 70);

            SetSkill(SkillName.Anatomia, 130, 140);
            SetSkill(SkillName.Envenenamento, 120);
            SetSkill(SkillName.ResistenciaMagica, 130, 140);
            SetSkill(SkillName.Anatomia, 130, 140);
            SetSkill(SkillName.Briga, 120, 130);
            SetSkill(SkillName.Bloqueio, 20, 30);

            SetSkill(SkillName.Arcanismo, 110, 120);
            SetSkill(SkillName.PoderMagico, 110, 120);
            SetSkill(SkillName.PreparoFisico, 120, 130);

            Fame = 12000;
            Karma = -12000;

            SetMagicalAbility(MagicalAbility.WrestlingMastery);
        }

        public HeadlessElf(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Deadly; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
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
