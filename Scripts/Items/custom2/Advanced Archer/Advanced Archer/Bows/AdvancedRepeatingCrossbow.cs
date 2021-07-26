using System;
using Server.Network;
using Server.Items;
using Server.Targeting;


namespace Server.Items
{
	[FlipableAttribute( 0x26C3, 0x26CD )]
	public class AdvancedRepeatingCrossbow : CBaseRanged
	{
		private BoltType m_BoltType;
		public override int EffectID{ get{ return 0x1BFE; } }
		public override Type AmmoType { get { return GetBoltSelected(); } }
		public override Item Ammo { get { return AmmoSelected(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MovingShot; } }

		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return 10; } }
		public override int AosMaxDamage{ get{ return 12; } }
		public override int AosSpeed{ get{ return 41; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 10; } }
		public override int OldMaxDamage{ get{ return 12; } }
		public override int OldSpeed{ get{ return 41; } }

		public override int DefMaxRange{ get{ return 7; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 80; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public BoltType BoltSelection
		{
			get { return m_BoltType; }
			set { m_BoltType = value; InvalidateProperties(); }
		}		
		
		[Constructable]
		public AdvancedRepeatingCrossbow() : base( 0x26C3 )
		{
			Weight = 6.0;
		}

        public AdvancedRepeatingCrossbow(Serial serial)
            : base(serial)
		{
		}

		public virtual Item AmmoSelected()
		{
			switch (m_BoltType)
			{
				case BoltType.Normal:
					return new Bolt();
				case BoltType.Poison:
					return new PoisonBolt();
				case BoltType.Explosive:
					return new ExplosiveBolt();
				case BoltType.ArmorPiercing:
					return new ArmorPiercingBolt();
				case BoltType.Freeze:
					return new FreezeBolt();
				case BoltType.Lightning:
					return new LightningBolt();
					
				default:
					return new Bolt();
			}
		}
		
		public virtual Type GetBoltSelected()
		{
			switch (m_BoltType)
			{
				case BoltType.Normal:
					return typeof(Bolt);
				case BoltType.Poison:
					return typeof(PoisonBolt);
				case BoltType.Explosive:
					return typeof(ExplosiveBolt);
				case BoltType.ArmorPiercing:
					return typeof(ArmorPiercingBolt);
				case BoltType.Freeze:
					return typeof(FreezeBolt);
				case BoltType.Lightning:
					return typeof(LightningBolt);
					
				default:
					return typeof(Bolt);
			}
		}	
		
		public override void OnDoubleClick(Mobile from)
		{
			if (IsChildOf(from.Backpack) || Parent == from)
			{
				from.SendMessage("Please choose which type of bolts you wish to use.");
				from.Target = new InternalTarget(this);
			}
			
			else
				return;
		}
		
		private class InternalTarget : Target
		{
            private AdvancedRepeatingCrossbow it_Bow;

            public InternalTarget(AdvancedRepeatingCrossbow bow)
                : base(1, false, TargetFlags.None)
			{
				it_Bow = bow;
			}
			
			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is Item)
				{
					Item item = (Item)targeted;
					
					if (item.GetType() == typeof(Bolt))
						it_Bow.BoltSelection = BoltType.Normal;
					else if (item.GetType() == typeof(PoisonBolt) )
						it_Bow.BoltSelection = BoltType.Poison;
					else if (item.GetType() == typeof(ExplosiveBolt) )
						it_Bow.BoltSelection = BoltType.Explosive;
					else if (item.GetType() == typeof(ArmorPiercingBolt) )
						it_Bow.BoltSelection = BoltType.ArmorPiercing;
					else if (item.GetType() == typeof(FreezeBolt) )
						it_Bow.BoltSelection = BoltType.Freeze;
					else if (item.GetType() == typeof(LightningBolt) )
						it_Bow.BoltSelection = BoltType.Lightning;
					else
						from.SendMessage("Must select an Bolt Type");
					
				}
				else
					from.SendMessage("Can Only Target Bolt Items");
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			list.Add(1060662, "{0}\t{1}", "Bolt Type", m_BoltType.ToString());
		}
		
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)1); // version
			
			writer.WriteEncodedInt((int)m_BoltType);
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			
			if (Weight == 7.0)
				Weight = 6.0;

			if (version == 0)
				version = 1;

			switch ( version )
			{
				case 1:
				{
					m_BoltType = (BoltType)reader.ReadEncodedInt();
					goto case 0;
				}

				case 0:
				{
					break;
				}
			}
		}
	}
}
