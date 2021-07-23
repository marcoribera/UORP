//
// ** Basic Trap Framework (BTF)
// ** Author: Lichbane
//
using System;
using Server.Mobiles;

namespace Server.Items
{
    public class PoisonDeadlyDartTrap : BaseTinkerTrap
    {
        private Boolean m_TrapArmed = false;
        private DateTime m_TimeTrapArmed;

        private static string m_ArmedName = "Uma armadilha de dardos envenanada mortalmente armada";
        private static string m_UnarmedName = "Uma armadilha de dardos envenanada mortalmente desarmada";
        private static double m_ExpiresIn = 999999.0;
        private static int m_ArmingSkill = 55;
        private static int m_DisarmingSkill = 75;
        private static int m_KarmaLoss = 120;
        private static bool m_AllowedInTown = true;

        [Constructable]
        public PoisonDeadlyDartTrap()
            : base(m_ArmedName, m_UnarmedName, m_ExpiresIn, m_ArmingSkill, m_DisarmingSkill, m_KarmaLoss, m_AllowedInTown)
        {
        }

        public override void TrapEffect(Mobile from)
        {
            from.PlaySound(0x4A);  // click sound

            if (from.Alive == true)
            {
                double penetration = Utility.RandomMinMax(20, 200);
                if (from.ArmorRating > penetration)
                {
                    from.SendMessage("Um dardo gruda na sua protecao e cai sem causar danos");
                }
                else
                {
                    from.ApplyPoison(from, Poison.Deadly);
                    from.SendMessage("Você sente uma fisgada de um dardo envenenado no seu corpo");
                }
            }
            bool m_TrapsLimit = Trapcrafting.Config.TrapsLimit;
            if ((m_TrapsLimit) && (((PlayerMobile)this.Owner).TrapsActive > 0))
                ((PlayerMobile)this.Owner).TrapsActive -= 1;

            this.Delete();
        }

        public PoisonDeadlyDartTrap(Serial serial) : base(serial)
        {
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
