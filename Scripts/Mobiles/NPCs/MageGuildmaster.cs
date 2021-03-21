using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Targeting;
using Server.Items;
using Server.Gumps;

namespace Server.Mobiles
{
    public class MageGuildmaster : BaseGuildmaster
    {
        public override bool ConvertsMageArmor { get { return true; } }

        [Constructable]
        public MageGuildmaster()
            : base("mage")
        {
            SetSkill(SkillName.PoderMagico, 85.0, 100.0);
            SetSkill(SkillName.Erudicao, 65.0, 88.0);
            SetSkill(SkillName.ResistenciaMagica, 64.0, 100.0);
            SetSkill(SkillName.Arcanismo, 90.0, 100.0);
            SetSkill(SkillName.Briga, 60.0, 83.0);
            SetSkill(SkillName.Contusivo, 36.0, 68.0);
        }
        
        public MageGuildmaster(Serial serial)
            : base(serial)
        {
        }

        public override NpcGuild NpcGuild
        {
            get
            {
                return NpcGuild.MagesGuild;
            }
        }
        public override VendorShoeType ShoeType
        {
            get
            {
                return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals;
            }
        }
        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new Server.Items.Robe(Utility.RandomBlueHue()));
            AddItem(new Server.Items.GnarledStaff());
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
