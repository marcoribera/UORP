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

            _SkillInfo[SkillCat.Combate] = new List<SkillName>() { SkillName.Anatomia, SkillName.Atirar, SkillName.Bloqueio, SkillName.Briga, SkillName.Bushido, SkillName.Contusivo, SkillName.Cortante, SkillName.DuasMaos, SkillName.Envenenamento, SkillName.Ninjitsu, SkillName.Perfurante, SkillName.PreparoFisico, SkillName.UmaMao };
            _SkillInfo[SkillCat.Diversos] = new List<SkillName>() { SkillName.Carisma, SkillName.Furtividade, SkillName.Mecanica, SkillName.Pacificar, SkillName.Percepcao, SkillName.Prestidigitacao, SkillName.Provocacao, SkillName.Tocar };
            _SkillInfo[SkillCat.Magia] = new List<SkillName>() { SkillName.Arcanismo, SkillName.Caos, SkillName.Feiticaria, SkillName.ImbuirMagica, SkillName.Misticismo, SkillName.Necromancia, SkillName.Ordem, SkillName.PoderMagico, SkillName.ResistenciaMagica };

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
