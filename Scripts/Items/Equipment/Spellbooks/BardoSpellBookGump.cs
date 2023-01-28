using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Spells;
using System.Collections.Generic;
using Server.Prompts;
using Server.Spells.Bardo;
using Server.Targeting;

namespace Server.Gumps
{ //TODO: Achar o fundo certo do GUMP e ajustar a aparência
    public class BardoSpellbookGump : Gump
    {
        private BardoSpellbook m_Book;

        private static Dictionary<int, string[]> PrimeiroCirculo = new Dictionary<int, string[]>() //Detalhes das magias de primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {284, new string[5]{ "Som de Festa", "2242", "2242", "  Enche uma garrafa com uma bebida alcoolica simples.", "Mantra: <I></I><BR>Skill: 10<BR>Mana: 4<BR>Atalho: .m 284<BR><BR><B>Reagentes:</B><BR>  Ginseng<BR>  Garlic, Mandrake Root." }},
            {280, new string[5]{ "Som da Inteligencia", "2242", "2242", " A música deixa seu alvo mais perspicaz.", "Mantra: <I></I><BR>Skill: 10<BR>Mana: 4<BR>Atalho: .m 280<BR><BR><B>Reagentes:</B><BR> " }},
            {277, new string[5]{ "Som da Burrada", "2242", "2242", "  A música deixa seu alvo embasbacado.", "Mantra: <I></I><BR>Skill: 10<BR>Mana: 4<BR>Atalho: .m 277<BR><BR><B>Reagentes:</B><BR>  " }},
            {279, new string[5]{ "Som da Fraqueza", "2242", "2242", " A música deixa seu alvo enfraquecido.", "Mantra: <I></I><BR>Skill: 10<BR>Mana: 4<BR>Atalho: .m 279<BR><BR><B>Reagentes:</B><BR> " }},
            {271, new string[5]{ "Pote de Cobras", "2242", "2242", "  Cria um pote com cobras agressivas.", "Mantra: <I></I><BR>Skill: 10<BR>Mana: 4<BR>Atalho: .m 271<BR><BR><B>Reagentes:</B><BR>  Mandrake Root <BR>  Nightshade." }},
            {264, new string[5]{ "Coelho no Chapéu", "2242", "2242", "  Tira um coelho mágico do chapeu.", "Mantra: <I></I><BR>Skill: 10<BR>Mana: 4<BR>Atalho: .m 264<BR><BR><B>Reagentes:</B><BR>  Sulfurous Ash<BR>  Nightshade." }},

        };
        private static Dictionary<int, string[]> SegundoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de segundo circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {260, new string[5]{ "Anular Bençãos", "2242", "2242", "A Música faz o alvo perder o foco.", "Mantra: <I></I><BR>Skill: 20<BR>Mana: 6<BR>Atalho: .m 260<BR><BR><B>Reagentes:</B><BR>  " } },
            {276, new string[5]{ "Som da Agilidade", "2242", "2242", " A música deixa seu alvo mais ágil.", "Mantra: <I></I><BR>Skill: 20<BR>Mana: 6<BR>Atalho: .m 276<BR><BR><B>Reagentes:</B><BR>  " } },
            {281, new string[5]{ "Som da Lerdeza", "2242", "2242", " A música deixa seu alvo mais lerdo.", "Mantra: <I></I><BR>Skill: 20<BR>Mana: 6<BR>Atalho: .m 281<BR><BR><B>Reagentes:</B><BR>  " } },
            {266, new string[5]{ "Garrafa de Agua", "2242", "2242", "  Joga um jato de agua no rosto do alvo.", "Mantra: <I></I><BR>Skill: 20<BR>Mana: 6<BR>Atalho: .m 266<BR><BR><B>Reagentes:</B><BR>  Fertile Dirt</B><BR>  Garlic." } },
            {268, new string[5]{ "Insultos", "2242", "2242", "  Faz diversos insultos ao alvo.", "Mantra: <I></I><BR>Skill: 20<BR>Mana: 6<BR>Atalho: .m 268<BR><BR><B>Reagentes:</B><BR>  Black Pearl </B><BR>  Spiders' Silk." } },
        };
        private static Dictionary<int, string[]> TerceiroCirculo = new Dictionary<int, string[]>() //Detalhes das magias de terceiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {278, new string[5]{ "Som da Força", "2242", "2242", "  A música deixa seu alvo mais forteA música deixa seu alvo mais forte.", "Mantra: <I></I><BR>Skill: 30<BR>Mana: 9<BR>Atalho: .m 278<BR><BR><B>Reagentes:</B><BR>  " }},
            {270, new string[5]{ "Poder da Flor", "2242", "2242", "  Uma flor cheirosa e perigosa é criada.", "Mantra: <I></I><BR>Skill: 30<BR>Mana: 9<BR>Atalho: .m 270<BR><BR><B>Reagentes:</B><BR>  Ginseng<BR>  Nightshade." }},
            {267, new string[5]{ "Hilário", "2242", "2242", " Faz o alvo ficar descontrolado.", "Mantra: <I></I><BR>Skill: 30<BR>Mana: 9<BR>Atalho: .m 287<BR><BR><B>Reagentes:</B><BR> Blood Moss <BR>  Spiders' Silk." }},
            {272, new string[5]{ "Presentes Surpesa", "2242", "2242", "  cria um presente surpresa.", "Mantra: <I></I><BR>Skill: 30<BR>Mana: 9<BR>Atalho: .m 272<BR><BR><B>Reagentes:</B><BR>  Ginseng<BR>  Spiders' Silk." }},
        };

        private static Dictionary<int, string[]> QuartoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de quarto circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {265, new string[5]{ "Encantar Criatura", "2242", "2242", "  Encanta um alvo como se fosse um animal de estimação.", "Mantra: <I></I><BR>Skill: 40<BR>Mana: 13<BR>Atalho: .m 265<BR><BR><B>Reagentes:</B><BR>  Mandrake Root</B><BR>  Garlic, Spiders' Silk." } },
            {262, new string[5]{ "Balões Explosivos", "2242", "2242", "  O medo deixa seu oponente enfraquecido.", "Mantra: <I></I><BR>Skill: 40<BR>Mana: 13<BR>Atalho: .m 262<BR><BR><B>Reagentes:</B><BR>  Sulfurous Ash </B><BR>  Spiders' Silk, Ginseng." } },
        };
            private static Dictionary<int, string[]> QuintoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de quinto circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {282, new string[5]{ "Som da Melhoria", "2242", "2242", " A música melhora todos os atributos de seus alvos.", "Mantra: <I></I><BR>Skill: 50<BR>Mana: 19<BR>Atalho: .m 282<BR><BR><B>Reagentes:</B><BR>  Garlic<BR>  " }},
            {287, new string[5]{ "Som Fatigante", "2242", "2242", " A música debilita seus oponentes em varios aspectos.", "Mantra: <I></I><BR>Skill: 50<BR>Mana: 19<BR>Atalho: .m 287<BR><BR><B>Reagentes:</B><BR>  Garlic<BR>  " }},
            {273, new string[5]{ "Saltando por aí", "2242", "2242", "  Possibilita que o bardo se esconda.", "Mantra: <I></I><BR>Skill: 50<BR>Mana: 19<BR>Atalho: .m 273<BR><BR><B>Reagentes:</B><BR>  Ginseng<BR>  Black Pearl, Blood Moss." }},
            {269, new string[5]{ "Palhacos", "2242", "2242", "  Chama alguns palhaços aliados .", "Mantra: <I></I><BR>Skill: 50<BR>Mana: 19<BR>Atalho: .m 269<BR><BR><B>Reagentes:</B><BR>  Black Pearl<BR>  Garlic, Blood Moss." }},
           

        };
        private static Dictionary<int, string[]> SextoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de sexto circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {285, new string[5]{ "Som do Foco", "2242", "2242", " O alvo fica mais resistente a danos mágicos, evitando distrações.", "Mantra: <I></I><BR>Skill: 60<BR>Mana: 28<BR>Atalho: .m 285<BR><BR><B>Reagentes:</B><BR>  " } },
            {286, new string[5]{ "Som Fascinante", "2242", "2242", " O som enfeitiça o alvo.", "Mantra: <I></I><BR>Skill: 60<BR>Mana: 28<BR>Atalho: .m 286<BR><BR><B>Reagentes:</B><BR>  " } },
        };
        private static Dictionary<int, string[]> SetimoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de setimo circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {283, new string[5]{ "Som debilitante ", "2242", "2242", "  A música debilita seus oponentes em varios aspectos.", "Mantra: <I></I><BR>Skill: 70<BR>Mana: 42<BR>Atalho: .m 283<BR><BR><B>Reagentes:</B><BR>  " }},
        };
        private static Dictionary<int, string[]> OitavoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de oitavo circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {261, new string[5]{ "Ataque sônico", "2242", "2242", "  A música provoca dores nos ouvidos dos inimigos.", "Mantra: <I></I><BR>Skill: 80<BR>Mana: 63<BR>Atalho: .m 261<BR><BR><B>Reagentes:</B><BR>  Black Pearl </B><BR>  Mandrake Root, Nightshade, Sulfurous Ash." } },
               };
        private static Dictionary<int, string[]> NonoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de nono circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {275, new string[5]{ "Som Agonizante", "2242", "2242", "  O som da música incomoda o alvo, deixando-o agonizante.", "Mantra: <I></I><BR>Skill: 80<BR>Mana: 94<BR>Atalho: .m 275<BR><BR><B>Reagentes:</B><BR>  " }},
        };
        private static Dictionary<int, string[]> DecimoCirculo = new Dictionary<int, string[]>() //Detalhes das magias de decimo circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {263, new string[5]{ "Canção de ninar", "2242", "2242", " A música deixa todos sonolentos.", "Mantra: <I></I><BR>Skill: 110<BR>Mana: 141<BR>Atalho: .m 263<BR><BR><B>Reagentes:</B><BR> " } },
             };
        private static Dictionary<int, string[]> DecimoPrimeiroCirculo = new Dictionary<int, string[]>() //Detalhes das magias de decimo primeiro circulo
        {
            // {ID da magia, Nome da magia, ID do icone da magia, ID do icone da magia pressionado, Descrição da magia, Detalhes da Magia
            {274, new string[5]{ "Musica Ardente", "2242", "2242", "  A Música provoca fogo nos arredores.", "Mantra: <I></I><BR>Skill: 110<BR>Mana: 211<BR>Atalho: .m 274<BR><BR><B>Reagentes:</B><BR> " } },
        };

        public bool HasSpell(Mobile from, int spellID)
        {
            return (m_Book.HasSpell(spellID));
        }

        public BardoSpellbookGump(Mobile from, BardoSpellbook book, int page) : base(100, 100)
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
                            AddButton(nBUTTONx+15, nBUTTONy + temp, 4033, 4033, 107 + magia.Key, GumpButtonType.Reply, 0);
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
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 107 + magia.Key, GumpButtonType.Reply, 0);
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
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 107 + magia.Key, GumpButtonType.Reply, 0);
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
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 107 + magia.Key, GumpButtonType.Reply, 0);
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
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 107 + magia.Key, GumpButtonType.Reply, 0);
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
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 107 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }
                    break;
                case 4:
                    AddButton(91, 50, 2235, 2235, 3, GumpButtonType.Reply, 0);
                    AddButton(362, 50, 2236, 2236, 5, GumpButtonType.Reply, 0);
                    AddHtml(100, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>7º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    AddHtml(250, 52, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>8º Circulo</CENTER></BASEFONT></BODY>", (bool)false, (bool)false);
                    foreach (var magia in SetimoCirculo)
                    {
                        if (this.HasSpell(from, magia.Key))
                        {
                            AddHtml(nHTMLx, nHTMLy + temp, 182, 26, @"<BODY><BASEFONT Color=#111111>" + magia.Value[0] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddButton(nBUTTONx, nBUTTONy + temp, 30008, 30009, magia.Key, GumpButtonType.Reply, 0);
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 107 + magia.Key, GumpButtonType.Reply, 0);
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
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 107 + magia.Key, GumpButtonType.Reply, 0);
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
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 107 + magia.Key, GumpButtonType.Reply, 0);
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
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 107 + magia.Key, GumpButtonType.Reply, 0);
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
                            AddButton(nBUTTONx + 15, nBUTTONy + temp, 4033, 4033, 107 + magia.Key, GumpButtonType.Reply, 0);
                            temp += 16; //representa o quão pra baixo no gump está a magia
                        }
                    }
                    break;
                default:
                    int MagiaParaDetalhar = page - 107;
                    //Console.WriteLine("Tenta detalhar a magia " + MagiaParaDetalhar);
                    if (this.HasSpell(from, MagiaParaDetalhar))
                    {
                        //Console.WriteLine("Tem a magia no livro");
                        int circulo = (int)((SpellRegistry.NewSpell(MagiaParaDetalhar, from, null) as BardoSpell).Circle);
                        //Console.WriteLine("Circulo:" + circulo);
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
                            //Console.WriteLine("Consegue detalhar a magia " + MagiaParaDetalhar);
                            AddButton(143, 76, Int16.Parse(detalhes[1]), Int16.Parse(detalhes[2]), MagiaParaDetalhar, GumpButtonType.Reply, 0);
                            AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>" + detalhes[0] + "</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                            AddHtml(93, 156, 145, 80, @"<BODY><BASEFONT Color=#111111>"+ detalhes[3] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                            AddHtml(250, 70, 145, 160, @"<BODY><BASEFONT Color=#111111>" + detalhes[4] + "</BASEFONT></BODY>", (bool)false, (bool)false);
                        }
                    }
                    else
                    {
                        AddHtml(100, 120, 132, 40, @"<BODY><BASEFONT Color=#111111><BIG><B><CENTER>Em Branco</CENTER></B></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                        AddButton(91, 50, 2235, 2235, 1, GumpButtonType.Reply, 0);
                        AddButton(362, 50, 2236, 2236, 6, GumpButtonType.Reply, 0);
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
                //Console.WriteLine("Passa pagina de circulo: " + page);
                m_Book.PaginaAtual = page;
                from.SendGump(new BardoSpellbookGump(from, m_Book, page));
            }
            else if (info.ButtonID >= m_Book.BookOffset && info.ButtonID <= (m_Book.BookOffset + m_Book.BookCount))
            {
                int paginaCirculo = 1;
                Spell magia = null;

                switch (info.ButtonID)
                {

                    case 260:
                        magia = new AnularBencaosSpell(from, null);
                        break;
                    case 261:
                        magia = new AtaqueSonicoSpell(from, null);
                        break;
                    case 262:
                        magia = new BaloesExplosivosSpell(from, null);
                        break;
                    case 263:
                        magia = new CancaoDeNinarSpell(from, null);
                        break;
                    case 264:
                        magia = new CoelhoNoChapeuSpell(from, null);
                        break;
                    case 265:
                        magia = new EncantarCriaturaSpell(from, null);
                        break;
                    case 266:
                        magia = new GarrafaDeAguaSpell(from, null);
                        break;
                    case 267:
                        magia = new HilarioSpell(from, null);
                        break;
                    case 268:
                        magia = new InsultosSpell(from, null);
                        break;
                    case 269:
                        magia = new PalhacosSpell(from, null);
                        break;
                    case 270:
                        magia = new PoderDaFlorSpell(from, null);
                        break;
                    case 271:
                        magia = new PoteDeCobrasSpell(from, null);
                        break;
                    case 272:
                        magia = new PresentesSurpresaSpell(from, null);
                        break;
                    case 273:
                        magia = new SaltandoPorAiSpell(from, null);
                        break;
                    case 274:
                        magia = new MusicaArdenteSpell(from, null);
                        break;
                    case 275:
                        magia = new SomAgonizanteSpell(from, null);
                        break;
                    case 276:
                        magia = new SomDaAgilidadeSpell(from, null);
                        break;
                    case 277:
                        magia = new SomDaBurradaSpell(from, null);
                        break;
                    case 278:
                        magia = new SomDaForcaSpell(from, null);
                        break;
                    case 279:
                        magia = new SomDaFraquezaSpell(from, null);
                        break;
                    case 280:
                        magia = new SomDaInteligenciaSpell(from, null);
                        break;
                    case 281:
                        magia = new SomDaLerdezaSpell(from, null);
                        break;
                    case 282:
                        magia = new SomDaMelhoriaSpell(from, null);
                        break;
                    case 283:
                        magia = new SomDebilitanteSpell(from, null);
                        break;
                    case 284:
                        magia = new SomDeFestaSpell(from, null);
                        break;
                    case 285:
                        magia = new SomDoFocoSpell(from, null);
                        break;
                    case 286:
                        magia = new SomFascinanteSpell(from, null);
                        break;
                    case 287:
                        magia = new SomFatiganteSpell(from, null);
                        break;
                    default:
                        break;
                }

                if (magia != null)
                {
                    //Console.WriteLine("magia.Circle = " + (int)magia.Circle);
                    paginaCirculo = 1 + ((int)(magia as BardoSpell).Circle) / 2;
                    //Console.WriteLine("Calcula o circulo da magia: " + paginaCirculo);
                    magia.Cast();
                }
                //Console.WriteLine("Casta e tenta abrir a pagina " + paginaCirculo);
                m_Book.PaginaAtual = paginaCirculo;
                from.SendGump(new BardoSpellbookGump(from, m_Book, paginaCirculo));


            }
            else if (info.ButtonID >= 107 + m_Book.BookOffset && info.ButtonID < (107 + m_Book.BookOffset + m_Book.BookCount))
            {
                //Console.WriteLine("Tenta abrir a pagina: "+ info.ButtonID+ ". ID da Magia: "+ (info.ButtonID-107));
                m_Book.PaginaAtual = info.ButtonID;
                from.SendGump(new BardoSpellbookGump(from, m_Book, m_Book.PaginaAtual));
            }
            return;
        }


/*
            private class InternalTarget : Target
        {
            private BardoSpellbook Book;

            public InternalTarget(BardoSpellbook book) : base(1, false, TargetFlags.None)
            {
                Book = book;
            }

            protected override void OnTarget(Mobile from, object target)
            {
                if (target is BaseInstrument)
                {
                    Book.Instrument = (BaseInstrument)target;
                    from.SendMessage("You set your instrument to play for these songs.");
                }
                else
                {
                    from.SendMessage("That is not an instrument you can play!");
                }
            }
        }*/
    }
   }




/*if (info.ButtonID == 99)
            {
                from.SendMessage("Click your instrument of bardic choice.");
                from.Target = new InternalTarget(m_Book);
            }
            else if (info.ButtonID > 0 && info.ButtonID < 7)
            {
                from.SendSound(0x55);
                int page = info.ButtonID;
                if (page < 1) { page = 6; }
                if (page > 6) { page = 1; }
                ////Console.WriteLine("Passa pagina de circulo: " + page);
                m_Book.PaginaAtual = page;
                from.SendGump(new BardoSpellbookGump(from, m_Book, page));
            }
            else if (m_Book.Instrument != null && !(from.InRange(m_Book.Instrument.GetWorldLocation(), 1)))
            {
                from.SendMessage("Your chosen instrument must be in your pack!");
            }
            else if (info.ButtonID >= m_Book.BookOffset && info.ButtonID <= (m_Book.BookOffset + m_Book.BookCount))
            {
                int paginaCirculo = 0;
                Spell magia = null;
                from.SendMessage("You need an instrument to play that song!");
                from.SendMessage("Select your instrument of bardic choice.");
                from.Target = new InternalTarget(m_Book);
*/
