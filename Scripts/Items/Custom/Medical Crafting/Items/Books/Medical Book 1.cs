using System;
using Server;

namespace Server.Items
{

	public class MedicalBook1 : BaseBook
	{
		private const string TITLE = "Medical Book 1";
		private const string AUTHOR = null;
		private const int PAGES = 5;
		private const bool WRITABLE = false;
		private const int STYLE = 0xFF1;
		// books: Brown 0xFEF, Tan 0xFF0, Red 0xFF1, Blue 0xFF2, 
		// OpenSmall 0xFF3, Open 0xFF4, OpenOld 0xFBD, 0xFBE
		// or use Utility.RandomList( 0xFEF, 0xFF0, 0xFF1, 0xFF2 )
		// in place of "STYLE" in the constructor

		[Constructable]
		public MedicalBook1() : base( STYLE, TITLE, AUTHOR, PAGES, WRITABLE )
		{
			// NOTE: There are 8 lines per page and
			// approx 22 to 24 characters per line!
			//  0----+----1----+----2----+
			int cnt = 0;
			string[] lines;
			lines = new string[]
			{
				
			};
			
		}

		
		public MedicalBook1( Serial serial ) : base( serial )
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}
	}

}


		
	
