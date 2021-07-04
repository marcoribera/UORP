

using System;
using Server;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0xA410, 0xA410)]
    public class AssassinsCowl : BaseHat
    {
        

        [Constructable]
        public AssassinsCowl()
            : base(0xA410)
        {
            Weight = 5.0;
            Name = "Capuz Reforçado";
            Weight = 3.0;
        }

    public override bool OnEquip(Mobile from)
        {
            if (ItemID == 0xA410)
            {
                if (from.AccessLevel == AccessLevel.Counselor)
                    from.NameMod = "Desconhecido";
                else if (from.AccessLevel == AccessLevel.GameMaster)
                    from.NameMod = "Desconhecido";
                else if (from.AccessLevel == AccessLevel.Seer)
                    from.NameMod = "Desconhecido";
                else if (from.AccessLevel == AccessLevel.Administrator)
                    from.NameMod = "Desconhecido";
                else if (from.AccessLevel == AccessLevel.Developer)
                    from.NameMod = "Desconhecido";
                else if (from.AccessLevel == AccessLevel.Owner)
                    from.NameMod = "Desconhecido";
                else
                    from.NameMod = "Desconhecido";

                from.DisplayGuildTitle = false;
                from.Criminal = false;
            }
            return base.OnEquip(from);
        }

        public override void OnRemoved(Object o)
        {
            if (o is Mobile)
            {
                ((Mobile)o).NameMod = null;
            }
            if (o is Mobile && ((Mobile)o).Kills >= 5)
            {
                ((Mobile)o).Criminal = true;
            }
            if (o is Mobile && ((Mobile)o).GuildTitle != null)
            {
                ((Mobile)o).DisplayGuildTitle = true;
            }

            base.OnRemoved(o);
        }

        

        public AssassinsCowl(Serial serial)
            : base(serial)
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
           }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

        }
    }
}
