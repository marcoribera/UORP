using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	// Based off a Spear
	[FlipableAttribute( 0x904, 0x406D )]
	public class ForkedFire : BaseSpear
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }
        public override int Hue { get { return 1359; } }
		public override int AosStrengthReq{ get{ return 50; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 42; } }
		public override float MlSpeed{ get{ return 2.00f; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 2; } }
		public override int OldMaxDamage{ get{ return 36; } }
		public override int OldSpeed{ get{ return 46; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 80; } }

        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

		[Constructable]
		public ForkedFire() : base( 0x904 )
		{
            Name = "Forked Fire";
            Hue = 1359;
			Weight = 7.0;

            Attributes.AttackChance = Utility.RandomMinMax(7, 40);
            Attributes.BonusDex = Utility.RandomMinMax(3, 15);
            Attributes.WeaponSpeed = Utility.RandomMinMax(8, 20);
            Attributes.SpellChanneling = 1;
            Attributes.Luck = Utility.RandomMinMax(150, 250);
            Attributes.RegenStam = Utility.RandomMinMax(8, 11);
            Attributes.ReflectPhysical = Utility.RandomMinMax(12, 25);

            WeaponAttributes.HitFireArea = Utility.RandomMinMax(8, 35);
            WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.HitLowerDefend = 10;
            WeaponAttributes.SelfRepair = 5;
		}

        public ForkedFire(Serial serial)
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