using System;
using Server.Targeting;
using Server.Engines.Craft;

namespace Server.Items
{
    public class UtilityItem
    {
        static public int RandomChoice(int itemID1, int itemID2)
        {
            int iRet = 0;
            switch ( Utility.Random(2) )
            {
                default:
                case 0:
                    iRet = itemID1;
                    break;
                case 1:
                    iRet = itemID2;
                    break;
            }
            return iRet;
        }
    }

    // ********** Dough **********
    public class Dough : Item
    {
        private int m_MinSkill;
        private int m_MaxSkill;
        private Food m_CookedFood;

        public int MinSkill { get { return m_MinSkill; } }
        public int MaxSkill { get { return m_MaxSkill; } }
        public Food CookedFood { get { return m_CookedFood; } }

        [Constructable]
        public Dough() : base(0x103d)
        {
            m_MinSkill = 0;
            m_MaxSkill = 10;
            m_CookedFood = new BreadLoaf();
        }

        public Dough(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            from.Target = new InternalTarget(this);
        }

        private class InternalTarget : Target
        {
            private Dough m_Item;

            public InternalTarget(Dough item) : base(1, false, TargetFlags.None)
            {
                m_Item = item;
            }

            public static bool IsHeatSource(object targeted)
            {
                int itemID;

                if (targeted is Item)
                    itemID = ((Item)targeted).ItemID & 0x3FFF;
                else if (targeted is StaticTarget)
                    itemID = ((StaticTarget)targeted).ItemID & 0x3FFF;
                else
                    return false;

                if (itemID >= 0xDE3 && itemID <= 0xDE9)
                    return true;
                else if (itemID >= 0x461 && itemID <= 0x48E)
                    return true;
                else if (itemID >= 0x92B && itemID <= 0x96C)
                    return true;
                else if (itemID == 0xFAC)
                    return true;
                else if (itemID >= 0x398C && itemID <= 0x399F)
                    return true;
                else if (itemID == 0xFB1)
                    return true;
                else if (itemID >= 0x197A && itemID <= 0x19A9)
                    return true;
                else if (itemID >= 0x184A && itemID <= 0x184C)
                    return true;
                else if (itemID >= 0x184E && itemID <= 0x1850)
                    return true;

                return false;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted) return;

                if (IsHeatSource(targeted))
                {
                    if (from.BeginAction(typeof(Item)))
                    {
                        from.PlaySound(0x225);

                        m_Item.Consume();

                        InternalTimer t = new InternalTimer(from, targeted as IPoint3D, from.Map, m_Item.MinSkill, m_Item.MaxSkill, m_Item.CookedFood);
                        t.Start();
                    }
                    else
                    {
                        from.SendLocalizedMessage(500119);
                    }
                }
                else if (targeted is Eggs)
                {
                    if (!((Item)targeted).Movable) return;
                    if (((Item)targeted).Parent == null)
                        new UnbakedQuiche().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                    else
                        from.AddToBackpack(new UnbakedQuiche());
                    if (m_Item.Parent == null)
                        new Eggshells(m_Item.Hue).MoveToWorld(m_Item.Location, (m_Item.Map));
                    else
                        from.AddToBackpack(new Eggshells(m_Item.Hue));
                    m_Item.Consume();
                    ((Item)targeted).Consume();
                    from.SendMessage("You made an unbaked quiche");
                }
                else if (targeted is CheeseWedgeSmall)
                {
                    if (!((Item)targeted).Movable) return;
                    if (((Item)targeted).Parent == null)
                        new UncookedPizza("cheese").MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                    else
                        from.AddToBackpack(new UncookedPizza("cheese"));
                    m_Item.Consume();
                    ((Item)targeted).Consume();
                    from.SendMessage("You made an uncooked cheese pizza");
                }
                else if (targeted is JarHoney)
                {
                    if (!((Item)targeted).Movable) return;
                    if (((Item)targeted).Parent == null)
                        new SweetDough().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                    else
                        from.AddToBackpack(new SweetDough());
                    m_Item.Consume();
                    ((Item)targeted).Consume();
                    from.SendMessage("You made a sweet dough");
                }
                else if (targeted is ChickenLeg || targeted is RawChickenLeg)
                {
                    if (!((Item)targeted).Movable) return;
                    if (((Item)targeted).Parent == null)
                        new SweetDough().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                    else
                        from.AddToBackpack(new UnbakedChickenPotPie());
                    m_Item.Consume();
                    ((Item)targeted).Consume();
                    from.SendMessage("You made a chicken pot pie");
                }
                else if (targeted is Apple)
                {
                    if (!((Item)targeted).Movable) return;
                    if (((Item)targeted).Parent == null)
                        new UnbakedApplePie().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                    else
                        from.AddToBackpack(new UnbakedApplePie());
                    m_Item.Consume();
                    ((Item)targeted).Consume();
                    from.SendMessage("You made an unbaked apple pie");
                }
                else if (targeted is Peach)
                {
                    if (!((Item)targeted).Movable) return;
                    if (((Item)targeted).Parent == null)
                        new UnbakedPeachCobbler().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                    else
                        from.AddToBackpack(new UnbakedPeachCobbler());
                    m_Item.Consume();
                    ((Item)targeted).Consume();
                    from.SendMessage("You made an unbaked peach cobbler");
                }
                else if (targeted is Pumpkin)
                {
                    if (!((Item)targeted).Movable) return;
                    if (((Item)targeted).Parent == null)
                        new UnbakedPumpkinPie().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                    else
                        from.AddToBackpack(new UnbakedPumpkinPie());
                    m_Item.Consume();
                    ((Item)targeted).Consume();
                    from.SendMessage("You made an unbaked pumpkin pie");
                }
                else if (targeted is Lime)
                {
                    if (!((Item)targeted).Movable) return;
                    if (((Item)targeted).Parent == null)
                        new UnbakedKeyLimePie().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                    else
                        from.AddToBackpack(new UnbakedKeyLimePie());
                    m_Item.Consume();
                    ((Item)targeted).Consume();
                    from.SendMessage("You made an unbaked key lime pie");
                }
                else if (targeted is Dough)
                {
                    if (!((Item)targeted).Movable) return;
                    if (((Item)targeted).Parent == null)
                        new UncookedFrenchBread().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                    else
                        from.AddToBackpack(new UncookedFrenchBread());
                    m_Item.Consume();
                    ((Item)targeted).Consume();
                    from.SendMessage("You ... add some more dough onto the dough");
                }
                else if (targeted is UncookedFrenchBread)
                {
                    if (!((Item)targeted).Movable) return;
                    if (((Item)targeted).Parent == null)
                        new UncookedDonuts().MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                    else
                        from.AddToBackpack(new UncookedDonuts());
                    m_Item.Consume();
                    ((Item)targeted).Consume();
                    from.SendMessage("You fumble around for a bit with even more dough, and eventually make these round doughy things");
                }

                try
                {
                    if (targeted is FromageDeVacheWedgeSmall)
                    {
                        if (!((Item)targeted).Movable) return;
                        if (((Item)targeted).Parent == null)
                            new UncookedPizza("cheese").MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                        else
                            from.AddToBackpack(new UncookedPizza("cheese"));
                        m_Item.Consume();
                        ((Item)targeted).Consume();
                        from.SendMessage("You made an uncooked cheese pizza");
                    }
                    else if (targeted is FromageDeBrebisWedgeSmall)
                    {
                        if (!((Item)targeted).Movable) return;
                        if (((Item)targeted).Parent == null)
                            new UncookedPizza("sheep cheese").MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                        else
                            from.AddToBackpack(new UncookedPizza("sheep cheese"));
                        m_Item.Consume();
                        ((Item)targeted).Consume();
                        from.SendMessage("You made an uncooked sheep cheese pizza");
                    }
                    else if (targeted is FromageDeChevreWedgeSmall)
                    {
                        if (!((Item)targeted).Movable) return;
                        if (((Item)targeted).Parent == null)
                            new UncookedPizza("goat cheese").MoveToWorld(((Item)targeted).Location, ((Item)targeted).Map);
                        else
                            from.AddToBackpack(new UncookedPizza("goat cheese"));
                        m_Item.Consume();
                        ((Item)targeted).Consume();
                        from.SendMessage("You made an uncooked goat cheese pizza");
                    }
                }
                catch
                {
                }
            }

            private class InternalTimer : Timer
            {
                private Mobile m_From;
                private IPoint3D m_Point;
                private Map m_Map;
                private int Min;
                private int Max;
                private Food m_CookedFood;

                public InternalTimer(Mobile from, IPoint3D p, Map map, int min, int max, Food cookedFood) : base(TimeSpan.FromSeconds(1.0))
                {
                    m_From = from;
                    m_Point = p;
                    m_Map = map;
                    Min = min;
                    Max = max;
                    m_CookedFood = cookedFood;
                }

                protected override void OnTick()
                {
                    m_From.EndAction(typeof(Item));

                    if (m_From.Map != m_Map || (m_Point != null && m_From.GetDistanceToSqrt(m_Point) > 3))
                    {
                        m_From.SendLocalizedMessage(500686);
                        return;
                    }

                    if (m_From.CheckSkill(SkillName.Culinaria, Min, Max))
                    {
                        if (m_From.AddToBackpack(m_CookedFood))
                            m_From.PlaySound(0x57);
                    }
                    else
                    {
                        m_From.SendLocalizedMessage(500686);
                    }
                }
            }
        }
    }

    // ********** SweetDough **********
    public class SweetDough : Item
    {
        public override int LabelNumber { get { return 1041340; } }
        private int m_MinSkill;
        private int m_MaxSkill;
        private Food m_CookedFood;

        public int MinSkill { get { return m_MinSkill; } }
        public int MaxSkill { get { return m_MaxSkill; } }
        public Food CookedFood { get { return m_CookedFood; } }

        [Constructable]
        public SweetDough() : base(0x103d)
        {
            Hue = 51;
            m_MinSkill = 5;
            m_MaxSkill = 20;
            m_CookedFood = new Muffins(3);

        }

        public SweetDough(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            from.Target = new InternalTarget(this);
        }

        private class InternalTarget : Target
        {
            private SweetDough m_Item;

            public InternalTarget(SweetDough item) : base(1, false, TargetFlags.None)
            {
                m_Item = item;
            }

            public static bool IsHeatSource(object targeted)
            {
                int itemID;

                if (targeted is Item)
                    itemID = ((Item)targeted).ItemID & 0x3FFF;
                else if (targeted is StaticTarget)
                    itemID = ((StaticTarget)targeted).ItemID & 0x3FFF;
                else
                    return false;

                if (itemID >= 0xDE3 && itemID <= 0xDE9)
                    return true;
                else if (itemID >= 0x461 && itemID <= 0x48E)
                    return true;
                else if (itemID >= 0x92B && itemID <= 0x96C)
                    return true;
                else if (itemID == 0xFAC)
                    return true;
                else if (itemID >= 0x398C && itemID <= 0x399F)
                    return true;
                else if (itemID == 0xFB1)
                    return true;
                else if (itemID >= 0x197A && itemID <= 0x19A9)
                    return true;
                else if (itemID >= 0x184A && itemID <= 0x184C)
                    return true;
                else if (itemID >= 0x184E && itemID <= 0x1850)
                    return true;

                return false;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted) return;

                if (targeted is BowlFlour)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You made a cake mix");
                    if (m_Item.Parent == null)
                        new CakeMix().MoveToWorld(m_Item.Location, m_Item.Map);
                    else
                        from.AddToBackpack(new CakeMix());
                    m_Item.Consume();
                    ((BowlFlour)targeted).Use(from);
                }

                else if (targeted is JarHoney)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You made a cookie mix");
                    if (m_Item.Parent == null)
                        new CookieMix().MoveToWorld(m_Item.Location, m_Item.Map);
                    else
                        from.AddToBackpack(new CookieMix());
                    m_Item.Consume();
                    ((JarHoney)targeted).Consume();
                }
                else if (IsHeatSource(targeted))
                {
                    if (from.BeginAction(typeof(Item)))
                    {
                        from.PlaySound(0x225);

                        m_Item.Consume();

                        InternalTimer t = new InternalTimer(from, targeted as IPoint3D, from.Map, m_Item.MinSkill, m_Item.MaxSkill, m_Item.CookedFood);
                        t.Start();
                    }
                    else
                    {
                        from.SendLocalizedMessage(500119);
                    }
                }
            }

            private class InternalTimer : Timer
            {
                private Mobile m_From;
                private IPoint3D m_Point;
                private Map m_Map;
                private int Min;
                private int Max;
                private Food m_CookedFood;

                public InternalTimer(Mobile from, IPoint3D p, Map map, int min, int max, Food cookedFood) : base(TimeSpan.FromSeconds(1.0))
                {
                    m_From = from;
                    m_Point = p;
                    m_Map = map;
                    Min = min;
                    Max = max;
                    m_CookedFood = cookedFood;

                }

                protected override void OnTick()
                {
                    m_From.EndAction(typeof(Item));

                    if (m_From.Map != m_Map || (m_Point != null && m_From.GetDistanceToSqrt(m_Point) > 3))
                    {
                        m_From.SendLocalizedMessage(500686);
                        return;
                    }

                    if (m_From.CheckSkill(SkillName.Culinaria, Min, Max))
                    {
                        if (m_From.AddToBackpack(m_CookedFood))
                            m_From.PlaySound(0x57);
                    }
                    else
                    {
                        m_From.PlaySound(0x57);

                        m_From.SendLocalizedMessage(500686);
                    }
                }
            }
        }
    }

    // ********** JarHoney **********
    public class JarHoney : Item
    {
        [Constructable]
        public JarHoney()
            : base(0x9ec)
        {
            Weight = 1.0;
            Stackable = true;
        }

        public JarHoney(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            Stackable = true;
        }

        /*public override void OnDoubleClick( Mobile from )
        {
        if ( !Movable )
        return;

        from.Target = new InternalTarget( this );
        }*/

        private class InternalTarget : Target
        {
            private readonly JarHoney m_Item;

            public InternalTarget(JarHoney item)
                : base(1, false, TargetFlags.None)
            {
                m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted)
                    return;

                if (targeted is Dough)
                {
                    m_Item.Delete();
                    ((Dough)targeted).Consume();

                    from.AddToBackpack(new SweetDough());
                }
				
                if (targeted is BowlFlour)
                {
                    m_Item.Consume();
                    ((BowlFlour)targeted).Delete();

                    from.AddToBackpack(new CookieMix());
                }
            }
        }
    }

    // ********** BowlFlour **********
    public class BowlFlour : Item, IUsesRemaining
    {
        private int m_Uses;

        public bool ShowUsesRemaining { get { return true; } set { } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Uses
        {
            get { return m_Uses; }
            set { m_Uses = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int UsesRemaining
        {
            get { return m_Uses; }
            set { m_Uses = value; InvalidateProperties(); }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1060584, m_Uses.ToString());
        }

        public virtual void DisplayDurabilityTo(Mobile m)
        {
            LabelToAffix(m, 1017323, Network.AffixType.Append, ": " + m_Uses.ToString());
        }

        public override void OnSingleClick(Mobile from)
        {
            DisplayDurabilityTo(from);

            base.OnSingleClick(from);
        }

        [Constructable]
        public BowlFlour() : this(10)
        {
        }

        [Constructable]
        public BowlFlour(int StartingUses) : base(0xa1e)
        {
            Weight = 2.0;
            m_Uses = StartingUses;
        }

        public BowlFlour(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);

            writer.Write((int)m_Uses);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_Uses = reader.ReadInt();
                        break;
                    }
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            from.Target = new InternalTarget(this);
        }

        public void Use(Mobile from)
        {
            m_Uses--;
            InvalidateProperties();

            if (m_Uses <= 0)
            {
                if (Parent == null)
                    new WoodenBowl().MoveToWorld(this.Location, this.Map);
                else
                    from.AddToBackpack(new WoodenBowl());
                Consume();
            }
        }

        private class InternalTarget : Target
        {
            private BowlFlour m_Item;

            public InternalTarget(BowlFlour item) : base(1, false, TargetFlags.None)
            {
                m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted) return;

                if (targeted is Pitcher)
                {
                    if (!((Item)targeted).Movable) return;

                    if (BaseBeverage.ConsumeTotal(from.Backpack, typeof(Pitcher), BeverageType.Water, 1))
                    {
                        Effects.PlaySound(from.Location, from.Map, 0x240);
                        from.AddToBackpack(new Dough());
                        from.SendMessage("You made some dough and put it them in your backpack");
                        m_Item.Use(from);
                    }
                }

                if (targeted is SweetDough)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You made a cake mix");
                    if (((SweetDough)targeted).Parent == null)
                        new CakeMix().MoveToWorld(((SweetDough)targeted).Location, ((SweetDough)targeted).Map);
                    else
                        from.AddToBackpack(new CakeMix());
                    ((SweetDough)targeted).Consume();
                    m_Item.Use(from);
                }

                if (targeted is TribalBerry)
                {
                    if (!((Item)targeted).Movable) return;

                    if (from.Skills[SkillName.Culinaria].Base >= 80.0)
                    {
                        m_Item.Use(from);
                        ((TribalBerry)targeted).Delete();

                        from.AddToBackpack(new TribalPaint());

                        from.SendLocalizedMessage(1042002);
                    }
                    else
                    {
                        from.SendLocalizedMessage(1042003);
                    }
                }
            }
        }
    }

    // ********** WoodenBowl **********
    public class WoodenBowl : Item
    {
        [Constructable]
        public WoodenBowl()
            : base(0x15f8)
        {
            Weight = 1.0;
        }

        public WoodenBowl(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    // ********** SackFlour **********
    [TypeAlias("Server.Items.SackFlourOpen")]
    public class SackFlour : Item, IHasQuantity
    {
        private int m_Quantity;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Quantity
        {
            get { return m_Quantity; }
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > 20)
                    value = 20;

                m_Quantity = value;

                InvalidateProperties();

                if (m_Quantity == 0)
                    Delete();
                else if (m_Quantity < 20 && (ItemID == 0x1039 || ItemID == 0x1045))
                    ++ItemID;

            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1060584, m_Quantity.ToString());
        }

        [Constructable]
        public SackFlour() : base(0x1039)
        {
            m_Quantity = 20;
        }

        public SackFlour(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);

            writer.Write((int)m_Quantity);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_Quantity = reader.ReadInt();
                        break;
                    }
                case 0:
                    {
                        m_Quantity = 20;
                        break;
                    }
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if ((ItemID == 0x1039 || ItemID == 0x1045))
                ++ItemID;
        }

    }

    // ********** SackFlourOpen **********
    public class SackFlourOpen : Item
	{
		public override int LabelNumber{ get{ return 1024166; } } // open sack of flour

		[Constructable]
		public SackFlourOpen() : base(0x103A)
		{
			Weight = 4.0;
		}

		public SackFlourOpen( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

    // ********** Eggshells **********
    public class Eggshells : Item
    {
        [Constructable]
        public Eggshells() : this(0)
        {
        }

        [Constructable]
        public Eggshells(int hue) : base(0x9b4)
        {
            Hue = hue;
            Weight = 0.5;
        }

        public Eggshells(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class WheatSheaf : Item
    {
        [Constructable]
        public WheatSheaf()
            : this(1)
        {
        }

        [Constructable]
        public WheatSheaf(int amount)
            : base(7869)
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            from.BeginTarget(4, false, TargetFlags.None, new TargetCallback(OnTarget));
        }

        public virtual void OnTarget(Mobile from, object obj)
        {
            if (obj is AddonComponent)
                obj = (obj as AddonComponent).Addon;

            IFlourMill mill = obj as IFlourMill;

            if (mill != null)
            {
                int needs = mill.MaxFlour - mill.CurFlour;

                if (needs > Amount)
                    needs = Amount;

                mill.CurFlour += needs;
                Consume(needs);
            }
        }

        public WheatSheaf(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FishHeads : Food
    {
        [Constructable]
        public FishHeads() : this(1)
        {
        }

        [Constructable]
        public FishHeads(int amount) : base(Utility.Random(7705, 2))
        {
            Weight = 0.1;
            Amount = amount;
            this.FillFactor = 0;
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage("*ugh*! That's cat food!");
            return;
        }

        public FishHeads(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
