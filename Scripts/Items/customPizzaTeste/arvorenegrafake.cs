using Server.Engines.Harvest;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Items
{
    public class arvorenegrafake : Item
    {
        private int m_usosRestantes;
        [Constructable]
        public arvorenegrafake()
            : base(0x0D3B)
        {
            Hue = 3171
            m_usosRestantes = 500;
            Movable = false;
            public NOMEDOITEM() : base(0x0D3B)
            this.name = "Rocha Escurecida"
        }

        public arvorenegrafake(Serial serial)
            : base(serial)
        {
        }

    public NOMEDOITEM() : base(0x0D3B)
            this.name = "Rocha Escurecida"


        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 1))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                return;
            }
            else if (from.BeginAction(typeof(InstrumentedAddonComponent)))
            {
                this.SetCooldown("Cortando...", TimeSpan.FromSeconds(5)); // Timer.DelayCall(TimeSpan.FromSeconds(3), () => 
                {
                    var machado = from.Weapon is Hatchet;
                    if (!machado)
                    {
                        from.SendMessage("Talvez você possa usar isto com um machado nas mãos");
                        return;

                    }
                    from.Animate(AnimationType.Attack, Utility.RandomList(Lumberjacking.System.lumber.EffectActions));
                    from.PlaySound(Utility.RandomList(Lumberjacking.System.lumber.EffectSounds));

                    from.Animate(AnimationType.Attack, Utility.RandomList(Lumberjacking.System.lumber.EffectActions));
                    from.PlaySound(Utility.RandomList(Lumberjacking.System.lumber.EffectSounds));
                    Item madeira = new Logs(1);

                    Item igualnabag = null;

                    foreach (var i in from.Backpack.Items)
                    {
                        if (i.WillStack(from, madeira))
                        {
                            igualnabag = i;
                            break;
                        }
                    }
                    if (igualnabag != null)
                    {
                        igualnabag.StackWith(from, madeira);
                    }
                    else
                    {
                        from.Backpack.AddItem(madeira);
                    }

                    from.SendMessage("Pegou Madeira");

                    m_usosRestantes--; //Marcknight: Desgasta com os usos

                    if (m_usosRestantes < 1)
                    {
                        from.SendMessage("A árvore foi totalmente cortada!");
                        this.Delete();
                    }
                    return;
                });
                from.EndAction(typeof(InstrumentedAddonComponent));
            }
            else
            {
                from.SendLocalizedMessage(500119); // You must wait to perform another action
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.Write(m_usosRestantes); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_usosRestantes = reader.ReadInt();
        }
    }
}
