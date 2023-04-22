using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Custom.Classes;
using Server.Gumps;


namespace Server.Commands
{
    class RPClasse
    {
        public static void Initialize()
        {
            CommandSystem.Register("RPClasse", AccessLevel.Player, RPClasse_OnCommand);
        }

        [Usage("RPClasse")]
        [Description("Entra ou sai de uma classe, desde que atenda aos requisitos.")]
        private static void RPClasse_OnCommand(CommandEventArgs e)
        {
            PlayerMobile player = e.Mobile as PlayerMobile; //Personagem que chamou o comando

            if (e.ArgString == "")
            {
                ClasseGump gump = new ClasseGump(player);
                player.SendGump(gump);
                return;
            }
            else //Colocar algo aqui caso passe a existir alguma opção desse comando com os parâmetros
            {
                ClasseGump gump = new ClasseGump(player);
                player.SendGump(gump);
                return;
            }

        }

        private class ClasseGump : Gump
        {
            private PlayerMobile player;

            public ClasseGump(PlayerMobile player) : base(120, 50)
            {
                this.player = player;
                //Lista classes
                List<ClassePersonagem> OpcoesClasse = new List<ClassePersonagem>();
                string textoAbandonarClasse = "";

                if (player.ClasseBasicaID == 0) //Se ainda não possuiu primeira classe
                {
                    OpcoesClasse = ClassDef.GetClassesByTier(TierClasse.ClasseBasica);
                }
                else if (player.ClasseIntermediariaID == 0) //Se ainda não possuiu segunda classe
                {
                    OpcoesClasse = ClassDef.GetClassesByTier(TierClasse.ClasseIntermediaria);


                    if(player.ClasseAbandonavel == TierClasse.ClasseBasica)
                    {
                        textoAbandonarClasse = String.Format("<BIG><B>Abandonar caminho do {0}</B></BIG><BR> Seus Cap de skill voltam aos da classe anterior.", ClassDef.GetClasse(player.ClasseBasicaID).Nome);
                    }
                }
                else if (player.ClasseAvancadaID == 0) //Se ainda não possuiu terceira classe
                {
                    OpcoesClasse = ClassDef.GetClassesByTier(TierClasse.ClasseAvancada);
                    if (player.ClasseAbandonavel == TierClasse.ClasseIntermediaria)
                    {
                        textoAbandonarClasse = String.Format("<BIG><B>Abandonar caminho do {0}</B></BIG><BR> Seus Cap de skill voltam aos da classe anterior.", ClassDef.GetClasse(player.ClasseIntermediariaID).Nome);
                    }
                }
                else if (player.ClasseLendariaID == 0) //Se ainda não possuiu quarta classe
                {
                    OpcoesClasse = ClassDef.GetClassesByTier(TierClasse.ClasseLendaria);
                    if (player.ClasseAbandonavel == TierClasse.ClasseAvancada)
                    {
                        textoAbandonarClasse = String.Format("<BIG><B>Abandonar caminho do {0}</B></BIG><BR> Seus Cap de skill voltam aos da classe anterior.", ClassDef.GetClasse(player.ClasseAvancadaID).Nome);
                    }
                }
                else
                {
                    if (player.ClasseAbandonavel == TierClasse.ClasseLendaria)
                    {
                        textoAbandonarClasse = String.Format("<BIG><B>Abandonar caminho do {0}</B></BIG><BR> Seus Cap de skill voltam aos da classe anterior.", ClassDef.GetClasse(player.ClasseLendariaID).Nome);
                    }
                }


                AddPage(0);
                AddImage(0, 0, 30236); //500x600

                //Ajustar o titulo da tela pra esse HTML
                //AddHtml(100, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>3º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);

                int SLbaseX = 163; //Posição 'x' base do label do Idioma
                int SLbaseY = 52; //Posição 'y' base do label do Idioma
                int SBbaseX = 125; //Posição 'x' base do button do Idioma
                int SBbaseY = 50; //Posição 'y' base do button do Idioma

                int linhaCount = 0;

                //Monta o gump com a lista de classes por tier
                foreach (ClassePersonagem classe in OpcoesClasse)
                {
                    AddButton(SBbaseX, SBbaseY + (20 * linhaCount), 30008, 30009, 100 + classe.ID, GumpButtonType.Reply, 0);
                    AddButton(SBbaseX+20, SBbaseY + (20 * linhaCount), 4033, 4033, 9999 + classe.ID, GumpButtonType.Reply, 0); //acima de 9999 é botão de abrir informações
                    AddLabel(SLbaseX, SLbaseY + (20 * linhaCount), 1153, String.Format("{0}: {1}", classe.Nome, classe.Desc));
                    linhaCount++;
                }
                if(textoAbandonarClasse != "")
                {
                    AddButton(SBbaseX-30, SBbaseY + (20 * linhaCount)+20, 2151, 2154, 50, GumpButtonType.Reply, 0);
                    AddHtml(SLbaseX-30, SLbaseY + (20 * linhaCount)+20, 350, 100, textoAbandonarClasse, (bool)false, (bool)false);
                }
                else if(player.ClasseBasicaID != 0)
                {
                    AddHtml(SLbaseX-30, SLbaseY + (20 * linhaCount) + 20, 350, 100, "Não é mais possível voltar à classe de Tier anterior a essa.", (bool)false, (bool)false);
                }
            }

            public override void OnResponse(NetState state, RelayInfo info)
            {
                //Console.WriteLine(String.Format("Botão da classe clicado. ID = {0}", info.ButtonID));
                if (info.ButtonID == 0) //Gump fechado
                {
                    player.SendMessage("Seleção de Classe cancelada.");
                    return;
                }
                else if (info.ButtonID == 50) //Escolheu esquecer a classe
                {
                    switch (player.ClasseAbandonavel)
                    {
                        case TierClasse.ClasseBasica:
                            ClassDef.GetClasse(player.ClasseBasicaID).EsqueceClasse(player);
                            return;
                        case TierClasse.ClasseIntermediaria:
                            ClassDef.GetClasse(player.ClasseIntermediariaID).EsqueceClasse(player);
                            return;
                        case TierClasse.ClasseAvancada:
                            ClassDef.GetClasse(player.ClasseAvancadaID).EsqueceClasse(player);
                            return;
                        case TierClasse.ClasseLendaria:
                            ClassDef.GetClasse(player.ClasseLendariaID).EsqueceClasse(player);
                            return;
                        default:
                            return;
                    }
                }
                else
                {
                    int classeID = info.ButtonID - 100;
                    ClassDef.GetClasse(classeID).AplicaClasse(player);
                    return;
                }
            }
        }
    }
}
