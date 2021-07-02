using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using System.Collections.Generic;

namespace Server.SkillHandlers
{
    public class Extracao
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.Extracao].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.Target = new InternalTarget();

            //m.SendLocalizedMessage(503406); //Que arma deseja examinar?
            m.SendMessage("Que material deseja examinar?");
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
                Item item = targeted as Item;

                if (item is Meteorite)
                {
                    if (((Meteorite)item).Polished)
                    {
                        from.SendLocalizedMessage(1158697); // The brilliance of the meteorite shimmers in the light as you rotate it in your hands! Brightly hued veins of exotic minerals reflect against the polished surface. You think to yourself you have never seen anything so full of splendor!
                    }
                    else
                    {
                        from.SendLocalizedMessage(1158696); // The rock seems to be otherwordly. Judging by the pitting and charring, it appears to have crash landed here from the sky! The rock feels surprisingly dense given its size. Perhaps if you polished it with an oil cloth you may discover what is inside...
                    }

                    return;
                }

                if (item is BaseJewel)
                {
                    from.PrivateOverheadMessage(MessageType.Emote, 0x3B2, 1041351, AffixType.Append, "  " + GetPriceFor(item).ToString(), "", from.NetState); // You guess the value of that item at:

                    if (Imbuing.TimesImbued(item) > 0)
                    {
                        from.PrivateOverheadMessage(MessageType.Emote, 0x3B2, 1111877, from.NetState); // You conclude that item cannot be magically unraveled. The magic in that item has been weakened due to either low durability or the imbuing process.
                    }
                    else
                    {
                        int weight = Imbuing.GetTotalWeight(item, -1, false, true);
                        string imbIngred = null;
                        double skill = from.Skills[SkillName.ImbuirMagica].Base;
                        bool badSkill = false;

                        if (!Imbuing.CanUnravelItem(from, item, false))
                        {
                            weight = 0;
                        }

                        if (weight > 0 && weight <= 200)
                        {
                            imbIngred = "Resíduo Mágico";
                        }
                        else if (weight > 200 && weight < 480)
                        {
                            imbIngred = "Essência Encantada";

                            if (skill < 45.0)
                                badSkill = true;
                        }
                        else if (weight >= 480)
                        {
                            imbIngred = "Fragmento de Relíquia";

                            if (skill < 95.0)
                                badSkill = true;
                        }

                        if (imbIngred != null)
                        {
                            if (badSkill)
                            {
                                from.PrivateOverheadMessage(MessageType.Emote, 0x3B2, 1111875, from.NetState); // Your Imbuing skill is not high enough to identify the imbuing ingredient.
                            }
                            else
                            {
                                from.PrivateOverheadMessage(MessageType.Emote, 0x3B2, 1111874, imbIngred, from.NetState); //You conclude that item will magically unravel into: ~1_ingredient~
                            }
                        }
                        else  // Cannot be Unravelled
                        {
                            from.PrivateOverheadMessage(MessageType.Emote, 0x3B2, 1111876, from.NetState); //You conclude that item cannot be magically unraveled. It appears to possess little to no magic.
                        }
                    }
                }
                else
                {
                    from.LocalOverheadMessage(MessageType.Emote, 0x3B2, 1111878); //You conclude that item cannot be magically unraveled.
                }

            }
            public static int GetPriceFor(Item item)
            {
                Type type = item.GetType();

                if (GenericBuyInfo.BuyPrices.ContainsKey(type))
                {
                    return GenericBuyInfo.BuyPrices[item.GetType()] * item.Amount;
                }

                if (TypeCostCache == null)
                    TypeCostCache = new Dictionary<Type, int>();

                if (!TypeCostCache.ContainsKey(type))
                    TypeCostCache[type] = Utility.RandomMinMax(2, 7);

                return TypeCostCache[type];
            }

            public static Dictionary<Type, int> TypeCostCache { get; set; }
        }
    }
}
