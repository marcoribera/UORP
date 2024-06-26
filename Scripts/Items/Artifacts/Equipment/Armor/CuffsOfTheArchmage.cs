using System;

namespace Server.Items
{
    public class CuffsOfTheArchmage : BoneArms
	{
        public override int LabelNumber { get { return 1157348; } } // cuffs of the archmage
        public override bool IsArtifact { get { return true; } }

        [Constructable]
        public CuffsOfTheArchmage()
        {
            SkillBonuses.SetValues(0, SkillName.ResistenciaMagica, 15.0);
            Attributes.BonusMana = 5;
            Attributes.RegenMana = 4;
            Attributes.SpellDamage = 20;
            ArmorAttributes.MageArmor = 1;
        }

        public CuffsOfTheArchmage(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance { get { return 15; } }
        public override int BaseFireResistance { get { return 15; } }
        public override int BaseColdResistance { get { return 15; } }
        public override int BasePoisonResistance { get { return 15; } }
        public override int BaseEnergyResistance { get { return 15; } }
        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class GargishCuffsOfTheArchmage : GargishStoneArms
    {
        public override int LabelNumber { get { return 1157348; } } // cuffs of the archmage
        public override bool IsArtifact { get { return true; } }

        [Constructable]
        public GargishCuffsOfTheArchmage()
        {
            SkillBonuses.SetValues(0, SkillName.ResistenciaMagica, 15.0);
            Attributes.BonusMana = 5;
            Attributes.RegenMana = 4;
            Attributes.SpellDamage = 20;
            ArmorAttributes.MageArmor = 1;
        }

        public GargishCuffsOfTheArchmage(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance { get { return 15; } }
        public override int BaseFireResistance { get { return 15; } }
        public override int BaseColdResistance { get { return 15; } }
        public override int BasePoisonResistance { get { return 15; } }
        public override int BaseEnergyResistance { get { return 15; } }
        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
