using System;
using Server.Targeting;
using Server.Engines.Craft;

namespace Server.Items
{
    public abstract class CookableFood : Item, IQuality, ICommodity
    {
        private ItemQuality _Quality;
        private int m_CookingLevel;

        [CommandProperty(AccessLevel.GameMaster)]
        public int CookingLevel
        {
            get
            {
                return m_CookingLevel;
            }
            set
            {
                m_CookingLevel = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ItemQuality Quality { get { return _Quality; } set { _Quality = value; InvalidateProperties(); } }

        public bool PlayerConstructed { get { return true; } }

        public CookableFood(int itemID, int cookingLevel)
            : base(itemID)
        {
            m_CookingLevel = cookingLevel;
        }

        public CookableFood(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description { get { return LabelNumber; } }
        bool ICommodity.IsDeedable { get { return true; } }

        public override void AddCraftedProperties(ObjectPropertyList list)
        {
            if (_Quality == ItemQuality.Exceptional)
            {
                list.Add(1060636); // Exceptional
            }
        }

        public virtual int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            Quality = (ItemQuality)quality;

            return quality;
        }

        public abstract Food Cook();

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2); // version

            writer.Write((int)_Quality);

            // Version 1
            writer.Write((int)m_CookingLevel);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    {
                        _Quality = (ItemQuality)reader.ReadInt();
                        goto case 1;
                    }
                case 1:
                    {
                        m_CookingLevel = reader.ReadInt();
                        break;
                    }
            }
        }

#if false
		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			from.Target = new InternalTarget( this );
		}
#endif

        public static bool IsHeatSource(object targeted)
        {
            int itemID;

            if (targeted is Item)
                itemID = ((Item)targeted).ItemID;
            else if (targeted is StaticTarget)
                itemID = ((StaticTarget)targeted).ItemID;
            else
                return false;

            if (itemID >= 0xDE3 && itemID <= 0xDE9)
                return true; // Campfire
            else if (itemID >= 0x461 && itemID <= 0x48E)
                return true; // Sandstone oven/fireplace
            else if (itemID >= 0x92B && itemID <= 0x96C)
                return true; // Stone oven/fireplace
            else if (itemID == 0xFAC)
                return true; // Firepit
            else if (itemID >= 0x184A && itemID <= 0x184C)
                return true; // Heating stand (left)
            else if (itemID >= 0x184E && itemID <= 0x1850)
                return true; // Heating stand (right)
            else if (itemID >= 0x398C && itemID <= 0x399F)
                return true; // Fire field

            return false;
        }

        private class InternalTarget : Target
        {
            private readonly CookableFood m_Item;

            public InternalTarget(CookableFood item)
                : base(1, false, TargetFlags.None)
            {
                m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted)
                    return;

                if (CookableFood.IsHeatSource(targeted))
                {
                    if (from.BeginAction(typeof(CookableFood)))
                    {
                        from.PlaySound(0x225);

                        m_Item.Consume();

                        InternalTimer t = new InternalTimer(from, targeted as IPoint3D, from.Map, m_Item);
                        t.Start();
                    }
                    else
                    {
                        from.SendLocalizedMessage(500119); // You must wait to perform another action
                    }
                }
            }

            private class InternalTimer : Timer
            {
                private readonly Mobile m_From;
                private readonly IPoint3D m_Point;
                private readonly Map m_Map;
                private readonly CookableFood m_CookableFood;

                public InternalTimer(Mobile from, IPoint3D p, Map map, CookableFood cookableFood)
                    : base(TimeSpan.FromSeconds(5.0))
                {
                    m_From = from;
                    m_Point = p;
                    m_Map = map;
                    m_CookableFood = cookableFood;
                }

                protected override void OnTick()
                {
                    m_From.EndAction(typeof(CookableFood));

                    if (m_From.Map != m_Map || (m_Point != null && m_From.GetDistanceToSqrt(m_Point) > 3))
                    {
                        m_From.SendLocalizedMessage(500686); // You burn the food to a crisp! It's ruined.
                        return;
                    }

                    if (m_From.CheckSkill(SkillName.Culinaria, m_CookableFood.CookingLevel, 100))
                    {
                        Food cookedFood = m_CookableFood.Cook();

                        if (m_From.AddToBackpack(cookedFood))
                            m_From.PlaySound(0x57);
                    }
                    else
                    {
                        m_From.SendLocalizedMessage(500686); // You burn the food to a crisp! It's ruined.
                    }
                }
            }
        }
    }

    // ********** RawRibs **********
    public class RawRibs : CookableFood
    {
        [Constructable]
        public RawRibs()
            : this(1)
        {
        }

        [Constructable]
        public RawRibs(int amount)
            : base(0x9F1, 10)
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        public RawRibs(Serial serial)
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

        public override Food Cook()
        {
            return new Ribs();
        }
    }

    // ********** RawLambLeg **********
    public class RawLambLeg : CookableFood
    {
        [Constructable]
        public RawLambLeg()
            : this(1)
        {
        }

        [Constructable]
        public RawLambLeg(int amount)
            : base(0x1609, 10)
        {
            Stackable = true;
            Amount = amount;
        }

        public RawLambLeg(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0 && Weight == 1)
                Weight = -1;
        }

        public override Food Cook()
        {
            return new LambLeg();
        }
    }

    // ********** RawChickenLeg **********
    public class RawChickenLeg : CookableFood
    {
        [Constructable]
        public RawChickenLeg()
            : base(0x1607, 10)
        {
            Weight = 1.0;
            Stackable = true;
        }

        public RawChickenLeg(Serial serial)
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

        public override Food Cook()
        {
            return new ChickenLeg();
        }
    }

    // ********** RawBird **********
    public class RawBird : CookableFood
    {
        [Constructable]
        public RawBird()
            : this(1)
        {
        }

        [Constructable]
        public RawBird(int amount)
            : base(0x9B9, 10)
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        public RawBird(Serial serial)
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

        public override Food Cook()
        {
            return new CookedBird();
        }
    }

    // ********** UnbakedPeachCobbler **********
    public class UnbakedPeachCobbler : CookableFood
    {
        public override int LabelNumber
        {
            get
            {
                return 1041335;
            }
        }// unbaked peach cobbler

        [Constructable]
        public UnbakedPeachCobbler()
            : base(0x1042, 25)
        {
            Weight = 1.0;
        }

        public UnbakedPeachCobbler(Serial serial)
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

        public override Food Cook()
        {
            return new PeachCobbler();
        }
    }

    // ********** UnbakedFruitPie **********
    public class UnbakedFruitPie : CookableFood
    {
        public override int LabelNumber
        {
            get
            {
                return 1041334;
            }
        }// unbaked fruit pie

        [Constructable]
        public UnbakedFruitPie()
            : base(0x1042, 25)
        {
            Weight = 1.0;
        }

        public UnbakedFruitPie(Serial serial)
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

        public override Food Cook()
        {
            return new FruitPie();
        }
    }

    // ********** UnbakedMeatPie **********
    public class UnbakedMeatPie : CookableFood
    {
        public override int LabelNumber
        {
            get
            {
                return 1041338;
            }
        }// unbaked meat pie

        [Constructable]
        public UnbakedMeatPie()
            : base(0x1042, 25)
        {
            Weight = 1.0;
        }

        public UnbakedMeatPie(Serial serial)
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

        public override Food Cook()
        {
            return new MeatPie();
        }
    }

    // ********** UnbakedPumpkinPie **********
    public class UnbakedPumpkinPie : CookableFood
    {
        public override int LabelNumber
        {
            get
            {
                return 1041342;
            }
        }// unbaked pumpkin pie

        [Constructable]
        public UnbakedPumpkinPie()
            : base(0x1042, 25)
        {
            Weight = 1.0;
        }

        public UnbakedPumpkinPie(Serial serial)
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

        public override Food Cook()
        {
            return new PumpkinPie();
        }
    }

    // ********** UnbakedApplePie **********
    public class UnbakedApplePie : CookableFood
    {
        public override int LabelNumber
        {
            get
            {
                return 1041336;
            }
        }// unbaked apple pie

        [Constructable]
        public UnbakedApplePie()
            : base(0x1042, 25)
        {
            Weight = 1.0;
        }

        public UnbakedApplePie(Serial serial)
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

        public override Food Cook()
        {
            return new ApplePie();
        }
    }

    // ********** UncookedCheesePizza **********
    [TypeAlias("Server.Items.UncookedPizza")]
    public class UncookedCheesePizza : CookableFood
    {
        public override int LabelNumber
        {
            get
            {
                return 1041341;
            }
        }// uncooked cheese pizza

        [Constructable]
        public UncookedCheesePizza()
            : base(0x1083, 20)
        {
            Weight = 1.0;
        }

        public UncookedCheesePizza(Serial serial)
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

            if (ItemID == 0x1040)
                ItemID = 0x1083;

            if (Hue == 51)
                Hue = 0;
        }

        public override Food Cook()
        {
            return new CheesePizza();
        }
    }

    // ********** UncookedSausagePizza **********
    public class UncookedSausagePizza : CookableFood
    {
        public override int LabelNumber
        {
            get
            {
                return 1041337;
            }
        }// uncooked sausage pizza

        [Constructable]
        public UncookedSausagePizza()
            : base(0x1083, 20)
        {
            Weight = 1.0;
        }

        public UncookedSausagePizza(Serial serial)
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

        public override Food Cook()
        {
            return new SausagePizza();
        }
    }

#if false
	// ********** UncookedPizza **********
	public class UncookedPizza : CookableFood
	{
		[Constructable]
		public UncookedPizza() : base( 0x1083, 20 )
		{
			Weight = 1.0;
		}

		public UncookedPizza( Serial serial ) : base( serial )
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

			if ( ItemID == 0x1040 )
				ItemID = 0x1083;

			if ( Hue == 51 )
				Hue = 0;
		}

		public override Food Cook()
		{
			return new Pizza();
		}
	}
#endif

    // ********** UnbakedQuiche **********
    public class UnbakedQuiche : CookableFood
    {
        public override int LabelNumber
        {
            get
            {
                return 1041339;
            }
        }// unbaked quiche

        [Constructable]
        public UnbakedQuiche()
            : base(0x1042, 25)
        {
            Weight = 1.0;
        }

        public UnbakedQuiche(Serial serial)
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

        public override Food Cook()
        {
            return new Quiche();
        }
    }

    // ********** Eggs **********
    public class Eggs : CookableFood
    {
        [Constructable]
        public Eggs()
            : this(1)
        {
        }

        [Constructable]
        public Eggs(int amount)
            : base(0x9B5, 15)
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        public Eggs(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version < 1)
            {
                Stackable = true;

                if (Weight == 0.5)
                    Weight = 1.0;
            }
        }

        public override Food Cook()
        {
            return new FriedEggs();
        }
    }

    // ********** BrightlyColoredEggs **********
    public class BrightlyColoredEggs : CookableFood
    {
        public override string DefaultName
        {
            get
            {
                return "brightly colored eggs";
            }
        }

        [Constructable]
        public BrightlyColoredEggs()
            : base(0x9B5, 15)
        {
            Weight = 0.5;
            Hue = 3 + (Utility.Random(20) * 5);
        }

        public BrightlyColoredEggs(Serial serial)
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

        public override Food Cook()
        {
            return new FriedEggs();
        }
    }

    // ********** EasterEggs **********
    public class EasterEggs : CookableFood
    {
        public override int LabelNumber
        {
            get
            {
                return 1016105;
            }
        }// Easter Eggs

        [Constructable]
        public EasterEggs()
            : base(0x9B5, 15)
        {
            Weight = 0.5;
            Hue = 3 + (Utility.Random(20) * 5);
        }

        public EasterEggs(Serial serial)
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

        public override Food Cook()
        {
            return new FriedEggs();
        }
    }

    // ********** CookieMix **********
    public class CookieMix : CookableFood
    {

        [Constructable]
        public CookieMix()
            : base(0x103F, 20)
        {
            Weight = 1.0;
        }

        public CookieMix(Serial serial)
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

        public override Food Cook()
        {
            return new Cookies();
        }
    }

    // ********** CakeMix **********
    public class CakeMix : CookableFood
    {
        public override int LabelNumber
        {
            get
            {
                return 1041002;
            }
        }// cake mix

        [Constructable]
        public CakeMix()
            : base(0x103F, 40)
        {
            Weight = 1.0;
        }

        public CakeMix(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override Food Cook()
        {
            return new Cake();
        }
    }

    public class RawFishSteak : CookableFood, ICommodity
    {
        public override double DefaultWeight
        {
            get
            {
                return 0.1;
            }
        }

        [Constructable]
        public RawFishSteak()
            : this(1)
        {
        }

        [Constructable]
        public RawFishSteak(int amount)
            : base(0x097A, 10)
        {
            Stackable = true;
            Amount = amount;
        }

        public RawFishSteak(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description { get { return LabelNumber; } }
        bool ICommodity.IsDeedable { get { return true; } }

        public override Food Cook()
        {
            return new FishSteak();
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

    public class RawRotwormMeat : CookableFood
    {
        [Constructable]
        public RawRotwormMeat()
            : this(1)
        {
        }

        [Constructable]
        public RawRotwormMeat(int amount)
            : base(0x2DB9, 10)
        {
            Stackable = true;
            Weight = 0.1;
            Amount = amount;
        }

        public RawRotwormMeat(Serial serial)
            : base(serial)
        {
        }

        public override Food Cook()
        {
            return null;
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

    public class PizzaCrust : CookableFood
    {
        [Constructable]
        public PizzaCrust() : base(0x1083, 20)
        {
            Weight = 0.5;
            Name = "Pizza Crust";
            Hue = 0x3FF;
        }
        public PizzaCrust(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
        public override Food Cook() { return new CheesePizza(); }
    }

    public class RawHalibutSteak : CookableFood
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RawHalibutSteak() : this(1) { }

        [Constructable]
        public RawHalibutSteak(int amount) : base(0x097A, 10)
        {
            Stackable = true;
            Amount = amount;
            Name = "Raw Halibut Steak";
        }

        public RawHalibutSteak(Serial serial) : base(serial) { }

        public override Food Cook()
        {
            return new HalibutFishSteak();
        }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class RawFlukeSteak : CookableFood
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RawFlukeSteak() : this(1) { }

        [Constructable]
        public RawFlukeSteak(int amount) : base(0x097A, 10)
        {
            Stackable = true;
            Amount = amount;
            Name = "Raw Fluke Steak";
        }

        public RawFlukeSteak(Serial serial) : base(serial) { }

        public override Food Cook()
        {
            return new FlukeFishSteak();
        }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RawMahiSteak : CookableFood
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RawMahiSteak() : this(1) { }

        [Constructable]
        public RawMahiSteak(int amount) : base(0x097A, 10)
        {
            Stackable = true;
            Amount = amount;
            Name = "Raw Mahi-Mahi Steak";
        }

        public RawMahiSteak(Serial serial) : base(serial) { }

        public override Food Cook()
        {
            return new MahiFishSteak();
        }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RawSalmonSteak : CookableFood
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RawSalmonSteak() : this(1) { }

        [Constructable]
        public RawSalmonSteak(int amount) : base(0x097A, 10)
        {
            Stackable = true;
            Amount = amount;
            Name = "Raw Salmon Steak";
        }

        public RawSalmonSteak(Serial serial) : base(serial) { }

        public override Food Cook()
        {
            return new SalmonFishSteak();
        }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RawRedSnapperSteak : CookableFood
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RawRedSnapperSteak() : this(1) { }

        [Constructable]
        public RawRedSnapperSteak(int amount) : base(0x097A, 10)
        {
            Stackable = true;
            Amount = amount;
            Name = "Raw Red Snapper Steak";
        }

        public RawRedSnapperSteak(Serial serial) : base(serial) { }

        public override Food Cook()
        {
            return new RedSnapperFishSteak();
        }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RawParrotFishSteak : CookableFood
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RawParrotFishSteak() : this(1) { }

        [Constructable]
        public RawParrotFishSteak(int amount) : base(0x097A, 10)
        {
            Stackable = true;
            Amount = amount;
            Name = "Raw Parrot Fish Steak";
        }

        public RawParrotFishSteak(Serial serial) : base(serial) { }

        public override Food Cook()
        {
            return new ParrotFishSteak();
        }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RawTroutSteak : CookableFood
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RawTroutSteak() : this(1) { }

        [Constructable]
        public RawTroutSteak(int amount) : base(0x097A, 10)
        {
            Stackable = true;
            Amount = amount;
            Name = "Raw Trout Steak";
        }

        public RawTroutSteak(Serial serial) : base(serial) { }

        public override Food Cook()
        {
            return new TroutFishSteak();
        }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RawShrimp : CookableFood
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RawShrimp() : this(1) { }

        [Constructable]
        public RawShrimp(int amount) : base(0x097A, 10)
        {
            Stackable = true;
            Amount = amount;
            Name = "Raw Shrimp";
        }

        public RawShrimp(Serial serial) : base(serial) { }

        public override Food Cook()
        {
            return new CookedShrimp();
        }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class RawSteak : CookableFood
    {
        [Constructable]
        public RawSteak() : this(1) { }

        [Constructable]
        public RawSteak(int amount) : base(0x3BCE, 10)
        {
            Name = "Raw Steak";
            Stackable = true;
            Amount = amount;
        }

        public RawSteak(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

        public override Food Cook()
        {
            return new CookedSteak();
        }
    }

    public class RawBacon : CookableFood
    {
        [Constructable]
        public RawBacon() : this(1) { }

        [Constructable]
        public RawBacon(int amount) : base(0x979, 0)
        {
            Name = "raw slice of bacon";
            Stackable = true;
            Amount = amount;
            Hue = 336;
        }

        public RawBacon(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

        public override Food Cook()
        {
            return new Bacon();
        }
    }

    public class RawBaconSlab : CookableFood, ICarvable
    {
        [Constructable]
        public RawBaconSlab() : this(1) { }

        [Constructable]
        public RawBaconSlab(int amount) : base(0x976, 0)
        {
            Name = "raw slab of bacon";
            Stackable = true;
            Hue = 41;
            Amount = amount;
        }

        public bool Carve(Mobile from, Item item)
        {
            if (!Movable)
                return false;

            base.ScissorHelper(from, new RawBacon(), 5);
            from.SendMessage("You cut the slab into slices.");
            return true;
        }

        public RawBaconSlab(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

        public override Food Cook()
        {
            return new SlabOfBacon();
        }
    }

    public class RawHam : CookableFood, ICarvable
    {
        [Constructable]
        public RawHam() : this(1) { }

        [Constructable]
        public RawHam(int amount) : base(0x9C9, 0)
        {
            Name = "raw ham";
            Stackable = true;
            Hue = 41;
            Amount = amount;
        }

        public bool Carve(Mobile from, Item item)
        {
            if (!Movable)
                return false;

            base.ScissorHelper(from, new RawHamSlices(), 5);
            from.SendMessage("You slice the ham.");
            return true;
        }

        public RawHam(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

        public override Food Cook()
        {
            return new Ham();
        }
    }

    public class RawHamSlices : CookableFood
    {
        [Constructable]
        public RawHamSlices() : this(1) { }

        [Constructable]
        public RawHamSlices(int amount) : base(0x1E1F, 0)
        {
            Name = "raw sliced ham";
            Stackable = true;
            Amount = amount;
            Hue = 336;
        }

        public RawHamSlices(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

        public override Food Cook()
        {
            return new HamSlices();
        }
    }

    public class BananaCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public BananaCakeMix() : base(0x103F, 75)
        {
            Name = "banana cake mix";
            Hue = 354;
        }

        public BananaCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new BananaCake();
        }

    }

    

    public class BrightEggs : Eggs
    {
        [Constructable]
        public BrightEggs() : this(1) { }

        [Constructable]
        public BrightEggs(int amount) : base()
        {
            Stackable = true;
            Amount = amount;
            Name = "brightly colored eggs";
            Hue = Utility.RandomList(0x135, 0xcd, 0x38, 0x3b, 0x42, 0x4f, 0x11e, 0x60, 0x317, 0x10, 0x136, 0x1f9, 0x1a, 0xeb, 0x86, 0x2e, 0x0497, 0x0481);
        }

        public BrightEggs(Serial serial) : base(serial)
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

    public class CantaloupeCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public CantaloupeCakeMix() : base(0x103F, 75)
        {
            Name = "cantaloupe cake mix";
            Hue = 145;
        }

        public CantaloupeCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new CantaloupeCake();
        }

    }

    public class CarrotCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public CarrotCakeMix() : base(0x103F, 75)
        {
            Name = "carrot cake mix";
            Hue = 248;
        }

        public CarrotCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new CarrotCake();
        }

    }

    public class CoconutCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public CoconutCakeMix() : base(0x103F, 75)
        {
            Name = "coconut cake mix";
            Hue = 556;
        }

        public CoconutCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new CoconutCake();
        }

    }

    public class FruitCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public FruitCakeMix() : base(0x103F, 75)
        {
            Name = "fruit cake mix";

        }

        public FruitCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new FruitCake(this.Hue);
        }

    }

    public class GrapeCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public GrapeCakeMix() : base(0x103F, 75)
        {
            Name = "grape cake mix";
            Hue = 21;
        }

        public GrapeCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new GrapeCake();
        }

    }

    public class HoneydewMelonCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public HoneydewMelonCakeMix() : base(0x103F, 75)
        {
            Name = "honeydew melon cake mix";
            Hue = 61;
        }

        public HoneydewMelonCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new HoneydewMelonCake();
        }

    }

    public class KeyLimeCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public KeyLimeCakeMix() : base(0x103F, 75)
        {
            Name = "key lime cake mix";
            Hue = 71;
        }

        public KeyLimeCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new KeyLimeCake();
        }

    }

    public class LemonCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public LemonCakeMix() : base(0x103F, 75)
        {
            Name = "lemon cake mix";
            Hue = 53;
        }

        public LemonCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new LemonCake();
        }

    }

    public class MeatCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public MeatCakeMix() : base(0x103F, 75)
        {
            Name = "meat cake mix";

        }

        public MeatCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new MeatCake(this.Hue);
        }

    }

    public class PeachCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public PeachCakeMix() : base(0x103F, 75)
        {
            Name = "peach cake mix";
            Hue = 46;
        }

        public PeachCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new PeachCake();
        }

    }

    public class PlateOfCookies : Food
    {
        [Constructable]
        public PlateOfCookies() : this(1)
        {
        }

        [Constructable]
        public PlateOfCookies(int amount) : base(amount, 0x160C)
        {
            this.Weight = 0.2;
            this.FillFactor = 1;
            this.Name = "plate of cookies";
        }

        public PlateOfCookies(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

        }
    }
    public class PumpkinCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public PumpkinCakeMix() : base(0x103F, 75)
        {
            Name = "pumpkin cake mix";
            Hue = 243;
        }

        public PumpkinCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new PumpkinCake();
        }

    }

    public class RawHeadlessFish : CookableFood, ICarvable
    {
        public bool Carve(Mobile from, Item item)
        {
            base.ScissorHelper(from, new RawFishSteak(), 4);
            return true;
        }

        [Constructable]
        public RawHeadlessFish() : this(1)
        {
        }

        [Constructable]
        public RawHeadlessFish(int amount) : base(Utility.Random(7703, 2), 20)
        {
            Stackable = true;
            Weight = 0.6;
            Amount = amount;
        }

        public override Food Cook()
        {
            return new CookedHeadlessFish();
        }

        public RawHeadlessFish(Serial serial) : base(serial)
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

    public class RawScaledFish : Item, ICarvable
    {
        public bool Carve(Mobile from, Item item)
        {
            if (!Movable)
                return false;

            base.ScissorHelper(from, new RawHeadlessFish(), 1);
            from.AddToBackpack(new FishHeads(item.Amount));
            return true;
        }

        [Constructable]
        public RawScaledFish() : this(1)
        {
        }

        [Constructable]
        public RawScaledFish(int amount) : base(Utility.Random(7701, 2))
        {
            Stackable = true;
            Weight = 0.8;
            Amount = amount;
        }

        public RawScaledFish(Serial serial) : base(serial)
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

    public class UnbakedChickenPotPie : CookableFood
    {
        [Constructable]
        public UnbakedChickenPotPie() : base(0x1042, 25)
        {
            Name = "unbaked chicken pot pie";
        }

        public UnbakedChickenPotPie(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new ChickenPotPie();
        }

    }

    public class UnbakedKeyLimePie : CookableFood
    {
        [Constructable]
        public UnbakedKeyLimePie() : base(0x1042, 25)
        {
            Name = "unbaked key lime pie";
        }

        public UnbakedKeyLimePie(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new KeyLimePie();
        }
    }

    public class UnbakedVegePie : CookableFood
    {
        [Constructable]
        public UnbakedVegePie() : this(null, 0)
        {
        }

        [Constructable]
        public UnbakedVegePie(string Desc) : this(Desc, 0)
        {
        }

        [Constructable]
        public UnbakedVegePie(int Color) : this(null, Color)
        {
        }

        [Constructable]
        public UnbakedVegePie(string Desc, int Color) : base(0x1042, 25)
        {

            if (Desc != "" && Desc != null)
                Name = "unbaked " + Desc + " vegetable pie";
            else
                Name = "unbaked vegetable pie";

            this.Hue = Color;
        }

        public UnbakedVegePie(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new VegePie();
        }
    }

    public class UncookedDonuts : CookableFood
    {
        [Constructable]
        public UncookedDonuts() : base(6867, 0)
        {
            Hue = 51;
            Name = "uncooked donuts";
        }

        public UncookedDonuts(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new Donuts();
        }
    }

    public class UncookedFrenchBread : CookableFood
    {
        [Constructable]
        public UncookedFrenchBread() : base(0x98C, 0)
        {
            Hue = 51;
            Name = "uncooked french bread";
        }

        public UncookedFrenchBread(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new FrenchBread();
        }
    }

    public class UncookedPizza : Item
    {
        public override int LabelNumber { get { return 1024227; } }

        private Food m_CookedFood { get { return new Pizza(this.Desc, this.Hue); } }

        private int m_MinSkill;
        private int m_MaxSkill;

        private string m_Desc;

        public string Desc
        {
            get { return m_Desc; }
            set
            {
                m_Desc = value;
                Name = "uncooked " + m_Desc + " pizza";
                InvalidateProperties();
            }
        }

        public Food CookedFood { get { return m_CookedFood; } }

        public int MinSkill { get { return m_MinSkill; } }
        public int MaxSkill { get { return m_MaxSkill; } }

        [Constructable]
        public UncookedPizza() : this("cheese")
        {
        }

        [Constructable]
        public UncookedPizza(string desc) : this(desc, 0)
        {
        }

        [Constructable]
        public UncookedPizza(int Color) : this("cheese", Color)
        {
        }

        [Constructable]
        public UncookedPizza(string desc, int Color) : base(0x1083)
        {
            if (Color != 0)
                Hue = Color;

            if (desc != "" && desc != null)
            {
                Desc = desc;
            }

            m_MinSkill = 0;
            m_MaxSkill = 100;
        }

        public UncookedPizza(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);

            writer.Write((string)m_Desc);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_Desc = reader.ReadString();
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

        public class InternalTarget : Target
        {
            private UncookedPizza m_Item;

            public InternalTarget(UncookedPizza item) : base(1, false, TargetFlags.None)
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

                else if (m_Item.Desc.Length > 80)
                {
                    from.SendMessage("The pizza has enough toppings already.");
                    return;
                }

                else if (targeted is Apple)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add apple to the pizza.");
                    m_Item.Desc += ", apple";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Banana || targeted is Bananas)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add banana to the pizza.");
                    m_Item.Desc += ", banana";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Cantaloupe)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add cantaloupe to the pizza.");
                    m_Item.Desc += ", cantaloupe";
                    ((Item)targeted).Consume();
                }

                else if (targeted is Coconut || targeted is SplitCoconut)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add coconut to the pizza.");
                    m_Item.Desc += ", coconut";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Cucumber)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add cucumber to the pizza.");
                    m_Item.Desc += ", cucumber";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Dates)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add dates to the pizza.");
                    m_Item.Desc += ", date";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Grapes)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add grape to the pizza.");
                    m_Item.Desc += ", grape";
                    ((Item)targeted).Consume();
                }
                else if (targeted is HoneydewMelon)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add honeydew melon to the pizza.");
                    m_Item.Desc += ", honeydew melon";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Lemon)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add lemon to the pizza.");
                    m_Item.Desc += ", lemon";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Lime)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add lime to the pizza.");
                    m_Item.Desc += ", lime";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Orange)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add orange to the pizza.");
                    m_Item.Desc += ", orange";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Peach)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add peach to the pizza.");
                    m_Item.Desc += ", peach";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Pear)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add pear to the pizza.");
                    m_Item.Desc += ", pear";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Pumpkin)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add pumpkin to the pizza.");
                    m_Item.Desc += ", pumpkin";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Tomato)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add tomato to the pizza.");
                    m_Item.Desc += ", tomato";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Watermelon)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add watermelon to the pizza.");
                    m_Item.Desc += ", watermelon";
                    ((Item)targeted).Consume();
                }

                else if (targeted is RawBacon || targeted is Bacon)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add bacon to the pizza.");
                    m_Item.Desc += ", bacon";
                    ((Item)targeted).Consume();
                }
                else if (targeted is ChickenLeg || targeted is RawChickenLeg)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add chicken meat to the pizza.");
                    m_Item.Desc += ", chicken";
                    ((Item)targeted).Consume();
                }
                else if (targeted is CookedBird || targeted is RawBird)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add bird meat to the pizza.");
                    m_Item.Desc += ", bird";
                    ((Item)targeted).Consume();
                }
                else if (targeted is FishSteak || targeted is RawFishSteak)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add fish to the pizza.");
                    m_Item.Desc += ", fish";
                    ((Item)targeted).Consume();
                }
                else if (targeted is RawHamSlices || targeted is HamSlices)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add ham to the pizza.");
                    m_Item.Desc += ", ham";
                    ((Item)targeted).Consume();
                }
                else if (targeted is LambLeg || targeted is RawLambLeg)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add lamb meat to the pizza.");
                    m_Item.Desc += ", lamb";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Ribs || targeted is RawRibs)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add ribs to the pizza.");
                    m_Item.Desc += ", ribs";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Sausage)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add sausage to the pizza.");
                    m_Item.Desc += ", sausage";
                    ((Item)targeted).Consume();
                }

                else if (targeted is Dough)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You make the pizza a thick crust one.");
                    m_Item.Desc += ", thick crust";
                    ((Item)targeted).Consume();
                }
                else if (targeted is TanMushroom || targeted is RedMushroom)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add mushrooms to the pizza.");
                    m_Item.Desc += ", mushrooms";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Silverleaf)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add silverleaf to the pizza.");
                    m_Item.Desc += ", silverleaf";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Spam)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add spam to the pizza.");
                    m_Item.Desc += ", spam";
                    ((Item)targeted).Consume();
                }

                else if (targeted is Garlic)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add garlic to the pizza.");
                    m_Item.Desc += ", garlic";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Ginseng)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add ginseng to the pizza.");
                    m_Item.Desc += ", ginseng";
                    ((Item)targeted).Consume();
                }

                else if (targeted is Cabbage)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add cabbage to the pizza.");
                    m_Item.Desc += ", cabbage";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Carrot)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add carrot to the pizza.");
                    m_Item.Desc += ", carrot";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Corn)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add corn to the pizza.");
                    m_Item.Desc += ", corn";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Lettuce)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add lettuce to the pizza.");
                    m_Item.Desc += ", lettuce";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Onion)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add onion to the pizza.");
                    m_Item.Desc += ", onion";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Turnip)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add turnip to the pizza.");
                    m_Item.Desc += ", turnip";
                    ((Item)targeted).Consume();
                }

                else if (targeted is BrightEggs || targeted is EasterEggs)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add an easter egg to the pizza!");
                    m_Item.Desc += ", surprise!";
                    m_Item.Hue = ((Item)targeted).Hue;
                    from.AddToBackpack(new Eggshells(m_Item.Hue));
                    ((Item)targeted).Consume();
                }
                else if (targeted is Eggs)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add egg to the pizza.");
                    m_Item.Desc += ", egg";
                    from.AddToBackpack(new Eggshells(m_Item.Hue));
                    ((Item)targeted).Consume();
                }
                else if (targeted is FriedEggs)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add egg to the pizza.");
                    m_Item.Desc += ", egg";
                    ((Item)targeted).Consume();
                }
                else if (targeted is FishHeads)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add fish heads to the pizza.");
                    m_Item.Desc += ", fish head";
                    ((Item)targeted).Consume();
                }
                else if (targeted is CheeseWedgeSmall)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add extra cheese to the pizza.");
                    m_Item.Desc += ", extra cheese";
                    ((Item)targeted).Consume();
                }
                else if (targeted is RedRaspberry || targeted is BlackRaspberry)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add raspberries to the pizza.");
                    m_Item.Desc += ", raspberry";
                    ((Item)targeted).Consume();
                }
                else if (targeted is Strawberries)
                {
                    if (!((Item)targeted).Movable) return;
                    from.SendMessage("You add strawberries to the pizza.");
                    m_Item.Desc += ", strawberry";
                    ((Item)targeted).Consume();
                }

                else if (targeted is RawHam || targeted is Ham || targeted is RawBaconSlab || targeted is SlabOfBacon || targeted is CheeseWheel || targeted is CheeseWedge)
                {
                    from.SendMessage("That portion is too large. Use a bladed object to cut it up first.");
                    return;
                }

                try
                {
                    if (targeted is FromageDeChevre || targeted is FromageDeChevreWedge || targeted is FromageDeBrebis || targeted is FromageDeBrebisWedge || targeted is FromageDeVache || targeted is FromageDeVacheWedge)
                    {
                        from.SendMessage("That portion is too large. Use a bladed object to cut it up first.");
                        return;
                    }
                    else if (targeted is FromageDeVacheWedgeSmall)
                    {
                        if (!((Item)targeted).Movable) return;
                        from.SendMessage("You add extra cheese to the pizza.");
                        m_Item.Desc += ", extra cheese";
                        ((Item)targeted).Consume();
                    }
                    else if (targeted is FromageDeChevreWedgeSmall)
                    {
                        if (!((Item)targeted).Movable) return;
                        from.SendMessage("You add goat cheese to the pizza.");
                        m_Item.Desc += ", goat cheese";
                        ((Item)targeted).Consume();
                    }
                    else if (targeted is FromageDeBrebisWedgeSmall)
                    {
                        if (!((Item)targeted).Movable) return;
                        from.SendMessage("You add sheep cheese to the pizza.");
                        m_Item.Desc += ", sheep cheese";
                        ((Item)targeted).Consume();
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

                public InternalTimer(Mobile from, IPoint3D p, Map map, int min, int max, Food cookedFood) : base(TimeSpan.FromSeconds(3.0))
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

    public class VegetableCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public VegetableCakeMix() : base(0x103F, 75)
        {
            Name = "vegetable cake mix";

        }

        public VegetableCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new VegetableCake(this.Hue);
        }

    }

    public class WatermelonCakeMix : CookableFood
    {
        public override int LabelNumber { get { return 1041002; } }

        [Constructable]
        public WatermelonCakeMix() : base(0x103F, 75)
        {
            Name = "watermelon cake mix";
            Hue = 34;
        }

        public WatermelonCakeMix(Serial serial) : base(serial)
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

        public override Food Cook()
        {
            return new WatermelonCake();
        }

    }
}
