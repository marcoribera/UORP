using System;
using Server.Targeting;
using System.Collections.Generic;
using Server.Items;
using System.IO;
using Server;
using Server.Mobiles;

namespace Server.Spells.Bardo
{
    public class SomDaMelhoriaSpell : BardoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Som Da Melhoria", "Te fiz uma pessoa melhor!",
            203,
            9061);

        private static Dictionary<Mobile, InternalTimer> _Table;

        public static bool IsBlessed(Mobile m)
        {
            return _Table != null && _Table.ContainsKey(m);
        }

        public override int EficienciaMagica(Mobile caster) { return 5; } //Servirá para calcular o modificador na eficiência das magias


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

        public static void AddBless(Mobile m, TimeSpan duration)
        {
            
            if (_Table == null)
                _Table = new Dictionary<Mobile, InternalTimer>();

            if (_Table.ContainsKey(m))
            {
                _Table[m].Stop();
            }

            _Table[m] = new InternalTimer(m, duration);
        }

        public static void RemoveBless(Mobile m, bool early = false)
        {
            if (_Table != null && _Table.ContainsKey(m))
            {
                _Table[m].Stop();
                m.Delta(MobileDelta.Stat);

                _Table.Remove(m);
            }
        }

        public SomDaMelhoriaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Fifth;
            }
        }

        public override double RequiredSkill
        {
            get
            {
                return 50.0;
            }
        }

        private bool CheckInstrument()
        {
            return Caster.Backpack.FindItemByType(typeof(BaseInstrument)) != null;
        }

        private BaseInstrument GetInstrument()
        {
            return Caster.Backpack.FindItemByType(typeof(BaseInstrument)) as BaseInstrument;
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
                int oldDex = SpellHelper.GetBuffOffset(m, StatType.Dex);
                int oldInt = SpellHelper.GetBuffOffset(m, StatType.Int);

                int newStr = SpellHelper.GetOffset(this,Caster, m, StatType.Str, false, true);
                int newDex = SpellHelper.GetOffset(this,Caster, m, StatType.Dex, false, true);
                int newInt = SpellHelper.GetOffset(this,Caster, m, StatType.Int, false, true);

                if ((newStr >= oldStr && newDex >= oldDex && newInt >= oldInt) || 
                    (newStr != 0 && newDex != 0 && newInt != 0))

                    SpellHelper.AddStatBonus(this, this.Caster, m, false, StatType.Str);
                    SpellHelper.AddStatBonus(this, this.Caster, m, true, StatType.Dex);
                    SpellHelper.AddStatBonus(this, this.Caster, m, true, StatType.Int);

                    int percentage = (int)(SpellHelper.GetOffsetScalar(this,this.Caster, m, false) * 100);
                    TimeSpan length = SpellHelper.GetDuration(this.Caster, m);
                    string args = String.Format("{0}\t{1}\t{2}", percentage, percentage, percentage);
                    BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Bless, 1075847, 1075848, length, m, args.ToString()));

                    m.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
                    m.PlaySound(0x1EA);

                    AddBless(Caster, length + TimeSpan.FromMilliseconds(50));
                }
            }

            this.FinishSequence();
        }

  
        private class InternalTimer : Timer
        {
            public Mobile Mobile { get; set; }

            public InternalTimer(Mobile m, TimeSpan duration)
                : base(duration)
            {
                Mobile = m;
                Start();
            }

            protected override void OnTick()
            {
                SomDaMelhoriaSpell.RemoveBless(Mobile);
            }
        }



        
       /* private void Serialize()
        {
            // Save the used musical instrument
            BaseInstrument instrument = GetInstrument();
            if (instrument != null)
            {
                // Serialize the instrument to be saved in the player's profile
                Caster.Serialize(instrument);
            }
        }


        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            // Deserialize the musical instrument
            BaseInstrument instrument = reader.ReadItem() as BaseInstrument;
            if (instrument != null)
            {
                // Add the deserialized instrument to the player's backpack
                AddItem(instrument);
            }
        }*/
    }
}
