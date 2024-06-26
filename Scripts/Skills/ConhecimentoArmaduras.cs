using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.SkillHandlers
{
    //Marcknight: Feito
    public class ConhecimentoArmaduras
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.ConhecimentoArmaduras].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.Target = new InternalTarget();

            m.SendLocalizedMessage(503409); //Que armadura deseja examinar?

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
                if (targeted is BaseWeapon)
                {
                    from.SendLocalizedMessage(503410); //Use Conhecimento Armas para analisar armas.
                    /*
                    if (from.CheckTargetSkill(SkillName.ConhecimentoArmas, targeted, 0, 100))
                    {
                        BaseWeapon weap = (BaseWeapon)targeted;

                        if (weap.MaxHitPoints != 0)
                        {
                            int hp = (int)((weap.HitPoints / (double)weap.MaxHitPoints) * 10);

                            if (hp < 0)
                                hp = 0;
                            else if (hp > 9)
                                hp = 9;

                            from.SendLocalizedMessage(1038285 + hp);
                        }

                        int damage = (weap.MaxDamage + weap.MinDamage) / 2;
                        int hand = (weap.Layer == Layer.OneHanded ? 0 : 1);

                        if (damage < 3)
                            damage = 0;
                        else
                            damage = (int)Math.Ceiling(Math.Min(damage, 30) / 5.0);

                        //else if ( damage < 6 )
                        //damage = 1;
                        //else if ( damage < 11 )
                        //damage = 2;
                        //else if ( damage < 16 )
                        //damage = 3;
                        //else if ( damage < 21 )
                        //damage = 4;
                        //else if ( damage < 26 )
                        //damage = 5;
                        //else
                        //damage = 6;

                        WeaponType type = weap.Type;

                        if (type == WeaponType.Ranged)
                            from.SendLocalizedMessage(1038224 + (damage * 9));
                        else if (type == WeaponType.Piercing)
                            from.SendLocalizedMessage(1038218 + hand + (damage * 9));
                        else if (type == WeaponType.Slashing)
                            from.SendLocalizedMessage(1038220 + hand + (damage * 9));
                        else if (type == WeaponType.Bashing)
                            from.SendLocalizedMessage(1038222 + hand + (damage * 9));
                        else
                            from.SendLocalizedMessage(1038216 + hand + (damage * 9));

                        if (weap.Poison != null && weap.PoisonCharges > 0)
                            from.SendLocalizedMessage(1038284); // It appears to have poison smeared on it. //Parece ter veneno espalhado no item.
                    }
                    else
                    {
                        from.SendLocalizedMessage(500353); // You are not certain... //Você não tem certeza...
                    }
                    */
                }
                else if (targeted is BaseArmor)
                {
                    BaseArmor arm = (BaseArmor)targeted;

                    if (from.CheckTargetSkill(SkillName.ConhecimentoArmaduras, targeted, 0, 100))
                    {
                        arm.Identified = true;
                        if (arm.MaxHitPoints != 0)
                        {
                            int hp = (int)((arm.HitPoints / (double)arm.MaxHitPoints) * 100);
                            //if (hp < 0)
                            //    hp = 0;
                            //else if (hp > 9)
                            //    hp = 9;

                            //from.SendLocalizedMessage(1038285 + hp);
                            from.SendLocalizedMessage(503412,String.Format("{0}/{1} ({2}%)", arm.HitPoints, arm.MaxHitPoints, hp)); //Durabilidade: 25/100 (25%)
                        }

                        //from.SendLocalizedMessage(1038295 + (int)Math.Ceiling(Math.Min(arm.ArmorRating, 35) / 5.0));
                        
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
                        from.SendLocalizedMessage(500353); // You are not certain... //Você não tem certeza...
                    }
                }
                /*
                else if (targeted is SwampDragon && ((SwampDragon)targeted).HasBarding)
                {
                    //Maracknight: TODO: Passar isso pra outra skill
                    /*
                    SwampDragon pet = (SwampDragon)targeted;

                    if (from.CheckTargetSkill(SkillName.ConhecimentoArmas, targeted, 0, 100))
                    {
                        int perc = (4 * pet.BardingHP) / pet.BardingMaxHP;

                        if (perc < 0)
                            perc = 0;
                        else if (perc > 4)
                            perc = 4;

                        pet.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1053021 - perc, from.NetState);
                    }
                    else
                    {
                        from.SendLocalizedMessage(500353); // You are not certain... //Você não tem certeza...
                    }
                }
                */
                else
                {
                    from.SendLocalizedMessage(503422); // Isso não é uma armadura.
                }
            }
        }
    }
}
