using System;
using Server.Items;

namespace Server.Mobiles 
{
    public class HireMage : BaseHire
    {
        [Constructable]
        public HireMage()
            : base(AIType.AI_Mage)
        {
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            Title = "the mage";
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

            HairItemID = Race.RandomHair(Female);
            HairHue = Race.RandomHairHue();
            Race.RandomFacialHair(this);

            SetStr(61, 75);
            SetDex(81, 95);
            SetInt(86, 100);

            SetDamage(10, 23);

            SetSkill(SkillName.PoderMagico, 100.0, 125);
            SetSkill(SkillName.Arcanismo, 100, 125);
            SetSkill(SkillName.ResistenciaMagica, 100, 125);
            SetSkill(SkillName.Anatomia, 100, 125);
            SetSkill(SkillName.Contusivo, 100, 125);

            Fame = 100;
            Karma = 100;

            AddItem(new Shirt());

            AddItem(new Robe(Utility.RandomNeutralHue()));

            if(Utility.RandomBool())
                AddItem(new Shoes(Utility.RandomNeutralHue()));
            else
                AddItem(new ThighBoots());

            PackGold(20, 100);
        }

        public HireMage(Serial serial)
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
