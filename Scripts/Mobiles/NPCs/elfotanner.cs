using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class elfotanner : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public elfotanner()
            : base("o curtidor")
        {
            this.SetSkill(SkillName.Costura, 36.0, 68.0);
        }

        public elfotanner(Serial serial)
            : base(serial)
        {
        }

        protected override List<SBInfo> SBInfos
        {
            get
            {
                return this.m_SBInfos;
            }
        }
        public override void InitSBInfo()
        {
            if (!this.IsStygianVendor)
            {
                this.m_SBInfos.Add(new SBTanner());
            }
            else
            {
                this.m_SBInfos.Add(new SBSATanner());
            }
        }

        public override bool ValidateBought(Mobile buyer, Item item)
        {
            if (item is Server.Items.TaxidermyKit && buyer.Skills[SkillName.Carpintaria].Value < 90.1)
            {
                this.SayTo(buyer, 1042603, 0x3B2); // You would not understand how to use the kit.
                return false;
            }

            return true;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
        public override void InitBody()
        {
            InitStats(100, 100, 25);
            this.Race = Race.Elf;
            Female = GetGender();
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
    }
}
