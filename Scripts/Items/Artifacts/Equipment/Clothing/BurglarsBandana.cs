using System;

namespace Server.Items
{
    public class BurglarsBandana : Bandana
	{
		public override bool IsArtifact { get { return true; } }
        [Constructable]
        public BurglarsBandana()
        {
            Hue = Utility.RandomBool() ? 0x58C : 0x10;
            SkillBonuses.SetValues(0, SkillName.Prestidigitacao, 10.0);
            SkillBonuses.SetValues(1, SkillName.Furtividade, 10.0);
            SkillBonuses.SetValues(2, SkillName.Prestidigitacao, 10.0);
            Attributes.BonusDex = 5;
        }

        public BurglarsBandana(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber
        {
            get
            {
                return 1063473;
            }
        }
        public override int BasePhysicalResistance
        {
            get
            {
                return 10;
            }
        }
        public override int BaseFireResistance
        {
            get
            {
                return 5;
            }
        }
        public override int BaseColdResistance
        {
            get
            {
                return 7;
            }
        }
        public override int BasePoisonResistance
        {
            get
            {
                return 10;
            }
        }
        public override int BaseEnergyResistance
        {
            get
            {
                return 10;
            }
        }
        public override int InitMinHits
        {
            get
            {
                return 255;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 255;
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version < 2)
            {
                this.Resistances.Physical = 0;
                this.Resistances.Fire = 0;
                this.Resistances.Cold = 0;
                this.Resistances.Poison = 0;
                this.Resistances.Energy = 0;
            }
        }
    }
}