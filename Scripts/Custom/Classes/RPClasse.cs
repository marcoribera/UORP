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

            foreach (Skill skill in player.Skills)
            {
                skill.Cap = 120.0;
            }
            player.SendMessage("Caps das skills ajustados para 120.");


            /*
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
            */
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
                        textoAbandonarClasse = String.Format("Abandonar caminho do {0} (Essa opção diminui os Cap de skill ganhos com a classe)", ClassDef.GetClasse(player.ClasseBasicaID).Nome);
                    }
                }
                else if (player.ClasseAvancadaID == 0) //Se ainda não possuiu terceira classe
                {
                    OpcoesClasse = ClassDef.GetClassesByTier(TierClasse.ClasseAvancada);
                    if (player.ClasseAbandonavel == TierClasse.ClasseIntermediaria)
                    {
                        textoAbandonarClasse = String.Format("Abandonar caminho do {0} (Essa opção diminui os Cap de skill ganhos com a classe)", ClassDef.GetClasse(player.ClasseIntermediariaID).Nome);
                    }
                }
                else if (player.ClasseLendariaID == 0) //Se ainda não possuiu quarta classe
                {
                    OpcoesClasse = ClassDef.GetClassesByTier(TierClasse.ClasseLendaria);
                    if (player.ClasseAbandonavel == TierClasse.ClasseAvancada)
                    {
                        textoAbandonarClasse = String.Format("Abandonar caminho do {0} (Essa opção diminui os Cap de skill ganhos com a classe)", ClassDef.GetClasse(player.ClasseAvancadaID).Nome);
                    }
                }
                else
                {
                    if (player.ClasseAbandonavel == TierClasse.ClasseLendaria)
                    {
                        textoAbandonarClasse = String.Format("Abandonar caminho do {0} (Essa opção diminui os Cap de skill ganhos com a classe)", ClassDef.GetClasse(player.ClasseLendariaID).Nome);
                    }
                }


                AddPage(0);
                AddImage(0, 0, 30236); //500x600

                int SLbaseX = 160; //Posição 'x' base do label do Idioma
                int SLbaseY = 59; //Posição 'y' base do label do Idioma
                int SBbaseX = 125; //Posição 'x' base do button do Idioma
                int SBbaseY = 50; //Posição 'y' base do button do Idioma

                int linhaCount = 0;

                //Monta o gump com a lista de classes por tier
                foreach (ClassePersonagem classe in OpcoesClasse)
                {
                    AddButton(SBbaseX, SBbaseY + (50 * linhaCount), 2151, 2154, 100 + classe.ID, GumpButtonType.Reply, 0);
                    AddLabel(SLbaseX, SLbaseY + (50 * linhaCount), 1153, String.Format("{0}: {1}", classe.Nome, classe.Desc));
                    linhaCount++;
                }
                if(textoAbandonarClasse != "")
                {
                    AddButton(SBbaseX, SBbaseY + (50 * linhaCount), 2151, 2154, 50, GumpButtonType.Reply, 0);
                    AddLabel(SLbaseX, SLbaseY + (50 * linhaCount), 1152, textoAbandonarClasse); //Ver se a cor prestou
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
