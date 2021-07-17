using System; 
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	public class MonsterContractType
	{
		public static MonsterContractType[] Get = new MonsterContractType[]
		{
			new MonsterContractType (typeof(TLSGoblinArqueiro), 				"Goblin Arqueiro", 	     40),
			new MonsterContractType (typeof(TLSGoblinArqueiro2), 				"Goblin Arqueiro Elite", 40),
			new MonsterContractType (typeof(TLSGoblinMachado), 			        "Goblin",                40),
			new MonsterContractType (typeof(TLSGoblinMontado), 				    "Goblin Montado",        40),
			new MonsterContractType (typeof(DireWolf), 			"Lobo",			40),
			new MonsterContractType (typeof(Panther), 			"Pantera",		40),
			new MonsterContractType (typeof(Orc), 		"Orc",		40),
			new MonsterContractType (typeof(OrcScout), 		"Andarilho Orc",		40),
			new MonsterContractType (typeof(OrcChopper), 		"Lenhador",		40),
			new MonsterContractType (typeof(OrcCaptain), 		"Captão Orc",	40),
			new MonsterContractType (typeof(HeadlessOne), 					"Escravizado",						40),
			new MonsterContractType (typeof(HeadlessMiner), 				"Minerador Escravizado",					40),
			new MonsterContractType (typeof(TLSElementalDeLava), 				"Elemental de Lava",				40),
			new MonsterContractType (typeof(FireAnt), 				"Formigas de Lava",						40),
			new MonsterContractType (typeof(Minotaur), 				"Minotauros",				40),
			new MonsterContractType (typeof(LesserElfBrigand), 				"Elfos Brigand",			40),
			new MonsterContractType (typeof(ElfBrigand), 					"Elfo Salteador",					40),
			new MonsterContractType (typeof(BrownBear), 			"Urso Marrom",		40),
			new MonsterContractType (typeof(Mongbat), 			"Morcego Gigante",			40),
			new MonsterContractType (typeof(EnslavedGrayGoblin), 		"Goblin Lacaio",	40),
			new MonsterContractType (typeof(FrostTroll), 				"Troll Invasor",			40),
			new MonsterContractType (typeof(GreyWolf), 			"Lobo Cinzento",			40),
			new MonsterContractType (typeof(Alligator), 				"Jacare Escamoso",					40),
			new MonsterContractType (typeof(GrayGoblin), 				"Goblin Magricelos",					40)  //Atenção para as virgulas
		};
		
		public static int Random()
		{
			double rarety = Utility.RandomDouble();
			int result = 0;
			for(int i=0;i<10;i++)
			{
				result = Utility.Random(MonsterContractType.Get.Length);
				if(rarety > MonsterContractType.Get[result].Rarety)
					break;
			}
			
			return result;
		}
		
		private Type m_Type;
		public Type Type
		{
			get { return m_Type;}
			set { m_Type = value;}
		}
		
		private string m_Name;
		public string Name
		{
			get { return m_Name;}
			set { m_Name = value;}
		}
		
		private int m_Rarety;
		public int Rarety
		{
			get { return m_Rarety;}
			set { m_Rarety = value;}
		}
		
		public MonsterContractType(Type type, string name, int rarety)
		{
			Type = type;
			Name = name;
			Rarety = rarety;
		}
	}
}
