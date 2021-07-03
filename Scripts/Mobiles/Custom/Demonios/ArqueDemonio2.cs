using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("corpo do arquedemonio")]
    public class ArqueDemonio2 : BaseCreature
    {
        [Constructable]
        public ArqueDemonio2()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Arque-Demonio";
            Body = 1665;
            BaseSoundID = 0x3E9;

            this.SetStr(986, 1185);
            this.SetDex(177, 255);
            this.SetInt(151, 250);

            this.SetHits(592, 711);

            this.SetDamage(30, 35);

            SetDamageType(ResistanceType.Physical, 85);
            SetDamageType(ResistanceType.Fire, 15);

            SetResistance(ResistanceType.Physical, 50, 60);
            SetResistance(ResistanceType.Fire, 60, 70);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.ResistenciaMagica, 85.1, 95.0);
            this.SetSkill(SkillName.Anatomia, 90.1, 100.0);
            this.SetSkill(SkillName.Briga, 90.1, 100.0);


            this.Fame = 24000;
            this.Karma = -24000;

            VirtualArmor = 90;

            SetWeaponAbility(WeaponAbility.CrushingBlow);
        }

        public ArqueDemonio2(Serial serial)
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