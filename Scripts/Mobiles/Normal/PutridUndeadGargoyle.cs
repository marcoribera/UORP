/* Based on Gargoyle, still no infos on Undead Gargoyle... Have to get also the correct body ID */
using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a putrid undead gargoyle corpse")]
    public class PutridUndeadGargoyle : BaseCreature
    {
        [Constructable]
        public PutridUndeadGargoyle()
            : base(AIType.AI_Mystic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a putrid undead gargoyle";
            Body = 722;
            BaseSoundID = 372;
            Hue = 1778;

            SetStr(525);
            SetDex(120, 125);
            SetInt(1145);

            SetHits(660, 665);

            SetDamage(21, 30);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Cold, 40);
            SetDamageType(ResistanceType.Energy, 40);

            SetResistance(ResistanceType.Physical, 55, 60);
            SetResistance(ResistanceType.Fire, 33);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50, 55);
            SetResistance(ResistanceType.Energy, 45, 50);

            SetSkill(SkillName.PoderMagico, 125.0);
            SetSkill(SkillName.Arcanismo, 127.0);
            SetSkill(SkillName.Misticismo, 100.0);
            SetSkill(SkillName.ResistenciaMagica, 180.0, 187.0);
            SetSkill(SkillName.Anatomia, 100.0);
            SetSkill(SkillName.Briga, 100.0, 102.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 32;

            if (0.05 > Utility.RandomDouble())
                PackItem(new TatteredAncientScroll());

            if (0.10 > Utility.RandomDouble())
                PackItem(new InfusedGlassStave());

            if (0.15 > Utility.RandomDouble())
                PackItem(new AncientPotteryFragments());
        }

        public PutridUndeadGargoyle(Serial serial)
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
        public override bool Unprovokable
        {
            get
            {
                return true;
            }
        }
        public override bool BardImmune
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
                return Poison.Deadly;
            }
        }
        public override Poison HitPoison
        {
            get
            {
                return Poison.Deadly;
            }
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosFilthyRich, 5);
            AddLoot(LootPack.MedScrolls);
            AddLoot(LootPack.Gems, Utility.RandomMinMax(1, 4));
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
