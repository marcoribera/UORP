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
            CommandSystem.Register("FichaRP", AccessLevel.Player, EditarFichaRP);
            CommandSystem.Register("RPAvaliar", AccessLevel.Counselor, AvaliarFichaRP);
        }


        [Usage("RPFicha")]
        [Description("Abre sua Ficha RP para visualização ou edição.")]
        private static void EditarFichaRP(CommandEventArgs e)
        {

            PlayerMobile player = e.Mobile as PlayerMobile; //Personagem que chamou o comando

            if (e.ArgString == "")
            {
                FichaRPGump gump = new FichaRPGump(player, player);
                player.SendGump(gump);
                return;
            }
            else //Colocar algo aqui caso passe a existir alguma opção desse comando com os parâmetros
            {
                FichaRPGump gump = new FichaRPGump(player, player);
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
            public PlayerMobile m, viewer;

            public FichaRPGump(PlayerMobile viewer, PlayerMobile m) : base(0, 0)
            {
                this.viewer = viewer;
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
                    from.SendGump(new AparenciaGump(this.viewer, this.m));
                }
                else if (info.ButtonID == (int)Buttons.BotaoPersonalidadeGump)
                {
                    from.SendGump(new PersonalidadeGump(this.viewer, this.m));
                }
                else if (info.ButtonID == (int)Buttons.BotaoObjetivosGump)
                {
                    from.SendGump(new ObjetivosGump(this.viewer, this.m));
                }
                else if (info.ButtonID == (int)Buttons.BotaoFeedbackStaffGump)
                {
                    from.SendGump(new FeedbackStaffGump(this.viewer, this.m));
                }
                else if (info.ButtonID == (int)Buttons.BotaoBackgroundGump)
                {
                    from.SendGump(new BackgroundGump(this.viewer, this.m));
                }
                else if (info.ButtonID == (int)Buttons.BotaoMemoriasMarcantesGump)
                {
                    from.SendGump(new MemoriasMarcantesGump(this.viewer, this.m));
                }
            }
        }

        public class AparenciaGump : Gump
        {
            public PlayerMobile m, viewer;
            public AparenciaGump(PlayerMobile viewer, PlayerMobile m) : base(0, 0)
            {
                this.viewer = viewer;
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
                this.AddHtml(520, 195, 112, 26, @"<CENTER>Aparência</CENTER>", (bool)false, (bool)false);

                this.AddBackground(390, 247, 344, 130, 9300);
                this.AddBackground(390, 397, 344, 130, 9300);
                this.AddBackground(390, 547, 344, 130, 9300);

                this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>Aparência do Rosto</B></BIG></CENTER>", (bool)false, (bool)false);
                this.AddHtml(390, 377, 346, 26, @"<CENTER><BIG><B>Aparência do Corpo</B></BIG></CENTER>", (bool)false, (bool)false);
                this.AddHtml(390, 527, 346, 26, @"<CENTER><BIG><B>Outros traços Marcantes</B></BIG></CENTER>", (bool)false, (bool)false);

                if (m == viewer)
                {
                    this.AddTextEntry(397, 250, 330, 126, 0, (int)Buttons2.AparenciaRosto, ficha.AparenciaRosto == "" ? @"Descreva a aparencia do rosto do seu personagem." : ficha.AparenciaRosto);
                    this.AddTextEntry(397, 400, 330, 126, 0, (int)Buttons2.AparenciaCorpo, ficha.AparenciaCorpo == "" ? @"Descreva a aparencia do corpo do seu personagem." : ficha.AparenciaCorpo);
                    this.AddTextEntry(397, 550, 330, 126, 0, (int)Buttons2.AparenciaMarcas, ficha.AparenciaMarcas == "" ? @"Descreva marcas ou outras peculiaridades da aparência do seu personagem." : ficha.AparenciaMarcas);
                }
                else
                {
                    this.AddHtml(397, 250, 330, 126, ficha.AparenciaRosto == "" ? @"Em branco." : ficha.AparenciaRosto, (bool)false, (bool)false);
                    this.AddHtml(397, 400, 330, 126, ficha.AparenciaCorpo == "" ? @"Em branco." : ficha.AparenciaCorpo, (bool)false, (bool)false);
                    this.AddHtml(397, 550, 330, 126, ficha.AparenciaMarcas == "" ? @"Em branco." : ficha.AparenciaMarcas, (bool)false, (bool)false);
                }
            }

            public enum Buttons2
            {
                AparenciaRosto,
                AparenciaCorpo,
                AparenciaMarcas
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (viewer == m)
                {
                    m.FichaRP.AparenciaRosto = info.GetTextEntry((int)Buttons2.AparenciaRosto).Text;
                    m.FichaRP.AparenciaCorpo = info.GetTextEntry((int)Buttons2.AparenciaCorpo).Text;
                    m.FichaRP.AparenciaMarcas = info.GetTextEntry((int)Buttons2.AparenciaMarcas).Text;
                    this.m.SendMessage(78, "Aparencia atualizada!");
                }
                sender.Mobile.SendGump(new FichaRPGump(this.viewer, this.m));
            }
        }

        public class PersonalidadeGump : Gump
        {
            public PlayerMobile m, viewer;
            public PersonalidadeGump(PlayerMobile viewer, PlayerMobile m) : base(0, 0)
            {
                this.viewer = viewer;
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
                this.AddHtml(520, 195, 112, 26, @"<CENTER>Personalidade</CENTER>", (bool)false, (bool)false);

                this.AddBackground(390, 247, 344, 130, 9300);
                this.AddBackground(390, 397, 344, 130, 9300);
                this.AddBackground(390, 547, 344, 130, 9300);

                this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>Traços de Personalidade Positivos</B></BIG></CENTER>", (bool)false, (bool)false);
                this.AddHtml(390, 377, 346, 26, @"<CENTER><BIG><B>Traços de Personalidade Negativos</B></BIG></CENTER>", (bool)false, (bool)false);
                this.AddHtml(390, 527, 346, 26, @"<CENTER><BIG><B>Outros Traços de Personalidade</B></BIG></CENTER>", (bool)false, (bool)false);

                if (m == viewer)
                {
                    this.AddTextEntry(397, 250, 330, 126, 0, (int)Buttons3.PersonalidadePositivo, ficha.PersonalidadePositivo == "" ? @"Descreva traços positivos da personalidade da personagem." : ficha.PersonalidadePositivo);
                    this.AddTextEntry(397, 400, 330, 126, 0, (int)Buttons3.PersonalidadeNegativo, ficha.PersonalidadeNegativo == "" ? @"Descreva traços negativos da personalidade da personagem." : ficha.PersonalidadeNegativo);
                    this.AddTextEntry(397, 550, 330, 126, 0, (int)Buttons3.PersonalidadeOutros, ficha.PersonalidadeOutros == "" ? @"Descreva outros traços da personalidade da personagem que julgar importantes." : ficha.PersonalidadeOutros);
                }
                else
                {
                    this.AddHtml(397, 250, 330, 126, ficha.PersonalidadePositivo == "" ? @"Em branco." : ficha.PersonalidadePositivo, (bool)false, (bool)false);
                    this.AddHtml(397, 400, 330, 126, ficha.PersonalidadeNegativo == "" ? @"Em branco." : ficha.PersonalidadeNegativo, (bool)false, (bool)false);
                    this.AddHtml(397, 550, 330, 126, ficha.PersonalidadeOutros == "" ? @"Em branco." : ficha.PersonalidadeOutros, (bool)false, (bool)false);
                }
            }

            public enum Buttons3
            {
                PersonalidadePositivo,
                PersonalidadeNegativo,
                PersonalidadeOutros
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (viewer == m)
                {
                    m.FichaRP.PersonalidadePositivo = info.GetTextEntry((int)Buttons3.PersonalidadePositivo).Text;
                    m.FichaRP.PersonalidadeNegativo = info.GetTextEntry((int)Buttons3.PersonalidadeNegativo).Text;
                    m.FichaRP.PersonalidadeOutros = info.GetTextEntry((int)Buttons3.PersonalidadeOutros).Text;
                    this.m.SendMessage(78, "Personalidade atualizada!");
                }
                sender.Mobile.SendGump(new FichaRPGump(this.viewer, this.m));
            }
        }
        public class BackgroundGump : Gump
        {
            public PlayerMobile m, viewer;
            public BackgroundGump(PlayerMobile viewer, PlayerMobile m) : base(0, 0)
            {
                this.viewer = viewer;
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
                if (m == viewer)
                {
                    this.AddBackground(390, 247, 344, 130, 9300);
                    this.AddButton(733, 247, 5402, 5402, (int)Buttons4.Adicionar, GumpButtonType.Reply, 0); //Botão para adicionar
                    this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>Adicionar Parágrafo</B></BIG></CENTER>", (bool)false, (bool)false);
                    this.AddTextEntry(397, 250, 330, 126, 0, 0, @"");

                    this.AddHtml(390, 377, 346, 26, @"<CENTER><BIG><B>Background Completo</B></BIG></CENTER>", (bool)false, (bool)false);
                    this.AddBackground(390, 397, 344, 290, 9300);
                    this.AddButton(733, 397, 5401, 5401, (int)Buttons4.Remover, GumpButtonType.Reply, 0); //Botão para remover a ultima inserção
                    this.AddHtml(397, 398, 335, 288, (ficha.BackgroundHistorico == "" && ficha.Background == "") ? @"<BASEFONT COLOR=#000000>Sem registros de Objetivos</BASEFONT>" : "<BASEFONT COLOR=#000000>" + ficha.BackgroundHistorico + "<BR>" + ficha.Background + "</BASEFONT>", false, true);
                }
                else
                {
                    this.AddBackground(390, 247, 344, 434, 9300);
                    this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>Background Completo</B></BIG></CENTER>", (bool)false, (bool)false);
                    this.AddHtml(397, 248, 335, 432, (ficha.BackgroundHistorico == "" && ficha.Background == "") ? @"<BASEFONT COLOR=#000000>Sem registros de Objetivos</BASEFONT>" : "<BASEFONT COLOR=#000000>" + ficha.BackgroundHistorico + "<BR>" + ficha.Background + "</BASEFONT>", false, true);
                }
            }

            public enum Buttons4
            {
                Nada,
                Adicionar,
                Remover
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (info.ButtonID == (int)Buttons4.Adicionar)
                {
                    var ficha = this.m.FichaRP;
                    string texto = info.GetTextEntry(0).Text;
                    if (texto != "")
                    {
                        if (ficha.Background != "")
                        {
                            ficha.BackgroundHistorico = ficha.BackgroundHistorico + "<BR>" + ficha.Background;
                        }
                        ficha.Background = info.GetTextEntry(0).Text;
                        this.m.SendMessage(78, "Novo parágrafo do Background adicionado!");
                    }
                    else
                    {
                        this.m.SendMessage(33, "Erro ao inserir. Escreva algo no campo de texto e tente novamente.");
                    }
                    sender.Mobile.SendGump(new BackgroundGump(this.viewer, this.m));
                }
                else if (info.ButtonID == (int)Buttons4.Remover)
                {
                    var ficha = this.m.FichaRP;
                    if (ficha.Background != "")
                    {
                        ficha.Background = "";
                        this.m.SendMessage(78, "Removido o ultimo registro!");
                    }
                    else
                    {
                        this.m.SendMessage(33, "Apenas o último registro inserido pode ser removido!");
                    }
                    sender.Mobile.SendGump(new BackgroundGump(this.viewer, this.m));
                }
                else
                {
                    sender.Mobile.SendGump(new FichaRPGump(this.viewer, this.m));
                }
            }
        }

        public class MemoriasMarcantesGump : Gump
        {
            public PlayerMobile m, viewer;
            public MemoriasMarcantesGump(PlayerMobile viewer, PlayerMobile m) : base(0, 0)
            {
                this.viewer = viewer;
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
                this.AddHtml(520, 195, 112, 26, @"<CENTER>Memorias Marcantes</CENTER>", (bool)false, (bool)false);

                if (m == viewer)
                {
                    this.AddBackground(390, 247, 344, 130, 9300);
                    this.AddButton(733, 247, 5402, 5402, (int)Buttons5.Adicionar, GumpButtonType.Reply, 0); //Botão para adicionar
                    this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>Adicionar Parágrafo</B></BIG></CENTER>", (bool)false, (bool)false);
                    this.AddTextEntry(397, 250, 330, 126, 0, 0, @"");

                    this.AddHtml(390, 377, 346, 26, @"<CENTER><BIG><B>Todas as Memórias</B></BIG></CENTER>", (bool)false, (bool)false);
                    this.AddBackground(390, 397, 344, 290, 9300);
                    this.AddButton(733, 397, 5401, 5401, (int)Buttons5.Remover, GumpButtonType.Reply, 0); //Botão para remover a ultima inserção
                    this.AddHtml(397, 398, 335, 288, (ficha.MemoriasMarcantesHistorico == "" && ficha.MemoriasMarcantes == "") ? @"<BASEFONT COLOR=#000000>Sem registros de Memórias Marcantes.</BASEFONT>" : @"<BASEFONT COLOR=#000000>" + ficha.MemoriasMarcantes + "<BR>" + ficha.MemoriasMarcantesHistorico + "</BASEFONT>", false, true);
                }
                else
                {
                    this.AddBackground(390, 247, 344, 434, 9300);
                    this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>Todas as Memórias</B></BIG></CENTER>", (bool)false, (bool)false);
                    this.AddHtml(397, 248, 335, 432, (ficha.MemoriasMarcantesHistorico == "" && ficha.MemoriasMarcantes == "") ? @"<BASEFONT COLOR=#000000>Sem registros de Memórias Marcantes.</BASEFONT>" : @"<BASEFONT COLOR=#000000>" + ficha.MemoriasMarcantes + "<BR>" + ficha.MemoriasMarcantesHistorico + "</BASEFONT>", false, true);
                }
            }

            public enum Buttons5
            {
                Nada,
                Adicionar,
                Remover
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (info.ButtonID == (int)Buttons5.Adicionar)
                {
                    var ficha = this.m.FichaRP;
                    string texto = info.GetTextEntry(0).Text;
                    if (texto != "")
                    {
                        if (ficha.MemoriasMarcantes != "")
                        {
                            ficha.MemoriasMarcantesHistorico = ficha.MemoriasMarcantes + "<BR>" + ficha.MemoriasMarcantesHistorico;
                        }

                        ficha.MemoriasMarcantes = @"<B>[" + DateTimeOffset.Now.ToString("g") + "]</B> " + info.GetTextEntry(0).Text;
                        this.m.SendMessage(78, "Nova Memória Marcante registrada!");
                    }
                    else
                    {
                        this.m.SendMessage(33, "Erro ao inserir. Escreva algo no campo de texto e tente novamente.");
                    }
                    sender.Mobile.SendGump(new MemoriasMarcantesGump(this.viewer, this.m));
                }
                else if (info.ButtonID == (int)Buttons5.Remover)
                {
                    var ficha = this.m.FichaRP;
                    if (ficha.MemoriasMarcantes != "")
                    {
                        ficha.MemoriasMarcantes = "";
                        this.m.SendMessage(78, "Removido o ultimo registro!");
                    }
                    else
                    {
                        this.m.SendMessage(33, "Apenas o último registro inserido pode ser removido!");
                    }
                    sender.Mobile.SendGump(new MemoriasMarcantesGump(this.viewer, this.m));
                }
                else
                {
                    sender.Mobile.SendGump(new FichaRPGump(this.viewer, this.m));
                }
            }
        }

        public class ObjetivosGump : Gump
        {
            public PlayerMobile m, viewer;
            public ObjetivosGump(PlayerMobile viewer, PlayerMobile m) : base(0, 0)
            {
                this.viewer = viewer;
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
                this.AddHtml(520, 195, 112, 26, @"<CENTER>Objetivos</CENTER>", (bool)false, (bool)false);
                if (m == viewer)
                {
                    this.AddBackground(390, 247, 344, 130, 9300);
                    this.AddButton(733, 247, 5402, 5402, (int)Buttons6.Adicionar, GumpButtonType.Reply, 0); //Botão para adicionar
                    this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>Adicionar Objetivo</B></BIG></CENTER>", (bool)false, (bool)false);
                    this.AddTextEntry(397, 250, 330, 126, 0, 0, @"");

                    this.AddHtml(390, 377, 346, 26, @"<CENTER><BIG><B>Histórico de Objetivos</B></BIG></CENTER>", (bool)false, (bool)false);
                    this.AddBackground(390, 397, 344, 290, 9300);
                    this.AddButton(733, 397, 5401, 5401, (int)Buttons6.Remover, GumpButtonType.Reply, 0); //Botão para remover a ultima inserção
                    this.AddHtml(397, 398, 335, 288, (ficha.ObjetivosHistorico == "" && ficha.ObjetivosAtual == "") ? @"<BASEFONT COLOR=#000000>Sem Objetivos registrados.</BASEFONT>" : @"<BASEFONT COLOR=#000000>" + ficha.ObjetivosAtual + "<BR>" + ficha.ObjetivosHistorico + "</BASEFONT>", false, true);
                }
                else
                {
                    this.AddBackground(390, 247, 344, 434, 9300);
                    this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>istórico de Objetivos</B></BIG></CENTER>", (bool)false, (bool)false);
                    this.AddHtml(397, 248, 335, 432, (ficha.ObjetivosHistorico == "" && ficha.ObjetivosAtual == "") ? @"<BASEFONT COLOR=#000000>Sem Objetivos registrados.</BASEFONT>" : @"<BASEFONT COLOR=#000000>" + ficha.ObjetivosAtual + "<BR>" + ficha.ObjetivosHistorico + "</BASEFONT>", false, true);
                }
            }

            public enum Buttons6
            {
                Nada,
                Adicionar,
                Remover
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (info.ButtonID == (int)Buttons6.Adicionar)
                {
                    var ficha = this.m.FichaRP;
                    string texto = info.GetTextEntry(0).Text;
                    if (texto != "")
                    {
                        if (ficha.ObjetivosAtual != "")
                        {
                            ficha.ObjetivosHistorico = ficha.ObjetivosAtual + "<BR>" + ficha.ObjetivosHistorico;
                        }

                        ficha.ObjetivosAtual = @"<B>[" + DateTimeOffset.Now.ToString("g") + "]</B> " + info.GetTextEntry(0).Text;
                        this.m.SendMessage(78, "Novo Objetivo registrado!");
                    }
                    else
                    {
                        this.m.SendMessage(33, "Erro ao inserir. Escreva algo no campo de texto e tente novamente.");
                    }
                    sender.Mobile.SendGump(new ObjetivosGump(this.viewer, this.m));
                }
                else if (info.ButtonID == (int)Buttons6.Remover)
                {
                    var ficha = this.m.FichaRP;
                    if (ficha.ObjetivosAtual != "")
                    {
                        ficha.ObjetivosAtual = "";
                        this.m.SendMessage(78, "Removido o ultimo registro!");
                    }
                    else
                    {
                        this.m.SendMessage(33, "Apenas o último registro inserido pode ser removido!");
                    }
                    sender.Mobile.SendGump(new ObjetivosGump(this.viewer, this.m));
                }
                else
                {
                    sender.Mobile.SendGump(new FichaRPGump(this.viewer, this.m));
                }
            }
        }

        public class FeedbackStaffGump : Gump
        {
            public PlayerMobile m, viewer;
            public FeedbackStaffGump(PlayerMobile viewer, PlayerMobile m) : base(0, 0)
            {
                this.viewer = viewer;
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
                this.AddHtml(520, 195, 112, 26, @"<CENTER>Feedback</CENTER>", (bool)false, (bool)false);

                if (viewer!= m)
                {
                    this.AddBackground(390, 247, 344, 130, 9300);
                    this.AddButton(733, 247, 5402, 5402, (int)Buttons7.Adicionar, GumpButtonType.Reply, 0); //Botão para adicionar
                    this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>Adicionar Feedback</B></BIG></CENTER>", (bool)false, (bool)false);
                    this.AddTextEntry(397, 250, 330, 126, 0, 0, @"");

                    this.AddHtml(390, 377, 346, 26, @"<CENTER><BIG><B>Histórico de Feedback</B></BIG></CENTER>", (bool)false, (bool)false);
                    this.AddBackground(390, 397, 344, 290, 9300);
                    this.AddButton(733, 397, 5401, 5401, (int)Buttons7.Remover, GumpButtonType.Reply, 0); //Botão para remover a ultima inserção
                    this.AddHtml(397, 398, 335, 288, (ficha.FeedbackStaffHistorico == "" && ficha.FeedbackStaff == "") ? @"<BASEFONT COLOR=#000000>Sem registros de Feedback da Staff.</BASEFONT>" : @"<BASEFONT COLOR=#000000>" + ficha.FeedbackStaff + "<BR>" + ficha.FeedbackStaffHistorico + "</BASEFONT>", false, true);
                }
                else
                {
                    this.AddBackground(390, 247, 344, 434, 9300);
                    this.AddHtml(390, 227, 346, 26, @"<CENTER><BIG><B>Histórico de Feedback</B></BIG></CENTER>", (bool)false, (bool)false); 
                    this.AddHtml(397, 248, 335, 432, (ficha.FeedbackStaffHistorico == "" && ficha.FeedbackStaff == "") ? @"<BASEFONT COLOR=#000000>Sem registros de Feedback da Staff.</BASEFONT>" : @"<BASEFONT COLOR=#000000>" + ficha.FeedbackStaff + "<BR>" + ficha.FeedbackStaffHistorico + "</BASEFONT>", false, true);
                }
            }

            public enum Buttons7
            {
                Nada,
                Adicionar,
                Remover
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (info.ButtonID == (int)Buttons7.Adicionar)
                {
                    var ficha = this.m.FichaRP;
                    string texto = info.GetTextEntry(0).Text;
                    if (texto != "")
                    {
                        if (ficha.FeedbackStaff != "")
                        {
                            ficha.FeedbackStaffHistorico = ficha.FeedbackStaff + "<BR>" + ficha.FeedbackStaffHistorico;
                        }

                        ficha.FeedbackStaff = @"<B>[" + DateTimeOffset.Now.ToString("g") + "] [" + viewer.Account.Username + "]</B><BR>" + info.GetTextEntry(0).Text;
                        this.m.SendMessage(78, "Novo Feedback da Staff registrado!");
                        this.viewer.SendMessage(78, "Seu feedback foi registrado!");
                    }
                    else
                    {
                        this.m.SendMessage(33, "Erro ao inserir. Escreva algo no campo de texto e tente novamente.");
                    }
                    sender.Mobile.SendGump(new FeedbackStaffGump(this.viewer, this.m));
                }
                else if (info.ButtonID == (int)Buttons7.Remover)
                {
                    var ficha = this.m.FichaRP;
                    if (ficha.FeedbackStaff != "")
                    {
                        ficha.FeedbackStaff = "";
                        this.m.SendMessage(78, "Removido o ultimo registro!");
                    }
                    else
                    {
                        this.m.SendMessage(33, "Apenas o último registro inserido pode ser removido!");
                    }
                    sender.Mobile.SendGump(new FeedbackStaffGump(this.viewer, this.m));
                }
                else
                {
                    sender.Mobile.SendGump(new FichaRPGump(this.viewer, this.m));
                }
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

                    from.SendGump(new FichaRPGump(from as PlayerMobile, targeted as PlayerMobile));
                }
            }
        }
    }
}
