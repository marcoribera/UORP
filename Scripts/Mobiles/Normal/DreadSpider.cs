using System;
using Server.Items;

namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.DreadSpiderWeak")]
    [CorpseName("a dread spider corpse")]
    public class DreadSpider : BaseCreature
    {
        [Constructable]
        public DreadSpider()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a dread spider";
            Body = 11;
            BaseSoundID = 1170;

            SetStr(196, 220);
            SetDex(126, 145);
            SetInt(286, 310);

            SetHits(118, 132);

            SetDamage(5, 17);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Poison, 80);

            SetResistance(ResistanceType.Physical, 40, 50);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.PoderMagico, 65.1, 80.0);
            SetSkill(SkillName.Arcanismo, 65.1, 80.0);
            SetSkill(SkillName.ResistenciaMagica, 45.1, 60.0);
            SetSkill(SkillName.Anatomia, 55.1, 70.0);
            SetSkill(SkillName.Briga, 60.1, 75.0);
            SetSkill(SkillName.Envenenamento, 80.0);
            SetSkill(SkillName.Percepcao, 50.0, 60.0);
            SetSkill(SkillName.Necromancia, 20.0);
            SetSkill(SkillName.PoderMagico, 20.0);

            Fame = 5000;
            Karma = -5000;

            VirtualArmor = 36;

            PackItem(new SpidersSilk(8));

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 96.0;
        }

        public DreadSpider(Serial serial)
            : base(serial)
        {
        }

        public override bool CanAngerOnTame { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        public override Poison HitPoison { get { return Poison.Lethal; } }
        public override int TreasureMapLevel { get { return 3; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (version == 0 && (AbilityProfile == null || AbilityProfile.MagicalAbility == MagicalAbility.None))
            {
                SetMagicalAbility(MagicalAbility.Poisoning);
            }
        }
    }
}
