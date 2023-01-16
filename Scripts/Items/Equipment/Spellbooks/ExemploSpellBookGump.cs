using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Spells;
using System.Collections.Generic;
using Server.Prompts;
using Server.Spells.Algoz;

namespace Server.Gumps
{ //TODO: Achar o fundo certo do GUMP e ajustar a aparência
    public class ExemploSpellbookGump : Gump
    {
        private ExemploSpellbook m_Book;

        private static Dictionary<int, string[]> PrimeiroCirculo = new Dictionary<int, string[]>() //Detalhes das magias de primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {70, new string[5]{ "1Embasbacar", "2242", "2242", "  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.", "Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR>Atalho: .m 70<BR><BR><B>Reagentes:</B><BR>  Ginseng<BR>  Nightshade." }}
        };
        private static Dictionary<int, string[]> SegundoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {71, new string[5]{ "2Intelecto do Acólito", "2242", "2242", "  Sua convicção o deixa mais perspicaz.", "Mantra: <I>Intelis Sup</I><BR>Skill: 20<BR>Mana: 6<BR><B>Reagentes:</B><BR>  Mandrake Root</B><BR>  Nightshade." } },
            {72, new string[5]{ "3Enfraquecer", "2242", "2242", "  O medo deixa seu oponente enfraquecido.", "Mantra: <I>Forcis Cort</I><BR>Skill: 20<BR>Mana: 6<BR><B>Reagentes:</B><BR>  Garlic</B><BR>  Nightshade." } }
        };
        private static Dictionary<int, string[]> TerceiroCirculo = new Dictionary<int, string[]>() //Detalhes das magias de primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {70, new string[5]{ "4Embasbacar", "2242", "2242", "  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.", "Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR><B>Reagentes:</B><BR>  Ginseng<BR>  Nightshade." }}
        };
        private static Dictionary<int, string[]> QuartoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {71, new string[5]{ "5Intelecto do Acólito", "2242", "2242", "  Sua convicção o deixa mais perspicaz.", "Mantra: <I>Intelis Sup</I><BR>Skill: 20<BR>Mana: 6<BR><B>Reagentes:</B><BR>  Mandrake Root</B><BR>  Nightshade." } },
            {72, new string[5]{ "6Enfraquecer", "2242", "2242", "  O medo deixa seu oponente enfraquecido.", "Mantra: <I>Forcis Cort</I><BR>Skill: 20<BR>Mana: 6<BR><B>Reagentes:</B><BR>  Garlic</B><BR>  Nightshade." } }
        };
        private static Dictionary<int, string[]> QuintoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {70, new string[5]{ "7Embasbacar", "2242", "2242", "  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.", "Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR><B>Reagentes:</B><BR>  Ginseng<BR>  Nightshade." }}
        };
        private static Dictionary<int, string[]> SextoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {71, new string[5]{ "8Intelecto do Acólito", "2242", "2242", "  Sua convicção o deixa mais perspicaz.", "Mantra: <I>Intelis Sup</I><BR>Skill: 20<BR>Mana: 6<BR><B>Reagentes:</B><BR>  Mandrake Root</B><BR>  Nightshade." } },
            {72, new string[5]{ "9Enfraquecer", "2242", "2242", "  O medo deixa seu oponente enfraquecido.", "Mantra: <I>Forcis Cort</I><BR>Skill: 20<BR>Mana: 6<BR><B>Reagentes:</B><BR>  Garlic</B><BR>  Nightshade." } }
        };
        private static Dictionary<int, string[]> SetimoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {70, new string[5]{ "10Embasbacar", "2242", "2242", "  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.", "Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR><B>Reagentes:</B><BR>  Ginseng<BR>  Nightshade." }}
        };
        private static Dictionary<int, string[]> OitavoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {71, new string[5]{ "11Intelecto do Acólito", "2242", "2242", "  Sua convicção o deixa mais perspicaz.", "Mantra: <I>Intelis Sup</I><BR>Skill: 20<BR>Mana: 6<BR><B>Reagentes:</B><BR>  Mandrake Root</B><BR>  Nightshade." } },
            {72, new string[5]{ "12Enfraquecer", "2242", "2242", "  O medo deixa seu oponente enfraquecido.", "Mantra: <I>Forcis Cort</I><BR>Skill: 20<BR>Mana: 6<BR><B>Reagentes:</B><BR>  Garlic</B><BR>  Nightshade." } }
        };
        private static Dictionary<int, string[]> NonoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {70, new string[5]{ "13Embasbacar", "2242", "2242", "  Gera um impulso de medo que atrapalha o raciocínio de seu alvo.", "Mantra: <I>Intelis Cort</I><BR>Skill: 10<BR>Mana: 4<BR><B>Reagentes:</B><BR>  Ginseng<BR>  Nightshade." }}
        };
        private static Dictionary<int, string[]> DecimoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {71, new string[5]{ "14Intelecto do Acólito", "2242", "2242", "  Sua convicção o deixa mais perspicaz.", "Mantra: <I>Intelis Sup</I><BR>Skill: 20<BR>Mana: 6<BR><B>Reagentes:</B><BR>  Mandrake Root</B><BR>  Nightshade." } },
            {72, new string[5]{ "15Enfraquecer", "2242", "2242", "  O medo deixa seu oponente enfraquecido.", "Mantra: <I>Forcis Cort</I><BR>Skill: 20<BR>Mana: 6<BR><B>Reagentes:</B><BR>  Garlic</B><BR>  Nightshade." } }
        };
        private static Dictionary<int, string[]> DecimoPrimeiroCirculo = new Dictionary<int, string[]>() //Detalhes das magias de primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {71, new string[5]{ "16Intelecto do Acólito", "2242", "2242", "  Sua convicção o deixa mais perspicaz.", "Mantra: <I>Intelis Sup</I><BR>Skill: 20<BR>Mana: 6<BR><B>Reagentes:</B><BR>  Mandrake Root</B><BR>  Nightshade." } },
            {72, new string[5]{ "17Enfraquecer", "2242", "2242", "  O medo deixa seu oponente enfraquecido.", "Mantra: <I>Forcis Cort</I><BR>Skill: 20<BR>Mana: 6<BR><B>Reagentes:</B><BR>  Garlic</B><BR>  Nightshade." } }
        };

        public bool HasSpell(Mobile from, int spellID)
        {
            return (m_Book.HasSpell(spellID));
        }

        public ExemploSpellbookGump(Mobile from, ExemploSpellbook book, int page) : base(100, 100)
        {
            m_Book = book;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddImage(41, 42, 11009);

            int PriorPage = page - 1;
            if (PriorPage < 1)
            {
                PriorPage = 6;
            }

            int NextPage = page + 1;  //TODO: Criar uma maneira de voltar pra página com as magias do circulo

            //NOME DO LIVRO

            int TotalMagias = book.BookCount; //Numero máximo de magias do livro

            //int MagiasNoLivro = book.SpellCount; //Numero atual de magias do livro

            int nHTMLx = 128;
            int nHTMLy = 81;

            int nBUTTONx = 94;
            int nBUTTONy = 82;

            int temp = 0;
            switch (page)
            {
                case 1:
                    AddButton(91, 50, 2235, 2235, 6, GumpButtonType.Reply, 0);
                    AddButton(362, 50, 2236, 2236, 2, GumpButtonType.Reply, 0);
                    AddHtml(100, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>1º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>2º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    foreach (var magia in PrimeiroCirculo)
                    {
                        if (this.HasSpell(from, magia.Key))
                        {
                            AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111>" + magia.Value[0] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30009, magia.Key, GumpButtonType.Reply, 0);
                            AddButton(nBUTTONx+15, nBUTTONy + temp, 4033, 4033, 100 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }

                    nHTMLx = 284;
                    nHTMLy = 81;
                    nBUTTONx = 250;
                    nBUTTONy = 82;
                    temp = 0;

                    foreach (var magia in SegundoCirculo)
                    {
                        if (this.HasSpell(from, magia.Key))
                        {
                            AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111>" + magia.Value[0] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30009, magia.Key, GumpButtonType.Reply, 0);
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 100 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }
                    break;
                case 2:
                    AddButton(91, 50, 2235, 2235, 1, GumpButtonType.Reply, 0);
                    AddButton(362, 50, 2236, 2236, 3, GumpButtonType.Reply, 0);
                    AddHtml(100, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>3º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>4º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    foreach (var magia in TerceiroCirculo)
                    {
                        if (this.HasSpell(from, magia.Key))
                        {
                            AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111>" + magia.Value[0] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30009, magia.Key, GumpButtonType.Reply, 0);
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 100 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }

                    nHTMLx = 284;
                    nHTMLy = 81;
                    nBUTTONx = 250;
                    nBUTTONy = 82;
                    temp = 0;

                    foreach (var magia in QuartoCirculo)
                    {
                        if (this.HasSpell(from, magia.Key))
                        {
                            AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111>" + magia.Value[0] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30009, magia.Key, GumpButtonType.Reply, 0);
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 100 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }
                    break;
                case 3:
                    AddButton(91, 50, 2235, 2235, 2, GumpButtonType.Reply, 0);
                    AddButton(362, 50, 2236, 2236, 4, GumpButtonType.Reply, 0);
                    AddHtml(100, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>5º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>6º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    foreach (var magia in QuintoCirculo)
                    {
                        if (this.HasSpell(from, magia.Key))
                        {
                            AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111>" + magia.Value[0] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30009, magia.Key, GumpButtonType.Reply, 0);
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 100 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }

                    nHTMLx = 284;
                    nHTMLy = 81;
                    nBUTTONx = 250;
                    nBUTTONy = 82;
                    temp = 0;

                    foreach (var magia in SextoCirculo)
                    {
                        if (this.HasSpell(from, magia.Key))
                        {
                            AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111>" + magia.Value[0] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30009, magia.Key, GumpButtonType.Reply, 0);
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 100 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }
                    break;
                case 4:
                    AddButton(91, 50, 2235, 2235, 3, GumpButtonType.Reply, 0);
                    AddButton(362, 50, 2236, 2236, 5, GumpButtonType.Reply, 0);
                    AddHtml(100, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>7º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>9º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    foreach (var magia in SetimoCirculo)
                    {
                        if (this.HasSpell(from, magia.Key))
                        {
                            AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111>" + magia.Value[0] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30009, magia.Key, GumpButtonType.Reply, 0);
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 100 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }

                    nHTMLx = 284;
                    nHTMLy = 81;
                    nBUTTONx = 250;
                    nBUTTONy = 82;
                    temp = 0;

                    foreach (var magia in OitavoCirculo)
                    {
                        if (this.HasSpell(from, magia.Key))
                        {
                            AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111>" + magia.Value[0] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30009, magia.Key, GumpButtonType.Reply, 0);
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 100 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }
                    break;
                case 5:
                    AddButton(91, 50, 2235, 2235, 4, GumpButtonType.Reply, 0);
                    AddButton(362, 50, 2236, 2236, 6, GumpButtonType.Reply, 0);
                    AddHtml(100, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>9º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>10º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    foreach (var magia in NonoCirculo)
                    {
                        if (this.HasSpell(from, magia.Key))
                        {
                            AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111>" + magia.Value[0] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30009, magia.Key, GumpButtonType.Reply, 0);
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 100 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }

                    nHTMLx = 284;
                    nHTMLy = 81;
                    nBUTTONx = 250;
                    nBUTTONy = 82;
                    temp = 0;

                    foreach (var magia in DecimoCirculo)
                    {
                        if (this.HasSpell(from, magia.Key))
                        {
                            AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111>" + magia.Value[0] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30009, magia.Key, GumpButtonType.Reply, 0);
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 100 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }
                    break;
                case 6:
                    AddButton(91, 50, 2235, 2235, 5, GumpButtonType.Reply, 0);
                    AddButton(362, 50, 2236, 2236, 1, GumpButtonType.Reply, 0);
                    AddHtml(100, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>11º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    //AddHtml(250, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>12º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    foreach (var magia in DecimoPrimeiroCirculo)
                    {
                        if (this.HasSpell(from, magia.Key))
                        {
                            AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111>" + magia.Value[0] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30009, magia.Key, GumpButtonType.Reply, 0);
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 100 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }
                    break;
                default:
                    int MagiaParaDetalhar = book.BookOffset + page - 7;
                    if (this.HasSpell(from, MagiaParaDetalhar))
                    {
                        int circulo = (int)(SpellRegistry.NewSpell(MagiaParaDetalhar, from, null).Circle);
                        Dictionary<int, string[]> CirculoDaMagiaDetalhada;

                        switch (circulo)
                        {
                            case 0:
                                CirculoDaMagiaDetalhada = PrimeiroCirculo;
                                AddButton(91, 50, 2235, 2235, 1, GumpButtonType.Reply, 0);
                                AddButton(362, 50, 2236, 2236, 1, GumpButtonType.Reply, 0);
                                break;
                            case 1:
                                CirculoDaMagiaDetalhada = SegundoCirculo;
                                AddButton(91, 50, 2235, 2235, 1, GumpButtonType.Reply, 0);
                                AddButton(362, 50, 2236, 2236, 1, GumpButtonType.Reply, 0);
                                break;
                            case 2:
                                CirculoDaMagiaDetalhada = TerceiroCirculo;
                                AddButton(91, 50, 2235, 2235, 2, GumpButtonType.Reply, 0);
                                AddButton(362, 50, 2236, 2236, 2, GumpButtonType.Reply, 0);
                                break;
                            case 3:
                                CirculoDaMagiaDetalhada = QuartoCirculo;
                                AddButton(91, 50, 2235, 2235, 2, GumpButtonType.Reply, 0);
                                AddButton(362, 50, 2236, 2236, 2, GumpButtonType.Reply, 0);
                                break;
                            case 4:
                                CirculoDaMagiaDetalhada = QuintoCirculo;
                                AddButton(91, 50, 2235, 2235, 3, GumpButtonType.Reply, 0);
                                AddButton(362, 50, 2236, 2236, 3, GumpButtonType.Reply, 0);
                                break;
                            case 5:
                                CirculoDaMagiaDetalhada = SextoCirculo;
                                AddButton(91, 50, 2235, 2235, 3, GumpButtonType.Reply, 0);
                                AddButton(362, 50, 2236, 2236, 3, GumpButtonType.Reply, 0);
                                break;
                            case 6:
                                CirculoDaMagiaDetalhada = SetimoCirculo;
                                AddButton(91, 50, 2235, 2235, 4, GumpButtonType.Reply, 0);
                                AddButton(362, 50, 2236, 2236, 4, GumpButtonType.Reply, 0);
                                break;
                            case 7:
                                CirculoDaMagiaDetalhada = OitavoCirculo;
                                AddButton(91, 50, 2235, 2235, 4, GumpButtonType.Reply, 0);
                                AddButton(362, 50, 2236, 2236, 4, GumpButtonType.Reply, 0);
                                break;
                            case 8:
                                CirculoDaMagiaDetalhada = NonoCirculo;
                                AddButton(91, 50, 2235, 2235, 5, GumpButtonType.Reply, 0);
                                AddButton(362, 50, 2236, 2236, 5, GumpButtonType.Reply, 0);
                                break;
                            case 9:
                                CirculoDaMagiaDetalhada = DecimoCirculo;
                                AddButton(91, 50, 2235, 2235, 5, GumpButtonType.Reply, 0);
                                AddButton(362, 50, 2236, 2236, 5, GumpButtonType.Reply, 0);
                                break;
                            case 10:
                                CirculoDaMagiaDetalhada = DecimoPrimeiroCirculo;
                                AddButton(91, 50, 2235, 2235, 6, GumpButtonType.Reply, 0);
                                AddButton(362, 50, 2236, 2236, 6, GumpButtonType.Reply, 0);
                                break;
                            default:
                                return;
                        }
                        string[] detalhes;
                        if (CirculoDaMagiaDetalhada.TryGetValue(MagiaParaDetalhar, out detalhes))
                        {
                            AddButton(143, 76, Int16.Parse(detalhes[1]), Int16.Parse(detalhes[2]), MagiaParaDetalhar, GumpButtonType.Reply, 0);
                            AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>" + detalhes[0] + "</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                            AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>"+ detalhes[3] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddHtml(250, 70, 145, 160, @"<BODY><BASEFONT Color=#111111>" + detalhes[4] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                        }
                    }
                    else
                    {
                        AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                    }
                    break;
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
            //Console.WriteLine("Botao apertado: " + info.ButtonID);
            if (info.ButtonID > 0 && info.ButtonID < 7)
            {
                from.SendSound(0x55);
                int page = info.ButtonID;
                if (page < 1) { page = 6; }
                if (page > 6) { page = 1; }
                //Console.WriteLine("Passa pagina de circulo: " + page);
                m_Book.PaginaAtual = page;
                from.SendGump(new ExemploSpellbookGump(from, m_Book, page));
            }
            else if (info.ButtonID >= m_Book.BookOffset && info.ButtonID <= (m_Book.BookOffset + m_Book.BookCount))
            {
                int paginaCirculo = 0;
                Spell magia = null;
                switch (info.ButtonID)
                {
                    case 70:
                        magia = new EmbasbacarSpell(from, null);
                        break;
                    case 71:
                        magia = new IntelectoDoAcolitoSpell(from, null);
                        break;
                    case 72:
                        magia = new EnfraquecerSpell(from, null);
                        break;
                    case 73:
                        magia = new ForcaDoAcolitoSpell(from, null);
                        break;
                    case 74:
                        magia = new AtrapalharSpell(from, null);
                        break;
                    case 75:
                        magia = new AgilidadeDoAcolitoSpell(from, null);
                        break;
                    case 76:
                        magia = new ToqueDaDorSpell(from, null);
                        break;
                    case 77:
                        magia = new BanimentoProfanoSpell(from, null);
                        break;
                    case 78:
                        magia = new ArmaVampiricaSpell(from, null);
                        break;
                    case 79:
                        magia = new BanirDemonioSpell(from, null);
                        break;
                    case 80:
                        magia = new BencaoProfanaSpell(from, null);
                        break;
                    case 81:
                        magia = new DesafioProfanoSpell(from, null);
                        break;
                    case 82:
                        magia = new EspiritoMalignoSpell(from, null);
                        break;
                    case 83:
                        magia = new FormaVampiricaSpell(from, null);
                        break;
                    case 84:
                        magia = new FuriaProfanaSpell(from, null);
                        break;
                    case 85:
                        magia = new HaloProfanoSpell(from, null);
                        break;
                    case 86:
                        magia = new PeleCadavericaSpell(from, null);
                        break;
                    case 87:
                        magia = new SaudeProfanaSpell(from, null);
                        break;
                    default:
                        break;
                }

                if (magia != null)
                {
                    paginaCirculo = 1 + ((int)magia.Circle) / 2;
                    magia.Cast();
                }
                //Console.WriteLine("Casta e tenta abrir a pagina " + paginaCirculo);
                m_Book.PaginaAtual = paginaCirculo;
                from.SendGump(new ExemploSpellbookGump(from, m_Book, paginaCirculo));

            }
            else if(info.ButtonID >= 100 + m_Book.BookOffset && info.ButtonID < (100 + m_Book.BookOffset + m_Book.BookCount))
            {
                //Console.WriteLine("Tenta abrir a pagina de detalhes "+ (info.ButtonID - 100 + 7));
                m_Book.PaginaAtual = info.ButtonID - 100 - m_Book.BookOffset + 7;
                from.SendGump(new ExemploSpellbookGump(from, m_Book, m_Book.PaginaAtual));
            }
            return;
        }
    }
}
