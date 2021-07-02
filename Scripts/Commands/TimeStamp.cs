using System;

namespace Server.Commands
{
    public class TimeStamp
    {
        public static void Initialize()
        {
            CommandSystem.Register("Hora", AccessLevel.Player, new CommandEventHandler(CheckTime_OnCommand));
        }

        [Usage("TimeStamp")]
        [Description("Confira a hora do seu servidor")]
        public static void CheckTime_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            DateTime now = DateTime.UtcNow;
            m.SendMessage("O horário atual é" + now + "(GMT+3)");         
        }
    }
}