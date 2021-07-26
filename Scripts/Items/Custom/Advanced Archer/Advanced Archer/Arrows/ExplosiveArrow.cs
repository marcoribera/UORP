using System;

namespace Server.Items
{
    public class ExplosiveArrow : Item
	{
		[Constructable]
		public ExplosiveArrow() : this( 1 )
		{
		}

		[Constructable]
		public ExplosiveArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Name = "Explosive Arrow";
			Hue = 32;
			Weight = 0.1;
			Amount = amount;
		}

		public ExplosiveArrow( Serial serial ) : base( serial )
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
