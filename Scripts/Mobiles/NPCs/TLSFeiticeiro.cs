using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class TLSFeiticeiro : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public TLSFeiticeiro()
            : base("- O Feiticeiro")
        {
            this.SetSkill(SkillName.ImbuirMagica, 65.0, 88.0);
            this.SetSkill(SkillName.Erudicao, 60.0, 83.0);
            this.SetSkill(SkillName.Feiticaria, 64.0, 100.0);
            this.SetSkill(SkillName.ResistenciaMagica, 65.0, 88.0);
            this.SetSkill(SkillName.Briga, 36.0, 68.0);

            Persuadable = true;
            ControlSlots = 3;
            MinPersuadeSkill = 100;

        }

        public TLSFeiticeiro(Serial serial)
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
            this.m_SBInfos.Add(new SBFeiticeiro());
        }

        public override void InitOutfit()
        {
            Item item = (Utility.RandomBool() ? null : new Server.Items.BoneHelm());

            if (item != null && !EquipItem(item))
            {
                item.Delete();
                item = null;
            }

            if (item == null)
                AddItem(new Server.Items.MaskOfKhalAnkur());
            AddItem(new Server.Items.BoneLegs());
            AddItem(new Server.Items.BoneChest());
            AddItem(new Server.Items.GlassStaff());

            base.InitOutfit();
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
