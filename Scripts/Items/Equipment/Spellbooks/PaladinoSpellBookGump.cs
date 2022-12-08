using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Spells.Paladino;
using Server.Prompts;

namespace Server.Gumps
{ //TODO: Achar o fundo certo do GUMP e ajustar a aparência
    public class PaladinoSpellbookGump : Gump
    {
        private PaladinoSpellbook m_Book;

        public bool HasSpell(Mobile from, int spellID)
        {
            return (m_Book.HasSpell(spellID));
        }

        public PaladinoSpellbookGump(Mobile from, PaladinoSpellbook book, int page) : base(100, 100)
        {
            m_Book = book;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddImage(41, 42, 11009);

            int PriorPage = page - 1;
            if (PriorPage < 1) { PriorPage = 18; }
            int NextPage = page + 1;

            AddButton(91, 50, 2235, 2235, PriorPage, GumpButtonType.Reply, 0);
            AddButton(362, 50, 2236, 2236, NextPage, GumpButtonType.Reply, 0);

            AddHtml(91, 52, 153, 31, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>Paladino</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

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
                            case 800:
                                SpellName = "Toque Cicatrizante";
                                break;
                            case 801:
                                SpellName = "Intelecto do Devoto";
                                break;
                            case 802:
                                SpellName = "Agilidade do Devoto";
                                break;
                            case 803:
                                SpellName = "Toque Curativo";
                                break;
                            case 804:
                                SpellName = "Banimento Sagrado";
                                break;
                            case 805:
                                SpellName = "Furia Sagrada";
                                break;
                            case 806:
                                SpellName = "Força do Devoto";
                                break;
                            case 807:
                                SpellName = "Arma Sagrada";
                                break;
                            case 808:
                                SpellName = "Benção Sagrada";
                                break;
                            case 809:
                                SpellName = "Toque Regenerador";
                                break;
                            case 810:
                                SpellName = "Desafio Sagrado";
                                break;
                            case 811:
                                SpellName = "Emanação pura";

                                break;
                            case 812:
                                SpellName = "Banimento Celestial";
                                break;
                            case 813:
                                SpellName = "Sacrificio Santo";
                                break;
                            case 814:
                                SpellName = "Saude Divina";
                                break;
                            case 815:
                                SpellName = "Halo divino";
                                break;
                            case 816:
                                SpellName = "Espirito benigno";
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
                if (this.HasSpell(from, 800))
                {
                    AddButton(143, 76, 2242, 2242, 800, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Toque cicatrizante</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  cura ferimentos superficiais com o toque.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Sanita Tact</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Garlic, Ginseng, Spiders' Silk.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 3)
            {
                if (this.HasSpell(from, 801))
                {
                    AddButton(143, 76, 2242, 2242, 801, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Intelecto do Devoto</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Sua convicção o deixa mais perspicaz.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Intelec Devot</I><BR>Skill: 20<BR>Mana: 6<BR>Eficiência: 20%<BR>Reagentes: Mandrake Root, Nightshade.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 4)
            {
                if (this.HasSpell(from, 802))
                {
                    AddButton(143, 76, 2242, 2242, 802, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Agilidade do Devoto</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Sua convicção o deixa mais ágil.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Devot Agilitas</I><BR>Skill: 20<BR>Mana: 6<BR>Eficiência: 20%<BR>Reagentes: Blood Moss, Mandrake Root.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 5)
            {
                if (this.HasSpell(from, 803))
                {
                    AddButton(143, 76, 2242, 2242, 803, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Toque Curativo</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  cura ferimentos leves com o toque.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Sanitat Tact</I><BR>Skill: 30<BR>Mana: 9<BR>Eficiência: 20%<BR>Reagentes: Garlic, Ginseng, Spiders' Silk.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
       
            else if (page == 6)
            {
                if (this.HasSpell(from, 804))
                {
                    AddButton(143, 76, 2242, 2242, 804, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Banimento Sagrado</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Tenta banir criaturas malignas.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Sanct Exili</I><BR>Skill: 40<BR>Mana: 13<BR>Eficiência: 20%<BR>Reagentes: Garlic, Ginseng, Spiders' Silk.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 7)
            {
                if (this.HasSpell(from, 805))
                {
                    AddButton(143, 76, 2242, 2242, 805, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Furia Sagrada</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Melhora seu ataque e dano as custas de sua defesa.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Sanctus Furor</I><BR>Skill: 40<BR>Mana: 13<BR>Eficiência: 20%<BR>Reagentes: Inexistente.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 8)
            {
                if (this.HasSpell(from, 806))
                {
                    AddButton(143, 76, 2242, 2242, 806, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Força do Devoto</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Sua convicção o deixa mais forte.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Devotee Forti</I><BR>Skill: 50<BR>Mana: 19<BR>Eficiência: 100%<BR>Reagentes: Blood Moss, Mandrake Root.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 9)
            {
                if (this.HasSpell(from, 807))
                {
                    AddButton(143, 76, 2242, 2242, 807, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Arma Sagrada</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Imbui sua arma com poder sagrado fazendo-a ter chance de causar todo seu dano contra a menor resistência do oponente.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Sanct Arm</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Inexistente.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 10)
            {
                if (this.HasSpell(from, 808))
                {
                    AddButton(143, 76, 2242, 2242, 808, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Benção Sagrada</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Sua convicção melhora todos os seus atributos.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Benedictio Sanc</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Mandrake Root, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 11)
            {
                if (this.HasSpell(from, 809))
                {
                    AddButton(143, 76, 2242, 2242, 809, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Toque regenarador</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Cura ferimentos moderados com o toque.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Regenerans Tact</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Garlic, Ginseng, Spiders' Silk.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 12)
            {
                if (this.HasSpell(from, 810))
                {
                    AddButton(143, 76, 2242, 2242, 810, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Desafio Sagrado</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Torna-se mais poderoso conta um tipo de inimigo especifico, mas enfraquecido contra os demais.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Sanctus Provoca</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Inexistente.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 13)
            {
                if (this.HasSpell(from, 811))
                {
                    AddButton(143, 76, 2242, 2242, 811, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Emanação pura</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Um facho de luz pura emana por todo teu corpo e atinge seu inimigo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Pura Emanat</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Inexistente.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 14)
            {
                if (this.HasSpell(from, 812))
                {
                    AddButton(143, 76, 2242, 2242, 812, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Banimento Celestial</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  As ordens dadas pelo Paladino fazem as criaturas conjuradas voltarem para seus planos de origem.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Coelestis Exsil</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Garlic, Mandrake Root, Sulfurous Ash.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }

            else if (page == 15)
            {
                if (this.HasSpell(from, 813))
                {
                    AddButton(143, 76, 2242, 2242, 813, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Sacrificio Santo</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> O Paladino realiza o sacrificio final em nome de seus aliados.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Sacrificium Sanct</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Inexistente.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 16)
            {
                if (this.HasSpell(from, 814))
                {
                    AddButton(143, 76, 2242, 2242, 814, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Saude Divina</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Sua constituicao se fortalece, reduzindo o poder dos venenos e dos danos a sua fortitude.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Divina Sanit</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Garlic, Spiders' Silk, Sulphurous Ash.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 17)
            {
                if (this.HasSpell(from, 815))
                {
                    AddButton(143, 76, 2242, 2242, 815, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Halo divino</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> A presenca divina é evocada e sentida através de uma luz quente que encobre o alvo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Divina Aureo</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Grave Dust, Nox Crystal, Pig Iron.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 18)
            {
                if (this.HasSpell(from, 816))
                {
                    AddButton(143, 76, 2242, 2242, 816, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Espirito benigno</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Um aliado do plano espiritual é invocado para auxiliar o Paladino.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Benignus Spirit</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Nox Crystal, Pig Iron.</BASEFONT></BODY>", (bool)false, (bool)false);
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

            if (info.ButtonID < 800 && info.ButtonID > 0)
            {
                from.SendSound(0x55);
                int page = info.ButtonID;
                if (page < 1) { page = 18; }
                if (page > 18) { page = 1; }
                from.SendGump(new PaladinoSpellbookGump(from, m_Book, page));
            }
            else if (info.ButtonID > 799)
            {
                switch (info.ButtonID)
                {
                    case 800:
                        new ToqueCicatrizanteSpell(from, null).Cast();
                        break;
                    case 801:
                        new IntelectoDoDevotoSpell(from, null).Cast();
                        break;
                    case 802:
                        new AgilidadeDoDevotoSpell(from, null).Cast();
                        break;
                    case 803:
                        new ToqueCurativoSpell(from, null).Cast();
                        break;
                    case 804:
                        new BanimentoSagradoSpell(from, null).Cast();
                        break;
                    case 805:
                        new FuriaSagradaSpell(from, null).Cast();
                        break;
                    case 806:
                        new ForcaDoDevotoSpell(from, null).Cast();
                        break;
                    case 807:
                        new ArmaSagradaSpell(from, null).Cast();
                        break;
                    case 808:
                        new BencaoSagradaSpell(from, null).Cast();
                        break;
                    case 809:
                        new ToqueRegeneradorSpell(from, null).Cast();
                        break;
                    case 810:
                        new DesafioSagradoSpell(from, null).Cast();
                        break;
                    case 811:
                        new EmanacaoPuraSpell(from, null).Cast();
                        break;
                    case 812:
                        new BanimentoCelestialSpell(from, null).Cast();
                        break;
                    case 813:
                        new SacrificioSantoSpell(from, null).Cast();
                        break;
                    case 814:
                        new SaudeDivinaSpell(from, null).Cast();
                        break;
                    case 815:
                        new HaloDivinoSpell(from, null).Cast();
                        break;
                    case 816:
                        new EspiritoBenignoSpell(from, null).Cast();
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
                from.SendGump(new PaladinoSpellbookGump(from, m_Book, 1));
            }
        }
    }
}
