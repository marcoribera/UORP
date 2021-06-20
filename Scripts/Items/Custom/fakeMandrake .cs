using Server.Engines.Harvest;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Items
{
    public class fakeMandrake : Item
    {
        private int m_usosRestantes;

        [Constructable]
        public fakeMandrake()
            : base(0x0CA5)
        {
            Hue = 997;
            m_usosRestantes = 500;
            Movable = false;
            Name = "Plantas Escurecidas";
        }

        public fakeMandrake(Serial serial)
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
                var boneharvester = from.Weapon is Fists;
                if (!boneharvester)
                {
                    from.SendMessage("Talvez você possa usar isto com as mãos livres");
                    from.EndAction(typeof(InstrumentedAddonComponent));
                    return;
                }
                from.Direction = from.GetDirectionTo(this.Location);
                from.Animate(32, 5, 1, true, false, 1); // Bow
                from.PlaySound(0x12d);

                if (!from.InRange(GetWorldLocation(), 1))
                {
                    from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                    from.EndAction(typeof(InstrumentedAddonComponent));
                    return;
                }

                Timer.DelayCall(TimeSpan.FromSeconds(1), () =>
                {
                    from.Direction = from.GetDirectionTo(this.Location);
                    from.Animate(32, 5, 1, true, false, 1); // Bow
                    from.PlaySound(0x12d);


                if (!from.InRange(GetWorldLocation(), 1))
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                        from.EndAction(typeof(InstrumentedAddonComponent));
                        return;
                    }

                    Timer.DelayCall(TimeSpan.FromSeconds(1), () =>
                    {
                        from.Direction = from.GetDirectionTo(this.Location);
                        from.Animate(32, 5, 1, true, false, 0); // Bow
                        from.PlaySound(0x12d);

                if (!from.InRange(GetWorldLocation(), 1))
                        {
                            from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                            from.EndAction(typeof(InstrumentedAddonComponent));
                            return;
                        }
                        Timer.DelayCall(TimeSpan.FromSeconds(1), () =>
                        {

                            Item mandrake = new MandrakeRoot(1);

                            Item igualnabag = null;

                            foreach (var i in from.Backpack.Items)
                            {
                                if (i.WillStack(from, mandrake))
                                {
                                    igualnabag = i;
                                    break;
                                }
                            }
                            if (igualnabag != null)
                            {
                                igualnabag.StackWith(from, mandrake);
                            }
                            else
                            {
                                from.Backpack.AddItem(mandrake);
                            }

                            from.SendMessage("Pegou Mandrake root");

                            m_usosRestantes--; //Marcknight: Desgasta com os usos

                            if (m_usosRestantes < 1)
                            {
                                from.SendMessage("A pedra desintegrou!");
                                this.Delete();
                            }
                            from.EndAction(typeof(InstrumentedAddonComponent));
                            return;
                        });
                    });
                });
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
