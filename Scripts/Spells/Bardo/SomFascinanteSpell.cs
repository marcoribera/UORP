using System;
using Server.Mobiles;
using Server.Spells.Chivalry;
using Server.Targeting;
using Server.Network;
using Server.Items;
using System.Collections.Generic;

namespace Server.Spells.Bardo
{
    public class SomFascinanteSpell : BardoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Som Fascinante", "Que grupo de pessoas maravilhoso!",
            218,
            9012);
        public SomFascinanteSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Sixth;
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
                return 60.0;
            }
        }
        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

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

                    for (int i = 0; i < targets.Count; ++i)
                    {
                        Mobile m = targets[i];
                        double duration;

                        if (Core.AOS)
                        {
                            int secs = (int)((this.GetDamageSkill(this.Caster) / 10) - (this.GetResistSkill(m) / 10));

                            if (!Core.SE)
                                secs += 2;

                            if (!m.Player)
                                secs *= 3;

                            if (secs < 0)
                                secs = 0;

                            duration = secs;
                        }
                        else
                        {
                            // Algorithm: ((20% of magery) + 7) seconds [- 50% if resisted]
                            duration = 7.0 + (this.Caster.Skills[SkillName.Caos].Value * 0.2);

                            if (this.CheckResisted(m))
                                duration *= 0.75;
                        }

                        if (m is PlagueBeastLord)
                        {
                            ((PlagueBeastLord)m).OnParalyzed(this.Caster);
                            duration = 120;
                        }

                        m.Paralyze(TimeSpan.FromSeconds(duration));

                        m.PlaySound(0x204);
                        m.FixedEffect(0x376A, 6, 1);

                        this.HarmfulSpell(m);
                    }

                    this.FinishSequence();
                }

            }
        }
    }
}
