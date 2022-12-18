using System;
using System.Collections.Generic;

namespace Server.Spells
{
    public class SpellRegistry
    {
        private static readonly Type[] m_Types = new Type[899];
        private static int m_Count;

        public static Type[] Types
        {
            get
            {
                m_Count = -1;
                return m_Types;
            }
        }
		
        //What IS this used for anyways.
        public static int Count
        {
            get
            {
                if (m_Count == -1)
                {
                    m_Count = 0;

                    for (int i = 0; i < m_Types.Length; ++i)
                        if (m_Types[i] != null)
                            ++m_Count;
                }

                return m_Count;
            }
        }

        private static readonly Dictionary<Type, Int32> m_IDsFromTypes = new Dictionary<Type, Int32>(m_Types.Length);
		
        private static readonly Dictionary<Int32, SpecialMove> m_SpecialMoves = new Dictionary<Int32, SpecialMove>();
        public static Dictionary<Int32, SpecialMove> SpecialMoves
        {
            get
            {
                return m_SpecialMoves;
            }
        }

        public static int GetRegistryNumber(ISpell s)
        {
            return GetRegistryNumber(s.GetType());
        }

        public static int GetRegistryNumber(SpecialMove s)
        {
            return GetRegistryNumber(s.GetType());
        }

        public static int GetRegistryNumber(Type type)
        {
            if (m_IDsFromTypes.ContainsKey(type))
                return m_IDsFromTypes[type];

            return -1;
        }

        public static void Register(int spellID, Type type)
        {
            if (spellID < 0 || spellID >= m_Types.Length)
                return;

            if (m_Types[spellID] == null)
                ++m_Count;

            m_Types[spellID] = type;

            if (!m_IDsFromTypes.ContainsKey(type))
                m_IDsFromTypes.Add(type, spellID);

            if (type.IsSubclassOf(typeof(SpecialMove)))
            {
                SpecialMove spm = null;

                try
                {
                    spm = Activator.CreateInstance(type) as SpecialMove;
                }
                catch
                {
                }

                if (spm != null)
                    m_SpecialMoves.Add(spellID, spm);
            }
        }

        public static SpecialMove GetSpecialMove(int spellID)
        {
            if (spellID < 0 || spellID >= m_Types.Length)
                return null;

            Type t = m_Types[spellID];

            if (t == null || !t.IsSubclassOf(typeof(SpecialMove)) || !m_SpecialMoves.ContainsKey(spellID))
                return null;

            return m_SpecialMoves[spellID];
        }

        private static readonly object[] m_Params = new object[2];

        public static Spell NewSpell(int spellID, Mobile caster, Item scroll)
        {
            if (spellID < 0 || spellID >= m_Types.Length)
                return null;

            Type t = m_Types[spellID];

            if (t != null && !t.IsSubclassOf(typeof(SpecialMove)))
            {
                m_Params[0] = caster;
                m_Params[1] = scroll;

                try
                {
                    return (Spell)Activator.CreateInstance(t, m_Params);
                }
                catch
                {
                }
            }

            return null;
        }

        private static readonly string[] m_CircleNames = new string[]
        {
            "Fist",
            "Second",
            "Third",
            "Forth",
            "Fith",
            "Sexth",
            "Seventh",
            "Eighth",
            "Necromancy",
            "Chivalry",
            "Bushido",
            "CosmosSolar", // Skill Ordem // magias que usam habilidades de luta tipo kitah
            "CosmosLunar", // Skill Ordem // magias que usam habilidades de luta tipo kitah

            "Paladino",
            "Ninjitsu",
            "Spellweaving",
            #region Stygian Abyss
            "Mystic",
            #endregion
            #region TOL
            "SkillMasteries",
            #endregion
            //Tipos novos de Magias:
            "Xama", //Skill Feiticaria //Facetas dos 4 elementos, seus espiritos e elementais - Não usa reagentes, mas tem maior custo de mana
            "Bruxo", //Skill Feiticaria //Maldições e invocações demonios
            "Paladino", //Skill Ordem //Magias de cura menores, buffs fortes de combate para si, em especial contra criaturas malignas
            "Algoz", //Skill Ordem //Magias de dreno menores, buffs fortes de combate para si, em especial contra criaturas benignas
            "ClerigoDaVida", //Skill Necromancia //Os mais diversos tipos de magia de cura, buffs de proteção, invocações celestiais, criação de alimentos e purificação
            "ClerigoDosMortos", //Skill Necromancia //Os mais diversos tipos de magia de dreno, debuffs debilitantes, invocações mortos vivos e envenenamentos
            "Mago", //Skill Arcanismo //Ataques mágicos diretos, magias de deslocamento, diversas magias utilitárias como telecinese, arrombar, etc
            "Metamorfo", //Skill Arcanismo //Os mais diversos tipo de Metamorfose e buffs para si
            "Druida", //Skill Misticismo //Magias de plantas, baseadas em clima e metamorfose em animais
            "Monge", //Skill Misticismo //Magias de projeção astral, curas para si (vida e veneno), buffs de combate para si
            "Bardo", //Skill Caos //buffs e debuffs, invocações festivas
            "Trapaceiro" //Skill Ninjitsu //Distração, buffs para si para as habilidades ladinas ou distrações (Debuffs para percepção dos outros)

        };

        public static Spell NewSpell(string name, Mobile caster, Item scroll)
        {
            for (int i = 0; i < m_CircleNames.Length; ++i)
            {
                Type t = ScriptCompiler.FindTypeByFullName(String.Format("Server.Spells.{0}.{1}", m_CircleNames[i], name));

                if (t != null && !t.IsSubclassOf(typeof(SpecialMove)))
                {
                    m_Params[0] = caster;
                    m_Params[1] = scroll;

                    try
                    {
                        return (Spell)Activator.CreateInstance(t, m_Params);
                    }
                    catch
                    {
                    }
                }
            }

            return null;
        }
    }
}
