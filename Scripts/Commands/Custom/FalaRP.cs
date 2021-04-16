using Server.Mobiles;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Server.Commands
{
    class RPfala
    {
        const int sussurro = 0, fala = 1, falaprivada = 2, emote = 3, grito = 4, mensagem = 5, narrar = 6;
        public static void Initialize()
        {
            CommandSystem.Register("RPsussurro", AccessLevel.GameMaster, sussurro_OnCommand);
            CommandSystem.Register("RPfala", AccessLevel.GameMaster, RPfala_OnCommand);
            CommandSystem.Register("RPfalaprivada", AccessLevel.GameMaster, RPfalaprivada_OnCommand);
            CommandSystem.Register("RPemote", AccessLevel.GameMaster, RPemote_OnCommand);
            CommandSystem.Register("RPgrito", AccessLevel.GameMaster, RPgrito_OnCommand);
            CommandSystem.Register("RPmensagem", AccessLevel.GameMaster, RPmensagem_OnCommand);
            CommandSystem.Register("RPnarrar", AccessLevel.GameMaster, RPnarrar_OnCommand);
        }

        [Usage("RPsussurro [texto]")]
        [Description("Envia um sussurro como se fosse o personagem alvo.")]
        private static void sussurro_OnCommand(CommandEventArgs arg)
        {
            if (arg.Length < 1)
            {
                arg.Mobile.SendMessage("RPsussurro <texto>");
            }
            else
            {
                arg.Mobile.Target = new MobileTarget(sussurro, arg.ArgString);
            }
        }

        [Usage("RPfala [texto]")]
        [Description("Envia uma fala como se fosse o personagem alvo.")]
        private static void RPfala_OnCommand(CommandEventArgs arg)
        {
            if (arg.Length < 1)
            {
                arg.Mobile.SendMessage("RPfala <texto>");
            }
            else
            {
                arg.Mobile.Target = new MobileTarget(fala, arg.ArgString);
            }
        }

        [Usage("RPfalaprivada [texto]")]
        [Description("Envia um texto apenas para o personagem alvo.")]
        private static void RPfalaprivada_OnCommand(CommandEventArgs arg)
        {
            if (arg.Length < 1)
            {
                arg.Mobile.SendMessage("RPfalaprivada <texto>");
            }
            else
            {
                arg.Mobile.Target = new MobileTarget(falaprivada, arg.ArgString);
            }
        }

        [Usage("RPemote [texto]")]
        [Description("Envia um emote como se fosse o personagem alvo.")]
        private static void RPemote_OnCommand(CommandEventArgs arg)
        {
            if (arg.Length < 1)
            {
                arg.Mobile.SendMessage("RPemote <texto>");
            }
            else
            {
                arg.Mobile.Target = new MobileTarget(emote, arg.ArgString);
            }
        }

        [Usage("RPgrito [texto]")]
        [Description("Envia um grito como se fosse o personagem alvo.")]
        private static void RPgrito_OnCommand(CommandEventArgs arg)
        {
            if (arg.Length < 1)
            {
                arg.Mobile.SendMessage("RPgrito <texto>");
            }
            else
            {
                arg.Mobile.Target = new MobileTarget(grito, arg.ArgString);
            }
        }

        [Usage("RPmensagem [texto]")]
        [Description("Envia uma mensagem de sistema para o personagem alvo.")]
        private static void RPmensagem_OnCommand(CommandEventArgs arg)
        {
            if (arg.Length < 1)
            {
                arg.Mobile.SendMessage("RPmensagem <texto>");
            }
            else
            {
                arg.Mobile.Target = new MobileTarget(mensagem, arg.ArgString);
            }
        }

        [Usage("RPnarrar [texto]")]
        [Description("Envia um texto localizado na posição alvo.")]
        private static void RPnarrar_OnCommand(CommandEventArgs arg)
        {
            if (arg.Length < 1)
            {
                arg.Mobile.SendMessage("RPnarrador <texto>");
            }
            else
            {
                arg.Mobile.Target = new MobileTarget(narrar, arg.ArgString);
            }
        }

        public class MobileTarget : Target
        {
            private readonly int m_tipo;
            private readonly string m_texto;
            public MobileTarget(int tipo, string texto)
                : base(-1, false, TargetFlags.None)
            {
                m_tipo = tipo;
                m_texto = texto;
            }

            protected override void OnTarget(Mobile from, object target)
            {
                if (target is PlayerMobile)
                {
                    PlayerMobile alvo = (PlayerMobile)target;

                    switch (m_tipo)
                    {
                        case sussurro:
                            alvo.PublicOverheadMessage(Network.MessageType.Whisper, alvo.WhisperHue, true, m_texto);
                            break;
                        case fala:
                            alvo.PublicOverheadMessage(Network.MessageType.Regular, alvo.SpeechHue, true, m_texto);
                            break;
                        case falaprivada:
                            alvo.PrivateOverheadMessage(Network.MessageType.Regular, from.SpeechHue, true, m_texto, alvo.NetState);
                            break;
                        case emote:
                            alvo.PublicOverheadMessage(Network.MessageType.Emote, alvo.EmoteHue, true, m_texto);
                            break;
                        case grito:
                            alvo.PublicOverheadMessage(Network.MessageType.Yell, alvo.YellHue, true, m_texto);
                            break;
                        case mensagem:
                            alvo.SendMessage(from.SpeechHue, m_texto);
                            break;
                        case narrar:
                            alvo.PublicOverheadMessage(Network.MessageType.Emote, from.SpeechHue, true, m_texto);
                            break;
                        default:
                            break;
                    }
                    return;
                }
                else if (target is Mobile)
                {
                    Mobile alvo = (Mobile)target;

                    switch (m_tipo)
                    {
                        case sussurro:
                            alvo.PublicOverheadMessage(Network.MessageType.Whisper, alvo.WhisperHue, true, m_texto);
                            break;
                        case fala:
                            alvo.PublicOverheadMessage(Network.MessageType.Regular, alvo.SpeechHue, true, m_texto);
                            break;
                        case falaprivada:
                            alvo.PrivateOverheadMessage(Network.MessageType.Regular, from.SpeechHue, true, m_texto, alvo.NetState);
                            break;
                        case emote:
                            alvo.PublicOverheadMessage(Network.MessageType.Emote, alvo.EmoteHue, true, m_texto);
                            break;
                        case grito:
                            alvo.PublicOverheadMessage(Network.MessageType.Yell, alvo.YellHue, true, m_texto);
                            break;
                        case mensagem:
                            alvo.SendMessage(from.SpeechHue, m_texto);
                            break;
                        case narrar:
                            alvo.PublicOverheadMessage(Network.MessageType.Emote, from.SpeechHue, true, m_texto);
                            break;
                        default:
                            break;
                    }
                    return;
                }
                //Marcknight TODO: FAzer mensagens aparecerem em lugares específicos
                else
                {
                    from.SendMessage("O alvo precisa ser um pesonagem.");
                }
            }
        }
    }
}





/*
 * program textcmd_sayabove( who, text )
    SendSysMessage( who, "Say above what or whom?" );

    var what := Target( who );
    if (what)
        PrintTextAbove( what, text );
    endif
endprogram*/
