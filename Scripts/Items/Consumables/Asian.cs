using System;

namespace Server.Items
{
    public class Wasabi : Item
    {
        [Constructable]
        public Wasabi()
            : base(0x24E8)
        {
            Weight = 1.0;
        }

        public Wasabi(Serial serial)
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

    public class WasabiClumps : Food
    {
        [Constructable]
        public WasabiClumps()
            : base(0x24EB)
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 2;
        }

        public WasabiClumps(Serial serial)
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

    public class EmptyBentoBox : Item
    {
        [Constructable]
        public EmptyBentoBox()
            : base(0x2834)
        {
            Weight = 5.0;
        }

        public EmptyBentoBox(Serial serial)
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

    public class BentoBox : Food
    {
        [Constructable]
        public BentoBox()
            : base(0x2836)
        {
            Stackable = false;
            Weight = 5.0;
            FillFactor = 2;
        }

        public BentoBox(Serial serial)
            : base(serial)
        {
        }

        public override bool Eat(Mobile from)
        {
            if (!base.Eat(from))
                return false;

            from.AddToBackpack(new EmptyBentoBox());
            return true;
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

    public class SushiRolls : Food
    {
        [Constructable]
        public SushiRolls()
            : base(0x283E)
        {
            Stackable = false;
            Weight = 3.0;
            FillFactor = 2;
        }

        public SushiRolls(Serial serial)
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

    public class SushiPlatter : Food
    {
        [Constructable]
        public SushiPlatter()
            : base(0x2840)
        {
            Stackable = Core.ML;
            Weight = 3.0;
            FillFactor = 2;
        }

        public SushiPlatter(Serial serial)
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

    public class GreenTeaBasket : Item
    {
        [Constructable]
        public GreenTeaBasket()
            : base(0x284B)
        {
            Weight = 1.0;
			Stackable = true;
        }

        public GreenTeaBasket(Serial serial)
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

    public class GreenTea : Food
    {
        [Constructable]
        public GreenTea()
            : base(0x284C)
        {
            Stackable = false;
            Weight = 4.0;
            FillFactor = 2;
        }

        public GreenTea(Serial serial)
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

    public class MisoSoup : Food
    {
        [Constructable]
        public MisoSoup()
            : base(0x284D)
        {
            Stackable = false;
            Weight = 4.0;
            FillFactor = 2;
        }

        public MisoSoup(Serial serial)
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

    public class WhiteMisoSoup : Food
    {
        [Constructable]
        public WhiteMisoSoup()
            : base(0x284E)
        {
            Stackable = false;
            Weight = 4.0;
            FillFactor = 2;
        }

        public WhiteMisoSoup(Serial serial)
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

    public class RedMisoSoup : Food
    {
        [Constructable]
        public RedMisoSoup()
            : base(0x284F)
        {
            Stackable = false;
            Weight = 4.0;
            FillFactor = 2;
        }

        public RedMisoSoup(Serial serial)
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

    public class AwaseMisoSoup : Food
    {
        [Constructable]
        public AwaseMisoSoup()
            : base(0x2850)
        {
            Stackable = false;
            Weight = 4.0;
            FillFactor = 2;
        }

        public AwaseMisoSoup(Serial serial)
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

    [FlipableAttribute(10302, 10303)]
    public class DarkSushiTray : Food
    {
        [Constructable]
        public DarkSushiTray() : base(1, Utility.RandomList(10302, 10303))
        {
            FillFactor = 5;
        }

        public DarkSushiTray(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Miso1 : Food
    {
        [Constructable]
        public Miso1() : base(1, 10317)
        {
            FillFactor = 5;
        }

        public Miso1(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class Miso2 : Food
    {
        [Constructable]
        public Miso2() : base(1, 10318)
        {
            FillFactor = 5;
        }

        public Miso2(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class Miso3 : Food
    {
        [Constructable]
        public Miso3() : base(1, 10319)
        {
            FillFactor = 5;
        }

        public Miso3(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class Miso4 : Food
    {
        [Constructable]
        public Miso4() : base(1, 10320)
        {
            FillFactor = 5;
        }

        public Miso4(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    [FlipableAttribute(10304, 10305)]
    public class LightSushiTray : Food
    {
        [Constructable]
        public LightSushiTray() : base(1, Utility.RandomList(10304, 10305))
        {
            FillFactor = 5;
        }

        public LightSushiTray(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}
