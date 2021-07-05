using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("corpo do Elemental de cristal")]
    public class ElementalDeCristal : BaseCreature
    {
        [Constructable]
        public ElementalDeCristal()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Elemental de cristal";
            Body = 1576;
            BaseSoundID = 278;

            SetStr(136, 160);
            SetDex(51, 65);
            SetInt(86, 110);

            SetHits(150);

            SetDamage(10, 15);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Energy, 20);

            SetResistance(ResistanceType.Physical, 50, 60);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 55, 70);

            SetSkill(SkillName.PoderMagico, 70.1, 75.0);
            SetSkill(SkillName.Arcanismo, 70.1, 75.0);
            SetSkill(SkillName.ResistenciaMagica, 80.1, 90.0);
            SetSkill(SkillName.Anatomia, 75.1, 85.0);
            SetSkill(SkillName.Briga, 65.1, 75.0);

            Fame = 6500;
            Karma = -6500;

            VirtualArmor = 54;

            SetWeaponAbility(WeaponAbility.ParalyzingBlow);
        }

        public ElementalDeCristal(Serial serial)
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
        public override int TreasureMapLevel
        {
            get
            {
                return 1;
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
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
