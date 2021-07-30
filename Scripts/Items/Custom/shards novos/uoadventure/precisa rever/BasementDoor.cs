using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Regions;
using System.Collections.Generic;
using System.Collections;
using Server.Commands;
using Server.Prompts;
using Server.ContextMenus;
using Server.Gumps;
using Server.Targeting;
using Server.Multis;
using Server.Spells;

namespace Server.Items
{
	[FlipableAttribute( 0x02C1, 0x02C2, 0x02C3, 0x02C4 )]
	public class BasementDoor : Item, ISecurable
	{
		

        public SecureLevel m_Level;
        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level { get { return m_Level; } set { m_Level = value; } }

		public string DoorShop;
		[CommandProperty(AccessLevel.Owner)]
		public string Door_Shop { get { return DoorShop; } set { DoorShop = value; InvalidateProperties(); } }

		[Constructable]
		public BasementDoor() : base( 0x02C1 )
		{
			Name = "basement trapdoor";
			Weight = 10;
			ItemID = Utility.RandomList( 0x02C1, 0x02C2, 0x02C3, 0x02C4 );

		
		}

		public void DoBasementDoor( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				Point3D p = new Point3D( 4095, 3550, 40 );
					if ( DoorShop == "iron" ){ p = new Point3D( 4075, 3562, 20 ); }
					else if ( DoorShop == "cloth" ){ p = new Point3D( 4100, 3534, 20 ); }
					else if ( DoorShop == "wood" ){ p = new Point3D( 4118, 3533, 20 ); }

				PlayerMobile pc = (PlayerMobile)m;

				

				string sX = m.X.ToString();
				string sY = m.Y.ToString();
				string sZ = m.Z.ToString();
				
				string sZone = this.Name;

				

				PublicTeleport( m, p, Map.Trammel, "the Basement", "enter" );
			}
		}

		

       

		public static void PublicTeleport( Mobile m, Point3D loc, Map map, string zone, string direction )
		{
			BaseCreature.TeleportPets( m, loc, map, false );
			m.MoveToWorld ( loc, map );
			m.PlaySound( 234 );
			
		}

		public static void ConfigureBasementDoors()
		{
			foreach ( Item item in World.Items.Values )
			if ( item is BasementDoor )
			{
				BasementDoor door = (BasementDoor)item;
				if ( door.Name == "iron" || door.Name == "cloth" || door.Name == "wood" || door.Name == "shop" ){ door.DoorShop = door.Name; }
				door.Name = "basement trapdoor";
			}
		}

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            SetSecureLevelEntry.AddTo(from, this, list);
        }      

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );		
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );
		}

		public bool CheckAccess(Mobile m)
		{
			BaseHouse house = BaseHouse.FindHouseAt(this);

			if (house != null && (house.Public ? house.IsBanned(m) : !house.HasAccess(m)))
				return false;

			return (house != null && house.HasSecureAccess(m, m_Level));
		}

		public static bool HatchAtOtherEnd( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				string sPublicDoor = "";
				int mX = 0;
				int mY = 0;
				int mZ = 0;
				Map mWorld = null;

				PlayerMobile pc = (PlayerMobile)m;

				

				if ( sPublicDoor != null )
				{
					string[] sPublicDoors = sPublicDoor.Split('#');
					int nEntry = 1;
					foreach (string exits in sPublicDoors)
					{
						if ( nEntry == 1 ){ mX = Convert.ToInt32(exits); }
						else if ( nEntry == 2 ){ mY = Convert.ToInt32(exits); }
						else if ( nEntry == 3 ){ mZ = Convert.ToInt32(exits); }
						else if ( nEntry == 4 ){ try { mWorld = Map.Parse( exits ); } catch{} if ( mWorld == null ){ mWorld = Map.Trammel; } }
						nEntry++;
					}
				}

				Point3D loc = new Point3D( mX, mY, mZ );

				if ( mWorld == null )
					return false;

				IPooledEnumerable eable = mWorld.GetItemsInRange( loc, 4 );

				foreach ( Item item in eable )
				{
					if ( item is BasementDoor )
					{
						eable.Free(); return true;
					}
				}

				eable.Free();
				return false;
			}
			return false;
		}

		public BasementDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write((int)m_Level);
            writer.Write( DoorShop );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel)reader.ReadInt();
			DoorShop = reader.ReadString();
			if ( DoorShop == "" || DoorShop == null ){ Visible = true; }

		
		}
	}
}
