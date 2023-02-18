using System;
using Server.Targeting;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Spells.Paladino
{
    public class ForcaDoDevotoSpell : PaladinoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Força Do Devoto", "Devotee Forti",
            212,
            9061,
            Reagent.MandrakeRoot,
            Reagent.Nightshade);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias

        public ForcaDoDevotoSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Third;
            }
        }
        
        public override void OnCast()
        {
            List<Mobile> targets = new List<Mobile>();

            Map map = this.Caster.Map;

            if (this.CheckSequence())
            {
                if (map != null)
                {
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(Caster.Location), 4);

                    foreach (Mobile m in eable)
                    {
                        if (m is PlayerMobile) //Se o char alvo é de player
                        {
                            if (m != Caster) //mas não é o conjurador
                            {
                                bool valido = false;

                                if (Caster.Party == null) //Fora de party afeta todos os players na área
                                {
                                    valido = true;
                                }
                                else if (m.Party == Caster.Party) // verifica se o conjurador tá numa Party e se ela é a mesma do alvo
                                {
                                    valido = true; //Se forem da mesma Party é um alvo válido
                                }

                                if ((Caster.Guild != null) && (m.Guild == Caster.Guild)) // verifica se o conjurador tá numa Guild e se ela é a mesma do alvo
                                {
                                    valido = true; //Se forem da mesma Guild é um alvo válido
                                }
                                if (valido) //se for um alvo válido adiciona na lista
                                {
                                    targets.Add(m as Mobile); //é um alvo válido.
                                }
                            }
                            else
                            {
                                targets.Add(m as Mobile); //Em buffs inclue o próprio conjurador
                            }
                        }
                        if (m is BaseCreature)
                        {
                            BaseCreature criatura = m as BaseCreature;
                            bool valido = false;
                            if (criatura.GetMaster() != null)
                            {
                                if (criatura.GetMaster() == Caster)
                                {
                                    valido = true;
                                }
                                if ((Caster.Party != null) && (criatura.GetMaster().Party == Caster.Party))
                                {
                                    valido = true;
                                }
                                if ((Caster.Guild != null) && (criatura.GetMaster().Guild == Caster.Guild))
                                {
                                    valido = true;
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
                    SpellHelper.Turn(this.Caster, m);

                    int oldStr = SpellHelper.GetBuffOffset(m, StatType.Str);
                    int newStr = SpellHelper.GetOffset(this, Caster, m, StatType.Str, false, true);

                    if (newStr < oldStr || newStr == 0)
                    {
                        DoHurtFizzle();
                    }
                    else
                    {
                        SpellHelper.AddStatBonus(this, this.Caster, m, false, StatType.Str);
                        int percentage = (int)(SpellHelper.GetOffsetScalar(this, this.Caster, m, false) * 100);
                        TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                        BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Strength, 1075845, length, m, percentage.ToString()));

                        m.FixedParticles(0x375A, 10, 15, 5017, SpellEffectHue, 3, EffectLayer.Waist);
                        m.PlaySound(0x1EE);
                    }
                }
            }

            this.FinishSequence();
        }
    }
}
