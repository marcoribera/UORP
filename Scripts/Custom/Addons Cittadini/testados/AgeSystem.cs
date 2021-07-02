//   ___|========================|___
//   \  |  Written by Felladrin  |  /	This script was released on RunUO Community under the GPL licensing terms.
//    > |      February 2010     | < 
//   /__|========================|__\	[Age System] - Current version: 1.3 (April 7, 2013)

using System;
using Server.Items;
using Server.Prompts;
using Server.Mobiles;
using Server.Targeting;
using Server.Accounting;

namespace Server.Commands
{
	public class AgeCommands
	{
		//===== System Config =====//

		public static bool AutoRenewAgeEnabled = true; // Should the characters get older through time automatically?

		private static TimeSpan AutoRenewDelay = TimeSpan.FromDays( 92 ); // How many Earth Days are equivalent to One Year for characters?

		private static TimeSpan AutoRenewCheck = TimeSpan.FromMinutes( 30 ); // Check for new birthdays every 30 minutes.

		public static bool AgeStatModEnabled = false; // Character's stats (Str,Dex,Int) are affected by the age?
		
		public static double maxBonus = 15;  // What is the bonus when the characters are at their best condition?

		public static double topStrAge = 35; // At what age the characters have the best strength condition?

		public static double topDexAge = 20; // At what age the characters have the best dexterity condition?

		public static double topIntAge = 50; // At what age the characters have the best intelligence condition?
		
		//===== System Config =====//

		public static void Initialize() 
		{
			CommandSystem.Register( "VerificarIdade", AccessLevel.Administrator, new CommandEventHandler( VerifyAge_OnCommand ) );
			CommandSystem.Register( "LimparSistemaDeIdade", AccessLevel.Administrator, new CommandEventHandler( ClearAgeSystem_OnCommand ) );
			CommandSystem.Register( "SetarIdade", AccessLevel.Administrator, new CommandEventHandler( SetAge_OnCommand ) );
			CommandSystem.Register( "NovaIdade", AccessLevel.Administrator, new CommandEventHandler( NewAge_OnCommand ) );
			CommandSystem.Register( "Idade", AccessLevel.GameMaster, new CommandEventHandler( MyAge_OnCommand ) );
			
			if ( AutoRenewAgeEnabled )
			{
				new AutoRenewAgeTimer().Start();
			}

			if ( AgeStatModEnabled )
			{
				foreach ( Mobile pm in World.Mobiles.Values )
				{
					if ( pm is PlayerMobile )
					{
						ApplyAgeStatMod( pm );
					}
				}
			}
		}
		
		public static void ApplyAgeStatMod( Mobile from )
		{
			try
			{
				double age = double.Parse( ((Account)from.Account).GetTag( "Idade de " + (from.RawName) ) );
			
				double strBonus, dexBonus, intBonus;
			
				if ( age < topStrAge )
					strBonus = age / topStrAge * maxBonus;
				else
					strBonus = (topStrAge / age * maxBonus) + (topStrAge / age * maxBonus - maxBonus);
				
				if ( age < topDexAge )
					dexBonus = age / topDexAge * maxBonus;
				else
					dexBonus = (topDexAge / age * maxBonus) + (topDexAge / age * maxBonus - maxBonus);
				
				if ( age < topIntAge )
					intBonus = age / topIntAge * maxBonus ;
				else
					intBonus = (topIntAge / age * maxBonus) + (topIntAge / age * maxBonus - maxBonus);
			
				from.AddStatMod( new StatMod( StatType.Str, "AgeModStr", (int)strBonus, TimeSpan.Zero ) );
				from.AddStatMod( new StatMod( StatType.Dex, "AgeModDex", (int)dexBonus, TimeSpan.Zero ) );
				from.AddStatMod( new StatMod( StatType.Int, "AgeModInt", (int)intBonus, TimeSpan.Zero ) );
			}
			catch
			{
				from.SendMessage( 33, "Sua idade deve ser um número. Fale urgentemente com alguem da staff!" );
			}
		}
		
		[Usage( "VerificarIdade" )]
		[Description( "mostra dados de idade da poupulação." )]
		public static void VerifyAge_OnCommand( CommandEventArgs e )
		{
			int WithoutAge = 0, TotalCounted = 0, SumAges = 0, EldestCharAge = 0, YoungestCharAge = 100, Unreadable = 0;
			
			Console.WriteLine("--- Sistema de Idade ---");
			Console.WriteLine("Os personagens que ainda não configuraram sua idade:");
			
			foreach ( Mobile pm in World.Mobiles.Values )
			{
				if ( pm is PlayerMobile )
				{
					try
					{
						if ( ((Account)pm.Account).GetTag( "Idade de " + (pm.RawName) ) == null || ((Account)pm.Account).GetTag( "Edad de " + (pm.RawName) ) == "")
						{
							if ( pm.Backpack == null )
							{
								pm.SendMessage( 33, "[Aviso] Problemas com sua mochila. Avise a staff urgentemente!" );
								Console.WriteLine("- {0} (Account: {1}) [MISSING BACKPACK]", pm.Name, ((Account)pm.Account).Username);
							}
							else
							{
								Item AgeChangeDeed = pm.Backpack.FindItemByType( typeof( AgeChangeDeed ) );
					
								if ( AgeChangeDeed == null )
								{
									pm.SendMessage( Utility.RandomMinMax(2,600), "Você acabou de receber um pergaminho para escrever a sua idade." );
									pm.AddToBackpack( new AgeChangeDeed() );
								}
								else
								{
									pm.SendMessage( Utility.RandomMinMax(2,600), "Existe um pergaminho para mudar sua idade." );
								}
					
								Console.WriteLine("- {0} (Account: {1})", pm.Name, ((Account)pm.Account).Username);
							}
					
							WithoutAge++;
						}
						else
						{
							int age = int.Parse( ((Account)pm.Account).GetTag( "Edad de " + (pm.RawName) ) );
					
							if ( age > EldestCharAge )
								EldestCharAge = age;
					
							if ( age < YoungestCharAge )
								YoungestCharAge = age;

							SumAges = SumAges + age;

							TotalCounted++;
						}
					}
					catch
					{
						Unreadable++; //The unreadable accounts are ignored to avoid server crash.
					}
				}
			}			
			
			if ( SumAges != 0 )
				e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "[Sistema de Idade]  A população atual de {0} pessoas, tem uma média de idade de {1}. A pessoa mais velha foi {2}, e a mais jovem, {3}.", (TotalCounted + WithoutAge), (SumAges / TotalCounted), EldestCharAge, YoungestCharAge);
			else
				e.Mobile.SendMessage( 33, "[Sistema de Idade] Não registrou sua idade ainda." );
			
			if ( WithoutAge == 0 )
			{
				e.Mobile.SendMessage( 67, "[Sistema de Idade] Todos os personagem registraram corretamente suas idades." );
				Console.WriteLine( "Todos os personagens registraram corretamente a sua idade." );
			}
			else     
			{
				e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "[Sistema de Idade] {0} jogadores ainda não registraram a sua idade. ElesEles receberam uma advertência solicitando para registrarem. Confira no console se eles o fizeram", WithoutAge );
				Console.WriteLine("Total: {0} personagens precisam ajustar suas idades.", WithoutAge);
				if ( Unreadable != 0 )
					Console.WriteLine("Aviso: {0} contas Inelegíveis detectadas.", Unreadable);
			}
			
			Console.WriteLine("--- Sistema de Idade ---");
		}
		
		[Usage( "LimparSistemaDeIdade" )]
		[Description("Remova todas as tags e elementos do sistema de seu servidor. Em seguida, você pode reativar o sistema ou remover o script da pasta ServUO e reiniciar o servidor.")]
		public static void ClearAgeSystem_OnCommand( CommandEventArgs e )
		{
			foreach ( Mobile pm in World.Mobiles.Values )
			{
				if ( pm is PlayerMobile )
				{
					try
					{
						if ( ((Account)pm.Account).GetTag( "Idade de " + (pm.RawName) ) != null )
						{
							((Account)pm.Account).RemoveTag( "Idade de " + (pm.RawName) );
							
							if ( pm.NameMod != null )
								pm.NameMod = null;
							
							pm.RemoveStatMod( "AgeModStr" );
							pm.RemoveStatMod( "AgeModDex" );
							pm.RemoveStatMod( "AgeModInt" );
						}
					}
					catch
					{
					}
				}
			}
			
			System.Collections.ArrayList itemlist = new System.Collections.ArrayList();
			
			foreach ( Item item in World.Items.Values )
			{
				if ( item is AgeChangeDeed || item is RejuvenationPotion )
					itemlist.Add( item );
			}
			
			foreach (Item item in itemlist)
				item.Delete();			
			
			Misc.AutoSave.Save();
			
			e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "[Sistema de Idade] Todas as tags e itens do Age System foram removidos do seu shard.");
			e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "[Sistema de Idade] Digite .VerificarIdade, se você quiser reativar o sistema.");
			e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "[Sistema de Idade] Remova o script da pasta ServUO e reinicie o servidor, se você nunca vai usá-lo novamente.");
		}

		[Usage( "SetarIdade" )]
		[Description("Defina a idade de um personagem para o valor especificado.")]
		public static void SetAge_OnCommand( CommandEventArgs e )
		{ 		
			string NewAgeValue = e.ArgString;
			
			if ( NewAgeValue != null && NewAgeValue.Length >= 1 && System.Text.RegularExpressions.Regex.IsMatch( NewAgeValue, @"^[0-9]+$" ) )
			{
				e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "Selecione o personagem que você gostaria de setar {0} de idade.", NewAgeValue );
				e.Mobile.Target = new SetAgeTarget( NewAgeValue );
			}	
			else
				e.Mobile.SendMessage("Uso: SetarIdade <Um número positivo>");
		}
	
		private class SetAgeTarget : Target
		{
			string ReceivedAge;
		
			public SetAgeTarget( string NewAgeValue ) : base( -1, false, TargetFlags.None )
			{
				ReceivedAge = NewAgeValue;
			}

			protected override void OnTarget( Mobile from, object targeted ) 
			{
				if ( targeted is PlayerMobile )
				{
					((Mobile)targeted).SendMessage( Utility.RandomMinMax(2,600), "Sua idade mudou para {0}. Agora é {1} anos de idade.", from.Name, ReceivedAge );					
					((Account)((Mobile)targeted).Account).SetTag( "Idade de " + (((Mobile)targeted).RawName), ReceivedAge );
					((Account)((Mobile)targeted).Account).SetTag( "ÚltimaIdadeRenovada " + (((Mobile)targeted).RawName), (DateTime.Now).ToString() );
					from.SendMessage( Utility.RandomMinMax(2,600), "Idade de {0} mudou exitosamente.", ((Mobile)targeted).Name );
					if ( AgeStatModEnabled )
					{
						ApplyAgeStatMod( ((Mobile)targeted) );
					}
				}
				else
					from.SendMessage( Utility.RandomMinMax(2,600), "Apenas você pode mudar a idade de personagens.");
			}		
		}
		
		[Usage( "NovaIdade" )]
		[Description("Faz com que todos os personagens fiquem um ano mais velhos.")]
		public static void NewAge_OnCommand( CommandEventArgs e )
		{
			foreach ( Mobile pm in World.Mobiles.Values )
			{
				if ( pm is PlayerMobile )
				{
					try
					{
						if ( ((Account)pm.Account).GetTag( "Idade de" + (pm.RawName) ) == null || ((Account)pm.Account).GetTag( "Idade de " + (pm.RawName) ) == "")
						{
							//Ignore them. To make a check on the server and adjust the characters who have not recorded their age yet use the comand [VerifyAge.
						}
						else
						{
							int age = int.Parse( ((Account)pm.Account).GetTag( "Idade de " + (pm.RawName) ) );
							((Account)pm.Account).SetTag("Idade de" + (pm.RawName), (age + 1).ToString() );

                            pm.SendMessage( Utility.RandomMinMax(2,600), "Parabéns !! Agora você tem {0} anos de idade!", (age + 1) );
							if ( AgeStatModEnabled )
							{
								ApplyAgeStatMod( pm );
							}
						}
					}
					catch
					{
					}
				}
			}
			
			e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "Feito. Todos os personagens estão agora um ano mais velhos. Use o comando .VerificarIdade para ver as estatísticas.");
		}
		
		[Usage( "Idade" )]
		[Description("Diga sua idade e mostre aos outros. (Configure a idade para ser mostrada em seu nome)")]
		public static void MyAge_OnCommand( CommandEventArgs e )
		{
			if ( ((Account)e.Mobile.Account).GetTag( "Idade de " + (e.Mobile.RawName) ) == null || ((Account)e.Mobile.Account).GetTag("Idade de " + (e.Mobile.RawName) ) == "")
			{
				if ( e.Mobile.Backpack == null )
				{
					e.Mobile.SendMessage( 33, "[Aviso] Você não tem uma mochila! Informe a staff com urgência!");
					Console.WriteLine("[Sistema de Idade: Aviso] O personagem '{0}' (Account: {1}) não tem mochila.", e.Mobile.Name, ((Account)e.Mobile.Account).Username);
				}
				else
				{
					Item AgeChangeDeed = e.Mobile.Backpack.FindItemByType( typeof( AgeChangeDeed ) );
					
					if ( AgeChangeDeed == null )
					{
						e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "Você ainda não escolheu sua idade. Use o pergaminho em sua mochila para isso.");
						e.Mobile.AddToBackpack( new AgeChangeDeed() );
					}
					else
					{
						e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "Você ainda não escolheu sua idade. Use o pergaminnho de sua mochila para isso.");
					}
				}
			}
			else
			{
				if ( e.Mobile.NameMod == null )
				{
					e.Mobile.Say( "Tenho {0} anos de idade.", ((Account)e.Mobile.Account).GetTag( "Idade de " + (e.Mobile.RawName) ) );
					e.Mobile.NameMod = e.Mobile.Name + " [Idade: " + ((Account)e.Mobile.Account).GetTag("Idade de " + (e.Mobile.RawName) ) + "]";
					e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "sua idade foi adicionada ao nome.");
				}
				else
				{
					e.Mobile.Whisper( "*{0} anos de idade*", ((Account)e.Mobile.Account).GetTag( "Idade de " + (e.Mobile.RawName) ) );
					e.Mobile.NameMod = null;
					e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "sua idade foi removida do nome..");
				}
			}
		}
		
		public class AutoRenewAgeTimer : Timer
		{
			public AutoRenewAgeTimer() : base( AutoRenewCheck, AutoRenewCheck )
			{
				Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				foreach ( Mobile pm in World.Mobiles.Values )
				{
					if ( pm is PlayerMobile )
					{
						try
						{
							if ( ((Account)pm.Account).GetTag( "Idade de " + (pm.RawName) ) == null || ((Account)pm.Account).GetTag( "Idade de " + (pm.RawName) ) == "" || ((Account)pm.Account).GetTag( "LastRenewAge " + (pm.RawName) ) == null )
							{
								//Ignore them.
							}
							else
							{
								DateTime LastRenew = DateTime.Parse( ((Account)pm.Account).GetTag( "LastRenewAge " + (pm.RawName) ) );
								
								if ( DateTime.Now > (LastRenew + AutoRenewDelay)  )
								{
									int age = int.Parse( ((Account)pm.Account).GetTag( "Idade de " + (pm.RawName) ) );
									((Account)pm.Account).SetTag("Idade de " + (pm.RawName), (age + 1).ToString() );
									((Account)pm.Account).SetTag( "ÚltimaRenovaçãoDeIdade " + (pm.RawName), (DateTime.Now).ToString() );
									pm.SendMessage( Utility.RandomMinMax(2,600), "HOJE É SEU ANIVERSÁRIO! Agora você tem {0} anos idade! PARABÉNS!", (age + 1) );
									if ( AgeStatModEnabled )
									{
										ApplyAgeStatMod( pm );
									}
								}	
							}
						}
						catch
						{
						}
					}
				}
			}
		}
	}
}

namespace Server.Items
{
	public class AgeChangeDeed : Item
	{ 
		[Constructable] 
		public AgeChangeDeed() : base( 0x14F0 )
		{ 
			Name = "Pergaminho de Mudança de Idade";
			Movable = false;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( this.Name );
			list.Add("Quantos anos você tem?");
		}

		public override void OnDoubleClick( Mobile from ) 
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.SendMessage( Utility.RandomMinMax(2,600), "Escreva uma idade para o seu personagem.");
				from.Prompt = new ChooseAge( from );
				this.Delete();
			}
			else
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
		}

		private class ChooseAge : Prompt
		{ 
			public ChooseAge( Mobile from )
			{
			}

			public override void OnResponse( Mobile from, string number ) 
			{
				if ( !(System.Text.RegularExpressions.Regex.IsMatch(number, @"^[0-9]+$")) )
				{
					from.SendMessage( Utility.RandomMinMax(2,600), "Deve ser um número positivo!" );
					from.AddToBackpack( new AgeChangeDeed() );
					return;
				}
				else if ( int.Parse(number) < 18 )
				{
					from.SendMessage( Utility.RandomMinMax(2,600), "Não pode começar antes dos 18 anos!" );
					from.AddToBackpack( new AgeChangeDeed() );
					return;
				}
				else if ( int.Parse(number) > 40 )
				{
					from.SendMessage( Utility.RandomMinMax(2,600), "Não pode começar com mais de 40!" );
					from.AddToBackpack( new AgeChangeDeed() );
					return;
				}
				else
				{
					((Account)from.Account).SetTag( "Idade de " + (from.RawName), number );
					((Account)from.Account).SetTag( "ÚltimaRenovaçãoDeIdade" + (from.RawName), (DateTime.Now).ToString() );
					from.SendMessage( Utility.RandomMinMax(2,600), "sua idade foi estabelecida. Agora você tem {0} anos de idade.", number );
					if ( Server.Commands.AgeCommands.AgeStatModEnabled )
					{
						Server.Commands.AgeCommands.ApplyAgeStatMod( from );
					}
				}	
			}

			public override void OnCancel( Mobile from )
			{
				from.AddToBackpack( new AgeChangeDeed() );
			}
		}
		
		public AgeChangeDeed( Serial serial ) : base( serial )
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

	public class RejuvenationPotion : Item
	{
		[Constructable] 
		public RejuvenationPotion() : base( 0xF0D )
		{
			Name = "Poção do Rejuvenescimento";
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( this.Name );
			list.Add( "Reduz sua idade" );
		}
		
		public override void OnDoubleClick( Mobile from ) 
		{
			if ( ((Account)from.Account).GetTag( "Idade de " + (from.RawName) ) == null || ((Account)from.Account).GetTag("Idade de " + (from.RawName) ) == "")
			{
				from.SendMessage( 33, "Antes de beber a poção você precisa saber sua idade!");
			}
			else
			{
				try
				{
					int age = int.Parse( ((Account)from.Account).GetTag( "Idade de " + (from.RawName) ) );				
				
					if ( age < 23 )
					{
						from.SendMessage( Utility.RandomMinMax(2,600), "Pensando melhor, você decide não beber essa poção, pois ela pode torná-lo tão jovem que você pode perder a razão.");
					}
					else
					{
						((Account)from.Account).SetTag("Idade de " + (from.RawName), (age - Utility.RandomMinMax(1,5)).ToString() );
						this.Delete();
						from.SendMessage( Utility.RandomMinMax(2,600), "Você bebe a poção e se sente mais disposto! Agora você tem {0} anos de idade!", ((Account)from.Account).GetTag( "Idade de " + (from.RawName) ) );
						if ( Server.Commands.AgeCommands.AgeStatModEnabled )
						{
							Server.Commands.AgeCommands.ApplyAgeStatMod( from );
						}
					}
				}
				catch
				{
					from.SendMessage( 33, "Sua idade não é definida como um número! Informe um membro da staff urgentemente!");
				}
			}
		}
		
		public RejuvenationPotion( Serial serial ) : base( serial )
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
