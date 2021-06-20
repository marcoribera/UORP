using System;

namespace Server.Mobiles
{
    public class EvilHealer : BaseHealer
    {
        [Constructable]
        public EvilHealer()
        {
            this.Title = "the healer";

            this.Karma = -10000;

            this.SetSkill(SkillName.Percepcao, 80.0, 100.0);
            this.SetSkill(SkillName.PoderMagico, 80.0, 100.0);
            this.SetSkill(SkillName.Cortante, 80.0, 100.0);

            Persuadable = true;
            ControlSlots = 3;
            MinPersuadeSkill = 100;


        }

        public EvilHealer(Serial serial)
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
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }
        public override bool IsActiveVendor
        {
            get
            {
                return true;
            }
        }
        public override bool CheckTeach(SkillName skill, Mobile from)
        {
            if (!base.CheckTeach(skill, from))
                return false;

            return (skill == SkillName.Percepcao) ||
                   (skill == SkillName.Medicina) ||
                   (skill == SkillName.PoderMagico) ||
                   (skill == SkillName.Cortante);
        }

        public override void InitSBInfo()
        {
            this.SBInfos.Add(new SBHealer());
        }

        public override bool CheckResurrect(Mobile m)
        {
            if (Core.AOS && m.Criminal)
            {
                this.Say(501222); // Thou art a criminal.  I shall not resurrect thee.
                return false;
            }

            return true;
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