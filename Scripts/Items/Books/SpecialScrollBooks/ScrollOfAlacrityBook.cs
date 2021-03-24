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

            _SkillInfo[SkillCat.Combate] = new List<SkillName>() { SkillName.Anatomia, SkillName.Atirar, SkillName.Bloqueio, SkillName.Briga, SkillName.Bushido, SkillName.Contusivo, SkillName.Cortante, SkillName.DuasMaos, SkillName.Envenenamento, SkillName.Ninjitsu, SkillName.Perfurante, SkillName.PreparoFisico, SkillName.UmaMao };
            _SkillInfo[SkillCat.Diversos] = new List<SkillName>() { SkillName.Carisma, SkillName.Furtividade, SkillName.Mecanica, SkillName.Pacificar, SkillName.Percepcao, SkillName.Prestidigitacao, SkillName.Provocacao, SkillName.Tocar };
            _SkillInfo[SkillCat.Magia] = new List<SkillName>() { SkillName.Arcanismo, SkillName.Caos, SkillName.Feiticaria, SkillName.ImbuirMagica, SkillName.Misticismo, SkillName.Necromancia, SkillName.Ordem, SkillName.PoderMagico, SkillName.ResistenciaMagica };
            _SkillInfo[SkillCat.Oficios] = new List<SkillName>() { SkillName.Agricultura, SkillName.Alquimia, SkillName.Carpintaria, SkillName.ConhecimentoArmaduras, SkillName.ConhecimentoArmas, SkillName.Costura, SkillName.Culinaria, SkillName.Erudicao, SkillName.Extracao, SkillName.Medicina, SkillName.Veterinaria };

            _ValueInfo = new Dictionary<int, double>();

            _ValueInfo[1154324] = 0.0;
        }
    }
}
