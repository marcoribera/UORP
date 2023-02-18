using System;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Bardo
{
    public class EncantarCriaturaSpell : BardoSpell
    { 
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Encantar Criatura", "Eu sou o mestre das marionetes!!",
            -1);
        public EncantarCriaturaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(3);
            }
        }
        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Fourth;
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
                return 40.0;
            }
        }
        public static bool IsValidTarget(BaseCreature bc)
        {
            if (bc == null || bc.IsParagon || (bc.Controlled && !bc.Allured) || bc.Summoned || bc.AllureImmune)
                return false;
				
            SlayerEntry slayer = SlayerGroup.GetEntryByName(SlayerName.Repond);
			
            if (slayer != null && slayer.Slays(bc))
                return true;
			
            return false;
        }

        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        public void Target(BaseCreature bc)
        {
            if (!this.Caster.CanSee(bc.Location) || !this.Caster.InLOS(bc))
            {
                this.Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (!IsValidTarget(bc))
            {
                this.Caster.SendLocalizedMessage(1074379); // You cannot charm that!
            }
            else if (this.Caster.Followers + 3 > this.Caster.FollowersMax)
            {
                this.Caster.SendLocalizedMessage(1049607); // You have too many followers to control that creature.
            }
            else if (bc.Allured)
            {
                this.Caster.SendLocalizedMessage(1074380); // This humanoid is already controlled by someone else.				
            }
            else if (this.CheckSequence())
            {
                
                double skill = this.Caster.Skills[this.CastSkill].Value;

                double chance = (skill / 150.0) + (skill / 20) / (50.0);

                if (chance > Utility.RandomDouble())
                {
                    bc.ControlSlots = 3;				
                    bc.Combatant = null;
						
                    if (this.Caster.Combatant == bc)
                    {
                        this.Caster.Combatant = null;
                        this.Caster.Warmode = false;
                    }
					
                    if (bc.SetControlMaster(this.Caster))
                    {
                        bc.PlaySound(0x5C4);
                        bc.Allured = true;
						
                        Container pack = bc.Backpack;

                        if (pack != null)
                        {
                            for (int i = pack.Items.Count - 1; i >= 0; --i)
                            {
                                if (i >= pack.Items.Count)
                                    continue;
			
                                pack.Items[i].Delete();
                            }
                        }
						
                        this.Caster.SendLocalizedMessage(1074377); // You allure the humanoid to follow and protect you.
                    }
                }
                else
                {
                    bc.PlaySound(0x5C5);
                    bc.ControlTarget = this.Caster;
                    bc.ControlOrder = OrderType.Attack;
                    bc.Combatant = this.Caster;

                    this.Caster.SendLocalizedMessage(1074378); // The humanoid becomes enraged by your charming attempt and attacks you.
                }
            }

            this.FinishSequence();
        }

        public class InternalTarget : Target
        {
            private readonly EncantarCriaturaSpell m_Owner;
            public InternalTarget(EncantarCriaturaSpell owner)
                : base(12, false, TargetFlags.None)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile m, object o)
            {
                if (o is BaseCreature)
                {
                    this.m_Owner.Target((BaseCreature)o);
                }
                else
                {
                    m.SendLocalizedMessage(1074379); // You cannot charm that!
                }
            }

            protected override void OnTargetFinish(Mobile m)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}
