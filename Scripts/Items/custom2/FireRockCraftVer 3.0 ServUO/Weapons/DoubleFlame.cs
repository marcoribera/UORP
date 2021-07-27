/* Created by Hammerhand*/

using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class DoubleFlame : BaseAxe
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.WhirlwindAttack; } }

        public override int Hue { get { return 1359; } }
		public override int AosStrengthReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 33; } }
        public override float MlSpeed { get { return 3.25f; } }

		public override int OldStrengthReq{ get{ return 45; } }
		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 35; } }
		public override int OldSpeed{ get{ return 37; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }

		[Constructable]
		public DoubleFlame() : base( 0xF4B )
		{
            Name = "Double Flame";
            Hue = 1359;
			Weight = 8.0;

            Attributes.AttackChance = Utility.RandomMinMax(7, 25);
            Attributes.BonusDex = Utility.RandomMinMax(3, 10);
            Attributes.WeaponSpeed = Utility.RandomMinMax(15, 30);
            Attributes.SpellChanneling = 1;
            Attributes.Luck = Utility.RandomMinMax(100, 140);
            Attributes.RegenStam = Utility.RandomMinMax(8, 15);
            WeaponAttributes.HitFireArea = Utility.RandomMinMax(6, 20);
            WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.HitLowerDefend = 17;
            WeaponAttributes.SelfRepair = 5;

            LootType = LootType.Regular;
		}

        public DoubleFlame(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}