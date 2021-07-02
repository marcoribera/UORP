// une petite commande .hungry parce que j'en ai marre de pas savoir si j'ai faim ou pas
// j'aurais pu faire plus simple pour la commande mais je voulais quelle soit integrable dans le Help
// elle affiche un pti gump mimi , qui vous donne numériquement la valeur de votre faim et soif 

/* a small hungry order because I have some enough of step knowledge if I am hungry or not
I could have made simpler for the order but I wanted which is integrable in Help it posts
a pti gump mimi, which numerically gives you the value of your hunger and thirst */

using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Commands;

namespace Server.Commands
{
	public class Hungry
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Fome", AccessLevel.Player, new CommandEventHandler( Hungry_OnCommand ) );
					}
		
		[Usage( "Fome || Sede" )]
		[Description( "Mostra o seu nível de fome e sede." )]
		public static void Hungry_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( gumpfaim ) );
			from.SendGump( new gumpfaim ( from ) );
			
		}
		
	}
}

namespace Server.Gumps
{
	public class gumpfaim : Gump
	{
		
		public gumpfaim(Mobile from) : base(0,0)
		{
			Closable = true;
			Dragable = true;
			
			AddPage(0);
			
			AddBackground( 0, 0, /*295*/ 245, 144, 5054);
			AddBackground( 14, 27, /*261*/ 211, 100, 3500);
			AddLabel( 60, 62, from.Hunger < 6 ? 33 : 0, string.Format( "Fome: {0} / 20", from.Hunger));
			AddLabel( 60, 81, from.Thirst < 6 ? 33 : 0, string.Format( "Sede: {0} / 20", from.Thirst));
			AddItem( 8, 78, 8093);
			AddItem( 19, 60, 4155);
		}
		
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			
		}
	}
}
