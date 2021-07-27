/* Created by Hammerhand*/

using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class Flame : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }

        public override int Hue { get { return 1359; } }
		public override int AosStrengthReq{ get{ return 25; } }
		public override int AosMinDamage{ get{ return 11; } }
		public override int AosMaxDamage{ get{ return 13; } }
		public override int AosSpeed{ get{ return 46; } }
        public override float MlSpeed { get { return 3.25f; } }

		public override int OldStrengthReq{ get{ return 10; } }
		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 26; } }
		public override int OldSpeed{ get{ return 58; } }

		public override int DefHitSound{ get{ return 0x23B; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 131; } }
		public override int InitMaxHits{ get{ return 190; } }

		[Constructable]
		public Flame() : base( 0x13FF )
		{
            Name = "Flame";
            Hue = 1359;
			Weight = 6.0;

            Attributes.AttackChance = Utility.RandomMinMax(10, 35);
            Attributes.BonusDex = Utility.RandomMinMax(9, 20);
            Attributes.SpellChanneling = 1;
            Attributes.Luck = Utility.RandomMinMax(140, 225);
            Attributes.ReflectPhysical = Utility.RandomMinMax(18, 30);

            WeaponAttributes.HitFireArea = Utility.RandomMinMax(10, 25);
            WeaponAttributes.HitLightning = Utility.RandomMinMax(8, 15);
            WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.DurabilityBonus = 15;
            WeaponAttributes.HitLeechStam = 10;
            WeaponAttributes.SelfRepair = 5;

            LootType = LootType.Regular;
		}

		public Flame( Serial serial ) : base( serial )
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