using System;
using Server.Gumps;

namespace Server.Items
{
    public class PaladinoSpellbook : Spellbook
    {
        [Constructable]
        public PaladinoSpellbook()
            : this((ulong)0)
        {
            Hue = 2038;
        }

        [Constructable]
        public PaladinoSpellbook(ulong content)
            : base(content, 0xA92F)
        {
            this.Layer = (Core.ML ? Layer.OneHanded : Layer.Invalid);
            Hue = 2038;
        }

        [Constructable]
        public PaladinoSpellbook(ulong content, Mobile gifted) : base(content, 0xA92F)
        {
            Hue = 2038;
        }
        public PaladinoSpellbook(Serial serial)
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
                return SpellbookType.Paladino;
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
                return 17;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            Container pack = from.Backpack;
            if (Parent == from || (pack != null && Parent == pack))
            {
                from.SendSound(0x55);
                from.CloseGump(typeof(PaladinoSpellbookGump));
                from.SendGump(new PaladinoSpellbookGump(from, this, 1));
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

    public class CompletePaladinoSpellbook : PaladinoSpellbook
    {
        [Constructable]
        public CompletePaladinoSpellbook()
            //: base((ulong)0x1FFFF)
            : base((ulong)0xA92F)
        {
        }

        public CompletePaladinoSpellbook(Serial serial)
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
