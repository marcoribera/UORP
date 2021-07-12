using System;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
    #region NetBook4
    [FlipableAttribute(0xFEF, 0xFF0, 0xFF1, 0xFF2, 0xFF3, 0xFF4, 0xFBD, 0xFBE)]
    public class NetBook4 : Item
    {
        private static string DefaultURL = "http://www.uotop100.com/"; // set default url here
        private string i_url;
        private bool i_Active = true;

        [CommandProperty(AccessLevel.GameMaster)]
        public string URL
        {
            get { return i_url; }
            set { i_url = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Active
        {
            get { return i_Active; }
            set { i_Active = value; }
        }

        [Constructable]
        public NetBook4(string url, string name)
            : base(0xFEF + Utility.Random(4))
        {
            Name = name;
            i_url = url;
        }

        [Constructable]
        public NetBook4(string url)
            : this(url, "a worn book")
        {
        }

        [Constructable]
        public NetBook4()
            : this(DefaultURL, "a worn book")
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (i_Active && i_url != null)
            {
                if (IsChildOf(from.Backpack) || from.InRange(this, 1))
                    from.SendGump(new NetBookGump4(from, i_url));
            }
        }

        public NetBook4(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)2);
            writer.Write(i_Active);
            writer.Write(i_url);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            switch (version)
            {
                case 2:
                    {
                        i_Active = reader.ReadBool();
                        goto case 1;
                    }
                case 1:
                    {
                        i_url = reader.ReadString();
                        break;
                    }
            }
        }
    }
    #endregion

    #region Gump
    public class NetBookGump4 : Gump
    {
        private string m_URL;

        public NetBookGump4(Mobile owner, string URL)
            : base(25, 25)
        {
            owner.CloseGump(typeof(NetBookGump4));

            m_URL = URL;

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);

            AddImage(0, 0, 0x898);
            AddButton(64, 160, 0xF7, 0xF8, 1, GumpButtonType.Reply, 0); // OK
            AddButton(228, 160, 0xF2, 0xF1, 2, GumpButtonType.Reply, 0); // Cancel

            string msg = "This will open a web page to vote for the shard. Do you wish to do so now?";
            AddHtml(36, 20, 112, 112, msg, false, false);
            AddHtml(200, 20, 112, 112, m_URL, false, false);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 1)
                state.Mobile.LaunchBrowser(m_URL);
        }
    }
    #endregion
}