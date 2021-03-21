using Server;
using System;

namespace Server.Items
{
    public class FemaleKimonoBearingTheCrestOfBlackthorn3 : FemaleKimono
    {
        public override bool IsArtifact { get { return true; } }

        [Constructable]
        public FemaleKimonoBearingTheCrestOfBlackthorn3()
            : base()
        {
            ReforgedSuffix = ReforgedSuffix.Blackthorn;
            SkillBonuses.SetValues(0, SkillName.Furtividade, 10.0);
            Hue = 2130;
        }

        public FemaleKimonoBearingTheCrestOfBlackthorn3(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
			
			if (version == 0)
            {
                MaxHitPoints = 0;
                HitPoints = 0;
            }
        }
    }
}