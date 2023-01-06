using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Spells.Monge;
using Server.Prompts;

namespace Server.Gumps
{ //TODO: Achar o fundo certo do GUMP e ajustar a aparência
    public class MongeSpellbookGump : Gump
    {
        private MongeSpellbook m_Book;

        public bool HasSpell(Mobile from, int spellID)
        {
            return (m_Book.HasSpell(spellID));
        }

        public MongeSpellbookGump(Mobile from, MongeSpellbook book, int page) : base(100, 100)
        {
            m_Book = book;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddImage(41, 42, 11058);

            int PriorPage = page - 1;
            if (PriorPage < 1) { PriorPage = 20; }
            int NextPage = page + 1;

            AddButton(91, 50, 2235, 2235, PriorPage, GumpButtonType.Reply, 0);
            AddButton(362, 50, 2236, 2236, NextPage, GumpButtonType.Reply, 0);

            AddHtml(91, 52, 153, 31, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>Técnicas</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

            if (page == 1)
            {
                int TotalMagias = book.BookCount; //Numero máximo de magias do livro

                //int MagiasNoLivro = book.SpellCount; //Numero atual de magias do livro

                int nHTMLx = 111;
                int nHTMLy = 81;

                int nBUTTONx = 94;
                int nBUTTONy = 82;

                int MagiaInicialID = book.BookOffset;  //ID da primeira magia do livro
                int AcabaContagem = MagiaInicialID + TotalMagias;  //ID da ultima magia do livro
                int UltimoPaginaUm = MagiaInicialID + 9; //Posição da magia do meio da livro
                int temp = 0;
                string SpellName = "";

                for (int i = MagiaInicialID; i < AcabaContagem; i++)
                {
                    if (this.HasSpell(from, i))
                    {
                        switch (i)
                        {
                            case 860:
                                SpellName = "Ataque Elemental";
                                break;
                            case 861:
                                SpellName = "Correr no Vento";
                                break;
                            case 862:
                                SpellName = "Disciplina Mental";
                                break;
                            case 863:
                                SpellName = "Esfera de Ki";
                                break;
                            case 864:
                                SpellName = "Ferimento Interno";
                                break;
                            case 865:
                                SpellName = "Golpe Ascendente";
                                break;
                            case 866:
                                SpellName = "Golpe Atordoante";
                                break;
                            case 867:
                                SpellName = "Golpes Fortes";
                                break;
                            case 868:
                                SpellName = "Golpes Velozes";
                                break;
                            case 869:
                                SpellName = "Investida Fatal";
                                break;
                            case 870:
                                SpellName = "Mente Veloz";
                                break;
                            case 871:
                                SpellName = "Metabolizar Ferida";
                                break;
                            case 872:
                                SpellName = "Suprimir Veneno";
                                break;
                            case 873:
                                SpellName = "Palma Explosiva";
                                break;
                            case 874:
                                SpellName = "Rigidez Perfeita";
                                break;
                            case 875:
                                SpellName = "Salto Aprimorado";
                                break;
                            case 876:
                                SpellName = "Soco do Ki";
                                break;
                            case 877:
                                SpellName = "Soco Tectônico";
                                break;
                            case 878:
                                SpellName = "Soco Vulcânico";
                                break;
                            case 879:
                                SpellName = "Superação";
                                break;
                            
                        }
                        AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + SpellName + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                        AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30008, i, GumpButtonType.Reply, 0);
                        temp += 16; //representa o quão pra baixo no gump está a magia
                    }
                    if (i == UltimoPaginaUm)
                    {
                        nHTMLx = 267;
                        nHTMLy = 81;
                        nBUTTONx = 250;
                        nBUTTONy = 82;
                        temp = 0;
                    }
                }
            }

            else if (page == 2)
            {
                if (this.HasSpell(from, 860))
                {
                    AddButton(143, 76, 2242, 2242, 860, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Ataque Elemental</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Você canaliza a energia dos elementos em seus ataques.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Elementa Impetus</I><BR>Skill: 60<BR>Mana: 63<BR>Eficiência: 100%<BR>Reagentes:Nenhum.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 3)
            {
                if (this.HasSpell(from, 861))
                {
                    AddButton(143, 76, 2242, 2242, 861, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Correr no Vento</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Você se torna mais rápido que o vento.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Currunt Ventum</I><BR>Skill: 90<BR>Mana: 94<BR>Eficiência: 60%<BR>Reagentes: Garlic, Bloodmoss.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 4)
            {
                if (this.HasSpell(from, 862))
                {
                    AddButton(143, 76, 2242, 2242, 862, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Disciplina Mental</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>Você aprende a usar teu Ki para refletindo ataques mágicos.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Anima Disciplina</I><BR>Skill: 50<BR>Mana: 63<BR>Eficiência: 60%<BR>Reagentes: Garlic, Mandrake Root, Spiders' Silk.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }

            else if (page == 5)
            {
                if (this.HasSpell(from, 863))
                {
                    AddButton(143, 76, 2242, 2242, 863, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Esfera de Ki</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Impulsiona o Ar canalizando seu Ki num soco de fogo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Energo Sphaera</I><BR>Skill: 40<BR>Mana: 9<BR>Eficiência: 80%<BR>Reagentes: Black Peal, Sulfurous Ash.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 6)
            {
                if (this.HasSpell(from, 864))
                {
                    AddButton(143, 76, 2242, 2242, 864, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Ferimento interno</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Seu golpe gera sangramento interno no seu inimigo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Internum Vulnus</I><BR>Skill: 60<BR>Mana: 28<BR>Eficiência: 100%<BR>Reagentes: Nightshade, Spiders' Silk.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }

            else if (page == 7)
            {
                if (this.HasSpell(from, 865))
                {
                    AddButton(143, 76, 2242, 2242, 865, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Golpe Ascendente</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Manipula a energia vital do alvo para deixar ele mais fraco.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Volans Impetus</I><BR>Skill: 100<BR>Mana: 42<BR>Eficiência: 100%<BR>Reagentes: Nenhum.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 8)
            {
                if (this.HasSpell(from, 866))
                {
                    AddButton(143, 76, 2242, 2242, 866, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Golpe Atordoante</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>Seu golpe paraliza o inimigo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Impetus Attonitus</I><BR>Skill: 50<BR>Mana: 28<BR>Eficiência: 100%<BR>Reagentes: Nenhum.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 9)
            {
                if (this.HasSpell(from, 867))
                {
                    AddButton(143, 76, 2242, 2242, 867, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Golpes Fortes</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>Concentra KI na musculatura proporcionando aumento da sua força.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Fortis Impetus</I><BR>Skill: 30<BR>Mana: 6<BR>Eficiência: 60%<BR>Reagentes: Blood Moss, Mandrake Root.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }

            else if (page == 10)
            {
                if (this.HasSpell(from, 868))
                {
                    AddButton(143, 76, 2242, 2242, 868, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Golpes Velozes</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Concentra KI na musculatura proporcionando aumento da sua agilidade.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Celeriter Impetus</I><BR>Skill: 20<BR>Mana: 4<BR>Eficiência: 60%<BR>Reagentes: Blood Moss, Mandrake Root.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 11)
            {
                if (this.HasSpell(from, 869))
                {
                    AddButton(143, 76, 2242, 2242, 869, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Investida Fatal</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>Você corre até o alvo, investindo com uma velocidade extraordinária.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Terribili Impetus</I><BR>Skill: 80<BR>Mana: 63<BR>Eficiência: 100%<BR>Reagentes: Nenhum.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 12)
            {
                if (this.HasSpell(from, 870))
                {
                    AddButton(143, 76, 2242, 2242, 870, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Mente veloz</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Concentra KI no cérebro proporcionando aumento da sua inteligência.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Celeriter Anima</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 60%<BR>Reagentes: Mandrake Root, Nightshade.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 13)
            {
                if (this.HasSpell(from, 871))
                {
                    AddButton(143, 76, 2242, 2242, 871, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Metabolizar Ferida</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Você consegue fechar ferimentos internos leves.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Expellere Injuria</I><BR>Skill: 40<BR>Mana: 13<BR>Eficiência: 60%<BR>Reagentes:Nenhum</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 14)
            {
                if (this.HasSpell(from, 872))
                {
                    AddButton(143, 76, 2242, 2242, 872, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Suprimir Veneno</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Tenta metabolizar o veneno com seu Ki, mas desgasta seu corpo no processo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Venenum Expellere</I><BR>Skill: 50<BR>Mana: 13<BR>Eficiência: 60%<BR>Reagentes: Garlic, Ginseng.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 15)
            {
                if (this.HasSpell(from, 873))
                {
                    AddButton(143, 76, 2242, 2242, 873, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Palma Explosiva</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Seu golpe pode explodir órgãos internos de alvos próximos.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: Ignis Palma <I>Purifica</I><BR>Skill: 70<BR>Mana: 28<BR>Eficiência: 60%<BR>Reagentes: Blood Moss, Mandrake Root.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 16)
            {
                if (this.HasSpell(from, 874))
                {
                    AddButton(143, 76, 2242, 2242, 874, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Rigidez Perfeita</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Você consegue deixar seus musculos rigidos, criando assim um armadura no seu corpo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Optimum Rigoris</I><BR>Skill: 20<BR>Mana: 13<BR>Eficiência: 60%<BR>Reagentes:Nenhum.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 17)
            {
                if (this.HasSpell(from, 875))
                {
                    AddButton(143, 76, 2242, 2242, 875, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Salto Aprimorado</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Você consegue usar seu Ki para dar saltos sobrehumanos..</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Optimum Volare</I><BR>Skill: 30<BR>Mana: 19<BR>Eficiência: 60%<BR>Reagentes: Bloodmoss, Mandrake Root.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 18)
            {
                if (this.HasSpell(from, 876))
                {
                    AddButton(143, 76, 2242, 2242, 876, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Soco de Ki</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Seu soco é tão rápido que desloca o ar.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Energo Impetus</I><BR>Skill: 10<BR>Mana: 19<BR>Eficiência: 100%<BR>Reagentes: Garlic, Spiders' Silk, Sulphurous Ash.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 19)
            {
                if (this.HasSpell(from, 877))
                {
                    AddButton(143, 76, 2242, 2242, 877, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Soco Tectônico</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Você consegue usar seu Ki para criar um terremoto, lentificando todos ao redor.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Tectonicas Impetus</I><BR>Skill: 120<BR>Mana: 141<BR>Eficiência: 100%<BR>Reagentes: Duas Mandrake Root.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 20)
            {
                if (this.HasSpell(from, 878))
                {
                    AddButton(143, 76, 2242, 2242, 878, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Soco Vulcânico</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Você consegue usar seu Ki para criar fogo e lava.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Volcanus Impetus</I><BR>Skill: 120<BR>Mana: 141<BR>Eficiência: 100%<BR>Reagentes: Duas Sulfurous Ash.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 21)
            {
                if (this.HasSpell(from, 879))
                {
                    AddButton(143, 76, 2242, 2242, 879, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Superação</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Acelera seu KI no corpo inteiro proporcionando aumento de todos os atributos.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Mollitiam</I><BR>Skill: 30<BR>Mana: 50<BR>Eficiência: 60%<BR>Reagentes: Mandrake Root, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }

        }


        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            if (info.ButtonID < 860 && info.ButtonID > 0)
            {
                from.SendSound(0x55);
                int page = info.ButtonID;
                if (page < 1) { page = 21; }
                if (page > 21) { page = 1; }
                from.SendGump(new MongeSpellbookGump(from, m_Book, page));
            }
            else if (info.ButtonID > 859)
            {
                switch (info.ButtonID)
                {
                    case 860:
                        new AtaqueElementalSpell(from, null).Cast();
                        break;
                    case 861:
                        new CorrerNoVentoSpell(from, null).Cast();
                        break;
                    case 862:
                        new DisciplinaMentalSpell(from, null).Cast();
                        break;
                    case 863:
                        new EsferaDeKiSpell(from, null).Cast();
                        break;
                    case 864:
                        new FerimentoInternoSpell(from, null).Cast();
                        break;
                    case 865:
                        new GolpeAscendenteSpell(from, null).Cast();
                        break;
                    case 866:
                        new GolpeAtordoanteSpell(from, null).Cast();
                        break;
                    case 867:
                        new GolpesFortesSpell(from, null).Cast();
                        break;
                    case 868:
                        new GolpesVelozesSpell(from, null).Cast();
                        break;
                    case 869:
                        new InvestidaFatalSpell(from, null).Cast();
                        break;
                    case 870:
                        new MenteVelozSpell(from, null).Cast();
                        break;
                    case 871:
                        new MetabolizarFeridaSpell(from, null).Cast();
                        break;
                    case 872:
                        new SuprimirVenenoSpell(from, null).Cast();
                        break;
                    case 873:
                        new PalmaExplosivaSpell(from, null).Cast();
                        break;
                    case 874:
                        new RigidezPerfeitaSpell(from, null).Cast();
                        break;
                    case 875:
                        new SaltoAprimoradoSpell(from, null).Cast();
                        break;
                    case 876:
                        new SocoDoKiSpell(from, null).Cast();
                        break;
                    case 877:
                        new SocoTectonicoSpell(from, null).Cast();
                        break;
                    case 878:
                        new SocoVulcanicoSpell(from, null).Cast();
                        break;
                    case 879:
                        new SuperacaoSpell(from, null).Cast();
                        break;

                    default:
                        break;
                }

                /*
				else if ( info.ButtonID == 751 ){ new DemonicTouchSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 752 ){ new DevilPactSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 753 ){ new GrimReaperSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 754 ){ new HagHandSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 755 ){ new HellfireSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 756 ){ new LucifersBoltSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 757 ){ new OrbOfOrcusSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 758 ){ new ShieldOfHateSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 759 ){ new SoulReaperSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 760 ){ new StrengthOfSteelSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 761 ){ new StrikeSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 762 ){ new SuccubusSkinSpell( from, null ).Cast(); }
				else if ( info.ButtonID == 763 ){ new WrathSpell( from, null ).Cast(); }
                */
                from.SendGump(new MongeSpellbookGump(from, m_Book, 1));
            }
        }
    }
}

