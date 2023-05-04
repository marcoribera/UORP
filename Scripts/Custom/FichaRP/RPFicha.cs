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
                this.AddImage(678, 158, 1596); //Topo da imagem de fundo
                this.AddImage(678, 300, 1597); //Meio da imagem de fundo
                this.AddImage(678, 438, 1599); //Baixo da imagem de fundo
                this.AddButton(720, 301, 5555, 5556, (int)Buttons.BotaoBackgroundGump, GumpButtonType.Reply, 0); //Bigorna - Representa como o char foi forjado
                this.AddButton(853, 301, 5557, 5558, (int)Buttons.BotaoMemoriasMarcantesGump, GumpButtonType.Reply, 0); // Fenix - Representa algo dificil de esquecer
                this.AddButton(980, 301, 5575, 5576, (int)Buttons.BotaoAparenciaGump, GumpButtonType.Reply, 0); // Perfil - Representa a aparência
                this.AddButton(720, 406, 5581, 5582, (int)Buttons.BotaoPersonalidadeGump, GumpButtonType.Reply, 0); // Engrenagem - Representa a maquinação da cabeça
                this.AddButton(853, 405, 5587, 5588, (int)Buttons.BotaoObjetivosGump, GumpButtonType.Reply, 0); //Elmo - Representa a resolução para alcançar objetivos
                this.AddButton(980, 403, 5583, 5584, (int)Buttons.BotaoFeedbackStaffGump, GumpButtonType.Reply, 0); //Balança - Representa uma avaliação justa
                this.AddImage(691, 365, 1589);
                this.AddImage(818, 365, 1589);
                this.AddImage(945, 365, 1589);
                this.AddImage(691, 470, 1589);
                this.AddImage(818, 470, 1589);
                this.AddImage(945, 469, 1589);
                //this.AddBackground(843, 236, 104, 24, 9300);
                this.AddHtml(843, 238, 104, 26, @"<CENTER><BIG><B>Ficha RP</B></BIG></CENTER>", (bool)false, (bool)false);
                this.AddHtml(695, 368, 125, 26, @"<CENTER>Background</CENTER>", (bool)false, (bool)false);
                this.AddHtml(820, 368, 125, 26, @"<CENTER>Memorias Marcantes</CENTER>", (bool)false, (bool)false);
                this.AddHtml(945, 368, 125, 26, @"<CENTER>Aparência</CENTER>", (bool)false, (bool)false);
                this.AddHtml(695, 473, 125, 26, @"<CENTER>Personalidade</CENTER>", (bool)false, (bool)false);
                this.AddHtml(820, 473, 125, 26, @"<CENTER>Objetivos</CENTER>", (bool)false, (bool)false);
                this.AddHtml(945, 473, 125, 26, @"<CENTER>Feedback</CENTER>", (bool)false, (bool)false);
            }

            public enum Buttons
            {
                Nada,
                BotaoBackgroundGump,
                BotaoMemoriasMarcantesGump,
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
                else if (info.ButtonID == (int)Buttons.BotaoBackgroundGump)
                {
                    from.SendGump(new BackgroundGump(this.m));
                }
                else if (info.ButtonID == (int)Buttons.BotaoMemoriasMarcantesGump)
                {
                    from.SendGump(new MemoriasMarcantesGump(this.m));
                }
            }
        }

        public class AparenciaGump : Gump
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
                this.AddImage(353, 463, 1598);
                this.AddImage(353, 603, 1599);

                this.AddImage(513, 193, 1589);
                this.AddHtml(520, 195, 112, 26, @"<CENTER>Aparência</CENTER>", (bool)false, (bool)false);

                this.AddBackground(390, 247, 344, 130, 9300);
                this.AddBackground(390, 397, 344, 130, 9300);
                this.AddBackground(390, 547, 344, 130, 9300);

                this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>Aparência do Rosto</B></BIG></CENTER>", (bool)false, (bool)false);
                this.AddHtml(390, 377, 346, 26, @"<CENTER><BIG><B>Aparência do Corpo</B></BIG></CENTER>", (bool)false, (bool)false);
                this.AddHtml(390, 527, 346, 26, @"<CENTER><BIG><B>Outros traços Marcantes</B></BIG></CENTER>", (bool)false, (bool)false);

                this.AddTextEntry(397, 250, 330, 126, 0, (int)Buttons2.AparenciaRosto, ficha.AparenciaRosto == "" ? @"Descreva a aparencia do rosto do seu personagem." : ficha.AparenciaRosto);
                this.AddTextEntry(397, 400, 330, 126, 0, (int)Buttons2.AparenciaCorpo, ficha.AparenciaCorpo == "" ? @"Descreva a aparencia do corpo do seu personagem." : ficha.AparenciaCorpo);
                this.AddTextEntry(397, 550, 330, 126, 0, (int)Buttons2.AparenciaMarcas, ficha.AparenciaMarcas == "" ? @"Descreva marcas ou outras peculiaridades da aparência do seu personagem." : ficha.AparenciaMarcas);
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
                this.AddImage(353, 463, 1598);
                this.AddImage(353, 603, 1599);

                this.AddImage(513, 193, 1589);
                this.AddHtml(520, 195, 112, 26, @"<CENTER>Personalidade</CENTER>", (bool)false, (bool)false);

                this.AddBackground(390, 247, 344, 130, 9300);
                this.AddBackground(390, 397, 344, 130, 9300);
                this.AddBackground(390, 547, 344, 130, 9300);

                this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>Traços de Personalidade Positivos</B></BIG></CENTER>", (bool)false, (bool)false);
                this.AddHtml(390, 377, 346, 26, @"<CENTER><BIG><B>Traços de Personalidade Negativos</B></BIG></CENTER>", (bool)false, (bool)false);
                this.AddHtml(390, 527, 346, 26, @"<CENTER><BIG><B>Outros Traços de Personalidade</B></BIG></CENTER>", (bool)false, (bool)false);

                this.AddTextEntry(397, 250, 330, 126, 0, (int)Buttons3.PersonalidadePositivo, ficha.PersonalidadePositivo == "" ? @"Descreva traços positivos da personalidade da personagem." : ficha.PersonalidadePositivo);
                this.AddTextEntry(397, 400, 330, 126, 0, (int)Buttons3.PersonalidadeNegativo, ficha.PersonalidadeNegativo == "" ? @"Descreva traços negativos da personalidade da personagem." : ficha.PersonalidadeNegativo);
                this.AddTextEntry(397, 550, 330, 126, 0, (int)Buttons3.PersonalidadeOutros, ficha.PersonalidadeOutros == "" ? @"Descreva outros traços da personalidade da personagem que julgar importantes." : ficha.PersonalidadeOutros);
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
        public class BackgroundGump : Gump
        {
            public PlayerMobile m;
            public BackgroundGump(PlayerMobile m) : base(0, 0)
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
                this.AddImage(353, 463, 1598);
                this.AddImage(353, 603, 1599);

                this.AddImage(513, 193, 1589);
                this.AddHtml(520, 195, 112, 26, @"<CENTER>Background</CENTER>", (bool)false, (bool)false);

                this.AddBackground(390, 247, 344, 130, 9300);
                this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>Adicionar Parágrafo</B></BIG></CENTER>", (bool)false, (bool)false);
                this.AddTextEntry(397, 250, 330, 126, 0, 0, @"Escreva um novo parágrafo de seu Background.");

                this.AddHtml(390, 377, 346, 26, @"<CENTER><BIG><B>Background Completo</B></BIG></CENTER>", (bool)false, (bool)false);
                this.AddBackground(390, 397, 344, 290, 9300);
                this.AddHtml(397, 398, 335, 288, (ficha.BackgroundHistorico == "" && ficha.Background == "") ? @"<BASEFONT COLOR=#000000>Sem registros de Objetivos</BASEFONT>" : "<BASEFONT COLOR=#000000>" + ficha.BackgroundHistorico + "<BR>" + ficha.Background + "</BASEFONT>", false, true);
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                var ficha = this.m.FichaRP;
                string texto = info.GetTextEntry(0).Text;
                if (texto != "" && texto != "Escreva um novo parágrafo de seu Background.")
                {
                    ficha.BackgroundHistorico = ficha.BackgroundHistorico + "<BR>" + ficha.Background;
                    ficha.Background = info.GetTextEntry(0).Text;
                    this.m.SendMessage(78, "Novo parágrafo do Background adicionado!");
                }
                sender.Mobile.SendGump(new FichaRPGump(this.m));
            }
        }

        public class MemoriasMarcantesGump : Gump
        {
            public PlayerMobile m;
            public MemoriasMarcantesGump(PlayerMobile m) : base(0, 0)
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
                this.AddHtml(540, 197, 151, 26, @"Memórias Marcantes", (bool)false, (bool)false);
                this.AddImage(353, 463, 1598);
                this.AddBackground(393, 230, 344, 433, 9300);
                this.AddTextEntry(400, 237, 330, 420, 0, 0, @"Escreva uma Memória Marcante que seu personagem vivenciou in-game");
                this.AddHtml(400, 437, 830, 120, (ficha.MemoriasMarcantesHistorico == "" && ficha.MemoriasMarcantes == "") ? @"Sem memórias registradas ainda." : ficha.MemoriasMarcantesHistorico + "<BR>" + ficha.MemoriasMarcantes, false, false);
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                var ficha = this.m.FichaRP;
                string texto = info.GetTextEntry(0).Text;
                if (texto != "" && texto != "Escreva uma Memória Marcante que seu personagem vivenciou in-game")
                {
                    if (ficha.MemoriasMarcantes != "")
                    {
                        ficha.MemoriasMarcantesHistorico = ficha.MemoriasMarcantesHistorico + "<BR>" + ficha.MemoriasMarcantes;
                    }
                    ficha.ObjetivosAtual = info.GetTextEntry(0).Text;
                    this.m.SendMessage(78, "Nova Memória Marcante registrada!");
                }
                sender.Mobile.SendGump(new FichaRPGump(this.m));
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
