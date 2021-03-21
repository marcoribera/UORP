using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a poison elementals corpse")]
    public class GreaterPoisonElemental : BaseCreature
    {
        [Constructable]
        public GreaterPoisonElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Greater Poison Elemental";
            Body = 162;
            BaseSoundID = 263;

            Hue = 667;

            SetStr(700, 771);
            SetDex(195, 203);
            SetInt(650, 691);

            SetHits(650, 702);
            SetStam(300, 322);
            SetMana(500, 530);

            SetDamage(12, 18);

            SetDamageType(ResistanceType.Physical, 10);
            SetDamageType(ResistanceType.Poison, 90);

            SetResistance(ResistanceType.Physical, 60, 70);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.PoderMagico, 80.1, 110.0);
            SetSkill(SkillName.Arcanismo, 80.1, 97.0);
            SetSkill(SkillName.Envenenamento, 100.1, 114.9);
            SetSkill(SkillName.ResistenciaMagica, 85.2, 93.2);
            SetSkill(SkillName.Anatomia, 80.1, 100.0);
            SetSkill(SkillName.Briga, 75.0, 88.3);
            SetSkill(SkillName.Percepcao, 71.5);

            Fame = 12500;
            Karma = -12500;

            VirtualArmor = 70;

            PackItem(new Nightshade(4));
            PackItem(new LesserPoisonPotion());
        }

        public GreaterPoisonElemental(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmune
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
                return Poison.Lethal;
            }
        }
        public override Poison HitPoison
        {
            get
            {
                return Poison.Lethal;
            }
        }
        public override double HitPoisonChance
        {
            get
            {
                return 0.75;
            }
        }
        public override int TreasureMapLevel
        {
            get
            {
                return 5;
            }
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.03)            
                c.DropItem(new LuckyCoin());           
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
