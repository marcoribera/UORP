using System;

namespace Server.Items
{
    public class EmbasbacarScroll : SpellScroll
    { //Encontrar um Gráfico diferente pra o scroll de Algoz
        [Constructable]
        public EmbasbacarScroll()
            : this(1)
        {
        }

        [Constructable]
        public EmbasbacarScroll(int amount)
            : base(70, 0x1F30, amount)
        {
        }

        public EmbasbacarScroll(Serial serial)
            : base(serial)
        {
        }
        public override int LabelNumber  //TODO: Adicionar os nomes dos novos itens no cliloc
        {
            get
            {
                if (m_Identified)
                {
                    return 2000000 + 70; //Criar entrada no CLILOC
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
