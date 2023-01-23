using System;

namespace Server.Items
{
    public class SomDaLerdezaScroll : SpellScroll
    { //Encontrar um Gráfico diferente pra o scroll de Algoz
        [Constructable]
        public SomDaLerdezaScroll()
            : this(1)
        {
            
        }

        [Constructable]
        public SomDaLerdezaScroll(int amount)
            : base(281, 0x1F30, amount)
        {
            Name = "Som da Lerdeza";
            Movable = true;
            Hue = 1719;

        }

        public SomDaLerdezaScroll(Serial serial)
            : base(serial)
        {
           
        }
        public override int LabelNumber  //TODO: Adicionar os nomes dos novos itens no cliloc
        {
            get
            {
                if (m_Identified)
                {
                    return 2000000 + 281; //Criar entrada no CLILOC
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
