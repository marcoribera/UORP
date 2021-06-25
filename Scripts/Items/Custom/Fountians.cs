//Lagatha The Sheild Maiden
using System;

namespace Server.Items
{
	[FlipableAttribute(42538, 42539)]
	public class BrassFountian : Item
	{
		public override string DefaultName{ get { return "Brass Fountian"; } }
		public override double DefaultWeight{ get { return 1.0; } }
		
		[Constructable]
		public BrassFountian ()
			: base(42539)
		{
		}

		public BrassFountian (Serial serial)
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
	
	[FlipableAttribute(42541, 42542, 42544, 42545)]
	public class Wallfountian : Item
	{
		public override string DefaultName{ get { return "Wall Fountian"; } }
		public override double DefaultWeight{ get { return 1.0; } }
		
		[Constructable]
		public Wallfountian ()
			: base(42541)
		{
		}

		public Wallfountian(Serial serial)
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
	
	[FlipableAttribute(42547, 42548,42550, 42551)]
	public class HandPump : Item
	{
		public override string DefaultName{ get { return "Hand Pump"; } }
		public override double DefaultWeight{ get { return 3.0; } }
		
		[Constructable]
		public HandPump ()
			: base(42547)
		{
		}

		public HandPump(Serial serial)
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
	
	
}