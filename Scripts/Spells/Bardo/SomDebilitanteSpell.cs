/*using System;
using System.Collections.Generic;
using System.Linq;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Spells.Fourth;
using Server.Spells.First;*/

using System;
using System.Collections.Generic;
using System.Linq;
using Server.Items;
using Server.Targeting;
using Server.Spells.Fourth;
using Server.Spells.First;
using Server.Mobiles;


namespace Server.Spells.Bardo
{
    public class SomDebilitanteSpell : BardoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Som Deblitante", "Parem de ser um bando de molengas",
            218,
            9031,
            false,
            Reagent.Garlic,
            Reagent.Nightshade,
            Reagent.MandrakeRoot,
            Reagent.SulfurousAsh); // o que é false?

        private static readonly Dictionary<Mobile, Timer> m_UnderEffect = new Dictionary<Mobile, Timer>();
        public SomDebilitanteSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Seventh;
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
                return 70.0;
            }
        }
        public override int EficienciaMagica(Mobile caster) { return 5; } //Servirá para calcular o modificador na eficiência das magias

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

                for (int i = 0; i < targets.Count; ++i)
                {
                    Mobile m = targets[i];

                    

                    int oldStr = SpellHelper.GetCurseOffset(m, StatType.Str);
                    int oldDex = SpellHelper.GetCurseOffset(m, StatType.Dex);
                    int oldInt = SpellHelper.GetCurseOffset(m, StatType.Int);

                    int newStr = SpellHelper.GetOffset(this, Caster, m, StatType.Str, true, true);
                    int newDex = SpellHelper.GetOffset(this, Caster, m, StatType.Dex, true, true);
                    int newInt = SpellHelper.GetOffset(this, Caster, m, StatType.Int, true, true);

                    if ((-newStr >= oldStr && -newDex >= oldDex && -newInt >= oldInt) ||
                        (newStr != 0 && newDex != 0 && newInt != 0))
                   

                    SpellHelper.AddStatCurse(this, Caster, m, StatType.Str, true);
                    SpellHelper.AddStatCurse(this, Caster, m, StatType.Dex, true);
                    SpellHelper.AddStatCurse(this, Caster, m, StatType.Int, true);

                    int percentage = (int)(SpellHelper.GetOffsetScalar(this, Caster, m, true) * 100);
                    TimeSpan length = SpellHelper.GetDuration(Caster, m);
                    string args;

                   

                    AddEffect(m, SpellHelper.GetDuration(Caster, m), oldStr, oldDex, oldInt);

                    if (m.Spell != null)
                        m.Spell.OnCasterHurt();

                    m.Paralyzed = false;

                    m.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);
                    m.PlaySound(0x1E1);

                  


                }
            }

            this.FinishSequence();
        }
        public static void AddEffect(Mobile m, TimeSpan duration, int strOffset, int dexOffset, int intOffset)
        {
            if (m == null)
                return;

            if (m_UnderEffect.ContainsKey(m))
            {
                m_UnderEffect[m].Stop();
                m_UnderEffect[m] = null;
            }

            // my spell is stronger, so lets remove the lesser spell
            if (WeakenSpell.IsUnderEffects(m) && SpellHelper.GetCurseOffset(m, StatType.Str) <= strOffset)
            {
                WeakenSpell.RemoveEffects(m, false);
            }

            if (ClumsySpell.IsUnderEffects(m) && SpellHelper.GetCurseOffset(m, StatType.Dex) <= dexOffset)
            {
                ClumsySpell.RemoveEffects(m, false);
            }

            if (FeeblemindSpell.IsUnderEffects(m) && SpellHelper.GetCurseOffset(m, StatType.Int) <= intOffset)
            {
                FeeblemindSpell.RemoveEffects(m, false);
            }

            m_UnderEffect[m] = Timer.DelayCall<Mobile>(duration, RemoveEffect, m); //= new CurseTimer(m, duration, strOffset, dexOffset, intOffset);
            m.UpdateResistances();
        }

        public static void RemoveEffect(Mobile m)
        {
            if (!WeakenSpell.IsUnderEffects(m))
                m.RemoveStatMod("[Magic] Str Curse");

            if (!ClumsySpell.IsUnderEffects(m))
                m.RemoveStatMod("[Magic] Dex Curse");

            if (!FeeblemindSpell.IsUnderEffects(m))
                m.RemoveStatMod("[Magic] Int Curse");

            BuffInfo.RemoveBuff(m, BuffIcon.Curse);

            if (m_UnderEffect.ContainsKey(m))
            {
                Timer t = m_UnderEffect[m];

                if (t != null)
                    t.Stop();

                m_UnderEffect.Remove(m);
            }

            m.UpdateResistances();
        }

        public static bool UnderEffect(Mobile m)
        {
            return m_UnderEffect.ContainsKey(m);
        }

     
    }
}
