using Server.Mobiles;
using Server.Gumps;
using Server.Network;
using Server.Misc;
using System.Collections.Generic;
using Server.Items;
using System.Linq;

namespace Server.Items
{
    public class TravelBook2 : Item 
	{
		[Constructable]
		public TravelBook2() : base( 0x2D50 )
		{
			Movable = true;
			Weight = 0;
            Hue = 96;
			Name = "Travel Book2";
			LootType = LootType.Blessed;			
		}		

		public override void OnDoubleClick( Mobile from )
		{
            from.SendGump( new TravelBookGump2( (PlayerMobile)from, this ) );
		}

		public TravelBook2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write(0); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            reader.ReadInt();
		}
	}
}

namespace Server.Gumps
{
    public class TravelBookGump2 : Gump
    {
        private static List<RunebookEntry> Locations = new List<RunebookEntry>
        {
            new RunebookEntry(new Point3D(1444,1227,0), Map.Trammel, "EVO Armor Gaurdian", null),
            new RunebookEntry(new Point3D(2723,2189,0), Map.Trammel, "Bucaneer's Den", null),
            new RunebookEntry(new Point3D(2234,1198,0), Map.Trammel, "Cove", null),
            new RunebookEntry(new Point3D(5228,3978,37), Map.Trammel, "Delucia", null),
            new RunebookEntry(new Point3D(1324,3779,0), Map.Trammel, "Jhelom", null),
            new RunebookEntry(new Point3D(2517,553,0), Map.Trammel, "Minoc", null),
            new RunebookEntry(new Point3D(4471,1178,0), Map.Trammel, "Moonglow", null),
            new RunebookEntry(new Point3D(3770,1315,0), Map.Trammel, "Nujel'm", null),

            new RunebookEntry(new Point3D(3686,2519,0), Map.Trammel, "Haven", null),
            new RunebookEntry(new Point3D(5675,3144,12), Map.Trammel, "Papua", null),
            new RunebookEntry(new Point3D(2875,3471,15), Map.Trammel, "Serpent's Hold", null),
            new RunebookEntry(new Point3D(591,2155,0), Map.Trammel, "Skara Brea", null),
            new RunebookEntry(new Point3D(1820,2822,0), Map.Trammel, "Trinsic", null),
            new RunebookEntry(new Point3D(2899,676,0), Map.Trammel, "Vesper", null),
            new RunebookEntry(new Point3D(5172,242,15), Map.Trammel, "Wind", null),
            new RunebookEntry(new Point3D(546,991,0), Map.Trammel, "Yew", null),

            new RunebookEntry(new Point3D(6032, 1500, 25), Map.Trammel, "Britain Passage", null),
            new RunebookEntry(new Point3D(2499, 922, 0), Map.Trammel, "Covetous", null),
            new RunebookEntry(new Point3D(4111, 434, 5), Map.Trammel, "Deceit", null),
            new RunebookEntry(new Point3D(5584, 631, 30), Map.Trammel, "Despise", null),
            new RunebookEntry(new Point3D(1176, 2639, 0), Map.Trammel, "Destard", null),
            new RunebookEntry(new Point3D(5761, 2907, 15), Map.Trammel, "Fire", null),
            new RunebookEntry(new Point3D(5222, 3661, 3), Map.Trammel, "Graveyards", null),
            new RunebookEntry(new Point3D(4722, 3825, 0), Map.Trammel, "Hythloth", null),

            new RunebookEntry(new Point3D(5499, 3224, 0), Map.Trammel, "Terathen Keep", null),
            new RunebookEntry(new Point3D(5154, 4063, 37), Map.Trammel, "Trinsic Passage", null),
            new RunebookEntry(new Point3D(2044, 238, 10), Map.Trammel, "Wrong", null),

            new RunebookEntry(new Point3D(1396, 432, -17), Map.Ilshenar, "Alecandretta's Bowl", null),
            new RunebookEntry(new Point3D(1517, 568, -13), Map.Ilshenar, "Ancient Citadel", null),
            new RunebookEntry(new Point3D(852, 602, -40), Map.Ilshenar, "Gargoyle City", null),
            new RunebookEntry(new Point3D(1203, 1124, -25), Map.Ilshenar, "Lake Shire", null),
            new RunebookEntry(new Point3D(820, 1073, -30), Map.Ilshenar, "Mistas", null),
            new RunebookEntry(new Point3D(1643, 310, 48), Map.Ilshenar, "Montor", null),
            new RunebookEntry(new Point3D(1362, 1073, -13), Map.Ilshenar, "Reg Volon", null),
            new RunebookEntry(new Point3D(1251, 743, -80), Map.Ilshenar, "Savage Camp", null),

            new RunebookEntry(new Point3D(567, 437, 21), Map.Ilshenar, "Terort Skitas", null),
            new RunebookEntry(new Point3D(576, 1150, -100), Map.Ilshenar, "Anka", null),
            new RunebookEntry(new Point3D(1747, 1228, -1), Map.Ilshenar, "Blood", null),
            new RunebookEntry(new Point3D(835, 777, -80), Map.Ilshenar, "Exodus", null),
            new RunebookEntry(new Point3D(1788, 573, 71), Map.Ilshenar, "Rock", null),
            new RunebookEntry(new Point3D(548, 462, -53), Map.Ilshenar, "Sorcerrs", null),
            new RunebookEntry(new Point3D(1363, 1033, -8), Map.Ilshenar, "Spectre", null),
            new RunebookEntry(new Point3D(651, 1302, -58), Map.Ilshenar, "Wisp", null),

            new RunebookEntry(new Point3D(998,519,-50), Map.Malas, "Luna", null),
            new RunebookEntry(new Point3D(2057,1343,-85), Map.Malas, "Umbra", null),
            new RunebookEntry(new Point3D(2367,1268,-85), Map.Malas, "Doom", null),

            new RunebookEntry(new Point3D(707,1237,25), Map.Tokuno, "Zento", null),
            new RunebookEntry(new Point3D(319,442,32), Map.Tokuno, "Bushido Dojo", null),
            new RunebookEntry(new Point3D(977,227,23), Map.Tokuno, "Fan Dancer's Dojo", null),
        };

        public TravelBookGump2(PlayerMobile from, Item item) : base(0, 0)
        {
            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
            AddImage(0, 0, 500);

            var i = 0;
            var page = 1;
            foreach(var m in Map.AllMaps.Where(s=> Locations.Any(l=>l.Map == s)))
            {
                AddLabel(88, 35 + 20 * i, 0, m.Name);
                var c = Locations.Where(l => l.Map == m).Count();
                AddButton(68, 40 + 20 * i++, 1209, 1209, 0, GumpButtonType.Page, page);
                page += c / 8 + (c % 8 > 0 ? 1 : 0);
            }

            AddLabel(53, 200, 0, ServerList.ServerName + " Travel Book2");

            page = 1;
            foreach (var m in Map.AllMaps.Where(s => Locations.Any(l => l.Map == s)))
            {
                AddPage(page++);
                i = 0;
                foreach(var l in Locations.Where(l => l.Map == m))
                {
                    if (i % 8 == 0 && i > 0)
                    {
                        AddPage(page++);
                        if (Locations.Where(j => j.Map == m).Count() > i)
                        {
                            AddLabel(235, 203, 0, @"Last Page");
                            AddButton(220, 206, 1209, 1209, 0, GumpButtonType.Page, page - 2);
                        }
                    }
                    if (i % 8 == 0)
                    {
                        AddLabel(293, 15, 0, m.Name);
                        if (Locations.Where(j => j.Map == m).Count() > i)
                        {
                            AddLabel(335, 203, 0, @"Next Page");
                            AddButton(320, 206, 1209, 1209, 0, GumpButtonType.Page, page);
                        }
                    }

                    AddLabel(273, 30 + 20 * (i % 8), 0, l.Description);
                    AddButton(251, 35 + 20 * (i++ % 8), 1209, 1209, Locations.IndexOf(l) + 1, GumpButtonType.Reply, 0);
                }
            }
        }
        
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 0)
                return;
            Mobile m = sender.Mobile;
            m.MoveToWorld(Locations[info.ButtonID - 1].Location, Locations[info.ButtonID - 1].Map);
        }

    }
}