/* Created by Hammerhand*/

using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    public class CustomGenericBuy2Info : GenericBuyInfo
    {
        public CustomGenericBuy2Info(Type type, int price, int amount, int itemID, int hue)
            : this(null, type, price, amount, itemID, hue, null)
        {
        }

        public CustomGenericBuy2Info(string name, Type type, int price, int amount, int itemID, int hue)
            : this(name, type, price, amount, itemID, hue, null)
        {
        }

        public CustomGenericBuy2Info(Type type, int price, int amount, int itemID, int hue, object[] args)
            : this(null, type, price, amount, itemID, hue, args)
        {
        }

        public CustomGenericBuy2Info(string name, Type type, int price, int amount, int itemID, int hue, object[] args)
            : base(name, type, price, amount, itemID, hue, args)
        {
            //amount = 20;

            Type = type;
            Price = price;
            MaxAmount = Amount = amount;
            ItemID = itemID;
            Hue = hue;
            Args = args;

            if (name == null)
                Name = (1020000 + (itemID & 0x3FFF)).ToString();
            else
                Name = name;
        }
    }
}