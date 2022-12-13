using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Spells.Algoz;
using Server.Prompts;

namespace Server.Gumps
{ //TODO: Achar o fundo certo do GUMP e ajustar a aparência
    public class AlgozSpellbookGump : Gump
    {
        private AlgozSpellbook m_Book;

        public bool HasSpell(Mobile from, int spellID)
        {
            return (m_Book.HasSpell(spellID));
        }

        public AlgozSpellbookGump(Mobile from, AlgozSpellbook book, int page) : base(100, 100)
        {
            m_Book = book;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddImage(41, 42, 11009);

            int PriorPage = page - 1;
            if (PriorPage < 1) { PriorPage = 19; }
            int NextPage = page + 1;

            AddButton(91, 50, 2235, 2235, PriorPage, GumpButtonType.Reply, 0);
            AddButton(362, 50, 2236, 2236, NextPage, GumpButtonType.Reply, 0);

            AddHtml(91, 52, 153, 31, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>Algoz</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

            if (page == 1)
            {
                int TotalMagias = book.BookCount; //Numero máximo de magias do livro

                //int MagiasNoLivro = book.SpellCount; //Numero atual de magias do livro

                int nHTMLx = 111;
                int nHTMLy = 81;

                int nBUTTONx = 94;
                int nBUTTONy = 82;

                int MagiaInicialID = book.BookOffset;  //ID da primeira magia do livro
                int AcabaContagem = MagiaInicialID + TotalMagias ;  //ID da ultima magia do livro
                int UltimoPaginaUm = MagiaInicialID + 8; //Posição da magia do meio da livro
                int temp = 0;
                string SpellName = "";

                for (int i = MagiaInicialID; i < AcabaContagem; i++)
                {
                    if (this.HasSpell(from, i))
                    {
                        switch (i)
                        {
                            case 70:
                                SpellName = "Embasbacar";
                                break;
                            case 71:
                                SpellName = "Intelecto do Acólito";
                                break;
                            case 72:
                                SpellName = "Enfraquecer";
                                break;
                            case 73:
                                SpellName = "Força do Acólito";
                                break;
                            case 74:
                                SpellName = "Atrapalhar";
                                break;
                            case 75:
                                SpellName = "Agilidade do Acólito";
                                break;
                            case 76:
                                SpellName = "Toque da Dor";
                                break;
                            case 77:
                                SpellName = "Banimento Profano";
                                break;
                            case 78:
                                SpellName = "Arma Vampirica";
                                break;
                            case 79:
                                SpellName = "Banimento Demoniaco";
                                break;
                            case 80:
                                SpellName = "Benção Profana";
                                break;
                            case 81:
                                SpellName = "Desafio Profano";
                                break;
                            case 82:
                                SpellName = "Espirito Maligno";
                                break;
                            case 83:
                                SpellName = "Forma Vampírica";
                                break;
                            case 84:
                                SpellName = "Fúria Profana";
                                break;
                            case 85:
                                SpellName = "Halo Profano";
                                break;
                            case 86:
                                SpellName = "Pele Cadavérica";
                                break;
                            case 87:
                                SpellName = "Saúde Profana";
                                break;

                        }
                        AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + SpellName + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                        AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30008, i, GumpButtonType.Reply, 0);
                        temp += 16; //representa o quãp pra baixo no gump está a magia
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
                if (this.HasSpell(from, 70))
                {
                    AddButton(143, 76, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 3)
            {
                if (this.HasSpell(from, 71))
                {
                    AddButton(143, 76, 2242, 2242, 71, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Intelecto do Acólito</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Sua convicção o deixa mais perspicaz.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Intelis Sup</I><BR>Skill: 20<BR>Mana: 6<BR>Eficiência: 20%<BR>Reagentes: Mandrake Root, Nightshade.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 4)
            {
                if (this.HasSpell(from, 72))
                {
                    AddButton(143, 76, 2242, 2242, 72, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Enfraquecer</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  O medo deixa seu oponente enfraquecido.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Forcis Cort</I><BR>Skill: 20<BR>Mana: 6<BR>Eficiência: 20%<BR>Reagentes: Garlic, Nightshade.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 5)
            {
                if (this.HasSpell(from, 73))
                {
                    AddButton(143, 76, 2242, 2242, 73, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Força do Acólito</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Sua convicção o deixa mais forte.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Forcis Sup</I><BR>Skill: 30<BR>Mana: 9<BR>Eficiência: 20%<BR>Reagentes: Blood Moss, Mandrake Root.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 6)
            {
                if (this.HasSpell(from, 74))
                {
                    AddButton(143, 76, 2242, 2242, 74, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Atrapalhar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  O medo deixa seu oponente mais lerdo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Agilis Cort</I><BR>Skill: 30<BR>Mana: 9<BR>Eficiência: 20%<BR>Reagentes: Bloodmoss, Nightshade.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 7)
            {
                if (this.HasSpell(from, 75))
                {
                    AddButton(143, 76, 2242, 2242, 75, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Agilidade do Acólito</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Sua convicção o deixa mais ágil.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Agilis Sup</I><BR>Skill: 40<BR>Mana: 13<BR>Eficiência: 20%<BR>Reagentes: Bloodmoss, Mandrake Root.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 8)
            {
                if (this.HasSpell(from, 76))
                {
                    AddButton(143, 76, 2242, 2242, 76, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Toque da Dor</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Inimigo ao alcance de toque sofre dano direto.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Levi Dolore</I><BR>Skill: 40<BR>Mana: 13<BR>Eficiência: 20%<BR>Reagentes: Nightshade, SpidersSilk.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 9)
            {
                if (this.HasSpell(from, 77))
                {
                    AddButton(143, 76, 2242, 2242, 77, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Banimento Profano</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Expulsão contra criaturas benignas.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Expelle Bonum</I><BR>Skill: 50<BR>Mana: 19<BR>Eficiência: 100%<BR>Reagentes: Ginseng, Nightshade.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 10)
            {
                if (this.HasSpell(from, 78))
                {
                    AddButton(143, 76, 2242, 2242, 78, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Arma Vampirica</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Adiciona o efeito de drenar vida na arma.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Sanguis Arma</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Pig iron.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 11)
            {
                if (this.HasSpell(from, 79))
                {
                    AddButton(143, 76, 2242, 2242, 79, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Banimento Demoniaco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Bane criaturas celestiais em uma area.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Daemon Exsil</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 12)
            {
                if (this.HasSpell(from, 80))
                {
                    AddButton(143, 76, 2242, 2242, 80, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Bencão Profana</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Sua convicção melhora todos os seus atributos.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Benedic Profanus</I><BR>Skill: 70<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Mandrake Root, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 13)
            {
                if (this.HasSpell(from, 81))
                {
                    AddButton(143, 76, 2242, 2242, 81, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Desafio Profano</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Torna-se mais poderoso conta um tipo de inimigo especifico, mas enfraquecido contra os demais.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Profanus Provoca</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Grave Dust, Nox Crystal.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 14)
            {
                if (this.HasSpell(from, 82))
                {
                    AddButton(143, 76, 2242, 2242, 82, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Espirito Maligno</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Usa as forças das trevas para assumir a forma de uma poderosa assombração.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Espiritus Malus</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Nox Crystal, Pig Iron.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 15)
            {
                if (this.HasSpell(from, 83))
                {
                    AddButton(143, 76, 2242, 2242, 83, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Forma Vampirica</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Usa as forças das trevas para assumir a forma de um vampiro.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Sanguis Corpus</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Batwing, Nox Crystal, Pig Iron.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }

            else if (page == 16)
            {
                if (this.HasSpell(from, 84))
                {
                    AddButton(143, 76, 2242, 2242, 84, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Fúria Profana</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Melhora seu ataque e dano as custas de sua defesa.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Profanus Furor</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: NoxCrystal, Pig Iron.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 17)
            {
                if (this.HasSpell(from, 85))
                {
                    AddButton(143, 76, 2242, 2242, 85, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Halo Profano</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Emana energia sombria causando dano aos inimigos próximos.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Profanus Aureola</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Grave Dust, Nox Crystal, Pig Iron.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 18)
            {
                if (this.HasSpell(from, 86))
                {
                    AddButton(143, 76, 2242, 2242, 86, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Pele Cadavérica</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Resseca a pele do alvo tornando-o mais vuneravel a danos de fogo e venenoses e mais resistente a danos gélidos e físicos.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Cutis Mortum</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Batwing, Grave Dust.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 19)
            {
                if (this.HasSpell(from, 87))
                {
                    AddButton(143, 76, 2242, 2242, 87, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Saúde Profana</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Aumenta sua resistência fisica e a energia e reduz as demais resistências.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Profanus Sanit</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Garlic, Spiders' Silk, Sulphurous Ash.</BASEFONT></BODY>", (bool)false, (bool)false);
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

            if (info.ButtonID < 70 && info.ButtonID > 0)
            {
                from.SendSound(0x55);
                int page = info.ButtonID;
                if (page < 1) { page = 19; }
                if (page > 19) { page = 1; }
                from.SendGump(new AlgozSpellbookGump(from, m_Book, page));
            }
            else if (info.ButtonID > 69)
            {
                switch (info.ButtonID)
                {
                    case 70:
                        new EmbasbacarSpell(from, null).Cast();
                        break;
                    case 71:
                        new IntelectoDoAcolitoSpell(from, null).Cast();
                        break;
                    case 72:
                        new EnfraquecerSpell(from, null).Cast();
                        break;
                    case 73:
                        new ForcaDoAcolitoSpell(from, null).Cast();
                        break;
                    case 74:
                        new AtrapalharSpell(from, null).Cast();
                        break;
                    case 75:
                        new AgilidadeDoAcolitoSpell(from, null).Cast();
                        break;
                    case 76:
                        new ToqueDaDorSpell(from, null).Cast();
                        break;
                    case 77:
                        new BanimentoProfanoSpell(from, null).Cast();
                        break;
                    case 78:
                        new ArmaVampiricaSpell(from, null).Cast();
                        break;
                    case 79:
                        new BanimentoDemoniacoSpell(from, null).Cast();
                        break;
                    case 80:
                        new BencaoProfanaSpell(from, null).Cast();
                        break;
                    case 81:
                        new DesafioProfanoSpell(from, null).Cast();
                        break;
                    case 82:
                        new EspiritoMalignoSpell(from, null).Cast();
                        break;
                    case 83:
                        new FormaVampiricaSpell(from, null).Cast();
                        break;
                    case 84:
                        new FuriaProfanaSpell(from, null).Cast();
                        break;
                    case 85:
                        new HaloProfanoSpell(from, null).Cast();
                        break;
                    case 86:
                        new PeleCadavericaSpell(from, null).Cast();
                        break;
                    case 87:
                        new SaudeProfanaSpell(from, null).Cast();
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
                from.SendGump(new AlgozSpellbookGump(from, m_Book, 1));
            }
        }
    }
}
