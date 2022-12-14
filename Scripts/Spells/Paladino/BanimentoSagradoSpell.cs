using System;
using System.Collections.Generic;
using System.Linq;

using Server.Items;
using Server.Mobiles;
using Server.Spells.Necromancy;


namespace Server.Spells.Paladino
{
    public class BanimentoSagradoSpell : PaladinoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Banimento Sagrado", "Sanct Exili",
            -1,
            9002,
            Reagent.Bloodmoss,
            Reagent.Incenso);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias

        public BanimentoSagradoSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(0.25);
            }
        }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Third;
            }
        }

       
        public override bool ConsumeReagents()
        {
            if (base.ConsumeReagents())
                return true;
            else
                return false;
        }
        public override bool BlocksMovement
        {
            get
            {
                return false;
            }
        }
        
       
        public override bool DelayedDamage
        {
            get
            {
                return false;
            }
        }
        public override void SendCastEffect()
        {
            Caster.FixedEffect(0x37C4, 10, 7, 4, 3); // At player
        }
        public override void OnCast()
        {
            if (this.CheckSequence())
            {
                Caster.PlaySound(0xF5);
                Caster.PlaySound(0x299);
                Caster.FixedParticles(0x37C4, 1, 25, 9922, 14, 3, EffectLayer.Head);

                int dispelSkill = 10;//ComputePowerValue(2);
                double chiv = Caster.Skills.Ordem.Value;

                foreach (var m in AcquireIndirectTargets(Caster.Location, 8).OfType<Mobile>())
                {
                    BaseCreature bc = m as BaseCreature;

                    if (bc != null)
                    {
                        bool dispellable = bc.Summoned && !bc.IsAnimatedDead;

                        if (dispellable)
                        {
                            double dispelChance = (50.0 + ((100 * (chiv - bc.GetDispelDifficulty())) / (bc.DispelFocus * 2))) / 100;
                            dispelChance *= dispelSkill / 100.0;

                            if (dispelChance > Utility.RandomDouble())
                            {
                                Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                                Effects.PlaySound(m, m.Map, 0x201);

                                m.Delete();
                                continue;
                            }
                        }

                        bool evil = !bc.Controlled && bc.Karma < 0;

                        if (evil)
                        {
                            // TODO: Is this right?
                            double fleeChance = (100 - Math.Sqrt(m.Fame / 2)) * chiv * dispelSkill;
                            fleeChance /= 1000000;

                            if (fleeChance > Utility.RandomDouble())
                            {
                                // guide says 2 seconds, it's longer
                                bc.BeginFlee(TimeSpan.FromSeconds(30.0));
                            }
                        }
                    }

                    TransformContext context = TransformationSpellHelper.GetContext(m);
                    if (context != null && context.Spell is NecromancerSpell)	//Trees are not evil!	TODO: OSI confirm? avaliar se isso esta certo, necromancerspell
                    {
                        // transformed ..
                        double drainChance = 0.5 * (this.Caster.Skills.Ordem.Value / Math.Max(m.Skills.Necromancia.Value, 1));

                        if (drainChance > Utility.RandomDouble())
                        {
                            int drain = (5 * dispelSkill) / 100;

                            m.Stam -= drain;
                            m.Mana -= drain;
                        }
                    }
                }
            }
            FinishSequence();
        }
       

    }// Dispiro Malas
       

       

           
 }
