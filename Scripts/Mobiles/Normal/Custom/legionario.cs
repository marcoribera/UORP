using System;
using Server.Items;

namespace Server.Mobiles
{
    public class Legionario : BaseEscortable
    {
        [Constructable]
        public Legionario()
        {
            this.Title = "Legionário";

            this.SetSkill(SkillName.Bloqueio, 20.0, 40.0);
            this.SetSkill(SkillName.Perfurante, 20.0, 40.0);
            this.SetSkill(SkillName.Anatomia, 20.0, 40.0);

            Persuadable = true;
            ControlSlots = 1;
            MinPersuadeSkill = 40;

        }

        public Legionario(Serial serial)
            : base(serial)
        {
        }

        public override bool CanTeach
        {
            get
            {
                return true;
            }
        }
        public override bool ClickTitle
        {
            get
            {
                return false;
            }
        }// Do not display 'the noble' when single-clicking
        public override void InitOutfit()
        {
            this.AddItem(new DragonTurtleHideHelm());
            this.AddItem(new HeaterShield());
            this.AddItem(new Leafblade());
            this.AddItem(new Tunic(Utility.RandomRedHue()));
       
            int lowHue = GetRandomHue();

            // this.AddItem(new ShortPants(lowHue));

            if (this.Female)
                this.AddItem(new Sandals(lowHue));
            else
                this.AddItem(new Sandals(lowHue));

            // if (!this.Female)
            //     this.AddItem(new BodySash(lowHue));

            // this.AddItem(new Cloak(GetRandomHue()));

            // if (!this.Female)
            //     this.AddItem(new Longsword());

            Utility.AssignRandomHair(this);

            this.PackGold(5, 25);
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

        private static int GetRandomHue()
        {
            switch ( Utility.Random(6) )
            {
                default:
                case 0:
                    return 0;
                case 1:
                    return Utility.RandomBlueHue();
                case 2:
                    return Utility.RandomGreenHue();
                case 3:
                    return Utility.RandomRedHue();
                case 4:
                    return Utility.RandomYellowHue();
                case 5:
                    return Utility.RandomNeutralHue();
            }
        }
    }
}