using System;
using Server.Targeting;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Spells.Algoz
{
    public class EmbasbacarSpell : AlgozSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Embasbacar", "Intelis Cort",
            212,
            9031,
            Reagent.Ginseng,
            Reagent.Nightshade);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias
        public EmbasbacarSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public static Dictionary<Mobile, Timer> m_Table = new Dictionary<Mobile, Timer>();

        public static bool IsUnderEffects(Mobile m)
        {
            return m_Table.ContainsKey(m);
        }

        public static void RemoveEffects(Mobile m, bool removeMod = true)
        {
            if (m_Table.ContainsKey(m))
            {
                Timer t = m_Table[m];

                if (t != null && t.Running)
                {
                    t.Stop();
                }

                BuffInfo.RemoveBuff(m, BuffIcon.FeebleMind);

                if (removeMod)
                    m.RemoveStatMod("[Magic] Int Curse");

                m_Table.Remove(m);
            }
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
            }
        }
        public override void OnCast()
        {
            if (this.CheckSequence())
            {
                List<Mobile> targets = new List<Mobile>();

                Map map = this.Caster.Map;

                if (map != null)
                {
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(Caster.Location), 4);

                    foreach (Mobile m in eable)
                    {
                        if (m is PlayerMobile) //Se o char alvo é de player
                        {
                            if (m != Caster) //mas não é o conjurador
                            {
                                bool valido = true;
                                if ((Caster.Party != null) && (m.Party == Caster.Party)) // verifica se o conjurador tá numa Party e se ela é a mesma do alvo
                                {
                                    valido = false; //Se forem da mesma Party é um alvo inválido
                                }
                                if ((Caster.Guild != null) && (m.Guild == Caster.Guild)) // verifica se o conjurador tá numa Guild e se ela é a mesma do alvo
                                {
                                    valido = false; //Se forem da mesma Guild é um alvo inválido
                                }
                                if (valido) //se for um alvo válido adiciona na lista
                                {
                                    targets.Add(m as Mobile); //é um alvo válido.
                                }
                            }
                        }
                        if (m is BaseCreature)
                        {
                            BaseCreature criatura = m as BaseCreature;
                            bool valido = true;
                            if (criatura.GetMaster() != null)
                            {
                                if (criatura.GetMaster() == Caster)
                                {
                                    valido = false;
                                }
                                if ((Caster.Party != null) && (criatura.GetMaster().Party == Caster.Party))
                                {
                                    valido = false;
                                }
                                if ((Caster.Guild != null) && (criatura.GetMaster().Guild == Caster.Guild))
                                {
                                    valido = false;
                                }
                            }
                            if (valido) //se for um alvo válido adiciona na lista
                            {
                                targets.Add(m as Mobile); //é um alvo válido.
                            }
                        }
                    }

                    eable.Free();
                }

                int oldOffset;
                int newOffset;

                for (int i = 0; i < targets.Count; ++i)
                {
                    Mobile m = targets[i];

                    oldOffset = SpellHelper.GetCurseOffset(m, StatType.Int);
                    newOffset = SpellHelper.GetOffset(this, Caster, m, StatType.Int, true, false);


                    if (m.Spell != null)
                        m.Spell.OnCasterHurt();

                    m.Paralyzed = false;

                    m.FixedParticles(0x3779, 10, 15, 5011, SpellEffectHue, 3, EffectLayer.Head);
                    m.PlaySound(0x1DF);

                    HarmfulSpell(m);

                    if (!(-newOffset > oldOffset || newOffset == 0))
                    {
                        if (-newOffset < oldOffset)
                        {
                            SpellHelper.AddStatCurse(this, this.Caster, m, StatType.Int, false, newOffset);

                            int percentage = (int)(SpellHelper.GetOffsetScalar(this, this.Caster, m, true) * 100);
                            TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                            BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.FeebleMind, 1075833, length, m, percentage.ToString())); //TODO: Criar Cliloc com as descrição do debuff

                            if (m_Table.ContainsKey(m))
                                m_Table[m].Stop();

                            m_Table[m] = Timer.DelayCall(length, () =>
                            {
                                RemoveEffects(m);
                            });
                        }
                    }
                }
            }

            this.FinishSequence();
        }
    }
}
