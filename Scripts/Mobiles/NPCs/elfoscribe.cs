using System;
using System.Collections.Generic;
using Server.Engines.BulkOrders;

namespace Server.Mobiles
{
    public class ElfoScribe : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public ElfoScribe()
            : base("o escriba")
        {
            this.SetSkill(SkillName.PoderMagico, 60.0, 83.0);
            this.SetSkill(SkillName.Erudicao, 90.0, 100.0);
        }

        public ElfoScribe(Serial serial)
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
            this.m_SBInfos.Add(new SBScribe(this));
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            this.AddItem(new Server.Items.Robe(Utility.RandomNeutralHue()));
        }

        #region Bulk Orders
        public override BODType BODType { get { return BODType.Inscription; } }

        public override bool IsValidBulkOrder(Item item)
        {
            return (item is SmallInscriptionBOD || item is LargeInscriptionBOD);
        }

        public override bool SupportsBulkOrders(Mobile from)
        {
            return BulkOrderSystem.NewSystemEnabled && from is PlayerMobile && from.Skills[SkillName.Erudicao].Base > 0;
        }

        public override void OnSuccessfulBulkOrderReceive(Mobile from)
        {
            if (from is PlayerMobile)
                ((PlayerMobile)from).NextInscriptionBulkOrder = TimeSpan.Zero;
        }

        #endregion

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