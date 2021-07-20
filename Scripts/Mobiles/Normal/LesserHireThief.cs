using System;
using Server.Items;

namespace Server.Mobiles
{
    public class LesserHireThief : BaseHire
    {
        [Constructable]
        public LesserHireThief()
        {
            this.SpeechHue = Utility.RandomDyedHue();
            this.Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
                this.Name = NameList.RandomName("female");

                switch ( Utility.Random(2) )
                {
                    case 0:
                        this.AddItem(new Skirt(Utility.RandomNeutralHue()));
                        break;
                    case 1:
                        this.AddItem(new Kilt(Utility.RandomNeutralHue()));
                        break;
                }
            }
            else
            {
                this.Body = 0x190;
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

            this.SetDamage(10, 23);

            this.SetSkill(SkillName.Prestidigitacao, 66.0, 97.5);
            this.SetSkill(SkillName.Pacificar, 65.0, 87.5);
            this.SetSkill(SkillName.ResistenciaMagica, 25.0, 47.5);
            this.SetSkill(SkillName.Medicina, 65.0, 87.5);
            this.SetSkill(SkillName.Anatomia, 65.0, 87.5);
            this.SetSkill(SkillName.Perfurante, 30.0, 48);
            this.SetSkill(SkillName.Bloqueio, 30.0, 48);
            this.SetSkill(SkillName.Mecanica, 65, 87);
            this.SetSkill(SkillName.Furtividade, 65, 87);
            this.SetSkill(SkillName.Prestidigitacao, 65, 87);

            this.Fame = 100;
            this.Karma = 0;


            Persuadable = true;
            ControlSlots = 2;
            MinPersuadeSkill = 49;

            this.AddItem(new Sandals(Utility.RandomNeutralHue()));
            this.AddItem(new Dagger());
            switch ( Utility.Random(2) )
            {
                case 0:
                    this.AddItem(new Doublet(Utility.RandomNeutralHue()));
                    break;
                case 1:
                    this.AddItem(new Shirt(Utility.RandomNeutralHue()));
                    break;
            }

            this.PackGold(0, 25);
        }

        public LesserHireThief(Serial serial)
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
