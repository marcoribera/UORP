using System;
using Server.Gumps;

namespace Server.Items
{
    public class BardoSpellBook : Spellbook
    {
        [Constructable]
        public BardoSpellBook()
            : this((ulong)0) //O os bits do numero hexadecimal utilizado no lugar desse 0 (zero) representam os 0 e 1 de ter ou não cada uma das magias do livro.
        {
            Name = "Livro Canções do Bardo";
            Hue = 59;
        }

        [Constructable]
        public BardoSpellBook(ulong content)
            : base(content, 0x2252)
        {
            this.Layer = (Core.ML ? Layer.OneHanded : Layer.Invalid);
            Hue = 59;
        }

        [Constructable]
        public BardoSpellBook(ulong content, Mobile gifted) : base(content, 0xA92F)
        {
            Hue = 59;
        }
        public BardoSpellBook(Serial serial)
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
                return SpellbookType.Bardo;
            }
        }
        public override int BookOffset
        {
            get
            {
                return 260;
            }
        }
        public override int BookCount
        {
            get
            {
                return 28;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            Container pack = from.Backpack;
            if (Parent == from || (pack != null && Parent == pack))
            {
                from.SendSound(0x55);
                from.CloseGump(typeof(BardoSpellbookGump));
                from.SendGump(new BardoSpellbookGump(from, this, this.PaginaAtual));
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

    public class CompleteBardoSpellbook : BardoSpellbook
    {
        [Constructable]
        public CompleteBardoSpellbook()
            //: base((ulong)0x1FFFF)
            : base((ulong)0x1FFFF) //aqui é um numero Hexadecimal cujos bits representam se tem ou não uma magia
        {
            Name = "Livro do Bardo Completo";
            Hue = 59;
        }

        public CompleteBardoSpellbook(Serial serial)
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
