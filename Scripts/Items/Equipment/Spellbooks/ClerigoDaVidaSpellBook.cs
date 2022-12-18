using System;
using Server.Gumps;

namespace Server.Items
{
    public class ClerigoDaVidaSpellbook : Spellbook
    {
        [Constructable]
        public ClerigoDaVidaSpellbook()
            : this((ulong)0) //O os bits do numero hexadecimal utilizado no lugar desse 0 (zero) representam os 0 e 1 de ter ou não cada uma das magias do livro.
        {
            Name = "Livro do Servo da Vida";
            Hue = 1072;
        }

        [Constructable]
        public ClerigoDaVidaSpellbook(ulong content)
            : base(content, 0x22C5)
        {
            this.Layer = (Core.ML ? Layer.OneHanded : Layer.Invalid);
            Hue = 1072;
        }

        [Constructable]
        public ClerigoDaVidaSpellbook(ulong content, Mobile gifted) : base(content, 0x22C5)
        {
            Hue = 1072;
        }
        public ClerigoDaVidaSpellbook(Serial serial)
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
                return SpellbookType.ClerigoDaVida;
            }
        }
        public override int BookOffset
        {
            get
            {
                return 900;
            }
        }
        public override int BookCount
        {
            get
            {
                return 19;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            Container pack = from.Backpack;
            if (Parent == from || (pack != null && Parent == pack))
            {
                from.SendSound(0x55);
                from.CloseGump(typeof(ClerigoDaVidaSpellbookGump));
                from.SendGump(new ClerigoDaVidaSpellbookGump(from, this, 1));
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

    public class CompleteClerigoDaVidaSpellbook : ClerigoDaVidaSpellbook
    {
        [Constructable]
        public CompleteClerigoDaVidaSpellbook()
            //: base((ulong)0x1FFFF)
            : base((ulong)0x7FFFF) //aqui é um numero Hexadecimal cujos bits representam se tem ou não uma magia
        {
            Name = "Livro do Servo da Vida Completo";
            Hue = 1072;
        }

        public CompleteClerigoDaVidaSpellbook(Serial serial)
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
