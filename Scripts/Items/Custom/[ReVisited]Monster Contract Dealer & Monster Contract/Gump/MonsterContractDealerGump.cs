using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class MonsterContractDealerGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "MonsterContractDealerGump", AccessLevel.GameMaster, new CommandEventHandler( MonsterContractDealerGump_OnCommand ) ); 
      } 

      private static void MonsterContractDealerGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new MonsterContractDealerGump( e.Mobile ) ); 
      } 

      public MonsterContractDealerGump( Mobile owner ) : base( 50,50 ) 
      { 
//----------------------------------------------------------------------------------------------------

				AddPage( 0 );
			AddImageTiled(  54, 33, 369, 400, 2624 );
			AddAlphaRegion( 54, 33, 369, 400 );

			AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
			
			AddImage( 97, 49, 9005 );
			AddImageTiled( 58, 39, 29, 390, 10460 );
			AddImageTiled( 412, 37, 31, 389, 10460 );
			AddLabel( 140, 60, 0x34, "Contrato de Caça" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>Ei! Você... venha aqui. Será que você poderia nos ajudar? Os anciões me instruiram a buscar ajuda e eu tenho um favor a lhe pedir.<BR><BR>As terras ao redor de nossa gruta sagrada tem se tornado cada vez mais perigosas. Eu tenho em minha posse, contratos para livrar a terra do mal e expulsar aqueles que vêm atacando nosso povo. Eu, infelizmente, meus dias de combate já passaram, mas terei o maior prazer em lhe dar os contratos e só ficarei com uma pequena comissão para mim se você conseguir concluí-los.<BR>" +
"<BASEFONT COLOR=YELLOW>Você tem interesse em um contrato??<BR><BR>Oh, Oh, muito obrigado! O contrato dirá quais perigos você deve eliminar de nossas terras. " + " </BODY>", false, true);

            AddImage( 430, 9, 10441);
			AddImageTiled( 40, 38, 17, 391, 9263 );
			AddImage( 6, 25, 10421 );
			AddImage( 34, 12, 10420 );
			AddImageTiled( 94, 25, 342, 15, 10304 );
			AddImageTiled( 40, 427, 415, 16, 10304 );
			AddImage( -10, 314, 10402 );
			AddImage( 56, 150, 10411 );
			AddImage( 155, 120, 2103 );
			AddImage( 136, 84, 96 );

			AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 ); 

//--------------------------------------------------------------------------------------------------------------
      } 

      public override void OnResponse( NetState state, RelayInfo info ) //Function for GumpButtonType.Reply Buttons 
      { 
         Mobile from = state.Mobile; 

         switch ( info.ButtonID ) 
         { 
            case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
            { 
               //Cancel 
               from.SendMessage("O contrato foi colocado na sua mochila. Obrigado por cuidar da floresta!");
               break; 
            } 

         }
      }
   }
}
