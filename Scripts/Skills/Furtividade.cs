using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Network;

namespace Server.SkillHandlers
{
    public class Furtividade
    {
        private static bool m_CombatOverride;
        public static bool CombatOverride
        {
            get
            {
                return m_CombatOverride;
            }
            set
            {
                m_CombatOverride = value;
            }
        }
        private static readonly int[,] m_ArmorTable = new int[,]
        {
            //	Gorget	Gloves	Helmet	Arms	Legs	Chest	Shield
            /* Cloth	*/ { 0, 0, 0, 0, 0, 0, 0 },
            /* Leather	*/ { 0, 0, 0, 0, 0, 0, 0 },
            /* Studded	*/ { 2, 2, 0, 4, 6, 10, 0 },
            /* Bone		*/ { 0, 5, 10, 10, 15, 25, 0 },
            /* Spined	*/ { 0, 0, 0, 0, 0, 0, 0 },
            /* Horned	*/ { 0, 0, 0, 0, 0, 0, 0 },
            /* Barbed	*/ { 0, 0, 0, 0, 0, 0, 0 },
            /* Ring		*/ { 0, 5, 0, 10, 15, 25, 0 },
            /* Chain	*/ { 0, 0, 10, 0, 15, 25, 0 },
            /* Plate	*/ { 5, 5, 10, 10, 15, 25, 0 },
            /* Dragon	*/ { 0, 5, 10, 10, 15, 25, 0 }
        };
        public static double HidingRequirement
        {
            get
            {
                return (Core.ML ? 30.0 : (Core.SE ? 50.0 : 80.0));
            }
        }

        public static int[,] ArmorTable
        {
            get
            {
                return m_ArmorTable;
            }
        }

        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.Furtividade].Callback = new SkillUseCallback(OnUse);
        }

        public static int GetArmorRating(Mobile m)
        {
            if (!Core.AOS)
                return (int)m.ArmorRating;

            int ar = 0;

            for (int i = 0; i < m.Items.Count; i++)
            {
                BaseArmor armor = m.Items[i] as BaseArmor;

                if (armor == null)
                    continue;

                int materialType = (int)armor.MaterialType;
                int bodyPosition = (int)armor.BodyPosition;

                if (materialType >= m_ArmorTable.GetLength(0) || bodyPosition >= m_ArmorTable.GetLength(1))
                    continue;

                if (armor.ArmorAttributes.MageArmor == 0)
                    ar += m_ArmorTable[materialType, bodyPosition];
            }

            return ar;
        }

        public static TimeSpan OnUse(Mobile m)
        {
            if (m.Flying)
            {
                m.SendLocalizedMessage(1113415); // You cannot use this ability while flying.
                m.RevealingAction();
                BuffInfo.RemoveBuff(m, BuffIcon.HidingAndOrStealth);
                return TimeSpan.FromSeconds(1.0);
            }
            /* //Removido para não verificar um nivel minimo de skill pra andar furtivo
            else if (m.Skills[SkillName.Furtividade].Base < HidingRequirement)
            {
                m.SendLocalizedMessage(502726); // You are not hidden well enough.  Become better at hiding.
                m.RevealingAction();
                BuffInfo.RemoveBuff(m, BuffIcon.HidingAndOrStealth);
            }
            */
            else if (!m.CanBeginAction(typeof(Furtividade)))
            {
                m.SendLocalizedMessage(1063086); // You cannot use this skill right now.
                m.RevealingAction();
                BuffInfo.RemoveBuff(m, BuffIcon.HidingAndOrStealth);
                return TimeSpan.FromSeconds(1.0);
            }
            if (m.Spell != null)
            {
                m.SendLocalizedMessage(501238); // You are busy doing something else and cannot hide.
                return TimeSpan.FromSeconds(1.0);
            }

            if (Server.Engines.VvV.ManaSpike.UnderEffects(m))
            {
                return TimeSpan.FromSeconds(1.0);
            }

            if (Core.ML && m.Target != null)
            {
                Targeting.Target.Cancel(m);
            }
            /*
            double bonus = 0.0;

            BaseHouse house = BaseHouse.FindHouseAt(m);

            if (house != null && house.IsFriend(m))
            {
                bonus = 100.0;
            }
            else if (!Core.AOS)
            {
                if (house == null)
                    house = BaseHouse.FindHouseAt(new Point3D(m.X - 1, m.Y, 127), m.Map, 16);

                if (house == null)
                    house = BaseHouse.FindHouseAt(new Point3D(m.X + 1, m.Y, 127), m.Map, 16);

                if (house == null)
                    house = BaseHouse.FindHouseAt(new Point3D(m.X, m.Y - 1, 127), m.Map, 16);

                if (house == null)
                    house = BaseHouse.FindHouseAt(new Point3D(m.X, m.Y + 1, 127), m.Map, 16);

                if (house != null)
                    bonus = 50.0;
            }
            */
            //int range = 18 - (int)(m.Skills[SkillName.Furtividade].Value / 10);
            int skill = Math.Min(150, (int)m.Skills[SkillName.Furtividade].Value);
            int range = Math.Min((int)((100 - skill) / 10) + 8, 18); //de 18 com 0 (zero) de skill até 3 com 150 de skill

            //Verifica se o atacante está no alcance
            bool badCombat = (!m_CombatOverride && m.Combatant is Mobile && m.InRange(m.Combatant.Location, range) && ((Mobile)m.Combatant).InLOS(m.Combatant));

            bool ok = (!badCombat /*&& m.CheckSkill( SkillName.Furtividade, 0.0 - bonus, 100.0 - bonus )*/);
            int armorRating = GetArmorRating(m);

            if (ok)
            {
                if (!m_CombatOverride)
                {
                    IPooledEnumerable eable = m.GetMobilesInRange(range);

                    foreach (Mobile check in eable)
                    {
                        if (check.InLOS(m) && check.Combatant == m)
                        {
                            badCombat = true;
                            ok = false;
                            break;
                        }
                    }

                    eable.Free();
                }

                ok = (!badCombat && m.CheckSkill(SkillName.Furtividade, 25.0 + (armorRating * 2), 50.0 + (armorRating * 2))); //Começa a ter chance de andar furtivo com 25 de skill
            }

            if (badCombat)
            {
                m.RevealingAction();

                m.LocalOverheadMessage(MessageType.Regular, 0x22, 501237); // You can't seem to hide right now.

                return TimeSpan.Zero;
                //return TimeSpan.FromSeconds(1.0);
            }
            else
            {
                if (ok)  //Se esconde e fica furtivo
                {
                    m.Hidden = true;
                    m.Warmode = false;
                    Server.Spells.Sixth.InvisibilitySpell.RemoveTimer(m);
                    Server.Items.InvisibilityPotion.RemoveTimer(m);
                    m.LocalOverheadMessage(MessageType.Regular, 0x1F4, 501240); // You have hidden yourself well.

                    int steps = 1 + (int) (m.Skills[SkillName.Furtividade].Value/5.0);

                    m.AllowedStealthSteps = steps;

                    m.IsStealthing = true;

                    m.SendLocalizedMessage(502730); // You begin to move quietly.

                    BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.HidingAndOrStealth, 1044107, 1075655));

                    if(m is PlayerMobile)
                    {
                        PlayerMobile player = (PlayerMobile)m;

                        //Timer.DelayCall(TimeSpan.FromSeconds(3), () =>
                        //{
                            player.SendMessage("Verificando se alguem te enxerga, logo depois de se esconder");
                            player.m_FurtivoTimer.Start();
                        //});
                    }
                    
                }
                else
                {
                    m.RevealingAction();
                    m.LocalOverheadMessage(MessageType.Regular, 0x22, 501241); // You can't seem to hide here.
                }
                return TimeSpan.FromSeconds(10.0);
            }
        }
    }
}
