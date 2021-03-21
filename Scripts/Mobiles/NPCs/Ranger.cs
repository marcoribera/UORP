using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Ranger : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public Ranger()
            : base("the ranger")
        {
            this.SetSkill(SkillName.Sobrevivencia, 55.0, 78.0);
            this.SetSkill(SkillName.Percepcao, 65.0, 88.0);
            this.SetSkill(SkillName.Furtividade, 45.0, 68.0);
            this.SetSkill(SkillName.Atirar, 65.0, 88.0);
            this.SetSkill(SkillName.Sobrevivencia, 65.0, 88.0);
            this.SetSkill(SkillName.Veterinaria, 60.0, 83.0);
        }

        public Ranger(Serial serial)
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
            this.m_SBInfos.Add(new SBRanger());
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            this.AddItem(new Server.Items.Shirt(Utility.RandomNeutralHue()));
            this.AddItem(new Server.Items.LongPants(Utility.RandomNeutralHue()));
            this.AddItem(new Server.Items.Bow());
            this.AddItem(new Server.Items.ThighBoots(Utility.RandomNeutralHue()));
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