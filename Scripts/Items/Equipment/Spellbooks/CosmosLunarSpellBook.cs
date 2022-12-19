using System;
using Server.Gumps;

namespace Server.Items
{
    public class CosmosLunarSpellbook : Spellbook
    {
        [Constructable]
        public CosmosLunarSpellbook()
            : this((ulong)0) //O os bits do numero hexadecimal utilizado no lugar desse 0 (zero) representam os 0 e 1 de ter ou não cada uma das magias do livro.
        {
            Name = "Livro do Cosmos Lunar";
            Hue = 206;
        }

        [Constructable]
        public CosmosLunarSpellbook(ulong content)
            : base(content, 0x42BF)
        {
            this.Layer = (Core.ML ? Layer.OneHanded : Layer.Invalid);
            Hue = 206;
        }

        [Constructable]
        public CosmosLunarSpellbook(ulong content, Mobile gifted) : base(content, 0x42BF)
        {
            Hue = 206;
        }
        public CosmosLunarSpellbook(Serial serial)
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
                return SpellbookType.CosmosLunar;
            }
        }
        public override int BookOffset
        {
            get
            {
                return 771;
            }
        }
        public override int BookCount
        {
            get
            {
                return 8;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            Container pack = from.Backpack;
            if (Parent == from || (pack != null && Parent == pack))
            {
                from.SendSound(0x55);
                from.CloseGump(typeof(CosmosLunarSpellbookGump));
                from.SendGump(new CosmosLunarSpellbookGump(from, this, 1));
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

    public class CompleteCosmosLunarSpellbook : CosmosLunarSpellbook
    {
        [Constructable]
        public CompleteCosmosLunarSpellbook()
            //: base((ulong)0x1FFFF)
            : base((ulong)0x1FFFF) //aqui é um numero Hexadecimal cujos bits representam se tem ou não uma magia
        {
            Name = "Livro do Cosmos Lunar Completo";
            Hue = 206;
        }

        public CompleteCosmosLunarSpellbook(Serial serial)
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
