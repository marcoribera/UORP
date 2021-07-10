using System;
using Server.Items;

namespace Server.Mobiles
{
    public class LesserElfoHireFighter : BaseHire
    {
        [Constructable]
        public LesserElfoHireFighter()
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
            ControlSlots = 1;
            MinPersuadeSkill = 19;
            IdiomaNativo = Mobiles.SpeechType.Avlitir;


            switch ( Utility.Random(2))
            {
                case 0:
                    AddItem(new Shoes(Utility.RandomNeutralHue()));
                    break;
                case 1:
                    AddItem(new Boots(Utility.RandomNeutralHue()));
                    break;
            }

            AddItem(new Shirt());

            // Pick a random sword
            switch ( Utility.Random(5))
            {
                case 0:
                    AddItem(new Longsword());
                    break;
                case 1:
                    AddItem(new Broadsword());
                    break;
                case 2:
                    AddItem(new VikingSword());
                    break;
                case 3:
                    AddItem(new BattleAxe());
                    break;
                case 4:
                    AddItem(new TwoHandedAxe());
                    break;
            }

            // Pick a random shield
            if (FindItemOnLayer(Layer.TwoHanded) == null)
            {
                switch (Utility.Random(8))
                {
                    case 0:
                        AddItem(new BronzeShield());
                        break;
                    case 1:
                        AddItem(new HeaterShield());
                        break;
                    case 2:
                        AddItem(new MetalKiteShield());
                        break;
                    case 3:
                        AddItem(new MetalShield());
                        break;
                    case 4:
                        AddItem(new WoodenKiteShield());
                        break;
                    case 5:
                        AddItem(new WoodenShield());
                        break;
                    case 6:
                        AddItem(new OrderShield());
                        break;
                    case 7:
                        AddItem(new ChaosShield());
                        break;
                }
            }

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
            // Pick some armour
            switch( Utility.Random(4) )
            {
                case 0: // Leather
                    AddItem(new LeatherChest());
                    AddItem(new LeatherArms());
                    AddItem(new LeatherGloves());
                    AddItem(new LeatherGorget());
                    AddItem(new LeatherLegs());
                    break;
                case 1: // Studded Leather
                    AddItem(new StuddedChest());
                    AddItem(new StuddedArms());
                    AddItem(new StuddedGloves());
                    AddItem(new StuddedGorget());
                    AddItem(new StuddedLegs());
                    break;
                case 2: // Ringmail
                    AddItem(new RingmailChest());
                    AddItem(new RingmailArms());
                    AddItem(new RingmailGloves());
                    AddItem(new RingmailLegs());
                    break;
                case 3: // Chain
                    AddItem(new ChainChest());
                    //AddItem(new ChainCoif());
                    AddItem(new ChainLegs());
                    break;
            }

            PackGold(25, 100);
        }

        public LesserElfoHireFighter(Serial serial)
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
