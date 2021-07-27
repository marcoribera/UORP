/* Created by Hammerhand*/

using System;
using Server.Items;

namespace Server.Items
{
	public class MGargishCrystalineFireKilt : BaseArmor
	{
        public override int Hue { get { return 1357; } }
		public override int BaseFireResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override int AosStrReq{ get{ return 90; } }

		public override int OldStrReq{ get{ return 60; } }
		public override int OldDexBonus{ get{ return -6; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

        public override bool AllowFemaleWearer { get { return false; } }

		[Constructable]
        public MGargishCrystalineFireKilt()
            : base(0x030C)
		{
            Name = "M Gargish CrystalineFire Kilt";
            Hue = 1357;
			Weight = 7.0;

            Attributes.BonusHits = Utility.RandomMinMax(3, 8);
            Attributes.DefendChance = Utility.RandomMinMax(7, 19);
            Attributes.LowerRegCost = Utility.RandomMinMax(8, 20);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(9, 20);
            Attributes.RegenHits = Utility.RandomMinMax(2, 7);
            Attributes.RegenStam = Utility.RandomMinMax(3, 5);

            EnergyBonus = Utility.RandomMinMax(2, 7);
            FireBonus = Utility.RandomMinMax(13, 25);
            PhysicalBonus = Utility.RandomMinMax(3, 15);

            LootType = LootType.Regular;
		}

        public MGargishCrystalineFireKilt(Serial serial)
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
		}
	}
}