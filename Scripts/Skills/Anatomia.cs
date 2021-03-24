using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.SkillHandlers
{
    //Marcknight: Feito
    public class Anatomia
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.Anatomia].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.Target = new Anatomia.InternalTarget();

            m.SendLocalizedMessage(500321); // Whom shall I examine? //Qual o alvo da análise?

            return TimeSpan.FromSeconds(1.0);
        }

        private class InternalTarget : Target
        {
            public InternalTarget()
                : base(8, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (from == targeted)
                {
                    from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 500324); // You know yourself quite well enough already. //Seus Status e Skills já lhe dizem o bastante, não acha?
                }
                else if (targeted is Mobile)
                {
                    Mobile targ = (Mobile)targeted;

                    int marginOfError = Math.Max(0, 50 - (int)(from.Skills[SkillName.Anatomia].Value / 2));

                    int str = targ.Str + Utility.RandomMinMax(-marginOfError, +marginOfError);
                    int dex = targ.Dex + Utility.RandomMinMax(-marginOfError, +marginOfError);
                    int stm = ((targ.Stam * 100) / Math.Max(targ.StamMax, 1)) + Utility.RandomMinMax(-marginOfError, +marginOfError);

                    if (targeted is TownCrier || targeted is BaseVendor)
                    {
                        targ.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, String.Format("Parece ter {0} de Str e {1} de Dex.", str, dex), from.NetState);
                        if (from.Skills[SkillName.Anatomia].Base >= 65.0)
                        {
                            targ.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, String.Format("Parece estar com {0}% da disposição máxima.", stm), from.NetState);
                        }
                    }
                    else if (from.CheckTargetSkill(SkillName.Anatomia, targ, 0, 120))
                    {
                        targ.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, String.Format("Parece ter {0} de Str, {1} de Dex.", str, dex), from.NetState);
                        if (from.Skills[SkillName.Anatomia].Base >= 65.0)
                        {
                            targ.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, String.Format("Parece estar com {0}% da disposição máxima.", stm), from.NetState);
                        }
                    }
                    else
                    {
                        targ.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1042666, from.NetState); // You can not quite get a sense of their physical characteristics. //Falhou em estimar as características físicas do alvo.
                    }
                }
                else if (targeted is Item)
                {
                    ((Item)targeted).SendLocalizedMessageTo(from, 500323, ""); // Only living things have anatomies! //Objetos não possuem anatomia.
                }
            }
        }
    }
}
