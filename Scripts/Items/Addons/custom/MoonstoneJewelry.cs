using System;

namespace Server.Items
{

 public class MoonstoneBracelet : BaseBracelet
    {
        [Constructable]
        public MoonstoneBracelet()
            : base(0x1F06)
        {
            this.Name = "Moonstone Bracelet";
            this.Weight = 0.1;
        }

        public MoonstoneBracelet(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (this.IsChildOf(from.Backpack))
            {

                if (Map == Map.Trammel)
                {
                    this.Hue = 0x53;
                }

                else if (Map == Map.Felucca)
                {
                    this.Hue = 0x21;
                }

                else 
                {
                    this.Hue = 0x387;
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

 public class MoonstoneEarrings : BaseEarrings
 {
     [Constructable]
     public MoonstoneEarrings()
         : base(0x1F07)
     {
         this.Name = "Moonstone Earrings";
         this.Weight = 0.1;
     }

     public MoonstoneEarrings(Serial serial)
         : base(serial)
     {
     }

     public override void OnDoubleClick(Mobile from)
     {
         if (this.IsChildOf(from.Backpack))
         {

             if (Map == Map.Trammel)
             {
                 this.Hue = 0x53;
             }

             else if (Map == Map.Felucca)
             {
                 this.Hue = 0x21;
             }

             else
             {
                 this.Hue = 0x387;
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

 public class MoonstoneRing : BaseRing
 {
     [Constructable]
     public MoonstoneRing()
         : base(0x1F09)
     {
         this.Name = "Moonstone Ring";
         this.Weight = 0.1;
     }

     public MoonstoneRing(Serial serial)
         : base(serial)
     {
     }

     public override void OnDoubleClick(Mobile from)
     {
         if (this.IsChildOf(from.Backpack))
         {

             if (Map == Map.Trammel)
             {
                 this.Hue = 0x53;
             }

             else if (Map == Map.Felucca)
             {
                 this.Hue = 0x21;
             }

             else
             {
                 this.Hue = 0x387;
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

 public class MoonstoneNecklace : BaseNecklace
 {
     [Constructable]
     public MoonstoneNecklace()
         : base(0x1F08)
     {
         this.Name = "Moonstone Necklace";
         this.Weight = 0.1;
     }

     public MoonstoneNecklace(Serial serial)
         : base(serial)
     {
     }

     public override void OnDoubleClick(Mobile from)
     {
         if (this.IsChildOf(from.Backpack))
         {

             if (Map == Map.Trammel)
             {
                 this.Hue = 0x53;
             }

             else if (Map == Map.Felucca)
             {
                 this.Hue = 0x21;
             }

             else
             {
                 this.Hue = 0x387;
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
 