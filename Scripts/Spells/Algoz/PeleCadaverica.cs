using System;
using System.Collections.Generic;
using Server.Targeting;
using Server.Spells.SkillMasteries;

namespace Server.Spells.Algoz
{
    public class PeleCadavericaSpell : AlgozSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "PeleCadaverica", "Cutis Mortum",
            203,
            9051,
            Reagent.BatWing,
            Reagent.GraveDust);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias

        private static readonly Dictionary<Mobile, ExpireTimer> m_Table = new Dictionary<Mobile, ExpireTimer>();

        public PeleCadavericaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(1.75);
            }
        }
       
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Fifth;
            }
        }
      
        public static bool RemoveCurse(Mobile m)
        {
            if (m_Table.ContainsKey(m))
            {
                m_Table[m].DoExpire();
                return true;
            }

            return false;
        }

        public static bool IsUnderEffects(Mobile m)
        {
            return m_Table.ContainsKey(m);
        }

        public static int GetResistMalus(Mobile m)
        {
            if (m_Table.ContainsKey(m))
            {
                return 70 - m_Table[m].Malus;
            }

            return 70;
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (CheckHSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                ApplyEffects(m);
                ConduitSpell.CheckAffected(Caster, m, ApplyEffects);
            }

            FinishSequence();
        }

        public void ApplyEffects(Mobile m, double strength = 1.0)
        {
            /* Transmogrifies the flesh of the target creature or player to resemble rotted corpse flesh,
                * making them more vulnerable to Fire and Poison damage,
                * but increasing their resistance to Physical and Cold damage.
                * 
                * The effect lasts for ((Spirit Speak skill level - target's Resist Magic skill level) / 25 ) + 40 seconds.
                * 
                * NOTE: Algorithm above is fixed point, should be:
                * ((ss-mr)/2.5) + 40
                * 
                * NOTE: Resistance is not checked if targeting yourself
                */

            if (m_Table.ContainsKey(m))
            {
                m_Table[m].DoExpire(false);
            }

            m.SendLocalizedMessage(1061689); // Your skin turns dry and corpselike.

            if (m.Spell != null)
                m.Spell.OnCasterHurt();

            m.FixedParticles(0x373A, 1, 15, 9913, SpellEffectHue, 7, EffectLayer.Head);
            m.PlaySound(0x1BB);

            double ss = GetDamageSkill(Caster);
            double mr = GetResistSkill(m);
            m.CheckSkill(SkillName.ResistenciaMagica, 0.0, m.Skills[SkillName.ResistenciaMagica].Cap);	//Skill check for gain

            TimeSpan duration = TimeSpan.FromSeconds((((ss - mr) / 2.5) + 40.0) * EficienciaMagica(Caster));

            int malus = (int)Math.Min(15, (Caster.Skills[CastSkill].Value + Caster.Skills[DamageSkill].Value) * 0.075);

            ResistanceMod[] mods = new ResistanceMod[4]
					{
						new ResistanceMod( ResistanceType.Fire, (int)(-10 * EficienciaMagica(Caster)) ),
						new ResistanceMod( ResistanceType.Poison, (int)(-10 * EficienciaMagica(Caster)) ),
						new ResistanceMod( ResistanceType.Cold, (int)(+10.0 * EficienciaMagica(Caster)) ),
						new ResistanceMod( ResistanceType.Physical, (int)(+10.0 * EficienciaMagica(Caster)) )
					};

            ExpireTimer timer = new ExpireTimer(m, mods, malus, duration);
            timer.Start();

            BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.CorpseSkin, 1075663, duration, m));

            m_Table[m] = timer;

            m.UpdateResistances();

            for (int i = 0; i < mods.Length; ++i)
                m.AddResistanceMod(mods[i]);

            HarmfulSpell(m);
        }

        private class ExpireTimer : Timer
        {
            private readonly Mobile m_Mobile;
            private readonly ResistanceMod[] m_Mods;
            private readonly int m_Malus;

            public int Malus { get { return m_Malus; } }

            public ExpireTimer(Mobile m, ResistanceMod[] mods, int malus, TimeSpan delay)
                : base(delay)
            {
                m_Mobile = m;
                m_Mods = mods;
                m_Malus = malus;
            }

            public void DoExpire(bool message = true)
            {
                for (int i = 0; i < m_Mods.Length; ++i)
                    m_Mobile.RemoveResistanceMod(m_Mods[i]);

                Stop();
                BuffInfo.RemoveBuff(m_Mobile, BuffIcon.CorpseSkin);

                if(m_Table.ContainsKey(m_Mobile))
                    m_Table.Remove(m_Mobile);

                m_Mobile.UpdateResistances();

                if(message)
                    m_Mobile.SendLocalizedMessage(1061688); // Your skin returns to normal.
            }

            protected override void OnTick()
            {
                DoExpire();
            }
        }

        private class InternalTarget : Target
        {
            private readonly PeleCadavericaSpell m_Owner;
            public InternalTarget(PeleCadavericaSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    m_Owner.Target((Mobile)o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}
