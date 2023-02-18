using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;
using System.Collections;

namespace Server.Spells.Bardo
{
	public class InsultosSpell : BardoSpell
    {
	


        private static SpellInfo m_Info = new SpellInfo(
            "Insultos", "",
            -1,
            0
        );


        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Second;
            }
        }
public override bool CheckCast()
        {
            // Check for a musical instrument in the player's backpack
            if (!CheckInstrument())
            {
                Caster.SendMessage("Você precisa ter um instrumento musical na sua mochila para canalizar essa magia.");
                return false;
            }


            return base.CheckCast();
        }


 private bool CheckInstrument()
        {
            return Caster.Backpack.FindItemByType(typeof(BaseInstrument)) != null;
        }


        private BaseInstrument GetInstrument()
        {
            return Caster.Backpack.FindItemByType(typeof(BaseInstrument)) as BaseInstrument;
        }

        public override double RequiredSkill
        {
            get
            {
                return 20.0;
            }
        }
        public InsultosSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public static bool HasEffect( Mobile m )
		{
			return ( m_Table[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			Timer t = (Timer)m_Table[m];

			if ( t != null )
			{
				t.Stop();
				m_Table.Remove( m );
			}
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				int TotalLoss = Server.Spells.Bardo.BardoSpell.Buff( Caster, "range" )+1;
				int TotalTime = (int)(Server.Spells.Bardo.BardoSpell.Buff( Caster, "time" )/2)+1;

				Timer t = new InternalTimer( m, TotalLoss, TotalTime );

				m_Table[m] = t;

				t.Start();

				Caster.Say( GetInsult() );

				switch ( Utility.Random( 3 ))
				{
					case 0: Caster.PlaySound( Caster.Female ? 794 : 1066 ); break;
					case 1: Caster.PlaySound( Caster.Female ? 801 : 1073 ); break;
					case 2: Caster.PlaySound( 792 ); break;
				};

				if ( Utility.RandomBool() ){ Effects.PlaySound( m.Location, m.Map, m.Female ? 0x31B : 0x42B ); m.Say( "*gemidos*" ); }
				else { Effects.PlaySound( m.Location, m.Map, m.Female ? 0x338 : 0x44A ); m.Say( "*rosna*" ); }

				m.SendMessage("Você foi bastante insultado!");
			}

			FinishSequence();
		}

		public static string GetInsult()
		{
			string str = "Um goblin com uma mão pregada em uma árvore seria uma ameaça maior do que você!";
			switch( Utility.RandomMinMax( 1, 100 ) )
			{
				case 1: str = "Um goblin com uma mão pregada em uma árvore seria uma ameaça maior do que você"; break;
				case 2: str = "Um gato molhado é mais forte do que você!"; break;
				case 3: str = "A magia da amizade animal era a única maneira de seus pais conseguirem que os filhotes da casa brincassem com você, sem medo!"; break;
				case 4: str = "Você é o cruzamento de um orc com um porco? Ah, claro que não, algumas coisas que você faz, nem um porco faria!"; break;
				case 5: str = "Você é sempre estúpido ou hoje você está fazendo um esforço especial!"; break;
				case 6: str = "Olhando para você, agora eu sei o que você ganha quando raspa o fundo do barril!"; break;
				case 7: str = "Pelos deuses você é feio! Aposto que seu pai se arrepende de ter conhecido sua mãe!"; break;
				case 8: str = "Você poderia chamar seu marido? Eu não gosto de brigar com mulheres feias!"; break;
				case 9: str = "Sua mãe tem que lançar um feitiço de escuridão para te alimentar!"; break;
				case 10: str = "Eu tinha ouvido que você era mais forte do que isso?"; break;
				case 11: str = "Você mora num chiqueiro?Volta logo, antes que o fazendeiro perceba que você sumiu!"; break;
				case 12: str = "Você se parece com algo que vi no chão do estábulo!"; break;
				case 13: str = "Você já viu uma pilha de esterco? Não? Então talvez é melhor você se olhar no espelho!"; break;
				case 14: str = "Mesmo ghouls não tocariam em algo tão nojento quanto você!"; break;
				case 15: str = "Ei, você já foi confundido com um verme de carcaça?"; break;
                case 16: str = "Ei, seu monte de esterco cheio de varíola, aposto que nem mesmo um vampiro faminto chegaria perto de você!"; break;
                case 17: str = "Como se sente em não ser digno de ninguém lançar um feitiço decente em você!"; break;
                case 18: str = "Posso dizer que sua reserva de coragem é alimentada pelo afluente que desce pela sua perna!"; break;
                case 19: str = "Eu poderia dizer que você é tão feio quanto um ogro, mas isso seria um insulto aos ogros!"; break;
                case 20: str = "Não sei se devo usar feitiço encantar pessoa ou encantar monstro!"; break;
                case 21: str = "Eu ouvi dizer o que aconteceu com sua mãe, realmente não é todo dia que o reflexo do espelho te mata!"; break;
                case 22: str = "Juro, se você fosse pior nisso, estaria fazendo todo meu trabalho!"; break;
                case 23: str = "Eu ia lançar a magia de ler mentes, mas acho que não vou encontrar nada ai dentro!"; break;
                case 24: str = "Eu estava pensando em conjurar som da burrada, mas duvido que funcione em você!"; break;
                case 25: str = "Eu queria saber o que você é, você é gordo o suficiente para ser um ogro, mas nunca vi um ogro tão feio antes!"; break;
                case 26: str = "Gostaria de ainda ter aquele feitiço de cegueira, assim não teria que aguentar mais a sua cara!"; break;
                case 27: str = "Gostaria de contar para sua mãe sobre sua morte, mas não falo goblin!"; break;
                case 28: str = "Eu tentaria insultar seu pai, mas você provavelmente foi confundido com um orc e foi deserdado!"; break;
                case 29: str = "Eu sacaria minha espada, mas não gostaria de assustar ela com a sua feiura!"; break;
                case 30: str = "Eu insultaria seus pais, mas você provavelmente nem sabe quem são eles!"; break;
                case 31: str = "Eu gostaria de deixar você com um pensamento...mas não sei se você entenderia!"; break;
                case 32: str = "Gostaria de ver as coisas do seu ponto de vista, mas não consigo ser tão burro assim!"; break;
                case 33: str = "Eu diria que você é um oponente digno, mas uma vez lutei contra um coelho empunhando um dente-de-leão!"; break;
                case 34: str = "Se eu fosse você, pegaria seu ouro de volta por aquele feitiço remover maldição!"; break;
                case 35: str = "Se a ignorância é uma bênção, você deve ser o mais feliz do mundo!"; break;
                case 36: str = "Se essa luta ficar mais difícil, vou ter que tentar!"; break;
                case 37: str = "Se sua mente explodisse, nem seu cabelo iria bagunçar!"; break;
                case 38: str = "Fico feliz que você seja alto...Significa que há mais de você para eu poder desprezar!"; break;
                case 39: str = "Dá dor de cabeça só de tentar pensar no seu nível!"; break;
                case 40: str = "Já ouvi falar de cabras com melhores habilidades de luta do que você!"; break;
                case 41: str = "Já vi pássaros mais ameaçadores!"; break;
                case 42: str = "Nenhum tesouro vale a pena, se eu tiver que olhar para você!"; break;
                case 43: str = "Não admira que você esteja se escondendo atrás de uma capa, eu também me esconderia com uma cara dessas!"; break;
                case 44: str = "Olha, eu te valorizo. Você está realmente tentando lutar!"; break;
                case 45: str = "Achei que os trogloditas cheiravam mal!"; break;
                case 46: str = "Por que você não me dá sua arma para que eu possa me acertar com ela, porque isso seria mais eficaz do que você tentar!"; break;
                case 47: str = "Um ogro soprou em mim ou é você?"; break;
                case 48: str = "Um dia vou fazer uma história dessa luta. Diga-me seu nome, espero que rime com horrivelmente massacrado!"; break;
                case 49: str = "Argh! Você acabou de lançar nuvem fedorenta ou sempre cheira assim!"; break;
                case 50: str = "Sua mãe nunca te ensinou a lutar?"; break;
                case 51: str = "Gosto de como você finge lutar!"; break;
                case 52: str = "Algum dia você irá longe e espero estar aqui!"; break;
                case 53: str = "Algum dia você encontrará um doppelganger de si mesmo e ficará desapontado!"; break;
                case 54: str = "Em algum lugar, você está privando uma vila de seu idiota!"; break;
                case 55: str = "Você parece fofinho ou está tentando ser ameaçador?"; break;
                case 56: str = "Diga-me, você fugiu de seus pais ou eles fugiram de você!"; break;
                case 57: str = "Não há um olho de baholder em que você é bonito!"; break;
                case 58: str = "Dizem que toda rosa tem seu espinho, não é, botão de ouro!"; break;
                case 59: str = "Ugh. O que é isso na sua cara? Oh... é só a sua cara!"; break;
                case 60: str = "Muito impressionante, acho que vou contratá-lo para um show de marionetes!"; break;
                case 61: str = "Quando os deuses estavam distribuindo rostos feios, você era o primeiro da fila?"; break;
                case 62: str = "Espera, espera, só preciso perguntar, o que você... ah, esquece, você é feio demais!"; break;
                case 63: str = "Bem, meu tempo de não te levar a sério está chegando ao meio!"; break;
                case 64: str = "Bem...eu já encontrei pães mais crocantes!"; break;
                case 65: str = "Você já foi atingido por um elemental de ácido ou sempre pareceu um bife comido pela metade?"; break;
                case 66: str = "O que cheira pior do que um goblin? Ah sim, você!"; break;
                case 67: str = "Que cheiro é esse? Achei que armas de sopro deveriam sair da sua boca!"; break;
                case 68: str = "Qual é a diferença entre você e um coelho doente? O coelho provavelmente poderia me desafiar!"; break;
                case 69: str = "Qual é a diferença entre você e uma árvore? Uma árvore provavelmente poderia se esquivar melhor!"; break;
                case 70: str = "Quando seu deus o criou, ele se esqueceu de adicionar um cérebro?"; break;
                case 71: str = "Conheci alguém que lutou tão bem quanto você! Foi o frango mais gostoso de todos!"; break;
                case 72: str = "Você gostaria que eu removesse essa maldição? Oh desculpe, você acabou de nascer assim!"; break;
                case 73: str = "Nossa, você é tão gordo que acho que alguém atrás de você está ganhando cobertura para esta luta!"; break;
                case 74: str = "Você é uma torta de larvas servida com o bacalhau de um anão!"; break;
                case 75: str = "Você é as fezes que se criam quando a vergonha come demais!"; break;
                case 76: str = "Você é o pior exemplo de sua espécie que eu já encontrei!"; break;
                case 77: str = "Você chama isso de ataque, já vi gatinhos mortos baterem com mais força!"; break;
                case 78: str = "Você sabe que estou bem aqui se quiser tentar me bater!"; break;
                case 79: str = "Você parece uma crosta na verruga de um troll!"; break;
                case 80: str = "Você parece a axila de uma bruxa do pântano com a barba por fazer!"; break;
                case 81: str = "Você se parece com sua mãe, e sua mãe se parece com seu pai!"; break;
                case 82: str = "Você arrancaria as pernas de um idiota da aldeia!"; break;
                case 83: str = "Sua respiração faria um elemental de estrume correr!"; break;
                case 84: str = "Talvez eu tenha que ressuscitá-lo depois disso para que possamos tentar novamente!"; break;
                case 85: str = "Sua mãe é tão feia, que o padre tentou usar banir mortos-vivos nela!"; break;
                case 86: str = "Sua mãe é tão gorda que fazer uma piada aqui diminuiria a gravidade de seu estado!"; break;
                case 87: str = "Sua mãe ocupa mais terreno do que o castelo da capital!"; break;
                case 88: str = "Sua mãe era uma kobold e seu pai cheirava a sabugueiro!"; break;
                case 89: str = "Sua mãe era tão estúpida que os zumbis fizeram um chapéu de burro para ela!"; break;
                case 90: str = "Sua mãe é tão feia que as pessoas se transformam em pedra apenas para o caso de terem um vislumbre de seu rosto!"; break;
                case 91: str = "Sua cara feia é um bom argumento contra ressuscitar os mortos!"; break;
                case 92: str = "Sua própria existência é um insulto a todos!"; break;
                case 93: str = "Você vai fazer uma excelente faixa!"; break;
                case 94: str = "Você é como um dragão, só que não tão forte ou feroz... ou qualquer coisa!"; break;
                case 95: str = "Você parece um gnomo sobre palafitas, muito fofo, mas não está funcionando!"; break;
                case 96: str = "Você é como um macaco treinado, só que sem o treinamento!"; break;
                case 97: str = "Você tem sorte de ter nascido bonita, ao contrário de mim, que nasci para ser uma grande mentirosa!"; break;
                case 98: str = "Você não é um completo idiota...Algumas partes obviamente estão faltando!"; break;
                case 99: str = "Você é tão estúpido, se um devorador de mentes tentasse comer seu cérebro, ele morreria de fome!"; break;
                case 100: str = "Você é o motivo pelo qual os bebês gnomos choram!"; break;



            }

			return str;
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Owner;
			private DateTime m_Expire;
			private double m_Time;
			private int m_Loss;

			public InternalTimer( Mobile owner, int loss, int time ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 1.5 ) )
			{
				m_Time = (double)time;
				m_Loss = loss;
				m_Owner = owner;
				m_Expire = DateTime.UtcNow + TimeSpan.FromSeconds( m_Time );
			}

			protected override void OnTick()
			{
				if ( !m_Owner.CheckAlive() || DateTime.UtcNow >= m_Expire )
				{
					Stop();
					m_Table.Remove( m_Owner );
					m_Owner.SendMessage("O insulto está passando.");
				}
				else if ( m_Owner.Mana < m_Loss )
				{
					m_Owner.Mana = 0;
				}
				else
				{
					m_Owner.Mana -= m_Loss;
				}
			}
		}

		private class InternalTarget : Target
		{
			private InsultosSpell m_Owner;

			public InternalTarget(InsultosSpell owner ) : base( 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
