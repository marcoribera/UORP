using System;
using System.IO;
using System.Collections;
using Server;
using Server.Misc;
using Server.Items;
using Server.Custom;

namespace Server.Mobiles
{

	public enum SpeechType
	{
        Comum, //(Comum atual de Eora/Fosso)
        Eorin, //(Comum antigo de Eora)
        Avlitir, //(Comum antigo de Avlit)
        Tahare, //(Elfos da floresta)
        Ihluv, //(Elfos urbanos)
        Kahlur, //(Elfos das profundezas/Drowzinho)
        Nanuk, //(Anão)
        Poolik, //(Halfling/Hobbit)
        Krusk, //(Orc/goblins)
        Celirus, //(Celestial)
        Infaris, //(Demoníaca)
        Prokbum, //(Gigantes)
        Drakir, //(Gárgulas/Dragões)
        Kriktik, //(Selvagens/Homens Rato)
        Shakshar, //(Ophidians e outros seres do tipo cobras)
        Morfat, //(Mortos Vivos)
        Faeris, //(Drúidas, Fadas, Minotauros e outros seres místicos)
        Ladvek, //(Linguagem de sinais do submundo)
        Nukan, //(Linguagem de sinais)
        Taract //(Aranhas e outras criaturas arácneas)
    }



    public class PolyGlotMobile : Mobile
	{

		private SpeechType LangSpeak;

        private bool m_KnowEorin; //(Comum antigo de Eora)
        private bool m_KnowAvlitir; //(Comum antigo de Avlit)
        private bool m_KnowTahare; //(Elfos da floresta)
        private bool m_KnowIhluv; //(Elfos urbanos)
        private bool m_KnowKahlur; //(Elfos das profundezas/Drowzinho)
        private bool m_KnowNanuk; //(Anão)
        private bool m_KnowPoolik; //(Halfling/Hobbit)
        private bool m_KnowKrusk; //(Orc/goblins)
        private bool m_KnowCelirus; //(Celestial)
        private bool m_KnowInfaris; //(Demoníaca)
        private bool m_KnowProkbum; //(Gigantes)
        private bool m_KnowDrakir; //(Gárgulas/Dragões)
        private bool m_KnowKriktik; //(Selvagens/Homens Rato)
        private bool m_KnowShakshar; //(Ophidians e outros seres do tipo cobras)
        private bool m_KnowMorfat; //(Mortos Vivos)
        private bool m_KnowFaeris; //(Drúidas, Fadas, Minotauros e outros seres místicos)
        private bool m_KnowLadvek; //(Linguagem de sinais do submundo)
        private bool m_KnowNukan; //(Linguagem de sinais)
        private bool m_KnowTaract; //(Aranhas e outras criaturas arácneas)

        private bool m_TTied; // For Tongue Tied spell              

        #region Custom Public Properties

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowEorin
        {
            get { return m_KnowEorin; }
            set { m_KnowEorin = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowAvlitir
        {
            get { return m_KnowAvlitir; }
            set { m_KnowAvlitir = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowTahare
        {
            get { return m_KnowTahare; }
            set { m_KnowTahare = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowIhluv
        {
            get { return m_KnowIhluv; }
            set { m_KnowIhluv = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowKahlur
        {
            get { return m_KnowKahlur; }
            set { m_KnowKahlur = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowNanuk
        {
            get { return m_KnowNanuk; }
            set { m_KnowNanuk = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowPoolik
        {
            get { return m_KnowPoolik; }
            set { m_KnowPoolik = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowKrusk
        {
            get { return m_KnowKrusk; }
            set { m_KnowKrusk = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowCelirus
        {
            get { return m_KnowCelirus; }
            set { m_KnowCelirus = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowInfaris
        {
            get { return m_KnowInfaris; }
            set { m_KnowInfaris = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowProkbum
        {
            get { return m_KnowProkbum; }
            set { m_KnowProkbum = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowDrakir
        {
            get { return m_KnowDrakir; }
            set { m_KnowDrakir = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowKriktik
        {
            get { return m_KnowKriktik; }
            set { m_KnowKriktik = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowShakshar
        {
            get { return m_KnowShakshar; }
            set { m_KnowShakshar = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowMorfat
        {
            get { return m_KnowMorfat; }
            set { m_KnowMorfat = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowFaeris
        {
            get { return m_KnowFaeris; }
            set { m_KnowFaeris = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowLadvek
        {
            get { return m_KnowLadvek; }
            set { m_KnowLadvek = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowNukan
        {
            get { return m_KnowNukan; }
            set { m_KnowNukan = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KnowTaract
        {
            get { return m_KnowTaract; }
            set { m_KnowTaract = value; }
        }

        [CommandProperty (AccessLevel.GameMaster)]
		public bool TTied
		{
			get{ return m_TTied; }
			set{ m_TTied = value; }
		}

		[CommandProperty (AccessLevel.GameMaster)]
		public SpeechType LanguageSpeaking
		{
			get{ return LangSpeak; }
			set{ LangSpeak = value; }
		}

        #endregion

        public PolyGlotMobile()
		{
            LangSpeak = SpeechType.Comum;
            m_KnowEorin = false; //(Comum antigo de Eora)
            m_KnowAvlitir = false; //(Comum antigo de Avlit)
            m_KnowTahare = false; //(Elfos da floresta)
            m_KnowIhluv = false; //(Elfos urbanos)
            m_KnowKahlur = false; //(Elfos das profundezas/Drowzinho)
            m_KnowNanuk = false; //(Anão)
            m_KnowPoolik = false; //(Halfling/Hobbit)
            m_KnowKrusk = false; //(Orc/goblins)
            m_KnowCelirus = false; //(Celestial)
            m_KnowInfaris = false; //(Demoníaca)
            m_KnowProkbum = false; //(Gigantes)
            m_KnowDrakir = false; //(Gárgulas/Dragões)
            m_KnowKriktik = false; //(Selvagens/Homens Rato)
            m_KnowShakshar = false; //(Ophidians e outros seres do tipo cobras)
            m_KnowMorfat = false; //(Mortos Vivos)
            m_KnowFaeris = false; //(Drúidas, Fadas, Minotauros e outros seres místicos)
            m_KnowLadvek = false; //(Linguagem de sinais do submundo)
            m_KnowNukan = false; //(Linguagem de sinais)
            m_KnowTaract = false; //(Aranhas e outras criaturas arácneas)
        }

        public PolyGlotMobile( Serial s ) : base( s )
		{
			
		}


		public override void Serialize (GenericWriter writer)
		{
			base.Serialize (writer);
			writer.Write ((int)3); //version	

            writer.Write((int)LangSpeak); //Idioma selecionado
            writer.Write((bool)m_KnowEorin); //(Comum antigo de Eora)
            writer.Write((bool)m_KnowAvlitir); //(Comum antigo de Avlit)
            writer.Write((bool)m_KnowTahare); //(Elfos da floresta)
            writer.Write((bool)m_KnowIhluv); //(Elfos urbanos)
            writer.Write((bool)m_KnowKahlur); //(Elfos das profundezas/Drowzinho)
            writer.Write((bool)m_KnowNanuk); //(Anão)
            writer.Write((bool)m_KnowPoolik); //(Halfling/Hobbit)
            writer.Write((bool)m_KnowKrusk); //(Orc/goblins)
            writer.Write((bool)m_KnowCelirus); //(Celestial)
            writer.Write((bool)m_KnowInfaris); //(Demoníaca)
            writer.Write((bool)m_KnowProkbum); //(Gigantes)
            writer.Write((bool)m_KnowDrakir); //(Gárgulas/Dragões)
            writer.Write((bool)m_KnowKriktik); //(Selvagens/Homens Rato)
            writer.Write((bool)m_KnowShakshar); //(Ophidians e outros seres do tipo cobras)
            writer.Write((bool)m_KnowMorfat); //(Mortos Vivos)
            writer.Write((bool)m_KnowFaeris); //(Drúidas, Fadas, Minotauros e outros seres místicos)
            writer.Write((bool)m_KnowLadvek); //(Linguagem de sinais do submundo)
            writer.Write((bool)m_KnowNukan); //(Linguagem de sinais)
            writer.Write((bool)m_KnowTaract); //(Aranhas e outras criaturas arácneas)
		}

		public override void Deserialize (GenericReader reader)
		{
			base.Deserialize (reader);
            int version = reader.PeekInt();

			switch (version)
			{
                case 3:
                    {
                        reader.ReadInt();
                        LangSpeak = (SpeechType) reader.ReadInt();
                        m_KnowEorin = reader.ReadBool(); //(Comum antigo de Eora)
                        m_KnowAvlitir = reader.ReadBool(); //(Comum antigo de Avlit)
                        m_KnowTahare = reader.ReadBool(); //(Elfos da floresta)
                        m_KnowIhluv = reader.ReadBool(); //(Elfos urbanos)
                        m_KnowKahlur = reader.ReadBool(); //(Elfos das profundezas/Drowzinho)
                        m_KnowNanuk = reader.ReadBool(); //(Anão)
                        m_KnowPoolik = reader.ReadBool(); //(Halfling/Hobbit)
                        m_KnowKrusk = reader.ReadBool(); //(Orc/goblins)
                        m_KnowCelirus = reader.ReadBool(); //(Celestial)
                        m_KnowInfaris = reader.ReadBool(); //(Demoníaca)
                        m_KnowProkbum = reader.ReadBool(); //(Gigantes)
                        m_KnowDrakir = reader.ReadBool(); //(Gárgulas/Dragões)
                        m_KnowKriktik = reader.ReadBool(); //(Selvagens/Homens Rato)
                        m_KnowShakshar = reader.ReadBool(); //(Ophidians e outros seres do tipo cobras)
                        m_KnowMorfat = reader.ReadBool(); //(Mortos Vivos)
                        m_KnowFaeris = reader.ReadBool(); //(Drúidas, Fadas, Minotauros e outros seres místicos)
                        m_KnowLadvek = reader.ReadBool(); //(Linguagem de sinais do submundo)
                        m_KnowNukan = reader.ReadBool(); //(Linguagem de sinais)
                        m_KnowTaract = reader.ReadBool(); //(Aranhas e outras criaturas arácneas)
                        goto case 0;
                    }
                case 2:
                    {
                        reader.ReadInt();
                        LangSpeak = SpeechType.Comum;
                        m_KnowEorin = reader.ReadBool(); //(Comum antigo de Eora)
                        m_KnowAvlitir = reader.ReadBool(); //(Comum antigo de Avlit)
                        m_KnowTahare = reader.ReadBool(); //(Elfos da floresta)
                        m_KnowIhluv = reader.ReadBool(); //(Elfos urbanos)
                        m_KnowKahlur = reader.ReadBool(); //(Elfos das profundezas/Drowzinho)
                        m_KnowNanuk = reader.ReadBool(); //(Anão)
                        m_KnowPoolik = reader.ReadBool(); //(Halfling/Hobbit)
                        m_KnowKrusk = reader.ReadBool(); //(Orc/goblins)
                        m_KnowCelirus = reader.ReadBool(); //(Celestial)
                        m_KnowInfaris = reader.ReadBool(); //(Demoníaca)
                        m_KnowProkbum = reader.ReadBool(); //(Gigantes)
                        m_KnowDrakir = reader.ReadBool(); //(Gárgulas/Dragões)
                        m_KnowKriktik = reader.ReadBool(); //(Selvagens/Homens Rato)
                        m_KnowShakshar = reader.ReadBool(); //(Ophidians e outros seres do tipo cobras)
                        m_KnowMorfat = reader.ReadBool(); //(Mortos Vivos)
                        m_KnowFaeris = reader.ReadBool(); //(Drúidas, Fadas, Minotauros e outros seres místicos)
                        m_KnowLadvek = reader.ReadBool(); //(Linguagem de sinais do submundo)
                        m_KnowNukan = reader.ReadBool(); //(Linguagem de sinais)
                        m_KnowTaract = reader.ReadBool(); //(Aranhas e outras criaturas arácneas)
                        goto case 0;
                    }
                case 1:
					{
                        reader.ReadInt();
						LangSpeak = SpeechType.Comum;
						reader.ReadBool();
						reader.ReadBool();
						reader.ReadBool();
						reader.ReadBool();
						reader.ReadBool();
						reader.ReadBool();
						reader.ReadBool();
						goto case 0;
					}

				case 0:
					break;
                default:
                    break;
            }
		}
	}
}


