using System;
using Server.Items;

namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.HumanBrigand")]
    public class LesserBrigand : BaseCreature
    {
        [Constructable]
        public LesserBrigand()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Title = "";
            Hue = Utility.RandomSkinHue();

            if (Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
                AddItem(new Skirt(Utility.RandomNeutralHue()));
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                AddItem(new ShortPants(Utility.RandomNeutralHue()));
            }

            this.SetStr(80, 96);
            this.SetDex(80, 90);
            this.SetInt(26, 40);

            SetHits(150, 180);
            SetMana(80, 100);

            SetDamage(10, 23);

            SetSkill(SkillName.Perfurante, 30, 45);
            SetSkill(SkillName.Contusivo, 30, 45);
            SetSkill(SkillName.ResistenciaMagica, 25.0, 47.5);
            SetSkill(SkillName.Cortante, 30, 45);
            SetSkill(SkillName.Anatomia, 30, 45);
            SetSkill(SkillName.Briga, 15.0, 37.5);

            Fame = 1000;
            Karma = -1000;

            Persuadable = true;
            ControlSlots = 2;
            MinPersuadeSkill = 50;

            AddItem(new Boots(Utility.RandomNeutralHue()));
            AddItem(new FancyShirt());
            AddItem(new Bandana());

            switch ( Utility.Random(7))
            {
                case 0:
                    AddItem(new Longsword());
                    break;
                case 1:
                    AddItem(new Cutlass());
                    break;
                case 2:
                    AddItem(new Broadsword());
                    break;
                case 3:
                    AddItem(new Axe());
                    break;
                case 4:
                    AddItem(new Club());
                    break;
                case 5:
                    AddItem(new Dagger());
                    break;
                case 6:
                    AddItem(new Spear());
                    break;
            }

            Utility.AssignRandomHair(this);
        }

        public LesserBrigand(Serial serial)
            : base(serial)
        {
        }

        public override bool ClickTitle
        {
            get
            {
                return false;
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

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.75)
                c.DropItem(new SeveredHumanEars());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
