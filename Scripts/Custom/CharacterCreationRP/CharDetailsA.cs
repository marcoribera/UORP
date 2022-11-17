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
    public class CharDetailsAGump : Gump
    {
        Mobile caller;
        int raceEscolhida;
        ClassePersonagem classeEscolhida;
        List<int> skillsEscolhidas;
        List<int> detailsAInteirosEscolhidos;
        List<string> detailsAStringsEscolhidos;
        const int atributosMax = 80;

        public static void Initialize()
        {
            //CommandSystem.Register("CharDetailsA", AccessLevel.Administrator, new CommandEventHandler(CharDetailsAGump_OnCommand));
        }

        [Usage("")]
        [Description("Gump de seleção detalhes A na criação de personagem.")]
        public static void CharDetailsAGump_OnCommand(CommandEventArgs e)
        {
            Mobile caller = e.Mobile;

            if (caller.HasGump(typeof(CharDetailsAGump)))
                caller.CloseGump(typeof(CharDetailsAGump));
            caller.SendGump(new CharDetailsAGump(caller));
        }

        public CharDetailsAGump() : base(0, 0)
        {
            return;
        }

        public CharDetailsAGump(Mobile from) : this()
        {
            caller = from;
        }


        public CharDetailsAGump(int selectedRace, ClassePersonagem selectedClasse, List<int> selectedSkills, List<int> detailAint, List<string> detailAstr) : base(0, 0)
        {
            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;

            raceEscolhida = selectedRace;

            if (selectedClasse != null)
            {
                classeEscolhida = selectedClasse;
            }
            if (selectedSkills != null)
            {
                skillsEscolhidas = selectedSkills;
            }
            if (detailAint != null)
            {
                detailsAInteirosEscolhidos = detailAint;
            }
            else
            {
                detailsAInteirosEscolhidos = new List<int>();
                detailsAInteirosEscolhidos.Insert(0,5);
                detailsAInteirosEscolhidos.Insert(1,15);
                detailsAInteirosEscolhidos.Insert(2,20);
                detailsAInteirosEscolhidos.Insert(3,20);
                detailsAInteirosEscolhidos.Insert(4,20);
                detailsAInteirosEscolhidos.Insert(5,40598);
            }
            if (detailAstr != null)
            {
                detailsAStringsEscolhidos = detailAstr;
            }
            else
            {
                detailsAStringsEscolhidos = new List<string>();
                detailsAStringsEscolhidos.Insert(0,"");
                detailsAStringsEscolhidos.Insert(1,"");

            }

            //idImagem homem 40359 a 40518
            //idImagem mulher 40519 a 40679
            //List<int> detailAint = {idSexo, idIdade, valorStr, valorDex, valorInt, idImagem} 
            //List<string> detailAstr = {nome, descricaofisica}

            int idSexo, idIdade, valorStr, valorDex, valorInt, idImagem;
            string nome, descricaofisica;

            idSexo = detailsAInteirosEscolhidos[0];
            idIdade = detailsAInteirosEscolhidos[1];
            valorStr = detailsAInteirosEscolhidos[2];
            valorDex = detailsAInteirosEscolhidos[3];
            valorInt = detailsAInteirosEscolhidos[4];
            idImagem = detailsAInteirosEscolhidos[5];
            nome = detailsAStringsEscolhidos[0];
            descricaofisica = detailsAStringsEscolhidos[1];
            
            AddPage(0);
            AddImage(0, 1, 40319);
            AddLabel(318, 129, 1153, @"Nome:");
            AddTextEntry(378, 130, 198, 20, 1153, 0, nome);

            //Seleção do sexo

            AddLabel(318, 175, 1153, @"Sexo:");

            AddButton(382, 177, idSexo == 5 ? 40310 : 40308, idSexo == 5 ? 40308 : 40310, 5, GumpButtonType.Reply, 0);
            AddLabel(403, 176, 1153, @"Feminino");

            AddButton(480, 177, idSexo == 6 ? 40310 : 40308, idSexo == 6 ? 40308 : 40310, 6, GumpButtonType.Reply, 0);
            AddLabel(502, 176, 1153, @"Masculino");


            //Seleção de faixa etária

            AddLabel(318, 218, 1153, @"Idade:");

            AddButton(381, 221, idIdade == 15 ? 40310 : 40308, idIdade == 15 ? 40308 : 40310, 15, GumpButtonType.Reply, 0);
            AddLabel(402, 219, 1153, @"Jovem");

            AddButton(446, 222, idIdade == 16 ? 40310 : 40308, idIdade == 16 ? 40308 : 40310, 16, GumpButtonType.Reply, 0);
            AddLabel(468, 219, 1153, idSexo == 5? "Madura" : "Maduro");

            AddButton(522, 221, idIdade == 17 ? 40310 : 40308, idIdade == 17 ? 40308 : 40310, 17, GumpButtonType.Reply, 0);
            AddLabel(544, 219, 1153, idSexo == 5 ? "Velha" : "Velho");

            //Seleção de atributos

            AddLabel(318, 258, 1153, String.Format(@"Atributos: {0}/{1}", (valorStr + valorDex + valorInt), atributosMax));

            AddLabel(338, 282, 1153, @"Força:");
            AddButton(442, 278, 1545, 1546, -10, GumpButtonType.Reply, 0);
            AddLabel(462, 278, 1153, valorStr.ToString());
            AddButton(490, 277, 1543, 1544, 10, GumpButtonType.Reply, 0);

            AddLabel(338, 308, 1153, @"Destreza:");
            AddButton(442, 305, 1545, 1546, -20, GumpButtonType.Reply, 0);
            AddLabel(461, 305, 1153, valorDex.ToString());
            AddButton(490, 304, 1543, 1544, 20, GumpButtonType.Reply, 0);

            AddLabel(338, 332, 1153, @"Inteligência:");
            AddButton(442, 331, 1545, 1546, -30, GumpButtonType.Reply, 0);
            AddLabel(461, 332, 1153, valorInt.ToString());
            AddButton(490, 331, 1543, 1544, 30, GumpButtonType.Reply, 0);


            //Texto com instruções

            AddHtml(73, 298, 173, 271, @"Aqui ficam as instruções dessa tela.", (bool)false, (bool)true);

            //Imagem do personagem

            //Botão para passagem de imagem de personagem (Avanço)
            AddButton(427, 479, 9909, 9910, -110, GumpButtonType.Reply, 0);
            AddImage(469, 383, idImagem);
            //Botão para passagem de imagem de personagem (Retorno)
            AddButton(738, 479, 9903, 9904, 110, GumpButtonType.Reply, 0);
            

            //Descrição física do personagem

            AddLabel(609, 125, 1153, @"Descrição Física:");
            AddTextEntry(608, 152, 253, 194, 1153, 0, descricaofisica);


            //Botão para passagem de página (Retorno)
            AddButton(77, 575, 4014, 4016, -999, GumpButtonType.Reply, 0);
            //Botão para passagem de página (Avanço)
            AddButton(843, 575, 4005, 4006, 999, GumpButtonType.Reply, 0);

        }



        public override void OnResponse(NetState sender, RelayInfo info)
        {
            caller = sender.Mobile;

            /*
            idSexo = detailAint[0];
            idIdade = detailAint[1];
            valorStr = detailAint[2];
            valorDex = detailAint[3];
            valorInt = detailAint[4];
            idImagem = detailAint[5];
            nome = detailAstr[0];
            descricaofisica = detailAstr[1];
            */

            detailsAStringsEscolhidos[0] = info.TextEntries[0].Text; //nome
            detailsAStringsEscolhidos[1] = info.TextEntries[1].Text; //descrição física

            switch (info.ButtonID)
            {
                case 5:
                    {
                        //Console.WriteLine("feminino");
                        detailsAInteirosEscolhidos[0] = 5;
                        //Ajusta imagem quando mudou o sexo do personagem
                        if (detailsAInteirosEscolhidos[1] == 15) //jovem
                        {
                            detailsAInteirosEscolhidos[5] = 40598;
                        }
                        else if (detailsAInteirosEscolhidos[1] == 16) //madura
                        {
                            detailsAInteirosEscolhidos[5] = 40648;
                        }
                        else if (detailsAInteirosEscolhidos[1] == 17) //velha
                        {
                            detailsAInteirosEscolhidos[5] = 40679;
                        }
                        break;
                    }
                case 6:
                    {
                        //Console.WriteLine("masculino");
                        detailsAInteirosEscolhidos[0] = 6;
                        //Ajusta imagem quando mudou o sexo do personagem
                        if (detailsAInteirosEscolhidos[1] == 15) //jovem
                        {
                            detailsAInteirosEscolhidos[5] = 40438;
                        }
                        else if (detailsAInteirosEscolhidos[1] == 16) //maduro
                        {
                            detailsAInteirosEscolhidos[5] = 40488;
                        }
                        else if (detailsAInteirosEscolhidos[1] == 17) //velho
                        {
                            detailsAInteirosEscolhidos[5] = 40518;
                        }
                        break;
                    }
                case 15: //jovem
                    {
                        detailsAInteirosEscolhidos[1] = 15;
                        if (detailsAInteirosEscolhidos[0] == 5) //feminino
                        {
                            detailsAInteirosEscolhidos[5] = 40598;
                        }
                        else //masculino
                        {
                            detailsAInteirosEscolhidos[5] = 40438;
                        }
                        break;
                    }
                case 16: //maduro
                    {
                        detailsAInteirosEscolhidos[1] = 16;
                        if (detailsAInteirosEscolhidos[0] == 5) //feminino
                        {
                            detailsAInteirosEscolhidos[5] = 40648;
                        }
                        else //masculino
                        {
                            detailsAInteirosEscolhidos[5] = 40488;
                        }
                        break;
                    }
                case 17: //velho
                    {
                        detailsAInteirosEscolhidos[1] = 17;
                        if (detailsAInteirosEscolhidos[0] == 5) //feminino
                        {
                            detailsAInteirosEscolhidos[5] = 40679;
                        }
                        else //masculino
                        {
                            detailsAInteirosEscolhidos[5] = 40518;
                        }
                        break;
                    }
                case -10:
                    {
                       //Console.WriteLine("-STR");
                        if (detailsAInteirosEscolhidos[2] > 10)
                        {
                            detailsAInteirosEscolhidos[2]--;
                        }
                        break;
                    }
                case 10:
                    {
                        //Console.WriteLine("+STR");
                        if (detailsAInteirosEscolhidos[2] + detailsAInteirosEscolhidos[3] + detailsAInteirosEscolhidos[4] < atributosMax) //TODO: Sinalizar no gump quanto ja foi utilizado
                        {
                            detailsAInteirosEscolhidos[2]++;
                        }
                        break;
                    }
                case -20:
                    {
                        //Console.WriteLine("-DEX");
                        if (detailsAInteirosEscolhidos[3] > 10)
                        {
                            detailsAInteirosEscolhidos[3]--;
                        }
                        break;

                    }
                case 20:
                    {
                        //Console.WriteLine("+DEX");
                        if (detailsAInteirosEscolhidos[2] + detailsAInteirosEscolhidos[3] + detailsAInteirosEscolhidos[4] < atributosMax) //TODO: Sinalizar no gump quanto ja foi utilizado
                        {
                            detailsAInteirosEscolhidos[3]++;
                        }
                        break;

                    }
                case -30:
                    {
                        //Console.WriteLine("-INT");
                        if (detailsAInteirosEscolhidos[4] > 10)
                        {
                            detailsAInteirosEscolhidos[4]--;
                        }
                        break;

                    }
                case 30:
                    {
                        //Console.WriteLine("+INT");
                        if (detailsAInteirosEscolhidos[2] + detailsAInteirosEscolhidos[3] + detailsAInteirosEscolhidos[4] < atributosMax) //TODO: Sinalizar no gump quanto ja foi utilizado
                        {
                            detailsAInteirosEscolhidos[4]++;
                        }
                        break;

                    }
                case -110:
                    {
                        //idImagem homem 40359 a 40518
                        //idImagem mulher 40519 a 40679
                        //Console.WriteLine("-IMG");
                        if (detailsAInteirosEscolhidos[0] == 5) //feminino
                        {
                            if (detailsAInteirosEscolhidos[1] == 15) //jovem
                            {
                                if (detailsAInteirosEscolhidos[5] > 40519 && detailsAInteirosEscolhidos[5] <= 40598)
                                {
                                    detailsAInteirosEscolhidos[5]--;
                                }
                                else {
                                    detailsAInteirosEscolhidos[5] = 40598;
                                }
                            }
                            else if (detailsAInteirosEscolhidos[1] == 16) //madura
                            {
                                if (detailsAInteirosEscolhidos[5] > 40599 && detailsAInteirosEscolhidos[5] <= 40648)
                                {
                                    detailsAInteirosEscolhidos[5]--;
                                }
                                else
                                {
                                    detailsAInteirosEscolhidos[5] = 40648;
                                }
                            }
                            else if (detailsAInteirosEscolhidos[1] == 17) //velha
                            {
                                if (detailsAInteirosEscolhidos[5] > 40650 && detailsAInteirosEscolhidos[5] <= 40679)
                                {
                                    detailsAInteirosEscolhidos[5]--;
                                }
                                else
                                {
                                    detailsAInteirosEscolhidos[5] = 40679;
                                }
                            }
                        }
                        else //masculino
                        {
                            if (detailsAInteirosEscolhidos[1] == 15) //jovem
                            {
                                if (detailsAInteirosEscolhidos[5] > 40359 && detailsAInteirosEscolhidos[5] <= 40438)
                                {
                                    detailsAInteirosEscolhidos[5]--;
                                }
                                else
                                {
                                    detailsAInteirosEscolhidos[5] = 40438;
                                }
                            }
                            else if (detailsAInteirosEscolhidos[1] == 16) //maduro
                            {
                                if (detailsAInteirosEscolhidos[5] > 40439 && detailsAInteirosEscolhidos[5] <= 40488)
                                {
                                    detailsAInteirosEscolhidos[5]--;
                                }
                                else
                                {
                                    detailsAInteirosEscolhidos[5] = 40488;
                                }
                            }
                            else if (detailsAInteirosEscolhidos[1] == 17) //velho
                            {
                                if (detailsAInteirosEscolhidos[5] > 40489 && detailsAInteirosEscolhidos[5] <= 40518)
                                {
                                    detailsAInteirosEscolhidos[5]--;
                                }
                                else
                                {
                                    detailsAInteirosEscolhidos[5] = 40518;
                                }
                            }
                        }
                        break;
                    }
                case 110:
                    {
                        //idImagem homem 40359 a 40518
                        //idImagem mulher 40519 a 40679
                        //Console.WriteLine("+IMG");
                        if (detailsAInteirosEscolhidos[0] == 5) //feminino
                        {
                            if (detailsAInteirosEscolhidos[1] == 15) //jovem
                            {
                                if (detailsAInteirosEscolhidos[5] >= 40519 && detailsAInteirosEscolhidos[5] < 40598)
                                {
                                    detailsAInteirosEscolhidos[5]++;
                                }
                                else
                                {
                                    detailsAInteirosEscolhidos[5] = 40519;
                                }
                            }
                            else if (detailsAInteirosEscolhidos[1] == 16) //madura
                            {
                                if (detailsAInteirosEscolhidos[5] >= 40599 && detailsAInteirosEscolhidos[5] < 40648)
                                {
                                    detailsAInteirosEscolhidos[5]++;
                                }
                                else
                                {
                                    detailsAInteirosEscolhidos[5] = 40599;
                                }
                            }
                            else if (detailsAInteirosEscolhidos[1] == 17) //velha
                            {
                                if (detailsAInteirosEscolhidos[5] >= 40650 && detailsAInteirosEscolhidos[5] < 40679)
                                {
                                    detailsAInteirosEscolhidos[5]++;
                                }
                                else
                                {
                                    detailsAInteirosEscolhidos[5] = 40650;
                                }
                            }
                        }
                        else //masculino
                        {
                            if (detailsAInteirosEscolhidos[1] == 15) //jovem
                            {
                                if (detailsAInteirosEscolhidos[5] >= 40359 && detailsAInteirosEscolhidos[5] < 40438)
                                {
                                    detailsAInteirosEscolhidos[5]++;
                                }
                                else
                                {
                                    detailsAInteirosEscolhidos[5] = 40359;
                                }
                            }
                            else if (detailsAInteirosEscolhidos[1] == 16) //maduro
                            {
                                if (detailsAInteirosEscolhidos[5] >= 40439 && detailsAInteirosEscolhidos[5] < 40488)
                                {
                                    detailsAInteirosEscolhidos[5]++;
                                }
                                else
                                {
                                    detailsAInteirosEscolhidos[5] = 40439;
                                }
                            }
                            else if (detailsAInteirosEscolhidos[1] == 17) //jovem
                            {
                                if (detailsAInteirosEscolhidos[5] >= 40489 && detailsAInteirosEscolhidos[5] < 40518)
                                {
                                    detailsAInteirosEscolhidos[5]++;
                                }
                                else
                                {
                                    detailsAInteirosEscolhidos[5] = 40489;
                                }
                            }
                        }
                        break;
                    }
                case -999:
                    {
                        //Console.WriteLine("Voltar para Classe");
                        caller.SendGump(new ClassSelectionGump(raceEscolhida, classeEscolhida, skillsEscolhidas));
                        return;
                    }
                case 999:
                    {
                        //Console.WriteLine("Avançar para Detalhes B");
                        if ((detailsAInteirosEscolhidos[2] + detailsAInteirosEscolhidos[3] + detailsAInteirosEscolhidos[4] == atributosMax) && detailsAStringsEscolhidos[0] != "" && detailsAStringsEscolhidos[1] != "")
                        {
                            caller.SendGump(new CharDetailsBGump(raceEscolhida, classeEscolhida, skillsEscolhidas, detailsAInteirosEscolhidos, detailsAStringsEscolhidos, null, null, null, null));
                            return;
                        }
                        break;
                    }
                default:
                    {
                        //Console.WriteLine("Char Detail A");
                        break;
                    }
            }
            caller.SendGump(new CharDetailsAGump(raceEscolhida, classeEscolhida, skillsEscolhidas, detailsAInteirosEscolhidos, detailsAStringsEscolhidos));
            return;
        }
    }
}


/*
Mulheres
Jovens
40519
40598

Maduras
40599
40648

Velhas
40650
40679
///////////////////////////////////
Homens
Jovens
40359
40438

maduros
40439
40488

velhos
40489
40518*/
