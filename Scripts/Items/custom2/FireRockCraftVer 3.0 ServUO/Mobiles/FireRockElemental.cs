/* Created by Makaar*/
/* Slightly Warped by Hammerhand*/

using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a firerock elemental corpse")]
    public class FireRockElemental : BaseCreature
    {

        [Constructable]
        public FireRockElemental()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a FireRock Elemental";
            Body = 112;
            BaseSoundID = 268;

            Hue = 1356;

            SetStr(286, 325);
            SetDex(226, 245);
            SetInt(171, 192);

            SetHits(1000, 1150);

            SetDamage(28);

            SetDamageType(ResistanceType.Physical, 25);
            SetDamageType(ResistanceType.Fire, 25);
            SetDamageType(ResistanceType.Cold, 25);
            SetDamageType(ResistanceType.Energy, 25);

            SetResistance(ResistanceType.Physical, 65, 75);
            SetResistance(ResistanceType.Fire, 80, 95);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.ResistenciaMagica, 50.1, 95.0);
            SetSkill(SkillName.Anatomia, 60.1, 100.0);
            SetSkill(SkillName.Briga, 60.1, 100.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 38;

            PackItem(new SulfurousAsh(3));

            AddItem(new LightSource());
            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Gems, 4);

            switch (Utility.Random(3))
            {
                case 0: PackItem(new LargeFireRock(2)); break;
            }
        }
        public override bool AutoDispel { get { return true; } }
        public override bool BleedImmune { get { return true; } }
        public override int TreasureMapLevel { get { return 1; } }
       

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature)
            {
                BaseCreature bc = (BaseCreature)from;

                if (bc.Controlled || bc.BardTarget == this)
                    damage = 0; // Immune to pets and provoked creatures
            }
        }

        public override void CheckReflect(Mobile caster, ref bool reflect)
        {
            reflect = true; // Every spell is reflected back to the caster
        }

        public FireRockElemental(Serial serial)
            : base(serial)
        {
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
