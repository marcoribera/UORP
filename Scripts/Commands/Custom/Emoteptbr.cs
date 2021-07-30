//Emote v2 by CMonkey123
/*
v2 changes:
	-Shortened script
	-Added emotes (thanks to zire):
		Reverência, desmaiar, punch, dar um tapa, stickouttongue, tapfoot
	-Added emote gump (thanks to zire)
*/
/* Emote v3 by GM Jubal from Ebonspire http://www.ebonspire.com
 * I Left the above comments in here for credit to properly go back to those whom originally wrote this
 * I simply made it so that the [e command would call the gump if used by itself or if the <sound> was
 * misspelled, shortened the code down from 1300+ lines down to only 635 lines including these comments.
 * Also fixed a couple of typos in the script.
 * This has been tested on both RunUO beta .36 and RunUO RC0 1.0
*/
/* Emote v4 by Lysdexic
 * Updated for RunUO 2.0 RC2
 * Puke command could be used for teleport bug... removed that ability.
 * Typos again... 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Commands.Generic;

namespace Server.Commands
{
	public enum EmotePage 
	{ 
		P1,
		P2,
		P3,
		P4,
	}
	public class Emote
	{	
		public static void Initialize()
		{
			CommandSystem.Register( "emote", AccessLevel.Player, new CommandEventHandler( Emote_OnCommand ) );
			CommandSystem.Register( "e", AccessLevel.Player, new CommandEventHandler( Emote_OnCommand ) );
		}

	  	[Usage( "<sound>" )] 
	      	[Description( "Emote with sounds, words, and possibly an animation with one command!")] 
		private static void Emote_OnCommand( CommandEventArgs e )
		{
			Mobile pm = e.Mobile;
        	 	string em = e.ArgString.Trim();
			int SoundInt;
			switch( em )
			{
				case "ah":
					SoundInt = 1;
					break;
				case "ahha":
					SoundInt = 2;
					break;					
				case "aplaudir":
					SoundInt = 3;
					break;				
				case "assoar o nariz":
					SoundInt = 4;
					break;					
				case "reverência":
					SoundInt = 5;
					break;
				case "tossir 01":
					SoundInt = 6;
					break;
				case "arrotar":
					SoundInt = 7;
					break;
				case "limpar a garganta":
					SoundInt = 8;
					break;
				case "tossir 02":
					SoundInt = 9;
					break;
				case "chorar":
					SoundInt = 10;
					break;
				case "desmaiar":					
					SoundInt = 11;
					break;
				case "peidar":
					SoundInt = 12;
					break;
				case "arfar":
					SoundInt = 13;
					break;
				case "rir":
					SoundInt = 14;
					break;
				case "gemer":
					SoundInt = 15;
					break;
				case "rosnar":
					SoundInt = 16;
					break;
				case "hey":
					SoundInt = 17;
					break;
				case "soluçar":
					SoundInt = 18;
					break;
				case "huh":
					SoundInt = 19;
					break;
				case "beijar":
					SoundInt = 20;
					break;
				case "gargalhar":
					SoundInt = 21;
					break;
				case "não":
					SoundInt = 22;
					break;
				case "oh":
					SoundInt = 23;
					break;
				case "oooh":
					SoundInt = 24;
					break;
				case "oops":
					SoundInt = 25;
					break;
				case "vazio":
					SoundInt = 26;
					break;
				case "socar": 					
					SoundInt = 27;
					break;
				case "berrar":
					SoundInt = 28;
					break;
				case "xiu":
					SoundInt = 29;
					break;
				case "suspirar":
					SoundInt = 30;
					break;
				case "dar um tapa":
					SoundInt = 31;
					break;
				case "espirrar":
					SoundInt = 32;
					break;
				case "fungar":
					SoundInt = 33;
					break;
				case "roncar":
					SoundInt = 34;
					break;
				case "cuspir":
					SoundInt = 35;
					break;
				case "mostrar a língua":
					SoundInt = 36;
					break;
				case "sapatear":
					SoundInt = 37;
					break;
				case "assobiar":
					SoundInt = 38;
					break;
				case "comemorar":
					SoundInt = 39;
					break;
				case "bocejar":
					SoundInt = 40;
					break;
				case "sim":
					SoundInt = 41;
					break;
				case "gritar":
					SoundInt = 42;
					break;				
				default:
					SoundInt = 0;
					e.Mobile.SendGump( new EmoteGump( e.Mobile, EmotePage.P1) );
					break;
			}
			if ( SoundInt > 0 )
				new ESound( pm, SoundInt );
		} 
	}
	public class EmoteGump : Gump 
	{ 
		private Mobile m_From; 
		private EmotePage m_Page; 
		private const int Blanco = 0xFFFFFF; 
		private const int Azul = 0x8080FF; 
		public void AddPageButton( int x, int y, int buttonID, string text, EmotePage page, params EmotePage[] subpage ) 
		{ 
			bool seleccionado = ( m_Page == page ); 
			for ( int i = 0; !seleccionado && i < subpage.Length; ++i ) 
			seleccionado = ( m_Page == subpage[i] ); 
			AddButton( x, y - 1, seleccionado ? 4006 : 4005, 4007, buttonID, GumpButtonType.Reply, 0 ); 
			AddHtml( x + 35, y, 200, 20, Color( text, seleccionado ? Azul : Blanco ), false, false ); 
		} 
		public void AddButtonLabeled( int x, int y, int buttonID, string text ) 
		{ 
			AddButton( x, y - 1, 4005, 4007, buttonID, GumpButtonType.Reply, 0 ); 
			AddHtml( x + 35, y, 240, 40, Color( text, Blanco ), false, false ); 
		} 
		public int GetButtonID( int type, int index ) 
		{ 
			return 1 + (index * 15) + type; 
		} 
		public string Color( string text, int color ) 
		{ 
			return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text ); 
		} 
		public EmoteGump ( Mobile from, EmotePage page) : base ( 600, 50 ) 
		{ 
			from.CloseGump( typeof( EmoteGump ) ); 
			m_From = from; 
			m_Page = page; 
			Closable = true; 
			Dragable = true; 
			AddPage( 0 );
			AddBackground( 0, 65, 180, 360, 5054);
			AddAlphaRegion( 10, 70, 160, 350 );
			AddImageTiled( 10, 70, 160, 20, 9354);
			AddLabel( 13, 70, 200, "Lista de Emotes");
			AddImage( 150, 285, 10412);
			AddImage( 150, 130, 10411);
			AddImage( 150, 0, 10410);
			switch ( page ) 
			{
				case EmotePage.P1:
				{
					AddButtonLabeled( 10, 90, GetButtonID( 1, 1 ), "Ah");
					AddButtonLabeled( 10, 115, GetButtonID( 1, 2 ), "Ahha");
					AddButtonLabeled( 10, 140, GetButtonID( 1, 3 ), "Aplaudir");
					AddButtonLabeled( 10, 165, GetButtonID( 1, 4 ), "Assoar o Nariz");
					AddButtonLabeled( 10, 190, GetButtonID( 1, 5 ), "Reverência");
					AddButtonLabeled( 10, 215, GetButtonID( 1, 6 ), "Tossir 01");
					AddButtonLabeled( 10, 240, GetButtonID( 1, 7 ), "Arrotar");
					AddButtonLabeled( 10, 265, GetButtonID( 1, 8 ), "Limpar a Garganta");
					AddButtonLabeled( 10, 290, GetButtonID( 1, 9 ), "Tossir 02");
					AddButtonLabeled( 10, 315, GetButtonID( 1, 10 ), "Chorar");
					AddButtonLabeled( 10, 340, GetButtonID( 1, 11 ), "Desmaiar");
					AddButtonLabeled( 10, 365, GetButtonID( 1, 12 ), "Peidar");
					AddButton( 120, 380, 4502, 0504, GetButtonID( 0,2 ), GumpButtonType.Reply, 0 );
					break; 
				}
				case EmotePage.P2:
				{ 
					AddButtonLabeled( 10, 90, GetButtonID( 1, 13 ), "Arfar");
					AddButtonLabeled( 10, 115, GetButtonID( 1, 14 ), "Rir");
					AddButtonLabeled( 10, 140, GetButtonID( 1, 15 ), "Gemer");
					AddButtonLabeled( 10, 165, GetButtonID( 1, 16 ), "Rosnar");
					AddButtonLabeled( 10, 190, GetButtonID( 1, 17 ), "Hey");
					AddButtonLabeled( 10, 215, GetButtonID( 1, 18 ), "Soluçar");
					AddButtonLabeled( 10, 240, GetButtonID( 1, 19 ), "Huh");
					AddButtonLabeled( 10, 265, GetButtonID( 1, 20 ), "Beijar");
					AddButtonLabeled( 10, 290, GetButtonID( 1, 21 ), "Gargalhar");
					AddButtonLabeled( 10, 315, GetButtonID( 1, 22 ), "Não");
					AddButtonLabeled( 10, 340, GetButtonID( 1, 23 ), "Oh");
					AddButtonLabeled( 10, 365, GetButtonID( 1, 24 ), "Oooh");
					AddButton( 60, 380, 4506, 4508, GetButtonID( 0,1 ), GumpButtonType.Reply, 0 );
					AddButton( 120, 380, 4502, 0504, GetButtonID( 0,3 ), GumpButtonType.Reply, 0 );
					break; 
				} 
				case EmotePage.P3:
				{
					AddButtonLabeled( 10, 90, GetButtonID( 1, 25 ), "Oops");
					// AddButtonLabeled(10, 115, GetButtonID(1, 26), "vazio");
					AddButtonLabeled( 10, 115, GetButtonID( 1, 27 ), "Socar");
					AddButtonLabeled( 10, 140, GetButtonID( 1, 28 ), "Berrar");
					AddButtonLabeled( 10, 165, GetButtonID( 1, 29 ), "Xiu");
					AddButtonLabeled( 10, 190, GetButtonID( 1, 30 ), "Suspirar");
					AddButtonLabeled( 10, 215, GetButtonID( 1, 31 ), "Dar um Tapa");
					AddButtonLabeled( 10, 240, GetButtonID( 1, 32 ), "Espirrar");
					AddButtonLabeled( 10, 265, GetButtonID( 1, 33 ), "Fungar");
					AddButtonLabeled( 10, 290, GetButtonID( 1, 34 ), "Roncar");
					AddButtonLabeled( 10, 315, GetButtonID( 1, 35 ), "Cuspir");
					AddButtonLabeled( 10, 340, GetButtonID( 1, 36 ), "Mostrar a Língua");
					AddButton( 60, 380, 4506, 4508, GetButtonID( 0,2 ), GumpButtonType.Reply, 0 );
					AddButton( 120, 380, 4502, 0504, GetButtonID( 0,4 ), GumpButtonType.Reply, 0 );
					break; 
				} 
				case EmotePage.P4:
				{
					AddButtonLabeled( 10, 90, GetButtonID( 1, 37 ), "Sapatear");
					AddButtonLabeled( 10, 115, GetButtonID( 1, 38 ), "Assobiar");
					AddButtonLabeled( 10, 140, GetButtonID( 1, 39 ), "Comemorar");
					AddButtonLabeled( 10, 165, GetButtonID( 1, 40 ), "Bocejar");
					AddButtonLabeled( 10, 190, GetButtonID( 1, 41 ), "Sim");
					AddButtonLabeled( 10, 215, GetButtonID( 1, 42 ), "Gritar");
					AddButton( 60, 380, 4506, 4508, GetButtonID( 0,3 ), GumpButtonType.Reply, 0 );
					break; 
				} 
			} 
		} 
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info ) 
		{ 
			int val = info.ButtonID - 1; 
			if ( val < 0 ) 
			return; 
	
			Mobile from = m_From;
			int type = val % 15; 
			int index = val / 15; 
	
			switch ( type )
			{
				case 0:
				{
					EmotePage page;
						switch ( index )
					{
						case 1: page = EmotePage.P1; break;
						case 2: page = EmotePage.P2; break;
						case 3: page = EmotePage.P3; break;
						case 4: page = EmotePage.P4; break;
						default: return;
					}
	
					from.SendGump( new EmoteGump( from, page) );
					break;
				}
				case 1:
				{
					if ( index > 0 && index < 13 )
					{
						from.SendGump( new EmoteGump( from, EmotePage.P1) );
					}
					else if ( index > 12 && index < 25 )
					{
						from.SendGump( new EmoteGump( from, EmotePage.P2) );
					}
					else if ( index > 24 && index < 37 )
					{
						from.SendGump( new EmoteGump( from, EmotePage.P3) );
					}
					else if ( index > 36 && index < 43 )
					{
						from.SendGump( new EmoteGump( from, EmotePage.P4) );
					}
					new ESound( from, index );
					break; 
				} 
			} 
		}
	}
	public class ItemRemovalTimer : Timer 
	{ 
		private Item i_item; 
		public ItemRemovalTimer( Item item ) : base( TimeSpan.FromSeconds( 1.0 ) ) 
		{ 
			Priority = TimerPriority.OneSecond; 
			i_item = item; 
		} 

		protected override void OnTick() 
		{ 
		        if (( i_item != null ) && ( !i_item.Deleted ))
		        { 
			        i_item.Delete();
			        Stop();
		        }
		} 
	} 

	
	
	public class ESound
	{
		public ESound( Mobile pm, int SoundMade )
		{
			switch( SoundMade )
			{
				case 1:
					pm.PlaySound( pm.Female ? 778 : 1049 );
					pm.Say( "*ah!*" );
					break;
				case 2:
					pm.PlaySound( pm.Female ? 779 : 1050 );
					pm.Say( "*ah ha!*" );
					break;
				case 3:
					pm.PlaySound( pm.Female ? 780 : 1051 );
					pm.Say( "*Aplaudindo*" );
					break;
				case 4:
					pm.PlaySound( pm.Female ? 781 : 1052 );
					pm.Say( "*Assobiando*" );				
					if ( !pm.Mounted )
						pm.Animate( 34, 5, 1, true, false, 0 );
					break;
				case 5:
					pm.Say( "*Reverenciando*" );
					if ( !pm.Mounted )
						pm.Animate( 32, 5, 1, true, false, 0 );
					break;
				case 6:
					pm.PlaySound( pm.Female ? 786 : 1057 );
					pm.Say( "*Tossindo*" );
					break;
				case 7:
					pm.PlaySound( pm.Female ? 782 : 1053 );
					pm.Say( "*Arrotando*" );
					if ( !pm.Mounted )
						pm.Animate( 33, 5, 1, true, false, 0 );
					break;
				case 8:
					pm.PlaySound( pm.Female ? 785 : 1055 );
					pm.Say( "*Limpando a Garganta*" );
					if ( !pm.Mounted )
						pm.Animate( 33, 5, 1, true, false, 0 );
					break;
				case 9:
					pm.PlaySound( pm.Female ? 785 : 1056 );
					pm.Say( "*Tossindo*" );				
					if ( !pm.Mounted )
						pm.Animate( 33, 5, 1, true, false, 0 );
					break;
				case 10:
					pm.PlaySound( pm.Female ? 787 : 1058 );
					pm.Say( "*Chorando*" );
					break;
				case 11:
					pm.PlaySound( pm.Female ? 791 : 1063 );
					pm.Say( "*Desmaiando*" );
					if ( !pm.Mounted )
						pm.Animate( 22, 5, 1, true, false, 0 );
					break;
				case 12:
					pm.PlaySound( pm.Female ? 792 : 1064 );
					pm.Say( "*Peidando*" );
					break;
				case 13:
					pm.PlaySound( pm.Female ? 793 : 1065 );
					pm.Say( "*Arfando*" );
					break;
				case 14:
					pm.PlaySound( pm.Female ? 794 : 1066 );
					pm.Say( "*Rindo*" );
					break;
				case 15:
					pm.PlaySound( pm.Female ? 795 : 1067 );
					pm.Say( "*Gemendo*" );
					break;
				case 16:
					pm.PlaySound( pm.Female ? 796 : 1068 );
					pm.Say( "*Rosnando*" );
					break;
				case 17:
					pm.PlaySound( pm.Female ? 797 : 1069 );
					pm.Say( "*Hey!*" );
					break;
				case 18:
					pm.PlaySound( pm.Female ? 798 : 1070 );
					pm.Say( "*Soluçando!*" );
					break;
				case 19:
					pm.PlaySound( pm.Female ? 799 : 1071 );
					pm.Say( "*huh?*" );
					break;
				case 20:
					pm.PlaySound( pm.Female ? 800 : 1072 );
					pm.Say( "*Beijando*" );
					break;
				case 21:
					pm.PlaySound( pm.Female ? 794 : 1073 );
					pm.Say( "*Gargalhando*" );
					break;
				case 22:
					pm.PlaySound( pm.Female ? 802 : 1074 );
					pm.Say( "*Não!*" );
					break;
				case 23:
					pm.PlaySound( pm.Female ? 803 : 1075 );
					pm.Say( "*Oh!*" );
					break;
				case 24:
					pm.PlaySound( pm.Female ? 811 : 1085 );
					pm.Say( "*oooh*" );
					break;
				case 25:
					pm.PlaySound( pm.Female ? 812 : 1086 );
					pm.Say( "*oops*" );
					break;
				case 26:
					pm.PlaySound(pm.Female ? 812 : 1086);
					pm.Say("");
					break;
				case 27:
					pm.PlaySound( 315 );
					pm.Say( "*Socando*" );
					if ( !pm.Mounted )
						pm.Animate( 31, 5, 1, true, false, 0 );
					break;
				case 28:
					pm.PlaySound( pm.Female ? 814 : 1088 );
					pm.Say( "*ahhhh!*" );
					break;
				case 29:
					pm.PlaySound( pm.Female ? 815 : 1089 );
					pm.Say( "*Shhh!*" );
					break;
				case 30:
					pm.PlaySound( pm.Female ? 816 : 1090 );
					pm.Say( "*Suspirando*" );
					break;
				case 31:
					pm.PlaySound( 948 );
					pm.Say( "*Dando um Tapa*" );
					if ( !pm.Mounted )
						pm.Animate( 11, 5, 1, true, false, 0 );
					break;
				case 32:
					pm.PlaySound( pm.Female ? 817 : 1091 );
					pm.Say( "*Espirrando*" );
					if ( !pm.Mounted )
						pm.Animate( 32, 5, 1, true, false, 0 );
					break;
				case 33:
					pm.PlaySound( pm.Female ? 818 : 1092 );
					pm.Say( "*Fungando*" );
					if( !pm.Mounted )
						pm.Animate( 34, 5, 1, true, false, 0 );
					break;
				case 34:
					pm.PlaySound( pm.Female ? 819 : 1093 );
					pm.Say( "*Roncando*" );
					break;
				case 35:
					pm.PlaySound( pm.Female ? 820 : 1094 );
					pm.Say( "*Cuspindo*" );
					if ( !pm.Mounted )
						pm.Animate( 6, 5, 1, true, false, 0 );
					break;
				case 36:
					pm.PlaySound( 792 );
					pm.Say( "*Mostrando a língua*" );
					break;
				case 37:
					pm.PlaySound( 874 );
					pm.Say( "*Sapateando*" );
					if ( !pm.Mounted )
						pm.Animate( 38, 5, 1, true, false, 0 );
					break;
				case 38:
					pm.PlaySound( pm.Female ? 821 : 1095 );	
					pm.Say( "*Assobiando*" );
					if ( !pm.Mounted )
						pm.Animate( 5, 5, 1, true, false, 0 );
					break;
				case 39:
					pm.PlaySound( pm.Female ? 783 : 1054 );
					pm.Say( "*Comemorando*" );
					break;
				case 40:
					pm.PlaySound( pm.Female ? 822 : 1096 );
					pm.Say( "*Bocejando*" );
					if ( !pm.Mounted )
						pm.Animate( 17, 5, 1, true, false, 0 );
					break;
				case 41:
					pm.PlaySound( pm.Female ? 823 : 1097 );
					pm.Say( "*Sim!*" );
					break;
				case 42:
					pm.PlaySound( pm.Female ? 824 : 1098 );
					pm.Say( "*Gritando*" );
					break;
			}
		}
	}
}
