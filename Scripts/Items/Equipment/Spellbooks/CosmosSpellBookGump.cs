using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Spells.Cosmos;
using Server.Prompts;

namespace Server.Gumps
{ //TODO: Achar o fundo certo do GUMP e ajustar a aparência
    public class CosmosSpellbookGump : Gump
    {
        private CosmosSpellbook m_Book;

        public bool HasSpell(Mobile from, int spellID)
        {
            return (m_Book.HasSpell(spellID));
        }

        public CosmosSpellbookGump(Mobile from, CosmosSpellbook book, int page) : base(100, 100)
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

            AddHtml(91, 52, 153, 31, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>Guerreiro do Cosmos</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

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
                            case 750:
                                SpellName = "Aceleração";
                                break;
                            case 751:
                                SpellName = "Defletir";
                                break;
                            case 752:
                                SpellName = "Mão Cósmica";
                                break;
                            case 753:
                                SpellName = "Olho da Mente";
                                break;
                            case 754:
                                SpellName = "Miragem";
                                break;
                            case 755:
                                SpellName = "Aura Psiquica";
                                break;
                            case 756:
                                SpellName = "Replicar";
                                break;
                            case 757:
                                SpellName = "Toque Calmante";
                                break;
                            case 758:
                                SpellName = "Campo de Êxtase";
                                break;
                            case 759:
                                SpellName = "Arremesso";
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
                if (this.HasSpell(from, 750))
                {
                    AddButton(143, 76, 2242, 2242, 750, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Aceleração</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> A sua conexão com o cosmos aumenta sua velocidade de movimentação.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Celeritas</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Blood Moss, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 3)
            {
                if (this.HasSpell(from, 751))
                {
                    AddButton(143, 76, 2242, 2242, 751, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Defletir</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> A sua conexão com o cosmos permite você desviar de feitiços magicos prejudiciais e lançar ele de volta ao seus inimigos.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Deflectere</I><BR>Skill: 20<BR>Mana: 6<BR>Eficiência: 20%<BR>Reagentes: Blood Moss, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 4)
            {
                if (this.HasSpell(from, 752))
                {
                    AddButton(143, 76, 2242, 2242, 752, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Mão Cósmica</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Permite que o guerreiro cosmico use ou mova um objeto que esteja fora de alcance. Também pode ser usado para ativar algumas Armadilhas de Baú de uma distância segura.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Manus Logos</I><BR>Skill: 20<BR>Mana: 6<BR>Eficiência: 20%<BR>Reagentes: Blood Moss, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 5)
            {
                if (this.HasSpell(from, 753))
                {
                    AddButton(143, 76, 2242, 2242, 753, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Olho da Mente</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Permite que o Guerreiro Cosmico se concentre e veja o que não pode ser visto. Ele pode perceber algo escondido ou uma armadilha escondida."".</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Mentalis Oculos</I><BR>Skill: 30<BR>Mana: 9<BR>Eficiência: 20%<BR>Reagentes: Blood Moss, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
       
            else if (page == 6)
            {
                if (this.HasSpell(from, 754))
                {
                    AddButton(143, 76, 2242, 2242, 754, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Miragem</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Cria uma copia exata do guerreiro do cosmos com energia física.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Fictus Imago</I><BR>Skill: 40<BR>Mana: 13<BR>Eficiência: 20%<BR>Reagentes: Blood Moss, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 7)
            {
                if (this.HasSpell(from, 755))
                {
                    AddButton(143, 76, 2242, 2242, 755, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Aura Psiquica</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> O cosmos protege seu corpo e mente, entretanto o deixa vulneravel a outros elementos..</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Psychica  Aureola</I><BR>Skill: 40<BR>Mana: 13<BR>Eficiência: 20%<BR>Reagentes: Blood Moss, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 8)
            {
                if (this.HasSpell(from, 756))
                {
                    AddButton(143, 76, 2242, 2242, 756, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Replicar</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> Cria um cristal com a essencia do mago.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Replicare</I><BR>Skill: 50<BR>Mana: 19<BR>Eficiência: 100%<BR>Reagentes: Blood Moss, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 9)
            {
                if (this.HasSpell(from, 757))
                {
                    AddButton(143, 76, 2242, 2242, 757, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Toque Calmante</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> A sua conexão com o comos regenera o corpo ferido.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Tactus Quietantis</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Blood Moss, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 10)
            {
                if (this.HasSpell(from, 758))
                {
                    AddButton(143, 76, 2242, 2242, 758, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Campo de Êxtase</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>  Um Guerreiro do Cosmos pode criar um campo ao redor de si que o colocará em êxtase por um período de tempo, onde ninguém poderá realizar nenhuma ação por um curto período de tempo.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Campus Immobilio</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Blood Moss, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
                }
                else
                {
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                }
            }
            else if (page == 11)
            {
                if (this.HasSpell(from, 759))
                {
                    AddButton(143, 76, 2242, 2242, 759, GumpButtonType.Reply, 0);
                    AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Arremesso</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111> A energia do cosmos é canalizada na sua arma e ela é arremessada na direção do seu alvo, voltando para a mão do guerreiro.</BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 82, 145, 160, @"<BODY><BASEFONT Color=#111111>Mantra: <I>Remissum</I><BR>Skill: 10<BR>Mana: 4<BR>Eficiência: 20%<BR>Reagentes: Blood Moss, Garlic.</BASEFONT></BODY>", (bool)false, (bool)false);
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

            if (info.ButtonID < 750 && info.ButtonID > 0)
            {
                from.SendSound(0x55);
                int page = info.ButtonID;
                if (page < 1) { page = 11; }
                if (page > 11) { page = 1; }
                from.SendGump(new CosmosSpellbookGump(from, m_Book, page));
            }
            else if (info.ButtonID > 749)
            {
                switch (info.ButtonID)
                {
                    case 750:
                        new AceleracaoSpell(from, null).Cast();
                        break;
                    case 751:
                        new DefletirSpell(from, null).Cast();
                        break;
                    case 752:
                        new MaoCosmicaSpell(from, null).Cast();
                        break;
                    case 753:
                        new OlhoDaMenteSpell(from, null).Cast();
                        break;
                    case 754:
                        new MiragemSpell(from, null).Cast();
                        break;
                    case 755:
                        new AuraPsiquicaSpell(from, null).Cast();
                        break;
                    case 756:
                        new ReplicarSpell(from, null).Cast();
                        break;
                    case 757:
                        new ToqueCalmanteSpell(from, null).Cast();
                        break;
                    case 758:
                        new CampoDeExtaseSpell(from, null).Cast();
                        break;
                    case 759:
                        new ArremessoSpell(from, null).Cast();
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
                from.SendGump(new CosmosSpellbookGump(from, m_Book, 1));
            }
        }
    }
}
