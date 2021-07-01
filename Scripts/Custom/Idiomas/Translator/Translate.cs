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
                //from.Emote(mySpeech);
                return;
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
                                //creature.Emote("*Parece não entender o que foi dito*");
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
            List<string> silabas = new List<string>();
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

            switch (idioma)
            {
                case SpeechType.Avlitir:
                    {
                        for (int i = 0; i < palavras.Length; i++)
                        {
                            var silabas = SepararSilabas(palavras[i]);
                            foreach (string silaba in silabas)
                            {
                                resultado += TraduzirParaAvlitir(silaba);
                            }
                            resultado += (i == ultimaPalavra ? "" : " ");
                        }
                        return resultado;
                    }
                case SpeechType.Celirus:
                    {
                        for (int i = 0; i < palavras.Length; i++)
                        {
                            var silabas = SepararSilabas(palavras[i]);
                            foreach (string silaba in silabas)
                            {
                                resultado += TraduzirParaCelirus(silaba);
                            }
                            resultado += (i == ultimaPalavra ? "" : " ");
                        }
                        return resultado;
                    }
                case SpeechType.Comum:
                    {
                        return texto;
                    }
                case SpeechType.Drakir:
                    {
                        for (int i = 0; i < palavras.Length; i++)
                        {
                            var silabas = SepararSilabas(palavras[i]);
                            foreach (string silaba in silabas)
                            {
                                resultado += TraduzirParaDrakir(silaba);
                            }
                            resultado += (i == ultimaPalavra ? "" : " ");
                        }
                        return resultado;
                    }
                case SpeechType.Eorin:
                    {
                        return texto;
                    }
                case SpeechType.Faeris:
                    {
                        return texto;
                    }
                case SpeechType.Ihluv:
                    {
                        return texto;
                    }
                case SpeechType.Infaris:
                    {
                        for (int i = 0; i < palavras.Length; i++)
                        {
                            var silabas = SepararSilabas(palavras[i]);
                            foreach (string silaba in silabas)
                            {
                                resultado += TraduzirParaInfaris(silaba);
                            }
                            resultado += (i == ultimaPalavra ? "" : " ");
                        }
                        return resultado;
                    }
                case SpeechType.Kahlur:
                    {
                        return texto;
                    }
                case SpeechType.Kriktik:
                    {
                        for (int i = 0; i < palavras.Length; i++)
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
                case SpeechType.Krusk:
                    {
                        return texto;
                    }
                case SpeechType.Ladvek:
                    {
                        return texto;
                    }
                case SpeechType.Morfat:
                    {
                        return texto;
                    }
                case SpeechType.Nanuk:
                    {
                        for (int i = 0; i < palavras.Length; i++)
                        {
                            var silabas = SepararSilabas(palavras[i]);
                            foreach (string silaba in silabas)
                            {
                                resultado += TraduzirParaNanuk(silaba);
                            }
                            resultado += (i == ultimaPalavra ? "" : " ");
                        }
                        return resultado;
                    }
                case SpeechType.Nukan:
                    {
                        return texto;
                    }
                case SpeechType.Poolik:
                    {
                        for (int i = 0; i < palavras.Length; i++)
                        {
                            var silabas = SepararSilabas(palavras[i]);
                            foreach (string silaba in silabas)
                            {
                                resultado += TraduzirParaPoolik(silaba);
                            }
                            resultado += (i == ultimaPalavra ? "" : " ");
                        }
                        return resultado;
                    }
                case SpeechType.Prokbum:
                    {
                        for (int i = 0; i < palavras.Length; i++)
                        {
                            var silabas = SepararSilabas(palavras[i]);
                            foreach (string silaba in silabas)
                            {
                                resultado += TraduzirParaProkbum(silaba);
                            }
                            resultado += (i == ultimaPalavra ? "" : " ");
                        }
                        return resultado;
                    }
                case SpeechType.Shakshar:
                    {
                        return texto;
                    }
                case SpeechType.Tahare:
                    {
                        return texto;
                    }
                case SpeechType.Taract:
                    {
                        return texto;
                    }
                default:
                    {
                        return texto;
                    }

            }
            return resultado;
        }

        private static String TraduzirParaAvlitir(String silaba)
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
                            return "uo";
                        case "e":
                        case "é":
                        case "è":
                        case "ê":
                        case "ë":
                            return "et";
                        case "i":
                        case "í":
                        case "ì":
                        case "î":
                        case "ï":
                            return "ec";
                        case "o":
                        case "ó":
                        case "ò":
                        case "ô":
                        case "õ":
                        case "ö":
                            return "o";
                        case "u":
                        case "ú":
                        case "ù":
                        case "û":
                        case "ü":
                            return "u";
                        case "y":
                        case "ý":
                        case "ÿ":
                            return "eps";
                        default:
                            return silaba;
                    }
                case 2: //String com 2 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "sphe";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "spi";
                            break;
                        case "d":
                            traduzido = "ded";
                            break;
                        case "f":
                            traduzido = "fac";
                            break;
                        case "g":
                            traduzido = "gra";
                            break;
                        case "h":
                            traduzido = "hic";
                            break;
                        case "j":
                            traduzido = "jus";
                            break;
                        case "k":
                            traduzido = "quid";
                            break;
                        case "l":
                            traduzido = "lau";
                            break;
                        case "m":
                            traduzido = "mem";
                            break;
                        case "n":
                            traduzido = "nih";
                            break;
                        case "p":
                            traduzido = "per";
                            break;
                        case "q":
                            traduzido = "quo";
                            break;
                        case "r":
                            traduzido = "res";
                            break;
                        case "s":
                            traduzido = "scr";
                            break;
                        case "t":
                            traduzido = "tui";
                            break;
                        case "v":
                            traduzido = "ven";
                            break;
                        case "w":
                            traduzido = "ung";
                            break;
                        case "x":
                            traduzido = "psi";
                            break;
                        case "z":
                            traduzido = "fi";
                            break;
                        default:
                            traduzido = "ksi";
                            break;
                    }
                    break;
                case 3: //String com 3 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "bona";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "cave";
                            break;
                        case "d":
                            traduzido = "de vis";
                            break;
                        case "f":
                            traduzido = "fiat";
                            break;
                        case "g":
                            traduzido = "gutta";
                            break;
                        case "h":
                            traduzido = "homo";
                            break;
                        case "j":
                            traduzido = "joco";
                            break;
                        case "k":
                            traduzido = "quibus";
                            break;
                        case "l":
                            traduzido = "lexu";
                            break;
                        case "m":
                            traduzido = "magna";
                            break;
                        case "n":
                            traduzido = "natur";
                            break;
                        case "p":
                            traduzido = "panem";
                            break;
                        case "q":
                            traduzido = "qual";
                            break;
                        case "r":
                            traduzido = "rati";
                            break;
                        case "s":
                            traduzido = "sapie";
                            break;
                        case "t":
                            traduzido = "temp";
                            break;
                        case "v":
                            traduzido = "venit";
                            break;
                        case "w":
                            traduzido = "ungol";
                            break;
                        case "x":
                            traduzido = "psith";
                            break;
                        case "z":
                            traduzido = "fici";
                            break;
                        default:
                            traduzido = "ksitae";
                            break;
                    }
                    break;
                case 4: //String com 4 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "bene";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "centum";
                            break;
                        case "d":
                            traduzido = "deletur";
                            break;
                        case "f":
                            traduzido = "fugit";
                            break;
                        case "g":
                            traduzido = "gravitas";
                            break;
                        case "h":
                            traduzido = "henet";
                            break;
                        case "j":
                            traduzido = "justae";
                            break;
                        case "k":
                            traduzido = "quomodo";
                            break;
                        case "l":
                            traduzido = "legeps";
                            break;
                        case "m":
                            traduzido = "male";
                            break;
                        case "n":
                            traduzido = "nemine";
                            break;
                        case "p":
                            traduzido = "patiens";
                            break;
                        case "q":
                            traduzido = "quonaet";
                            break;
                        case "r":
                            traduzido = "rudis";
                            break;
                        case "s":
                            traduzido = "scilicet";
                            break;
                        case "t":
                            traduzido = "tantum";
                            break;
                        case "v":
                            traduzido = "vaelo";
                            break;
                        case "w":
                            traduzido = "unfecha";
                            break;
                        case "x":
                            traduzido = "psuchae";
                            break;
                        case "z":
                            traduzido = "acciffach";
                            break;
                        default:
                            traduzido = "eitam";
                            break;
                    }
                    break;
                default: // String com mais de 4 caracteres
                    traduzido = TraduzirParaAvlitir(silaba.Substring(0, 3)) + TraduzirParaAvlitir(silaba.Substring(3));
                    break;
            }
            return traduzido;
        }
        private static String TraduzirParaCelirus(String silaba)
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
                            return "un";
                        case "e":
                        case "é":
                        case "è":
                        case "ê":
                        case "ë":
                            return "ph";
                        case "i":
                        case "í":
                        case "ì":
                        case "î":
                        case "ï":
                            return "on";
                        case "o":
                        case "ó":
                        case "ò":
                        case "ô":
                        case "õ":
                        case "ö":
                            return "ed";
                        case "u":
                        case "ú":
                        case "ù":
                        case "û":
                        case "ü":
                            return "an";
                        case "y":
                        case "ý":
                        case "ÿ":
                            return "ce";
                        default:
                            return silaba;
                    }
                case 2: //String com 2 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "pa";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "veh";
                            break;
                        case "d":
                            traduzido = "me";
                            break;
                        case "f":
                            traduzido = "or";
                            break;
                        case "g":
                            traduzido = "ged";
                            break;
                        case "h":
                            traduzido = "na";
                            break;
                        case "j":
                            traduzido = "go";
                            break;
                        case "k":
                            traduzido = "eh";
                            break;
                        case "l":
                            traduzido = "ur";
                            break;
                        case "m":
                            traduzido = "tal";
                            break;
                        case "n":
                            traduzido = "drul";
                            break;
                        case "p":
                            traduzido = "mal";
                            break;
                        case "q":
                            traduzido = "ger";
                            break;
                        case "r":
                            traduzido = "don";
                            break;
                        case "s":
                            traduzido = "fam";
                            break;
                        case "t":
                            traduzido = "gisg";
                            break;
                        case "v":
                            traduzido = "van";
                            break;
                        case "w":
                            traduzido = "wel";
                            break;
                        case "x":
                            traduzido = "pal";
                            break;
                        case "z":
                            traduzido = "ceph";
                            break;
                        default:
                            traduzido = "nun";
                            break;
                    }
                    break;
                case 3: //String com 3 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "alep";
                            break;
                        case "c":
                            traduzido = "beth";
                            break;
                        case "d":
                            traduzido = "gim";
                            break;
                        case "f":
                            traduzido = "dale";
                            break;
                        case "g":
                            traduzido = "vau";
                            break;
                        case "h":
                            traduzido = "zain";
                            break;
                        case "j":
                            traduzido = "chet";
                            break;
                        case "k":
                            traduzido = "thes";
                            break;
                        case "l":
                            traduzido = "iod";
                            break;
                        case "m":
                            traduzido = "caph";
                            break;
                        case "n":
                            traduzido = "lame";
                            break;
                        case "p":
                            traduzido = "mem";
                            break;
                        case "q":
                            traduzido = "samet";
                            break;
                        case "r":
                            traduzido = "ain";
                            break;
                        case "s":
                            traduzido = "pe";
                            break;
                        case "t":
                            traduzido = "zade";
                            break;
                        case "v":
                            traduzido = "kuff";
                            break;
                        case "w":
                            traduzido = "res";
                            break;
                        case "x":
                            traduzido = "schin";
                            break;
                        case "z":
                            traduzido = "tau";
                            break;
                        default:
                            traduzido = "menem";
                            break;
                    }
                    break;
                case 4: //String com 4 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "aybtep";
                            break;
                        case "c":
                            traduzido = "bahati";
                            break;
                        case "d":
                            traduzido = "het";
                            break;
                        case "f":
                            traduzido = "kamen";
                            break;
                        case "g":
                            traduzido = "hohl";
                            break;
                        case "h":
                            traduzido = "floonsa";
                            break;
                        case "j":
                            traduzido = "penemue";
                            break;
                        case "k":
                            traduzido = "samyaza";
                            break;
                        case "l":
                            traduzido = "lodoh";
                            break;
                        case "m":
                            traduzido = "harut";
                            break;
                        case "n":
                            traduzido = "lameth";
                            break;
                        case "p":
                            traduzido = "memeth";
                            break;
                        case "q":
                            traduzido = "sameto";
                            break;
                        case "r":
                            traduzido = "ainan";
                            break;
                        case "s":
                            traduzido = "peedh";
                            break;
                        case "t":
                            traduzido = "zadece";
                            break;
                        case "v":
                            traduzido = "kuffon";
                            break;
                        case "w":
                            traduzido = "resph";
                            break;
                        case "x":
                            traduzido = "schinan";
                            break;
                        case "z":
                            traduzido = "tauceh";
                            break;
                        default:
                            traduzido = "menem";
                            break;
                    }
                    break;
                default: // String com mais de 4 caracteres
                    traduzido = TraduzirParaCelirus(silaba.Substring(0, 3)) + TraduzirParaCelirus(silaba.Substring(3));
                    break;
            }
            return traduzido;
        }

        private static String TraduzirParaDrakir(String silaba)
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
                            return "are";
                        case "e":
                        case "é":
                        case "è":
                        case "ê":
                        case "ë":
                            return "eer";
                        case "i":
                        case "í":
                        case "ì":
                        case "î":
                        case "ï":
                            return "iss";
                        case "o":
                        case "ó":
                        case "ò":
                        case "ô":
                        case "õ":
                        case "ö":
                            return "or";
                        case "u":
                        case "ú":
                        case "ù":
                        case "û":
                        case "ü":
                            return "oo";
                        case "y":
                        case "ý":
                        case "ÿ":
                            return "yr";
                        default:
                            return silaba;
                    }
                case 2: //String com 2 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "ir";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "ach";
                            break;
                        case "d":
                            traduzido = "as";
                            break;
                        case "f":
                            traduzido = "sh";
                            break;
                        case "g":
                            traduzido = "ph";
                            break;
                        case "h":
                            traduzido = "u";
                            break;
                        case "j":
                            traduzido = "th";
                            break;
                        case "k":
                            traduzido = "h";
                            break;
                        case "l":
                            traduzido = "sa";
                            break;
                        case "m":
                            traduzido = "Jul";
                            break;
                        case "n":
                            traduzido = "nu";
                            break;
                        case "p":
                            traduzido = "ka";
                            break;
                        case "q":
                            traduzido = "ksh";
                            break;
                        case "r":
                            traduzido = "pe";
                            break;
                        case "s":
                            traduzido = "re";
                            break;
                        case "t":
                            traduzido = "ru";
                            break;
                        case "v":
                            traduzido = "sah";
                            break;
                        case "w":
                            traduzido = "zu";
                            break;
                        case "x":
                            traduzido = "vo";
                            break;
                        case "z":
                            traduzido = "nuz";
                            break;
                        default:
                            traduzido = "rii";
                            break;
                    }
                    break;
                case 3: //String com 3 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "irs";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "achr";
                            break;
                        case "d":
                            traduzido = "asr";
                            break;
                        case "f":
                            traduzido = "shs";
                            break;
                        case "g":
                            traduzido = "phr";
                            break;
                        case "h":
                            traduzido = "us";
                            break;
                        case "j":
                            traduzido = "thr";
                            break;
                        case "k":
                            traduzido = "ha";
                            break;
                        case "l":
                            traduzido = "sar";
                            break;
                        case "m":
                            traduzido = "Julr";
                            break;
                        case "n":
                            traduzido = "nus";
                            break;
                        case "p":
                            traduzido = "kas";
                            break;
                        case "q":
                            traduzido = "kshr";
                            break;
                        case "r":
                            traduzido = "peh";
                            break;
                        case "s":
                            traduzido = "rer";
                            break;
                        case "t":
                            traduzido = "rus";
                            break;
                        case "v":
                            traduzido = "sahr";
                            break;
                        case "w":
                            traduzido = "zur";
                            break;
                        case "x":
                            traduzido = "vos";
                            break;
                        case "z":
                            traduzido = "nuzh";
                            break;
                        default:
                            traduzido = "riir";
                            break;
                    }
                    break;
                case 4: //String com 4 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "irsh";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "achur";
                            break;
                        case "d":
                            traduzido = "asrh";
                            break;
                        case "f":
                            traduzido = "shsa";
                            break;
                        case "g":
                            traduzido = "phri";
                            break;
                        case "h":
                            traduzido = "usk";
                            break;
                        case "j":
                            traduzido = "thra";
                            break;
                        case "k":
                            traduzido = "has";
                            break;
                        case "l":
                            traduzido = "sark";
                            break;
                        case "m":
                            traduzido = "Julra";
                            break;
                        case "n":
                            traduzido = "nush";
                            break;
                        case "p":
                            traduzido = "kash";
                            break;
                        case "q":
                            traduzido = "kshri";
                            break;
                        case "r":
                            traduzido = "pehr";
                            break;
                        case "s":
                            traduzido = "rerh";
                            break;
                        case "t":
                            traduzido = "rusk";
                            break;
                        case "v":
                            traduzido = "sahri";
                            break;
                        case "w":
                            traduzido = "zurae";
                            break;
                        case "x":
                            traduzido = "vosor";
                            break;
                        case "z":
                            traduzido = "nuzheer";
                            break;
                        default:
                            traduzido = "riiryr";
                            break;
                    }
                    break;
                default: // String com mais de 4 caracteres
                    traduzido = TraduzirParaDrakir(silaba.Substring(0, 3)) + TraduzirParaDrakir(silaba.Substring(3));
                    break;
            }
            return traduzido;
        }
        private static String TraduzirParaInfaris(String silaba)
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
                            return "a";
                        case "e":
                        case "é":
                        case "è":
                        case "ê":
                        case "ë":
                            return "e";
                        case "i":
                        case "í":
                        case "ì":
                        case "î":
                        case "ï":
                            return "g";
                        case "o":
                        case "ó":
                        case "ò":
                        case "ô":
                        case "õ":
                        case "ö":
                            return "u";
                        case "u":
                        case "ú":
                        case "ù":
                        case "û":
                        case "ü":
                            return "x";
                        case "y":
                        case "ý":
                        case "ÿ":
                            return "o";
                        default:
                            return silaba;
                    }
                case 2: //String com 2 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "az";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "ll";
                            break;
                        case "d":
                            traduzido = "me";
                            break;
                        case "f":
                            traduzido = "no";
                            break;
                        case "g":
                            traduzido = "asj";
                            break;
                        case "h":
                            traduzido = "re";
                            break;
                        case "j":
                            traduzido = "daz";
                            break;
                        case "k":
                            traduzido = "te";
                            break;
                        case "l":
                            traduzido = "gul";
                            break;
                        case "m":
                            traduzido = "ui";
                            break;
                        case "n":
                            traduzido = "kar";
                            break;
                        case "p":
                            traduzido = "ur";
                            break;
                        case "q":
                            traduzido = "laz";
                            break;
                        case "r":
                            traduzido = "xi";
                            break;
                        case "s":
                            traduzido = "lek";
                            break;
                        case "t":
                            traduzido = "za";
                            break;
                        case "v":
                            traduzido = "lok";
                            break;
                        case "w":
                            traduzido = "ze";
                            break;
                        case "x":
                            traduzido = "maz";
                            break;
                        case "z":
                            traduzido = "ril";
                            break;
                        default:
                            traduzido = "ruk";
                            break;
                    }
                    break;
                case 3: //String com 3 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "shi";
                            break;
                        case "c":
                            traduzido = "alar";
                            break;
                        case "d":
                            traduzido = "tor";
                            break;
                        case "f":
                            traduzido = "aman";
                            break;
                        case "g":
                            traduzido = "zar";
                            break;
                        case "h":
                            traduzido = "amir";
                            break;
                        case "j":
                            traduzido = "ante";
                            break;
                        case "k":
                            traduzido = "adare";
                            break;
                        case "l":
                            traduzido = "ashj";
                            break;
                        case "m":
                            traduzido = "kiel";
                            break;
                        case "n":
                            traduzido = "maev";
                            break;
                        case "p":
                            traduzido = "maez";
                            break;
                        case "q":
                            traduzido = "orah";
                            break;
                        case "r":
                            traduzido = "parn";
                            break;
                        case "s":
                            traduzido = "raka";
                            break;
                        case "t":
                            traduzido = "rikk";
                            break;
                        case "v":
                            traduzido = "refir";
                            break;
                        case "w":
                            traduzido = "veni";
                            break;
                        case "x":
                            traduzido = "zenn";
                            break;
                        case "z":
                            traduzido = "zila";
                            break;
                        default:
                            traduzido = "belan";
                            break;
                    }
                    break;
                case 4: //String com 4 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "buras";
                            break;
                        case "c":
                            traduzido = "enkil";
                            break;
                        case "d":
                            traduzido = "golad";
                            break;
                        case "f":
                            traduzido = "gular";
                            break;
                        case "g":
                            traduzido = "kemil";
                            break;
                        case "h":
                            traduzido = "melar";
                            break;
                        case "j":
                            traduzido = "modas";
                            break;
                        case "k":
                            traduzido = "nagas";
                            break;
                        case "l":
                            traduzido = "hakir";
                            break;
                        case "m":
                            traduzido = "refir";
                            break;
                        case "n":
                            traduzido = "revos";
                            break;
                        case "p":
                            traduzido = "soran";
                            break;
                        case "q":
                            traduzido = "tiros";
                            break;
                        case "r":
                            traduzido = "zekul";
                            break;
                        case "s":
                            traduzido = "kazit";
                            break;
                        case "t":
                            traduzido = "xumh";
                            break;
                        case "v":
                            traduzido = "risor";
                            break;
                        case "w":
                            traduzido = "huths";
                            break;
                        case "x":
                            traduzido = "reth";
                            break;
                        case "z":
                            traduzido = "srak";
                            break;
                        default:
                            traduzido = "anemo";
                            break;
                    }
                    break;
                default: // String com mais de 4 caracteres
                    traduzido = TraduzirParaInfaris(silaba.Substring(0, 3)) + TraduzirParaInfaris(silaba.Substring(3));
                    break;
            }
            return traduzido;
        }
        private static String TraduzirParaNanuk(String silaba)
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
                            return "e";
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
                            return "tha";
                        case "o":
                        case "ó":
                        case "ò":
                        case "ô":
                        case "õ":
                        case "ö":
                            return "aa";
                        case "u":
                        case "ú":
                        case "ù":
                        case "û":
                        case "ü":
                            return "u";
                        case "y":
                        case "ý":
                        case "ÿ":
                            return "uh";
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
                            traduzido = "ch";
                            break;
                        case "d":
                            traduzido = "dh";
                            break;
                        case "f":
                            traduzido = "ko";
                            break;
                        case "g":
                            traduzido = "gh";
                            break;
                        case "h":
                            traduzido = "h";
                            break;
                        case "j":
                            traduzido = "nd";
                            break;
                        case "k":
                            traduzido = "kh";
                            break;
                        case "l":
                            traduzido = "ll";
                            break;
                        case "m":
                            traduzido = "mb";
                            break;
                        case "n":
                            traduzido = "ng";
                            break;
                        case "p":
                            traduzido = "ph";
                            break;
                        case "q":
                            traduzido = "qu";
                            break;
                        case "r":
                            traduzido = "rh";
                            break;
                        case "s":
                            traduzido = "sh";
                            break;
                        case "t":
                            traduzido = "d";
                            break;
                        case "v":
                            traduzido = "w";
                            break;
                        case "w":
                            traduzido = "h";
                            break;
                        case "x":
                            traduzido = "xi";
                            break;
                        case "z":
                            traduzido = "zh";
                            break;
                        default:
                            traduzido = "du";
                            break;
                    }
                    break;
                case 3: //String com 3 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "kah";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "chu";
                            break;
                        case "d":
                            traduzido = "dhu";
                            break;
                        case "f":
                            traduzido = "kor";
                            break;
                        case "g":
                            traduzido = "ghw";
                            break;
                        case "h":
                            traduzido = "hu";
                            break;
                        case "j":
                            traduzido = "ndu";
                            break;
                        case "k":
                            traduzido = "khw";
                            break;
                        case "l":
                            traduzido = "lls";
                            break;
                        case "m":
                            traduzido = "mbi";
                            break;
                        case "n":
                            traduzido = "ngw";
                            break;
                        case "p":
                            traduzido = "pha";
                            break;
                        case "q":
                            traduzido = "quh";
                            break;
                        case "r":
                            traduzido = "rhu";
                            break;
                        case "s":
                            traduzido = "shu";
                            break;
                        case "t":
                            traduzido = "dh";
                            break;
                        case "v":
                            traduzido = "wh";
                            break;
                        case "w":
                            traduzido = "hu";
                            break;
                        case "x":
                            traduzido = "xil";
                            break;
                        case "z":
                            traduzido = "zhu";
                            break;
                        default:
                            traduzido = "duh";
                            break;
                    }
                    break;
                case 4: //String com 4 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "kahr";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "chuz";
                            break;
                        case "d":
                            traduzido = "dhuz";
                            break;
                        case "f":
                            traduzido = "kort";
                            break;
                        case "g":
                            traduzido = "ghwr";
                            break;
                        case "h":
                            traduzido = "huk";
                            break;
                        case "j":
                            traduzido = "ndur";
                            break;
                        case "k":
                            traduzido = "khwu";
                            break;
                        case "l":
                            traduzido = "llsi";
                            break;
                        case "m":
                            traduzido = "mbir";
                            break;
                        case "n":
                            traduzido = "ngwu";
                            break;
                        case "p":
                            traduzido = "phar";
                            break;
                        case "q":
                            traduzido = "quhu";
                            break;
                        case "r":
                            traduzido = "rhur";
                            break;
                        case "s":
                            traduzido = "shur";
                            break;
                        case "t":
                            traduzido = "dha";
                            break;
                        case "v":
                            traduzido = "whu";
                            break;
                        case "w":
                            traduzido = "hur";
                            break;
                        case "x":
                            traduzido = "xilr";
                            break;
                        case "z":
                            traduzido = "zhur";
                            break;
                        default:
                            traduzido = "duhr";
                            break;
                    }
                    break;
                default: // String com mais de 4 caracteres
                    traduzido = TraduzirParaNanuk(silaba.Substring(0, 3)) + TraduzirParaNanuk(silaba.Substring(3));
                    break;
            }
            return traduzido;
        }
private static String TraduzirParaPoolik(String silaba)
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
                            return "a";
                        case "e":
                        case "é":
                        case "è":
                        case "ê":
                        case "ë":
                            return "eil";
                        case "i":
                        case "í":
                        case "ì":
                        case "î":
                        case "ï":
                            return "e";
                        case "o":
                        case "ó":
                        case "ò":
                        case "ô":
                        case "õ":
                        case "ö":
                            return "o";
                        case "u":
                        case "ú":
                        case "ù":
                        case "û":
                        case "ü":
                            return "uil";
                        case "y":
                        case "ý":
                        case "ÿ":
                            return "i";
                        default:
                            return silaba;
                    }
                case 2: //String com 2 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "bi";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "ci";
                            break;
                        case "d":
                            traduzido = "d";
                            break;
                        case "f":
                            traduzido = "so";
                            break;
                        case "g":
                            traduzido = "v";
                            break;
                        case "h":
                            traduzido = "fi";
                            break;
                        case "j":
                            traduzido = "di";
                            break;
                        case "k":
                            traduzido = "qi";
                            break;
                        case "l":
                            traduzido = "s";
                            break;
                        case "m":
                            traduzido = "m";
                            break;
                        case "n":
                            traduzido = "gi";
                            break;
                        case "p":
                            traduzido = "k";
                            break;
                        case "q":
                            traduzido = "q";
                            break;
                        case "r":
                            traduzido = "v";
                            break;
                        case "s":
                            traduzido = "hi";
                            break;
                        case "t":
                            traduzido = "t";
                            break;
                        case "v":
                            traduzido = "w";
                            break;
                        case "w":
                            traduzido = "v";
                            break;
                        case "x":
                            traduzido = "n";
                            break;
                        case "z":
                            traduzido = "st";
                            break;
                        default:
                            traduzido = "pi";
                            break;
                    }
                    break;
                case 3: //String com 3 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "bis";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "cis";
                            break;
                        case "d":
                            traduzido = "de";
                            break;
                        case "f":
                            traduzido = "sol";
                            break;
                        case "g":
                            traduzido = "vi";
                            break;
                        case "h":
                            traduzido = "fis";
                            break;
                        case "j":
                            traduzido = "dis";
                            break;
                        case "k":
                            traduzido = "qis";
                            break;
                        case "l":
                            traduzido = "se";
                            break;
                        case "m":
                            traduzido = "mi";
                            break;
                        case "n":
                            traduzido = "gis";
                            break;
                        case "p":
                            traduzido = "ke";
                            break;
                        case "q":
                            traduzido = "qi";
                            break;
                        case "r":
                            traduzido = "ve";
                            break;
                        case "s":
                            traduzido = "his";
                            break;
                        case "t":
                            traduzido = "te";
                            break;
                        case "v":
                            traduzido = "wi";
                            break;
                        case "w":
                            traduzido = "vei";
                            break;
                        case "x":
                            traduzido = "na";
                            break;
                        case "z":
                            traduzido = "ste";
                            break;
                        default:
                            traduzido = "pil";
                            break;
                    }
                    break;
                case 4: //String com 4 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "biso";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "cist";
                            break;
                        case "d":
                            traduzido = "del";
                            break;
                        case "f":
                            traduzido = "solc";
                            break;
                        case "g":
                            traduzido = "vil";
                            break;
                        case "h":
                            traduzido = "fisc";
                            break;
                        case "j":
                            traduzido = "dist";
                            break;
                        case "k":
                            traduzido = "qisc";
                            break;
                        case "l":
                            traduzido = "sei";
                            break;
                        case "m":
                            traduzido = "mia";
                            break;
                        case "n":
                            traduzido = "gist";
                            break;
                        case "p":
                            traduzido = "kei";
                            break;
                        case "q":
                            traduzido = "qil";
                            break;
                        case "r":
                            traduzido = "vei";
                            break;
                        case "s":
                            traduzido = "hisl";
                            break;
                        case "t":
                            traduzido = "ter";
                            break;
                        case "v":
                            traduzido = "wie";
                            break;
                        case "w":
                            traduzido = "veil";
                            break;
                        case "x":
                            traduzido = "nas";
                            break;
                        case "z":
                            traduzido = "stef";
                            break;
                        default:
                            traduzido = "pilt";
                            break;
                    }
                    break;
                default: // String com mais de 4 caracteres
                    traduzido = TraduzirParaPoolik(silaba.Substring(0, 3)) + TraduzirParaPoolik(silaba.Substring(3));
                    break;
            }
            return traduzido;
        }
        private static String TraduzirParaProkbum(String silaba)
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
                            return "ahw";
                        case "e":
                        case "é":
                        case "è":
                        case "ê":
                        case "ë":
                            return "eh";
                        case "i":
                        case "í":
                        case "ì":
                        case "î":
                        case "ï":
                            return "im";
                        case "o":
                        case "ó":
                        case "ò":
                        case "ô":
                        case "õ":
                        case "ö":
                            return "om";
                        case "u":
                        case "ú":
                        case "ù":
                        case "û":
                        case "ü":
                            return "oo";
                        case "y":
                        case "ý":
                        case "ÿ":
                            return "ey";
                        default:
                            return silaba;
                    }
                case 2: //String com 2 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "be";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "ce";
                            break;
                        case "d":
                            traduzido = "de";
                            break;
                        case "f":
                            traduzido = "em";
                            break;
                        case "g":
                            traduzido = "ge";
                            break;
                        case "h":
                            traduzido = "ham";
                            break;
                        case "j":
                            traduzido = "jod";
                            break;
                        case "k":
                            traduzido = "ka";
                            break;
                        case "l":
                            traduzido = "le";
                            break;
                        case "m":
                            traduzido = "em";
                            break;
                        case "n":
                            traduzido = "en";
                            break;
                        case "p":
                            traduzido = "pem";
                            break;
                        case "q":
                            traduzido = "koo";
                            break;
                        case "r":
                            traduzido = "ra";
                            break;
                        case "s":
                            traduzido = "es";
                            break;
                        case "t":
                            traduzido = "tem";
                            break;
                        case "v":
                            traduzido = "veh";
                            break;
                        case "w":
                            traduzido = "um";
                            break;
                        case "x":
                            traduzido = "es";
                            break;
                        case "z":
                            traduzido = "set";
                            break;
                        default:
                            traduzido = "pi";
                            break;
                    }
                    break;
                case 3: //String com 3 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "bur";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "cram";
                            break;
                        case "d":
                            traduzido = "dam";
                            break;
                        case "f":
                            traduzido = "fem";
                            break;
                        case "g":
                            traduzido = "gim";
                            break;
                        case "h":
                            traduzido = "hor";
                            break;
                        case "j":
                            traduzido = "jar";
                            break;
                        case "k":
                            traduzido = "koo";
                            break;
                        case "l":
                            traduzido = "eri";
                            break;
                        case "m":
                            traduzido = "eh";
                            break;
                        case "n":
                            traduzido = "nim";
                            break;
                        case "p":
                            traduzido = "opri";
                            break;
                        case "q":
                            traduzido = "koom";
                            break;
                        case "r":
                            traduzido = "reh";
                            break;
                        case "s":
                            traduzido = "sem";
                            break;
                        case "t":
                            traduzido = "toor";
                            break;
                        case "v":
                            traduzido = "vuru";
                            break;
                        case "w":
                            traduzido = "um";
                            break;
                        case "x":
                            traduzido = "koxo";
                            break;
                        case "z":
                            traduzido = "sam";
                            break;
                        default:
                            traduzido = "pey";
                            break;
                    }
                    break;
                case 4: //String com 4 caracteres
                    switch (primeiraLetra)
                    {
                        case "b":
                            traduzido = "bumu";
                            break;
                        case "c":
                        case "ç":
                            traduzido = "camo";
                            break;
                        case "d":
                            traduzido = "dari";
                            break;
                        case "f":
                            traduzido = "furo";
                            break;
                        case "g":
                            traduzido = "gim";
                            break;
                        case "h":
                            traduzido = "hor";
                            break;
                        case "j":
                            traduzido = "jarr";
                            break;
                        case "k":
                            traduzido = "koir";
                            break;
                        case "l":
                            traduzido = "eira";
                            break;
                        case "m":
                            traduzido = "ehre";
                            break;
                        case "n":
                            traduzido = "nibam";
                            break;
                        case "p":
                            traduzido = "oprbum";
                            break;
                        case "q":
                            traduzido = "kooar";
                            break;
                        case "r":
                            traduzido = "rehrum";
                            break;
                        case "s":
                            traduzido = "semar";
                            break;
                        case "t":
                            traduzido = "torat";
                            break;
                        case "v":
                            traduzido = "vurm";
                            break;
                        case "w":
                            traduzido = "umhe";
                            break;
                        case "x":
                            traduzido = "kom";
                            break;
                        case "z":
                            traduzido = "sbem";
                            break;
                        default:
                            traduzido = "pea";
                            break;
                    }
                    break;
                default: // String com mais de 4 caracteres
                    traduzido = TraduzirParaProkbum(silaba.Substring(0, 3)) + TraduzirParaProkbum(silaba.Substring(3));
                    break;
            }
            return traduzido;
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
