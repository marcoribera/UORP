using System;

namespace Server.Items
{
    public class pedrafakedeminerar : Item
    {
        [Constructable]
        public pedrafakedeminerar()
            : base(0x08E3)
        {
            this.Movable = false;
        }

        public pedrafakedeminerar(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            var picareta = from.Weapon is Pickaxe
               if (!picareta)
            {
                from.SendMessage("Talvez você possa usar isto com uma picareta nas mãos");
                return;

            }
            from.playattackanimantion();
            from.additem(new IronOre(1));
            from.SendMessage("Pegou Ferro")
        }
    }
     
}
