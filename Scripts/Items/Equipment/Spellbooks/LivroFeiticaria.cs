using System;

namespace Server.Items
{
    public class LivroFeiticaria : Spellbook
    {
        [Constructable]
        public LivroFeiticaria()
            : this((ulong)0)
        {
        }

        [Constructable]
        public LivroFeiticaria(ulong content)
            : base(content, 0xEFA) //Livro de magia Padrão //desenho do spellweaving book:  0x2D50
        {
            this.Hue = 0x8A2;

            this.Layer = Layer.OneHanded;
            this.Name = "Livro de Feitiçaria";
        }

        public LivroFeiticaria(Serial serial)
            : base(serial)
        {
            this.Name = "Livro de Feitiçaria";
        }

        public override SpellbookType SpellbookType
        {
            get
            {
                return SpellbookType.Feiticaria;
            }
        }
        public override int BookOffset
        {
            get
            {
                return 420;
            }
        }
        public override int BookCount
        {
            get
            {
                return 1;
            }
        }
        public override void OnDoubleClick(Mobile from)
        {
            Console.WriteLine((int)SpellbookType);
            base.OnDoubleClick(from);
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
