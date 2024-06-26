using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a minotaur corpse")]
    public class Minotaur : BaseCreature
    {
        [Constructable]
        public Minotaur()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)// NEED TO CHECK
        {
            Name = "a minotaur";
            Body = 263;

            SetStr(301, 340);
            SetDex(91, 110);
            SetInt(31, 50);

            SetHits(301, 340);

            SetDamage(11, 20);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 25, 35);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.PoderMagico, 0);
            SetSkill(SkillName.Arcanismo, 0);
            SetSkill(SkillName.Envenenamento, 0);
            SetSkill(SkillName.Anatomia, 0);
            SetSkill(SkillName.ResistenciaMagica, 56.1, 64.0);
            SetSkill(SkillName.Anatomia, 93.3, 97.8);
            SetSkill(SkillName.Briga, 90.4, 92.1);

            Fame = 5000;
            Karma = -5000;

            VirtualArmor = 28; // Don't know what it should be

            Persuadable = true;
            ControlSlots = 3;
            MinPersuadeSkill = 95;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetWeaponAbility(WeaponAbility.ParalyzingBlow);
        }

        public Minotaur(Serial serial)
            : base(serial)
        {
        }

		public override int TreasureMapLevel { get { return 3; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);  // Need to verify
        }

        // Using Tormented Minotaur sounds - Need to veryfy
        public override int GetAngerSound()
        {
            return 0x597;
        }

        public override int GetIdleSound()
        {
            return 0x596;
        }

        public override int GetAttackSound()
        {
            return 0x599;
        }

        public override int GetHurtSound()
        {
            return 0x59a;
        }

        public override int GetDeathSound()
        {
            return 0x59c;
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
