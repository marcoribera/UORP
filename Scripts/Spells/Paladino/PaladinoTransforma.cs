using System;
using Server.Spells.Paladino;

namespace Server.Spells.Paladino
{
    public abstract class PaladinoTransforma : PaladinoSpell, ITransformationSpell
    {
        private static readonly int[] m_ManaTable = new int[] { 4, 6, 9, 13, 19, 28, 42, 63, 94, 141, 211 };
        private const double ChanceOffset = 20.0, ChanceLength = 120.0 / 10.0; //originalmente era: ChanceOffset = 20.0, ChanceLength = 100.0 /7.0

        public PaladinoTransforma(Mobile caster, Item scroll, SpellInfo info)
            : base(caster, scroll, info)
        {
        }

        public abstract int Body { get; }
        public virtual int Hue
        {
            get
            {
                return 0;
            }
        }

        public abstract double RequiredSkill { get; }

   

        public virtual int PhysResistOffset
        {
            get
            {
                return 0;
            }
        }
        public virtual int FireResistOffset
        {
            get
            {
                return 0;
            }
        }
        public virtual int ColdResistOffset
        {
            get
            {
                return 0;
            }
        }
        public virtual int PoisResistOffset
        {
            get
            {
                return 0;
            }
        }
        public virtual int NrgyResistOffset
        {
            get
            {
                return 0;
            }
        }
        public override bool BlockedByHorrificBeast
        {
            get
            {
                return false;
            }
        }
        public virtual double TickRate
        {
            get
            {
                return 1.0;
            }
        }
        public override bool CheckCast()
        {
            if (!TransformationSpellHelper.CheckCast(this.Caster, this))
                return false;

            return base.CheckCast();
        }

        public override void OnCast()
        {
            TransformationSpellHelper.OnCast(this.Caster, this);

            this.FinishSequence();
        }

        public virtual void OnTick(Mobile m)
        {
        }

        public virtual void DoEffect(Mobile m)
        {
        }

        public virtual void RemoveEffect(Mobile m)
        {
        }
    }
}
