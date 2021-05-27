using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.SkillHandlers
{
    //Marcknight: Feito
    public class Alquimia
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.Alquimia].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.Target = new InternalTarget();

            //m.SendLocalizedMessage(503406); //Que arma deseja examinar?
            m.SendMessage("Qual poção você deseja examinar?");
            return TimeSpan.FromSeconds(1.0);
        }

        [PlayerVendorTarget]
        private class InternalTarget : Target
        {
            public InternalTarget()
                : base(2, false, TargetFlags.None)
            {
                this.AllowNonlocal = true;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BasePotion)
                {
                    BasePotion potion = (BasePotion)targeted;
                    if (potion.Identified)
                    {
                        from.SendMessage("Poção já identificada");
                        return;
                    }
                    else if (from.CheckTargetSkill(SkillName.Alquimia, targeted, 0, 100))
                    {
                        potion.Identified = true;
                    }
                    else
                    {
                        from.SendLocalizedMessage(500353); // You are not certain... //Você não tem certeza...
                    }
                }
                else if (targeted is BaseArmor)
                {
                    from.SendLocalizedMessage(503407); //Use Conhecimento Armaduras para analisar armaduras.
                    /*
                    if (from.CheckTargetSkill(SkillName.ConhecimentoArmas, targeted, 0, 100))
                    {
                        BaseArmor arm = (BaseArmor)targeted;

                        if (arm.MaxHitPoints != 0)
                        {
                            int hp = (int)((arm.HitPoints / (double)arm.MaxHitPoints) * 10);

                            if (hp < 0)
                                hp = 0;
                            else if (hp > 9)
                                hp = 9;

                            from.SendLocalizedMessage(1038285 + hp);
                        }

                        from.SendLocalizedMessage(1038295 + (int)Math.Ceiling(Math.Min(arm.ArmorRating, 35) / 5.0));
                        
                        //if ( arm.ArmorRating < 1 )
                        //from.SendLocalizedMessage( 1038295 ); // This armor offers no defense against attackers.
                        //else if ( arm.ArmorRating < 6 )
                        //from.SendLocalizedMessage( 1038296 ); // This armor provides almost no protection.
                        //else if ( arm.ArmorRating < 11 )
                        //from.SendLocalizedMessage( 1038297 ); // This armor provides very little protection.
                        //else if ( arm.ArmorRating < 16 )
                        //from.SendLocalizedMessage( 1038298 ); // This armor offers some protection against blows.
                        //else if ( arm.ArmorRating < 21 )
                        //from.SendLocalizedMessage( 1038299 ); // This armor serves as sturdy protection.
                        //else if ( arm.ArmorRating < 26 )
                        //from.SendLocalizedMessage( 1038300 ); // This armor is a superior defense against attack.
                        //else if ( arm.ArmorRating < 31 )
                        //from.SendLocalizedMessage( 1038301 ); // This armor offers excellent protection.
                        //else
                        //from.SendLocalizedMessage( 1038302 ); // This armor is superbly crafted to provide maximum protection.
                        
                    }
                    else
                    {
                        from.SendLocalizedMessage(500353); // You are not certain...
                    }
                    */
                }
                else
                {
                    from.SendLocalizedMessage(503408); // Isso não é uma arma.
                }
            }
        }
    }
}
