using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an abyssmal horror corpse")]
    public class AbysmalHorror : BaseCreature
    {
        [Constructable]
        public AbysmalHorror()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an abyssmal horror";
            Body = 312;
            BaseSoundID = 0x451;

            SetStr(401, 420);
            SetDex(81, 90);
            SetInt(401, 420);

            SetHits(6000);

            SetDamage(13, 17);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 30, 35);
            SetResistance(ResistanceType.Fire, 100);
            SetResistance(ResistanceType.Cold, 50, 55);
            SetResistance(ResistanceType.Poison, 60, 65);
            SetResistance(ResistanceType.Energy, 77, 80);

            SetSkill(SkillName.Briga, 84.1, 88.0);
            SetSkill(SkillName.Anatomia, 100.0);
            SetSkill(SkillName.ResistenciaMagica, 117.6, 120.0);
            SetSkill(SkillName.Envenenamento, 70.0, 80.0);
            SetSkill(SkillName.Percepcao, 100.0);
            SetSkill(SkillName.Arcanismo, 112.6, 117.5);
            SetSkill(SkillName.PoderMagico, 200.0);
            SetSkill(SkillName.Necromancia, 120.0);
            SetSkill(SkillName.PoderMagico, 120.0);
            SetSkill(SkillName.PreparoFisico, 10.0, 20.0);

            Fame = 26000;
            Karma = -26000;

            VirtualArmor = 54;

            SetWeaponAbility(WeaponAbility.MortalStrike);
            SetWeaponAbility(WeaponAbility.WhirlwindAttack);
            SetWeaponAbility(WeaponAbility.Block);
            //Arcane Pyromancy
        }

        public AbysmalHorror(Serial serial)
            : base(serial)
        {
        }

        public override bool CanFlee { get { return false; } }

        public override bool IgnoreYoungProtection
        {
            get
            {
                return Core.ML;
            }
        }
        public override bool BardImmune
        {
            get
            {
                return !Core.SE;
            }
        }
        public override bool Unprovokable
        {
            get
            {
                return Core.SE;
            }
        }
        public override bool AreaPeaceImmune
        {
            get
            {
                return Core.SE;
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
