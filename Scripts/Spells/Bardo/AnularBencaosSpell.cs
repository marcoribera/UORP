using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;
using System.Collections.Generic;
using Server.Spells.First;
using Server.Spells.Second;
using Server.Spells.Third;
using Server.Spells.Fourth;
using Server.Spells.Fifth;
using Server.Spells.Ninjitsu;
using System.Collections;
using Server.Network;
using Server.Gumps;
using Server.Spells;
using Server.Misc;


//using Server.OneTime.Events;

namespace Server.Spells.Bardo
{
    public enum BuffType
    {
        None,
        MagicReflect,
        ReactiveArmor,
        Protection,
        Transformation,
        StrBonus,
        DexBonus,
        IntBonus,
        BarrabHemolymph,
        UraliTrance,
        Bless
    }

    public class AnularBencaosSpell : BardoSpell
    {
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(5); } }


        private static SpellInfo m_Info = new SpellInfo(
                "Anular Bençãos", "Parece que o jogo virou, hein?",
                230,
                9022
            );

        public AnularBencaosSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
            ExpiryMessage = "O efeito da música parece ter acabado";
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
            Caster.Target = new InternalTarget(this);

      //      BardFunctions.UseBardInstrument(BardoSpellbook.Instrument, sings, Caster);
        }

        public override double RequiredSkill
        {
            get
            {
                return 20.0;
            }
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Second;
            }
        }

        public void OnTarget(object o)
        {
            Mobile target = o as Mobile;

            if (target == null)
                return;

            if (m_CurseTable.ContainsKey(Caster))
            {
                Caster.SendLocalizedMessage(1154212); //You may not use the Purge Magic spell while you are under its curse.
            }
            else if (m_ImmuneTable.ContainsKey(target) || m_CurseTable.ContainsKey(target))
            {
                Caster.SendLocalizedMessage(1080119); // Your Purge Magic has been resisted!
            }
            else if (CheckHSequence(target))
            {
                if (CheckResisted(target))
                {
                    target.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                    Caster.SendLocalizedMessage(1080119); //Your Purge Magic has been resisted!
                }
                else
                {
                    SpellHelper.CheckReflect((int)Circle, Caster, ref target);

                    Caster.PlaySound(0x655);
                    Effects.SendLocationParticles(EffectItem.Create(target.Location, target.Map, EffectItem.DefaultDuration), 0x3728, 1, 13, 0x834, 0, 0x13B2, 0);

                    BuffType type = GetRandomBuff(target);

                    if (type != BuffType.None)
                    {
                        string arg = "";

                        switch (type)
                        {
                            case BuffType.MagicReflect:
                                MagicReflectSpell.EndReflect(target);
                                arg = "magic reflect";
                                break;
                            case BuffType.ReactiveArmor:
                                ReactiveArmorSpell.EndArmor(target);
                                arg = "reactive armor";
                                break;
                            case BuffType.Protection:
                                ProtectionSpell.EndProtection(target);
                                arg = "protection";
                                break;
                            case BuffType.Transformation:
                                TransformationSpellHelper.RemoveContext(target, true);
                                arg = "transformation spell";
                                break;
                            case BuffType.StrBonus:
                                arg = "strength bonus";
                                target.RemoveStatMod("[Magic] Str Buff");
                                BuffInfo.RemoveBuff(target, BuffIcon.Strength);
                                break;
                            case BuffType.DexBonus:
                                arg = "dexterity bonus";
                                target.RemoveStatMod("[Magic] Dex Buff");
                                BuffInfo.RemoveBuff(target, BuffIcon.Agility);
                                break;
                            case BuffType.IntBonus:
                                arg = "intelligence bonus";
                                target.RemoveStatMod("[Magic] Int Buff");
                                BuffInfo.RemoveBuff(target, BuffIcon.Cunning);
                                break;
                            case BuffType.BarrabHemolymph:
                                arg = "Barrab hemolymph";
                                EodonianPotion.RemoveEffects(target, PotionEffect.Barrab);
                                break;
                            case BuffType.UraliTrance:
                                arg = "Urali Trance";
                                EodonianPotion.RemoveEffects(target, PotionEffect.Urali);
                                break;
                            case BuffType.Bless:
                                arg = "bless";
                                target.RemoveStatMod("[Magic] Str Buff");
                                target.RemoveStatMod("[Magic] Dex Buff");
                                target.RemoveStatMod("[Magic] Int Buff");
                                BuffInfo.RemoveBuff(target, BuffIcon.Bless);
                                BlessSpell.RemoveBless(target);
                                break;
                        }

                        target.SendLocalizedMessage(1080117, arg); //Your ~1_ABILITY_NAME~ has been purged.
                        Caster.SendLocalizedMessage(1080118, arg); //Your target's ~1_ABILITY_NAME~ has been purged.

                        int duration = (int)((Caster.Skills[CastSkill].Value + Caster.Skills[DamageSkill].Value) / 15);

                        if (duration <= 0)
                            duration = 1;

                        m_ImmuneTable.Add(target, new ImmuneTimer(target, TimeSpan.FromSeconds(duration)));
                    }
                    else
                    {
                        Caster.SendLocalizedMessage(1080120); //Your target has no magic that can be purged.

                        int duration = (int)((Caster.Skills[CastSkill].Value + Caster.Skills[DamageSkill].Value) / 28);

                        if (duration <= 0)
                            duration = 1;

                        m_CurseTable.Add(target, new CurseTimer(target, Caster, TimeSpan.FromSeconds(duration)));
                    }
                }
            }

            FinishSequence();
        }

        public BuffType GetRandomBuff(Mobile target)
        {
            List<BuffType> buffs = new List<BuffType>();

            if (MagicReflectSpell.HasReflect(target))
                buffs.Add(BuffType.MagicReflect);

            if (ReactiveArmorSpell.HasArmor(target))
                buffs.Add(BuffType.ReactiveArmor);

            if (ProtectionSpell.HasProtection(target))
                buffs.Add(BuffType.Protection);

            TransformContext context = TransformationSpellHelper.GetContext(target);

            if (context != null && context.Type != typeof(AnimalForm))
                buffs.Add(BuffType.Transformation);

            if (BlessSpell.IsBlessed(target))
            {
                buffs.Add(BuffType.Bless);
            }
            else
            {
                StatMod mod = target.GetStatMod("[Magic] Str Buff");
                if (mod != null)
                    buffs.Add(BuffType.StrBonus);

                mod = target.GetStatMod("[Magic] Dex Buff");
                if (mod != null)
                    buffs.Add(BuffType.DexBonus);

                mod = target.GetStatMod("[Magic] Int Buff");
                if (mod != null)
                    buffs.Add(BuffType.IntBonus);
            }

            if (EodonianPotion.IsUnderEffects(target, PotionEffect.Barrab))
                buffs.Add(BuffType.BarrabHemolymph);

            if (EodonianPotion.IsUnderEffects(target, PotionEffect.Urali))
                buffs.Add(BuffType.UraliTrance);

            if (buffs.Count == 0)
                return BuffType.None;

            BuffType type = buffs[Utility.Random(buffs.Count)];
            buffs.Clear();

            return type;
        }

        private static Dictionary<Mobile, ImmuneTimer> m_ImmuneTable = new Dictionary<Mobile, ImmuneTimer>();
        private static Dictionary<Mobile, CurseTimer> m_CurseTable = new Dictionary<Mobile, CurseTimer>();

        public static void RemoveImmunity(Mobile from)
        {
            if (m_ImmuneTable.ContainsKey(from))
            {
                m_ImmuneTable[from].Stop();
                m_ImmuneTable.Remove(from);
            }
        }

        public static void RemoveCurse(Mobile from, Mobile caster)
        {
            if (m_CurseTable.ContainsKey(from))
            {
                m_CurseTable[from].Stop();

                if (DateTime.UtcNow > m_CurseTable[from].StartTime)
                {
                    TimeSpan inEffect = DateTime.UtcNow - m_CurseTable[from].StartTime;

                    int damage = 5 * (int)inEffect.TotalSeconds;

                    from.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                    from.PlaySound(0x307);

                    m_CurseTable.Remove(from);

                    AOS.Damage(from, caster, damage, 0, 0, 0, 0, 0, 100, 0);
                }
            }

            m_ImmuneTable[from] = new ImmuneTimer(from, TimeSpan.FromSeconds(16));
        }

        public static void OnMobileDoDamage(Mobile from)
        {
            if (from != null && m_CurseTable.ContainsKey(from))
                RemoveCurse(from, m_CurseTable[from].Caster);
        }

        public static bool IsUnderCurseEffects(Mobile from)
        {
            return m_CurseTable.ContainsKey(from);
        }

        private class ImmuneTimer : Timer
        {
            private Mobile m_Mobile;

            public ImmuneTimer(Mobile mob, TimeSpan duration) : base(duration)
            {
                m_Mobile = mob;
                Start();
            }

            protected override void OnTick()
            {
                AnularBencaosSpell.RemoveImmunity(m_Mobile);
            }
        }

        private class CurseTimer : Timer
        {
            private Mobile m_Mobile;
            private Mobile m_Caster;
            private DateTime m_StartTime;

            public DateTime StartTime { get { return m_StartTime; } }
            public Mobile Caster { get { return m_Caster; } }

            public CurseTimer(Mobile mob, Mobile caster, TimeSpan duration)
                : base(duration)
            {
                m_Mobile = mob;
                m_Caster = caster;
                m_StartTime = DateTime.UtcNow;
                Start();
            }

            protected override void OnTick()
            {
                AnularBencaosSpell.RemoveCurse(m_Mobile, m_Caster);
            }
        }

        public class InternalTarget : Target
        {
            public AnularBencaosSpell Owner { get; set; }

            public InternalTarget(AnularBencaosSpell owner)
                : this(owner, false)
            {
            }

            public InternalTarget(AnularBencaosSpell owner, bool allowland)
                : base(12, allowland, TargetFlags.Harmful)
            {
                Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o == null)
                    return;

                if (!from.CanSee(o))
                    from.SendLocalizedMessage(500237); // Target can not be seen.
                else
                {
                    SpellHelper.Turn(from, o);
                    Owner.OnTarget(o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                Owner.FinishSequence();
            }
        }
    }
}