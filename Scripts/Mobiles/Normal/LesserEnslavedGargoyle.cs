using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an enslaved gargoyle corpse")]
    public class LesserEnslavedGargoyle : BaseCreature
    {
        [Constructable]
        public LesserEnslavedGargoyle()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "";
            Body = 0x2F1;
            BaseSoundID = 0x174;

            SetStr(302, 360);
            SetDex(76, 95);
            SetInt(81, 105);

            SetHits(186, 212);

            SetDamage(7, 14);

            SetResistance(ResistanceType.Physical, 30, 40);
            SetResistance(ResistanceType.Fire, 50, 70);
            SetResistance(ResistanceType.Cold, 15, 25);
            SetResistance(ResistanceType.Poison, 25, 30);
            SetResistance(ResistanceType.Energy, 25, 30);

            SetSkill(SkillName.ResistenciaMagica, 70.1, 85.0);
            SetSkill(SkillName.Anatomia, 50.1, 70.0);
            SetSkill(SkillName.Briga, 20.1, 40.0);

            Fame = 3500;
            Karma = 0;

            VirtualArmor = 35;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 35;

            if (0.2 > Utility.RandomDouble())
                PackItem(new GargoylesPickaxe());

            SetSpecialAbility(SpecialAbility.AngryFire);
        }

        public LesserEnslavedGargoyle(Serial serial)
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
        public override int TreasureMapLevel
        {
            get
            {
                return 1;
            }
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.Gems);
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