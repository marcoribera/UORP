using System;
using Server.Items;

namespace Server.Mobiles
{
    public class LesserHireRangerArcher : BaseHire
    {
        [Constructable]
        public LesserHireRangerArcher()
            : base(AIType.AI_Archer)
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

            SetSkill(SkillName.Briga, 1, 15);
            SetSkill(SkillName.Bloqueio, 20, 30);
            SetSkill(SkillName.Atirar, 23, 43);
            SetSkill(SkillName.Arcanismo, 22, 33);
            SetSkill(SkillName.Cortante, 18, 43);
            SetSkill(SkillName.Perfurante, 15, 37);
            SetSkill(SkillName.Anatomia, 20, 40);

            Fame = 100;
            Karma = 125;

            Persuadable = true;
            ControlSlots = 2;
            MinPersuadeSkill = 50;

            AddItem(new Shoes(Utility.RandomNeutralHue()));
            AddItem(new Shirt());

            // Pick a random sword
            switch ( Utility.Random(2))
            {
                case 0:
                    AddItem(new Bow());
                    break;
                case 1:
                    AddItem(new CompositeBow());
                    break;
            }

            AddItem(new RangerChest());
            AddItem(new RangerArms());
            AddItem(new RangerGloves());
            AddItem(new RangerGorget());
            AddItem(new RangerLegs());

            PackItem(new Arrow(20));
            PackGold(10, 75);
        }

        public LesserHireRangerArcher(Serial serial)
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
