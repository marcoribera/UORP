using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Gumps.CharacterCreationRP
{
    public class RaceSelectionGump : Gump
    {
        Mobile caller;

        public static void Initialize()
        {
            CommandSystem.Register("CriarChar", AccessLevel.Administrator, new CommandEventHandler(RaceGump_OnCommand));
        }

        [Usage("CriarChar")]
        [Description("Gump de seleção de raça na criação de personagem.")]
        public static void RaceGump_OnCommand(CommandEventArgs e)
        {
            Mobile caller = e.Mobile;

            if (caller.HasGump(typeof(RaceSelectionGump)))
                caller.CloseGump(typeof(RaceSelectionGump));
            caller.SendGump(new RaceSelectionGump(caller));
        }

        public RaceSelectionGump(Mobile from) : this()
        {
            caller = from;
        }

        public RaceSelectionGump() : base(0, 0)
        {
            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;

            AddPage(0);
            AddImage(0, -3, 40317);
            AddHtml(310, 371, 269, 196, @"", false, true);
            AddHtml(602, 372, 268, 193, @"", false, true);
            AddHtml(73, 298, 178, 310, @"", false, true);

            //Botão para passagem de página
            AddButton(840, 574, 2360, 2361, 0, GumpButtonType.Reply, 0);

            //Radiobox para seleção de raça
            AddGroup(0);
            AddRadio(324, 343, 2360, 2361, true, 11);
            AddLabel(345, 340, 1153, @"Aludia"); //11
            AddRadio(624, 342, 2360, 2361, false, 22);
            AddLabel(647, 340, 1153, @"Daru");  //22;
        }



        public override void OnResponse(NetState sender, RelayInfo info)
        {
            caller = sender.Mobile;

            switch (info.Switches[0])
            {
                case 11:
                    {
                        //Console.WriteLine("Aludia");
                        break;
                    }
                case 22:
                    {
                        //Console.WriteLine("Daru");
                        break;
                    }
                default:
                    {
                        //Console.WriteLine("Deu Ruim no Gump da Raça");
                        break;
                    }
            }
            caller.SendGump(new ClassSelectionGump(info.Switches[0],null,null));
            return;
        }
    }
}
