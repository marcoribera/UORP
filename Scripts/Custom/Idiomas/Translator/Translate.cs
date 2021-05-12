using System;
using System.Text;
using Server.Gumps;
using Server.Network;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Custom
{
    public class Translate
    {
        public static void Initialize()
        {
            // Register our speech handler
            EventSink.Speech += new SpeechEventHandler(EventSink_Speech);
        }

        private static void EventSink_Speech(SpeechEventArgs args)
        {
            // Needed for compatibility with other custom speech events
            if (!(args.Mobile is PlayerMobile) || (args.Speech == null) || (args.Speech == ""))
                return;

            PlayerMobile from = args.Mobile as PlayerMobile;

            // Needed to withhold access level properties 
            if ((from.AccessLevel > AccessLevel.Player) && from.Hidden)
                return;

            // If you aren't alive, you don't have vocal cords.  
            if (!from.Alive)
                return;

            // Will this speech unhide your character?
            if ((!(args.Type == MessageType.Whisper)) && (from.AccessLevel == AccessLevel.Player))
                from.RevealingAction();

            int tileLength = 5;

            switch (args.Type)
            {
                case MessageType.Yell:
                    tileLength = 20;
                    break;
                case MessageType.Regular:
                    tileLength = 10;
                    break;
                case MessageType.Whisper:
                    tileLength = 1;
                    break;
                default:
                    tileLength = 10;
                    break;
            }

            // Created for boats and any other item with HandlesOnSpeech = true
            foreach (Item junk in from.Map.GetItemsInRange(from.Location, tileLength))
            {
                if (junk.HandlesOnSpeech)
                    junk.OnSpeech(args);
            }

            // Check for items in your backpack that handle speech while speaking a noncommon language
            foreach (Item junk in from.Backpack.Items)
            {
                if (junk.HandlesOnSpeech)
                    junk.OnSpeech(args);
            }

            String mySpeech = args.Speech;
            string mySpeechNPC = args.Speech;
            String mySpeechTranslated = "";

            if (args.Type == MessageType.Emote)
            {
                from.Emote(mySpeech);
            }
            else
            {
                args.Blocked = true;
                ArrayList list = new ArrayList();

                //Gera lista de criaturas dentro do alcance da fala
                foreach (Mobile mob in from.Map.GetMobilesInRange(from.Location, tileLength))
                {
                    list.Add(mob);
                }

                if (list != null)
                {
                    //Gera apenas uma vez as versões do texto pra quem tem o Idioma e pra quem não tem

                    //Exibe intensidade da voz ao final do texto
                    if (args.Type == MessageType.Yell)
                    {
                        mySpeech = String.Format("[{0}] [Grito] {1}", from.LanguageSpeaking.ToString(), mySpeech);
                        mySpeechTranslated = String.Format("[Grito] {0}", TraduzirPara(from.LanguageSpeaking, args.Speech));
                    }
                    else if (args.Type == MessageType.Whisper)
                    {
                        mySpeech = String.Format("[{0}] [Sussurro] {1}", from.LanguageSpeaking.ToString(), mySpeech);
                        mySpeechTranslated = String.Format("[Sussurro] {0}", TraduzirPara(from.LanguageSpeaking, args.Speech));
                    }
                    else
                    {
                        mySpeech = String.Format("[{0}] {1}", from.LanguageSpeaking.ToString(), mySpeech);
                        mySpeechTranslated = TraduzirPara(from.LanguageSpeaking, args.Speech);
                    }

                    for (int j = 0; j < list.Count; j++)
                    {
                        Mobile m = list[j] as Mobile;
                        if (m.Player)
                        {
                            PlayerMobile playerm = m as PlayerMobile;

                            switch (from.LanguageSpeaking)
                            {
                                case SpeechType.Avlitir:
                                    {
                                        if (playerm.KnowAvlitir)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Celirus:
                                    {
                                        if (playerm.KnowCelirus)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Comum:
                                    {
                                        from.SayTo(playerm, mySpeech);
                                        break;
                                    }
                                case SpeechType.Drakir:
                                    {
                                        if (playerm.KnowDrakir)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Eorin:
                                    {
                                        if (playerm.KnowEorin)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Faeris:
                                    {
                                        if (playerm.KnowFaeris)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Ihluv:
                                    {
                                        if (playerm.KnowIhluv)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Infaris:
                                    {
                                        if (playerm.KnowInfaris)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Kahlur:
                                    {
                                        if (playerm.KnowKahlur)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Kriktik:
                                    {
                                        if (playerm.KnowKriktik)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Krusk:
                                    {
                                        if (playerm.KnowKrusk)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Ladvek:
                                    {
                                        if (playerm.KnowLadvek)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Morfat:
                                    {
                                        if (playerm.KnowMorfat)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Nanuk:
                                    {
                                        if (playerm.KnowNanuk)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Nukan:
                                    {
                                        if (playerm.KnowNukan)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Poolik:
                                    {
                                        if (playerm.KnowPoolik)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Prokbum:
                                    {
                                        if (playerm.KnowProkbum)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Shakshar:
                                    {
                                        if (playerm.KnowShakshar)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Tahare:
                                    {
                                        if (playerm.KnowTahare)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                case SpeechType.Taract:
                                    {
                                        if (playerm.KnowTaract)
                                        {
                                            from.SayTo(playerm, mySpeech);
                                        }
                                        else
                                        {
                                            from.SayTo(playerm, mySpeechTranslated);
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                        }
                        else if ((m is PolyGlotMobile) && m.HandlesOnSpeech(args.Mobile))
                        {

                            // This area is needed so that npcs will hear you.  NPCs can learn and use languages however you must change
                            // their base class from Mobile to PolyGlotMobile in order for this script to be active.

                            PolyGlotMobile npclistener = m as PolyGlotMobile;

                            switch (from.LanguageSpeaking)
                            {
                                case SpeechType.Avlitir:
                                    {
                                        if (npclistener.KnowAvlitir)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Celirus:
                                    {
                                        if (npclistener.KnowCelirus)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Comum:
                                    {
                                        npclistener.OnSpeech(args);
                                        break;
                                    }
                                case SpeechType.Drakir:
                                    {
                                        if (npclistener.KnowDrakir)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Eorin:
                                    {
                                        if (npclistener.KnowEorin)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Faeris:
                                    {
                                        if (npclistener.KnowFaeris)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Ihluv:
                                    {
                                        if (npclistener.KnowIhluv)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Infaris:
                                    {
                                        if (npclistener.KnowInfaris)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Kahlur:
                                    {
                                        if (npclistener.KnowKahlur)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Kriktik:
                                    {
                                        if (npclistener.KnowKriktik)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Krusk:
                                    {
                                        if (npclistener.KnowKrusk)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Ladvek:
                                    {
                                        if (npclistener.KnowLadvek)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Morfat:
                                    {
                                        if (npclistener.KnowMorfat)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Nanuk:
                                    {
                                        if (npclistener.KnowNanuk)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Nukan:
                                    {
                                        if (npclistener.KnowNukan)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Poolik:
                                    {
                                        if (npclistener.KnowPoolik)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Prokbum:
                                    {
                                        if (npclistener.KnowProkbum)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Shakshar:
                                    {
                                        if (npclistener.KnowShakshar)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Tahare:
                                    {
                                        if (npclistener.KnowTahare)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                case SpeechType.Taract:
                                    {
                                        if (npclistener.KnowTaract)
                                        {
                                            npclistener.OnSpeech(args);
                                        }
                                        else
                                        {
                                            npclistener.Emote("*Parece não entender o que foi dito*");
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }

                        }
                        else if (m is BaseCreature && m.HandlesOnSpeech(args.Mobile))
                        {
                            //Trata o envio de texto para criaturas que entendem o Idioma utilizado
                            BaseCreature creature = m as BaseCreature;
                            if (creature.IdiomaNativo == from.LanguageSpeaking)
                            {
                                creature.OnSpeech(args);
                            }
                            else
                            {
                                creature.Emote("*Parece não entender o que foi dito*");
                            }
                        }
                        else
                        {
                            // For all other mobiles
                            //args.Blocked = false;
                            if (m.HandlesOnSpeech(args.Mobile))
                                m.OnSpeech(args);
                        }

                    }
                    list.Clear();
                }
                else
                {
                    //Caso não tenha ouvintes, encerra o script
                    return;
                }
            }
        }

        public static String Vogais = "aeiouyAEIOUY";
        public static String Consoantes = "bcdfghjklmnpqrstvwxzBCDFGHJKLMNPQRSTVWXZ";

        private static List<string> SepararSilabas(String palavra)
        {
            List <string> silabas = new List<string>();
            int InicioSilabaIndex = 0;
            int ultimoCaractere = palavra.Length - 1;

            for (var i = 0; i < palavra.Length; i++)
            {
                if (Vogais.Contains(palavra.Substring(i, 1))) //Quando é uma vogal
                {
                    silabas.Add(palavra.Substring(InicioSilabaIndex, 1 + i - InicioSilabaIndex)); //Insere a silaba na lista
                    InicioSilabaIndex = i + 1; //define a posicao inicial da proxima silaba
                }
                else if (Consoantes.Contains(palavra.Substring(i, 1))) //Se é uma consoante
                {
                    if (i == ultimoCaractere) //Se o ultimo caractere for uma consoante
                    {
                        silabas.Add(palavra.Substring(InicioSilabaIndex, 1 + i - InicioSilabaIndex)); //Insere a silaba na lista
                    }
                }
                else //Se não é vogal nem consoante
                {
                    if (InicioSilabaIndex == i) //Quando os simbolos anteriores ao caractere especial ja foram inseridos
                    {
                        silabas.Add(palavra.Substring(i, 1)); //Insere caractere especial
                        InicioSilabaIndex = i + 1; //define a posicao inicial da proxima silaba
                    }
                    else if (InicioSilabaIndex < i)  //Quando os simbolos anteriores ao caractere especial não foram inseridos ainda
                    {
                        silabas.Add(palavra.Substring(InicioSilabaIndex, i - InicioSilabaIndex)); //Cria uma silaba com as consoantes anteriores
                        silabas.Add(palavra.Substring(i, 1)); //Insere caractere especial
                        InicioSilabaIndex = i + 1; //define a posicao inicial da proxima silaba
                    }
                }
            }
            return silabas;
        }

        private static String TraduzirPara(SpeechType idioma, String texto)
        {
            string[] palavras = texto.Split(' '); //Separa as sequencias de caracteres que não são espaços
            int ultimaPalavra = palavras.Length - 1;
            string resultado = "";

            for (int i=0; i < palavras.Length; i++)
            {
                var silabas = SepararSilabas(palavras[i]);
                foreach (string silaba in silabas)
                {
                    resultado += TraduzirParaKriktik(silaba);
                }
                resultado += (i == ultimaPalavra ? "" : " ");
            }
            return resultado;
        }

        private static String TraduzirParaKriktik(String silaba)
        {
            int tamanho = silaba.Length;
            string traduzido = "";
            string primeiraLetra = silaba.Substring(0, 1).ToLower();

            switch (tamanho)
            {
                case 0: //String vazio
                    break;
                case 1: //String com 1 caractere
                    switch (primeiraLetra)
                    {
                        case "a":
                        case "à":
                        case "á":
                        case "â":
                        case "ã":
                        case "ä":
                            return "í";
                        case "e":
                        case "é":
                        case "è":
                        case "ê":
                        case "ë":
                            return "î";
                        case "i":
                        case "í":
                        case "ì":
                        case "î":
                        case "ï":
                            return "é";
                        case "o":
                        case "ó":
                        case "ò":
                        case "ô":
                        case "õ":
                        case "ö":
                            return "è";
                        case "u":
                        case "ú":
                        case "ù":
                        case "û":
                        case "ü":
                            return "ù";
                        case "y":
                        case "ý":
                        case "ÿ":
                            return "ú";
                        default:
                            return silaba;
                    }
                case 2: //String com 2 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "ka";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "ke";
                            break;
                        case "d":
                            traduzido = "ki";
                            break;
                        case "f":
                            traduzido = "ko";
                            break;
                        case "g":
                            traduzido = "ku";
                            break;
                        case "h":
                            traduzido = "ká";
                            break;
                        case "j":
                            traduzido = "ké";
                            break;
                        case "k":
                            traduzido = "kí";
                            break;
                        case "l":
                            traduzido = "kó";
                            break;
                        case "m":
                            traduzido = "kú";
                            break;
                        case "n":
                            traduzido = "kà";
                            break;
                        case "p":
                            traduzido = "kè";
                            break;
                        case "q":
                            traduzido = "kì";
                            break;
                        case "r":
                            traduzido = "kò";
                            break;
                        case "s":
                            traduzido = "kù";
                            break;
                        case "t":
                            traduzido = "ík";
                            break;
                        case "v":
                            traduzido = "ìk";
                            break;
                        case "w":
                            traduzido = "ìá";
                            break;
                        case "x":
                            traduzido = "àí";
                            break;
                        case "z":
                            traduzido = "íà";
                            break;
                        default:
                            traduzido = "íì";
                            break;
                    }
                    break;
                case 3: //String com 3 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "íhk";
                            break;
                        case "c":
                            traduzido = "íkh";
                            break;
                        case "d":
                            traduzido = "íkì";
                            break;
                        case "f":
                            traduzido = "ìkí";
                            break;
                        case "g":
                            traduzido = "kìk";
                            break;
                        case "h":
                            traduzido = "ìkt";
                            break;
                        case "j":
                            traduzido = "tík";
                            break;
                        case "k":
                            traduzido = "tìk";
                            break;
                        case "l":
                            traduzido = "kít";
                            break;
                        case "m":
                            traduzido = "kìt";
                            break;
                        case "n":
                            traduzido = "kàh";
                            break;
                        case "p":
                            traduzido = "kàh";
                            break;
                        case "q":
                            traduzido = "hàk";
                            break;
                        case "r":
                            traduzido = "hák";
                            break;
                        case "s":
                            traduzido = "kòh";
                            break;
                        case "t":
                            traduzido = "kóh";
                            break;
                        case "v":
                            traduzido = "kùh";
                            break;
                        case "w":
                            traduzido = "kúh";
                            break;
                        case "x":
                            traduzido = "ùkí";
                            break;
                        case "z":
                            traduzido = "úkì";
                            break;
                        default:
                            traduzido = "kìh";
                            break;
                    }
                    break;
                case 4: //String com 4 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "íhkh";
                            break;
                        case "c":
                            traduzido = "íkhà";
                            break;
                        case "d":
                            traduzido = "íkìk";
                            break;
                        case "f":
                            traduzido = "ìkíà";
                            break;
                        case "g":
                            traduzido = "kìká";
                            break;
                        case "h":
                            traduzido = "ìktú";
                            break;
                        case "j":
                            traduzido = "tíkù";
                            break;
                        case "k":
                            traduzido = "tìkh";
                            break;
                        case "l":
                            traduzido = "kítk";
                            break;
                        case "m":
                            traduzido = "kìtì";
                            break;
                        case "n":
                            traduzido = "kàkú";
                            break;
                        case "p":
                            traduzido = "kàhk";
                            break;
                        case "q":
                            traduzido = "hàkò";
                            break;
                        case "r":
                            traduzido = "háké";
                            break;
                        case "s":
                            traduzido = "kòkì";
                            break;
                        case "t":
                            traduzido = "kóhé";
                            break;
                        case "v":
                            traduzido = "kùhí";
                            break;
                        case "w":
                            traduzido = "kúhk";
                            break;
                        case "x":
                            traduzido = "ùkík";
                            break;
                        case "z":
                            traduzido = "úkìk";
                            break;
                        default:
                            traduzido = "kìóh";
                            break;
                    }
                    break;
                default: // String com mais de 4 caracteres
                    traduzido = TraduzirParaKriktik(silaba.Substring(0, 3)) + TraduzirParaKriktik(silaba.Substring(3));
                    break;
            }
            return traduzido;
        }
        /*
        switch (idioma)
        {
            case SpeechType.Avlitir:
                {
                    return TraduzirParaAvlitir(String texto);
                }
            case SpeechType.Celirus:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Comum:
                {
                    return texto;
                }
            case SpeechType.Drakir:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Eorin:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Faeris:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Ihluv:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Infaris:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Kahlur:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Kriktik:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Krusk:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Ladvek:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Morfat:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Nanuk:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Nukan:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Poolik:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Prokbum:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Shakshar:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Tahare:
                {
                    return TraduzirPara(String texto);
                }
            case SpeechType.Taract:
                {
                    return TraduzirPara(String texto);
                }
            default:
                {
                    return texto;
                }

        }
        */

        /*
            // Capitalize the first word of sentence
            if ((mySpeechTranslated != "") && (mySpeechTranslated != null))
            {
                tempy = mySpeechTranslated.Substring(0, 1).ToUpper();
                mySpeechTranslated = mySpeechTranslated.Substring(1, mySpeechTranslated.Length - 1);
                mySpeechTranslated = tempy + mySpeechTranslated;
            }
            tempy = ""; // reset temp
        */

    }
}
