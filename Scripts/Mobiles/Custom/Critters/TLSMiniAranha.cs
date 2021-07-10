using System;

namespace Server.Mobiles
{
    [CorpseName("corpo da pequena aranha")]
    public class TLSMiniAranha : BaseCreature
    {
        [Constructable]
        public TLSMiniAranha()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "um pequeno escorpiao";
            this.Body = 1560;
            this.Hue = Utility.RandomSnakeHue();
            this.BaseSoundID = 0x388;

            this.SetStr(22, 34);
            this.SetDex(16, 25);
            this.SetInt(6, 10);

            this.SetHits(15, 19);
            this.SetMana(0);

            this.SetDamage(1, 4);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 15, 20);
            this.SetResistance(ResistanceType.Poison, 20, 30);

            this.SetSkill(SkillName.Envenenamento, 50.1, 70.0);
            this.SetSkill(SkillName.ResistenciaMagica, 15.1, 20.0);
            this.SetSkill(SkillName.Anatomia, 19.3, 34.0);
            this.SetSkill(SkillName.Briga, 19.3, 34.0);

            this.Fame = 300;
            this.Karma = -300;

            this.VirtualArmor = 16;

            this.Tamable = true;
            this.ControlSlots = 1;
            this.MinTameSkill = 59.1;
        }

        public TLSMiniAranha(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Lesser;
            }
        }
        public override Poison HitPoison
        {
            get
            {
                return Poison.Lesser;
            }
        }
        public override bool DeathAdderCharmable
        {
            get
            {
                return true;
            }
        }
        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override FoodType FavoriteFood
        {
            get
            {
                return FoodType.Eggs;
            }
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