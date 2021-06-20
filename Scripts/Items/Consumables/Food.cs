using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Engines.Craft;

using CustomsFramework;

namespace Server.Items
{
    public abstract class Food : Item, IEngravable, IQuality
    {
        private Mobile m_Poisoner;
        private Poison m_Poison;
        private int m_FillFactor;
        private bool m_PlayerConstructed;
        private ItemQuality _Quality;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Poisoner
        {
            get
            {
                return m_Poisoner;
            }
            set
            {
                m_Poisoner = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerConstructed
        {
            get
            {
                return m_PlayerConstructed;
            }
            set
            {
                m_PlayerConstructed = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Poison Poison
        {
            get
            {
                return m_Poison;
            }
            set
            {
                m_Poison = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int FillFactor
        {
            get
            {
                return m_FillFactor;
            }
            set
            {
                m_FillFactor = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual ItemQuality Quality { get { return _Quality; } set { _Quality = value; InvalidateProperties(); } }

		private string m_EngravedText = string.Empty;

		[CommandProperty(AccessLevel.GameMaster)]
		public string EngravedText
		{
			get { return m_EngravedText; }
			set
			{
				if (value != null)
					m_EngravedText = value;
				else
					m_EngravedText = string.Empty;

				InvalidateProperties();
			}
		}

		public Food(int itemID)
            : this(1, itemID)
        {
        }

        public Food(int amount, int itemID)
            : base(itemID)
        {
            Stackable = true;
            Amount = amount;
            m_FillFactor = 1;
        }

        public Food(Serial serial)
            : base(serial)
        {
        }

        public override void OnAfterDuped(Item newItem)
        {
            Food food = newItem as Food;

            if (food == null)
                return;

            food.PlayerConstructed = m_PlayerConstructed;
            food.Poisoner = m_Poisoner;
            food.Poison = m_Poison;
            food.Quality = _Quality;

            base.OnAfterDuped(newItem);
        }

        public virtual int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            Quality = (ItemQuality)quality;

            return quality;
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            if (from.Alive)
                list.Add(new ContextMenus.EatEntry(from, this));
        }

        public virtual bool TryEat(Mobile from)
        {
            if (Deleted || !Movable || !from.CheckAlive() || !CheckItemUse(from))
                return false;

            return Eat(from);
        }

        public override void AddCraftedProperties(ObjectPropertyList list)
        {
            if (_Quality == ItemQuality.Exceptional)
            {
                list.Add(1060636); // Exceptional
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (from.InRange(GetWorldLocation(), 1))
            {
                Eat(from);
            }
        }

        public override bool WillStack(Mobile from, Item dropped)
        {
            return dropped is Food && ((Food)dropped).PlayerConstructed == PlayerConstructed && base.WillStack(from, dropped);
        }

		public override void AddNameProperty(ObjectPropertyList list)
		{
			base.AddNameProperty(list);

			if (!String.IsNullOrEmpty(EngravedText))
			{
				list.Add(1072305, Utility.FixHtml(EngravedText)); // Engraved: ~1_INSCRIPTION~
			}
		}

		public virtual bool Eat(Mobile from)
        {
            // Fill the Mobile with FillFactor
            if (CheckHunger(from))
            {
                // Play a random "eat" sound
                from.PlaySound(Utility.Random(0x3A, 3));

                if (from.Body.IsHuman && !from.Mounted)
                {
                    if (Core.SA)
                    {
                        from.Animate(AnimationType.Eat, 0);
                    }
                    else
                    {
                        from.Animate(34, 5, 1, true, false, 0);
                    }
                }

                if (m_Poison != null)
                    from.ApplyPoison(m_Poisoner, m_Poison);

                Consume();

                EventSink.InvokeOnConsume(new OnConsumeEventArgs(from, this));

                return true;
            }

            return false;
        }

        public virtual bool CheckHunger(Mobile from)
        {
            return FillHunger(from, m_FillFactor);
        }

        public static bool FillHunger(Mobile from, int fillFactor)
        {
            if (from.Hunger >= 20)
            {
                from.SendLocalizedMessage(500867); // You are simply too full to eat any more!
                return false;
            }

            int iHunger = from.Hunger + fillFactor;

            if (from.Stam < from.StamMax)
                from.Stam += Utility.Random(6, 3) + fillFactor / 5;

            if (iHunger >= 20)
            {
                from.Hunger = 20;
                from.SendLocalizedMessage(500872); // You manage to eat the food, but you are stuffed!
            }
            else
            {
                from.Hunger = iHunger;

                if (iHunger < 5)
                    from.SendLocalizedMessage(500868); // You eat the food, but are still extremely hungry.
                else if (iHunger < 10)
                    from.SendLocalizedMessage(500869); // You eat the food, and begin to feel more satiated.
                else if (iHunger < 15)
                    from.SendLocalizedMessage(500870); // After eating the food, you feel much less hungry.
                else
                    from.SendLocalizedMessage(500871); // You feel quite full after consuming the food.
            }

            return true;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)7); // version

            writer.Write((int)_Quality);

			writer.Write(m_EngravedText);

            writer.Write((bool)m_PlayerConstructed);
            writer.Write(m_Poisoner);

            Poison.Serialize(m_Poison, writer);
            writer.Write(m_FillFactor);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch ( version )
            {
                case 1:
                    {
                        switch ( reader.ReadInt() )
                        {
                            case 0:
                                m_Poison = null;
                                break;
                            case 1:
                                m_Poison = Poison.Lesser;
                                break;
                            case 2:
                                m_Poison = Poison.Regular;
                                break;
                            case 3:
                                m_Poison = Poison.Greater;
                                break;
                            case 4:
                                m_Poison = Poison.Deadly;
                                break;
                        }

                        break;
                    }
                case 2:
                    {
                        m_Poison = Poison.Deserialize(reader);
                        break;
                    }
                case 3:
                    {
                        m_Poison = Poison.Deserialize(reader);
                        m_FillFactor = reader.ReadInt();
                        break;
                    }
                case 4:
                    {
                        m_Poisoner = reader.ReadMobile();
                        goto case 3;
                    }
                case 5:
                    {
                        m_PlayerConstructed = reader.ReadBool();
                        goto case 4;
                    }
				case 6:
					m_EngravedText = reader.ReadString();
					goto case 5;
                case 7:
                    _Quality = (ItemQuality)reader.ReadInt();
                    goto case 6;
            }
        }
    }

    public class BreadLoaf : Food
    {
        public override ItemQuality Quality { get { return ItemQuality.Normal; } set { } }

        [Constructable]
        public BreadLoaf()
            : this(1)
        {
        }

        [Constructable]
        public BreadLoaf(int amount)
            : base(amount, 0x103B)
        {
            Weight = 1.0;
            FillFactor = 3;
        }

        public BreadLoaf(Serial serial)
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

    public class Bacon : Food
    {
        [Constructable]
        public Bacon()
            : this(1)
        {
        }

        [Constructable]
        public Bacon(int amount)
            : base(amount, 0x979)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Bacon(Serial serial)
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

    public class SlabOfBacon : Food
    {
        [Constructable]
        public SlabOfBacon()
            : this(1)
        {
        }

        [Constructable]
        public SlabOfBacon(int amount)
            : base(amount, 0x976)
        {
            Weight = 1.0;
            FillFactor = 3;
        }

        public SlabOfBacon(Serial serial)
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

    public class FishSteak : Food
    {
        public override ItemQuality Quality { get { return ItemQuality.Normal; } set { } }

        public override double DefaultWeight
        {
            get
            {
                return 0.1;
            }
        }

        [Constructable]
        public FishSteak()
            : this(1)
        {
        }

        [Constructable]
        public FishSteak(int amount)
            : base(amount, 0x97B)
        {
            FillFactor = 3;
        }

        public FishSteak(Serial serial)
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

    public class CheeseWheel : Food
    {
        public override double DefaultWeight
        {
            get
            {
                return 0.1;
            }
        }

        [Constructable]
        public CheeseWheel()
            : this(1)
        {
        }

        [Constructable]
        public CheeseWheel(int amount)
            : base(amount, 0x97E)
        {
            FillFactor = 3;
        }

        public CheeseWheel(Serial serial)
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

    public class CheeseWedge : Food
    {
        public override double DefaultWeight
        {
            get
            {
                return 0.1;
            }
        }

        [Constructable]
        public CheeseWedge()
            : this(1)
        {
        }

        [Constructable]
        public CheeseWedge(int amount)
            : base(amount, 0x97D)
        {
            FillFactor = 3;
        }

        public CheeseWedge(Serial serial)
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

    public class CheeseWedgeSmall : Food
    {
        [Constructable]
        public CheeseWedgeSmall() : this(1) { }
        [Constructable]
        public CheeseWedgeSmall(int amount) : base(amount, 0x97C)
        {
            this.Weight = 0.1;
            this.FillFactor = 3;
        }
        public CheeseWedgeSmall(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }

    public class CheeseSlice : Food
    {
        public override double DefaultWeight
        {
            get
            {
                return 0.1;
            }
        }

        [Constructable]
        public CheeseSlice()
            : this(1)
        {
        }

        [Constructable]
        public CheeseSlice(int amount)
            : base(amount, 0x97C)
        {
            FillFactor = 1;
        }

        public CheeseSlice(Serial serial)
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

    public class FrenchBread : Food
    {
        [Constructable]
        public FrenchBread()
            : this(1)
        {
        }

        [Constructable]
        public FrenchBread(int amount)
            : base(amount, 0x98C)
        {
            Weight = 2.0;
            FillFactor = 3;
        }

        public FrenchBread(Serial serial)
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

    public class FriedEggs : Food
    {
        public override ItemQuality Quality { get { return ItemQuality.Normal; } set { } }

        [Constructable]
        public FriedEggs()
            : this(1)
        {
        }

        [Constructable]
        public FriedEggs(int amount)
            : base(amount, 0x9B6)
        {
            Weight = 1.0;
            FillFactor = 4;
        }

        public FriedEggs(Serial serial)
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

    public class CookedBird : Food
    {
        public override ItemQuality Quality { get { return ItemQuality.Normal; } set { } }

        [Constructable]
        public CookedBird()
            : this(1)
        {
        }

        [Constructable]
        public CookedBird(int amount)
            : base(amount, 0x9B7)
        {
            Weight = 1.0;
            FillFactor = 5;
        }

        public CookedBird(Serial serial)
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

    public class RoastPig : Food
    {
        [Constructable]
        public RoastPig()
            : this(1)
        {
        }

        [Constructable]
        public RoastPig(int amount)
            : base(amount, 0x9BB)
        {
            Weight = 45.0;
            FillFactor = 20;
        }

        public RoastPig(Serial serial)
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

    public class Sausage : Food
    {
        [Constructable]
        public Sausage()
            : this(1)
        {
        }

        [Constructable]
        public Sausage(int amount)
            : base(amount, 0x9C0)
        {
            Weight = 1.0;
            FillFactor = 4;
        }

        public Sausage(Serial serial)
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

    public class Ham : Food
    {
        [Constructable]
        public Ham()
            : this(1)
        {
        }

        [Constructable]
        public Ham(int amount)
            : base(amount, 0x9C9)
        {
            Weight = 1.0;
            FillFactor = 5;
        }

        public Ham(Serial serial)
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

    public class Cake : Food
    {
        [Constructable]
        public Cake()
            : base(0x9E9)
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 10;
        }

        public Cake(Serial serial)
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

    public class Ribs : Food
    {
        public override ItemQuality Quality { get { return ItemQuality.Normal; } set { } }

        [Constructable]
        public Ribs()
            : this(1)
        {
        }

        [Constructable]
        public Ribs(int amount)
            : base(amount, 0x9F2)
        {
            Weight = 1.0;
            FillFactor = 5;
        }

        public Ribs(Serial serial)
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

    public class Cookies : Food
    {
        [Constructable]
        public Cookies()
            : base(0x160b)
        {
            Stackable = Core.ML;
            Weight = 1.0;
            FillFactor = 4;
        }

        public Cookies(Serial serial)
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

    public class Muffins : Food
    {
        [Constructable]
        public Muffins()
            : base(0x9eb)
        {
            Stackable = true;
            Weight = 1.0;
            FillFactor = 4;
        }

        public Muffins(Serial serial)
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

            if (version == 0)
                Stackable = true;
        }
    }

    [TypeAlias("Server.Items.Pizza")]
    public class CheesePizza : Food
    {
        public override int LabelNumber
        {
            get
            {
                return 1044516;
            }
        }// cheese pizza

        [Constructable]
        public CheesePizza()
            : base(0x1040)
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 6;
        }

        public CheesePizza(Serial serial)
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

    public class SausagePizza : Food
    {
        public override int LabelNumber
        {
            get
            {
                return 1044517;
            }
        }// sausage pizza

        [Constructable]
        public SausagePizza()
            : base(0x1040)
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 6;
        }

        public SausagePizza(Serial serial)
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

    #if false
	public class Pizza : Food
	{
		[Constructable]
		public Pizza() : base( 0x1040 )
		{
			Stackable = false;
			Weight = 1.0;
			FillFactor = 6;
		}

		public Pizza( Serial serial ) : base( serial )
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
    #endif

    public class FruitPie : Food
    {
        public override int LabelNumber
        {
            get
            {
                return 1041346;
            }
        }// baked fruit pie

        [Constructable]
        public FruitPie()
            : base(0x1041)
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 5;
        }

        public FruitPie(Serial serial)
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

    public class MeatPie : Food
    {
        public override int LabelNumber
        {
            get
            {
                return 1041347;
            }
        }// baked meat pie

        [Constructable]
        public MeatPie()
            : base(0x1041)
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 5;
        }

        public MeatPie(Serial serial)
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

    public class PumpkinPie : Food
    {
        public override int LabelNumber
        {
            get
            {
                return 1041348;
            }
        }// baked pumpkin pie

        [Constructable]
        public PumpkinPie()
            : base(0x1041)
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 5;
        }

        public PumpkinPie(Serial serial)
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

    public class ApplePie : Food
    {
        public override int LabelNumber
        {
            get
            {
                return 1041343;
            }
        }// baked apple pie

        [Constructable]
        public ApplePie()
            : base(0x1041)
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 5;
        }

        public ApplePie(Serial serial)
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

    public class PeachCobbler : Food
    {
        public override int LabelNumber
        {
            get
            {
                return 1041344;
            }
        }// baked peach cobbler

        [Constructable]
        public PeachCobbler()
            : base(0x1041)
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 5;
        }

        public PeachCobbler(Serial serial)
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

    public class Quiche : Food
    {
        public override int LabelNumber
        {
            get
            {
                return 1041345;
            }
        }// baked quiche

        [Constructable]
        public Quiche()
            : base(0x1041)
        {
            Stackable = Core.ML;
            Weight = 1.0;
            FillFactor = 5;
        }

        public Quiche(Serial serial)
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

    public class LambLeg : Food
    {
        public override ItemQuality Quality { get { return ItemQuality.Normal; } set { } }

        [Constructable]
        public LambLeg()
            : this(1)
        {
        }

        [Constructable]
        public LambLeg(int amount)
            : base(amount, 0x160a)
        {
            Weight = 2.0;
            FillFactor = 5;
        }

        public LambLeg(Serial serial)
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

    public class ChickenLeg : Food
    {
        public override ItemQuality Quality { get { return ItemQuality.Normal; } set { } }

        [Constructable]
        public ChickenLeg()
            : this(1)
        {
        }

        [Constructable]
        public ChickenLeg(int amount)
            : base(amount, 0x1608)
        {
            Weight = 1.0;
            FillFactor = 4;
        }

        public ChickenLeg(Serial serial)
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

    [FlipableAttribute(0xC74, 0xC75)]
    public class HoneydewMelon : Food
    {
        [Constructable]
        public HoneydewMelon()
            : this(1)
        {
        }

        [Constructable]
        public HoneydewMelon(int amount)
            : base(amount, 0xC74)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public HoneydewMelon(Serial serial)
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

    [FlipableAttribute(0xC64, 0xC65)]
    public class YellowGourd : Food
    {
        [Constructable]
        public YellowGourd()
            : this(1)
        {
        }

        [Constructable]
        public YellowGourd(int amount)
            : base(amount, 0xC64)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public YellowGourd(Serial serial)
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

    [FlipableAttribute(0xC66, 0xC67)]
    public class GreenGourd : Food
    {
        [Constructable]
        public GreenGourd()
            : this(1)
        {
        }

        [Constructable]
        public GreenGourd(int amount)
            : base(amount, 0xC66)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public GreenGourd(Serial serial)
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

    [FlipableAttribute(0xC7F, 0xC81)]
    public class EarOfCorn : Food
    {
        [Constructable]
        public EarOfCorn()
            : this(1)
        {
        }

        [Constructable]
        public EarOfCorn(int amount)
            : base(amount, 0xC81)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public EarOfCorn(Serial serial)
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

    public class Turnip : Food
    {
        [Constructable]
        public Turnip()
            : this(1)
        {
        }

        [Constructable]
        public Turnip(int amount)
            : base(amount, 0xD3A)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Turnip(Serial serial)
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

    public class SheafOfHay : Item
    {
        [Constructable]
        public SheafOfHay()
            : base(0xF36)
        {
            Weight = 10.0;
        }

        public SheafOfHay(Serial serial)
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

    public class ThreeTieredCake : Item, IQuality
    {
        private ItemQuality _Quality;
        private int _Pieces;

        [CommandProperty(AccessLevel.GameMaster)]
        public ItemQuality Quality { get { return _Quality; } set { _Quality = value; InvalidateProperties(); } }

        public bool PlayerConstructed { get { return true; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Pieces 
        { 
            get { return _Pieces; }
            set 
            { 
                _Pieces = value; 

                if (_Pieces <= 0) 
                    Delete(); 
            } 
        }

        public override int LabelNumber { get { return 1098235; } } // A Three Tiered Cake 

        [Constructable]
        public ThreeTieredCake()
            : base(0x4BA3)
        {
            Weight = 1.0;
            Pieces = 10;
        }

        public virtual int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            Quality = (ItemQuality)quality;

            return quality;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                var cake = new Cake();
                cake.ItemID = 0x4BA4;

                from.PrivateOverheadMessage(Network.MessageType.Regular, 1154, 1157341, from.NetState); // *You cut a slice from the cake.*
                from.AddToBackpack(cake);

                Pieces--;
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
        }

        public override void AddCraftedProperties(ObjectPropertyList list)
        {
            if (_Quality == ItemQuality.Exceptional)
            {
                list.Add(1060636); // Exceptional
            }
        }

        public ThreeTieredCake(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write((int)_Quality);
            writer.Write(_Pieces);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            _Quality = (ItemQuality)reader.ReadInt();
            _Pieces = reader.ReadInt();
        }
    }

    public class Hamburger : Food
    {
        public override int LabelNumber { get { return 1125202; } } // hamburger

        [Constructable]
        public Hamburger()
            : this(1)
        {
        }

        [Constructable]
        public Hamburger(int amount)
            : base(amount, 0xA0DA)
        {
            FillFactor = 2;
        }

        public Hamburger(Serial serial)
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

    [Flipable(0xA0D8, 0xA0D9)]
    public class HotDog : Food
    {
        public override int LabelNumber { get { return 1125201; } } // hot dog

        [Constructable]
        public HotDog()
            : this(1)
        {
        }

        [Constructable]
        public HotDog(int amount)
            : base(amount, 0xA0D8)
        {
            FillFactor = 2;
        }

        public HotDog(Serial serial)
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

    [Flipable(0xA0D6, 0xA0D7)]
    public class CookableSausage : Food
    {
        public override int LabelNumber { get { return 1125198; } } // sausage

        [Constructable]
        public CookableSausage()
            : base(0xA0D6)
        {
            FillFactor = 2;
        }

        public CookableSausage(Serial serial)
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

    public class PulledPorkPlatter : Food
    {
        public override int LabelNumber { get { return 1123351; } } // Pulled Pork Platter

        [Constructable]
        public PulledPorkPlatter()
            : base(1, 0x999F)
        {
            FillFactor = 5;
            Stackable = false;
            Hue = 1157;
        }

        public PulledPorkPlatter(Serial serial)
            : base(serial)
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

    public class PulledPorkSandwich : Food
    {
        public override int LabelNumber { get { return 1123352; } } // Pulled Pork Sandwich

        [Constructable]
        public PulledPorkSandwich()
            : base(1, 0x99A0)
        {
            FillFactor = 3;
            Stackable = false;
        }

        public PulledPorkSandwich(Serial serial)
            : base(serial)
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

    public class PorkHock : Food
    {
        [Constructable]
        public PorkHock() : this(1) { }
        [Constructable]
        public PorkHock(int amount) : base(amount, 0x9D3)
        {
            this.Stackable = true;
            this.Weight = 2.0;
            this.Amount = amount;
            this.Name = "Pork Hock";
            this.Hue = 0x457;
        }
        public PorkHock(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class BananaBread : Food
    {
        [Constructable]
        public BananaBread() : this(1) { }
        [Constructable]
        public BananaBread(int amount) : base(amount, 0x103B)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Banana Bread";
        }
        public BananaBread(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class BlackberryCobbler : Food
    {
        [Constructable]
        public BlackberryCobbler() : this(1) { }
        [Constructable]
        public BlackberryCobbler(int amount) : base(amount, 0x1041)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Blackberry Cobbler";
        }
        public BlackberryCobbler(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class BlueberryPie : Food
    {
        [Constructable]
        public BlueberryPie() : this(1) { }
        [Constructable]
        public BlueberryPie(int amount) : base(amount, 0x1041)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Blueberry Pie";
        }
        public BlueberryPie(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class CherryPie : Food
    {
        [Constructable]
        public CherryPie() : this(1) { }
        [Constructable]
        public CherryPie(int amount) : base(amount, 0x1041)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Cherry Pie";
        }
        public CherryPie(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class ChickenPie : Food
    {
        [Constructable]
        public ChickenPie() : this(1) { }
        [Constructable]
        public ChickenPie(int amount) : base(amount, 0x1042)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Chicken Pie";
        }
        public ChickenPie(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class BeefPie : Food
    {
        [Constructable]
        public BeefPie() : this(1) { }
        [Constructable]
        public BeefPie(int amount) : base(amount, 0x1042)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Beef Pie";
        }
        public BeefPie(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class CornBread : Food
    {
        [Constructable]
        public CornBread() : this(1) { }
        [Constructable]
        public CornBread(int amount) : base(amount, 0x103C)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Corn Bread";
            this.Hue = 0x1C7;
        }
        public CornBread(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class HamPineapplePizza : Food
    {
        [Constructable]
        public HamPineapplePizza() : this(1) { }
        [Constructable]
        public HamPineapplePizza(int amount) : base(amount, 0x1040)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Ham and Pineapple Pizza";
        }
        public HamPineapplePizza(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class KeyLimePie2 : Food
    {
        [Constructable]
        public KeyLimePie2() : this(1) { }
        [Constructable]
        public KeyLimePie2(int amount) : base(amount, 0x1042)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Key Lime Pie 2";
        }
        public KeyLimePie2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class LemonMerenguePie : Food
    {
        [Constructable]
        public LemonMerenguePie() : this(1) { }
        [Constructable]
        public LemonMerenguePie(int amount) : base(amount, 0x1042)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Lemon Merengue Pie";
        }
        public LemonMerenguePie(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class MushroomOnionPizza : Food
    {
        [Constructable]
        public MushroomOnionPizza() : this(1) { }
        [Constructable]
        public MushroomOnionPizza(int amount) : base(amount, 0x1040)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Mushroom and Onion Pizza";
        }
        public MushroomOnionPizza(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class PumpkinBread : Food
    {
        [Constructable]
        public PumpkinBread() : this(1) { }
        [Constructable]
        public PumpkinBread(int amount) : base(amount, 0x103B)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Pumpkin Bread";
            this.Hue = 0x1C1;
        }
        public PumpkinBread(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class SausOnionMushPizza : Food
    {
        [Constructable]
        public SausOnionMushPizza() : this(1) { }
        [Constructable]
        public SausOnionMushPizza(int amount) : base(amount, 0x1040)
        {
            this.Weight = 1.0;
            this.FillFactor = 5;
            this.Name = "Sausage Onion and Mushroom Pizza";
        }
        public SausOnionMushPizza(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class ShepherdsPie : Food
    {
        [Constructable]
        public ShepherdsPie() : this(1) { }
        [Constructable]
        public ShepherdsPie(int amount) : base(amount, 0x1042)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Shepherds Pie";
        }
        public ShepherdsPie(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class TacoPizza : Food
    {
        [Constructable]
        public TacoPizza() : this(1) { }
        [Constructable]
        public TacoPizza(int amount) : base(amount, 0x1040)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Taco Pizza";
        }
        public TacoPizza(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class TurkeyPie : Food
    {
        [Constructable]
        public TurkeyPie() : this(1) { }
        [Constructable]
        public TurkeyPie(int amount) : base(amount, 0x1042)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Turkey Pie";
        }
        public TurkeyPie(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }

    public class SlicedTurkey : Food
    {
        [Constructable]
        public SlicedTurkey() : this(1) { }
        [Constructable]
        public SlicedTurkey(int amount) : base(amount, 0x1E1F)
        {
            this.Weight = 0.2;
            this.FillFactor = 3;
            this.Name = "Sliced Turkey";
            this.Hue = 0x457;
            this.Stackable = true;
        }
        public SlicedTurkey(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }

    public class TurkeyHock : Food
    {
        [Constructable]
        public TurkeyHock() : this(1) { }
        [Constructable]
        public TurkeyHock(int amount) : base(amount, 0x9D3)
        {
            this.Stackable = true;
            this.Weight = 2.0;
            this.Amount = amount;
            this.Name = "Turkey Hock";
            this.Hue = 0x457;
        }
        public TurkeyHock(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }

    public class VeggiePizza : Food
    {
        [Constructable]
        public VeggiePizza() : this(1) { }
        [Constructable]
        public VeggiePizza(int amount) : base(amount, 0x1040)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Vegetable Pizza";
        }
        public VeggiePizza(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class GarlicBread : Food
    {
        [Constructable]
        public GarlicBread() : this(1) { }
        [Constructable]
        public GarlicBread(int amount) : base(amount, 0x98C)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Name = "Garlic Bread";
            this.Hue = 0x1C8;
        }
        public GarlicBread(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
        
    public class HalibutFishSteak : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public HalibutFishSteak() : this(1) { }

        [Constructable]
        public HalibutFishSteak(int amount) : base(amount, 0x97B)
        {
            this.FillFactor = 3;
            Name = "Halibut Fish Steak";
        }

        public HalibutFishSteak(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class FlukeFishSteak : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public FlukeFishSteak() : this(1) { }

        [Constructable]
        public FlukeFishSteak(int amount) : base(amount, 0x97B)
        {
            this.FillFactor = 3;
            Name = "Fluke Fish Steak";
        }

        public FlukeFishSteak(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class MahiFishSteak : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public MahiFishSteak() : this(1) { }

        [Constructable]
        public MahiFishSteak(int amount) : base(amount, 0x97B)
        {
            this.FillFactor = 3;
            Name = "Mahi Fish Steak";
        }

        public MahiFishSteak(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SalmonFishSteak : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public SalmonFishSteak() : this(1) { }

        [Constructable]
        public SalmonFishSteak(int amount) : base(amount, 0x97B)
        {
            this.FillFactor = 3;
            Name = "Salmon Fish Steak";
        }

        public SalmonFishSteak(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RedSnapperFishSteak : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RedSnapperFishSteak() : this(1) { }

        [Constructable]
        public RedSnapperFishSteak(int amount) : base(amount, 0x97B)
        {
            this.FillFactor = 3;
            Name = "Red Snapper Fish Steak";
        }

        public RedSnapperFishSteak(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ParrotFishSteak : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public ParrotFishSteak() : this(1) { }

        [Constructable]
        public ParrotFishSteak(int amount) : base(amount, 0x97B)
        {
            this.FillFactor = 3;
            Name = "Parrot Fish Fish Steak";
        }

        public ParrotFishSteak(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TroutFishSteak : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public TroutFishSteak() : this(1) { }

        [Constructable]
        public TroutFishSteak(int amount) : base(amount, 0x97B)
        {
            this.FillFactor = 3;
            Name = "Trout Fish Steak";
        }

        public TroutFishSteak(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class CookedShrimp : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public CookedShrimp() : this(1) { }

        [Constructable]
        public CookedShrimp(int amount) : base(amount, 0x97B)
        {
            this.FillFactor = 3;
            Name = "Cooked Shrimp";
        }

        public CookedShrimp(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class CookedSteak : Food
    {
        [Constructable]
        public CookedSteak() : this(1) { }

        [Constructable]
        public CookedSteak(int amount) : base(amount, 0x3BCD)
        {
            this.Weight = 1.0;
            this.FillFactor = 5;
            Name = "Cooked Steak";
        }

        public CookedSteak(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class WeddingCake : Food
    {
        [Constructable]
        public WeddingCake() : base(0x3BCC)
        {
            Name = "Wedding Cake";
            this.Weight = 10.0;
            this.FillFactor = 10;
        }

        public WeddingCake(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class SliceOfWeddingCake : Food
    {
        [Constructable]
        public SliceOfWeddingCake() : this(1) { }

        [Constructable]
        public SliceOfWeddingCake(int amount) : base(amount, 0x3BCB)
        {
            Name = "Slice of Wedding Cake";
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public SliceOfWeddingCake(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class PennyCandy : Food
    {
        [Constructable]
        public PennyCandy() : this(1) { }

        [Constructable]
        public PennyCandy(int amount) : base(amount, 0x3BC7)
        {
            Name = "Candy";
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public PennyCandy(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    [FlipableAttribute(0x3BC5, 0x3BC4)]
    public class SliceOfCake : Food
    {
        [Constructable]
        public SliceOfCake() : this(1) { }

        [Constructable]
        public SliceOfCake(int amount) : base(amount, 0x3BC5)
        {
            Name = "Slice of Cake";
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public SliceOfCake(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    [FlipableAttribute(0x3BC0, 0x3BBF)]
    public class RoastHam : Food
    {
        [Constructable]
        public RoastHam() : base(0x3BC0)
        {
            Name = "Roast Ham";
            this.Weight = 10.0;
            this.FillFactor = 10;
        }

        public RoastHam(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class BirthdayCake : Food
    {
        [Constructable]
        public BirthdayCake() : base(0x3BBD)
        {
            Name = "Birthday Cake";
            this.Weight = 10.0;
            this.FillFactor = 10;
        }

        public BirthdayCake(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class HamSlices : Food
    {
        [Constructable]
        public HamSlices() : this(1) { }

        [Constructable]
        public HamSlices(int amount) : base(amount, 0x1E1F)
        {
            this.Weight = 0.2;
            this.FillFactor = 1;
        }

        public HamSlices(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class Meatballs : Food
    {
        [Constructable]
        public Meatballs() : this(1) { }
        [Constructable]
        public Meatballs(int amount) : base(amount, 0xE74)
        {
            this.Weight = 1.0;
            this.Stackable = true;
            this.Hue = 0x475;
            this.FillFactor = 4;
            this.Name = "Meatballs";
        }
        public Meatballs(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class Meatloaf : Food
    {
        [Constructable]
        public Meatloaf() : this(1) { }
        [Constructable]
        public Meatloaf(int amount) : base(amount, 0xE79)
        {
            this.Weight = 1.0;
            this.Stackable = true;
            this.Hue = 0x475;
            this.FillFactor = 5;
            this.Name = "Meatloaf";
        }
        public Meatloaf(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class PotatoStrings : Food
    {
        [Constructable]
        public PotatoStrings() : this(1) { }
        [Constructable]
        public PotatoStrings(int amount) : base(amount, 0x1B8D)
        {
            this.Weight = 1.0;
            this.Stackable = true;
            this.Hue = 0x225;
            this.FillFactor = 3;
            this.Name = "Potato Strings";
        }
        public PotatoStrings(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class Pancakes : Food
    {
        [Constructable]
        public Pancakes() : this(1) { }
        [Constructable]
        public Pancakes(int amount) : base(amount, 0x1E1F)
        {
            this.Weight = 1.0;
            this.Stackable = true;
            this.Hue = 0x22A;
            this.FillFactor = 5;
            this.Name = "Pancakes";
        }
        public Pancakes(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class Waffles : Food
    {
        [Constructable]
        public Waffles() : this(1) { }
        [Constructable]
        public Waffles(int amount) : base(amount, 0x1E1F)
        {
            this.Weight = 1.0;
            this.Stackable = true;
            this.Hue = 0x1C7;
            this.FillFactor = 4;
            this.Name = "Waffles";
        }
        public Waffles(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class GrilledHam : Food
    {
        [Constructable]
        public GrilledHam() : this(1) { }
        [Constructable]
        public GrilledHam(int amount) : base(amount, 0x1E1F)
        {
            this.Weight = 1.0;
            this.Stackable = true;
            this.Hue = 0x33D;
            this.FillFactor = 4;
            this.Name = "Grilled Ham";
        }
        public GrilledHam(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class Hotwings : Food
    {
        [Constructable]
        public Hotwings() : this(1) { }
        [Constructable]
        public Hotwings(int amount) : base(amount, 0x1608)
        {
            this.Weight = 1.0;
            this.Stackable = true;
            this.Hue = 0x21A;
            this.FillFactor = 3;
            this.Name = "Hotwings";
        }
        public Hotwings(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class Taco : Food
    {
        [Constructable]
        public Taco() : this(1) { }
        [Constructable]
        public Taco(int amount) : base(amount, 0x1370)
        {
            this.Weight = 1.0;
            this.Stackable = true;
            this.Hue = 0x465;
            this.FillFactor = 3;
            this.Name = "Taco";
        }
        public Taco(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class CornOnCob : Food
    {
        [Constructable]
        public CornOnCob() : this(1) { }
        [Constructable]
        public CornOnCob(int amount) : base(amount, 0xC81)
        {
            this.Weight = 1.0;
            this.Stackable = true;
            this.Hue = 0x0;
            this.FillFactor = 3;
            this.Name = "Cooked Corn on the Cob";
        }
        public CornOnCob(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class Hotdog : Food
    {
        [Constructable]
        public Hotdog() : this(1) { }
        [Constructable]
        public Hotdog(int amount) : base(amount, 0xC7F)
        {
            this.Weight = 1.0;
            this.Stackable = true;
            this.Hue = 0x457;
            this.FillFactor = 3;
            this.Name = "Hotdog";
        }
        public Hotdog(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class CookedSunflowerSeeds : Food
    {
        [Constructable]
        public CookedSunflowerSeeds() : this(1) { }
        [Constructable]
        public CookedSunflowerSeeds(int amount) : base(amount, 0xF27)
        {
            this.Weight = 0.1;
            this.Stackable = true;
            this.Hue = 0x44F;
            this.FillFactor = 2;
            this.Name = "Sunflower Seeds";
        }
        public CookedSunflowerSeeds(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }

    public class BlueberryMuffins : Food
    {
        [Constructable]
        public BlueberryMuffins() : this(1) { }
        [Constructable]
        public BlueberryMuffins(int amount) : base(amount, 0x9EB)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Hue = 0x1FC;
            this.Name = "Blueberry Muffins";
        }
        public BlueberryMuffins(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class PumpkinMuffins : Food
    {
        [Constructable]
        public PumpkinMuffins() : this(1) { }
        [Constructable]
        public PumpkinMuffins(int amount) : base(amount, 0x9EB)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Hue = 0x1C0;
            this.Name = "Pumpkin Muffins";
        }
        public PumpkinMuffins(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class ChocSunflowerSeeds : Food
    {
        [Constructable]
        public ChocSunflowerSeeds() : this(1) { }
        [Constructable]
        public ChocSunflowerSeeds(int amount) : base(amount, 0x9B4)
        {
            this.Weight = 1.0;
            this.FillFactor = 2;
            this.Hue = 0x41B;
            this.Name = "Chocolate Sunflower Seeds";
        }
        public ChocSunflowerSeeds(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class RiceKrispTreat : Food
    {
        [Constructable]
        public RiceKrispTreat() : this(1) { }
        [Constructable]
        public RiceKrispTreat(int amount) : base(amount, 0x1044)
        {
            this.Weight = 1.0;
            this.FillFactor = 2;
            this.Hue = 0x9B;
            this.Name = "Rice Krisp Treat";
        }
        public RiceKrispTreat(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class Brownies : Food
    {
        [Constructable]
        public Brownies() : this(1) { }
        [Constructable]
        public Brownies(int amount) : base(amount, 0x160B)
        {
            this.Weight = 1.0;
            this.FillFactor = 2;
            this.Hue = 0x47D;
            this.Name = "Brownies";
        }
        public Brownies(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
    public class ChocolateCake : Food
    {
        [Constructable]
        public ChocolateCake() : this(1) { }
        [Constructable]
        public ChocolateCake(int amount) : base(amount, 0x9E9)
        {
            this.Weight = 2.0;
            this.FillFactor = 3;
            this.Hue = 0x45D;
            this.Name = "Chocolate Cake";
        }
        public ChocolateCake(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }

    [FlipableAttribute(0x9B5, 0x9B5)]
    public class Candy : Food
    {
        [Constructable]
        public Candy() : this(1) { }
        [Constructable]
        public Candy(int amount) : base(amount, 0x9B5)
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
            this.Hue = 24;
            this.Name = "Candy";
        }
        public Candy(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }

    public class BananaCake : Food
    {
        [Constructable]
        public BananaCake() : base(0x9E9)
        {
            Name = "a banana cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = 354;
        }

        public BananaCake(Serial serial) : base(serial)
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

    public class BasePie : Food
    {
        [Constructable]
        public BasePie() : this(null, 0)
        {
        }

        [Constructable]
        public BasePie(string Desc) : this(Desc, 0)
        {
        }

        [Constructable]
        public BasePie(int Color) : this(null, Color)
        {
        }

        [Constructable]
        public BasePie(string Desc, int Color) : base(0x1041)
        {
            this.Weight = 1.0;
            this.FillFactor = 5;
            if (Desc != "" && Desc != null)
                this.Name = "a " + Desc + " pie";
            else
                this.Name = "a pie";

            this.Hue = Color;
        }

        public BasePie(Serial serial) : base(serial)
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

    public class CantaloupeCake : Food
    {
        [Constructable]
        public CantaloupeCake() : base(0x9E9)
        {
            Name = "a cantaloupe cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = 145;
        }

        public CantaloupeCake(Serial serial) : base(serial)
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

    public class CarrotCake : Food
    {
        [Constructable]
        public CarrotCake() : base(0x9E9)
        {
            Name = "a carrot cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = 248;
        }

        public CarrotCake(Serial serial) : base(serial)
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

    public class ChickenPotPie : Food
    {
        [Constructable]
        public ChickenPotPie() : base(0x1041)
        {
            this.Name = "baked chicken pot pie";
            this.Weight = 1.0;
            this.FillFactor = 10;
        }

        public ChickenPotPie(Serial serial) : base(serial)
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

    public class CoconutCake : Food
    {
        [Constructable]
        public CoconutCake() : base(0x9E9)
        {
            Name = "a coconut cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = 556;
        }

        public CoconutCake(Serial serial) : base(serial)
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

    public class CookedHeadlessFish : Food, ICarvable
    {
        public bool Carve(Mobile from, Item item)
        {
            base.ScissorHelper(from, new FishSteak(), 4);
            return true;
        }

        [Constructable]
        public CookedHeadlessFish() : this(1)
        {
        }

        [Constructable]
        public CookedHeadlessFish(int amount) : base(Utility.Random(7708, 2))
        {
            Stackable = true;
            Weight = 0.4;
            Amount = amount;
            this.FillFactor = 12;
        }

        public CookedHeadlessFish(Serial serial) : base(serial)
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

    public class Donuts : Food
    {
        [Constructable]
        public Donuts() : this(1)
        {
        }

        [Constructable]
        public Donuts(int amount) : base(amount, 6867)
        {
            this.Weight = 2.0;
            this.FillFactor = 3;
        }

        public Donuts(Serial serial) : base(serial)
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

    public class FruitCake : Food
    {
        [Constructable]
        public FruitCake() : this(0)
        {
        }

        [Constructable]
        public FruitCake(int Color) : base(0x9E9)
        {
            Name = "a fruit cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = Color;
        }

        public FruitCake(Serial serial) : base(serial)
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

    public class GrapeCake : Food
    {
        [Constructable]
        public GrapeCake() : base(0x9E9)
        {
            Name = "a grape cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = 21;
        }

        public GrapeCake(Serial serial) : base(serial)
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

    public class HoneydewMelonCake : Food
    {
        [Constructable]
        public HoneydewMelonCake() : base(0x9E9)
        {
            Name = "a honeydew melon cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = 61;
        }

        public HoneydewMelonCake(Serial serial) : base(serial)
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

    public class KeyLimeCake : Food
    {
        [Constructable]
        public KeyLimeCake() : base(0x9E9)
        {
            Name = "a key lime cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = 71;
        }

        public KeyLimeCake(Serial serial) : base(serial)
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

    public class KeyLimePie : Food
    {
        [Constructable]
        public KeyLimePie() : base(0x1041)
        {
            Name = "baked key lime pie";
            this.Weight = 1.0;
            this.FillFactor = 5;
        }

        public KeyLimePie(Serial serial) : base(serial)
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

    public class LemonCake : Food
    {
        [Constructable]
        public LemonCake() : base(0x9E9)
        {
            Name = "a lemon cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = 53;
        }

        public LemonCake(Serial serial) : base(serial)
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

    public class MeatCake : Food
    {
        [Constructable]
        public MeatCake() : this(0)
        {
        }

        [Constructable]
        public MeatCake(int Color) : base(0x9E9)
        {
            Name = "a meat cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = Color;
        }

        public MeatCake(Serial serial) : base(serial)
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

    public class PeachCake : Food
    {
        [Constructable]
        public PeachCake() : base(0x9E9)
        {
            Name = "a peach cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = 46;
        }

        public PeachCake(Serial serial) : base(serial)
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

    public class Pizza : Food
    {
        private string m_Desc;

        [CommandProperty(AccessLevel.Counselor)]
        public string Desc
        {
            get { return m_Desc; }
            set
            {
                m_Desc = value;
                Name = "cooked " + m_Desc + " pizza";
                InvalidateProperties();
            }
        }

        [Constructable]
        public Pizza() : this("cheese", 0)
        {
        }

        [Constructable]
        public Pizza(int color) : this("cheese", color)
        {
        }

        [Constructable]
        public Pizza(string desc) : this(desc, 0)
        {
        }

        [Constructable]
        public Pizza(string desc, int color) : base(0x1040)
        {
            this.Weight = 1.0;
            this.FillFactor = 6;

            if (desc != "" && desc != null)
            {
                Desc = desc;
            }
            else
            {
                Desc = "cheese";
            }

            Hue = color;
        }

        public Pizza(Serial serial) : base(serial)
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
                    m_Desc = reader.ReadString();
                    break;
            }
        }
    }
        
    public class PumpkinCake : Food
    {
        [Constructable]
        public PumpkinCake() : base(0x9E9)
        {
            Name = "a pumpkin cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = 243;
        }

        public PumpkinCake(Serial serial) : base(serial)
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

    public class VegePie : Food
    {
        [Constructable]
        public VegePie() : base(0x1041)
        {
            this.Weight = 1.0;
            this.FillFactor = 5;
            Name = "vegetable pie";
        }

        public VegePie(Serial serial) : base(serial)
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

    public class VegetableCake : Food
    {
        [Constructable]
        public VegetableCake() : this(0)
        {
        }

        [Constructable]
        public VegetableCake(int Color) : base(0x9E9)
        {
            Name = "a vegetable cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = Color;
        }

        public VegetableCake(Serial serial) : base(serial)
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

    public class WatermelonCake : Food
    {
        [Constructable]
        public WatermelonCake() : base(0x9E9)
        {
            Name = "a watermelon cake";
            this.Weight = 1.0;
            this.FillFactor = 15;
            Hue = 34;
        }

        public WatermelonCake(Serial serial) : base(serial)
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

    public class BeefHock : Food
    {
        [Constructable]
        public BeefHock() : this(1) { }
        [Constructable]
        public BeefHock(int amount) : base(amount, 0x9D3)
        {
            this.Stackable = true;
            this.Weight = 1.0;
            this.Amount = amount;
            this.Name = "Beef Hock";
            this.Hue = 0x459;
        }
        public BeefHock(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }

    public class Prune : Food
    {
        [Constructable]
        public Prune() : this(1)
        {
        }

        [Constructable]
        public Prune(int amount) : base(amount, 0xF2B)
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
            this.Hue = 0x205;
            this.Name = "Prune";
            this.Stackable = true;
        }

        public Prune(Serial serial) : base(serial)
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

    public class Silverleaf : Food
    {
        [Constructable]
        public Silverleaf() : this(1) { }
        [Constructable]
        public Silverleaf(int amount) : base(amount, 0x9B6)
        {
            this.Name = "Silverleaf meal";
            this.Hue = 96;
            this.Weight = 0.5;
            this.FillFactor = 0;
        }
        public Silverleaf(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}
