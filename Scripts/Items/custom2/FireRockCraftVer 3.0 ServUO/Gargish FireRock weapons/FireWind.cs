/* Created by Hammerhand */

using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class FireWind : BaseThrown
	{
		public override int MinThrowRange{ get{ return 7; } }		// MaxRange 10
        public override int Hue { get { return 1359; } }
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 60; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 22; } }
		public override int AosSpeed{ get{ return 25; } }
		public override float MlSpeed{ get{ return 4.00f; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 9; } }
		public override int OldMaxDamage{ get{ return 41; } }
		public override int OldSpeed{ get{ return 20; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

		[Constructable]
		public FireWind() : base( 0x090A )
		{
            Name = "FireWind";
			Weight = 6.0;
            Hue = 1359;
			Layer = Layer.OneHanded;

            WeaponAttributes.BattleLust = 1;
            Velocity = Utility.RandomMinMax(6, 13);
            Attributes.SpellChanneling = 1;
            Attributes.AttackChance = Utility.RandomMinMax(7, 15);
            Attributes.ReflectPhysical = Utility.RandomMinMax(6, 14);
            Attributes.SpellChanneling = 1;
            Attributes.RegenHits = Utility.RandomMinMax(4, 7);
            Attributes.WeaponSpeed = Utility.RandomMinMax(10, 15);
            Attributes.WeaponDamage = Utility.RandomMinMax(9, 16);
		}
        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct)
        {
            direct = chaos = nrgy = pois = cold =0;
            phys = 40;
            fire = 60;
        }
        public FireWind(Serial serial)
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