using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.SkillHandlers
{
      public class Erudicao
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.Erudicao].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.Target = new InternalTarget();

            //m.SendLocalizedMessage(503406); //Que arma deseja examinar?
            m.SendMessage("O que você deseja examinar?");
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
                if (targeted is SpellScroll)
                {
                    SpellScroll scroll  = (SpellScroll)targeted;
                    if (scroll.Identified)
                    {
                        from.SendMessage("Pergaminho já identificado");
                        return;
                    }
                    else if (from.CheckTargetSkill(SkillName.Erudicao, targeted, 0, 100))
                    {
                        scroll.Identified = true;
                        from.SendMessage("Você identificou esse pergaminho");
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
                    from.SendMessage("Isso não é um pergaminho");
                }
            }
        }
    }
}
