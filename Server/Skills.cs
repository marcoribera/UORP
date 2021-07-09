#region References
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Server.Network;
#endregion

namespace Server
{
	public enum StatCode
	{
		Str,
		Dex,
		Int
	}

	public delegate TimeSpan SkillUseCallback(Mobile user);

	public enum SkillLock : byte
	{
		Up = 0,
		Down = 1,
		Locked = 2
	}

	public enum SkillName
	{
        Anatomia = 0,
        Atirar = 1,
        Bloqueio = 2,
        Briga = 3,
        Bushido = 4,
        Contusivo = 5,
        Cortante = 6,
        DuasMaos = 7,
        Envenenamento = 8,
        Ninjitsu = 9,
        Perfurante = 10,
        PreparoFisico = 11,
        UmaMao = 12,
        Carisma = 13,
        Furtividade = 14,
        Mecanica = 15,
        Pacificar = 16,
        Percepcao = 17,
        Prestidigitacao = 18,
        Provocacao = 19,
        Sobrevivencia = 20,
        Tocar = 21,
        Arcanismo = 22,
        Caos = 23,
        Feiticaria = 24,
        ImbuirMagica = 25,
        Misticismo = 26,
        Necromancia = 27,
        Ordem = 28,
        PoderMagico = 29,
        ResistenciaMagica = 30,
        Adestramento = 31,
        Agricultura = 32,
        Alquimia = 33,
        Carpintaria = 34,
        ConhecimentoArmas = 35,
        Costura = 36,
        Culinaria = 37,
        Erudicao = 38,
        Extracao = 39,
        Ferraria = 40,
        Medicina = 41,
        Veterinaria = 42,
        ConhecimentoArmaduras = 43
    }

	[PropertyObject]
	public class Skill
	{
		private readonly Skills m_Owner;
		private readonly SkillInfo m_Info;
		private ushort m_Base;
		private ushort m_Cap;
		private SkillLock m_Lock;

		public override string ToString()
		{
			return String.Format("[{0}: {1}]", Name, Base);
		}

		public Skill(Skills owner, SkillInfo info, GenericReader reader)
		{
			m_Owner = owner;
			m_Info = info;

			int version = reader.ReadByte();

			switch (version)
			{
				case 0:
					{
						m_Base = reader.ReadUShort();
						m_Cap = reader.ReadUShort();
						m_Lock = (SkillLock)reader.ReadByte();

						break;
					}
				case 0xFF:
					{
						m_Base = 0;
						m_Cap = 1000;
						m_Lock = SkillLock.Up;

						break;
					}
				default:
					{
						if ((version & 0xC0) == 0x00)
						{
							if ((version & 0x1) != 0)
							{
								m_Base = reader.ReadUShort();
							}

							if ((version & 0x2) != 0)
							{
								m_Cap = reader.ReadUShort();
							}
							else
							{
								m_Cap = 1000;
							}

							if ((version & 0x4) != 0)
							{
								m_Lock = (SkillLock)reader.ReadByte();
							}

                            if ((version & 0x8) != 0)
                            {
                                VolumeLearned = reader.ReadInt();
                            }

                            if ((version & 0x10) != 0)
                            {
                                NextGGSGain = reader.ReadDateTime();
                            }
						}

						break;
					}
			}

			if (m_Lock < SkillLock.Up || m_Lock > SkillLock.Locked)
			{
				Console.WriteLine("Bad skill lock -> {0}.{1}", owner.Owner, m_Lock);
				m_Lock = SkillLock.Up;
			}
		}

		public Skill(Skills owner, SkillInfo info, int baseValue, int cap, SkillLock skillLock)
		{
			m_Owner = owner;
			m_Info = info;
			m_Base = (ushort)baseValue;
			m_Cap = (ushort)cap;
			m_Lock = skillLock;
		}

		public void SetLockNoRelay(SkillLock skillLock)
		{
			if (skillLock < SkillLock.Up || skillLock > SkillLock.Locked)
			{
				return;
			}

			m_Lock = skillLock;
		}

		public void Serialize(GenericWriter writer)
		{
            if (m_Base == 0 && m_Cap == 1000 && m_Lock == SkillLock.Up && VolumeLearned == 0 && NextGGSGain == DateTime.MinValue)
			{
				writer.Write((byte)0xFF); // default
			}
			else
			{
				int flags = 0x0;

				if (m_Base != 0)
				{
					flags |= 0x1;
				}

				if (m_Cap != 1000)
				{
					flags |= 0x2;
				}

				if (m_Lock != SkillLock.Up)
				{
					flags |= 0x4;
				}

                if (VolumeLearned != 0)
                {
                    flags |= 0x8;
                }

                if (NextGGSGain != DateTime.MinValue)
                {
                    flags |= 0x10;
                }

				writer.Write((byte)flags); // version

				if (m_Base != 0)
				{
					writer.Write((short)m_Base);
				}

				if (m_Cap != 1000)
				{
					writer.Write((short)m_Cap);
				}

				if (m_Lock != SkillLock.Up)
				{
					writer.Write((byte)m_Lock);
				}

                if (VolumeLearned != 0)
                {
                    writer.Write((int)VolumeLearned);
                }

                if (NextGGSGain != DateTime.MinValue)
                {
                    writer.Write(NextGGSGain);
                }
			}
		}

		public Skills Owner { get { return m_Owner; } }

		public SkillName SkillName { get { return (SkillName)m_Info.SkillID; } }

		public int SkillID { get { return m_Info.SkillID; } }

		[CommandProperty(AccessLevel.Counselor)]
		public string Name { get { return m_Info.Name; } }

		public SkillInfo Info { get { return m_Info; } }

		[CommandProperty(AccessLevel.Counselor)]
		public SkillLock Lock { get { return m_Lock; } }

        [CommandProperty(AccessLevel.Counselor)]
        public int VolumeLearned
        {
            get;
            set;
        }

        [CommandProperty(AccessLevel.Counselor)]
        public DateTime NextGGSGain
        {
            get;
            set;
        }

		public int BaseFixedPoint
		{
			get { return m_Base; }
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				else if (value >= 0x10000)
				{
					value = 0xFFFF;
				}

				ushort sv = (ushort)value;

				int oldBase = m_Base;

				if (m_Base != sv)
				{
					m_Owner.Total = (m_Owner.Total - m_Base) + sv;

					m_Base = sv;

					m_Owner.OnSkillChange(this);

					Mobile m = m_Owner.Owner;

					if (m != null)
					{
						m.OnSkillChange(SkillName, (double)oldBase / 10);
					}
				}
			}
		}

		[CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
		public double Base { get { return (m_Base / 10.0); } set { BaseFixedPoint = (int)(value * 10.0); } }

		public int CapFixedPoint
		{
			get { return m_Cap; }
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				else if (value >= 0x10000)
				{
					value = 0xFFFF;
				}

				ushort sv = (ushort)value;

				if (m_Cap != sv)
				{
					m_Cap = sv;

					m_Owner.OnSkillChange(this);
				}
			}
		}

		[CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
		public double Cap 
        { 
            get { return (m_Cap / 10.0); }
            set
            {
                double old = m_Cap / 10;

                CapFixedPoint = (int)(value * 10.0);

                if (old != value && Owner.Owner != null)
                {
                    EventSink.InvokeSkillCapChange(new SkillCapChangeEventArgs(Owner.Owner, this, old, value));
                }
            }
        }

		private static bool m_UseStatMods;

		public static bool UseStatMods { get { return m_UseStatMods; } set { m_UseStatMods = value; } }

		public int Fixed { get { return (int)(Value * 10); } }

		[CommandProperty(AccessLevel.Counselor)]
		public double Value
		{
			get
			{
				//There has to be this distinction between the racial values and not to account for gaining skills and these skills aren't displayed nor Totaled up.
				double value = NonRacialValue;

				double raceBonus = m_Owner.Owner.GetRacialSkillBonus(this.SkillName);

				if (raceBonus > value)
				{
					value = raceBonus;
				}

				return value;
			}
		}

		[CommandProperty(AccessLevel.Counselor)]
		public double NonRacialValue
		{
			get
			{
				double baseValue = Base;
				double inv = 100.0 - baseValue;

				if (inv < 0.0)
				{
					inv = 0.0;
				}

				inv /= 100.0;

				double statsOffset = ((m_UseStatMods ? m_Owner.Owner.Str : m_Owner.Owner.RawStr) * m_Info.StrScale) +
									 ((m_UseStatMods ? m_Owner.Owner.Dex : m_Owner.Owner.RawDex) * m_Info.DexScale) +
									 ((m_UseStatMods ? m_Owner.Owner.Int : m_Owner.Owner.RawInt) * m_Info.IntScale);
				double statTotal = m_Info.StatTotal * inv;

				statsOffset *= inv;

				if (statsOffset > statTotal)
				{
					statsOffset = statTotal;
				}

				double value = baseValue + statsOffset;

				m_Owner.Owner.ValidateSkillMods();

				var mods = m_Owner.Owner.SkillMods;

				double bonusObey = 0.0, bonusNotObey = 0.0;

				for (int i = 0; i < mods.Count; ++i)
				{
					SkillMod mod = mods[i];

					if (mod.Skill == (SkillName)m_Info.SkillID)
					{
						if (mod.Relative)
						{
							if (mod.ObeyCap)
							{
								bonusObey += mod.Value;
							}
							else
							{
								bonusNotObey += mod.Value;
							}
						}
						else
						{
							bonusObey = 0.0;
							bonusNotObey = 0.0;
							value = mod.Value;
						}
					}
				}

				value += bonusNotObey;

				if (value < Cap)
				{
					value += bonusObey;

					if (value > Cap)
					{
						value = Cap;
					}
				}

				m_Owner.Owner.MutateSkill((SkillName)m_Info.SkillID, ref value);

				return value;
			}
		}

        public bool IsMastery
        {
            get
            {
                return m_Info.IsMastery;
            }
        }

        public bool LearnMastery(int volume)
        {
            if (!IsMastery || HasLearnedVolume(volume))
                return false;

            VolumeLearned = volume;

            if (VolumeLearned > 3)
                VolumeLearned = 3;

            if (VolumeLearned < 0)
                VolumeLearned = 0;

            return true;
        }

        public bool HasLearnedVolume(int volume)
        {
            return VolumeLearned >= volume;
        }

        public bool HasLearnedMastery()
        {
            return VolumeLearned > 0;
        }

        public bool SetCurrent()
        {
            if (IsMastery)
            {
                m_Owner.CurrentMastery = (SkillName)m_Info.SkillID;
                return true;
            }

            return false;
        }

		public void Update()
		{
			m_Owner.OnSkillChange(this);
		}
	}

	public class SkillInfo
	{
		private readonly int m_SkillID;

		public SkillInfo(
			int skillID,
			string name,
			double strScale,
			double dexScale,
			double intScale,
			string title,
			SkillUseCallback callback,
			double strGain,
			double dexGain,
			double intGain,
			double gainFactor,
			StatCode primary,
            StatCode secondary, 
            bool mastery = false,
            bool usewhilecasting = false)
		{
			Name = name;
			Title = title;
			m_SkillID = skillID;
			StrScale = strScale / 100.0;
			DexScale = dexScale / 100.0;
			IntScale = intScale / 100.0;
			Callback = callback;
			StrGain = strGain;
			DexGain = dexGain;
			IntGain = intGain;
			GainFactor = gainFactor;
			Primary = primary;
			Secondary = secondary;
            IsMastery = mastery;
            UseWhileCasting = usewhilecasting;

			StatTotal = strScale + dexScale + intScale;
		}

		public StatCode Primary { get; private set; }
		public StatCode Secondary { get; private set; }

		public SkillUseCallback Callback { get; set; }

		public int SkillID { get { return m_SkillID; } }

		public string Name { get; set; }

		public string Title { get; set; }

		public double StrScale { get; set; }

		public double DexScale { get; set; }

		public double IntScale { get; set; }

		public double StatTotal { get; set; }

		public double StrGain { get; set; }

		public double DexGain { get; set; }

		public double IntGain { get; set; }

		public double GainFactor { get; set; }

        public bool IsMastery { get; set; }

        public bool UseWhileCasting { get; set; }

        public int Localization { get { return 1044060 + SkillID; } }

        public static double FACILIMO = 0.8f;
        public static double FACIL = 0.6f;
        public static double MEDIO = 0.4f;
        public static double DIFICIL = 0.2f;
        public static double DIFICILIMO = 0.1f;

        private static SkillInfo[] m_Table = new SkillInfo[44]
		{
			new SkillInfo(0, "Anatomia", 0.0, 0.0, 5.0, "em Anatomia", null, 0.0, 0.0, 0.5, DIFICILIMO, StatCode.Int, StatCode.Dex),
			new SkillInfo(1, "Armas de Atirar", 0.0, 5.0, 0.0, "Atirador(a)", null, 0.5, 1.0, 0.0, DIFICIL, StatCode.Dex, StatCode.Str),
			new SkillInfo(2, "Bloqueio", 5.0, 0.0, 5.0, "Defensor(a)", null, 1.0, 0.5, 0.0, DIFICIL, StatCode.Str, StatCode.Dex),
			new SkillInfo(3, "Briga", 5.0, 5.0, 5.0, "Lutador(a)", null, 1.0, 1.0, 0.5, DIFICIL, StatCode.Str, StatCode.Dex),
            new SkillInfo(4, "Bushido", 5.0, 5.0, 0.0, "Samurai", null, 0.5, 1.0, 0.0, DIFICILIMO, StatCode.Dex, StatCode.Str),
            new SkillInfo(5, "Armas Contusivas", 10.0, 0.0, 0.0, "Demolidor(a)", null, 2.0, 0.0, 0.0, DIFICIL, StatCode.Str, StatCode.Dex),
            new SkillInfo(6, "Armas Cortantes", 5.0, 5.0, 0.0, "Retalhador(a)", null, 1.0, 1.0, 0.0, DIFICIL, StatCode.Str, StatCode.Dex),
            new SkillInfo(7, "Armas de Duas Mãos", 10.0, 0.0, 0.0, "Devastador(a)", null, 2.0, 0.0, 0.0, DIFICIL, StatCode.Str, StatCode.Dex),
            new SkillInfo(8, "Envenenamento", 0.0, 5.0, 5.0, "Assassino(a)", null, 0.0, 0.5, 1.0, DIFICILIMO, StatCode.Int, StatCode.Dex),
            new SkillInfo(9, "Ninjitsu", 0.0, 5.0, 0.0, "Ninja", null, 0.0, 1.0, 0.0, DIFICILIMO, StatCode.Dex, StatCode.Int),
            new SkillInfo(10, "Armas Perfurantes", 0.0, 10.0, 0.0, "Empalador(a)", null, 0.0, 2.0, 0.0, DIFICIL, StatCode.Dex, StatCode.Int),
            new SkillInfo(11, "Preparo Físico", 0.0, 5.0, 0.0, "Atleta", null, 0.0, 1.0, 0.0, DIFICILIMO, StatCode.Dex, StatCode.Str),
            new SkillInfo(12, "Armas de Uma Mão", 5.0, 5.0, 0.0, "Destruidor(a)", null, 1.0, 1.0, 0.0, DIFICIL, StatCode.Str, StatCode.Dex),
            new SkillInfo(13, "Carisma", 0.0, 0.0, 10.0, "Influenciador(a)", null, 0.0, 0.0, 2.0, DIFICILIMO, StatCode.Int, StatCode.Dex),
            new SkillInfo(14, "Furtividade", 0.0, 10.0, 0.0, "Infiltrador(a)", null, 0.0, 2.0, 0.0, MEDIO, StatCode.Dex, StatCode.Int),
            new SkillInfo(15, "Mecânica", 0.0, 10.0, 10.0, "Mecânico(a)", null, 0.0, 2.0, 2.0, MEDIO, StatCode.Dex, StatCode.Int),
            new SkillInfo(16, "Pacificar", 0.0, 0.0, 5.0, "Pacificador(a)", null, 0.0, 0.0, 1.0, DIFICILIMO, StatCode.Int, StatCode.Dex),
            new SkillInfo(17, "Percepção", 0.0, 0.0, 10.0, "Investigador(a)", null, 0.0, 0.0, 2.0, MEDIO, StatCode.Int, StatCode.Dex),
            new SkillInfo(18, "Prestidigitação", 0.0, 10.0, 5.0, "Ilusionista", null, 0.0, 2.0, 1.0, DIFICIL, StatCode.Dex, StatCode.Int),
            new SkillInfo(19, "Provocação", 0.0, 0.0, 10.0, "Atiçador(a)", null, 0.0, 0.0, 2.0, DIFICILIMO, StatCode.Int, StatCode.Dex),
            new SkillInfo(20, "Sobrevivência", 5.0, 5.0, 5.0, "Ranger", null, 1.0, 1.0, 1.0, MEDIO, StatCode.Str, StatCode.Dex),
            new SkillInfo(21, "Tocar Instrumentos", 0.0, 5.0, 5.0, "Musicista", null, 0.0, 1.0, 1.0, FACIL, StatCode.Dex, StatCode.Int),
			new SkillInfo(22, "Arcanismo", 0.0, 0.0, 10.0, "Arcanista", null, 0.0, 0.0, 2.0, DIFICIL, StatCode.Int, StatCode.Dex),
            new SkillInfo(23, "Caos", 0.0, 0.0, 10.0, "Agente do Caos", null, 0.0, 0.0, 2.0, DIFICIL, StatCode.Int, StatCode.Dex),
            new SkillInfo(24, "Feitiçaria", 0.0, 0.0, 10.0, "Feiticeiro(a)", null, 0.0, 0.0, 2.0, DIFICIL, StatCode.Int, StatCode.Dex),
            new SkillInfo(25, "Imbuir Mágica", 0.0, 0.0, 30.0, "Encantador(a)", null, 0.0, 0.0, 6.0, DIFICIL, StatCode.Int, StatCode.Dex),
            new SkillInfo(26, "Misticismo", 0.0, 0.0, 10.0, "do Misticismo", null, 0.0, 0.0, 2.0, DIFICIL, StatCode.Int, StatCode.Dex),
            new SkillInfo(27, "Necromancia", 0.0, 0.0, 10.0, "Necromante", null, 0.0, 0.0, 2.0, DIFICIL, StatCode.Int, StatCode.Dex),
            new SkillInfo(28, "Ordem", 0.0, 0.0, 10.0, "Clérigo(a)", null, 0.0, 0.0, 2.0, DIFICIL, StatCode.Int, StatCode.Dex),
            new SkillInfo(29, "Poder Mágico", 0.0, 0.0, 10.0, "do Poder", null, 0.0, 0.0, 2.0, DIFICILIMO, StatCode.Int, StatCode.Dex),
            new SkillInfo(30, "Resistência Mágica", 0.0, 0.0, 20.0, "Anti-mago(a)", null, 0.0, 0.0, 4.0, DIFICILIMO, StatCode.Int, StatCode.Str),
            new SkillInfo(31, "Adestramento", 0.0, 5.0, 5.0, "Domador(a)", null, 0.0, 1.0, 1.0, DIFICILIMO, StatCode.Int, StatCode.Dex),
            new SkillInfo(32, "Agricultura", 10.0, 10.0, 0.0, "Fazendeiro(a)", null, 2.0, 2.0, 0.0, MEDIO, StatCode.Str, StatCode.Dex),
            new SkillInfo(33, "Alquimia", 0.0, 5.0, 5.0, "Alquimista", null, 0.0, 1.0, 1.0, MEDIO, StatCode.Int, StatCode.Dex),
            new SkillInfo(34, "Carpintaria", 10.0, 10.0, 5.0, "Carpinteiro(a)", null, 2.0, 2.0, 1.0, MEDIO, StatCode.Str, StatCode.Dex),
            new SkillInfo(35, "Conhecimento Armas", 0.0, 0.0, 5.0, "Artífice de Armas", null, 0.0, 0.0, 1.0, DIFICILIMO, StatCode.Int, StatCode.Dex),
            new SkillInfo(36, "Conhecimento Armaduras", 0.0, 0.0, 5.0, "Artífice de Armaduras", null, 0.0, 0.0, 1.0, DIFICILIMO, StatCode.Int, StatCode.Dex),
            new SkillInfo(37, "Costura", 0.0, 10.0, 5.0, "Alfaiate", null, 0.0, 2.0, 1.0, MEDIO, StatCode.Dex, StatCode.Int),
            new SkillInfo(38, "Culinára", 0.0, 10.0, 5.0, "Cozinheiro(a)", null, 0.0, 2.0, 1.0, MEDIO, StatCode.Dex, StatCode.Int),
            new SkillInfo(39, "Erudição", 0.0, 0.0, 20.0, "Estudioso(a)", null, 0.0, 0.0, 4.0, DIFICIL, StatCode.Int, StatCode.Dex),
            new SkillInfo(40, "Extração", 20.0, 5.0, 0.0, "Extrator(a)", null, 4.0, 1.0, 0.0, MEDIO, StatCode.Str, StatCode.Dex),
            new SkillInfo(41, "Ferraria", 20.0, 5.0, 0.0, "Ferreiro(a)", null, 4.0, 1.0, 0.0, MEDIO, StatCode.Str, StatCode.Dex),
            new SkillInfo(42, "Medicina", 0.0, 5.0, 5.0, "Médico(a)", null, 0.0, 1.0, 1.0, DIFICILIMO, StatCode.Int, StatCode.Dex),
            new SkillInfo(43, "Veterinária", 0.0, 5.0, 5.0, "Veterinário(a)", null, 0.0, 1.0, 1.0, DIFICILIMO, StatCode.Int, StatCode.Dex)
        };

		public static SkillInfo[] Table { get { return m_Table; } set { m_Table = value; } }
	}

	[PropertyObject]
	public class Skills : IEnumerable<Skill>
	{
		private readonly Mobile m_Owner;
		private readonly Skill[] m_Skills;
		private int m_Total, m_Cap;
		private Skill m_Highest;

		#region Skill Getters & Setters

		[CommandProperty(AccessLevel.Counselor)]
		public Skill Anatomia { get { return this[SkillName.Anatomia]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Atirar { get { return this[SkillName.Atirar]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Bloqueio { get { return this[SkillName.Bloqueio]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Briga { get { return this[SkillName.Briga]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Bushido { get { return this[SkillName.Bushido]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Contusivo { get { return this[SkillName.Contusivo]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Cortante { get { return this[SkillName.Cortante]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill DuasMaos { get { return this[SkillName.DuasMaos]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Envenenamento { get { return this[SkillName.Envenenamento]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Ninjitsu { get { return this[SkillName.Ninjitsu]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Perfurante { get { return this[SkillName.Perfurante]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill PreparoFisico { get { return this[SkillName.PreparoFisico]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill UmaMao { get { return this[SkillName.UmaMao]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Carisma { get { return this[SkillName.Carisma]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Furtividade { get { return this[SkillName.Furtividade]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Mecanica { get { return this[SkillName.Mecanica]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Pacificar { get { return this[SkillName.Pacificar]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Percepcao { get { return this[SkillName.Percepcao]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Prestidigitacao { get { return this[SkillName.Prestidigitacao]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Provocacao { get { return this[SkillName.Provocacao]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Sobrevivencia { get { return this[SkillName.Sobrevivencia]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Tocar { get { return this[SkillName.Tocar]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Arcanismo { get { return this[SkillName.Arcanismo]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Caos { get { return this[SkillName.Caos]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Feiticaria { get { return this[SkillName.Feiticaria]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill ImbuirMagica { get { return this[SkillName.ImbuirMagica]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Misticismo { get { return this[SkillName.Misticismo]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Necromancia { get { return this[SkillName.Necromancia]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Ordem { get { return this[SkillName.Ordem]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill PoderMagico { get { return this[SkillName.PoderMagico]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill ResistenciaMagica { get { return this[SkillName.ResistenciaMagica]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Adestramento { get { return this[SkillName.Adestramento]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Agricultura { get { return this[SkillName.Agricultura]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Alquimia { get { return this[SkillName.Alquimia]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Carpintaria { get { return this[SkillName.Carpintaria]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill ConhecimentoArmaduras { get { return this[SkillName.ConhecimentoArmaduras]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill ConhecimentoArmas { get { return this[SkillName.ConhecimentoArmas]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Costura { get { return this[SkillName.Costura]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Culinaria { get { return this[SkillName.Culinaria]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
		public Skill Erudicao { get { return this[SkillName.Erudicao]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public Skill Extracao { get { return this[SkillName.Extracao]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
		public Skill Ferraria { get { return this[SkillName.Ferraria]; } set { } }

		[CommandProperty(AccessLevel.Counselor)]
		public Skill Medicina { get { return this[SkillName.Medicina]; } set { } }

		[CommandProperty(AccessLevel.Counselor)]
		public Skill Veterinaria { get { return this[SkillName.Veterinaria]; } set { } }

		[CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
		public int Cap {
            get
            {
                return Math.Min(m_Cap + 500 + (50 * (Convert.ToInt32(Math.Floor((DateTime.Now.Subtract(m_Owner.CreationTime)).TotalDays)))), 7000);
            }
            set
            {
                m_Cap = value;
            }
        }

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public SkillName CurrentMastery
        {
            get;
            set;
        }

		public int Total { get { return m_Total; } set { m_Total = value; } }

		public Mobile Owner { get { return m_Owner; } }

		public int Length { get { return m_Skills.Length; } }

		public Skill this[SkillName name] { get { return this[(int)name]; } }

		public Skill this[int skillID]
		{
			get
			{
				if (skillID < 0 || skillID >= m_Skills.Length)
				{
					return null;
				}

				Skill sk = m_Skills[skillID];

				if (sk == null)
				{
					m_Skills[skillID] = sk = new Skill(this, SkillInfo.Table[skillID], 0, 1000, SkillLock.Up);
				}

				return sk;
			}
		}

        public Skill Highest
        {
            get
            {
                if (m_Highest == null)
                {
                    Skill highest = null;
                    int value = int.MinValue;

                    for (int i = 0; i < m_Skills.Length; ++i)
                    {
                        Skill sk = m_Skills[i];

                        if (sk != null && sk.BaseFixedPoint > value)
                        {
                            value = sk.BaseFixedPoint;
                            highest = sk;
                        }
                    }

                    if (highest == null && m_Skills.Length > 0)
                    {
                        highest = this[0];
                    }

                    m_Highest = highest;
                }

                return m_Highest;
            }
        }
        #endregion

        public override string ToString()
		{
			return "...";
		}

		public static bool UseSkill(Mobile from, SkillName name)
		{
			return UseSkill(from, (int)name);
		}

		public static bool UseSkill(Mobile from, int skillID)
		{
			if (!from.CheckAlive())
			{
				return false;
			}
			else if (!from.Region.OnSkillUse(from, skillID))
			{
				return false;
			}
			else if (!from.AllowSkillUse((SkillName)skillID))
			{
				return false;
			}

			if (skillID >= 0 && skillID < SkillInfo.Table.Length)
			{
				SkillInfo info = SkillInfo.Table[skillID];

				if (info.Callback != null)
				{
					if (Core.TickCount - from.NextSkillTime >= 0 && (info.UseWhileCasting || from.Spell == null))
					{
						from.DisruptiveAction();

						from.NextSkillTime = Core.TickCount + (int)(info.Callback(from)).TotalMilliseconds;

						return true;
					}
					else
					{
						from.SendSkillMessage();
					}
				}
				else
				{
					from.SendLocalizedMessage(500014); // That skill cannot be used directly.
				}
			}

			return false;
		}

		public void Serialize(GenericWriter writer)
		{
			m_Total = 0;

			writer.Write(4); // version

            writer.Write((int)CurrentMastery);

			writer.Write(m_Cap);
			writer.Write(m_Skills.Length);

			for (int i = 0; i < m_Skills.Length; ++i)
			{
				Skill sk = m_Skills[i];

				if (sk == null)
				{
					writer.Write((byte)0xFF);
				}
				else
				{
					sk.Serialize(writer);
					m_Total += sk.BaseFixedPoint;
				}
			}
		}

		public Skills(Mobile owner)
		{
			m_Owner = owner;
            m_Cap = Config.Get("PlayerCaps.TotalSkillCap", 7000); ;

			var info = SkillInfo.Table;

			m_Skills = new Skill[info.Length];

			//for ( int i = 0; i < info.Length; ++i )
			//	m_Skills[i] = new Skill( this, info[i], 0, 1000, SkillLock.Up );
		}

		public Skills(Mobile owner, GenericReader reader)
		{
			m_Owner = owner;

			int version = reader.ReadInt();

			switch (version)
			{
                case 4:
                    CurrentMastery = (SkillName)reader.ReadInt();
                    goto case 3;
				case 3:
				case 2:
					{
						m_Cap = reader.ReadInt();

						goto case 1;
					}
				case 1:
					{
						if (version < 2)
						{
							m_Cap = 7000;
						}

						if (version < 3)
						{
							/*m_Total =*/
							reader.ReadInt();
						}

						var info = SkillInfo.Table;

						m_Skills = new Skill[info.Length];

						int count = reader.ReadInt();

						for (int i = 0; i < count; ++i)
						{
							if (i < info.Length)
							{
								Skill sk = new Skill(this, info[i], reader);

                                if (sk.BaseFixedPoint != 0 || sk.CapFixedPoint != 1000 || sk.Lock != SkillLock.Up || sk.VolumeLearned != 0)
								{
									m_Skills[i] = sk;
									m_Total += sk.BaseFixedPoint;
								}
							}
							else
							{
								new Skill(this, null, reader);
							}
						}

						//for ( int i = count; i < info.Length; ++i )
						//	m_Skills[i] = new Skill( this, info[i], 0, 1000, SkillLock.Up );

						break;
					}
				case 0:
					{
						reader.ReadInt();

						goto case 1;
					}
			}
		}

		public void OnSkillChange(Skill skill)
		{
			if (skill == m_Highest) // could be downgrading the skill, force a recalc
			{
				m_Highest = null;
			}
			else if (m_Highest != null && skill.BaseFixedPoint > m_Highest.BaseFixedPoint)
			{
				m_Highest = skill;
			}

			m_Owner.OnSkillInvalidated(skill);

			NetState ns = m_Owner.NetState;

			if (ns != null)
			{
				ns.Send(new SkillChange(skill));

				m_Owner.Delta(MobileDelta.Skills);
				m_Owner.ProcessDelta();
			}
		}

		public IEnumerator<Skill> GetEnumerator()
		{
			return m_Skills.Where(s => s != null).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return m_Skills.Where(s => s != null).GetEnumerator();
		}
	}
}
