using System;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    public class Jason : HumilityQuestMobile
    {
        public override int Greeting { get { return 1075764; } }

        public override bool IsActiveVendor { get { return true; } }
        public override bool CanTeach { get { return true; } }

        public override bool CheckTeach(SkillName skill, Mobile from)
        {
            return (skill == SkillName.Percepcao)
                || (skill == SkillName.Medicina)
                || (skill == SkillName.PoderMagico)
                || (skill == SkillName.Cortante);
        }

        public override void InitSBInfo()
        {
            SBInfos.Add(new SBHealer());
        }

        [Constructable]
        public Jason()
            : base("Jason", "the Healer")
        {
            SetSkill(SkillName.Percepcao, 80.0, 100.0);
            SetSkill(SkillName.PoderMagico, 80.0, 100.0);
            SetSkill(SkillName.Cortante, 80.0, 100.0);
        }

        public Jason(Serial serial)
            : base(serial)
        {
        }

        public override void InitBody()
        {
            this.InitStats(100, 100, 25);

            this.Female = false;
            this.Race = Race.Human;
            this.Body = 0x190;

            this.Hue = Race.RandomSkinHue();
            this.HairItemID = Race.RandomHair(false);
            this.HairHue = Race.RandomHairHue();
        }

        public override void InitOutfit()
        {
            this.AddItem(new Server.Items.Backpack());
            this.AddItem(new Server.Items.Robe(Utility.RandomYellowHue()));
            this.AddItem(new Server.Items.Sandals());
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