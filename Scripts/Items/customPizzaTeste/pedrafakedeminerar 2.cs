using Server.Engines.Harvest;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Items
{
    public class pedrafakedeminerar : Item
    {
        private int m_usosRestantes;
        [Constructable]
        public pedrafakedeminerar()
            : base(0x08E3)
        {
            m_usosRestantes = 500;
            Movable = false;
        }

        public pedrafakedeminerar(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 1))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                return;
            }
            else if (from.BeginAction(typeof(InstrumentedAddonComponent)))
            {
                this.SetCooldown("Minerando...", TimeSpan.FromSeconds(5)); // Timer.DelayCall(TimeSpan.FromSeconds(3), () => 
                {
                    var picareta = from.Weapon is Pickaxe;
                    if (!picareta)
                    {
                        from.SendMessage("Talvez você possa usar isto com uma picareta nas mãos");
                        return;

                    }
                    from.Animate(AnimationType.Attack, Utility.RandomList(Mining.System.OreAndStone.EffectActions));
                    from.PlaySound(Utility.RandomList(Mining.System.OreAndStone.EffectSounds));

                    from.Animate(AnimationType.Attack, Utility.RandomList(Mining.System.OreAndStone.EffectActions));
                    from.PlaySound(Utility.RandomList(Mining.System.OreAndStone.EffectSounds));
                    Item minerio = new IronOre(1);

                    Item igualnabag = null;

                    foreach (var i in from.Backpack.Items)
                    {
                        if (i.WillStack(from, minerio))
                        {
                            igualnabag = i;
                            break;
                        }
                    }
                    if (igualnabag != null)
                    {
                        igualnabag.StackWith(from, minerio);
                    }
                    else
                    {
                        from.Backpack.AddItem(minerio);
                    }

                    from.SendMessage("Pegou Ferro");

                    m_usosRestantes--; //Desgasta com os usos

                    if (m_usosRestantes < 1)
                    {
                        from.SendMessage("A pedra estilhaçou!");
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
