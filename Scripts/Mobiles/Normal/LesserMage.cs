using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class LesserMage : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public LesserMage()
            : base("")
        {
            this.SetSkill(SkillName.PoderMagico, 30.0, 44.0);
            this.SetSkill(SkillName.Erudicao, 60.0, 83.0);
            this.SetSkill(SkillName.Arcanismo, 32.0, 50.0);
            this.SetSkill(SkillName.ResistenciaMagica, 30.0, 50.0);
            this.SetSkill(SkillName.Briga, 15.0, 40.0);

            Persuadable = true;
            ControlSlots = 2;
            MinPersuadeSkill = 50;
            this.SetStr(80, 96);
            this.SetDex(80, 90);
            this.SetInt(26, 40);

            SetHits(150, 180);
            SetMana(80, 100);

        }

        public LesserMage(Serial serial)
            : base(serial)
        {
        }

        public override NpcGuild NpcGuild
        {
            get
            {
                return NpcGuild.MagesGuild;
            }
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
            this.m_SBInfos.Add(new SBMage());
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
