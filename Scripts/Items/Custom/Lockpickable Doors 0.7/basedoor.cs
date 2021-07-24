using System;
using System.Collections.Generic;
using Server.Commands;
using Server.Network;
using Server.Targeting;

using Server.Regions;
using Server.Mobiles;

namespace Server.Items
{
    public abstract class BaseDoor : Item, ILockable, ITelekinesisable, ILockpickable, ITrappable
    {
        private bool m_Open, m_Locked;
        private int m_OpenedID, m_OpenedSound;
        private int m_ClosedID, m_ClosedSound;
        private Point3D m_Offset;
        private BaseDoor m_Link;
        private uint m_KeyValue;

        private bool _timed;
        private TimeSpan _timeToClose;
        private Timer m_Timer;


        #region Door traps
        private BaseDoorTrap _trap;
        private DoorTrapType _trapType;
        private int _trapPower;
        private int _trapLevel;
        private bool _trapOnLockpick;

        public BaseDoorTrap DoorTrap { get { return _trap; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public DoorTrapType TrapType { get { return _trapType; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int TrapUsesRemaining { get { return (_trap == null ? 0 : _trap.Uses); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool TrapAvailable { get { return (_trap != null && _trap.Active); } }

        public virtual bool AttachTrap(BaseDoorTrap trap)
        {
            if (HasTrap() || trap == null)
                return false;

            _trap = trap;
            _trapType = trap.TrapType;

            trap.Active = true;
            trap.Door = this;

            return true;
        }

        public virtual bool HasTrap()
        {
            return (_trap != null && _trapType != DoorTrapType.None);
        }

        public virtual void RemoveTrap()
        {
            _trap = null;
            _trapType = DoorTrapType.None;
        }

        public virtual bool CanInstallTrap(Mobile m)
        {
            Region reg = Region.Find(m.Location, m.Map);

            return (!Locked && Open && (reg == null || !(reg is GuardedRegion)));
        }


        public bool Trapped
        {
            get { return TrapAvailable; }
        }
        #endregion


        #region ILockpickable implementation

        private bool _public;
        private bool _hasLock;
        private int _lockLevel, _maxLockLevel;
        private int _lockLvl;
        private int _maxLockLvl;
        private Mobile _picker;
        private int _requiredSkill;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsPublic { get { return _public; } set { _public = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int LockLevel { get { return _lockLvl; } set { _lockLvl = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxLockLevel { get { return _maxLockLvl; } set { _maxLockLvl = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Picker { get { return _picker; } set { _picker = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int RequiredSkill { get { return _requiredSkill; } set { _requiredSkill = value; } }

        public void LockPick(Mobile from)
        {
            if (TrapAvailable)
                _trap.Trigger(from);

            Locked = false;
            Picker = from;

            if (Link != null)
                Link.Picker = from;
        }

        public void Relock()
        {
            Locked = true;
            Picker = null;

            if (Link != null)
                Link.Picker = null;
        }
        #endregion


        private static readonly Point3D[] m_Offsets = new Point3D[]
        {
            new Point3D(-1, 1, 0),
            new Point3D(1, 1, 0),
            new Point3D(-1, 0, 0),
            new Point3D(1,-1, 0),
            new Point3D(1, 1, 0),
            new Point3D(1,-1, 0),
            new Point3D(0, 0, 0),
            new Point3D(0,-1, 0),
            new Point3D(0, 0, 0), //no base door nao tem
            new Point3D(0, 0, 0),
            new Point3D(0, 0, 0),
            new Point3D(0, 0, 0)
        };

      
        public BaseDoor(int closedID, int openedID, int openedSound, int closedSound, Point3D offset)
            : base(closedID)
        {
            m_OpenedID = openedID;
            m_ClosedID = closedID;
            m_OpenedSound = openedSound;
            m_ClosedSound = closedSound;
            m_Offset = offset;

            m_Timer = new InternalTimer(this);

            Movable = false;
        }

        public BaseDoor(Serial serial)
            : base(serial)
        {
            m_Timer = new InternalTimer(this);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Locked
        {
            get
            {
                return m_Locked;
            }
            set
            {
                m_Locked = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public uint KeyValue
        {
            get
            {
                return m_KeyValue;
            }
            set
            {
                m_KeyValue = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Open
        {
            get
            {
                return m_Open;
            }
            set
            {
                if (m_Open != value)
                {
                    m_Open = value;

                    ItemID = m_Open ? m_OpenedID : m_ClosedID;

                    if (m_Open)
                        Location = new Point3D(X + m_Offset.X, Y + m_Offset.Y, Z + m_Offset.Z);
                    else
                        Location = new Point3D(X - m_Offset.X, Y - m_Offset.Y, Z - m_Offset.Z);

                    Effects.PlaySound(this, Map, m_Open ? m_OpenedSound : m_ClosedSound);

                    if (m_Open)
                        m_Timer.Start();
                    else
                        m_Timer.Stop();
                }
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int OpenedID
        {
            get
            {
                return m_OpenedID;
            }
            set
            {
                m_OpenedID = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int ClosedID
        {
            get
            {
                return m_ClosedID;
            }
            set
            {
                m_ClosedID = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int OpenedSound
        {
            get
            {
                return m_OpenedSound;
            }
            set
            {
                m_OpenedSound = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int ClosedSound
        {
            get
            {
                return m_ClosedSound;
            }
            set
            {
                m_ClosedSound = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D Offset
        {
            get
            {
                return m_Offset;
            }
            set
            {
                m_Offset = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public BaseDoor Link
        {
            get
            {
                if (m_Link != null && m_Link.Deleted)
                    m_Link = null;

                return m_Link;
            }
            set
            {
                m_Link = value;
            }
        }
        public virtual bool UseChainedFunctionality
        {
            get
            {
                return false;
            }
        }
        // Called by RunUO
        public static void Initialize()
        {
            EventSink.OpenDoorMacroUsed += new OpenDoorMacroEventHandler(EventSink_OpenDoorMacroUsed);

            CommandSystem.Register("Link", AccessLevel.GameMaster, new CommandEventHandler(Link_OnCommand));
            CommandSystem.Register("ChainLink", AccessLevel.GameMaster, new CommandEventHandler(ChainLink_OnCommand));
        }

        [Usage("Link")]
        [Description("Links two targeted doors together.")]
        private static void Link_OnCommand(CommandEventArgs e)
        {
            e.Mobile.BeginTarget(-1, false, TargetFlags.None, new TargetCallback(Link_OnFirstTarget));
            e.Mobile.SendMessage("Target the first door to link.");
        }

        private static void Link_OnFirstTarget(Mobile from, object targeted)
        {
            BaseDoor door = targeted as BaseDoor;

            if (door == null)
            {
                from.BeginTarget(-1, false, TargetFlags.None, new TargetCallback(Link_OnFirstTarget));
                from.SendMessage("Isso não é uma porta. Tente denovo.");
            }
            else
            {
                from.BeginTarget(-1, false, TargetFlags.None, new TargetStateCallback(Link_OnSecondTarget), door);
                from.SendMessage("Target the second door to link.");
            }
        }

        private static void Link_OnSecondTarget(Mobile from, object targeted, object state)
        {
            BaseDoor first = (BaseDoor)state;
            BaseDoor second = targeted as BaseDoor;

            if (second == null)
            {
                from.BeginTarget(-1, false, TargetFlags.None, new TargetStateCallback(Link_OnSecondTarget), first);
                from.SendMessage("Isso não é uma porta. Tente denovo.");
            }
            else
            {
                first.Link = second;
                second.Link = first;
                from.SendMessage("The doors have been linked.");
            }
        }

        [Usage("ChainLink")]
        [Description("Chain-links two or more targeted doors together.")]
        private static void ChainLink_OnCommand(CommandEventArgs e)
        {
            e.Mobile.BeginTarget(-1, false, TargetFlags.None, new TargetStateCallback(ChainLink_OnTarget), new List<BaseDoor>());
            e.Mobile.SendMessage("Target the first of a sequence of doors to link.");
        }

        private static void ChainLink_OnTarget(Mobile from, object targeted, object state)
        {
            BaseDoor door = targeted as BaseDoor;

            if (door == null)
            {
                from.BeginTarget(-1, false, TargetFlags.None, new TargetStateCallback(ChainLink_OnTarget), state);
                from.SendMessage("That is not a door. Try again.");
            }
            else
            {
                List<BaseDoor> list = (List<BaseDoor>)state;

                if (list.Count > 0 && list[0] == door)
                {
                    if (list.Count >= 2)
                    {
                        for (int i = 0; i < list.Count; ++i)
                            list[i].Link = list[(i + 1) % list.Count];

                        from.SendMessage("The chain of doors have been linked.");
                    }
                    else
                    {
                        from.BeginTarget(-1, false, TargetFlags.None, new TargetStateCallback(ChainLink_OnTarget), state);
                        from.SendMessage("You have not yet targeted two unique doors. Target the second door to link.");
                    }
                }
                else if (list.Contains(door))
                {
                    from.BeginTarget(-1, false, TargetFlags.None, new TargetStateCallback(ChainLink_OnTarget), state);
                    from.SendMessage("You have already targeted that door. Target another door, or retarget the first door to complete the chain.");
                }
                else
                {
                    list.Add(door);

                    from.BeginTarget(-1, false, TargetFlags.None, new TargetStateCallback(ChainLink_OnTarget), state);

                    if (list.Count == 1)
                        from.SendMessage("Target the second door to link.");
                    else
                        from.SendMessage("Target another door to link. To complete the chain, retarget the first door.");
                }
            }
        }

        private static void EventSink_OpenDoorMacroUsed(OpenDoorMacroEventArgs args)
        {
            Mobile m = args.Mobile;

            if (m.Map != null)
            {
                int x = m.X, y = m.Y;

                switch (m.Direction & Direction.Mask)
                {
                    case Direction.North: --y; break;
                    case Direction.Right: ++x; --y; break;
                    case Direction.East: ++x; break;
                    case Direction.Down: ++x; ++y; break;
                    case Direction.South: ++y; break;
                    case Direction.Left: --x; ++y; break;
                    case Direction.West: --x; break;
                    case Direction.Up: --x; --y; break;
                }

                Sector sector = m.Map.GetSector(x, y);

                foreach (Item item in sector.Items)
                {
                    if (item.Location.X == x && item.Location.Y == y && (item.Z + item.ItemData.Height) > m.Z && (m.Z + 16) > item.Z && (item is BaseDoor) && m.CanSee(item) && m.InLOS(item))
                    {
                        if (m.CheckAlive())
                        {
                            if (item is BaseDoor)
                            {
                                if (((BaseDoor)item).Open)
                                    m.SendMessage("Closing door...");
                                else
                                    m.SendLocalizedMessage(500024); //Opening door...
                            }

                            item.OnDoubleClick(m);
                        }

                        break;
                    }
                }
            }
        }

        public static Point3D GetOffset(DoorFacing facing)
        {
            return m_Offsets[(int)facing];
        }

       

        private class InternalTimer : Timer
        {
            private BaseDoor m_Door;

            public InternalTimer(BaseDoor door)
                : base(door.TimeToClose)
            {
                Priority = TimerPriority.OneSecond;
                m_Door = door;
            }

            protected override void OnTick()
            {
                if (m_Door.Open && m_Door.Timed)
                    m_Door.Open = false;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DoorTrapType DoorTrapType
        {
            get
            {
                return _trapType;
            }
            set
            {
                _trapType = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int TrapPower
        {
            get
            {
                return _trapPower;
            }
            set
            {
                _trapPower = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int TrapLevel
        {
            get
            {
                return _trapLevel;
            }
            set
            {
                _trapLevel = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool TrapOnLockpick
        {
            get
            {
                return _trapOnLockpick;
            }
            set
            {
                _trapOnLockpick = value;
            }
        }

        public virtual bool TrapOnOpen { get { return true; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool HasLock
        {
            get
            {
                return _hasLock;
            }
            set
            {
                _hasLock = value;
            }
        }

      

       

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Timed
        {
            get { return _timed; }
            set
            {
                _timed = value;

                if (m_Timer != null && m_Timer.Running)
                    m_Timer.Stop();

                if (value)
                {
                    m_Timer = new InternalTimer(this);
                    m_Timer.Start();
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan TimeToClose
        {
            get { return _timeToClose; }
            set { _timeToClose = value; }
        }

        public bool CanClose()
        {
            if (!m_Open)
                return true;

            Map map = Map;

            if (map == null)
                return false;

            Point3D p = new Point3D(X - m_Offset.X, Y - m_Offset.Y, Z - m_Offset.Z);

            return CheckFit(map, p, 16);
        }

        private bool CheckFit(Map map, Point3D p, int height)
        {
            if (map == Map.Internal)
                return false;

            int x = p.X;
            int y = p.Y;
            int z = p.Z;

            Sector sector = map.GetSector(x, y);
            List<Item> items = sector.Items;
            List<Mobile> mobs = sector.Mobiles;

            for (int i = 0; i < items.Count; ++i)
            {
                Item item = items[i];

                if (item.ItemID < 0x4000 && item.AtWorldPoint(x, y) && !(item is BaseDoor))
                {
                    ItemData id = item.ItemData;
                    bool surface = id.Surface;
                    bool impassable = id.Impassable;

                    if ((surface || impassable) && (item.Z + id.CalcHeight) > z && (z + height) > item.Z)
                        return false;
                }
            }

            for (int i = 0; i < mobs.Count; ++i)
            {
                Mobile m = mobs[i];

                if (m.Location.X == x && m.Location.Y == y)
                {
                    if (m.Hidden && m.AccessLevel > AccessLevel.Player)
                        continue;

                    if (!m.Alive)
                        continue;

                    if ((m.Z + 16) > z && (z + height) > m.Z)
                        return false;
                }
            }

            return true;
        }

        public List<BaseDoor> GetChain()
        {
            List<BaseDoor> list = new List<BaseDoor>();
            BaseDoor c = this;

            do
            {
                list.Add(c);
                c = c.Link;
            }
            while (c != null && !list.Contains(c));

            return list;
        }

        public bool IsFreeToClose()
        {
            if (!UseChainedFunctionality)
                return CanClose();

            List<BaseDoor> list = GetChain();

            bool freeToClose = true;

            for (int i = 0; freeToClose && i < list.Count; ++i)
                freeToClose = list[i].CanClose();

            return freeToClose;
        }

        public virtual void OnTelekinesis(Mobile from)
        {
            Effects.SendLocationParticles(EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 5022);
            Effects.PlaySound(Location, Map, 0x1F5);

            Use(from);
        }

        public virtual bool IsInside(Mobile from)
        {
            return false;
        }

        public virtual bool UseLocks()
        {
            return true;
        }

        public virtual void Use(Mobile from)
        {
            if (m_Locked && !m_Open && UseLocks())
            {
                if (from.AccessLevel >= AccessLevel.GameMaster)
                {
                    from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 502502); // That is locked, but you open it with your godly powers.
                                                                                   //from.Send( new MessageLocalized( Serial, ItemID, MessageType.Regular, 0x3B2, 3, 502502, "", "" ) ); // That is locked, but you open it with your godly powers.
                }
                else if (Key.ContainsKey(from.Backpack, this.KeyValue))
                {
                    from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 501282); // You quickly unlock, open, and relock the door
                }
                else if (IsInside(from))
                {
                    from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 501280); // That is locked, but is usable from the inside.
                }
                else
                {
                    if (Hue == 0x44E && Map == Map.Malas) // doom door into healer room in doom
                        this.SendLocalizedMessageTo(from, 1060014); // Only the dead may pass.
                    else
                        from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 502503); // That is locked.

                    return;
                }
            }

            if (m_Open && !IsFreeToClose())
                return;

            if (m_Open)
                OnClosed(from);
            else
                OnOpened(from);

            if (UseChainedFunctionality)
            {
                bool open = !m_Open;

                List<BaseDoor> list = GetChain();

                for (int i = 0; i < list.Count; ++i)
                    list[i].Open = open;
            }
            else
            {
                Open = !m_Open;

                BaseDoor link = Link;

                if (m_Open && link != null && !link.Open)
                    link.Open = true;
            }
        }

        public virtual void OnOpened(Mobile from)
        {
        }

        public virtual void OnClosed(Mobile from)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel < AccessLevel.Counselor && (/*!from.InLOS( this ) || */!from.InRange(GetWorldLocation(), 2)))
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
            else
                Use(from);
        }

       

        

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2); // version

            writer.Write((int)_lockLevel);
            writer.Write((int)_maxLockLevel);
            writer.Write((int)_requiredSkill);
            writer.Write((bool)_hasLock);

            writer.Write(_timed);
            writer.Write(_timeToClose);

            writer.Write(m_KeyValue);

            writer.Write(m_Open);
            writer.Write(m_Locked);
            writer.Write(m_OpenedID);
            writer.Write(m_ClosedID);
            writer.Write(m_OpenedSound);
            writer.Write(m_ClosedSound);
            writer.Write(m_Offset);
            writer.Write(m_Link);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    {
                        _lockLevel = reader.ReadInt();
                        _maxLockLevel = reader.ReadInt();
                        _requiredSkill = reader.ReadInt();
                        _hasLock = reader.ReadBool();

                        goto case 1;
                    }
                case 1:
                    {
                        _timed = reader.ReadBool();
                        _timeToClose = reader.ReadTimeSpan();

                        goto case 0;
                    }
                case 0:
                    {
                        m_KeyValue = reader.ReadUInt();
                        m_Open = reader.ReadBool();
                        m_Locked = reader.ReadBool();
                        m_OpenedID = reader.ReadInt();
                        m_ClosedID = reader.ReadInt();
                        m_OpenedSound = reader.ReadInt();
                        m_ClosedSound = reader.ReadInt();
                        m_Offset = reader.ReadPoint3D();
                        m_Link = reader.ReadItem() as BaseDoor;

                        break;
                    }
            }
        }
    }
}
