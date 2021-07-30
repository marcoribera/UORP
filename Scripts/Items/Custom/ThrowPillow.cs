using System;
using Server.Items;

namespace Server.Items
{
	public class ThrowPillow : Item
	{
		[Constructable]
		public ThrowPillow() : base(0x1944)
		{
			Movable = true;
			Weight = 4.0;
		}

		public ThrowPillow(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 20.0;
		}
	}
}