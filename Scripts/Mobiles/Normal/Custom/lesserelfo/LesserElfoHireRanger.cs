using System;
using Server.Items;

namespace Server.Mobiles
{
    public class LesserElfoHireRanger : BaseHire
    {
        [Constructable]
        public LesserElfoHireRanger()
        {

            this.SpeechHue = Utility.RandomDyedHue();
            this.Hue = Utility.RandomSkinHue();

            this.Title = "";
            this.HairItemID = this.Race.RandomHair(this.Female);
            this.HairHue = this.Race.RandomHairHue();
            this.Race.RandomFacialHair(this);
            InitStats(100, 100, 25);
            this.Race = Race.Elf;

            SpeechHue = Utility.RandomDyedHue();

            if (Female)
            {
                Body = 0x25E;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x25D;
                Name = NameList.RandomName("male");
            }

            Hue = Utility.RandomSkinHue();
            Utility.AssignRandomHair(this);
            Utility.AssignRandomFacialHair(this);

            this.SpeechHue = Utility.RandomDyedHue();
            this.Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x25E;
                this.Name = NameList.RandomName("female");
                this.AddItem(new ShortPants(Utility.RandomNeutralHue()));
            }
            else
            {
                this.Body = 0x25D;
                this.Name = NameList.RandomName("male");
                this.AddItem(new ShortPants(Utility.RandomNeutralHue()));
            }
            this.Title = "";
            this.HairItemID = this.Race.RandomHair(this.Female);
            this.HairHue = this.Race.RandomHairHue();
            this.Race.RandomFacialHair(this);

            this.SetStr(80, 96);
            this.SetDex(80, 90);
            this.SetInt(26, 40);

            SetHits(150, 180);
            SetMana(80, 100);

            this.SetDamage(5, 10);

            this.SetSkill(SkillName.Anatomia, 35, 57);
            this.SetSkill(SkillName.Arcanismo, 10, 19);
            this.SetSkill(SkillName.Cortante, 45, 67);
            this.SetSkill(SkillName.Atirar, 10, 19);
            this.SetSkill(SkillName.Bloqueio, 45, 60);
            this.SetSkill(SkillName.Tocar, 66.0, 97.5);
            this.SetSkill(SkillName.Pacificar, 65.0, 87.5);

            this.Fame = 100;
            this.Karma = 100;

            Persuadable = true;
            ControlSlots = 2;
            MinPersuadeSkill = 49;
            IdiomaNativo = Mobiles.SpeechType.Avlitir;


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

        public LesserElfoHireRanger(Serial serial)
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
