using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class LesserMystic : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public LesserMystic()
            : base("")
        {
            this.SetSkill(SkillName.PoderMagico, 65.0, 88.0);
            this.SetSkill(SkillName.Erudicao, 60.0, 83.0);
            this.SetSkill(SkillName.Misticismo, 30.0, 50.0);
            this.SetSkill(SkillName.ResistenciaMagica, 65.0, 88.0);
            this.SetSkill(SkillName.Briga, 30.0, 50.0);

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 51;

        }

        public LesserMystic(Serial serial)
            : base(serial)
        {
        }

        public override VendorShoeType ShoeType
        {
            get
            {
                return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals;
            }
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
            this.m_SBInfos.Add(new SBMystic());
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            this.AddItem(new Server.Items.Robe(Utility.RandomBlueHue()));
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
    }
}
