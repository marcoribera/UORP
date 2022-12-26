using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Spells.Elementarista;
using Server.Prompts;

namespace Server.Gumps
{ //TODO: Achar o fundo certo do GUMP e ajustar a aparência
    public class ElementaristaSpellbookGump : Gump
    {
        private ElementaristaSpellbook m_Book;

        public bool HasSpell(Mobile from, int spellID)
        {
            return (m_Book.HasSpell(spellID));
        }

        public ElementaristaSpellbookGump(Mobile from, ElementaristaSpellbook book, int page) : base(100, 100)
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

            AddHtml(91, 52, 153, 31, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>Elementarista</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

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
                            case 620:
                                SpellName = "Ataque Congelante";
                                break;
                            case 621:
                                SpellName = "Avalanche";
                                break;
                            case 622:
                                SpellName = "Bola de Gelo";
                                break;
                            case 623:
                                SpellName = "Campo Gélido";
                                break;
                            case 624:
                                SpellName = "Tempestade Congelante";
                                break;
                           case 625:
                                SpellName = "Congelar";
                                break;
                            case 626:
                                SpellName = "Morte Massiva";
                                break;
                            case 627:
                                SpellName = "Nuvem Gasosa";
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
                if (this.HasSpell(from, 620))
                {
                    AddButton(143, 76, 2242, 2242, 620, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Ataque Congelante</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Danifica um inimigo com uma coluna de cristais de gelo .</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Frigore Impetum</I><BR>Skill: 67<BR>Mana: 19<BR>Eficiência: 100%<BR>Reagentes: Nox Crystal, Pena e Tinteiro.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 3)
            {
                if (this.HasSpell(from, 621))
                {
                    AddButton(143, 76, 2242, 2242, 621, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Avalanche</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Causa dano aos inimigos próximos com queda de gelo e neve que causam dano. </BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Lapsus Glaciem</I><BR>Skill: 70<BR>Mana: 42<BR>Eficiência: 100%<BR>Reagentes: Vela, Bat Wing.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 4)
            {
                if (this.HasSpell(from, 622))
                {
                    AddButton(143, 76, 2242, 2242, 622, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Bola de Gelo</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Joga uma bola de neve mágica em direção ao seu inimigo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Glacies Esfera</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 100%<BR>Reagentes: Vela, Incenso.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
                 
            else if (page == 5)
            {
                if (this.HasSpell(from, 623))
                {
                    AddButton(143, 76, 2242, 2242, 623, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Campo Gélido</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Cria uma parede de gelo que pode causar.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Psychicae Fracor</I><BR>Skill: 30<BR>Mana: 9<BR>Eficiência: 100%<BR>Reagentes: Bloodmoss, Grave Dust.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 6)
            {
                if (this.HasSpell(from, 624))
                {
                    AddButton(143, 76, 2242, 2242, 624, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Tempestade Congelante</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Cria uma tempestade de gelo ao redor do inimigo, .</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Torpore Tempestate</I><BR>Skill: 55<BR>Mana: 28<BR>Eficiência: 100%<BR>Reagentes: Incenso, Daemon Blood.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
           
            else if (page == 7)
            {
                if (this.HasSpell(from, 625))
                {
                    AddButton(143, 76, 2242, 2242, 625, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Congelar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>Joga um pingente de gelo mágico em seus inimigos.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Frigidus</I><BR>Skill: 15<BR>Mana: 4<BR>Eficiência: 100%<BR>Reagentes: Vela, Sulfurous Ash.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 8)
            {
                if (this.HasSpell(from, 626))
                {
                    AddButton(143, 76, 2242, 2242, 626, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Morte Massiva</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Causa dano a inimigos próximos com magias mortais invocadas do vazio.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Magna Mors</I><BR>Skill: 90<BR>Mana: 63<BR>Eficiência: 60%<BR>Reagentes: Dragon Blood,Pena e Tinteiro.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 9)
            {
                if (this.HasSpell(from, 627))
                {
                    AddButton(143, 76, 2242, 2242, 627, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Nuvem Gasosa</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Cria uma nuvem gasosa de névoa venenosa que consumirá os inimigos próximos.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Rursus Directum</I><BR>Skill: 45<BR>Mana: 13<BR>Eficiência: 60%<BR>Reagentes: Nox Crystal, Vela.</BASEFONT></BODY>", (bool)false, (bool)false);
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

            if (info.ButtonID < 620 && info.ButtonID > 0)
            {
                from.SendSound(0x55);
                int page = info.ButtonID;
                if (page < 1) { page = 9; }
                if (page > 9) { page = 1; }
                from.SendGump(new ElementaristaSpellbookGump(from, m_Book, page));
            }
            else if (info.ButtonID > 620)
            {
                switch (info.ButtonID)
                {
                    case 620:
                        new AtaqueCongelanteSpell(from, null).Cast();
                        break;
                    case 621:
                        new AvalancheSpell(from, null).Cast();
                        break;
                    case 622:
                        new BolaDeGeloSpell(from, null).Cast();
                        break;
                    case 623:
                        new CampoGelidoSpell(from, null).Cast();
                        break;
                    case 624:
                        new TempestadeCongelanteSpell(from, null).Cast();
                        break;
                    case 625:
                        new CongelarSpell(from, null).Cast();
                        break;
                    case 626:
                        new MorteMassivaSpell(from, null).Cast();
                        break;
                    case 627:
                        new NuvemGasosaSpell(from, null).Cast();
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
                from.SendGump(new ElementaristaSpellbookGump(from, m_Book, 1));
            }
        }
    }
}
