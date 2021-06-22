using System; 
using Server.Network; 

namespace Server.Items 
{ 

   [FlipableAttribute( 10590, 10591 )] 
   public class CustomFuton : Item
   { 
      [Constructable] 
      public CustomFuton() : base( 10590 )
      { 
         Weight = 20.0;
      } 

      public CustomFuton( Serial serial ) : base( serial )
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
