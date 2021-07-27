/* Created by Hammerhand */

using System;
using Server.Items;

namespace Server.Items
{
	public class FGargishCrystalineFireChest : BaseArmor
	{
        public override int Hue { get { return 1357; } }
		public override int BaseFireResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override int AosStrReq{ get{ return 95; } }
		public override int OldStrReq{ get{ return 45; } }
		public override int OldDexBonus{ get{ return -5; } }
		public override int ArmorBase{ get{ return 30; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }
        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

        public override bool AllowMaleWearer { get { return false; } }

		[Constructable]
        public FGargishCrystalineFireChest()
            : base(0x0309)
		{
            Name = "F Gargish CrystalineFire Chest";
            Hue = 1357;
			Weight = 4.0;

            Attributes.BonusDex = Utility.RandomMinMax(3, 5);
            Attributes.BonusInt = Utility.RandomMinMax(2, 5);
            Attributes.DefendChance = Utility.RandomMinMax(8, 15);
            Attributes.Luck = Utility.RandomMinMax(75, 125);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(8, 20);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);
            Attributes.RegenStam = Utility.RandomMinMax(2, 5);
            ArmorAttributes.DurabilityBonus = 10;

            FireBonus = Utility.RandomMinMax(13, 25);
            PoisonBonus = Utility.RandomMinMax(3, 9);
            PhysicalBonus = Utility.RandomMinMax(4, 12);
            StrBonus = Utility.RandomMinMax(3, 5);

            LootType = LootType.Regular;
		}

        public FGargishCrystalineFireChest(Serial serial)
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
				Weight = 4.0;
		}
	}
}