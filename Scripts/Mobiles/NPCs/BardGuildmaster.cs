using System;

namespace Server.Mobiles
{
    public class BardGuildmaster : BaseGuildmaster
    {
        [Constructable]
        public BardGuildmaster()
            : base("bard")
        {
            this.SetSkill(SkillName.Atirar, 80.0, 100.0);
            this.SetSkill(SkillName.Caos, 80.0, 100.0);
            this.SetSkill(SkillName.Tocar, 80.0, 100.0);
            this.SetSkill(SkillName.Pacificar, 80.0, 100.0);
            this.SetSkill(SkillName.Provocacao, 80.0, 100.0);
            this.SetSkill(SkillName.Cortante, 80.0, 100.0);
        }

        public BardGuildmaster(Serial serial)
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