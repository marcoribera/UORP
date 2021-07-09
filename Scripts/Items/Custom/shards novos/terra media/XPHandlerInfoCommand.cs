using System;
using Server;
using Server.Mobiles;
using Server.Targeting;

namespace Server.ME.Commands {
	public class XPHandlerInfoCommand {
		[Usage("XPHandlerInfo")]
		[Description("Zeigt Informationen über den XPHandler eines Spielers an")]
		args.Mobile.SendMessage("Über wessen XPHandler willst du Infos haben?");
			args.Mobile.Target = new InternalTarget();
		}

		private class InternalTarget : Target {
			public InternalTarget()
				: base(-1, false) {
			}

			protected override void OnTarget(Mobile from, object targeted) {
				if (targeted is PlayerMobile) {
					PlayerMobile mob = targeted as PlayerMobile;
					from.SendMessage("CharID: {0}", mob.CharID);
					mob.XPHandler.SendXPHandlerInfoTo(from);
				}
			}
		}
	}
}
