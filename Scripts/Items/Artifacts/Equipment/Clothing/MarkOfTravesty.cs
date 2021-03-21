using System;

namespace Server.Items
{
    public class MarkOfTravesty : SavageMask
	{
		public override bool IsArtifact { get { return true; } }
        [Constructable]
        public MarkOfTravesty()
            : base()
        {
            Hue = 0x495;		
            Attributes.BonusMana = 8;
            Attributes.RegenHits = 3;		
            ClothingAttributes.SelfRepair = 3;
			
            switch( Utility.Random(15) )
            {
                case 0: 
                    SkillBonuses.SetValues(0, SkillName.PoderMagico, 10);
                    SkillBonuses.SetValues(1, SkillName.Arcanismo, 10);
                    break;
                case 1: 
                    SkillBonuses.SetValues(0, SkillName.Adestramento, 10);
                    SkillBonuses.SetValues(1, SkillName.Adestramento, 10);
                    break;
                case 2: 
                    SkillBonuses.SetValues(0, SkillName.Cortante, 10);
                    SkillBonuses.SetValues(1, SkillName.Anatomia, 10);
                    break;
                case 3: 
                    SkillBonuses.SetValues(0, SkillName.Caos, 10);
                    SkillBonuses.SetValues(1, SkillName.Tocar, 10);
                    break;
                case 4: 
                    SkillBonuses.SetValues(0, SkillName.Perfurante, 10);
                    SkillBonuses.SetValues(1, SkillName.Anatomia, 10);
                    break;
                case 5: 
                    SkillBonuses.SetValues(0, SkillName.Ordem, 10);
                    SkillBonuses.SetValues(1, SkillName.ResistenciaMagica, 10);
                    break;
                case 6: 
                    SkillBonuses.SetValues(0, SkillName.Anatomia, 10);
                    SkillBonuses.SetValues(1, SkillName.Medicina, 10);
                    break;
                case 7: 
                    SkillBonuses.SetValues(0, SkillName.Ninjitsu, 10);
                    SkillBonuses.SetValues(1, SkillName.Furtividade, 10);
                    break;
                case 8: 
                    SkillBonuses.SetValues(0, SkillName.Bushido, 10);
                    SkillBonuses.SetValues(1, SkillName.Bloqueio, 10);
                    break;
                case 9: 
                    SkillBonuses.SetValues(0, SkillName.Atirar, 10);
                    SkillBonuses.SetValues(1, SkillName.Anatomia, 10);
                    break;
                case 10: 
                    SkillBonuses.SetValues(0, SkillName.Contusivo, 10);
                    SkillBonuses.SetValues(1, SkillName.Anatomia, 10);
                    break;
                case 11: 
                    SkillBonuses.SetValues(0, SkillName.Necromancia, 10);
                    SkillBonuses.SetValues(1, SkillName.PoderMagico, 10);
                    break;
                case 12: 
                    SkillBonuses.SetValues(0, SkillName.Furtividade, 10);
                    SkillBonuses.SetValues(1, SkillName.Prestidigitacao, 10);
                    break;
                case 13: 
                    SkillBonuses.SetValues(0, SkillName.Pacificar, 10);
                    SkillBonuses.SetValues(1, SkillName.Tocar, 10);
                    break;
                case 14:
                    SkillBonuses.SetValues(0, SkillName.Provocacao, 10);
                    SkillBonuses.SetValues(1, SkillName.Tocar, 10);
                    break;
            }
        }

        public MarkOfTravesty(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber
        {
            get
            {
                return 1074493;
            }
        }// Mark of Travesty
        public override int BasePhysicalResistance
        {
            get
            {
                return 8;
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
                return 11;
            }
        }
        public override int BasePoisonResistance
        {
            get
            {
                return 20;
            }
        }
        public override int BaseEnergyResistance
        {
            get
            {
                return 15;
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
			
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
			
            int version = reader.ReadInt();
        }
    }
}