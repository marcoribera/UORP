using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
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
                arg.Mobile.SendMessage("RPnarrar <texto>");
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
                : base(-1, true, TargetFlags.None)
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
                            alvo.DoSpeech(m_texto, new int[] { }, MessageType.Whisper, alvo.SpeechHue);
                            break;
                        case fala:
                            alvo.DoSpeech(m_texto, new int[] { }, MessageType.Regular, alvo.SpeechHue);
                            break;
                        case falaprivada:
                            alvo.PrivateOverheadMessage(MessageType.Regular, from.SpeechHue, true, m_texto, alvo.NetState);
                            break;
                        case emote:
                            alvo.DoSpeech(String.Format("*{0}*", m_texto), new int[] { }, MessageType.Emote, alvo.EmoteHue);
                            break;
                        case grito:
                            alvo.DoSpeech(m_texto, new int[] { }, MessageType.Yell, alvo.SpeechHue);
                            break;
                        case mensagem:
                            alvo.SendMessage(from.SpeechHue, m_texto);
                            break;
                        case narrar:
                            alvo.DoSpeech(String.Format("Narrador: *{0}*", m_texto), new int[] { 1 }, MessageType.Encoded, alvo.SpeechHue);
                            break;
                        default:
                            from.SendMessage("Esse tipo de alvo não aceita esse comando");
                            break;
                    }
                }
                else if (target is Mobile)
                {
                    Mobile alvo = (Mobile)target;

                    switch (m_tipo)
                    {
                        case sussurro:
                            alvo.DoSpeech(m_texto, new int[] { }, MessageType.Whisper, from.SpeechHue);
                            break;
                        case fala:
                            alvo.DoSpeech(m_texto, new int[] { }, MessageType.Regular, from.SpeechHue);
                            break;
                        case emote:
                            alvo.DoSpeech(String.Format("*{0}*", m_texto), new int[] { }, MessageType.Emote, from.EmoteHue);
                            break;
                        case grito:
                            alvo.DoSpeech(m_texto, new int[] { }, MessageType.Yell, from.SpeechHue);
                            break;
                        case narrar:
                            alvo.DoSpeech(String.Format("Narrador: *{0}*", m_texto), new int[] { 1 }, MessageType.Encoded, from.SpeechHue);
                            break;
                        default:
                            from.SendMessage("Esse tipo de alvo não aceita esse comando");
                            break;
                    }
                    return;
                }
                else if (target is Item)
                {
                    Item alvo = (Item)target;

                    switch (m_tipo)
                    {
                        case sussurro:
                            alvo.PublicOverheadMessage(MessageType.Whisper, from.SpeechHue, false, m_texto);
                            break;
                        case fala:
                            alvo.PublicOverheadMessage(MessageType.Regular, from.SpeechHue, false, m_texto);
                            break;
                        case emote:
                            alvo.PublicOverheadMessage(MessageType.Emote, from.EmoteHue, false, String.Format("*{0}*", m_texto));
                            break;
                        case grito:
                            alvo.PublicOverheadMessage(MessageType.Yell, from.SpeechHue, false, m_texto);
                            break;
                        case narrar:
                            alvo.PublicOverheadMessage(MessageType.Emote, from.SpeechHue, false, String.Format("Narrador: *{0}*", m_texto));
                            break;
                        default:
                            from.SendMessage("Esse tipo de alvo não aceita esse comando");
                            break;
                    }
                    return;
                }
                else if (target is StaticTarget)
                {
                    StaticTarget alvo = (StaticTarget) target;
                    Item falador = new Item(0x0001); //item "no draw"
                    falador.MoveToWorld(alvo.Location, from.Map);
                    falador.Movable = false;
                    DeleteFaladorTimer apagador = new DeleteFaladorTimer(falador, TimeSpan.FromSeconds(10));
                    
                    switch (m_tipo)
                    {
                        case sussurro:
                            falador.PublicOverheadMessage(MessageType.Whisper, from.SpeechHue, false, m_texto);
                            break;
                        case fala:
                            falador.PublicOverheadMessage(MessageType.Regular, from.SpeechHue, false, m_texto);
                            break;
                        case emote:
                            falador.PublicOverheadMessage(MessageType.Emote, from.EmoteHue, false, String.Format("*{0}*", m_texto));
                            break;
                        case grito:
                            falador.PublicOverheadMessage(MessageType.Yell, from.SpeechHue, false, m_texto);
                            break;
                        case narrar:
                            falador.PublicOverheadMessage(MessageType.Emote, from.SpeechHue, false, String.Format("Narrador: *{0}*", m_texto));
                            break;
                        default:
                            from.SendMessage("Esse tipo de alvo não aceita esse comando");
                            break;
                    }
                    apagador.Start();
                }
                else if (target is LandTarget)
                {
                    LandTarget alvo = (LandTarget)target;
                    Item falador = new Item(0x0001); //item "no draw"
                    falador.MoveToWorld(alvo.Location, from.Map);
                    falador.Movable = false;
                    DeleteFaladorTimer apagador = new DeleteFaladorTimer(falador, TimeSpan.FromSeconds(10));

                    switch (m_tipo)
                    {
                        case sussurro:
                            falador.PublicOverheadMessage(MessageType.Whisper, from.SpeechHue, false, m_texto);
                            break;
                        case fala:
                            falador.PublicOverheadMessage(MessageType.Regular, from.SpeechHue, false, m_texto);
                            break;
                        case emote:
                            falador.PublicOverheadMessage(MessageType.Emote, from.EmoteHue, false, String.Format("*{0}*", m_texto));
                            break;
                        case grito:
                            falador.PublicOverheadMessage(MessageType.Yell, from.SpeechHue, false, m_texto);
                            break;
                        case narrar:
                            falador.PublicOverheadMessage(MessageType.Emote, from.SpeechHue, false, String.Format("Narrador: *{0}*", m_texto));
                            break;
                        default:
                            from.SendMessage("Esse tipo de alvo não aceita esse comando");
                            break;
                    }
                    apagador.Start();
                }
                else
                {
                    from.SendMessage("Alvo inválido.");
                }
                return;
            }
        }

        private class DeleteFaladorTimer : Timer
        {
            private readonly Item m_Item;

            public DeleteFaladorTimer(Item item, TimeSpan duration)
                : base(duration)
            {
                Priority = TimerPriority.FiveSeconds;
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
                Stop();
            }
        }
    }
}
