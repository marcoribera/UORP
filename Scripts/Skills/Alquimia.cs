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
                        from.SendMessage("Você identificou a poção");
                    }
                    else
                    {
                        from.SendLocalizedMessage(500353); // You are not certain... //Você não tem certeza...
                    }
                }
                else if (targeted is BaseArmor)
                {
                    from.SendLocalizedMessage(503407); //Use Conhecimento Armaduras para analisar armaduras.
                }
                else if (targeted is BaseWeapon)
                {
                    from.SendLocalizedMessage(503410); //Use Conhecimento Armas para analisar armas.
                }
                //TODO : colocar Erudicao pra tratar depois aqui;
                else
                {
                    from.SendMessage("Isso não é uma poção");
                }
            }
        }
    }
}
