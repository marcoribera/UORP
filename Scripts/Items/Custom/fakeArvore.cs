using Server.Engines.Harvest;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Items
{
    public class fakeArvore : Item
    {
        private int m_usosRestantes;

        [Constructable]
        public fakeArvore()
            : base(0x21AF)
        {
            Hue = 997;
            m_usosRestantes = 500;
            Movable = false;
            Name = "Galhos Escurecidos";
        }

        public fakeArvore(Serial serial)
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
                var hatchet = from.Weapon is Hatchet;
                if (!hatchet)
                {
                    from.SendMessage("Talvez você possa usar isto com um Hatchet nas mãos");
                    from.EndAction(typeof(InstrumentedAddonComponent));
                    return;
                }
                from.Direction = from.GetDirectionTo(this.Location);
                from.Animate(AnimationType.Attack, Utility.RandomList(Lumberjacking.System.Definition.EffectActions));
                from.PlaySound(Utility.RandomList(Lumberjacking.System.Definition.EffectSounds));

                Timer.DelayCall(TimeSpan.FromSeconds(1), () =>
                {
                    if (!from.InRange(GetWorldLocation(), 1))
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                        from.EndAction(typeof(InstrumentedAddonComponent));
                        return;
                       }

                    from.Direction = from.GetDirectionTo(this.Location);
                    from.Animate(AnimationType.Attack, Utility.RandomList(Lumberjacking.System.Definition.EffectActions));
                    from.PlaySound(Utility.RandomList(Lumberjacking.System.Definition.EffectSounds));

                    if (!from.InRange(GetWorldLocation(), 1))
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                        from.EndAction(typeof(InstrumentedAddonComponent));
                        return;
                    }

                    Timer.DelayCall(TimeSpan.FromSeconds(1), () =>
                    {
                        from.Direction = from.GetDirectionTo(this.Location);
                        from.Animate(AnimationType.Attack, Utility.RandomList(Lumberjacking.System.Definition.EffectActions));
                        from.PlaySound(Utility.RandomList(Lumberjacking.System.Definition.EffectSounds));

                        if (!from.InRange(GetWorldLocation(), 1))
                        {
                            from.Direction = from.GetDirectionTo(this.Location);
                            from.Animate(AnimationType.Attack, Utility.RandomList(Lumberjacking.System.Definition.EffectActions));
                            from.PlaySound(Utility.RandomList(Lumberjacking.System.Definition.EffectSounds));
                            return;
                        }
                        Timer.DelayCall(TimeSpan.FromSeconds(1), () =>
                        {
                            Item madeira = new Log(1);

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
                                from.SendMessage("A árvore desintegrou!");
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
