using System;

using Server.Mobiles;

namespace Server.Engines.SorcerersDungeon
{
    [CorpseName("a garish gingerman corpse")]
    public class GarishGingerman : BaseCreature
    {
        [Constructable]
        public GarishGingerman()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a garish gingerman";

            Body = 14;
            BaseSoundID = 268;
            Hue = 1461;

            SetStr(400);
            SetDex(150);
            SetInt(1200);

            SetHits(8000);

            SetDamage(21, 27);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Energy, 50);

            SetResistance(ResistanceType.Physical, 60, 70);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 80, 90);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 100);

            SetSkill(SkillName.PoderMagico, 120);
            SetSkill(SkillName.Arcanismo, 120);
            SetSkill(SkillName.ResistenciaMagica, 200);
            SetSkill(SkillName.Anatomia, 100.0);
            SetSkill(SkillName.Briga, 120);

            Fame = 12000;
            Karma = -12000;

            SetMagicalAbility(MagicalAbility.MageryMastery);
        }

        public GarishGingerman(Serial serial)
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
