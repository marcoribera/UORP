using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Mobiles.Custom.MiniBosses
{
    public class Teia : Item
    {
        Mobile preso;

        [Constructable]
        public Teia(Mobile preso) : base(0x10D3)
        {
            this.preso = preso;
        }

        public Teia(Serial s) : base(s) { }

        public override void Serialize(GenericWriter writer)
        {
            { }
        }

        public override void Deserialize(GenericReader writer)
        {
            { }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from != preso)
            {
                preso.OverheadMessage("* foi solto *");
                from.OverheadMessage("* cortou a teia *");
                //from.PlayAttackAnimation();
                //preso.PlayHurtSound();
                preso.SendMessage(from.Name + " cortou a teia e lhe liberou");
                from.SendMessage("Voce cortou a teia, liberando " + preso.Name);
                this.Delete();
            }
        }
    }
}
