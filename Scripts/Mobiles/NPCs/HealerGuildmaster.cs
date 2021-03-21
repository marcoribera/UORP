using System;

namespace Server.Mobiles
{
    public class HealerGuildmaster : BaseGuildmaster
    {
        [Constructable]
        public HealerGuildmaster()
            : base("healer")
        {
            this.SetSkill(SkillName.Anatomia, 85.0, 100.0);
            this.SetSkill(SkillName.Medicina, 90.0, 100.0);
            this.SetSkill(SkillName.Percepcao, 75.0, 98.0);
            this.SetSkill(SkillName.ResistenciaMagica, 75.0, 98.0);
            this.SetSkill(SkillName.PoderMagico, 65.0, 88.0);
        }

        public HealerGuildmaster(Serial serial)
            : base(serial)
        {
        }

        public override NpcGuild NpcGuild
        {
            get
            {
                return NpcGuild.HealersGuild;
            }
        }
        public override VendorShoeType ShoeType
        {
            get
            {
                return VendorShoeType.Sandals;
            }
        }
        public override void InitOutfit()
        {
            base.InitOutfit();

            this.AddItem(new Server.Items.Robe(Utility.RandomYellowHue()));
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