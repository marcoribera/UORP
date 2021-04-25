using System;
using Server.Items;

namespace Server.Mobiles
{
    public class LesserSamurai : BaseCreature
    {
        [Constructable]
        public LesserSamurai()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            this.Title = "";

            this.InitStats(100, 100, 25);

            this.SetSkill(SkillName.ConhecimentoArmas, 20.0, 40.0);
            this.SetSkill(SkillName.Bushido, 20.0, 40.0);
            this.SetSkill(SkillName.Bloqueio, 20.0, 40.0);
            this.SetSkill(SkillName.Cortante, 20.0, 40.0);

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 40;

            this.SpeechHue = Utility.RandomDyedHue();

            this.Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
                this.Name = NameList.RandomName("female");
            }
            else
            {
                this.Body = 0x190;
                this.Name = NameList.RandomName("male");
            }

            switch ( Utility.Random(3) )
            {
                case 0:
                    this.AddItem(new Lajatang());
                    break;
                case 1:
                    this.AddItem(new Wakizashi());
                    break;
                case 2:
                    this.AddItem(new NoDachi());
                    break;
            }

            switch ( Utility.Random(3) )
            {
                case 0:
                    this.AddItem(new LeatherSuneate());
                    break;
                case 1:
                    this.AddItem(new PlateSuneate());
                    break;
                case 2:
                    this.AddItem(new StuddedHaidate());
                    break;
            }

            switch ( Utility.Random(4) )
            {
                case 0:
                    this.AddItem(new LeatherJingasa());
                    break;
                case 1:
                    this.AddItem(new ChainHatsuburi());
                    break;
                case 2:
                    this.AddItem(new HeavyPlateJingasa());
                    break;
                case 3:
                    this.AddItem(new DecorativePlateKabuto());
                    break;
            }

            this.AddItem(new LeatherDo());
            this.AddItem(new LeatherHiroSode());
            this.AddItem(new SamuraiTabi(Utility.RandomNondyedHue())); // TODO: Hue

            int hairHue = Utility.RandomNondyedHue();

            Utility.AssignRandomHair(this, hairHue);

            if (Utility.Random(7) != 0)
                Utility.AssignRandomFacialHair(this, hairHue);

            this.PackGold(250, 300);
        }

        public LesserSamurai(Serial serial)
            : base(serial)
        {
        }

        public override bool CanTeach
        {
            get
            {
                return true;
            }
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

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}