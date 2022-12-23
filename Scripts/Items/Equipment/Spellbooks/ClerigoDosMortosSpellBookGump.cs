using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Spells.ClerigoDosMortos;
using Server.Prompts;

namespace Server.Gumps
{ //TODO: Achar o fundo certo do GUMP e ajustar a aparência
    public class ClerigoDosMortosSpellbookGump : Gump
    {
        private ClerigoDosMortosSpellbook m_Book;

        public bool HasSpell(Mobile from, int spellID)
        {
            return (m_Book.HasSpell(spellID));
        }

        public ClerigoDosMortosSpellbookGump(Mobile from, ClerigoDosMortosSpellbook book, int page) : base(100, 100)
        {
            m_Book = book;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddImage(41, 42, 11058);

            int PriorPage = page - 1;
            if (PriorPage < 1) { PriorPage = 19; }
            int NextPage = page + 1;

            AddButton(91, 50, 2235, 2235, PriorPage, GumpButtonType.Reply, 0);
            AddButton(362, 50, 2236, 2236, NextPage, GumpButtonType.Reply, 0);

            AddHtml(91, 52, 153, 31, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>Mortos</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

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
                            case 950:
                                SpellName = "Abrir Feridas";
                                break;
                            case 951:
                                SpellName = "Alimento Mortal";
                                break;
                            case 952:
                                SpellName = "Campo Venenoso";
                                break;
                            case 953:
                                SpellName = "Drenar Agilidade";
                                break;
                            case 954:
                                SpellName = "Drenar Essencia";
                                break;
                            case 955:
                                SpellName = "Drenar Forca";
                                break;
                            case 956:
                                SpellName = "Drenar Mente";
                                break;
                            case 957:
                                SpellName = "Drenar Mana";
                                break;
                            case 958:
                                SpellName = "Drenar Alma";
                                break;
                            case 959:
                                SpellName = "Entorpecer";
                                break;
                            case 960:
                                SpellName = "Envenenar Mente";
                                break;
                            case 961:
                                SpellName = "Envenenar";
                                break;
                            case 962:
                                SpellName = "Erguer Cadaver";
                                break;
                            case 963:
                                SpellName = "Escurecer Vista";
                                break;
                            case 964:
                                SpellName = "Fogo Da Morte";
                                break;
                            case 965:
                                SpellName = "Pacto De Sangue";
                                break;
                            case 966:
                                SpellName = "Abrigo das Trevas";
                                break;
                            case 967:
                                SpellName = "Ritual Lich";
                                break;
                            case 968:
                                SpellName = "Sonolencia";
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
                if (this.HasSpell(from, 950))
                {
                    AddButton(143, 76, 2242, 2242, 950, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Abrir Feridas</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> A sua conexão com a morte permite putrefar a carne viva.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Causa Vulnera</I><BR>Skill: 20<BR>Mana: 6<BR>Eficiência: 60%<BR>Reagentes: Nightshade, Spiders' Silk.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 3)
            {
                if (this.HasSpell(from, 951))
                {
                    AddButton(143, 76, 2242, 2242, 951, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Alimento Mortal</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Cria carnes cruas.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Mortuus Cibum</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 60%<BR>Reagentes: Garlic, Ginseng, Mandrake Root.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 4)
            {
                if (this.HasSpell(from, 952))
                {
                    AddButton(143, 76, 2242, 2242, 952, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Campo Venenoso</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>Cria um perigoso campo venenoso em uma área escolhida.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Venenata Agri</I><BR>Skill: 110<BR>Mana: 140<BR>Eficiência: 80%<BR>Reagentes: Black Pearl, Nightshade, Spiders' Silk.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }

            else if (page == 5)
            {
                if (this.HasSpell(from, 953))
                {
                    AddButton(143, 76, 2242, 2242, 953, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Drenar Agilidade</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Manipula a energia vital do alvo para deixar ele mais lerdo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Exhaurire Agilitas</I><BR>Skill: 20<BR>Mana: 6<BR>Eficiência: 60%<BR>Reagentes: Blood Moss, Nightshade.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 6)
            {
                if (this.HasSpell(from, 954))
                {
                    AddButton(143, 76, 2242, 2242, 954, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Drenar Essência</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Manipula a energia vital do alvo para melhorar todos os atributos.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Exhaurire Essentia</I><BR>Skill: 30<BR>Mana: 9<BR>Eficiência: 260%<BR>Reagentes: Garlic, Nightshade, Sulfurous Ashc.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }

            else if (page == 7)
            {
                if (this.HasSpell(from, 955))
                {
                    AddButton(143, 76, 2242, 2242, 955, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Drenar Força</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Manipula a energia vital do alvo para deixar ele mais fraco.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Exhauriat Force</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 60%<BR>Reagentes: Garlic, Nightshade.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 8)
            {
                if (this.HasSpell(from, 956))
                {
                    AddButton(143, 76, 2242, 2242, 956, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Drenar Mente</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Manipula a energia vital do alvo para deixar ele mais burro.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Exhaurire Intelligentia</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 60%<BR>Reagentes: Nightshade, Ginseng.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 9)
            {
                if (this.HasSpell(from, 957))
                {
                    AddButton(143, 76, 2242, 2242, 957, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Drenar Mana</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>Suga uma parte da energia magica do inimigo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Exhaurire Mana</I><BR>Skill: 40<BR>Mana: 13<BR>Eficiência: 100%<BR>Reagentes: Black Pearl, Blood Moss, Mandrake Root, Spiders' Silk.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }

            else if (page == 10)
            {
                if (this.HasSpell(from, 958))
                {
                    AddButton(143, 76, 2242, 2242, 958, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Drenar Alma</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Drena uma parte consideravel da energia mágica do inimigo do clérigo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Exhaurire Quintam</I><BR>Skill: 70<BR>Mana: 28<BR>Eficiência: 100%<BR>Reagentes: Black Pearl, Mandrake Root, Spiders' Silk.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 11)
            {
                if (this.HasSpell(from, 959))
                {
                    AddButton(143, 76, 2242, 2242, 959, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Entorpecer</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>Manipula a energia vital do alvo para deixar ele mais lerdo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Obstupefacio</I><BR>Skill: 70<BR>Mana: 28<BR>Eficiência: 80%<BR>Reagentes: Garlic, Mandrake Root, Nightshade, Sulfurous Ash.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 12)
            {
                if (this.HasSpell(from, 960))
                {
                    AddButton(143, 76, 2242, 2242, 960, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Envenenar a Mente</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> O clérigo usa seus poderes para envenenar os pensamentos.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Venenum Mentis</I><BR>Skill: 50<BR>Mana: 19<BR>Eficiência: 60%<BR>Reagentes: Black Pearl, Mandrake Root, Nightshade, Sulfurous Ash.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 13)
            {
                if (this.HasSpell(from, 961))
                {
                    AddButton(143, 76, 2242, 2242, 961, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Envenenar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> O corpo de seu inimigo se convulsiona fortemente com um forte veneno.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Ut Venenum</I><BR>Skill: 65<BR>Mana: 42<BR>Eficiência: 80%<BR>Reagentes:2 Nightshade.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 14)
            {
                if (this.HasSpell(from, 962))
                {
                    AddButton(143, 76, 2242, 2242, 962, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Erguer Cadáver</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Transforma corpos em mortos-vivo que atacam as criaturas ao seu redor.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Suscitare Mortuos</I><BR>Skill: 40<BR>Mana: 13<BR>Eficiência: 100%<BR>Reagentes: Daemon Blood, Grave Dust.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 15)
            {
                if (this.HasSpell(from, 963))
                {
                    AddButton(143, 76, 2242, 2242, 963, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Escurecer Vista</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> O clérigo da Morte escurece a vista de seu inimigo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: Obscurare Sententiam <I>Purifica</I><BR>Skill: 30<BR>Mana: 9<BR>Eficiência: 60%<BR>Reagentes: Spiders' Silk, Sulphurous Ash.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 16)
            {
                if (this.HasSpell(from, 964))
                {
                    AddButton(143, 76, 2242, 2242, 964, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Fogo de Morte</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Dano venenoso muito alto em uma área.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Ignis Mortis</I><BR>Skill: 100<BR>Mana: 42<BR>Eficiência: 100%<BR>Reagentes:Spiders' Silk, Sulfurous Ash.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 17)
            {
                if (this.HasSpell(from, 965))
                {
                    AddButton(143, 76, 2242, 2242, 965, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Pacto de Sangue</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Aumenta o dano de um aliado, ao custo de sofrer mais dano.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Sanguis Foedus</I><BR>Skill: 20<BR>Mana: 6<BR>Eficiência: 80%<BR>Reagentes: Dois Daemon Blood.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 18)
            {
                if (this.HasSpell(from, 966))
                {
                    AddButton(143, 76, 2242, 2242, 966, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Abrigo das Trevas</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Protege o Clérigo contra o envenenamento.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Tenebris Tegimen</I><BR>Skill: 30<BR>Mana: 9<BR>Eficiência: 80%<BR>Reagentes: Garlic, Spiders' Silk, Sulphurous Ash.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 19)
            {
                if (this.HasSpell(from, 967))
                {
                    AddButton(143, 76, 2242, 2242, 967, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Ritual Lich</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Permite o clérigo se tornar um morto vivo poderoso.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Mortuis Rituali</I><BR>Skill: 120<BR>Mana: 211<BR>Eficiência: 100%<BR>Reagentes: Item.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 20)
            {
                if (this.HasSpell(from, 968))
                {
                    AddButton(143, 76, 2242, 2242, 968, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Sonolência</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Gera um desejo incontrolável de dormir no alvo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Somnolence</I><BR>Skill: 30<BR>Mana: 30<BR>Eficiência: 60%<BR>Reagentes: Nightshade, Spiders' Silk, Black Pearl.</BASEFONT></BODY>", (bool)false, (bool)false);
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

            if (info.ButtonID < 950 && info.ButtonID > 0)
            {
                from.SendSound(0x55);
                int page = info.ButtonID;
                if (page < 1) { page = 20; }
                if (page > 20) { page = 1; }
                from.SendGump(new ClerigoDosMortosSpellbookGump(from, m_Book, page));
            }
            else if (info.ButtonID > 949)
            {
                switch (info.ButtonID)
                {
                    case 950:
                        new AbrirFeridasSpell(from, null).Cast();
                        break;
                    case 951:
                        new AlimentoDaMorteSpell(from, null).Cast();
                        break;
                    case 952:
                        new CampoVenenosoSpell(from, null).Cast();
                        break;
                    case 953:
                        new DrenarAgilidadeSpell(from, null).Cast();
                        break;
                    case 954:
                        new DrenarEssenciaSpell(from, null).Cast();
                        break;
                    case 955:
                        new DrenarForcaSpell(from, null).Cast();
                        break;
                    case 956:
                        new DrenarInteligenciaSpell(from, null).Cast();
                        break;
                    case 957:
                        new DrenarManaSpell(from, null).Cast();
                        break;
                    case 958:
                        new DrenarQuintessenciaSpell(from, null).Cast();
                        break;
                    case 959:
                        new EntorpecerSpell(from, null).Cast();
                        break;
                    case 960:
                        new EnvenenarMenteSpell(from, null).Cast();
                        break;
                    case 961:
                        new EnvenenarSpell(from, null).Cast();
                        break;
                    case 962:
                        new ErguerCadaverSpell(from, null).Cast();
                        break;
                    case 963:
                        new EscurecerVistaSpell(from, null).Cast();
                        break;
                    case 964:
                        new FogoDaMorteSpell(from, null).Cast();
                        break;
                    case 965:
                        new PactoDeSangueSpell(from, null).Cast();
                        break;
                    case 966:
                        new ProtecaoDasTrevasSpell(from, null).Cast();
                        break;
                    case 967:
                        new RitualLichSpell(from, null).Cast();
                        break;
                    case 968:
                        new SonolenciaSpell(from, null).Cast();
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
                from.SendGump(new ClerigoDosMortosSpellbookGump(from, m_Book, 1));
            }
        }
    }
}

