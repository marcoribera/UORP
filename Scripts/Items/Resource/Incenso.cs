using System;
//using Server.Engines.Craft;

namespace Server.Items
{
    public class Incenso : BaseReagent, ICommodity//, ICraftable
    {
        [Constructable]
        public Incenso()
            : this(1)
        {
           

        }

        [Constructable]
        public Incenso(int amount)
            : base(0x0E26, amount)
        {
            this.Hue = 981;
            this.Name = "Incenso";
           this.Stackable = true;
            this.Weight = 0.5;
        }

        public Incenso(Serial serial)
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
       // public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        //{
          //  Amount = 1;
            //return 1;
        //}
    }
}
