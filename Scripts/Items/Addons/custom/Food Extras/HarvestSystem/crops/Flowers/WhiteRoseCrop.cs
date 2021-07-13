using System;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
namespace Server.Items.Crops
{
	public class WhiteRoseSeed : BaseCrop
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public WhiteRoseSeed() : this( 1 ) { }

		[Constructable]
		public WhiteRoseSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "White Rose Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Mounted && !CropHelper.CanWorkMounted ) { from.SendMessage( "You cannot plant a seed while mounted." ); return; }
			Point3D m_pnt = from.Location;
			Map m_map = from.Map;
			if ( !IsChildOf( from.Backpack ) ) { from.SendLocalizedMessage( 1042010 ); return; }
			else if ( !CropHelper.CheckCanGrow( this, m_map, m_pnt.X, m_pnt.Y ) ) { from.SendMessage( "This seed will not grow here." ); return; }
			ArrayList cropshere = CropHelper.CheckCrop( m_pnt, m_map, 0 );
			if ( cropshere.Count > 0 ) { from.SendMessage( "There is already a crop growing here." ); return; }
			ArrayList cropsnear = CropHelper.CheckCrop( m_pnt, m_map, 1 );
			if ( ( cropsnear.Count > 8 ) ) { from.SendMessage( "There are too many crops nearby." ); return; }
			if ( this.BumpZ ) ++m_pnt.Z;
			if ( !from.Mounted ) from.Animate( 32, 5, 1, true, false, 0 );
			from.SendMessage("You plant the seed.");
			this.Consume();
			if (Utility.RandomDouble() <= 0.01)
			{
				Item item2 = new BlackRoseSeedling( from );
				item2.Location = m_pnt;
				item2.Map = m_map;
				from.SendMessage("The seed was a Muntant Seed !!!!");
			}
			else
			{
				Item item = new WhiteRoseSeedling( from );
				item.Location = m_pnt;
				item.Map = m_map;
			}
		}

		public WhiteRoseSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class WhiteRoseSeedling : BaseCrop
	{
		private static Mobile m_sower;
		public Timer thisTimer;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Sower{ get{ return m_sower; } set{ m_sower = value; } }

		[Constructable]
		public WhiteRoseSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "White Rose Seedling";
			m_sower = sower;
			init( this );
		}
		public static void init( WhiteRoseSeedling plant )
		{
			plant.thisTimer = new CropHelper.GrowTimer( plant, typeof(WhiteRoseCrop), plant.Sower );
			plant.thisTimer.Start();
		}
		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Mounted && !CropHelper.CanWorkMounted ) { from.SendMessage( "The crop is too small to harvest while mounted." ); return; }
			else from.SendMessage( "This crop is too young to harvest." );
		}
		public WhiteRoseSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); writer.Write( m_sower ); }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_sower = reader.ReadMobile();
			init( this );
		}
	}

	public class WhiteRoseCrop : BaseCrop
	{
		private const int max = 1;
		private int fullGraphic;
		private int pickedGraphic;
		private DateTime lastpicked;
		private Mobile m_sower;
		private int m_yield;
		public Timer regrowTimer;
		private DateTime m_lastvisit;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime LastSowerVisit{ get{ return m_lastvisit; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Growing{ get{ return regrowTimer.Running; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Sower{ get{ return m_sower; } set{ m_sower = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Yield{ get{ return m_yield; } set{ m_yield = value; } }

		public int Capacity{ get{ return max; } }
		public int FullGraphic{ get{ return fullGraphic; } set{ fullGraphic = value; } }
		public int PickGraphic{ get{ return pickedGraphic; } set{ pickedGraphic = value; } }
		public DateTime LastPick{ get{ return lastpicked; } set{ lastpicked = value; } }

		[Constructable]
		public WhiteRoseCrop() : this(null) { }

		[Constructable]
		public WhiteRoseCrop( Mobile sower ) : base( 0x234D )
		{
			Movable = false;
			Name = "White Rose Plant";
			Hue = 1953;
			m_sower = sower;
			m_lastvisit = DateTime.Now;
			init( this, false );
		}

		public static void init ( WhiteRoseCrop plant, bool full )
		{
			plant.PickGraphic = ( 0x234C );
			plant.FullGraphic = ( 0x234D );
			plant.LastPick = DateTime.Now;
			plant.regrowTimer = new CropTimer( plant );
			if ( full ) { plant.Yield = plant.Capacity; ((Item)plant).ItemID = plant.FullGraphic; }
			else { plant.Yield = 0; ((Item)plant).ItemID = plant.PickGraphic; plant.regrowTimer.Start(); }
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_sower == null || m_sower.Deleted ) m_sower = from;
			if ( from != m_sower ) { from.SendMessage( "You do not own this plant !!!" ); return; }

			if ( from.Mounted && !CropHelper.CanWorkMounted ) { from.SendMessage( "You cannot harvest a crop while mounted." ); return; }
			if ( DateTime.Now > lastpicked.AddSeconds(3) )
			{
				lastpicked = DateTime.Now;
				int cookValue = (int)from.Skills[SkillName.Culinaria].Value / 20;
				if ( cookValue == 0 ) { from.SendMessage( "You have no idea how to harvest this crop." ); return; }
				if ( from.InRange( this.GetWorldLocation(), 1 ) )
				{
					if ( m_yield < 1 ) { from.SendMessage( "There is nothing here to harvest." ); }
					else
					{
						from.Direction = from.GetDirectionTo( this );
						from.Animate( from.Mounted ? 29:32, 5, 1, true, false, 0 );
						m_lastvisit = DateTime.Now;
						if ( cookValue > m_yield ) cookValue = m_yield + 1;
						int pick = Utility.RandomMinMax( cookValue - 4, cookValue );
						if (pick < 0 ) pick = 0;
						if ( pick == 0 ) { from.SendMessage( "You do not manage to harvest any crops." ); return; }
						m_yield -= pick;
						from.SendMessage( "You harvest {0} crop{1}!", pick, ( pick == 1 ? "" : "s" ) );
						if (m_yield < 1) ((Item)this).ItemID = pickedGraphic;
						WhiteRose crop = new WhiteRose();
						from.AddToBackpack( crop );
						if (pick == 2)
						{
							WhiteRose crop2 = new WhiteRose();
							from.AddToBackpack( crop2 );
						}
						if ( !regrowTimer.Running ) { regrowTimer.Start(); }
					}
				}
				else { from.SendMessage( "You are too far away to harvest anything." ); }
			}
		}

		private class CropTimer : Timer
		{
			private WhiteRoseCrop i_plant;
			public CropTimer( WhiteRoseCrop plant ) : base( TimeSpan.FromSeconds( 450 ), TimeSpan.FromSeconds( 15 ) )
			{
				Priority = TimerPriority.OneSecond;
				i_plant = plant;
			}
			protected override void OnTick()
			{
				if ( Utility.RandomBool() )
				{
					if ( ( i_plant != null ) && ( !i_plant.Deleted ) )
					{
						int current = i_plant.Yield;
						if ( ++current >= i_plant.Capacity )
						{
							current = i_plant.Capacity;
							((Item)i_plant).ItemID = i_plant.FullGraphic;
							Stop();
						}
						else if ( current <= 0 ) current = 1;
						i_plant.Yield = current;
					}
					else Stop();
				}
			}
		}

		public override void OnChop( Mobile from )
		{
			if ( !this.Deleted ) Chop( from );
		}

		public void Chop( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				if ( from == m_sower )
				{
					from.Direction = from.GetDirectionTo( this );
					double lumberValue = from.Skills[SkillName.Extracao].Value / 100;
					if ( ( lumberValue > .5 ) && ( Utility.RandomDouble() <= lumberValue ) )
					{
						WhiteRose fruit = new WhiteRose( Utility.Random( m_yield +2 ) );
						from.AddToBackpack( fruit );
					}
						this.Delete();
						from.SendMessage( "You chop the plant up" );
				}
				else from.SendMessage( "You do not own this plant !!!" );
			}
			else from.SendLocalizedMessage( 500446 );
		}

		public WhiteRoseCrop( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( m_lastvisit );
			writer.Write( m_sower );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_lastvisit = reader.ReadDateTime();
			m_sower = reader.ReadMobile();
			init( this, true );
		}
	}
}