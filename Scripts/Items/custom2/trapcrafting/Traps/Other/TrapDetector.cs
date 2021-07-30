//
// ** Basic Trap Framework (BTF)
// ** Author: Lichbane
//
using System;
using Server.Regions;

namespace Server.Items
{
  	public class TrapDetector : Item
    {
        private Mobile m_Player;

        [Constructable]
        public TrapDetector() : base(0x1B73)
        {
            Weight = 7.0;
            Name = "Um detector portatil de armadilhas";

        }

        public override void OnDoubleClick(Mobile from)
		{
            m_Player = from;
            if (IsChildOf(from.Backpack))
                from.SendMessage("O detector de armadilhas deve ser colocado no solo para funcionar.");

            else if (!from.InRange(this.GetWorldLocation(), 2))
                from.SendMessage("Você ativa o detector de armadilhas a distancia.");

            else
            {
                from.SendMessage("Seu detector de armadilhas começa a sonar.");
                from.PlaySound(0x2F3); // Earthquake Sound

                bool trapfound = false;
                Point3D location = ((Item)this).Location;
                double Skill = m_Player.Skills[SkillName.Percepcao].Value;
                int range = (int)(Skill / 10.0);

                IPooledEnumerable itemsInRange = m_Player.Map.GetItemsInRange(location, range);
                foreach (Item item in itemsInRange)
                {
                    if (item is BaseTinkerTrap)
                    {
                        BaseTinkerTrap trap = (BaseTinkerTrap)item;
                        double detectMin = trap.DisarmingSkillReq - 10;
                        double detectMax = trap.DisarmingSkillReq + 10;
                        if ((m_Player.CheckTargetSkill(SkillName.Percepcao, trap, detectMin, detectMax)) || (trap.Owner == from))
                        {
                            trap.Visible = true;
                            trapfound = true;
                        }
                    }
                }
                itemsInRange.Free();

                //
                // Report back!
                //
                if (trapfound)
                    m_Player.SendMessage(0x59, "O detector de armadilhas parece ter encontrado algo.");
                else
                    m_Player.SendMessage(0x59, "O detector de armadilhas não encontrou nada.");

                //
                // 10% of the device burning out (rather than charges)
                //
                if (0.1 > Utility.RandomDouble())
                {
                    m_Player.PlaySound(0x5C);
                    m_Player.SendMessage(0x59, "Parece que o detector de armadilhas queimou sozinho.");
                    this.Delete();
                }
            }
        }

        public TrapDetector(Serial serial) : base(serial)
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
