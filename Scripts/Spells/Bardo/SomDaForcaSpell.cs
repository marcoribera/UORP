using System;
using Server.Targeting;
using Server.Network;
using Server.Items;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Spells.Bardo
{
    public class SomDaForcaSpell : BardoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Som da Força", "Uau, que musculoso!",
            212,
            9061);
        public SomDaForcaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }


        public override int EficienciaMagica(Mobile caster) { return 5; } //Servirá para calcular o modificador na eficiência das magias
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Third;
            }
        }

        public override double RequiredSkill
        {
            get
            {
                return 30.0;
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
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(Caster.Location), 10);

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
                                if ((Caster.Party == null) && (criatura.GetMaster() != null)) //se a criatura não estiver em party com o conjurador, e seu mestre não for o caster
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
                    int oldStr = SpellHelper.GetBuffOffset(m, StatType.Str);
                    int newStr = SpellHelper.GetOffset(this, Caster, m, StatType.Str, false, true);

                    if (newStr >= oldStr && newStr != 0)
                    {
                        SpellHelper.AddStatBonus(this, this.Caster, m, false, StatType.Str);
                        int percentage = (int)(SpellHelper.GetOffsetScalar(this, this.Caster, m, false) * 100);
                        TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                        BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Strength, 1075845, length, m, percentage.ToString()));

                        m.FixedParticles(0x375A, 10, 15, 5017, EffectLayer.Waist);
                        m.PlaySound(0x1EE);
                    }
                }

                this.FinishSequence();
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
    }
   
}
