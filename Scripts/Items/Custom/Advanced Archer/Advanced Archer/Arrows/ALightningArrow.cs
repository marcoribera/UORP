using System;

namespace Server.Items
{
	public class ALightningArrow : Item
	{
		[Constructable]
		public ALightningArrow() : this( 1 )
		{
		}

		[Constructable]
		public ALightningArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Name = "Lightning Arrow";
			Hue = 1174;
			Weight = 0.1;
			Amount = amount;
		}

        public ALightningArrow(Serial serial)
            : base(serial)
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
}
