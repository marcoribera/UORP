using System;
using Server.Items;

namespace Server.Mobiles
{

    [CorpseName("corpo do Elemental de acido")]
    public class ElementalDeAcido : BaseCreature, IAcidCreature
    {
        [Constructable]
        public ElementalDeAcido()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Elemental de acido";
            Body = 1580;
            BaseSoundID = 263;

            SetStr(326, 355);
            SetDex(66, 85);
            SetInt(271, 295);

            SetHits(196, 213);

            SetDamage(9, 15);

            SetDamageType(ResistanceType.Physical, 10);
            SetDamageType(ResistanceType.Poison, 90);

            SetResistance(ResistanceType.Physical, 60, 70);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.Anatomia, 30.3, 60.0);
            SetSkill(SkillName.PoderMagico, 80.1, 95.0);
            SetSkill(SkillName.Arcanismo, 70.1, 85.0);
            SetSkill(SkillName.ResistenciaMagica, 60.1, 85.0);
            SetSkill(SkillName.Anatomia, 80.1, 90.0);
            SetSkill(SkillName.Briga, 70.1, 90.0);

            Fame = 10000;
            Karma = -10000;

            VirtualArmor = 70;

            PackItem(new Nightshade(4));
        }

        public ElementalDeAcido(Serial serial)
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
                return 2;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Rich);
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

            switch (version)
            {
                case 0:
                    Body = 158;
                    break;
            }
        }
    }
}
