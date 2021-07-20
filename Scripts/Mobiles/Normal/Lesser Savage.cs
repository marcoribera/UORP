using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a savage corpse")]
    public class LesserSavage : BaseCreature
    {
        [Constructable]
        public LesserSavage()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = NameList.RandomName("savage");

            if (this.Female = Utility.RandomBool())
                this.Body = 184;
            else
                this.Body = 183;

            this.SetStr(96, 115);
            this.SetDex(86, 105);
            this.SetInt(51, 65);

            this.SetDamage(23, 27);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetSkill(SkillName.Perfurante, 30.0, 45.5);
            this.SetSkill(SkillName.Contusivo, 30.0, 45.5);
            this.SetSkill(SkillName.Envenenamento, 60.0, 82.5);
            this.SetSkill(SkillName.ResistenciaMagica, 57.5, 80.0);
            this.SetSkill(SkillName.Cortante, 30.0, 45.5);
            this.SetSkill(SkillName.Anatomia, 60.0, 82.5);

            this.Fame = 1000;
            this.Karma = -1000;

            Persuadable = true;
            ControlSlots = 2;
            MinPersuadeSkill = 45;

            this.PackItem(new Bandage(Utility.RandomMinMax(1, 15)));

            if (this.Female && 0.1 > Utility.RandomDouble())
                this.PackItem(new TribalBerry());
            else if (!this.Female && 0.1 > Utility.RandomDouble())
                this.PackItem(new BolaBall());

            this.AddItem(new Spear());
            this.AddItem(new BoneArms());
            this.AddItem(new BoneLegs());

            this.SetStr(80, 96);
            this.SetDex(80, 90);
            this.SetInt(26, 40);

            SetHits(150, 180);
            SetMana(80, 100);

            if (0.5 > Utility.RandomDouble())
                this.AddItem(new SavageMask());
            else if (0.1 > Utility.RandomDouble())
                this.AddItem(new OrcishKinMask());
        }

        public LesserSavage(Serial serial)
            : base(serial)
        {
        }

        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }
        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }

        public override TribeType Tribe { get { return TribeType.Savage; } }

        public override OppositionGroup OppositionGroup
        {
            get
            {
                return OppositionGroup.SavagesAndOrcs;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Meager);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.BodyMod == 183 || m.BodyMod == 184)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            if (aggressor.BodyMod == 183 || aggressor.BodyMod == 184)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                aggressor.BodyMod = 0;
                aggressor.HueMod = -1;
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
                aggressor.SendLocalizedMessage(1040008); // Your skin is scorched as the tribal paint burns away!

                if (aggressor is PlayerMobile)
                    ((PlayerMobile)aggressor).SavagePaintExpiration = TimeSpan.Zero;
            }
        }

        public override void AlterMeleeDamageTo(Mobile to, ref int damage)
        {
            if (to is Dragon || to is WhiteWyrm || to is SwampDragon || to is Drake || to is Nightmare || to is Hiryu || to is LesserHiryu || to is Daemon)
                damage *= 3;
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
