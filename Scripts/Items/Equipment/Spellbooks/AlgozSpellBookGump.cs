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
            AddImage(41, 42, 11008);

            int PriorPage = page - 1;
            if (PriorPage < 1) { PriorPage = 19; }
            int NextPage = page + 1;

            AddButton(91, 50, 2235, 2235, PriorPage, GumpButtonType.Reply, 0);
            AddButton(362, 50, 2236, 2236, NextPage, GumpButtonType.Reply, 0);

            AddHtml(91, 52, 153, 31, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>Algoz</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

            if (page == 1)
            {
                int SpellsInBook = 18;
                int SafetyCatch = 0;
                int SpellsListed = 69;
                string SpellName = "";

                int nHTMLx = 111;
                int nHTMLy = 81;

                int nBUTTONx = 94;
                int nBUTTONy = 82;

                while (SpellsInBook > 0)
                {
                    SpellsListed++;
                    SafetyCatch++;

                    if (this.HasSpell(from, SpellsListed))
                    {
                        SpellsInBook--;

                        switch (SpellsListed)
                        {
                            case 70:
                                SpellName = "Embasbacar";
                                break;
                            case 71:
                                SpellName = "Embasbacar";
                                break;
                            case 72:
                                SpellName = "Embasbacar";
                                break;
                            case 73:
                                SpellName = "Embasbacar";
                                break;
                            case 74:
                                SpellName = "Embasbacar";
                                break;
                            case 75:
                                SpellName = "Embasbacar";
                                break;
                            case 76:
                                SpellName = "Embasbacar";
                                break;
                            case 77:
                                SpellName = "Embasbacar";
                                break;
                            case 78:
                                SpellName = "Embasbacar";
                                break;
                            case 79:
                                SpellName = "Embasbacar";
                                break;
                            case 80:
                                SpellName = "Embasbacar";
                                break;
                            case 81:
                                SpellName = "Embasbacar";
                                break;
                            case 82:
                                SpellName = "Embasbacar";
                                break;
                            case 83:
                                SpellName = "Embasbacar";
                                break;
                            case 84:
                                SpellName = "Embasbacar";
                                break;
                            case 85:
                                SpellName = "Embasbacar";
                                break;
                            case 86:
                                SpellName = "Embasbacar";
                                break;
                            case 87:
                                SpellName = "Embasbacar";
                                break;
                        }

                        AddHtml(nHTMLx, nHTMLy, 182, 26, @"<BODY><BASEFONT Color=#111111><BIG>" + SpellName + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                        AddButton(nBUTTONx, nBUTTONy, 30008, 30008, SpellsListed, GumpButtonType.Reply, 0);

                        nHTMLy = nHTMLy + 30;
                        if (SpellsInBook == 7) { nHTMLx = 382; nHTMLy = 108; }

                        nBUTTONy = nBUTTONy + 30;
                        if (SpellsInBook == 7) { nBUTTONx = 360; nBUTTONy = 112; }
                    }

                    if (SafetyCatch > 14) { SpellsInBook = 0; }
                }
            }

            else if (page == 2)
            {
                if (this.HasSpell(from, 70))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 3)
            {
                if (this.HasSpell(from, 71))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 4)
            {
                if (this.HasSpell(from, 72))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 5)
            {
                if (this.HasSpell(from, 73))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 6)
            {
                if (this.HasSpell(from, 74))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 7)
            {
                if (this.HasSpell(from, 75))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 8)
            {
                if (this.HasSpell(from, 76))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 9)
            {
                if (this.HasSpell(from, 77))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 10)
            {
                if (this.HasSpell(from, 78))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 11)
            {
                if (this.HasSpell(from, 79))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 12)
            {
                if (this.HasSpell(from, 80))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 13)
            {
                if (this.HasSpell(from, 81))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 14)
            {
                if (this.HasSpell(from, 82))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 15)
            {
                if (this.HasSpell(from, 83))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }

            else if (page == 16)
            {
                if (this.HasSpell(from, 84))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 17)
            {
                if (this.HasSpell(from, 85))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 18)
            {
                if (this.HasSpell(from, 86))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 19)
            {
                if (this.HasSpell(from, 87))
                {
                    AddButton(143, 82, 2242, 2242, 70, GumpButtonType.Reply, 0);
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Embasbacar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 150, 145, 80, @"<BODY><BASEFONT Color=#111111><BIG>  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 150, 160, @"<BODY><BASEFONT Color=#111111><BIG>Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Ginseng, Nightshade.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 130, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
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
                if (info.ButtonID == 70) { new EmbasbacarSpell(from, null).Cast(); }
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
