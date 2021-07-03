//////////////////////////////////////////////////$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
////==========================================////$                                 $
////        Upgraded By: Triple               ////$   Will like to thank for all    $
////==========================================////$   who help me with this upgrade!$
//////////////////////////////////////////////////$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

using System; 
using Server.Items; 

namespace Server.Items 
{ 
   	public class HiryuDust: Item 
   	{ 
		[Constructable]
		public HiryuDust() : this( 1 )
		{
		}

		[Constructable]
		public HiryuDust( int amount ) : base( 0x26B8 )
		{
			Stackable = true;
			Weight = 0.0;
			Amount = amount;
			Name = "Hiryu dust";
			Hue = 1153;
		}

            	public HiryuDust( Serial serial ) : base ( serial ) 
            	{             
           	} 

           	public override void Serialize( GenericWriter writer ) 
           	{ 
              		base.Serialize( writer ); 
              		writer.Write( (int) 0 ); 
           	} 
            
           	public override void Deserialize( GenericReader reader ) 
           	{ 
              		base.Deserialize( reader ); 
              		int version = reader.ReadInt(); 
           	} 
        } 
} 