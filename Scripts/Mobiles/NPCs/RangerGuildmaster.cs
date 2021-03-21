using System;

namespace Server.Mobiles
{
    public class RangerGuildmaster : BaseGuildmaster
    {
        [Constructable]
        public RangerGuildmaster()
            : base("ranger")
        {
            this.SetSkill(SkillName.Adestramento, 64.0, 100.0);
            this.SetSkill(SkillName.Sobrevivencia, 75.0, 98.0);
            this.SetSkill(SkillName.Furtividade, 75.0, 98.0);
            this.SetSkill(SkillName.ResistenciaMagica, 75.0, 98.0);
            this.SetSkill(SkillName.Anatomia, 65.0, 88.0);
            this.SetSkill(SkillName.Atirar, 90.0, 100.0);
            this.SetSkill(SkillName.Sobrevivencia, 90.0, 100.0);
            this.SetSkill(SkillName.Furtividade, 60.0, 83.0);
            this.SetSkill(SkillName.Perfurante, 36.0, 68.0);
            this.SetSkill(SkillName.Adestramento, 36.0, 68.0);
            this.SetSkill(SkillName.Cortante, 45.0, 68.0);
        }

        public RangerGuildmaster(Serial serial)
            : base(serial)
        {
        }

        public override NpcGuild NpcGuild
        {
            get
            {
                return NpcGuild.RangersGuild;
            }
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