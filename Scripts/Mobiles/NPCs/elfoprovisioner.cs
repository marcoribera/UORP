using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class ElfoProvisioner : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public ElfoProvisioner()
            : base("o vende tudo")
        {
            this.SetSkill(SkillName.Sobrevivencia, 45.0, 68.0);
            this.SetSkill(SkillName.Anatomia, 45.0, 68.0);
        }

        public ElfoProvisioner(Serial serial)
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
            this.m_SBInfos.Add(new SBProvisioner());

            if (this.IsTokunoVendor)
                this.m_SBInfos.Add(new SBSEHats());
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
