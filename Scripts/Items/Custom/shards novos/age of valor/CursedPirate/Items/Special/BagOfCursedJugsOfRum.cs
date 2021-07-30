/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\engines\CursedPirate\Items\Special\BagOfCursedJugsOfRum.cs
Lines of code: 32
***********************************************************/


using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BagOfCursedJugsOfRum : Bag
	{
		[Constructable]
		public BagOfCursedJugsOfRum() : this( 50 )
		{
		}

		[Constructable]
		public BagOfCursedJugsOfRum( int amount )
		{
			for ( int i = 0; i < amount; ++i )
				DropItem(new CursedJugOfRum());
		}

		public BagOfCursedJugsOfRum( Serial serial ) : base( serial )
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