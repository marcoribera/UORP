using System;

namespace Server.Mobiles
{
    [CorpseName("a platinum drake corpse")]
    public class PlatinumDrake : BaseCreature, IElementalCreature
    {
        private ElementType m_Type;

        public ElementType ElementType { get { return m_Type; } }

        [Constructable]
        public PlatinumDrake()
            : this((ElementType)Utility.Random(5))
        {
        }

        [Constructable]
        public PlatinumDrake(ElementType type)
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            m_Type = type;

            switch (type)
            {
                case ElementType.Physical:
                    Body = 0x589;
                    Hue = 0;
                    SetDamageType(ResistanceType.Physical, 100);
                    break;
                case ElementType.Fire:
                    Body = 0x58A;
                    Hue = 33929;
                    SetDamageType(ResistanceType.Physical, 0);
                    SetDamageType(ResistanceType.Fire, 100);
                    break;
                case ElementType.Cold:
                    Body = 0x58A;
                    Hue = 34134;
                    SetDamageType(ResistanceType.Physical, 0);
                    SetDamageType(ResistanceType.Cold, 100);
                    break;
                case ElementType.Poison:
                    Body = 0x58A;
                    Hue = 34136;
                    SetDamageType(ResistanceType.Physical, 0);
                    SetDamageType(ResistanceType.Poison, 100);
                    break;
                case ElementType.Energy:
                    Body = 0x58A;
                    Hue = 34141;
                    SetDamageType(ResistanceType.Physical, 0);
                    SetDamageType(ResistanceType.Energy, 100);
                    break;
            }

            Name = "Platinum Drake";
            Female = true;
            BaseSoundID = 362;

            SetStr(400, 430);
            SetDex(133, 152);
            SetInt(101, 140);

            SetHits(241, 258);

            SetDamage(11, 17);

            SetResistance(ResistanceType.Physical, 30, 50);
            SetResistance(ResistanceType.Fire, 30, 50);
            SetResistance(ResistanceType.Cold, 30, 50);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 30, 50);

            SetSkill(SkillName.ResistenciaMagica, 65.1, 80.0);
            SetSkill(SkillName.Anatomia, 65.1, 90.0);
            SetSkill(SkillName.Briga, 65.1, 80.0);
            SetSkill(SkillName.Percepcao, 50.0, 60.0);
            SetSkill(SkillName.PreparoFisico, 5.0, 20.0);

            Fame = 5500;
            Karma = -5500;

            VirtualArmor = 46;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 85.0;

            PackReg(3);
        }

        public PlatinumDrake(Serial serial)
            : base(serial)
        {
        }

        public static TrainingDefinition _PoisonDrakeDefinition;

        public override TrainingDefinition TrainingDefinition
        {
            get
            {
                if (m_Type == ElementType.Poison)
                {
                    if (_PoisonDrakeDefinition == null)
                    {
                        _PoisonDrakeDefinition = new TrainingDefinition(GetType(), Class.None, MagicalAbility.Dragon2, PetTrainingHelper.SpecialAbilityNone, PetTrainingHelper.WepAbility2, PetTrainingHelper.AreaEffectArea2, 2, 5);
                    }

                    return _PoisonDrakeDefinition;
                }

                return base.TrainingDefinition;
            }
        }

        public override bool ReacquireOnMovement { get { return !Controlled; } }
        public override int TreasureMapLevel { get { return 2; } }
        public override int Meat { get { return 10; } }
        public override int DragonBlood { get { return 8; } }
        public override int Hides { get { return 22; } }
        public override HideType HideType { get { return HideType.Horned; } }
        public override int Scales { get { return 2; } }
        public override ScaleType ScaleType { get { return ScaleType.Black; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat | FoodType.Fish; } }
        public override bool CanFly { get { return true; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);

            writer.Write((int)m_Type);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_Type = (ElementType)reader.ReadInt();
        }
    }
}
