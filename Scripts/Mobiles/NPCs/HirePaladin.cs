using System;
using Server.Items;

namespace Server.Mobiles 
{
    public class HirePaladin : BaseHire 
    {
        [Constructable] 
        public HirePaladin()
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
            }

            Title = "the paladin";
            HairItemID = Race.RandomHair(Female);
            HairHue = Race.RandomHairHue();
            Race.RandomFacialHair(this);

            switch( Utility.Random(5) )
            {
                case 0:
                    break;
                case 1:
                    AddItem(new Bascinet());
                    break;
                case 2:
                    AddItem(new CloseHelm());
                    break;
                case 3:
                    AddItem(new NorseHelm());
                    break;
                case 4:
                    AddItem(new Helmet());
                    break;
            }

            SetStr(86, 100);
            SetDex(81, 95);
            SetInt(61, 75);

            SetDamage(10, 23);

            SetSkill(SkillName.Cortante, 66.0, 97.5);
            SetSkill(SkillName.Anatomia, 65.0, 87.5);
            SetSkill(SkillName.ResistenciaMagica, 25.0, 47.5);
            SetSkill(SkillName.Medicina, 65.0, 87.5);
            SetSkill(SkillName.Anatomia, 65.0, 87.5);
            SetSkill(SkillName.Briga, 15.0, 37.5);
            SetSkill(SkillName.Bloqueio, 45.0, 60.5);
            SetSkill(SkillName.Ordem, 85, 100);

            Fame = 100;
            Karma = 250;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 100;

            AddItem(new Shoes(Utility.RandomNeutralHue()));
            AddItem(new Shirt());
            AddItem(new VikingSword());
            AddItem(new MetalKiteShield());
 
            AddItem(new PlateChest());
            AddItem(new PlateLegs());
            AddItem(new PlateArms());
            AddItem(new LeatherGorget());
            PackGold(20, 100);
        }

        public HirePaladin(Serial serial)
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