using System;
using System.Collections.Generic;
using System.Linq;

using Server.Items;
using Server.Mobiles;
using Server.Spells.Necromancy;
using Server.Spells.SkillMasteries;


namespace Server.Spells.Algoz
{
    public class BanimentoProfanoSpell : AlgozSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Banimento profano", "Expelle Bonum",
            212,
            9031,
            Reagent.Garlic,
            Reagent.MandrakeRoot,
            Reagent.SulfurousAsh);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias
        public BanimentoProfanoSpell(Mobile caster, Item scroll)
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
        public override void OnCast()
        {
            if (this.CheckSequence())
            {
                Caster.PlaySound(0xF5);
                Caster.PlaySound(0x299);
                Caster.FixedParticles(0x37C4, 1, 25, 9922, 14, 3, EffectLayer.Head);

                foreach (var m in AcquireIndirectTargets(Caster.Location, 8).OfType<Mobile>())
                {
                    BaseCreature bc = m as BaseCreature;

                    if (bc != null)
                    {
                        bool dispellable = bc.Summoned && (bc.Karma > 2000);

                        if (dispellable)
                        {
                            double dispelChance = (50.0 + (100 * (Caster.Skills.PoderMagico.Value - bc.GetDispelDifficulty()) / (bc.DispelFocus * 2))) / 100;

                            if (dispelChance > Utility.RandomDouble())
                            {
                                Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                                Effects.PlaySound(m, m.Map, 0x201);

                                m.Delete();
                                continue;
                            }
                        }

                        bool scarable = !bc.Controlled && bc.Karma > 2000;

                        if (scarable)
                        {
                            // TODO: Is this right?
                            double fleeChance = (100 - Math.Sqrt(m.Fame / 2)) * Caster.Skills.PoderMagico.Value * Caster.Skills.Ordem.Value;
                            fleeChance /= 1000000;

                            if (fleeChance > Utility.RandomDouble())
                            {
                                // guide says 2 seconds, it's longer
                                bc.BeginFlee(TimeSpan.FromSeconds(30.0));
                            }
                        }
                    }

                    TransformContext context = TransformationSpellHelper.GetContext(m);
                    if (context != null && m.Karma > 2000)
                    {
                        // transformed and good..
                        double drainChance = 0.5 * (Caster.Skills.PoderMagico.Value / Math.Max(m.Skills.ResistenciaMagica.Value, 1));

                        if (drainChance > Utility.RandomDouble())
                        {
                            int drain = (5 * EficienciaMagica(this.Caster));

                            m.Stam -= drain;
                            m.Mana -= drain;
                        }
                    }
                }
            }
           FinishSequence();
        }
    }
}
