using System;
using Server.Items;
using Server.Spells.First;
using Server.Spells.Fourth;
using Server.Spells.Necromancy;
using Server.Targeting;
using Server.Spells.Mysticism;

namespace Server.Spells.ClerigoDaVida
{
    public class RemoverMaldicaoSpell : ClerigoDaVidaSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Remover Maldição", "Ad Removere",
            -1,
            9002);
        public RemoverMaldicaoSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override double RequiredSkill
        {
            get
            {
                return 50.0;
            }
        }
        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Third;
            }
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(1.5);
            }
        }
       
      
        
        public override bool CheckDisturb(DisturbType type, bool firstCircle, bool resistable)
        {
            return true;
        }

        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (this.CheckBSequence(m))
            {
                SpellHelper.Turn(this.Caster, m);

                /* Attempts to remove all Curse effects from Target.
                * Curses include Mage spells such as Clumsy, Weaken, Feeblemind and Paralyze
                * as well as all Necromancer curses.
                * Chance of removing curse is affected by Caster's Karma.
                */

                int chance = 0;

                if (this.Caster.Karma < -5000)
                    chance = 0;
                else if (this.Caster.Karma < 0)
                    chance = (int)Math.Sqrt(20000 + this.Caster.Karma) - 122;
                else if (this.Caster.Karma < 5625)
                    chance = (int)Math.Sqrt(this.Caster.Karma) + 25;
                else
                    chance = 100;

                if (chance > Utility.Random(100))
                {
                    m.PlaySound(0xF6);
                    m.PlaySound(0x1F7);
                    m.FixedParticles(0x3709, 1, 30, 9963, 13, 3, EffectLayer.Head);

                    IEntity from = new Entity(Serial.Zero, new Point3D(m.X, m.Y, m.Z - 10), this.Caster.Map);
                    IEntity to = new Entity(Serial.Zero, new Point3D(m.X, m.Y, m.Z + 50), this.Caster.Map);
                    Effects.SendMovingParticles(from, to, 0x2255, 1, 0, false, false, 13, 3, 9501, 1, 0, EffectLayer.Head, 0x100);

                    m.Paralyzed = false;

                    EvilOmenSpell.TryEndEffect(m);
                    StrangleSpell.RemoveCurse(m);
                    CorpseSkinSpell.RemoveCurse(m);
                    CurseSpell.RemoveEffect(m);
                    MortalStrike.EndWound(m);
                    WeakenSpell.RemoveEffects(m);
                    FeeblemindSpell.RemoveEffects(m);
                    ClumsySpell.RemoveEffects(m);

                    if (Core.ML)
                    {
                        BloodOathSpell.RemoveCurse(m);
                    }

                    MindRotSpell.ClearMindRotScalar(m);
                    SpellPlagueSpell.RemoveFromList(m);

                    BuffInfo.RemoveBuff(m, BuffIcon.MassCurse);
                }
                else
                {
                    m.PlaySound(0x1DF);
                }
            }

            this.FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly RemoverMaldicaoSpell m_Owner;
            public InternalTarget(RemoverMaldicaoSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Beneficial)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    this.m_Owner.Target((Mobile)o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}
