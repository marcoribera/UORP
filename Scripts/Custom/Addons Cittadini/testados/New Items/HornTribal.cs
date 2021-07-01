using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class HornedTribalMaskS : BaseHat
  {


      [Constructable]
		public HornedTribalMaskS()
			: base(0x1549)
		{
          Name = "A Horned Tribal Mask";
	  Weight = 5.0;
          Layer = Layer.Helm;
          Hue = 0;//randomhuebetweenthosenumbers?
		}

		public HornedTribalMaskS( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
