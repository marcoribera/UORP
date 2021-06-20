using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Thief : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public Thief()
            : base("the thief")
        {
            SetSkill(SkillName.Sobrevivencia, 55.0, 78.0);
            SetSkill(SkillName.Percepcao, 65.0, 88.0);
            SetSkill(SkillName.Furtividade, 45.0, 68.0);
            SetSkill(SkillName.Atirar, 65.0, 88.0);
            SetSkill(SkillName.Sobrevivencia, 65.0, 88.0);
            SetSkill(SkillName.Veterinaria, 60.0, 83.0);
            SetSkill(SkillName.Mecanica, 75.0, 98.0);

            Persuadable = true;
            ControlSlots = 3;
            MinPersuadeSkill = 90;
        }

        public Thief(Serial serial)
            : base(serial)
        {
        }

        protected override List<SBInfo> SBInfos
        {
            get
            {
                return m_SBInfos;
            }
        }
        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBThief());
        }

        public override void InitOutfit()
        {
            AddItem(new Server.Items.Shirt(Utility.RandomNeutralHue()));
            AddItem(new Server.Items.LongPants(Utility.RandomNeutralHue()));
            AddItem(new Server.Items.Dagger());
            AddItem(new Server.Items.ThighBoots(Utility.RandomNeutralHue()));

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
