using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Custom.Classes;

namespace Server.Gumps.CharacterCreationRP
{
    public class FinishCharGump : Gump
    {
        Mobile caller;
        int raceEscolhida;
        ClassePersonagem classeEscolhida;
        List<int> skillsEscolhidas;
        List<int> detailsAInteirosEscolhidos;
        List<string> detailsAStringsEscolhidos;

        List<int> defeitosEscolhidos;
        List<int> qualidadesEscolhidas;
        List<int> skillsComplementaresEscolhidas;
        List<int> detailsBInteirosEscolhidos;

        Dictionary<int, string> listaQualidades = new Dictionary<int, string>()
            {
                               {1,"Qualidade 1"},
                               {2,"Qualidade 2"},
                               {3,"Qualidade 3"},
                               {4,"Qualidade 4"},
                               {5,"Qualidade 5"},
                               {6,"Qualidade 6"}
            };
        Dictionary<int, string> listaDefeitos = new Dictionary<int, string>()
            {
                               {1,"Defeito 1"},
                               {2,"Defeito 2"},
                               {3,"Defeito 3"},
                               {4,"Defeito 4"},
                               {5,"Defeito 5"},
                               {6,"Defeito 6"}
            };

        Dictionary<int,int>  listaCabelosM = new Dictionary<int,int>()
            {
                {1, 52449},
                {2, 52450},
                {3, 52452},
                {4, 52453},
                {5, 52454},
                {6, 52456},
                {7, 52458},
                {8, 52565},
                {9, 52466},
                {10, 52467},
                {11, 52468},
                {12, 52469},
                {13, 52477},
                {14, 52479},
                {15, 52490},
                {16, 52492},
                {17, 52495},
                {18, 52496},
                {19, 52505},
                {20, 52535}
            };
        Dictionary<int, int> listaCabelosF = new Dictionary<int, int>()
            {
                {1, 52455},
                {2, 52457},
                {3, 52459},
                {4, 52463},
                {5, 52471},
                {6, 52472},
                {7, 52475},
                {8, 52477},
                {9, 52478},
                {10, 52479},
                {11, 52483},
                {12, 52487},
                {13, 52488},
                {14, 52490},
                {15, 52491},
                {16, 52497},
                {17, 52498},
                {18, 52500},
                {19, 52503},
                {20, 52561}
            };

        List<SkillName> listaSkillsComplementares;

        public static void Initialize()
        {
            //CommandSystem.Register("CharDetailsB", AccessLevel.Administrator, new CommandEventHandler(FinishCharGump_OnCommand));
        }

        [Usage("")]
        [Description("Gump de seleção detalhes B na criação de personagem.")]
        public static void FinishCharGump_OnCommand(CommandEventArgs e)
        {
            Mobile caller = e.Mobile;

            if (caller.HasGump(typeof(FinishCharGump)))
                caller.CloseGump(typeof(FinishCharGump));
            caller.SendGump(new FinishCharGump(caller));
        }

        public FinishCharGump() : base(0, 0)
        {
            return;
        }

        public FinishCharGump(Mobile from) : this()
        {
            caller = from;
        }


        public FinishCharGump(int selectedRace, ClassePersonagem selectedClasse, List<int> selectedSkills, List<int> detailAint, List<string> detailAstr, List<int> detailBint, List<int> selectedDefeitos, List<int> selectedQualidades, List<int> selectedSkillsComplementares) : base(0, 0)
        {
            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;

            //inicializa raça
            raceEscolhida = selectedRace;

            //inicializa classe
            if (selectedClasse != null)
            {
                classeEscolhida = selectedClasse;
            }

            //inicializa skills de classe
            if (selectedSkills != null)
            {
                skillsEscolhidas = selectedSkills;
            }

            //inicializa detalhes da tela A
            if (detailAint != null)
            {
                detailsAInteirosEscolhidos = detailAint;
            }

            if (detailAstr != null)
            {
                detailsAStringsEscolhidos = detailAstr;
            }

            //inicializa skill complementares
            if (selectedSkillsComplementares != null)
            {
                skillsComplementaresEscolhidas = selectedSkillsComplementares;
            }
            
            //inicializa qualidades
            if (selectedQualidades != null)
            {
                qualidadesEscolhidas = selectedQualidades;
            }
            
            //inicializa defeitos
            if (selectedDefeitos != null)
            {
                defeitosEscolhidos = selectedDefeitos;
            }
            
            //inicializa detalhes da tela B
            if (detailBint != null)
            {
                detailsBInteirosEscolhidos = detailBint;
            }

            //armazena em variaveis com nomes mais intuitivos os valores pertinentes para criação do personagem

            int idRace, idSexo, idIdade, valorStr, valorDex, valorInt, idImagem, idCabelo;
            int[] idVestimenta = new int[4];
            string nome, descricaofisica;

            idRace = raceEscolhida;
            idSexo = detailsAInteirosEscolhidos[0];
            idIdade = detailsAInteirosEscolhidos[1];
            valorStr = detailsAInteirosEscolhidos[2];
            valorDex = detailsAInteirosEscolhidos[3];
            valorInt = detailsAInteirosEscolhidos[4];
            idImagem = detailsAInteirosEscolhidos[5];
            nome = detailsAStringsEscolhidos[0];
            descricaofisica = detailsAStringsEscolhidos[1];
            idCabelo = detailsBInteirosEscolhidos[0];
            idVestimenta[0] = detailsBInteirosEscolhidos[1];
            idVestimenta[1] = detailsBInteirosEscolhidos[2];
            idVestimenta[2] = detailsBInteirosEscolhidos[3];
            idVestimenta[3] = detailsBInteirosEscolhidos[4];

            AddPage(0);
            AddImage(0, 0, 40322);

            AddHtml(314, 120, 273, 19, String.Format("<BASEFONT color=#ffffff size=6><CENTER><B>{0}</B></CENTER></BASEFONT>", nome), (bool)false, (bool)false);
            //AddLabel(314, 120, 1153, String.Format("<BASEFONT color=#ffffff size=7><B>{0}</B></BASEFONT>"), nome);

            AddHtml(73, 298, 173, 271,
                @"Instruções da tela aqui.",
                (bool)false, (bool)true);

            AddLabel(318, 166, 1153, @"Raça:");
            AddLabel(364, 167, 556, idRace == 11 ? "Aludia" : "Daru");

            AddLabel(437, 166, 1153, @"Classe:");
            AddLabel(495, 166, 556, classeEscolhida.Nome);

            AddLabel(318, 200, 1153, @"Sexo:");
            AddLabel(365, 200, 556, idSexo == 5? "Fem":"Mas");

            AddLabel(442, 200, 1153, @"Idade:");
            AddLabel(496, 200, 556, idIdade == 15 ? "Jovem" : idIdade == 16 ? (idSexo == 5 ? "Madura" : "Maduro") : idSexo == 5 ? "Velha" : "Velho");

            AddLabel(318, 233, 1153, @"Atributos:");
            AddLabel(318, 264, 1153, @"Força:");
            AddLabel(355, 264, 556, valorStr.ToString());
            AddLabel(389, 264, 1153, @"Destreza:");
            AddLabel(445, 264, 556, valorDex.ToString());
            AddLabel(475, 264, 1153, @"Inteligencia:");
            AddLabel(542, 264, 556, valorInt.ToString());

            AddLabel(318, 315, 1153, @"Defeitos:");
            int LbaseX = 317; //Posição 'x' base do label do Defeito
            int LbaseY = 337; //Posição 'y' base do label do Defeito //22 entre cada label
            int linhaCount = 0;
            int totalCount = 0;

            foreach (int defeito in defeitosEscolhidos)
            {
                if ((totalCount > 0) && (totalCount % 2 == 0))
                {
                    linhaCount++;
                }
                AddLabel(LbaseX + (124 * (totalCount % 2)), LbaseY + (21 * linhaCount), 556, listaDefeitos[defeito]);
                totalCount++;
            }

            AddLabel(318, 389, 1153, @"Qualidades:");
            LbaseX = 317; //Posição 'x' base do label da Qualidade
            LbaseY = 409; //Posição 'y' base do label da Qualidade //22 entre cada label
            linhaCount = 0;
            totalCount = 0;

            foreach (int qualidade in qualidadesEscolhidas)
            {
                if ((totalCount > 0) && (totalCount % 2 == 0))
                {
                    linhaCount++;
                }
                AddLabel(LbaseX + (124 * (totalCount % 2)), LbaseY + (21 * linhaCount), 556, listaQualidades[qualidade]);
                totalCount++;
            }

            AddLabel(318, 459, 1153, @"Habilidades Treinadas:");
            LbaseX = 317; //Posição 'x' base do label da Habilidade
            LbaseY = 485; //Posição 'y' base do label da Habilidade //22 entre cada label
            linhaCount = 0;
            totalCount = 0;

            foreach (int habilidadeT in skillsEscolhidas)
            {
                if ((totalCount > 0) && (totalCount % 2 == 0))
                {
                    linhaCount++;
                }
                AddLabel(LbaseX + (124 * (totalCount % 2)), LbaseY + (21 * linhaCount), 556, SkillInfo.Table[habilidadeT].Name);
                totalCount++;
            }
            
            AddLabel(318, 535, 1153, @"Habilidades Complementares:");
            LbaseX = 317; //Posição 'x' base do label da Habilidade
            LbaseY = 556; //Posição 'y' base do label da Habilidade //22 entre cada label
            linhaCount = 0;
            totalCount = 0;

            foreach (int habilidadeC in skillsComplementaresEscolhidas)
            {
                if ((totalCount > 0) && (totalCount % 2 == 0))
                {
                    linhaCount++;
                }
                AddLabel(LbaseX + (124 * (totalCount % 2)), LbaseY + (21 * linhaCount), 556, SkillInfo.Table[habilidadeC].Name);
                totalCount++;
            }

            AddLabel(615, 368, 1153, @"Descrição Física:");
            AddHtml(612, 398, 258, 174, descricaofisica, (bool)false, (bool)true);
            
            AddLabel(613, 579, 1153, @"Cabelo:");
            AddLabel(666, 579, 556, idCabelo.ToString());

            AddLabel(698, 579, 1153, @"Roupa:");
            AddLabel(754, 579, 556, String.Format("Tipo {0}",idVestimenta[3]));

            /*
            AddButton(826, 582, 2074, 2075, 0, GumpButtonType.Reply, 0);
            AddButton(316, 581, 2071, 2072, 0, GumpButtonType.Reply, 0);
            */


            //imagem do char
            AddImage(616, 132, idImagem);


            //Botão para passagem de página (Retorno)
            AddButton(77, 575, 4014, 4016, -999, GumpButtonType.Reply, 0);
            //Botão para passagem de página (Avanço)
            AddButton(826, 582, 2074, 2075, 999, GumpButtonType.Reply, 0);

        }



        public override void OnResponse(NetState sender, RelayInfo info)
        {
            caller = sender.Mobile;

            /*
            skillsComplementaresEscolhidas = info.Switches.CastToList<int>();

            if (info.ButtonID == -999)
            {
                caller.SendGump(new CharDetailsBGump(raceEscolhida, classeEscolhida, skillsEscolhidas, detailsAInteirosEscolhidos, detailsAStringsEscolhidos, detailsBInteirosEscolhidos, defeitosEscolhidos, qualidadesEscolhidas, skillsComplementaresEscolhidas));
                return;
            }
            else if (info.ButtonID == 999)
            {
                if (skillsComplementaresEscolhidas.Count == 2)
                {
                    //caller.SendGump(new FinishCharGump(raceEscolhida, classeEscolhida, skillsEscolhidas, detailsAInteirosEscolhidos, detailsAStringsEscolhidos, detailsBInteirosEscolhidos, defeitosEscolhidos, qualidadesEscolhidas, skillsComplementaresEscolhidas));
                    return;
                }
            }
            else if (info.ButtonID < 100) //kits de vestuario
                switch (info.ButtonID)
                {
                    case 0:
                        if (detailsAInteirosEscolhidos[0] == 5)
                        { //feminino
                            detailsBInteirosEscolhidos[1] = 62758;
                            detailsBInteirosEscolhidos[2] = 0;
                            detailsBInteirosEscolhidos[3] = 63155;
                            detailsBInteirosEscolhidos[4] = 0;
                        }
                        else
                        { //masculino
                            detailsBInteirosEscolhidos[1] = 53530;
                            detailsBInteirosEscolhidos[2] = 63251;
                            detailsBInteirosEscolhidos[3] = 53179;
                            detailsBInteirosEscolhidos[4] = 0;
                        }
                        break;
                    case 1:
                        if (detailsAInteirosEscolhidos[0] == 5)
                        { //feminino
                            detailsBInteirosEscolhidos[1] = 63488;
                            detailsBInteirosEscolhidos[2] = 63194;
                            detailsBInteirosEscolhidos[3] = 63179;
                            detailsBInteirosEscolhidos[4] = 1;
                        }
                        else
                        { //masculino
                            detailsBInteirosEscolhidos[1] = 53454;
                            detailsBInteirosEscolhidos[2] = 53191;
                            detailsBInteirosEscolhidos[3] = 53180;
                            detailsBInteirosEscolhidos[4] = 1;
                        }
                        break;
                    case 2:
                        if (detailsAInteirosEscolhidos[0] == 5)
                        { //feminino
                            detailsBInteirosEscolhidos[1] = 63629;
                            detailsBInteirosEscolhidos[2] = 63205;
                            detailsBInteirosEscolhidos[3] = 63180;
                            detailsBInteirosEscolhidos[4] = 2;
                        }
                        else
                        { //masculino
                            detailsBInteirosEscolhidos[1] = 53461;
                            detailsBInteirosEscolhidos[2] = 53215;
                            detailsBInteirosEscolhidos[3] = 53180;
                            detailsBInteirosEscolhidos[4] = 2;
                        }
                        break;
                    default:
                        if (detailsAInteirosEscolhidos[0] == 5)
                        { //feminino
                            detailsBInteirosEscolhidos[1] = 62758;
                            detailsBInteirosEscolhidos[2] = 0;
                            detailsBInteirosEscolhidos[3] = 63155;
                            detailsBInteirosEscolhidos[4] = 0;
                        }
                        else
                        { //masculino
                            detailsBInteirosEscolhidos[1] = 53530;
                            detailsBInteirosEscolhidos[2] = 63251;
                            detailsBInteirosEscolhidos[3] = 53179;
                            detailsBInteirosEscolhidos[4] = 0;
                        }
                        break;
                }
            else if (info.ButtonID < 300)
            { //defeitos
                int defeito = info.ButtonID - 100;
                if (defeitosEscolhidos.Contains(defeito))
                {
                    defeitosEscolhidos.Remove(defeito);
                }
                else
                {
                    defeitosEscolhidos.Add(defeito);
                }
            }
            else if (info.ButtonID < 500)
            { //qualidades
                int qualidade = info.ButtonID - 300;
                if (qualidadesEscolhidas.Contains(qualidade))
                {
                    qualidadesEscolhidas.Remove(qualidade);
                }
                else
                {
                    qualidadesEscolhidas.Add(qualidade);
                }
            }
            else if (info.ButtonID < 700)
            { //cabelos
                Console.WriteLine();
                int cabelos = info.ButtonID - 500;
                Console.WriteLine(String.Format("Cabelo ID: %0",cabelos));
                detailsBInteirosEscolhidos[0] = cabelos;
            }
            */
            caller.SendGump(new FinishCharGump(raceEscolhida, classeEscolhida, skillsEscolhidas, detailsAInteirosEscolhidos, detailsAStringsEscolhidos,detailsBInteirosEscolhidos,defeitosEscolhidos,qualidadesEscolhidas,skillsComplementaresEscolhidas));
            return;
        }
    }
}
