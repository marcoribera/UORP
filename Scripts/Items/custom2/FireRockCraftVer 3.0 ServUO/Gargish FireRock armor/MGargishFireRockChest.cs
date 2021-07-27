/* Created by Hammerhand*/

using System;
using Server.Items;

namespace Server.Items
{
	public class MGargishFireRockChest : BaseArmor
	{
        public override int Hue { get { return 1359; } }
		public override int BaseFireResistance{ get{ return 6; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override int AosStrReq{ get{ return 95; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int OldDexBonus{ get{ return -8; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

        public override bool AllowFemaleWearer { get { return false; } }

		[Constructable]
        public MGargishFireRockChest()
            : base(0x030A)
		{
            Name = "M Gargish FireRock Chest";
            Hue = 1359;
			Weight = 10.0;

            Attributes.BonusDex = Utility.RandomMinMax(3, 5);
            Attributes.BonusHits = Utility.RandomMinMax(2, 7);
            Attributes.DefendChance = Utility.RandomMinMax(4, 15);
            Attributes.LowerManaCost = Utility.RandomMinMax(7, 15);
            Attributes.Luck = Utility.RandomMinMax(75, 125);
            Attributes.ReflectPhysical = Utility.RandomMinMax(4, 12);
            Attributes.RegenStam = Utility.RandomMinMax(2, 6);
            ArmorAttributes.SelfRepair = 3;

            EnergyBonus = Utility.RandomMinMax(3, 5);
            FireBonus = Utility.RandomMinMax(13, 15);
            PhysicalBonus = Utility.RandomMinMax(4, 12);

            LootType = LootType.Regular;
		}

        public MGargishFireRockChest(Serial serial)
            : base(serial)
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

			if ( Weight == 1.0 )
				Weight = 10.0;
		}
	}
}