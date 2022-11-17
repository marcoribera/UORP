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
    public class CharDetailsBGump : Gump
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
            //CommandSystem.Register("CharDetailsB", AccessLevel.Administrator, new CommandEventHandler(CharDetailsBGump_OnCommand));
        }

        [Usage("")]
        [Description("Gump de seleção detalhes B na criação de personagem.")]
        public static void CharDetailsBGump_OnCommand(CommandEventArgs e)
        {
            Mobile caller = e.Mobile;

            if (caller.HasGump(typeof(CharDetailsBGump)))
                caller.CloseGump(typeof(CharDetailsBGump));
            caller.SendGump(new CharDetailsBGump(caller));
        }

        public CharDetailsBGump() : base(0, 0)
        {
            return;
        }

        public CharDetailsBGump(Mobile from) : this()
        {
            caller = from;
        }


        public CharDetailsBGump(int selectedRace, ClassePersonagem selectedClasse, List<int> selectedSkills, List<int> detailAint, List<string> detailAstr, List<int> detailBint, List<int> selectedDefeitos, List<int> selectedQualidades, List<int> selectedSkillsComplementares) : base(0, 0)
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
            else
            {
                skillsComplementaresEscolhidas = new List<int>();
            }

            //inicializa qualidades
            if (selectedQualidades != null)
            {
                qualidadesEscolhidas = selectedQualidades;
            }
            else
            {
                qualidadesEscolhidas = new List<int>();
            }

            //inicializa defeitos
            if (selectedDefeitos != null)
            {
                defeitosEscolhidos = selectedDefeitos;
            }
            else
            {
                defeitosEscolhidos = new List<int>();
            }

            //inicializa detalhes da tela B
            if (detailBint != null)
            {
                detailsBInteirosEscolhidos = detailBint;
            }
            else
            {
                detailsBInteirosEscolhidos = new List<int>();
                detailsBInteirosEscolhidos.Insert(0, 1);  //Tipo de cabelo
                detailsBInteirosEscolhidos.Insert(1, detailsAInteirosEscolhidos[0] == 5 ? 62758: 53530); //Tipo de vestimenta
                detailsBInteirosEscolhidos.Insert(2, detailsAInteirosEscolhidos[0] == 5 ? 0 : 63251); //Tipo de vestimenta
                detailsBInteirosEscolhidos.Insert(3, detailsAInteirosEscolhidos[0] == 5 ? 63155 : 53179); //Tipo de vestimenta
                detailsBInteirosEscolhidos.Insert(4, 0); //ID do tipo de vestimenta

            }

            int idSexo, idIdade;

            idSexo = detailsAInteirosEscolhidos[0];
            idIdade = detailsAInteirosEscolhidos[1];

            AddPage(0);
            AddImage(0, -1, idSexo == 5 ? 40321 : 40320);
            AddHtml(73, 298, 173, 271,
                @"Instruções da tela aqui.",
                (bool)false, (bool)true);

            //Opções de Defeitos

            AddLabel(318, 123, 1153, @"Defeitos:");

            int LbaseX = 338; //Posição 'x' base do label do Defeito
            int LbaseY = 148; //Posição 'y' base do label do Defeito //22 entre cada label
            int BbaseX = 318; //Posição 'x' base do button do Defeito
            int BbaseY = 152; //Posição 'y' base do button do Defeito //22 entre cada label
            int linhaCount = 0;
            int totalCount = 0;
            bool marcado = false;
            foreach (KeyValuePair<int, string> defeito in listaDefeitos)
            {
                if ((totalCount > 0) && (totalCount % 3 == 0))
                {
                    linhaCount++;
                }
                if (defeitosEscolhidos != null) { marcado = defeitosEscolhidos.Contains((int)defeito.Key); }
                else { marcado = false; }

                AddButton(BbaseX + (124 * (totalCount % 3)), BbaseY + (22 * linhaCount), marcado ? 40310 : 40308, marcado ? 40308 : 40310, 100 + defeito.Key, GumpButtonType.Reply, 0);
                AddLabel(LbaseX + (124 * (totalCount % 3)), LbaseY + (22 * linhaCount), 1153, defeito.Value);
                totalCount++;
            }
            AddHtml(704, 129, 159, 100, @"Descrição do Defeito", (bool)false, (bool)true); //Descrição do defeito selecionado


            //Opções de Qualidades

            AddLabel(318, 255, 1153, @"Qualidades:");
            LbaseX = 338; //Posição 'x' base do label da Qualidade
            LbaseY = 280; //Posição 'y' base do label da Qualidade //22 entre cada label
            BbaseX = 318; //Posição 'x' base do button da Qualidade
            BbaseY = 284; //Posição 'y' base do button da Qualidade //22 entre cada label
            linhaCount = 0;
            totalCount = 0;
            marcado = false;
            foreach (KeyValuePair<int, string> qualidade in listaQualidades)
            {
                if ((totalCount > 0) && (totalCount % 3 == 0))
                {
                    linhaCount++;
                }
                if (qualidadesEscolhidas != null) { marcado = qualidadesEscolhidas.Contains((int)qualidade.Key); }
                else { marcado = false; }

                AddButton(BbaseX + (124 * (totalCount % 3)), BbaseY + (22 * linhaCount), marcado ? 40310 : 40308, marcado ? 40308 : 40310, 300 + qualidade.Key, GumpButtonType.Reply, 0);
                AddLabel(LbaseX + (124 * (totalCount % 3)), LbaseY + (22 * linhaCount), 1153, qualidade.Value);
                totalCount++;
            }

            AddHtml(704, 261, 159, 100, @"Descrição da Qualidade", (bool)false, (bool)true); //Descrição da qualidade selecionada


            //Seleção de skills complementares

            switch (classeEscolhida.ID)
            {
                case 20: //classes crafters
                case 21:
                    //listaSkillsComplementares = new List<SkillName>() { SkillName.AnimalLore, SkillName.Begging, SkillName.Camping, SkillName.Healing, SkillName.Forensics, SkillName.MagicResist, SkillName.Snooping, SkillName.Stealing, SkillName.Lockpicking, SkillName.Wrestling, SkillName.Tracking, SkillName.Swords, SkillName.Macing, SkillName.Fencing, SkillName.Tactics, SkillName.Parry, SkillName.Magery, SkillName.EvalInt};
                    break;
                default: //demais classes
                    //listaSkillsComplementares = new List<SkillName>() { SkillName.Cooking, SkillName.Fishing, SkillName.Herding, SkillName.Inscribe, SkillName.Cartography, SkillName.Tailoring, SkillName.Fletching, SkillName.Carpentry, SkillName.Lumberjacking, SkillName.ArmsLore, SkillName.Alchemy, SkillName.Mining};
                    break;
            }

            AddLabel(320, 382, 1153, @"Outras Habilidades:");

            LbaseX = 338; //Posição 'x' base do label das Skills Complementares
            LbaseY = 407; //Posição 'y' base do label das Skills Complementares //22 entre cada label
            BbaseX = 318; //Posição 'x' base do button das Skills Complementares
            BbaseY = 411; //Posição 'y' base do button das Skills Complementares //22 entre cada label
            linhaCount = 0;
            totalCount = 0;
            marcado = false;
            foreach (SkillName skill in listaSkillsComplementares)
            {
                if ((totalCount > 0) && (totalCount % 2 == 0))
                {
                    linhaCount++;
                }
                if (skillsComplementaresEscolhidas != null) { marcado = skillsComplementaresEscolhidas.Contains((int)skill); }
                else { marcado = false; }

                AddCheck(BbaseX + (104 * (totalCount % 2)), BbaseY + (22 * linhaCount), 40308, 40310, marcado, (int)skill);
                AddLabel(LbaseX + (104 * (totalCount % 2)), LbaseY + (22 * linhaCount), 1153, skill.ToString());
                totalCount++;
            }

            //Seleção de Cabelos

            AddLabel(690, 378, 1153, @"Cabelo:");
            LbaseX = 708; //Posição 'x' base do label do Cabelo
            LbaseY = 402; //Posição 'y' base do label do Cabelo //22 entre cada label
            BbaseX = 688; //Posição 'x' base do button do Cabelo
            BbaseY = 406; //Posição 'y' base do button do Cabelo //22 entre cada label
            linhaCount = 0;
            totalCount = 0;
            marcado = false;
            foreach (KeyValuePair<int, int> cabelo in idSexo == 5? listaCabelosF : listaCabelosM) // 5 é feminino
            {
                if ((totalCount > 0) && (totalCount % 4 == 0))
                {
                    linhaCount++;
                }
                if (cabelo.Key == detailsBInteirosEscolhidos[0]) { marcado = true; }
                else { marcado = false; }
                AddButton(BbaseX + (45 * (totalCount % 4)), BbaseY + (20 * linhaCount), marcado ? 40310 : 40308, marcado ? 40308 : 40310, 500 + cabelo.Key, GumpButtonType.Reply, 0);
                AddLabel(LbaseX + (45 * (totalCount % 4)), LbaseY + (20 * linhaCount), 1153, cabelo.Key.ToString());
                totalCount++;
            }

            AddImage(521, 359, detailsAInteirosEscolhidos[0] == 5? listaCabelosF[detailsBInteirosEscolhidos[0]] : listaCabelosM[detailsBInteirosEscolhidos[0]]); //cabelo

            //Peças de roupa

            AddLabel(690, 502, 1153, @"Roupa:");

            //classe.ID == classeID) ? 40310 : 40308)
            AddButton(691, 537, detailsBInteirosEscolhidos[4] == 0 ? 40310 : 40308, detailsBInteirosEscolhidos[4] == 0 ? 40308 : 40310, 0, GumpButtonType.Reply, 0);
            AddLabel(714, 535, 1153, @"Vestimenta tipo 1");

            AddButton(691, 558, detailsBInteirosEscolhidos[4] == 1 ? 40310 : 40308, detailsBInteirosEscolhidos[4] == 1 ? 40308 : 40310, 1, GumpButtonType.Reply, 0);
            AddLabel(714, 555, 1153, @"Vestimenta tipo 2");

            AddButton(691, 579, detailsBInteirosEscolhidos[4] == 2 ? 40310 : 40308, detailsBInteirosEscolhidos[4] == 2 ? 40308 : 40310, 2, GumpButtonType.Reply, 0);
            AddLabel(714, 577, 1153, @"Vestimenta tipo 3");

            if (detailsBInteirosEscolhidos[3] != 0) { AddImage(521, 359, detailsBInteirosEscolhidos[3]); } //botas
            if (detailsBInteirosEscolhidos[2] != 0) { AddImage(521, 359, detailsBInteirosEscolhidos[2]); } //calça
            if (detailsBInteirosEscolhidos[1] != 0) { AddImage(521, 359, detailsBInteirosEscolhidos[1]); } //camiseta
            
            

            //358, 362, 363, 357




            //Botão para passagem de página (Retorno)
            AddButton(77, 575, 4014, 4016, -999, GumpButtonType.Reply, 0);
            //Botão para passagem de página (Avanço)
            AddButton(843, 575, 4005, 4006, 999, GumpButtonType.Reply, 0);

        }



        public override void OnResponse(NetState sender, RelayInfo info)
        {
            caller = sender.Mobile;
            //skillsComplementaresEscolhidas = info.Switches.CastToList<int>();

            if (info.ButtonID == -999)
            {
                caller.SendGump(new CharDetailsAGump(raceEscolhida, classeEscolhida, skillsEscolhidas, detailsAInteirosEscolhidos, detailsAStringsEscolhidos));
                return;
            }
            else if (info.ButtonID == 999)
            {
                if (skillsComplementaresEscolhidas.Count == 2)
                {
                    caller.SendGump(new FinishCharGump(raceEscolhida, classeEscolhida, skillsEscolhidas, detailsAInteirosEscolhidos, detailsAStringsEscolhidos, detailsBInteirosEscolhidos, defeitosEscolhidos, qualidadesEscolhidas, skillsComplementaresEscolhidas));
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
                int cabelos = info.ButtonID - 500;
                //Console.WriteLine(String.Format("Cabelo ID: %0",cabelos));
                detailsBInteirosEscolhidos[0] = cabelos;
            }
            caller.SendGump(new CharDetailsBGump(raceEscolhida, classeEscolhida, skillsEscolhidas, detailsAInteirosEscolhidos, detailsAStringsEscolhidos,detailsBInteirosEscolhidos,defeitosEscolhidos,qualidadesEscolhidas,skillsComplementaresEscolhidas));
            return;
        }
    }
}
