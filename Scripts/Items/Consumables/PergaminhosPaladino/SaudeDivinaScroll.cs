using System;

namespace Server.Items
{
    public class SaudeDivinaScroll : SpellScroll
    { //Encontrar um Gráfico diferente pra o scroll de Algoz
        [Constructable]
        public SaudeDivinaScroll()
            : this(1)
        {
        }

        [Constructable]
        public SaudeDivinaScroll(int amount)
            : base(814, 0x1F30, amount)
        {
            Name = "Saude Divina";
            Movable = true;
            Hue = 1719;
        }

        public SaudeDivinaScroll(Serial serial)
            : base(serial)
        {
        }
        public override int LabelNumber  //TODO: Adicionar os nomes dos novos itens no cliloc
        {
            get
            {
                if (m_Identified)
                {
                    return 2000000 + 814; //Criar entrada no CLILOC
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
