using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a chaos daemon corpse")]
    public class DemonioDoCaos : BaseCreature
    {
        [Constructable]
        public DemonioDoCaos()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "um Demonio do Caos";
            Body = Utility.RandomList(1728, 1717);;
            BaseSoundID = 0x3E9;

            SetStr(106, 130);
            SetDex(171, 200);
            SetInt(56, 80);

            SetHits(91, 110);

            SetDamage(12, 17);

            SetDamageType(ResistanceType.Physical, 85);
            SetDamageType(ResistanceType.Fire, 15);

            SetResistance(ResistanceType.Physical, 50, 60);
            SetResistance(ResistanceType.Fire, 60, 70);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.ResistenciaMagica, 85.1, 95.0);
            SetSkill(SkillName.Anatomia, 70.1, 80.0);
            SetSkill(SkillName.Briga, 95.1, 100.0);

            Fame = 3000;
            Karma = -4000;

            VirtualArmor = 15;

            SetWeaponAbility(WeaponAbility.CrushingBlow);
        }

        public DemonioDoCaos(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
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