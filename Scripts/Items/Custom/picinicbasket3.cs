using System;
using Server;
using Server.Items;

[Flipable(0xA0DB, 0xA0DC)]

public class PicnicBasket3 : BaseContainer
{
    [Constructable]
    public PicnicBasket3()
        : base(0xA0DB)
    {
        this.Name = "Enchanted Picinic Basket";
        this.Weight = 1.0;
    }

    public PicnicBasket3(Serial serial)
        : base(serial)
    {
    }

    public override int DefaultGumpID
    {
        get
        {
            return 0x3F;
        }
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
