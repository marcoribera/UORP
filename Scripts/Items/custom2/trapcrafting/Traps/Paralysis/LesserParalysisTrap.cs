//
// ** Basic Trap Framework (BTF)
// ** Author: Lichbane
//
using System;
using Server.Mobiles;

namespace Server.Items
{
    public class ParalysisLesserTrap : BaseTinkerTrap
    {
        private Boolean m_TrapArmed = false;
        private DateTime m_TimeTrapArmed;

        private static string m_ArmedName = "uma armadilha de paralisia menor armada";
        private static string m_UnarmedName = "uma armadilha de paralisia menor desarmada";
        private static double m_ExpiresIn = 999999.0;
        private static int m_ArmingSkill = 25;
        private static int m_DisarmingSkill = 60;
        private static int m_KarmaLoss = 0;
        private static bool m_AllowedInTown = true;

        [Constructable]
        public ParalysisLesserTrap()
            : base(m_ArmedName, m_UnarmedName, m_ExpiresIn, m_ArmingSkill, m_DisarmingSkill, m_KarmaLoss, m_AllowedInTown)
        {
        }

        public ParalysisLesserTrap(Serial serial) : base(serial)
        {
        }

        public override void TrapEffect(Mobile from)
        {
            from.PlaySound(0x4A);  // click sound

            from.PlaySound(0x204);
            from.FixedEffect(0x376A, 6, 1);

            int duration     = Utility.RandomMinMax(2, 4);
            from.Paralyze(TimeSpan.FromSeconds(duration));

            bool m_TrapsLimit = Trapcrafting.Config.TrapsLimit;
            if ((m_TrapsLimit) && (((PlayerMobile)this.Owner).TrapsActive > 0))
                ((PlayerMobile)this.Owner).TrapsActive -= 1;

            this.Delete();
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
