using System;
using Server.Network;
namespace Server.Items
{
	public class Batter : Item
	{
		[Constructable]
		public Batter() : base( 0xE23 )
		{
		      	Weight = 0.5;
			Stackable = true;
			Name = "Batter";
			Hue = 0x227;
		}
		public Batter( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
	public class Butter : Item
	{
		[Constructable]
		public Butter() : base( 0x1044 )
		{
		      	Weight = 0.5;
			Stackable = true;
			Name = "Butter";
			Hue = 0x161;
		}
		public Butter( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
	public class ChocolateMix : Item
	{
		[Constructable]
		public ChocolateMix() : base( 0xE23 )
		{
		      	Weight = 0.5;
			Stackable = true;
			Name = "Chocolate Mix";
			Hue = 0x414;
		}
		public ChocolateMix( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
	public class PastaNoodles : Item
	{
		[Constructable]
		public PastaNoodles() : base( 0x1038 )
		{
		      	Weight = 0.5;
			Stackable = true;
			Name = "Pasta Noodles";
			Hue = 0x100;
		}
		public PastaNoodles( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
	public class PieMix : Item
	{
		[Constructable]
		public PieMix() : base( 0x103F )
		{
		      	Weight = 0.5;
			Stackable = true;
			Name = "Butter";
			Hue = 0x45A;
		}
		public PieMix( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
	public class Tortilla : Item
	{
		[Constructable]
		public Tortilla() : base( 0x973 )
		{
		      	Weight = 0.5;
			Stackable = true;
			Name = "Tortilla";
			Hue = 0x22C;
		}
		public Tortilla( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
	public class WaffleMix : Item
	{
		[Constructable]
		public WaffleMix() : base( 0x103F )
		{
		      	Weight = 0.5;
			Stackable = true;
			Name = "Waffle Mix";
			Hue = 0x227;
		}
		public WaffleMix( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
	public class GroundBeef : Item
	{
		[Constructable]
		public GroundBeef() : this(1){}

		[Constructable]
		public GroundBeef(int amount) : base( 9908 )
		{
			Stackable = true;
			Name = "Ground Beef";
			Hue = 0x21B;
			Amount = amount;
		}
		public GroundBeef( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
	public class GroundPork : Item
	{
		[Constructable]
		public GroundPork() : this(1){}

		[Constructable]
		public GroundPork(int amount) : base( 9908 )
		{
		      	Weight = 0.5;
			Stackable = true;
			Name = "Ground Pork";
			Hue = 0x221;
			Amount = amount;
		}
		public GroundPork( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
	public class Tofu : Item
	{
		[Constructable]
		public Tofu() : base( 0x1044 )
		{
		      	Weight = 0.5;
			Stackable = true;
			Name = "Tofu";
			Hue = 0x38F;
		}
		public Tofu( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
    [TypeAlias("Server.Items.SackcornFlourOpen")]
    public class SackcornFlour : Item, IHasQuantity
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
        public SackcornFlour() : base(0x1039)
        {
            Name = "A Sack of Cornflour";
            m_Quantity = 20;
        }

        public SackcornFlour(Serial serial) : base(serial) { }

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

    public class BarbecueSauce : Item
    {
        [Constructable]
        public BarbecueSauce() : base(0xEFC)
        {
            Stackable = true;
            Name = "Barbecue Sauce";
            Hue = 0x278;
        }
        public BarbecueSauce(Serial serial) : base(serial)
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
    public class EnchiladaSauce : Item
    {
        [Constructable]
        public EnchiladaSauce() : base(0xF04)
        {
            Stackable = true;
            Name = "Enchilada Sauce";
            Hue = 0x1B5;
        }
        public EnchiladaSauce(Serial serial) : base(serial)
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
    public class HotSauce : Item
    {
        [Constructable]
        public HotSauce() : base(0xEFD)
        {
            Stackable = true;
            Name = "Hot Sauce";
            Hue = 0x25;
        }
        public HotSauce(Serial serial) : base(serial)
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
    public class CookingOil : Item
    {
        [Constructable]
        public CookingOil() : base(0xE2B)
        {
            Stackable = true;
            Name = "Cooking Oil";
            Hue = 0x2D6;
        }
        public CookingOil(Serial serial) : base(serial)
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
    public class SoySauce : Item
    {
        [Constructable]
        public SoySauce() : base(0xE2C)
        {
            Stackable = true;
            Name = "Soy Sauce";
            Hue = 0x39E;
        }
        public SoySauce(Serial serial) : base(serial)
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
    public class Teriyaki : Item
    {
        [Constructable]
        public Teriyaki() : base(0xE2C)
        {
            Stackable = true;
            Name = "Teriyaki";
            Hue = 0x362;
        }
        public Teriyaki(Serial serial) : base(serial)
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
    public class TomatoSauce : Item
    {
        [Constructable]
        public TomatoSauce() : base(0x1006)
        {
            Stackable = true;
            Name = "Tomato Sauce";
            Hue = 0x26;
        }
        public TomatoSauce(Serial serial) : base(serial)
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
    public class Vinegar : Item
    {
        [Constructable]
        public Vinegar() : base(0x99B)
        {
            Stackable = true;
            Name = "Vinegar";
            Hue = 0x0;
        }
        public Vinegar(Serial serial) : base(serial)
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
    public class Cream : Item
    {
        [Constructable]
        public Cream() : base(0x1F8C)
        {
            Stackable = true;
            Name = "Cream";
            Hue = 0x0;
        }
        public Cream(Serial serial) : base(serial)
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
    public class FruitJam : Item
    {
        [Constructable]
        public FruitJam() : base(0x1006)
        {
            Stackable = true;
            Name = "Mixed Fruit Jam";
            Hue = 0x18;
        }
        public FruitJam(Serial serial) : base(serial)
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
    public class PeanutButter : Item
    {
        [Constructable]
        public PeanutButter() : base(0x1006)
        {
            Stackable = true;
            Name = "Peanut Butter";
            Hue = 0x413;
        }
        public PeanutButter(Serial serial) : base(serial)
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
