using System;
using Server.Targeting;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Bardo
{
    public class SomDaFraquezaSpell : BardoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Som da Fraqueza", "Sua mochila ficou pesada, meu amigo?",
            212,
            9031);

        public static Dictionary<Mobile, Timer> m_Table = new Dictionary<Mobile, Timer>();

        public static bool IsUnderEffects(Mobile m)
        {
            return m_Table.ContainsKey(m);
        }
        public override int EficienciaMagica(Mobile caster) { return 5; } //Servirá para calcular o modificador na eficiência das magias
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
            }
        }

        public override bool CheckCast()
        {
            // Check for a musical instrument in the player's backpack
            if (!CheckInstrument())
            {
                Caster.SendMessage("Você precisa ter um instrumento musical na sua mochila para canalizar essa magia.");
                return false;
            }


            return base.CheckCast();
        }


        private bool CheckInstrument()
        {
            return Caster.Backpack.FindItemByType(typeof(BaseInstrument)) != null;
        }


        private BaseInstrument GetInstrument()
        {
            return Caster.Backpack.FindItemByType(typeof(BaseInstrument)) as BaseInstrument;
        }
        public override double RequiredSkill
        {
            get
            {
                return 10.0;
            }
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

                BuffInfo.RemoveBuff(m, BuffIcon.Weaken);

                if (removeMod)
                    m.RemoveStatMod("[Magic] Str Curse");

                m_Table.Remove(m);
            }
        }

        public SomDaFraquezaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }


        public override void OnCast()
        {
            if (this.CheckSequence())
            {
                List<Mobile> targets = new List<Mobile>();

                Map map = this.Caster.Map;

                if (map != null)
                {
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(Caster.Location), 10);

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
                                if ((Caster.Party == null) && (criatura.GetMaster() != null)) //se a criatura não estiver em party com o conjurador, e seu mestre não for o caster
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

                    if (CheckHSequence(m))

                        SpellHelper.Turn(Caster, m);
                    SpellHelper.CheckReflect((int)Circle, Caster, ref m);

                    if (Mysticism.StoneFormSpell.CheckImmunity(m))
                    {
                        Caster.SendLocalizedMessage(1080192); // Your target resists your ability reduction magic.
                        return;
                    }

                    oldOffset = SpellHelper.GetCurseOffset(m, StatType.Str);
                    newOffset = SpellHelper.GetOffset(this, Caster, m, StatType.Str, true, false);

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
                            SpellHelper.AddStatCurse(this, this.Caster, m, StatType.Str, false, newOffset);

                            int percentage = (int)(SpellHelper.GetOffsetScalar(this, this.Caster, m, true) * 100);
                            TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                            BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Weaken, 1075837, length, m, percentage.ToString()));

                            if (m_Table.ContainsKey(m))
                                m_Table[m].Stop();

                            m_Table[m] = Timer.DelayCall(length, () =>
                            {
                                RemoveEffects(m);
                            });
                        }
                    }
                }

                this.FinishSequence();

            }
        }
    }
}
