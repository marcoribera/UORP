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
			int act = Utility.Random( 17 );
			if ( m is PlayerMobile ){ act = Utility.Random( 11 ); }
			switch ( act )
			{
				case 0: m.Say("Por que o rei foi ao dentista? Para colocar uma coroa nos dentes."); break;
				case 1: m.Say("Existem muitos castelos no mundo, mas quem é forte o suficiente para mover um? Qualquer jogador de xadrez!"); break;
				case 2: m.Say("Que rei ficou famoso porque passou tantas noites em sua Távola Redonda escrevendo livros? Rei Autor."); break;
				case 3: m.Say("Como você encontra uma princesa? Você segue as pegadas do príncipe"); break;
				case 4: m.Say("Por que Arthur tinha uma mesa redonda? Para que ninguém pudesse encurralá-lo!"); break;
				case 5: m.Say("Por que o cavaleiro correu gritando por um abridor de latas? Ele tinha uma abelha em sua armadura!"); break;
				case 6: m.Say("Como você chama um gato que voa sobre a parede do castelo? Um gato-a-pult!"); break;
				case 7: m.Say("Como a garota dragão ganhou o concurso de beleza? Ela era a fera do show!"); break;
				case 8: m.Say("Por que o dinossauro viveu mais que o dragão? Porque não fumava!"); break;
				case 9: m.Say("O que o dragão disse quando viu o Cavaleiro? 'Chega de comida enlatada!'"); break;
				case 10: m.Say("O que você faz com um dragão verde? Espere até que amadureça!"); break;
				case 11: m.PlaySound( m.Female ? 780 : 1051 ); m.Say( "*aplaude*" ); break;
				case 12: m.Say( "*se curva*" ); m.Animate( 32, 5, 1, true, false, 0 ); break;
				case 13: m.PlaySound( m.Female ? 794 : 1066 ); m.Say( "*risos*" ); break;
				case 14: m.PlaySound( m.Female ? 801 : 1073 ); m.Say( "*gargalha*" ); break;
				case 15: m.PlaySound( 792 ); m.Say( "*mostra a língua*" ); break;
				case 16: m.PlaySound( m.Female ? 783 : 1054 ); m.Say( "*woohoo!*" ); break;
			};

			if ( act < 17 && Utility.RandomBool() )
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
