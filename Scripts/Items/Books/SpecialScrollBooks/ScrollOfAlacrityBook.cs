using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using System.Collections.Generic;

namespace Server.Items
{
    [Flipable(0x9981, 0x9982)]
    public class ScrollOfAlacrityBook : BaseSpecialScrollBook
    {
        public override Type ScrollType { get { return typeof(ScrollOfAlacrity); } }
        public override int LabelNumber { get { return 1154321; } } // Scrolls of Alacrity Book
        public override int BadDropMessage { get { return 1154323; } } // This book only holds Scrolls of Alacrity.
        public override int DropMessage { get { return 1154326; } }    // You add the scroll to your Scrolls of Alacrity book.
        public override int RemoveMessage { get { return 1154322; } }  // You remove a Scroll of Alacrity and put it in your pack.
        public override int GumpTitle { get { return 1154324; } }  // Alacrity Scrolls

        [Constructable]
        public ScrollOfAlacrityBook()
            : base(0x9981)
        {
            Hue = 1195;
        }

        public ScrollOfAlacrityBook(Serial serial)
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

            _SkillInfo[SkillCat.Miscellaneous] = new List<SkillName>() { SkillName.ConhecimentoArmas, SkillName.Carisma, SkillName.Sobrevivencia, SkillName.Erudicao, SkillName.Percepcao, SkillName.Erudicao, SkillName.Alquimia};
            _SkillInfo[SkillCat.Combat] = new List<SkillName>() { SkillName.Anatomia, SkillName.Atirar, SkillName.Perfurante, SkillName.PreparoFisico, SkillName.Medicina, SkillName.Contusivo, SkillName.Bloqueio, SkillName.Cortante, SkillName.Anatomia, SkillName.Atirar, SkillName.Briga };
            _SkillInfo[SkillCat.TradeSkills] = new List<SkillName>() { SkillName.Alquimia, SkillName.Ferraria, SkillName.Carpintaria, SkillName.Carpintaria, SkillName.Culinaria, SkillName.Erudicao, SkillName.Extracao, SkillName.Extracao, SkillName.Costura, SkillName.Mecanica };
            _SkillInfo[SkillCat.Magic] = new List<SkillName>() { SkillName.Bushido, SkillName.Ordem, SkillName.PoderMagico, SkillName.ImbuirMagica, SkillName.Arcanismo, SkillName.Misticismo, SkillName.Necromancia, SkillName.Ninjitsu, SkillName.ResistenciaMagica, SkillName.Feiticaria, SkillName.PoderMagico };
            _SkillInfo[SkillCat.Wilderness] = new List<SkillName>() { SkillName.Adestramento, SkillName.Adestramento, SkillName.Sobrevivencia, SkillName.Adestramento, SkillName.Sobrevivencia, SkillName.Veterinaria };
            _SkillInfo[SkillCat.Thievery] = new List<SkillName>() { SkillName.Percepcao, SkillName.Furtividade, SkillName.Mecanica, SkillName.Envenenamento, SkillName.Mecanica, SkillName.Prestidigitacao, SkillName.Prestidigitacao, SkillName.Furtividade };
            _SkillInfo[SkillCat.Bard] = new List<SkillName>() { SkillName.Caos, SkillName.Tocar, SkillName.Pacificar, SkillName.Provocacao };

            _ValueInfo = new Dictionary<int, double>();

            _ValueInfo[1154324] = 0.0;
        }
    }
}
