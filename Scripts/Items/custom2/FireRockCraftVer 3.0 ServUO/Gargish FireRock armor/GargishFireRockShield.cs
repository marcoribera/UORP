/* Created by Hammerhand*/

using System;
using Server;

namespace Server.Items
{
	public class GargishFlameShield : BaseShield
	{
        public override int Hue { get { return 1359; } }
		public override int BaseFireResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 250; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override int AosStrReq{ get{ return 90; } }
		public override int ArmorBase{ get{ return 23; } }

        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

		[Constructable]
		public GargishFlameShield() : base( 0x4205 )
		{
            Name = "Gargish Flame Shield";
            Hue = 1359;
			Weight = 8.0;

            Attributes.BonusStam = Utility.RandomMinMax(4, 11);
            Attributes.RegenStam = Utility.RandomMinMax(5, 13);
            Attributes.SpellDamage = Utility.RandomMinMax(20, 35);
            Attributes.Luck = Utility.RandomMinMax(110, 250);
            Attributes.BonusStr = Utility.RandomMinMax(5, 10);
            Attributes.SpellChanneling = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(7, 15);

            PhysicalBonus = Utility.RandomMinMax(4, 10);
            FireBonus = Utility.RandomMinMax(9, 15);

            LootType = LootType.Regular;
		}

        public GargishFlameShield(Serial serial)
            : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
}
