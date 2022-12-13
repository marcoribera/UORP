using System;
using Server.Engines.Craft;


namespace Server.Items
{
    public class PenaETinteiro :BaseReagent, ICommodity, ICraftable
    {
        [Constructable]
        public PenaETinteiro()
            : this(1)
        
        {
           
        }

        [Constructable]
        public PenaETinteiro(int amount)
            : base(0x0FBF, amount)
        {
            this.Weight = 1.0;
            this.Stackable = true;
          

            Name = "Pena e Tinteiro";
        }

        public PenaETinteiro(Serial serial)
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
