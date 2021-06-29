using System;
using Server.Items;

namespace Server.Mobiles
{
    public class Legionario : BaseHire
    {
        [Constructable]
        public Legionario()
        {
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();

            if (Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = "Legionaria";
            }
            else
            {
                Body = 0x190;
                Name = "Legionario";
            }

            HairItemID = Race.RandomHair(Female);
            HairHue = Race.RandomHairHue();
            Race.RandomFacialHair(this);

            SetStr(91, 91);
            SetDex(91, 91);
            SetInt(50, 50);

            SetDamage(7, 14);

            SetSkill(SkillName.Anatomia, 36, 67);
            SetSkill(SkillName.Perfurante, 64, 100);
            SetSkill(SkillName.Bloqueio, 60, 82);
            SetSkill(SkillName.PreparoFisico, 36, 67);
            SetSkill(SkillName.Briga, 25, 47);

            Fame = 100;
            Karma = 100;

            Persuadable = true;
            ControlSlots = 3;
            MinPersuadeSkill = 65;

            this.AddItem(new Sandals(Utility.RandomNeutralHue()));
            this.AddItem(new DragonTurtleHideHelm());
            this.AddItem(new HeaterShield());
            this.AddItem(new Leafblade());
            this.AddItem(new Tunic(Utility.RandomRedHue()));

            PackGold(25, 50);
        }

        public Legionario(Serial serial)
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
