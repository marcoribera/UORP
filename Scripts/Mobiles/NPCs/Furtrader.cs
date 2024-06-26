using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Furtrader : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public Furtrader()
            : base("the furtrader")
        {
            this.SetSkill(SkillName.Sobrevivencia, 55.0, 78.0);
            //SetSkill( SkillName.Alquimia, 60.0, 83.0 );
            this.SetSkill(SkillName.Adestramento, 85.0, 100.0);
            this.SetSkill(SkillName.Culinaria, 45.0, 68.0);
            this.SetSkill(SkillName.Sobrevivencia, 36.0, 68.0);
        }

        public Furtrader(Serial serial)
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
            this.m_SBInfos.Add(new SBFurtrader());
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