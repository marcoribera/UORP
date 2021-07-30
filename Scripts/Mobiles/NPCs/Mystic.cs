using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Mystic : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public Mystic()
            : base("- O Místico")
        {
            this.SetSkill(SkillName.PoderMagico, 65.0, 88.0);
            this.SetSkill(SkillName.Erudicao, 60.0, 83.0);
            this.SetSkill(SkillName.Misticismo, 64.0, 100.0);
            this.SetSkill(SkillName.ResistenciaMagica, 65.0, 88.0);
            this.SetSkill(SkillName.Briga, 36.0, 68.0);

            Persuadable = true;
            ControlSlots = 3;
            MinPersuadeSkill = 100;

        }

        public Mystic(Serial serial)
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
