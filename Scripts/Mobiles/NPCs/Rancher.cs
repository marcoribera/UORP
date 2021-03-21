using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Rancher : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public Rancher()
            : base("the rancher")
        {
            this.SetSkill(SkillName.Adestramento, 55.0, 78.0);
            this.SetSkill(SkillName.Adestramento, 55.0, 78.0);
            this.SetSkill(SkillName.Adestramento, 64.0, 100.0);
            this.SetSkill(SkillName.Veterinaria, 60.0, 83.0);
        }

        public Rancher(Serial serial)
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
            this.m_SBInfos.Add(new SBRancher());
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