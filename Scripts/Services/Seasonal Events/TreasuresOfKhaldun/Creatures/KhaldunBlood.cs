using Server.Items;
using System;

namespace Server.Mobiles
{
    [CorpseName("a Bloody corpse")]
    public class KhaldunBlood : BaseCreature
    {
        [Constructable]
        public KhaldunBlood()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Blood";
            Body = 0x33;
            BaseSoundID = 456;

            Hue = Utility.RandomList(33, 1157);

            SetStr(220, 286);
            SetDex(125, 130);
            SetInt(100, 101);

            SetHits(130);

            SetDamage(8, 18);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 30, 50);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.ResistenciaMagica, 60.0, 80.0);
            SetSkill(SkillName.Anatomia, 85.0, 100.0);
            SetSkill(SkillName.Briga, 85.0, 100.0);
            SetSkill(SkillName.Percepcao, 40.0);

            Fame = 300;
            Karma = -300;

            VirtualArmor = 8;

            SetWeaponAbility(WeaponAbility.BleedAttack);
        }

        public KhaldunBlood(Serial serial)
            : base(serial)
        {
        }

        public override void OnBeforeDamage(Mobile from, ref int totalDamage, Server.DamageType type)
        {
            if (Region.IsPartOf("Khaldun") && IsChampionSpawn && !Caddellite.CheckDamage(from, type))
            {
                totalDamage = 0;
            }

            base.OnBeforeDamage(from, ref totalDamage, type);
        }

        public override Poison PoisonImmune { get { return Poison.Lesser; } }
        public override Poison HitPoison { get { return Poison.Lesser; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
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
