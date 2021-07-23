using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a gazer corpse")]
    public class Gazer : BaseCreature
    {
        [Constructable]
        public Gazer()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "a gazer";
            this.Body = 22;
            this.BaseSoundID = 377;

            this.SetStr(96, 125);
            this.SetDex(86, 105);
            this.SetInt(141, 165);

            this.SetHits(58, 75);

            this.SetDamage(5, 10);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 35, 40);
            this.SetResistance(ResistanceType.Fire, 40, 50);
            this.SetResistance(ResistanceType.Cold, 20, 30);
            this.SetResistance(ResistanceType.Poison, 10, 20);
            this.SetResistance(ResistanceType.Energy, 20, 30);

            this.SetSkill(SkillName.PoderMagico, 50.1, 65.0);
            this.SetSkill(SkillName.Arcanismo, 50.1, 65.0);
            this.SetSkill(SkillName.ResistenciaMagica, 60.1, 75.0);
            this.SetSkill(SkillName.Anatomia, 50.1, 70.0);
            this.SetSkill(SkillName.Briga, 50.1, 70.0);

            this.Fame = 3500;
            this.Karma = -3500;

            this.VirtualArmor = 36;

            this.PackItem(new Nightshade(4));
            PackItem(new GazerEye(Utility.RandomMinMax(0, 1)));
        }

        public Gazer(Serial serial)
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
        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override void GenerateLoot()
        {
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
