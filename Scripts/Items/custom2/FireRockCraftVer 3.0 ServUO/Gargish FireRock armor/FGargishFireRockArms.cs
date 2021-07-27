/* Created by Hammerhand */

using System;
using Server.Items;

namespace Server.Items
{
    public class FGargishFireRockArms : BaseArmor
    {
        public override int Hue { get { return 1359; } }
        public override int BaseFireResistance { get { return 6; } }

        public override int InitMinHits { get { return 100; } }
        public override int InitMaxHits { get { return 150; } }

        public override int AosStrReq { get { return 80; } }
        public override int OldStrReq { get { return 40; } }

        public override int OldDexBonus { get { return -2; } }

        public override int ArmorBase { get { return 40; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }
        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

        public override bool AllowMaleWearer { get { return false; } }

        [Constructable]
        public FGargishFireRockArms()
            : base(0x0307)
        {
            Name = "F Gargish FireRock Arms";
            Hue = 1359;
            Weight = 5.0;

            Attributes.BonusInt = Utility.RandomMinMax(3, 5);
            Attributes.LowerRegCost = Utility.RandomMinMax(4, 15);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(5, 8);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);
            ArmorAttributes.DurabilityBonus = 12;

            EnergyBonus = Utility.RandomMinMax(5, 9);
            FireBonus = Utility.RandomMinMax(10, 25);
            PoisonBonus = Utility.RandomMinMax(3, 8);
            PhysicalBonus = Utility.RandomMinMax(2, 7);

            LootType = LootType.Regular;
        }


        public FGargishFireRockArms(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (Weight == 1.0)
                Weight = 5.0;
        }
    }
}