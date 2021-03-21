using System;

namespace Server.Mobiles
{
    public class WarriorGuildmaster : BaseGuildmaster
    {
        [Constructable]
        public WarriorGuildmaster()
            : base("warrior")
        {
            this.SetSkill(SkillName.ConhecimentoArmas, 75.0, 98.0);
            this.SetSkill(SkillName.Bloqueio, 85.0, 100.0);
            this.SetSkill(SkillName.ResistenciaMagica, 60.0, 83.0);
            this.SetSkill(SkillName.Anatomia, 85.0, 100.0);
            this.SetSkill(SkillName.Cortante, 90.0, 100.0);
            this.SetSkill(SkillName.Contusivo, 60.0, 83.0);
            this.SetSkill(SkillName.Perfurante, 60.0, 83.0);
        }

        public WarriorGuildmaster(Serial serial)
            : base(serial)
        {
        }

        public override NpcGuild NpcGuild
        {
            get
            {
                return NpcGuild.WarriorsGuild;
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