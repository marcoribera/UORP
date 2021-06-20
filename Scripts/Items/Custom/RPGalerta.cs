using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;



namespace Server.Items
{
	public class RPalerta : WarningItem
	{
		private bool m_MensagemPrivada;
		public override bool OnlyToTriggerer => m_MensagemPrivada;

		[CommandProperty(AccessLevel.GameMaster)]
		public bool IsPrivateMsg { get => m_MensagemPrivada; set => m_MensagemPrivada = value; }

		[Constructable]
		public RPalerta() :base(0x1640, 6, "!!!Mensagem de alerta!!!") //ID do shackles
        {
            Visible = false;
            m_MensagemPrivada = true;
			ResetDelay = TimeSpan.FromSeconds(10);
		}
		public RPalerta(Serial serial) : base(serial)
		{
		}

        public override void SendMessage(Mobile triggerer, bool mensagemPrivada, string messageString, int messageNumber)
        {
            Item falador = new InvisibleTile(); //item "no draw"
            falador.MoveToWorld(Location, Map);
            falador.Movable = false;
            DeleteFaladorTimer apagador = new DeleteFaladorTimer(falador, TimeSpan.FromSeconds(10));
            if (mensagemPrivada)
            {
                falador.PrivateOverheadMessage(MessageType.Emote, triggerer.EmoteHue, false, messageString, triggerer.NetState);
            }
            else
            {
                if (messageString != null)
                    falador.PublicOverheadMessage(MessageType.Regular, triggerer.EmoteHue, false, messageString);
                else
                    falador.PublicOverheadMessage(MessageType.Regular, triggerer.EmoteHue, messageNumber);
            }
            apagador.Start();
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

        public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(0);

			writer.Write(m_MensagemPrivada);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch(version)
			{
				case 0:
					{
						m_MensagemPrivada = reader.ReadBool();
						break;
					}
			}
		}
	}
}
