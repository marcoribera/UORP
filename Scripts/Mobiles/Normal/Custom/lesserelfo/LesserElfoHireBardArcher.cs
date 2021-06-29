using System;
using Server.Items;

namespace Server.Mobiles
{
    public class LesserElfoHireBardArcher : BaseHire
    {
        [Constructable]
        public LesserElfoHireBardArcher()
            : base(AIType.AI_Archer)
        {
            this.SpeechHue = Utility.RandomDyedHue();
            this.Hue = Utility.RandomSkinHue();

            {
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
            }

            this.Title = "the bard";
            this.HairItemID = this.Race.RandomHair(this.Female);
            this.HairHue = this.Race.RandomHairHue();
            this.Race.RandomFacialHair(this);

            this.SetStr(16, 16);
            this.SetDex(26, 26);
            this.SetInt(26, 26);

            this.SetDamage(5, 10);

            this.SetSkill(SkillName.Anatomia, 35, 57);
            this.SetSkill(SkillName.Arcanismo, 22, 22);
            this.SetSkill(SkillName.Cortante, 45, 67);
            this.SetSkill(SkillName.Atirar, 16, 38);
            this.SetSkill(SkillName.Bloqueio, 45, 60);
            this.SetSkill(SkillName.Tocar, 66.0, 97.5);
            this.SetSkill(SkillName.Pacificar, 65.0, 87.5);

            this.Fame = 100;
            this.Karma = 100;

            Persuadable = true;
            ControlSlots = 1;
            MinPersuadeSkill = 35;
            IdiomaNativo = Mobiles.SpeechType.Avlitir;


            this.AddItem(new Shoes(Utility.RandomNeutralHue()));

            switch ( Utility.Random(2) )
            {
                case 0:
                    this.AddItem(new Doublet(Utility.RandomDyedHue()));
                    break;
                case 1:
                    this.AddItem(new Shirt(Utility.RandomDyedHue()));
                    break;
            }
            switch ( Utility.Random(4) )
            {
                case 0:
                    this.PackItem(new Harp());
                    break;
                case 1:
                    this.PackItem(new Lute());
                    break;
                case 2:
                    this.PackItem(new Drums());
                    break;
                case 3:
                    this.PackItem(new Tambourine());
                    break;
            }

            this.PackItem(new Longsword());
            this.AddItem(new Bow());
            this.PackItem(new Arrow(100));
            this.PackGold(10, 50);
        }

        public LesserElfoHireBardArcher(Serial serial)
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
