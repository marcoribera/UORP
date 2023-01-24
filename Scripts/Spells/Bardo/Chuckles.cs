using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class ChucklesJester : BaseCreature
	{
		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextTalk && InRange( m, 4 ) && InLOS( m ) )
				{
					DoJokes( this );
					m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( 30 ));
				}
			}
		}

		public static void DoJokes( Mobile m )
		{
			int act = Utility.Random( 28 );
			if ( m is PlayerMobile ){ act = Utility.Random( 22 ); }
			switch ( act )
			{
				case 0: m.Say("Why did the king go to the dentist? To get his teeth crowned."); break;
				case 1: m.Say("When a knight in armor was killed in battle, what sign did they put on his grave? Rust in peace!"); break;
				case 2: m.Say("What do you call a mosquito in a tin suit? A bite in shining armor."); break;
				case 3: m.Say("There are many castles in the world, but who is strong enough to move one? Any chess player"); break;
				case 4: m.Say("What king was famous because he spent so many nights at his Round Table writing books? King Author!"); break;
				case 5: m.Say("How do you find a princess? You follow the foot prince."); break;
				case 6: m.Say("Why were the early days called the dark ages? Because there were so many knights!"); break;
				case 7: m.Say("Why did Arthur have a round table? So no one could corner him!"); break;
				case 8: m.Say("Who invented King Arthur's round table? Sir Cumference!"); break;
				case 9: m.Say("Why did the knight run about shouting for a tin opener? He had a bee in his suit of armor!"); break;
				case 10: m.Say("What was Camelot famous for? It's knight life!"); break;
				case 11: m.Say("What did the toad say when the princess would not kiss him? Warts the matter with you?"); break;
				case 12: m.Say("What do you call the young royal who keeps falling down? Prince Harming!"); break;
				case 13: m.Say("What do you call a cat that flies over the castle wall? A cat-a-pult!"); break;
				case 14: m.Say("What game do the fish play in the moat? Trout or dare!"); break;
				case 15: m.Say("What did the fish say to the other when the horse fell in the moat? See horse!"); break;
				case 16: m.Say("What do you call an angry princess just awakened from a long sleep? Slapping beauty!"); break;
				case 17: m.Say("How did the prince get into the castle when the drawbridge was broken? He used a rowmoat!"); break;
				case 18: m.Say("How did the girl dragon win the beauty contest? She was the beast of the show!"); break;
				case 19: m.Say("Why did the dinosaur live longer than the dragon? Because it didn�t smoke!"); break;
				case 20: m.Say("What did the dragon say when it saw the Knight? 'Not more tinned food!'"); break;
				case 21: m.Say("What do you do with a green dragon? Wait until it ripens!"); break;
				case 22: m.PlaySound( m.Female ? 780 : 1051 ); m.Say( "*claps*" ); break;
				case 23: m.Say( "*bows*" ); m.Animate( 32, 5, 1, true, false, 0 ); break;
				case 24: m.PlaySound( m.Female ? 794 : 1066 ); m.Say( "*giggles*" ); break;
				case 25: m.PlaySound( m.Female ? 801 : 1073 ); m.Say( "*laughs*" ); break;
				case 26: m.PlaySound( 792 ); m.Say( "*sticks out tongue*" ); break;
				case 27: m.PlaySound( m.Female ? 783 : 1054 ); m.Say( "*woohoo!*" ); break;
			};

			if ( act < 22 && Utility.RandomBool() )
			{
				switch ( Utility.Random( 6 ))
				{
					case 0: m.PlaySound( m.Female ? 780 : 1051 ); break;
					case 1: m.Animate( 32, 5, 1, true, false, 0 ); break;
					case 2: m.PlaySound( m.Female ? 794 : 1066 ); break;
					case 3: m.PlaySound( m.Female ? 801 : 1073 ); break;
					case 4: m.PlaySound( 792 ); break;
					case 5: m.PlaySound( m.Female ? 783 : 1054 ); break;
				};
			}

		}

		[Constructable]
		public ChucklesJester() : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			NameHue = 1154;

			Body = 0x190;

			Name = "Chuckles";
			Title = "the Jester";
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();

			SetStr( 100 );
			SetDex( 100 );
			SetInt( 100 );

			SetDamage( 15, 20 );
			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Briga, 100 );
			Karma = 1000;
			VirtualArmor = 30;

			AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
			AddItem( new Shoes( Utility.RandomNeutralHue() ) );
			AddItem( new JesterSuit( Utility.RandomNeutralHue() ) );
			AddItem( new JesterHat( Utility.RandomNeutralHue() ) );

			Utility.AssignRandomHair( this );
		}

		

		

		

		public ChucklesJester( Serial serial ) : base( serial )
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
