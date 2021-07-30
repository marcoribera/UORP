using System;
 using Server;
 using Server.Items;
 using Server.Mobiles;

namespace Server.Multis
{
     public class GypsyCampGreen : BaseCamp
     {

         [Constructable]
         public GypsyCampGreen() : base( 0x72 )
         {
         }

         public override void AddComponents()
         {

             TreasureLevel2 two = new TreasureLevel2();
             TreasureLevel3 three = new TreasureLevel3();

             CauldronBurningAddon cba = new CauldronBurningAddon();
             CauldronUnlitAddon cua = new CauldronUnlitAddon();

             two.LiftOverride = true;
             three.LiftOverride = true;

             switch (Utility.Random(7))
             {
                 case 0: AddItem(two, -6, 2, 0); break;
                 case 1: AddItem(two, 6, 4, 0); break;
                 case 2: AddItem(two, 0, -2, 0); break;
                 case 3: AddItem(two, 6, 0, 0); break;
                 case 4: AddItem(two, 6, 2, 0); break;
                 case 5: AddItem(two, 6, 4, 0); break;
                 case 6: AddItem(two, -6, 6, 0); break;
             }

             switch (Utility.Random(7))
             {
                 case 0: AddItem(three, 6, -2, 0); break;
                 case 1: AddItem(three, -6, -2, 0); break;
                 case 2: AddItem(three, -2, 0, 0); break;
                 case 3: AddItem(three, 0, 6, 0); break;
                 case 4: AddItem(three, 2, 6, 0); break;
                 case 5: AddItem(three, 4, 6, 0); break;
                 case 6: AddItem(three, -6, 4, 0); break;
             }

             switch (Utility.Random(15))
             {
                 case 0: AddItem(cba, -8, 2, 0); break;
                 case 1: AddItem(cua, 8, 4, 0); break;
                 case 2: AddItem(cba, 0, -8, 0); break;
                 case 3: AddItem(cua, 8, 0, 0); break;
                 case 4: AddItem(cba, 8, 2, 0); break;
                 case 5: AddItem(cua, 6, 8, 0); break;
                 case 6: AddItem(cba, -8, 6, 0); break;
                 case 7: AddItem(cua, -6, 8, 0); break;
                 case 8: AddItem(cba, 8, -2, 0); break;
                 case 9: AddItem(cua, -8, -2, 0); break;
                 case 10: AddItem(cba, -8, 0, 0); break;
                 case 11: AddItem(cua, 0, 8, 0); break;
                 case 12: AddItem(cba, 2, 8, 0); break;
                 case 13: AddItem(cua, 4, 8, 0); break;
                 case 14: AddItem(cba, -8, 4, 0); break;
             }

             AddMobile(new Gypsy(), 15, 0, -2);
             AddMobile(new Gypsy(), 15, 8, 8);
             AddMobile(new Gypsy(), 15, -8, 8);
             AddMobile(new Gypsy(), 15, -8, -8);
             AddMobile(new Gypsy(), 15, 8, -8);
             AddMobile(new Gypsy(), 15, 0, 7);
         }
 
         public GypsyCampGreen( Serial serial ) : base( serial )
         {
         }

         public override void Serialize( GenericWriter writer )
         {
             base.Serialize( writer );

             writer.Write( (int) 0 ); // version
         }

         public override void Deserialize( GenericReader reader )
         {
             base.Deserialize( reader );

             int version = reader.ReadInt();
         }
     }
}