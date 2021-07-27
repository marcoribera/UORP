using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class SoaringFlames : BaseThrown
	{
		public override int MinThrowRange{ get{ return 4; } }		// MaxRange 8
        public override int Hue { get { return 1359; } }
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.MovingShot; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.InfusedThrow; } }

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 25; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 9; } }
		public override int OldMaxDamage{ get{ return 41; } }
		public override int OldSpeed{ get{ return 20; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

		[Constructable]
		public SoaringFlames() : base( 0x901 )
		{
            Name = "Soaring Flames";
            Hue = 1359;
			Weight = 6.0;
			Layer = Layer.OneHanded;

            AbsorptionAttributes.EaterFire = 5;
            Attributes.AttackChance = Utility.RandomMinMax(10, 35);
            Attributes.BonusDex = Utility.RandomMinMax(9, 20);
            Attributes.SpellChanneling = 1;
            Attributes.Luck = Utility.RandomMinMax(50, 125);
            Attributes.ReflectPhysical = Utility.RandomMinMax(18, 30);
            Attributes.WeaponSpeed = Utility.RandomMinMax(8, 25);

            WeaponAttributes.HitFireArea = Utility.RandomMinMax(10, 25);
            WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.DurabilityBonus = 15;
            WeaponAttributes.HitLeechStam = 10;
            WeaponAttributes.SelfRepair = 5;
		}

        public SoaringFlames(Serial serial)
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