/* Created by Hammerhand*/

using System;
using Server;

namespace Server.Items
{
	public class FlameShield : BaseShield
	{
        public override int Hue { get { return 1359; } }
		public override int BaseFireResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 250; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override int AosStrReq{ get{ return 90; } }

		public override int ArmorBase{ get{ return 23; } }

		[Constructable]
		public FlameShield() : base( 0x2B01 )
		{
            Name = "Flame Shield";
            Hue = 1359;
			Weight = 8.0;

            Attributes.CastRecovery = 15;
            Attributes.CastSpeed = 10;
            Attributes.SpellDamage = 35;
            Attributes.Luck = 1000;
            Attributes.BonusStr = 10;
            Attributes.WeaponSpeed = 35;
            Attributes.SpellChanneling = 1;
            Attributes.ReflectPhysical = 15;

            PhysicalBonus = 10;
            FireBonus = 15;

            LootType = LootType.Regular;
		}

        public FlameShield(Serial serial)
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
