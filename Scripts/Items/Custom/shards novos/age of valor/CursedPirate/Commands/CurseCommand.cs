/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\engines\CursedPirate\Commands\CurseCommand.cs
Lines of code: 73
***********************************************************/


using System;
using Server.Items;
using System.Collections;
using Server.Misc;
using Server.Mobiles;
using Server.Gumps;
using Server.Targeting;
using Server.Commands;

namespace Server.Commands
{
    public class PirateCurseCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("pcurse", AccessLevel.Administrator, new CommandEventHandler(pc_OnCommand));
            CommandSystem.Register("rpcurse", AccessLevel.Administrator, new CommandEventHandler(rpc_OnCommand));
        }

        [Usage("pcurse")]
        [Description("Temporarily curse a player with the cursed pirate curse.")]
        public static void pc_OnCommand(CommandEventArgs e)
        {
            PlayerMobile from = e.Mobile as PlayerMobile;

            if (from != null)
                from.Target = new InternalTarget(from, true);
        }

        [Usage("rpcurse")]
        [Description("Remove curse from a player with the cursed pirate curse.")]
        public static void rpc_OnCommand(CommandEventArgs e)
        {
            PlayerMobile from = e.Mobile as PlayerMobile;

            if (from != null)
                from.Target = new InternalTarget(from, false);
        }

        private class InternalTarget : Target
        {
            private Mobile m_From;
            private bool m_Curse;

            public InternalTarget(Mobile from, bool curse)
                : base(8, false, TargetFlags.None)
            {
                m_From = from;
                m_Curse = curse;

                if (m_Curse)
                {
                    m_From.SendMessage("Target a player to curse with the pirate curse.");
                }
                else
                {
                    m_From.SendMessage("Target a player to remove the pirate curse from.");
                }
            }

            protected override void OnTarget(Mobile from, object obj)
            {
                PlayerMobile target = obj as PlayerMobile;

                if (target == null || target.AccessLevel > AccessLevel.Player)
                {
                    m_From.SendMessage("That is not a player!");
                }
                else
                {
                    if (m_Curse)
                    {
                        PirateCurse.CursePlayer(target);
                    }
                    else
                    {
                        PirateCurse.RemoveCurse(target);
                    }
                }
            }
        }
    }
}
