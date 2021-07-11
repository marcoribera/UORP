using System;
using Server;
using Server.Items;
using Server.Prompts;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Regions;
using Server.Multis;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
	public class BeeHiveHouse : AddonComponent, IChopable
	{
		public int m_HiveHoney;
		public int m_HiveWaxes;
		public int m_ApiChance;
		public int m_LoreSkill;
		public bool m_HiveFull;
		public bool m_HiveSick;

		[CommandProperty( AccessLevel.GameMaster )]
		public int HiveHoney { get { return m_HiveHoney; } set { m_HiveHoney = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int HiveWaxes { get { return m_HiveWaxes; } set { m_HiveWaxes = value; } }

		[CommandProperty( AccessLevel.GameMaster )] public int ApiChance { get { return m_ApiChance; } set { m_ApiChance = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int LoreSkill { get { return m_LoreSkill; } set { m_LoreSkill = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool HiveFull { get { return m_HiveFull; } set { m_HiveFull = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool HiveSick { get { return m_HiveSick; } set { m_HiveSick = value; } }

		[Constructable]
		public BeeHiveHouse() : base( 0x91a )
		{
			Name = "A Bee Hive";
			Movable = false;
		}

		public override void OnSingleClick( Mobile from ) { this.LabelTo( from, this.Name ); }

		public override void OnDoubleClick (Mobile from )
		{
			BaseHouse house = BaseHouse.FindHouseAt( from );
			if ( house == null || !house.IsCoOwner( from ) ) from.SendLocalizedMessage( 502092 );
			else if ( !house.IsInside( this ) ) from.SendLocalizedMessage( 1042270 );
			else if (! from.InRange( this.GetWorldLocation(), 1 )) from.LocalOverheadMessage( MessageType.Regular, 906, 1019045 );
			else
			{
				m_LoreSkill = ((int)(from.Skills[SkillName.Agricultura].Value));
				m_ApiChance = Utility.Random( 1, 100 );
				Container pack = from.Backpack;

				if ((m_HiveFull == false) && (m_HiveSick == false))
				{
					new BeeHiveHouseTimer(this).Start();
					this.PublicOverheadMessage( MessageType.Regular, 0x35, false, string.Format("The Bee Hive starts to be colonized and the bees begin working hard." ));
					m_HiveHoney = 0;
					m_HiveWaxes = 0;
				}
				else if ((m_HiveFull == true) && (m_HiveSick == true))
				{
					this.PublicOverheadMessage( MessageType.Regular, 0x35, false, string.Format("This Bee Hive is sick, the bees need to be cured." ));
				}
				else if ((m_HiveFull == true) && (m_ApiChance > m_LoreSkill) && (m_HiveSick == false) )
				{
					if ( Utility.RandomDouble() > (from.Skills[SkillName.Agricultura].Value / 100) )
					{
						from.FixedParticles( 0x91B, 64, 240, 9916, 0, 3, EffectLayer.Head );
						from.Poison = Poison.Lesser;
						from.SendMessage(0x33, "You have angered the bees!" );
						from.PlaySound (0x230);
					}
					else from.SendMessage( "The bees are to agitated to get the honey from right now.");
				}
				else if ((m_HiveFull == true) && (m_HiveSick == false) && (m_HiveHoney >= 1) && (m_HiveWaxes >= 1))
				{
					if ( from.Skills.Culinaria.Base >= 100)
					{
						m_HiveHoney += Utility.Random( ((int)(from.Skills.Culinaria.Value / 75)) );
						m_HiveWaxes += Utility.Random( ((int)(from.Skills.Culinaria.Value / 75)) );
					}
					from.AddToBackpack( new JarHoney(HiveHoney) );
					from.AddToBackpack( new Beeswax(m_HiveWaxes) );

					from.SendMessage( String.Format( "You gather {0} honey and {1} wax.", m_HiveHoney, m_HiveWaxes ) );
					from.PlaySound (0x58);

					m_HiveHoney = 0;
					m_HiveWaxes = 0;
				}
				else this.PublicOverheadMessage( MessageType.Regular, 0x35, false, string.Format("The bees are working hard." ));
			}
		}

		public void OnChop(Mobile from)
		{
			BaseHouse house = BaseHouse.FindHouseAt(from);
			if (house != null && house.IsOwner(from))
			{
				ArrayList list = new ArrayList();
				foreach ( Item itembee in this.GetItemsInRange( 0 ) )
				{
					if (itembee is BeeSwarm) list.Add( itembee );
				}
				if (list.Count > 0)
				{
					for ( int j = 0; j < list.Count; ++j )
					{
						Item item2 = (Item)list[j];
						if (item2 != null) item2.Delete();
					}
				}
			}
			base.OnChop(from);
		}

		public BeeHiveHouse( Serial serial ) : base( serial ) { }
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int) m_HiveHoney );
			writer.Write( (int) m_HiveWaxes );
			writer.Write( (int) m_ApiChance );
			writer.Write( (int) m_LoreSkill);
			writer.Write( (bool) m_HiveFull);
			writer.Write( (bool) m_HiveSick);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_HiveHoney = reader.ReadInt();
			m_HiveWaxes = reader.ReadInt();
			m_ApiChance = reader.ReadInt();
			m_LoreSkill = reader.ReadInt();
			m_HiveFull = reader.ReadBool();
			m_HiveSick = reader.ReadBool();
			if ( m_HiveFull == true ) new BeeHiveHouseTimer(this).Start();
		}
	}

	public class BeeHiveHouseAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new BeeHiveHouseDeed(); } }

		[Constructable]
		public BeeHiveHouseAddon()
		{
			AddComponent( new BeeHiveHouse(), 0, 0, 0 );
		}

		public BeeHiveHouseAddon( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BeeHiveHouseDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new BeeHiveHouseAddon(); } }
		public override int LabelNumber{ get{ return 1022330; } }

		[Constructable]
		public BeeHiveHouseDeed(){ Name = "Bee Hive";}

		public BeeHiveHouseDeed( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}