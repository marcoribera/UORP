using System;
using Server.Gumps;

namespace Server.Items
{
    public class AlgozSpellbook : Spellbook
    {
        [Constructable]
        public AlgozSpellbook()
            : this((ulong)0)
        {
            Hue = 31;
        }

        [Constructable]
        public AlgozSpellbook(ulong content)
            : base(content, 0x2253)
        {
            this.Layer = (Core.ML ? Layer.OneHanded : Layer.Invalid);
            Hue = 31;
        }

        [Constructable]
        public AlgozSpellbook(ulong content, Mobile gifted) : base(content, 0x2253)
        {
            Hue = 31;
        }
        public AlgozSpellbook(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber
        {
            get
            {
                if (m_Identified)
                {
                    return 2000000 + 1; //TODO: Adicionar os nomes dos novos itens no cliloc
                    /*
                    if (ItemID < 0x4000)
                    {
                        return 1020000 + ItemID;
                    }
                    else
                    {
                        return 1078872 + ItemID;
                    }
                    */
                }
                else
                {
                    return 1038000; // Não Identificado
                }
            }
        }

        public override SpellbookType SpellbookType
        {
            get
            {
                return SpellbookType.Algoz;
            }
        }
        public override int BookOffset
        {
            get
            {
                return 70;
            }
        }
        public override int BookCount
        {
            get
            {
                return 18;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            Container pack = from.Backpack;
            if (Parent == from || (pack != null && Parent == pack))
            {
                from.SendSound(0x55);
                from.CloseGump(typeof(AlgozSpellbookGump));
                from.SendGump(new AlgozSpellbookGump(from, this, 1));
            }
            else from.SendLocalizedMessage(500207); // The spellbook must be in your backpack (and not in a container within) to open.
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0 && Core.ML)
                this.Layer = Layer.OneHanded;
        }
    }

    public class CompleteAlgozSpellbook : AlgozSpellbook
    {
        [Constructable]
        public CompleteAlgozSpellbook()
            : base((ulong)0x1FFFF)
        {
        }

        public CompleteAlgozSpellbook(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}
