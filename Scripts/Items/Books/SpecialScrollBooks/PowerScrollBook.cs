using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using System.Collections.Generic;

namespace Server.Items
{
    [Flipable(0x9A95, 0x9AA7)]
    public class PowerScrollBook : BaseSpecialScrollBook
    {
        public override Type ScrollType { get { return typeof(PowerScroll); } }
        public override int LabelNumber { get { return 1155684; } } // Power Scroll Book
        public override int BadDropMessage { get { return 1155691; } } // This book only holds Power Scrolls.
        public override int DropMessage { get { return 1155692; } }    // You add the scroll to your Power Scroll book.
        public override int RemoveMessage { get { return 1155690; } }  // You remove a Power Scroll and put it in your pack.
        public override int GumpTitle { get { return 1155689; } }  // Power Scrolls

        [Constructable]
        public PowerScrollBook() 
            : base(0x9A95)
        {
            Hue = 1153;
        }

        public PowerScrollBook(Serial serial)
            : base(serial)
        {
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

        public override Dictionary<SkillCat, List<SkillName>> SkillInfo { get { return _SkillInfo; } }
        public override Dictionary<int, double> ValueInfo { get { return _ValueInfo; } }

        public static Dictionary<SkillCat, List<SkillName>> _SkillInfo;
        public static Dictionary<int, double> _ValueInfo;

        public static void Initialize()
        {
            _SkillInfo = new Dictionary<SkillCat, List<SkillName>>();

            _SkillInfo[SkillCat.Miscellaneous] = new List<SkillName>();
            _SkillInfo[SkillCat.Combat] = new List<SkillName>() { SkillName.Anatomia, SkillName.Atirar, SkillName.Perfurante, SkillName.PreparoFisico, SkillName.Medicina, SkillName.Contusivo, SkillName.Bloqueio, SkillName.Cortante, SkillName.Anatomia, SkillName.Atirar, SkillName.Briga };
            _SkillInfo[SkillCat.TradeSkills] = new List<SkillName>() { SkillName.Ferraria, SkillName.Costura };
            _SkillInfo[SkillCat.Magic] = new List<SkillName>() { SkillName.Bushido, SkillName.Ordem, SkillName.PoderMagico, SkillName.ImbuirMagica, SkillName.Arcanismo, SkillName.Misticismo, SkillName.Necromancia, SkillName.Ninjitsu, SkillName.ResistenciaMagica, SkillName.Feiticaria, SkillName.PoderMagico };
            _SkillInfo[SkillCat.Wilderness] = new List<SkillName>() { SkillName.Adestramento, SkillName.Adestramento, SkillName.Sobrevivencia, SkillName.Veterinaria };
            _SkillInfo[SkillCat.Thievery] = new List<SkillName>() { SkillName.Prestidigitacao, SkillName.Furtividade };
            _SkillInfo[SkillCat.Bard] = new List<SkillName>() { SkillName.Caos, SkillName.Tocar, SkillName.Pacificar, SkillName.Provocacao };

            _ValueInfo = new Dictionary<int, double>();

            _ValueInfo[1155685] = 105;
            _ValueInfo[1155686] = 110;
            _ValueInfo[1155687] = 115;
            _ValueInfo[1155688] = 120;
        }
    }
}
