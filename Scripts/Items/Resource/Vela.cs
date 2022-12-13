using System;
using Server.Engines.Craft;

namespace Server.Items
{
    public class Vela : BaseReagent, ICommodity, ICraftable
    {
        [Constructable]
        public Vela()
            : this(1)
        
        {

          
        }

        [Constructable]
        public Vela(int amount)
            : base(0x1430, amount)
        {
            Stackable = true;
            Weight = 0.5;
            this.Name = "Vela";
        }

        public Vela(Serial serial)
            : base(serial)
        {
        }

        
        TextDefinition ICommodity.Description
        {
            get
            {
                return this.LabelNumber;
            }
        }
        bool ICommodity.IsDeedable
        {
            get
            {
                return true;
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
        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            Amount = 1;
            return 1;
        }
    }
}
