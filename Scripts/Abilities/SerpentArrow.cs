using System;

namespace Server.Items
{
    public class SerpentArrow : WeaponAbility
    {
        public SerpentArrow()
        {
        }

        public override int BaseMana
        {
            get
            {
                return 25;
            }
        }

        public override SkillName GetSecondarySkill(Mobile from)
        {
            return SkillName.Envenenamento;
        }

        public override void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            if (!this.Validate(attacker) || !this.CheckMana(attacker, true))
                return;

            ClearCurrentAbility(attacker);

			defender.SendLocalizedMessage(1112369); // 	You have been poisoned by a lethal arrow!

            int level;

            if (Core.AOS)
            {
                if (attacker.InRange(defender, 2))
                {
                    int total = (attacker.Skills.Envenenamento.Fixed) / 2;

                    if (total >= 1000)
                        level = 3;
                    else if (total > 850)
                        level = 2;
                    else if (total > 650)
                        level = 1;
                    else
                        level = 0;
                }
                else
                {
                    level = 0;
                }
            }
            else
            {
                double total = attacker.Skills[SkillName.Envenenamento].Value;

                double dist = attacker.GetDistanceToSqrt(defender);

                if (dist >= 3.0)
                    total -= (dist - 3.0) * 10.0;

                if (total >= 200.0 && 1 > Utility.Random(10))
                    level = 3;
                else if (total > (Core.AOS ? 170.1 : 170.0))
                    level = 2;
                else if (total > (Core.AOS ? 130.1 : 130.0))
                    level = 1;
                else
                    level = 0;
            }

            defender.ApplyPoison(attacker, Poison.GetPoison(level));

            defender.FixedParticles(0x374A, 10, 15, 5021, EffectLayer.Waist);
            defender.PlaySound(0x474);
        }
    }
}
