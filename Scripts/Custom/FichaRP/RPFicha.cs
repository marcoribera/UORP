using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Gumps;
using Server.Custom.FichaRP;

namespace Server.Commands
{
    internal class RPFicha
    {
        public static void Initialize()
        {
            CommandSystem.Register("RPFicha", AccessLevel.Player, EditarFichaRP);
            CommandSystem.Register("RPAvaliar", AccessLevel.Counselor, AvaliarFichaRP);
        }


        [Usage("RPFicha")]
        [Description("Abre sua Ficha RP para visualização ou edição.")]
        private static void EditarFichaRP(CommandEventArgs e)
        {

            PlayerMobile player = e.Mobile as PlayerMobile; //Personagem que chamou o comando

            if (e.ArgString == "")
            {
                FichaRPGump gump = new FichaRPGump(player);
                player.SendGump(gump);
                return;
            }
            else //Colocar algo aqui caso passe a existir alguma opção desse comando com os parâmetros
            {
                FichaRPGump gump = new FichaRPGump(player);
                player.SendGump(gump);
                return;
            }

        }

        [Usage("RPAvaliar")]
        [Description("Abre sua Ficha RP do alvo para visualização ou feedback da staff.")]
        private static void AvaliarFichaRP(CommandEventArgs e)
        {

            PlayerMobile player = e.Mobile as PlayerMobile; //Personagem que chamou o comando

            if (e.ArgString == "")
            {
                e.Mobile.Target = new TargetFichaRP();
                return;
            }
            else //Colocar algo aqui caso passe a existir alguma opção desse comando com os parâmetros
            {
                e.Mobile.Target = new TargetFichaRP();
                return;
            }

        }

        public class FichaRPGump : Gump
        {
            public PlayerMobile m;

            public FichaRPGump(PlayerMobile m) : base(0, 0)
            {
                this.m = m;
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddImage(678, 158, 1596);
                this.AddImage(678, 300, 1597);
                this.AddImage(678, 438, 1599);
                this.AddButton(853, 301, 5575, 5576, (int)Buttons.BotaoAparenciaGump, GumpButtonType.Reply, 0);
                this.AddButton(724, 406, 5587, 5588, (int)Buttons.BotaoPersonalidadeGump, GumpButtonType.Reply, 0);
                this.AddButton(855, 405, 5569, 5570, (int)Buttons.BotaoObjetivosGump, GumpButtonType.Reply, 0);
                this.AddButton(980, 403, 5581, 5582, (int)Buttons.BotaoFeedbackStaffGump, GumpButtonType.Reply, 0);
                this.AddImage(820, 365, 1589);
                this.AddImage(943, 469, 1589);
                this.AddImage(818, 470, 1589);
                this.AddImage(694, 470, 1589);
                this.AddBackground(843, 236, 104, 24, 9300);
                this.AddHtml(856, 238, 125, 26, @"Ficha RP", (bool)false, (bool)false);
                this.AddHtml(861, 368, 151, 26, @"Aparencia", (bool)false, (bool)false);
                this.AddHtml(713, 473, 151, 26, @"Personalidade", (bool)false, (bool)false);
                this.AddHtml(837, 475, 151, 26, @"Objetivos", (bool)false, (bool)false);
                this.AddHtml(968, 473, 151, 26, @"Feedback", (bool)false, (bool)false);
            }

            public enum Buttons
            {
                Nada,
                BotaoAparenciaGump,
                BotaoPersonalidadeGump,
                BotaoObjetivosGump,
                BotaoFeedbackStaffGump
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                var from = sender.Mobile;
                if (info.ButtonID == (int)Buttons.BotaoAparenciaGump)
                {
                    from.SendGump(new AparenciaGump(this.m));
                }
                else if (info.ButtonID == (int)Buttons.BotaoPersonalidadeGump)
                {
                    from.SendGump(new PersonalidadeGump(this.m));
                }
                else if (info.ButtonID == (int)Buttons.BotaoObjetivosGump)
                {
                    from.SendGump(new ObjetivosGump(this.m));
                }
                else if (info.ButtonID == (int)Buttons.BotaoFeedbackStaffGump)
                {
                    from.SendGump(new FeedbackStaffGump(sender.Mobile as PlayerMobile, this.m));
                }
            }
        }

        public class AparenciaGump : Server.Gumps.Gump
        {
            public PlayerMobile m;
            public AparenciaGump(PlayerMobile from) : base(0, 0)
            {
                this.m = from;
                var ficha = this.m.FichaRP;
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddImage(353, 179, 1596);
                this.AddImage(353, 321, 1597);
                this.AddImage(353, 603, 1599);
                this.AddImage(513, 193, 1589);
                this.AddHtml(551, 197, 151, 26, @"Aparência", (bool)false, (bool)false);
                this.AddBackground(390, 227, 344, 26, 9300);
                this.AddBackground(390, 259, 344, 26, 9300);
                this.AddImage(353, 463, 1598);
                this.AddBackground(391, 294, 344, 406, 9300);
                this.AddTextEntry(397, 300, 330, 395, 0, (int)Buttons2.AparenciaRosto, ficha.AparenciaRosto == "" ? @"Descreva a aparencia do rosto do seu personagem." : ficha.AparenciaRosto);
                this.AddTextEntry(394, 230, 331, 20, 0, (int)Buttons2.AparenciaCorpo, ficha.AparenciaCorpo == "" ? @"Descreva a aparencia do corpo do seu personagem." : ficha.AparenciaCorpo);
                this.AddTextEntry(397, 263, 331, 20, 0, (int)Buttons2.AparenciaMarcas, ficha.AparenciaMarcas == "" ? @"Descreva marcas ou outras peculiaridades da aparência do seu personagem." : ficha.AparenciaMarcas);
            }

            public enum Buttons2
            {
                AparenciaRosto,
                AparenciaCorpo,
                AparenciaMarcas
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                m.FichaRP.AparenciaRosto = info.GetTextEntry((int)Buttons2.AparenciaRosto).Text;
                m.FichaRP.AparenciaCorpo = info.GetTextEntry((int)Buttons2.AparenciaCorpo).Text;
                m.FichaRP.AparenciaMarcas = info.GetTextEntry((int)Buttons2.AparenciaMarcas).Text;
                sender.Mobile.SendGump(new FichaRPGump(this.m));
                this.m.SendMessage(78, "Aparencia atualizada!");
            }
        }

        public class PersonalidadeGump : Gump
        {
            public PlayerMobile m;
            public PersonalidadeGump(PlayerMobile from) : base(0, 0)
            {
                this.m = from;
                var ficha = this.m.FichaRP;
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddImage(353, 179, 1596);
                this.AddImage(353, 321, 1597);
                this.AddImage(353, 603, 1599);
                this.AddImage(513, 193, 1589);
                this.AddHtml(551, 197, 151, 26, @"Personalidade", (bool)false, (bool)false);
                this.AddBackground(390, 227, 344, 26, 9300);
                this.AddBackground(390, 259, 344, 26, 9300);
                this.AddImage(353, 463, 1598);
                this.AddBackground(391, 294, 344, 406, 9300);
                this.AddTextEntry(397, 300, 330, 395, 0, (int)Buttons3.PersonalidadePositivo, ficha.PersonalidadePositivo == "" ? @"Descreva traços positivos da personalidade da personagem." : ficha.PersonalidadePositivo);
                this.AddTextEntry(394, 230, 331, 20, 0, (int)Buttons3.PersonalidadeNegativo, ficha.PersonalidadeNegativo == "" ? @"Descreva traços negativos da personalidade da personagem." : ficha.PersonalidadeNegativo);
                this.AddTextEntry(397, 263, 331, 20, 0, (int)Buttons3.PersonalidadeOutros, ficha.PersonalidadeOutros == "" ? @"Descreva outros traços da personalidade da personagem que julgar importantes." : ficha.PersonalidadeOutros);
            }

            public enum Buttons3
            {
                PersonalidadePositivo,
                PersonalidadeNegativo,
                PersonalidadeOutros
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                m.FichaRP.PersonalidadePositivo = info.GetTextEntry((int)Buttons3.PersonalidadePositivo).Text;
                m.FichaRP.PersonalidadeNegativo = info.GetTextEntry((int)Buttons3.PersonalidadeNegativo).Text;
                m.FichaRP.PersonalidadeOutros = info.GetTextEntry((int)Buttons3.PersonalidadeOutros).Text;
                sender.Mobile.SendGump(new FichaRPGump(this.m));
                this.m.SendMessage(78, "Personalidade atualizada!");
            }
        }
        public class ObjetivosGump : Gump
        {
            public PlayerMobile m;
            public ObjetivosGump(PlayerMobile m) : base(0, 0)
            {
                this.m = m;
                var ficha = this.m.FichaRP;
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddImage(353, 179, 1596);
                this.AddImage(353, 321, 1597);
                this.AddImage(353, 603, 1599);
                this.AddImage(513, 193, 1589);
                this.AddHtml(540, 197, 151, 26, @"Objetivos", (bool)false, (bool)false);
                this.AddImage(353, 463, 1598);
                this.AddBackground(393, 230, 344, 433, 9300);
                this.AddTextEntry(400, 237, 330, 420, 0, 0, @"Descreva o próximo objetivo do personagem");
                this.AddHtml(400, 437, 830, 120, (ficha.ObjetivosHistorico == "" && ficha.ObjetivosAtual == "") ? @"Sem registros de Objetivos" : ficha.ObjetivosAtual + "<BR>" + ficha.ObjetivosHistorico, false, false);
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                var ficha = this.m.FichaRP;
                string texto = info.GetTextEntry(0).Text;
                if (texto != "" && texto != "Descreva o próximo objetivo do personagem")
                {
                    if (ficha.ObjetivosAtual != "")
                    {
                        ficha.ObjetivosHistorico = ficha.ObjetivosAtual + "<BR>" + ficha.ObjetivosHistorico;
                    }
                    ficha.ObjetivosAtual = info.GetTextEntry(0).Text;
                    this.m.SendMessage(78, "Novo objetivo registrado!");
                }
                sender.Mobile.SendGump(new FichaRPGump(this.m));
            }
        }

        public class FeedbackStaffGump : Gump
        {
            public PlayerMobile m;
            public FeedbackStaffGump(PlayerMobile viewer, PlayerMobile m) : base(0, 0)
            {
                this.m = m;
                var ficha = this.m.FichaRP;
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddImage(353, 179, 1596);
                this.AddImage(353, 321, 1597);
                this.AddImage(353, 603, 1599);
                this.AddImage(513, 193, 1589);
                this.AddHtml(540, 197, 151, 26, @"Feedback Staff", (bool)false, (bool)false);
                this.AddImage(353, 463, 1598);
                this.AddBackground(394, 230, 344, 464, 9300);
                if (viewer.AccessLevel >= AccessLevel.Counselor)
                {
                    this.AddTextEntry(400, 237, 330, 420, 0, 0, @"Escreva um feedback sobre o personagem");
                    this.AddHtml(400, 437, 830, 120, (ficha.FeedbackStaff == "" && ficha.FeedbackStaffHistorico == "") ? @"Nenhum feedback da staff ainda" : (ficha.FeedbackStaff + "<BR>" + ficha.FeedbackStaffHistorico), false, true);
                }
                else
                {
                    this.AddHtml(400, 237, 830, 120, (ficha.FeedbackStaff == "" && ficha.FeedbackStaffHistorico == "") ? @"Nenhum feedback da staff ainda" : (ficha.FeedbackStaff + "<BR>" + ficha.FeedbackStaffHistorico), false, true);
                }
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                var ficha = this.m.FichaRP;
                if (sender.Mobile.AccessLevel >= AccessLevel.Counselor)
                {
                    string texto = info.GetTextEntry(0).Text;
                    if (texto != "" && texto != "Escreva um feedback sobre o personagem")
                    {
                        if (ficha.FeedbackStaff != "")
                        {
                            ficha.FeedbackStaffHistorico = ficha.FeedbackStaff + "<BR>" + ficha.FeedbackStaffHistorico;
                        }
                        ficha.FeedbackStaff = texto;
                        this.m.SendMessage(78, "Você recebeu um novo feedback da Staff!");
                    }
                }
                sender.Mobile.SendGump(new FichaRPGump(this.m));
            }
        }

        public class TargetFichaRP : Target
        {
            public TargetFichaRP() : base(-1, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is PlayerMobile)
                {
                    PlayerMobile alvo = targeted as PlayerMobile;
                    
                }
            }
        }
    }
}
