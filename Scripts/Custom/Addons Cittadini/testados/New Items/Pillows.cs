/*
 * Created by SharpDevelop.
 * User: alexanderfb
 * Date: 1/25/2005
 * Time: 10:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using Server;

namespace Server.Items 
{
	[Flipable( 0x13AB, 0x13AC )]
	public class TasslePillow : Item, IDyable
	{
		[Constructable]
		public TasslePillow() : base( 0x13AB )
		{
			Name = "a tassled pillow";
		}
		
		public TasslePillow( Serial serial ) : base( serial ) 
		{ 
		}
	
		public override void Serialize( GenericWriter writer ) 
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
	
		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
		
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
			return false;

			Hue = sender.DyedHue;

			return true;
		}
	}
	
	[Flipable( 0x13A9, 0x13AA )]
	public class FloorPillow : Item, IDyable
	{
		[Constructable]
		public FloorPillow() : base( 0x13A9 )
		{
			Name = "a floor pillow";
		}
		
		public FloorPillow( Serial serial ) : base( serial ) 
		{ 
		}
	
		public override void Serialize( GenericWriter writer ) 
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
	
		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
		
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
			return false;

			Hue = sender.DyedHue;

			return true;
		}
	}
	
	[Flipable( 0x13A7, 0x13A8 )]
	public class RoundPillow : Item, IDyable
	{
		[Constructable]
		public RoundPillow() : base( 0x13A7 )
		{
			Name = "a round pillow";
		}
		
		public RoundPillow( Serial serial ) : base( serial ) 
		{ 
		}
	
		public override void Serialize( GenericWriter writer ) 
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
	
		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
		
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
			return false;

			Hue = sender.DyedHue;

			return true;
		}
	}
	
	[Flipable( 0x13A4, 0x13A5 )]
	public class BedPillow : Item, IDyable
	{
		[Constructable]
		public BedPillow() : base( 0x13A7 )
		{
			Name = "a bed pillow";
		}
		
		public BedPillow( Serial serial ) : base( serial ) 
		{ 
		}
	
		public override void Serialize( GenericWriter writer ) 
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
	
		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
		
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
			return false;

			Hue = sender.DyedHue;

			return true;
		}
	}
}
