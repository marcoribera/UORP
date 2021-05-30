using System;

namespace Server.Mobiles
{
    [CorpseName("an ettins corpse")]
    public class LesserEttin : BaseCreature
    {
        [Constructable]
        public LesserEttin()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "Ettin";
            this.Body = 18;
            this.BaseSoundID = 367;

            this.SetStr(136, 165);
            this.SetDex(56, 75);
            this.SetInt(31, 55);

            this.SetHits(82, 99);

            this.SetDamage(7, 17);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 35, 40);
            this.SetResistance(ResistanceType.Fire, 15, 25);
            this.SetResistance(ResistanceType.Cold, 40, 50);
            this.SetResistance(ResistanceType.Poison, 15, 25);
            this.SetResistance(ResistanceType.Energy, 15, 25);

            this.SetSkill(SkillName.ResistenciaMagica, 40.1, 55.0);
            this.SetSkill(SkillName.Anatomia, 50.1, 70.0);
            this.SetSkill(SkillName.Briga, 25.1, 30.0);

            this.Fame = 3000;
            this.Karma = -3000;

            this.VirtualArmor = 38;

            Persuadable = true;
            ControlSlots = 1;
            MinPersuadeSkill = 32;

        }

        public LesserEttin(Serial serial)
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
        public override int TreasureMapLevel
        {
            get
            {
                return 1;
            }
        }
        public override int Meat
        {
            get
            {
                return 4;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Meager);
            this.AddLoot(LootPack.Average);
            this.AddLoot(LootPack.Potions);
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