using System; 
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;
using System.Collections.Generic;

namespace Server.Commands
{
	public class SelecionarIdioma
	{
		public static void Initialize()
		{
            CommandSystem.Register("Idioma", AccessLevel.Player, new CommandEventHandler(Idioma_OnCommand));
		}

        [Usage("Idioma [nome do idioma]")]
        [Description("Seleciona o idioma a ser utilizado pelo seu personagem.")]
        private static void Idioma_OnCommand(CommandEventArgs e)
        {
            PlayerMobile player = e.Mobile as PlayerMobile; //Personagem que chamou o comando

            if (e.ArgString == "")
            {
                IdiomasGump gump = new IdiomasGump(player);
                player.SendGump(gump);
                return;
            }
            else
            {
                try
                {
                    SpeechType idioma = (SpeechType)Enum.Parse(typeof(SpeechType), e.ArgString, true);

                    switch (idioma)
                    {
                        case SpeechType.Avlitir:
                            {
                                if (!player.KnowAvlitir)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Celirus:
                            {
                                if (!player.KnowCelirus)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Comum:
                            {
                                break;
                            }
                        case SpeechType.Drakir:
                            {
                                if (!player.KnowDrakir)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Eorin:
                            {
                                if (!player.KnowEorin)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Faeris:
                            {
                                if (!player.KnowFaeris)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Ihluv:
                            {
                                if (!player.KnowIhluv)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Infaris:
                            {
                                if (!player.KnowInfaris)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Kahlur:
                            {
                                if (!player.KnowKahlur)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Kriktik:
                            {
                                if (!player.KnowKriktik)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Krusk:
                            {
                                if (!player.KnowKrusk)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Ladvek:
                            {
                                if (!player.KnowLadvek)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Morfat:
                            {
                                if (!player.KnowMorfat)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Nanuk:
                            {
                                if (!player.KnowNanuk)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Nukan:
                            {
                                if (!player.KnowNukan)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Poolik:
                            {
                                if (!player.KnowPoolik)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Prokbum:
                            {
                                if (!player.KnowProkbum)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Shakshar:
                            {
                                if (!player.KnowShakshar)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Tahare:
                            {
                                if (!player.KnowTahare)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Taract:
                            {
                                if (!player.KnowTaract)
                                    goto default;
                                else
                                    break;
                            }
                        default:
                            {
                                player.SendMessage(String.Format("Não existe o idioma: {0}.", e.ArgString));
                                player.LanguageSpeaking = SpeechType.Comum;
                                player.SendMessage("Selecionado o idioma: Comum.");
                                player.SendMessage("Para abrir um Menu de seleção de Idioma, utilize o comando sem parâmetros.");
                                return;
                            }
                    }

                    player.LanguageSpeaking = idioma;
                    player.SendMessage(String.Format("Selecionado o idioma: {0}.", idioma.ToString()));
                    return;
                }
                catch (ArgumentException)
                {
                    player.SendMessage(String.Format("Não existe o idioma: {0}.", e.ArgString));
                    player.LanguageSpeaking = SpeechType.Comum;
                    player.SendMessage("Selecionado o idioma: Comum.");
                    player.SendMessage("Para abrir um Menu de seleção de Idioma, utilize o comando sem parâmetros.");
                    return;
                }
            }
        }

        private class IdiomasGump: Gump
        {
            private PlayerMobile player;

            public IdiomasGump(PlayerMobile player) : base(120, 50)
            {
                this.player = player;
                //Lsita os idiomas conhecidos
                List<SpeechType> IdiomasConhecidos = new List<SpeechType>();

                IdiomasConhecidos.Add(SpeechType.Comum);
                if (player.KnowAvlitir)
                    IdiomasConhecidos.Add(SpeechType.Avlitir);
                if (player.KnowCelirus)
                    IdiomasConhecidos.Add(SpeechType.Celirus);
                if (player.KnowDrakir)
                    IdiomasConhecidos.Add(SpeechType.Drakir);
                if (player.KnowEorin)
                    IdiomasConhecidos.Add(SpeechType.Eorin);
                if (player.KnowFaeris)
                    IdiomasConhecidos.Add(SpeechType.Faeris);
                if (player.KnowIhluv)
                    IdiomasConhecidos.Add(SpeechType.Ihluv);
                if (player.KnowInfaris)
                    IdiomasConhecidos.Add(SpeechType.Infaris);
                if (player.KnowKahlur)
                    IdiomasConhecidos.Add(SpeechType.Kahlur);
                if (player.KnowKriktik)
                    IdiomasConhecidos.Add(SpeechType.Kriktik);
                if (player.KnowKrusk)
                    IdiomasConhecidos.Add(SpeechType.Krusk);
                if (player.KnowLadvek)
                    IdiomasConhecidos.Add(SpeechType.Ladvek);
                if (player.KnowMorfat)
                    IdiomasConhecidos.Add(SpeechType.Morfat);
                if (player.KnowNanuk)
                    IdiomasConhecidos.Add(SpeechType.Nanuk);
                if (player.KnowNukan)
                    IdiomasConhecidos.Add(SpeechType.Nukan);
                if (player.KnowPoolik)
                    IdiomasConhecidos.Add(SpeechType.Poolik);
                if (player.KnowProkbum)
                    IdiomasConhecidos.Add(SpeechType.Prokbum);
                if (player.KnowShakshar)
                    IdiomasConhecidos.Add(SpeechType.Shakshar);
                if (player.KnowTahare)
                    IdiomasConhecidos.Add(SpeechType.Tahare);
                if (player.KnowTaract)
                    IdiomasConhecidos.Add(SpeechType.Taract);


                AddPage(0);
                AddImage(0, 0, 30236); //500x600

                int SLbaseX = 160; //Posição 'x' base do label do Idioma
                int SLbaseY = 59; //Posição 'y' base do label do Idioma
                int SBbaseX = 125; //Posição 'x' base do button do Idioma
                int SBbaseY = 50; //Posição 'y' base do button do Idioma

                int linhaCount = 0;
                int totalCount = 0;
                bool marcado = false;

                //Monta o gump com a lista de idiomas conhecidos
                foreach (SpeechType idioma in IdiomasConhecidos)
                {
                    if ((totalCount > 0) && (totalCount % 2 == 0))
                    {
                        linhaCount++;
                    }
                    marcado = (player.LanguageSpeaking == idioma);

                    AddButton(SBbaseX + (180 * (totalCount % 2)), SBbaseY + (50 * linhaCount), marcado ? 2154 : 2151, marcado ? 2154 : 2154, 100 + (int)idioma, GumpButtonType.Reply, 0);
                    AddLabel(SLbaseX + (180 * (totalCount % 2)), SLbaseY + (50 * linhaCount), 1153, idioma.ToString());
                    totalCount++;
                }
            }

            public override void OnResponse(NetState state, RelayInfo info)
            {
                //Console.WriteLine(String.Format("Botão da classe clicado. ID = {0}", info.ButtonID));
                if (info.ButtonID == 0) {
                    player.SendMessage("Mudança de Idioma cancelada.");
                    return;
                }
                else{
                    SpeechType idioma = (SpeechType)(info.ButtonID - 100);

                    switch (idioma)
                    {
                        case SpeechType.Avlitir:
                            {
                                if (!player.KnowAvlitir)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Celirus:
                            {
                                if (!player.KnowCelirus)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Comum:
                            {
                                break;
                            }
                        case SpeechType.Drakir:
                            {
                                if (!player.KnowDrakir)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Eorin:
                            {
                                if (!player.KnowEorin)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Faeris:
                            {
                                if (!player.KnowFaeris)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Ihluv:
                            {
                                if (!player.KnowIhluv)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Infaris:
                            {
                                if (!player.KnowInfaris)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Kahlur:
                            {
                                if (!player.KnowKahlur)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Kriktik:
                            {
                                if (!player.KnowKriktik)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Krusk:
                            {
                                if (!player.KnowKrusk)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Ladvek:
                            {
                                if (!player.KnowLadvek)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Morfat:
                            {
                                if (!player.KnowMorfat)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Nanuk:
                            {
                                if (!player.KnowNanuk)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Nukan:
                            {
                                if (!player.KnowNukan)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Poolik:
                            {
                                if (!player.KnowPoolik)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Prokbum:
                            {
                                if (!player.KnowProkbum)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Shakshar:
                            {
                                if (!player.KnowShakshar)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Tahare:
                            {
                                if (!player.KnowTahare)
                                    goto default;
                                else
                                    break;
                            }
                        case SpeechType.Taract:
                            {
                                if (!player.KnowTaract)
                                    goto default;
                                else
                                    break;
                            }
                        default:
                            {
                                player.SendMessage(String.Format("Você não conhece o idioma: {0}.", idioma.ToString()));
                                return;
                            }
                    }
                    player.LanguageSpeaking = idioma;
                    player.SendMessage(String.Format("Selecionado o idioma: {0}.", idioma.ToString()));
                    return;
                }
            }
        }
    }
}
