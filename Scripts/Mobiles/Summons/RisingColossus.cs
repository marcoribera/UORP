using Server.Items;
using System;

namespace Server.Mobiles
{
    public class RisingColossus : BaseCreature
    {
        private int m_DispelDifficulty;
        public override double DispelDifficulty { get { return m_DispelDifficulty; } }
        public override double DispelFocus { get { return 45.0; } }

        [Constructable]
        public RisingColossus(Mobile m, double baseskill, double boostskill)
            : base(AIType.AI_Mystic, FightMode.Closest, 10, 1, 0.4, 0.5)
        {
            int level = (int)(baseskill + boostskill);
            int statbonus = (int)((baseskill - 83) / 1.3 + ((boostskill - 30) / 1.3) + 6);
            int hitsbonus = (int)((baseskill - 83) * 1.14 + ((boostskill - 30) * 1.03) + 20);
            double skillvalue = boostskill != 0 ? ((baseskill + boostskill) / 2) : ((baseskill + 20) / 2);

            Name = "a rising colossus";
            Body = 829;

            SetHits(315 + hitsbonus);

            SetStr(677 + statbonus);
            SetDex(107 + statbonus);
            SetInt(127 + statbonus);

            SetDamage(level / 12, level / 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 65, 70);
            SetResistance(ResistanceType.Fire, 50, 55);
            SetResistance(ResistanceType.Cold, 50, 55);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 65, 70);

            SetSkill(SkillName.ResistenciaMagica, skillvalue);
            SetSkill(SkillName.Anatomia, skillvalue);
            SetSkill(SkillName.Briga, skillvalue);
            SetSkill(SkillName.Anatomia, skillvalue);
            SetSkill(SkillName.Misticismo, skillvalue);
            SetSkill(SkillName.Percepcao, 70.0);
            SetSkill(SkillName.PoderMagico, skillvalue);
            SetSkill(SkillName.Misticismo, m.Skills[SkillName.Misticismo].Value);
            SetSkill(SkillName.PreparoFisico, m.Skills[SkillName.PreparoFisico].Value);

            VirtualArmor = 58;
            ControlSlots = 5;

            m_DispelDifficulty = 91 + (int)((baseskill * 83) / 5.2);

            SetWeaponAbility(WeaponAbility.ArmorIgnore);
            SetWeaponAbility(WeaponAbility.CrushingBlow);
        }

        public override double GetFightModeRanking(Mobile m, FightMode acqType, bool bPlayerOnly)
        {
            return (m.Int + m.Skills[SkillName.Arcanismo].Value) / Math.Max(GetDistanceToSqrt(m), 1.0);
        }

        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }

        public override int GetAttackSound()
        {
            return 0x627;
        }

        public override int GetHurtSound()
        {
            return 0x629;
        }

        public override bool BleedImmune { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public RisingColossus(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);

            writer.Write((int)m_DispelDifficulty);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_DispelDifficulty = reader.ReadInt();
        }
    }
}
