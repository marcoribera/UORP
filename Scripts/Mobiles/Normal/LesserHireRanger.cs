using System;
using Server.Items;

namespace Server.Mobiles
{
    public class LesserHireRanger : BaseHire
    {
        [Constructable]
        public LesserHireRanger()
        {
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();

            if (Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                AddItem(new ShortPants(Utility.RandomNeutralHue()));
            }

            Title = "";
            HairItemID = Race.RandomHair(Female);
            HairHue = Race.RandomHairHue();
            Race.RandomFacialHair(this);

            this.SetStr(80, 96);
            this.SetDex(80, 90);
            this.SetInt(26, 40);

            SetHits(150, 180);
            SetMana(80, 100);

            SetDamage(13, 24);

            SetSkill(SkillName.Briga, 15, 37);
            SetSkill(SkillName.Bloqueio, 45, 60);
            SetSkill(SkillName.Atirar, 33, 49);
            SetSkill(SkillName.Arcanismo, 40, 50);
            SetSkill(SkillName.Cortante, 35, 50);
            SetSkill(SkillName.Perfurante, 15, 37);
            SetSkill(SkillName.Anatomia, 65, 87);

            Fame = 100;
            Karma = 125;

            Persuadable = true;
            ControlSlots = 2;
            MinPersuadeSkill = 49;

            AddItem(new Shoes(Utility.RandomNeutralHue()));
            AddItem(new Shirt());

            // Pick a random sword
            switch ( Utility.Random(3))
            {
                case 0:
                    AddItem(new Longsword());
                    break;
                case 1:
                    AddItem(new VikingSword());
                    break;
                case 2:
                    AddItem(new Broadsword());
                    break;
            }

            SetWearable(new StuddedChest(), 0x59C);
            SetWearable(new StuddedArms(), 0x59C);
            SetWearable(new StuddedGloves(), 0x59C);
            SetWearable(new StuddedLegs(), 0x59C);
            SetWearable(new StuddedGorget(), 0x59C);

            PackItem(new Arrow(20));
            PackGold(10, 75);
        }

        public LesserHireRanger(Serial serial)
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
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);// version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
