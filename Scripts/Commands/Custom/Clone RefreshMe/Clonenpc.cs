using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a clone corpse" )]
	public class Cloner : BaseCreature
	{
 private DateTime m_ExpireTime;
 
 [CommandProperty( AccessLevel.GameMaster )]
		public DateTime ExpireTime
		{
			get{ return m_ExpireTime; }
			set{ m_ExpireTime = value; }
		}
  
		[Constructable]
		public Cloner() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a clone";
			Body = 0xD9;
			//Hue = Utility.RandomAnimalHue();
			//BaseSoundID = 0x85;

			SetStr( 27, 37 );
			SetDex( 28, 43 );
			SetInt( 29, 37 );

			SetHits( 17, 22 );
			SetMana( 0 );

			SetDamage( 4, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10, 15 );

			SetSkill( SkillName.ResistenciaMagica, 22.1, 47.0 );
			SetSkill( SkillName.Anatomia, 19.2, 31.0 );
			SetSkill( SkillName.Briga, 19.2, 31.0 );

			//Fame = 0;
			//Karma = 300;

			VirtualArmor = 12;

			//Tamable = true;
			ControlSlots = 1;
			//MinTameSkill = -15.3;
                        m_ExpireTime = DateTime.Now + TimeSpan.FromMinutes( 10.0 );
		}
  
                    public override void OnThink()
		    {
			bool expired;

			expired = ( DateTime.Now >= m_ExpireTime );

			/*if ( !expired && m_Owner != null )
				expired = m_Owner.Deleted || Map != m_Owner.Map || !InRange( m_Owner, 16 );*/

			if ( expired )
			{
				//PlaySound( GetIdleSound() );
				Delete();
			}
			else
			{
				base.OnThink();
			}
		}
  //####################SPEECH GOES HERE###########################
      public override bool HandlesOnSpeech( Mobile from )
      {
         if ( from.InRange( this.Location, 3 ) )
            return true;

         return base.HandlesOnSpeech( from );
      }
      public override void OnSpeech( SpeechEventArgs args )
      {


	if (  args.Speech.ToLower().IndexOf( "hello" ) >= 0 ||  args.Speech.ToLower().IndexOf( "hi" ) >= 0)
             switch ( Utility.Random( 2 ))
             {
              case 0: Say( String.Format( "Hello" ));  break;
		case 1: Say( String.Format( "sticks tounge out" ));  break;
              		}
        if (  args.Speech.ToLower().IndexOf( "sup" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "nada you." ));  break;
		}

	if (  args.Speech.ToLower().IndexOf( "smile" ) >= 0 )
             switch ( Utility.Random( 2 ))
             {
              case 0: Say( String.Format( "*Smiles sweetly*" ));  break;
		case 1: Say( String.Format( "*giggles*" ));  break;
		}

	if (  args.Speech.ToLower().IndexOf( "grin" ) >= 0 )
             switch ( Utility.Random( 2 ))
             {
              case 0: Say( String.Format( "*Grins*" ));  break;
		case 1: Say( String.Format( "*sticks tounge out*" ));  break;
		}

	if (  args.Speech.ToLower().IndexOf( "spank" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "Have to catch me first you big oaf." ));  break;
		}

       if (  args.Speech.ToLower().IndexOf( "really" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "yep" ));  break;
		}

       if (  args.Speech.ToLower().IndexOf( "stop" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "why?" ));  break;
		}
        if (  args.Speech.ToLower().IndexOf( "yes" ) >= 0 || args.Speech.ToLower().IndexOf( "no" ) >= 0 || args.Speech.ToLower().IndexOf( "maybe" ) >= 0 )
             switch ( Utility.Random( 2 ))
             {
              case 0: Say( String.Format( "are you sure?" ));  break;
              case 1: Say( String.Format( "ok" ));  break;
		}

	if (  args.Speech.ToLower().IndexOf( "?" ) >= 0 )
             switch ( Utility.Random( 3 ))
             {
              case 0: Say( String.Format( "yes" ));  break;
              case 1: Say( String.Format( "no" ));  break;
              case 2: Say( String.Format( "maybe" ));  break;
		}
        if (  args.Speech.ToLower().IndexOf( "naughty" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "*looks innocent*" ));  break;
		}
        if (  args.Speech.ToLower().IndexOf( "huh" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "huh" ));  break;
		}

	if (  args.Speech.ToLower().IndexOf( "bad" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "*looks innocent*" ));  break;
		}
       if (  args.Speech.ToLower().IndexOf( "mock" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "of course" ));  break;
		}

	if (  args.Speech.ToLower().IndexOf( "sweet" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "*grins evily*" ));  break;
		}

        if (  args.Speech.ToLower().IndexOf( "no" ) >= 0 )
             switch ( Utility.Random( 2 ))
             {
              case 0: Say( String.Format( "why" ));  break;
              case 1: Say( String.Format( "are you sure" ));  break;
		}

	if (  args.Speech.ToLower().IndexOf( "drow" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "*eyes widen*" ));  break;
		}
        if (  args.Speech.ToLower().IndexOf( "how old are you" ) >= 0 )
             switch ( Utility.Random( 2 ))
             {
              case 0: Say( String.Format( "im 6" ));  break;
              case 1: Say( String.Format( "im 8" ));  break;
		}

	if (  args.Speech.ToLower().IndexOf( "kill" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "*looks scared*" ));  break;
		}

	if (  args.Speech.ToLower().IndexOf( "*slap*" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "ouch!! *sobs*" ));  break;
		}


	if (  args.Speech.ToLower().IndexOf( "train" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "I can hit harder than you, your a weakling" ));  break;
		}
       if (  args.Speech.ToLower().IndexOf( "why" ) >= 0 )
             switch ( Utility.Random( 2 ))
             {
              case 0: Say( String.Format( "because" ));  break;
              case 1: Say( String.Format( "just because" ));  break;
		}
	if (  args.Speech.ToLower().IndexOf( "work" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "you need to work, you have arms like matchsticks *giggles*" ));  break;
		}

	if (  args.Speech.ToLower().IndexOf( "away" ) >= 0 )
             switch ( Utility.Random( 2 ))
             {
              case 0: Say( String.Format( "gonna make me?" ));  break;
              case 1: Say( String.Format( "your no fun" ));  break;
		}
       if (  args.Speech.ToLower().IndexOf( "whats up" ) >= 0 )
             switch ( Utility.Random( 2 ))
             {
              case 0: Say( String.Format( "nothing really" ));  break;
              case 1: Say( String.Format( "just having a little fun" ));  break;
		}
        if (  args.Speech.ToLower().IndexOf( "hey" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "what do you want" ));  break;
		}

	if (  args.Speech.ToLower().IndexOf( "gold" ) >= 0 )
             switch ( Utility.Random( 2 ))
             {
              case 0: Say( String.Format( "my daddy told me not to take money from strangers *smiles sweetly*" ));  break;
              case 1: Say( String.Format( "oh ill take your gold just drop it on the ground" ));  break;
		}

        if (  args.Speech.ToLower().IndexOf( "play" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "dont wanna play on my own *pouts*" ));  break;
		}


	if (  args.Speech.ToLower().IndexOf( "how are you" ) >= 0 )
             switch ( Utility.Random( 4 ))
             {
              case 0: Say( String.Format( "I'm not supposed to talk to strangers" ));  break;
		case 1: Say( String.Format( "Whats it to you *sticks tounge out*" ));  break;
                 case 2: Say( String.Format( "Im great wanna play?" ));  break;
                  case 3: Say( String.Format( "Tell me how you are first" ));  break;
              	}

        if (  args.Speech.ToLower().IndexOf( "im good" ) >= 0 )
             switch ( Utility.Random( 2 ))
             {
              case 0: Say( String.Format( "good" ));  break;
               case 1: Say( String.Format( "thats great" ));  break;
              	}

	if (  args.Speech.ToLower().IndexOf( "waves" ) >= 0 )
             switch ( Utility.Random( 2 ))
             {
              case 0: Say( String.Format( "*waves*" ));  break;
               case 1: Say( String.Format( "bye bye" ));  break;
              	}

	if (  args.Speech.ToLower().IndexOf( "skill" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "whatcha doing?" ));  break;
              	}

	if (  args.Speech.ToLower().IndexOf( "rude" ) >= 0 )
             switch ( Utility.Random( 3 ))
             {
              case 0: Say( String.Format( "*sticks tounge out*" ));  break;
 		case 1: Say( String.Format( "*giggles*" ));  break;
                 case 2: Say( String.Format( "what exactly are you trying to say?" ));  break;
              	}

	if (  args.Speech.ToLower().IndexOf( "little" ) >= 0 )
             switch ( Utility.Random( 3 ))
             {
              case 0: Say( String.Format( "*sticks tounge out*" ));  break;
 		case 1: Say( String.Format( "*giggles*" ));  break;
		case 2: Say( String.Format( "*smiles sweetly*" ));  break;
              	}


       	if (  args.Speech.ToLower().IndexOf( "goodbye" ) >= 0 )
             switch ( Utility.Random( 1 ))
             {
              case 0: Say( String.Format( "see ya!" ));  break;
              	}}
//####################SPEECH GOES HERE###########################
		//public override int Meat{ get{ return 1; } }
		//public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		//public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }
                public override bool OnBeforeDeath()
                {
                    this.Delete();
                    return base.OnBeforeDeath();
                }
		public Cloner(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
               
	}
}
