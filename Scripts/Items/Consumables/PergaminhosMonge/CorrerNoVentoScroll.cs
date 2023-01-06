using System;

namespace Server.Items
{
    public class CorrerNoVentoScroll : SpellScroll
    {
        [Constructable]
        public CorrerNoVentoScroll()
            : this(1)
        {
        }

        [Constructable]
        public CorrerNoVentoScroll(int amount)
            : base(861, 0x1F33, amount)
        {
            Hue = 47;
        }
        public override int LabelNumber  //TODO: Adicionar os nomes dos novos itens no cliloc
        {
            get
            {
                if (m_Identified)
                {
                    return 2000000 + 861; //Criar entrada no CLILOC
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
        public CorrerNoVentoScroll(Serial ser)
            : base(ser)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
