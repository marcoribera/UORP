using System;
using Server.Gumps;

namespace Server.Items
{
    public class ClerigoDosMortosSpellbook : Spellbook
    {
        [Constructable]
        public ClerigoDosMortosSpellbook()
            : this((ulong)0) //O os bits do numero hexadecimal utilizado no lugar desse 0 (zero) representam os 0 e 1 de ter ou não cada uma das magias do livro.
        {
            Name = "Livro do Servo dos Mortos";
            Hue = 2635;
        }

        [Constructable]
        public ClerigoDosMortosSpellbook(ulong content)
            : base(content, 0x22C5)
        {
            this.Layer = (Core.ML ? Layer.OneHanded : Layer.Invalid);
            Hue = 2635;
        }

        [Constructable]
        public ClerigoDosMortosSpellbook(ulong content, Mobile gifted) : base(content, 0x22C5)
        {
            Hue = 2635;
        }
        public ClerigoDosMortosSpellbook(Serial serial)
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
                return SpellbookType.ClerigoDosMortos;
            }
        }
        public override int BookOffset
        {
            get
            {
                return 950;
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
                from.CloseGump(typeof(ClerigoDosMortosSpellbookGump));
                from.SendGump(new ClerigoDosMortosSpellbookGump(from, this, 1));
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

    public class CompleteClerigoDosMortosSpellbook : ClerigoDosMortosSpellbook
    {
        [Constructable]
        public CompleteClerigoDosMortosSpellbook()
            //: base((ulong)0x1FFFF)
            : base((ulong)0x7FFFF) //aqui é um numero Hexadecimal cujos bits representam se tem ou não uma magia
        {
            Name = "Livro do Servo dos Mortos Completo";
            Hue = 2635;
        }

        public CompleteClerigoDosMortosSpellbook(Serial serial)
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
