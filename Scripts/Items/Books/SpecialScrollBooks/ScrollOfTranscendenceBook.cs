using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using System.Collections.Generic;

namespace Server.Items
{
    [Flipable(0x577E, 0x577F)]
    public class ScrollOfTranscendenceBook : BaseSpecialScrollBook
    {
        public override Type ScrollType { get { return typeof(ScrollOfTranscendence); } }
        public override int LabelNumber { get { return 1151679; } } // Scrolls of Transcendence Book
        public override int BadDropMessage { get { return 1151677; } } // This book only holds Scrolls of Transcendence.
        public override int DropMessage { get { return 1151674; } }    // You add the scroll to your Scrolls of Transcendence book.
        public override int RemoveMessage { get { return 1151658; } }  // You remove a Scroll of Transcendence and put it in your pack. 
        public override int GumpTitle { get { return 1151675; } }  // Scrolls of Transcendence

        [Constructable]
        public ScrollOfTranscendenceBook()
            : base(0x577E)
        {
            Hue = 0x490;
        }

        public ScrollOfTranscendenceBook(Serial serial)
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

            _ValueInfo[1151659] = 0.1;
            _ValueInfo[1151660] = 0.2;
            _ValueInfo[1151661] = 0.3;
            _ValueInfo[1151662] = 0.4;
            _ValueInfo[1151663] = 0.5;
            _ValueInfo[1151664] = 0.6;
            _ValueInfo[1151665] = 0.7;
            _ValueInfo[1151666] = 0.8;
            _ValueInfo[1151667] = 0.9;
            _ValueInfo[1151668] = 1.0;
            _ValueInfo[1151669] = 3.0;
            _ValueInfo[1151670] = 5.0;
        }
    }
}
