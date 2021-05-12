using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a gargoyle corpse")]
    public class GargoyleEnforcer : BaseCreature
    {
        [Constructable]
        public GargoyleEnforcer()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Gargoyle Enforcer";
            Body = 0x2F2;
            BaseSoundID = 0x174;

            SetStr(760, 850);
            SetDex(102, 150);
            SetInt(152, 200);

            SetHits(482, 485);

            SetDamage(7, 14);

            SetResistance(ResistanceType.Physical, 40, 60);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 25, 35);
            SetResistance(ResistanceType.Energy, 15, 25);

            SetSkill(SkillName.ResistenciaMagica, 120.1, 130.0);
            SetSkill(SkillName.Anatomia, 70.1, 80.0);
            SetSkill(SkillName.Briga, 80.1, 90.0);
            SetSkill(SkillName.Cortante, 80.1, 90.0);
            SetSkill(SkillName.Anatomia, 70.1, 80.0);
            SetSkill(SkillName.Arcanismo, 80.1, 90.0);
            SetSkill(SkillName.PoderMagico, 70.3, 100.0);

            Fame = 5000;
            Karma = -5000;

            Persuadable = true;
            ControlSlots = 3;
            MinPersuadeSkill = 95;

            VirtualArmor = 50;

            if (0.2 > Utility.RandomDouble())
                PackItem(new GargoylesPickaxe());

            SetWeaponAbility(WeaponAbility.WhirlwindAttack);
        }

        public GargoyleEnforcer(Serial serial)
            : base(serial)
        {
        }

        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override bool CanFly
        {
            get
            {
                return true;
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls);
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
