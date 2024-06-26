using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;
using Server.Spells.SkillMasteries;

namespace Server.Misc
{
    public delegate Int32 RegenBonusHandler(Mobile from);

    public class RegenRates
    {
        public static List<RegenBonusHandler> HitsBonusHandlers = new List<RegenBonusHandler>();
        public static List<RegenBonusHandler> StamBonusHandlers = new List<RegenBonusHandler>();
        public static List<RegenBonusHandler> ManaBonusHandlers = new List<RegenBonusHandler>();
        public static List<RegenBonusHandler> DesmaioBonusHandlers = new List<RegenBonusHandler>(); // Criado para lidar com o Desmaio

        [CallPriority(10)]
        public static void Configure()
        {
            Mobile.DefaultHitsRate = TimeSpan.FromSeconds(11.0);
            Mobile.DefaultStamRate = TimeSpan.FromSeconds(7.0);
            Mobile.DefaultManaRate = TimeSpan.FromSeconds(7.0);
            Mobile.DefaultDesmaioRate = TimeSpan.FromSeconds(15.0); //Delay entre as recuperações de 0.1 de Desmaio

            Mobile.ManaRegenRateHandler = new RegenRateHandler(Mobile_ManaRegenRate);
            Mobile.DesmaioRegenRateHandler = new RegenRateHandler(Mobile_DesmaioRegenRate); //Hendler de Regen Rate de Desmaio

            if (Core.AOS)
            {
                Mobile.StamRegenRateHandler = new RegenRateHandler(Mobile_StamRegenRate);
                Mobile.HitsRegenRateHandler = new RegenRateHandler(Mobile_HitsRegenRate);
            }
        }

        public static double GetArmorOffset(Mobile from)
        {
            double rating = 0.0;

            if (!Core.AOS)
                rating += GetArmorMeditationValue(from.ShieldArmor as BaseArmor);

            rating += GetArmorMeditationValue(from.NeckArmor as BaseArmor);
            rating += GetArmorMeditationValue(from.HandArmor as BaseArmor);
            rating += GetArmorMeditationValue(from.HeadArmor as BaseArmor);
            rating += GetArmorMeditationValue(from.ArmsArmor as BaseArmor);
            rating += GetArmorMeditationValue(from.LegsArmor as BaseArmor);
            rating += GetArmorMeditationValue(from.ChestArmor as BaseArmor);

            return rating / 4;
        }

        private static void CheckBonusSkill(Mobile m, int cur, int max, SkillName skill)
        {
            if (!m.Alive)
                return;

            double n = (double)cur / max;
            double v = Math.Sqrt(m.Skills[skill].Value * 0.005);

            n *= (1.0 - v);
            n += v;

            m.CheckSkill(skill, n);
        }

        public static bool CheckTransform(Mobile m, Type type)
        {
            return TransformationSpellHelper.UnderTransformation(m, type);
        }

        public static bool CheckAnimal(Mobile m, Type type)
        {
            return AnimalForm.UnderTransformation(m, type);
        }

        private static TimeSpan Mobile_HitsRegenRate(Mobile from)
        {
            return TimeSpan.FromSeconds(1.0 / (0.1 * (1 + HitPointRegen(from))));
        }

        private static TimeSpan Mobile_StamRegenRate(Mobile from)
        {
            if (from.Skills == null)
                return Mobile.DefaultStamRate;

            CheckBonusSkill(from, from.Stam, from.StamMax, SkillName.PreparoFisico);

            double bonus = from.Skills[SkillName.PreparoFisico].Value * 0.1;

            bonus += StamRegen(from);

            if (Core.SA)
            {
                double rate = 1.0 / (1.42 + (bonus / 100));

                if (from is BaseCreature && ((BaseCreature)from).IsMonster)
                {
                    rate *= 1.95;
                }

                return TimeSpan.FromSeconds(rate);
            }
            else
            {
                return TimeSpan.FromSeconds(1.0 / (0.1 * (2 + bonus)));
            }
        }

        private static TimeSpan Mobile_ManaRegenRate(Mobile from)
        {
            if (from.Skills == null)
                return Mobile.DefaultManaRate;

            if (!from.Meditating)
                CheckBonusSkill(from, from.Mana, from.ManaMax, SkillName.PoderMagico); //Marcknight: Fazer escolher a maior dentre as skills de conhecimento mágico

            double rate;
            double armorPenalty = GetArmorOffset(from);

            if (Core.ML)
            {
                double med = from.Skills[SkillName.PoderMagico].Value; //Marcknight: Fazer escolher a maior dentre as skills de conhecimento mágico
                double focus = from.Skills[SkillName.PreparoFisico].Value;

                double focusBonus = focus / 200;
                double medBonus = 0;

                CheckBonusSkill(from, from.Mana, from.ManaMax, SkillName.PreparoFisico);

                if (armorPenalty == 0)
                {
                    medBonus = (0.0075 * med) + (0.0025 * from.Int);

                    if (medBonus >= 100.0)
                        medBonus *= 1.1;

                    if (from.Meditating)
                    {
                        medBonus *= 2;
                    }
                }

                double itemBase = ((((med / 2) + (focus / 4)) / 90) * .65) + 2.35;
                double intensityBonus = Math.Sqrt(ManaRegen(from));

                if (intensityBonus > 5.5)
                    intensityBonus = 5.5;

                double itemBonus = ((itemBase * intensityBonus) - (itemBase - 1)) / 10;

                rate = 1.0 / (0.2 + focusBonus + medBonus + itemBonus);
            }
            else if (Core.AOS)
            {
                double medPoints = from.Int + (from.Skills[SkillName.PoderMagico].Value * 3); //Marcknight: Fazer escolher a maior dentre as skills de conhecimento mágico

                medPoints *= (from.Skills[SkillName.PoderMagico].Value < 100.0) ? 0.025 : 0.0275; //Marcknight: Fazer escolher a maior dentre as skills de conhecimento mágico

                CheckBonusSkill(from, from.Mana, from.ManaMax, SkillName.PreparoFisico);

                double focusPoints = (from.Skills[SkillName.PreparoFisico].Value * 0.05);

                if (armorPenalty > 0)
                    medPoints = 0; // In AOS, wearing any meditation-blocking armor completely removes meditation bonus

                double totalPoints = focusPoints + medPoints + (from.Meditating ? (medPoints > 13.0 ? 13.0 : medPoints) : 0.0);

                totalPoints += ManaRegen(from);

                if (totalPoints < -1)
                    totalPoints = -1;

                if (Core.ML)
                    totalPoints = Math.Floor(totalPoints);

                rate = 1.0 / (0.1 * (2 + totalPoints));
            }
            else
            {
                double medPoints = (from.Int + from.Skills[SkillName.PoderMagico].Value) * 0.5; //Marcknight: Fazer escolher a maior dentre as skills de conhecimento mágico

                if (medPoints <= 0)
                    rate = 7.0;
                else if (medPoints <= 100)
                    rate = 7.0 - (239 * medPoints / 2400) + (19 * medPoints * medPoints / 48000);
                else if (medPoints < 120)
                    rate = 1.0;
                else
                    rate = 0.75;

                rate += armorPenalty;

                if (from.Meditating)
                    rate *= 0.5;

                if (rate < 0.5)
                    rate = 0.5;
                else if (rate > 7.0)
                    rate = 7.0;
            }

            if (double.IsNaN(rate))
            {
                return Mobile.DefaultManaRate;
            }

            return TimeSpan.FromSeconds(rate);
        }

        private static TimeSpan Mobile_DesmaioRegenRate(Mobile from)
        {
            if (from.Skills == null)
            {
                return Mobile.DefaultDesmaioRate;
            }
            else
            {
                return TimeSpan.FromSeconds(1.0 / (DesmaioRegen(from)));
            }
        }

        public static double HitPointRegen(Mobile from)
        {
            double points = AosAttributes.GetValue(from, AosAttribute.RegenHits);

            if (from is BaseCreature)
                points += ((BaseCreature)from).DefaultHitsRegen;

            if (Core.ML && from is PlayerMobile && from.Race == Race.Human)	//Is this affected by the cap?
                points += 2;

            if (points < 0)
                points = 0;

            if (Core.ML && from is PlayerMobile)	//does racial bonus go before/after?
                points = Math.Min(points, 18);

            if (CheckTransform(from, typeof(HorrificBeastSpell)))
                points += 20;

            if (CheckAnimal(from, typeof(Dog)) || CheckAnimal(from, typeof(Cat)))
                points += from.Skills[SkillName.Ninjitsu].Fixed / 30;

            // Skill Masteries - goes after cap
            points += RampageSpell.GetBonus(from, RampageSpell.BonusType.HitPointRegen);
            points += CombatTrainingSpell.RegenBonus(from);
            points += BarrabHemolymphConcentrate.HPRegenBonus(from);

            if (Core.AOS)
                foreach (RegenBonusHandler handler in HitsBonusHandlers)
                    points += handler(from);

            return points;
        }

        public static double StamRegen(Mobile from)
        {
            double points = AosAttributes.GetValue(from, AosAttribute.RegenStam);

            if (from is BaseCreature)
                points += ((BaseCreature)from).DefaultStamRegen;

            if (CheckTransform(from, typeof(VampiricEmbraceSpell)))
                points += 15;

            if (CheckAnimal(from, typeof(Kirin)))
                points += 20;

            if (Core.ML && from is PlayerMobile)
                points = Math.Min(points, 24);

            // Skill Masteries - goes after cap
            points += RampageSpell.GetBonus(from, RampageSpell.BonusType.StamRegen);

            if (points < -1)
                points = -1;

            if (Core.AOS)
                foreach (RegenBonusHandler handler in StamBonusHandlers)
                    points += handler(from);

            return points;
        }

        public static double ManaRegen(Mobile from)
        {
            double points = AosAttributes.GetValue(from, AosAttribute.RegenMana);

            if (from is BaseCreature)
                points += ((BaseCreature)from).DefaultManaRegen;

            if (CheckTransform(from, typeof(VampiricEmbraceSpell)))
                points += 3;
            else if (CheckTransform(from, typeof(LichFormSpell)))
                points += 13;

            if (from is PlayerMobile && from.Race == Race.Gargoyle)
                points += 2;

            if (!Core.ML && from is PlayerMobile)
                points = Math.Min(points, 18);

            foreach (RegenBonusHandler handler in ManaBonusHandlers)
                points += handler(from);

            return points;
        }

        public static double DesmaioRegen(Mobile from)
        {
            //FLS: Adicionar aqui se tiver algum fator que influencie no regen de DP
            return 0.1;
        }

        public static double GetArmorMeditationValue(BaseArmor ar)
        {
            if (ar == null || ar.ArmorAttributes.MageArmor != 0 || ar.Attributes.SpellChanneling != 0)
                return 0.0;

            switch ( ar.MeditationAllowance )
            {
                default:
                case ArmorMeditationAllowance.None:
                    return ar.BaseArmorRatingScaled;
                case ArmorMeditationAllowance.Half:
                    return ar.BaseArmorRatingScaled / 2.0;
                case ArmorMeditationAllowance.All:
                    return 0.0;
            }
        }
    }
}
