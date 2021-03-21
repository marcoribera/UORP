using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Bard : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public Bard()
            : base("the bard")
        {
            this.SetSkill(SkillName.Caos, 64.0, 100.0);
            this.SetSkill(SkillName.Tocar, 64.0, 100.0);
            this.SetSkill(SkillName.Pacificar, 65.0, 88.0);
            this.SetSkill(SkillName.Provocacao, 60.0, 83.0);
            this.SetSkill(SkillName.Atirar, 36.0, 68.0);
            this.SetSkill(SkillName.Cortante, 36.0, 68.0);
        }

        public Bard(Serial serial)
            : base(serial)
        {
        }

        public override NpcGuild NpcGuild
        {
            get
            {
                return NpcGuild.BardsGuild;
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
            this.m_SBInfos.Add(new SBBard());
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