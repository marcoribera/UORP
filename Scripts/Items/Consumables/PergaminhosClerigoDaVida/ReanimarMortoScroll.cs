using System;

namespace Server.Items
{
    public class ReanimarMortoScroll : SpellScroll
    { //Encontrar um Gráfico diferente pra o scroll de Algoz
        [Constructable]
        public ReanimarMortoScroll()
            : this(1)
        {
        }

        [Constructable]
        public ReanimarMortoScroll(int amount)
            : base(914, 0x1F30, amount)
        {
            Hue = 1072;
        }

        public ReanimarMortoScroll(Serial serial)
            : base(serial)
        {
        }
        public override int LabelNumber  //TODO: Adicionar os nomes dos novos itens no cliloc
        {
            get
            {
                if (m_Identified)
                {
                    return 2000000 + 914; //Criar entrada no CLILOC
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
