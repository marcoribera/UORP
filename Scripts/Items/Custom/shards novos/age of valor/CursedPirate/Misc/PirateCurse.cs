/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\engines\CursedPirate\Misc\PirateCurse.cs
Lines of code: 253
***********************************************************/


using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items 
{ 
	public abstract class PirateCurse 
	{
        public static void CursedPirateLoot(BaseCreature pirate, int chance)
        {
            switch (Utility.Random(chance))
            {
                case 0: pirate.PackItem(new CursedJugOfRum()); break;
            }
        }

		public static void SummonPirate( Mobile m, int i_type )
		{
			Map map = m.Map;
			if (map == null)
				return;

			bool validLocation = false;
			Point3D loc = m.Location;

			for (int j = 0; !validLocation && j < 10; ++j)
			{
				int x = loc.X + Utility.Random(3) - 1;
				int y = loc.Y + Utility.Random(3) - 1;
				int z = map.GetAverageZ(x, y);

				if (validLocation = map.CanFit(x, y, loc.Z, 16, false, false))
					loc = new Point3D(x, y, loc.Z);
				else if (validLocation = map.CanFit(x, y, z, 16, false, false))
					loc = new Point3D(x, y, z);
			}

			if (!validLocation)
				return;

			BaseCreature spawn;
			
			switch ( i_type )
				{
				default: return;
				case 0:
				{
					m.SendMessage("You have summoned a cursed pirate!");

					spawn = new CursedPirate();
					break;
				}
				case 1:
				{
					m.SendMessage("Uh oh, you have summoned the cursed pirate king");

					spawn = new CursedPirateKing();
					break;
				}
			}

			spawn.FightMode = FightMode.Closest;

			spawn.MoveToWorld( loc, map );
			spawn.Combatant = m;
		}

		public static void HurtPlayer(Mobile m)
		{
			m.SendMessage("Ouch, that hurts");

			AOS.Damage(m, m, Utility.RandomMinMax(30, 150), 0, 100, 0, 0, 0);

			if (m.Alive && m.Body.IsHuman && !m.Mounted)
				m.Animate(20, 7, 1, true, false, 0); // take hit
		}

		public static void CursePlayer(Mobile m)
		{
			if (IsCursed(m))
				return;

			if (!UnEquipPlayer(m))
				return;

			ExpireTimer timer = (ExpireTimer)m_Table[m];

			if (timer != null)
				timer.DoExpire();
			else
				m.SendMessage("You feel yourself transform into a cursed pirate");

			Effects.SendLocationEffect(m.Location, m.Map, 0x3709, 28, 10, 0x1D3, 5);

			TimeSpan duration = TimeSpan.FromSeconds(240.0);

			ResistanceMod[] mods = new ResistanceMod[4]
			{
				new ResistanceMod( ResistanceType.Fire, -10 ),
				new ResistanceMod( ResistanceType.Poison, -10 ),
				new ResistanceMod( ResistanceType.Cold, +10 ),
				new ResistanceMod( ResistanceType.Physical, +10 )
			};

			timer = new ExpireTimer(m, mods, duration);
			timer.Start();

			m_Table[m] = timer;

			for (int i = 0; i < mods.Length; ++i)
				m.AddResistanceMod(mods[i]);

			//m.ApplyPoison(m, Poison.Greater);

			m.Criminal = true;
		}

		public static bool IsCursed(Mobile m)
		{
			BankBox bankBox = m.BankBox;
			if (bankBox == null)
				return false;

			Item piratebag = bankBox.FindItemByType(typeof(PirateBag));
			if (piratebag == null)
				return false;

			m.SendMessage("You are already under the effects of the pirate curse!");
			return true;
		}

		public static bool UnEquipPlayer(Mobile m)
		{
			ArrayList ItemsToMove = new ArrayList();

			PirateBag bag = new PirateBag();

			BankBox bankBox = m.BankBox;
			if (bankBox == null || !bankBox.TryDropItem(m, bag, false))
			{
				bag.Delete();
				return false;
			}
			else
			{
				foreach (Item item in m.Items)
					if (item.Layer != Layer.Bank && item.Layer != Layer.Hair && item.Layer != Layer.FacialHair && item.Layer != Layer.Mount && item.Layer != Layer.Backpack)
						ItemsToMove.Add(item);
				
				foreach (Item item in ItemsToMove)
					bag.AddItem(item);

				bag.Owner = m;
				bag.PlayerTitle = m.Title;
				bag.PlayerHue = m.Hue;
                
				m.Title = "the Cursed Pirate";
				m.Hue = Utility.RandomMinMax(0x8596, 0x8599);

				EquipPirateItems(m);
			}

			return true;
		}

		private static void EquipPirateItems(Mobile player)
		{
			//Equip the cursed pirate garb.
			//Cutlass
			Cutlass cutlass = new Cutlass();
			cutlass.Movable = false;
			cutlass.Skill = SkillName.Cortante;
            cutlass.Layer = Layer.OneHanded;
			player.AddItem(cutlass);

			//Fancy Shirt
			FancyShirt shirt = new FancyShirt(Utility.RandomNeutralHue());
			shirt.Movable = false;
			player.AddItem(shirt);

			//Long Pants
			LongPants pants = new LongPants(Utility.RandomNeutralHue());
			pants.Movable = false;
			player.AddItem(pants);

			//Tricorne Hat
			TricorneHat hat = new TricorneHat(Utility.RandomNeutralHue());
			hat.Movable = false;
			player.AddItem(hat);

			//Thigh Boots
			ThighBoots boots = new ThighBoots();
			boots.Movable = false;
			player.AddItem(boots);
		}

        public static bool UnequipPirateItems(Mobile m)
        {
            ArrayList ItemsToMove = new ArrayList();
            ArrayList ItemsToDelete = new ArrayList();

            Bag bag = new Bag();
            BankBox bankBox = m.BankBox;
            if (bankBox == null || !bankBox.TryDropItem(m, bag, false))
            {
                bag.Delete();
                m.SendMessage("There was a problem removing the pirate curse, please page for help!  Code 101");
                return false;
            }

            Bag bag2 = new Bag();
            bag2.Name = "Pirate Curse Unequiped Items";
            bag2.Hue = m.Hue;

            Container pack = m.Backpack;
            if (pack == null && !pack.CheckHold(m, bag2, false, true))
            {
                bag2.Delete();
                m.SendMessage("There was a problem removing the pirate curse, please page for help!  Code 102");
                return false;
            }
            pack.AddItem(bag2);

            foreach (Item item in m.Items)
            {
                if (item.Layer != Layer.Bank && item.Layer != Layer.Hair && item.Layer != Layer.FacialHair && item.Layer != Layer.Mount && item.Layer != Layer.Backpack)
                {
                    if (item.Layer == Layer.OneHanded || item.Layer == Layer.Shirt || item.Layer == Layer.Pants || item.Layer == Layer.Helm || item.Layer == Layer.Shoes)
                        ItemsToDelete.Add(item);
                    else
                        ItemsToMove.Add(item);
                }
            }

            foreach (Item item in ItemsToDelete)
                bag.AddItem(item);

            bag.Delete();

            foreach (Item item in ItemsToMove)
                bag2.AddItem(item);

            RestorePlayerItems(m);

            return true;
        }

        public static bool RestorePlayerItems(Mobile player)
        {
            ArrayList ItemsToEquip = new ArrayList();

            BankBox bankBox = player.BankBox;
            if (bankBox == null)
            {
                player.SendMessage("There was a problem returning your item, please page for help!  Code 201");

                return false;
            }

            Item bag = bankBox.FindItemByType(typeof(PirateBag));
            if (bag == null)
            {
                player.SendMessage("There was a problem returning your item, please page for help!  Code 202");

                return false;
            }

            PirateBag piratebag = bag as PirateBag;
            foreach (Item item in piratebag.Items)
                ItemsToEquip.Add(item);

            foreach (Item item in ItemsToEquip)
                player.AddItem(item);

            player.Title = piratebag.PlayerTitle;
            player.Hue = piratebag.PlayerHue;

            piratebag.Delete();

            return true;
        }

		private static Hashtable m_Table = new Hashtable();

		public static bool RemoveCurse(Mobile m)
		{
			ExpireTimer t = (ExpireTimer)m_Table[m];

			if (t == null)
				return false;

			m.SendMessage("The effects of the pirate curse have been removed");
			t.DoExpire();

			return true;
		}

		private class ExpireTimer : Timer
		{
			private Mobile m_Mobile;
			private ResistanceMod[] m_Mods;

			public ExpireTimer(Mobile m, ResistanceMod[] mods, TimeSpan delay):base(delay)
			{
				m_Mobile = m;
				m_Mods = mods;
			}

			public void DoExpire()
			{
				UnequipPirateItems(m_Mobile);

				for (int i = 0; i < m_Mods.Length; ++i)
					m_Mobile.RemoveResistanceMod(m_Mods[i]);

				if (m_Mobile.Criminal)
					m_Mobile.Criminal = false;
				if (m_Mobile.Poisoned)
					m_Mobile.CurePoison(m_Mobile);

				Stop();
				m_Table.Remove(m_Mobile);
			}

			protected override void OnTick()
			{
				m_Mobile.SendMessage("You feel the effects of the pirate curse wear off");
				DoExpire();
			}
		}
	}
} 
