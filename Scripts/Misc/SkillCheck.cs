#region References
using System;

using Server.Engines.Quests;
using Server.Factions;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Regions;
using Server.Spells.SkillMasteries;
#endregion

namespace Server.Misc
{
	public class SkillCheck
	{
		private static readonly TimeSpan _StatGainDelay;
		private static readonly TimeSpan _PetStatGainDelay;

		private static readonly int _PlayerChanceToGainStats;
		private static readonly int _PetChanceToGainStats;

		private static readonly bool _AntiMacroCode;

		/// <summary>
		///     How long do we remember targets/locations?
		/// </summary>
		public static TimeSpan AntiMacroExpire = TimeSpan.FromMinutes(5.0);

		/// <summary>
		///     How many times may we use the same location/target for gain
		/// </summary>
		public const int Allowance = 3;

		/// <summary>
		///     The size of each location, make this smaller so players dont have to move as far
		/// </summary>
		private const int LocationSize = 4;

		public static bool GGSActive { get { return !Siege.SiegeShard; } }

		static SkillCheck()
		{
			_AntiMacroCode = Config.Get("PlayerCaps.EnableAntiMacro", false);

			_StatGainDelay = Config.Get("PlayerCaps.PlayerStatTimeDelay", TimeSpan.FromMinutes(15.0));
			_PetStatGainDelay = Config.Get("PlayerCaps.PetStatTimeDelay", TimeSpan.FromMinutes(5.0));

			_PlayerChanceToGainStats = Config.Get("PlayerCaps.PlayerChanceToGainStats", 5);
			_PetChanceToGainStats = Config.Get("PlayerCaps.PetChanceToGainStats", 5);

			if (!Config.Get("PlayerCaps.EnablePlayerStatTimeDelay", false))
				_StatGainDelay = TimeSpan.FromSeconds(0.5);

			if (!Config.Get("PlayerCaps.EnablePetStatTimeDelay", false))
				_PetStatGainDelay = TimeSpan.FromSeconds(0.5);
		}

		private static readonly bool[] UseAntiMacro =
		{
			// true if this skill uses the anti-macro code, false if it does not
		    false, //Anatomia = 0,
            false, //Atirar = 1,
            false, //Bloqueio = 2,
            false, //Briga = 3,
            false, //Bushido = 4,
            false, //Contusivo = 5,
            false, //Cortante = 6,
            false, //DuasMaos = 7,
            false, //Envenenamento = 8,
            false, //Ninjitsu = 9,
            false, //Perfurante = 10,
            false, //PreparoFisico = 11,
            false, //UmaMao = 12,
            false, //Carisma = 13,
            false, //Furtividade = 14,
            false, //Mecanica = 15,
            false, //Pacificar = 16,
            false, //Percepcao = 17,
            false, //Prestidigitacao = 18,
            false, //Provocacao = 19,
            false, //Sobrevivencia = 20,
            false, //Tocar = 21,
            false, //Arcanismo = 22,
            false, //Caos = 23,
            false, //Feiticaria = 24,
            false, //ImbuirMagica = 25,
            false, //Misticismo = 26,
            false, //Necromancia = 27,
            false, //Ordem = 28,
            false, //PoderMagico = 29,
            false, //ResistenciaMagica = 30,
            false, //Adestramento = 31,
            false, //Agricultura = 32,
            false, //Alquimia = 33,
            false, //Carpintaria = 34,
            false, //ConhecimentoArmas = 35,
            false, //Costura = 36,
            false, //Culinaria = 37,
            false, //Erudicao = 38,
            false, //Extracao = 39,
            false, //Ferraria = 40,
            false, //Medicina = 41,
            false, //Veterinaria = 42,
            false //ConhecimentoArmaduras = 43
		};

		public static void Initialize()
		{
			Mobile.SkillCheckLocationHandler = XmlSpawnerSkillCheck.Mobile_SkillCheckLocation;
			Mobile.SkillCheckDirectLocationHandler = XmlSpawnerSkillCheck.Mobile_SkillCheckDirectLocation;

			Mobile.SkillCheckTargetHandler = XmlSpawnerSkillCheck.Mobile_SkillCheckTarget;
			Mobile.SkillCheckDirectTargetHandler = XmlSpawnerSkillCheck.Mobile_SkillCheckDirectTarget;
		}

		public static bool Mobile_SkillCheckLocation(Mobile from, SkillName skillName, double minSkill, double maxSkill)
		{
			var skill = from.Skills[skillName];

			if (skill == null)
				return false;

			var value = skill.Value;

			//TODO: Is there any other place this can go?
			if (skillName == SkillName.Sobrevivencia && BaseGalleon.FindGalleonAt(from, from.Map) is TokunoGalleon)
				value += 1;

			if (value < minSkill)
				return false; // Too difficult

			if (value >= maxSkill)
				return true; // No challenge

			var chance = (value - minSkill) / (maxSkill - minSkill);

			CrystalBallOfKnowledge.TellSkillDifficulty(from, skillName, chance);

			return CheckSkill(from, skill, new Point2D(from.Location.X / LocationSize, from.Location.Y / LocationSize), chance);
		}

		public static bool Mobile_SkillCheckDirectLocation(Mobile from, SkillName skillName, double chance)
		{
			var skill = from.Skills[skillName];

			if (skill == null)
				return false;

			CrystalBallOfKnowledge.TellSkillDifficulty(from, skillName, chance);

			if (chance < 0.0)
				return false; // Too difficult

			if (chance >= 1.0)
				return true; // No challenge

			return CheckSkill(from, skill, new Point2D(from.Location.X / LocationSize, from.Location.Y / LocationSize), chance);
		}

        #region Craft All Gains
        /// <summary>
        /// This should be a successful skill check, where a system can register several skill gains at once. Only system
        /// using this currently is UseAllRes for CraftItem.cs
        /// </summary>
        /// <param name="from"></param>
        /// <param name="skill"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool CheckSkill(Mobile from, SkillName sk, double minSkill, double maxSkill, int amount)
        {
            if (from.Skills.Cap == 0)
                return false;

            var skill = from.Skills[sk];
            var value = skill.Value;
            var gains = 0;

            for (int i = 0; i < amount; i++)
            {
                var gc = GetGainChance(from, skill, (value - minSkill) / (maxSkill - minSkill), value) / 10;

                if (AllowGain(from, skill, new Point2D(from.Location.X / LocationSize, from.Location.Y / LocationSize)))
                {
                    if (from.Alive && (skill.Base < 10.0 || Utility.RandomDouble() <= gc || CheckGGS(from, skill)))
                    {
                        gains++;
                        value += 0.1;
                    }
                }

            }

            if (gains > 0)
            {
                Gain(from, skill, gains);
                EventSink.InvokeSkillCheck(new SkillCheckEventArgs(from, skill, true));
                return true;
            }

            return false;
        }

        private static double GetGainChance(Mobile from, Skill skill, double gains, double chance)
        {
            var gc = (double)(from.Skills.Cap - (from.Skills.Total + (gains * 10))) / from.Skills.Cap;

            gc += (skill.Cap - (skill.Base + (gains * 10))) / skill.Cap;
            gc /= 4;

            gc *= skill.Info.GainFactor;

            if (gc < 0.01)
                gc = 0.01;

            if (gc > 1.00)
                gc = 1.00;

            return gc;
        }
        #endregion

        public static bool CheckSkill(Mobile from, Skill skill, object obj, double chance)
		{
			if (from.Skills.Cap == 0)
				return false;

            var success = Utility.Random(100) <= (int)(chance * 100);
            var gc = GetGainChance(from, skill, chance, success);

			if (AllowGain(from, skill, obj))
			{
				if (from.Alive && (skill.Base < 10.0 || Utility.RandomDouble() <= gc || CheckGGS(from, skill)))
				{
					Gain(from, skill);
				}
			}

            EventSink.InvokeSkillCheck(new SkillCheckEventArgs(from, skill, success));

            return success;
		}

        private static double GetGainChance(Mobile from, Skill skill, double chance, bool success)
        {
            var gc = (double)(from.Skills.Cap - from.Skills.Total) / from.Skills.Cap;

            gc += (skill.Cap - skill.Base) / skill.Cap;
            gc /= 2;

            gc += (1.0 - chance) * (success ? 0.5 : (Core.AOS ? 0.0 : 0.2));
            gc /= 2;

            gc *= skill.Info.GainFactor;

            if (gc < 0.01)
                gc = 0.01;

            // Pets get a 100% bonus
            if (from is BaseCreature && ((BaseCreature)from).Controlled)
                gc += gc * 1.00;

            if (gc > 1.00)
                gc = 1.00;

            return gc;
        }

		public static bool Mobile_SkillCheckTarget(
			Mobile from,
			SkillName skillName,
			object target,
			double minSkill,
			double maxSkill)
		{
			var skill = from.Skills[skillName];

			if (skill == null)
				return false;

			var value = skill.Value;

			if (value < minSkill)
				return false; // Too difficult

			if (value >= maxSkill)
				return true; // No challenge

			var chance = (value - minSkill) / (maxSkill - minSkill);

			CrystalBallOfKnowledge.TellSkillDifficulty(from, skillName, chance);

			return CheckSkill(from, skill, target, chance);
		}

		public static bool Mobile_SkillCheckDirectTarget(Mobile from, SkillName skillName, object target, double chance)
		{
			var skill = from.Skills[skillName];

			if (skill == null)
				return false;

			CrystalBallOfKnowledge.TellSkillDifficulty(from, skillName, chance);

			if (chance < 0.0)
				return false; // Too difficult

			if (chance >= 1.0)
				return true; // No challenge

			return CheckSkill(from, skill, target, chance);
		}

		private static bool AllowGain(Mobile from, Skill skill, object obj)
		{
			if (Core.AOS && Faction.InSkillLoss(from)) //Changed some time between the introduction of AoS and SE.
				return false;

			if (from is PlayerMobile)
			{
				#region SA
				if (skill.Info.SkillID == (int)SkillName.Atirar && from.Race == Race.Gargoyle)
					return false;

				if (skill.Info.SkillID == (int)SkillName.Atirar && @from.Race != Race.Gargoyle)
					return false;
				#endregion

				if (_AntiMacroCode && UseAntiMacro[skill.Info.SkillID])
					return ((PlayerMobile)from).AntiMacroCheck(skill, obj);
			}
			return true;
		}

		public enum Stat
		{
			Str,
			Dex,
			Int
		}

        public static void Gain(Mobile from, Skill skill)
        {
            Gain(from, skill, (int)(from.Region.SkillGain(from) * 10));
        }

        public static void Gain(Mobile from, Skill skill, int toGain)
		{
			if (from.Region.IsPartOf<Jail>())
				return;

			if (from is BaseCreature && ((BaseCreature)from).IsDeadPet)
				return;

			if (skill.SkillName == SkillName.PreparoFisico && from is BaseCreature &&
				(!PetTrainingHelper.Enabled || !((BaseCreature)from).Controlled))
				return;

			if (skill.Base < skill.Cap && skill.Lock == SkillLock.Up)
			{
				var skills = from.Skills;

				if (from is PlayerMobile && Siege.SiegeShard)
				{
					var minsPerGain = Siege.MinutesPerGain(from, skill);

					if (minsPerGain > 0)
					{
						if (Siege.CheckSkillGain((PlayerMobile)from, minsPerGain, skill))
						{
							CheckReduceSkill(skills, toGain, skill);

							if (skills.Total + toGain <= skills.Cap)
							{
								skill.BaseFixedPoint += toGain;
							}
						}

						return;
					}
				}

				if (toGain == 1 && skill.Base <= 10.0)
					toGain = Utility.Random(4) + 1;

				#region Mondain's Legacy
				if (from is PlayerMobile && QuestHelper.EnhancedSkill((PlayerMobile)from, skill))
				{
					toGain *= Utility.RandomMinMax(2, 4);
				}
				#endregion

				#region Scroll of Alacrity
				if (from is PlayerMobile && skill.SkillName == ((PlayerMobile)from).AcceleratedSkill &&
					((PlayerMobile)from).AcceleratedStart > DateTime.UtcNow)
				{
					// You are infused with intense energy. You are under the effects of an accelerated skillgain scroll.
					((PlayerMobile)from).SendLocalizedMessage(1077956);

					toGain = Utility.RandomMinMax(2, 5);
				}
				#endregion

				#region Skill Masteries
				else if (from is BaseCreature && !(from is Server.Engines.Despise.DespiseCreature) && (((BaseCreature)from).Controlled || ((BaseCreature)from).Summoned))
				{
					var master = ((BaseCreature)from).GetMaster();

					if (master != null)
					{
						var spell = SkillMasterySpell.GetSpell(master, typeof(WhisperingSpell)) as WhisperingSpell;

						if (spell != null && master.InRange(from.Location, spell.PartyRange) && master.Map == from.Map &&
							spell.EnhancedGainChance >= Utility.Random(100))
						{
							toGain = Utility.RandomMinMax(2, 5);
						}
					}
				}
				#endregion

				if (from is PlayerMobile)
				{
					CheckReduceSkill(skills, toGain, skill);
				}

				if (!from.Player || (skills.Total + toGain <= skills.Cap))
				{
					skill.BaseFixedPoint = Math.Min(skill.CapFixedPoint, skill.BaseFixedPoint + toGain);

					EventSink.InvokeSkillGain(new SkillGainEventArgs(from, skill, toGain));

					if (from is PlayerMobile)
						UpdateGGS(from, skill);
				}
			}

			#region Mondain's Legacy
			if (from is PlayerMobile)
				QuestHelper.CheckSkill((PlayerMobile)from, skill);
			#endregion

			if (skill.Lock == SkillLock.Up &&
				(!Siege.SiegeShard || !(from is PlayerMobile) || Siege.CanGainStat((PlayerMobile)from)))
			{
				var info = skill.Info;

				// Old gain mechanic
				if (!Core.ML)
				{
					var scalar = 1.0;

					if (from.StrLock == StatLockType.Up && (info.StrGain / 33.3) * scalar > Utility.RandomDouble())
						GainStat(from, Stat.Str);
					else if (from.DexLock == StatLockType.Up && (info.DexGain / 33.3) * scalar > Utility.RandomDouble())
						GainStat(from, Stat.Dex);
					else if (from.IntLock == StatLockType.Up && (info.IntGain / 33.3) * scalar > Utility.RandomDouble())
						GainStat(from, Stat.Int);
				}
				else
				{
					TryStatGain(info, from);
				}
			}
		}

		private static void CheckReduceSkill(Skills skills, int toGain, Skill gainSKill)
		{
			if (skills.Total / skills.Cap >= Utility.RandomDouble())
			{
				foreach (var toLower in skills)
				{
					if (toLower != gainSKill && toLower.Lock == SkillLock.Down && toLower.BaseFixedPoint >= toGain)
					{
						toLower.BaseFixedPoint -= toGain;
						break;
					}
				}
			}
		}

		public static void TryStatGain(SkillInfo info, Mobile from)
		{
			// Chance roll
			double chance;

            if (from is BaseCreature && ((BaseCreature)from).Controlled)
            {
                chance = _PetChanceToGainStats / 100.0;
            }
            else
            {
                chance = _PlayerChanceToGainStats / 100.0;
            }

			if (Utility.RandomDouble() >= chance)
			{
				return;
			}

			// Selection
			var primaryLock = StatLockType.Locked;
			var secondaryLock = StatLockType.Locked;

			switch (info.Primary)
			{
				case StatCode.Str:
					primaryLock = from.StrLock;
					break;
				case StatCode.Dex:
					primaryLock = from.DexLock;
					break;
				case StatCode.Int:
					primaryLock = from.IntLock;
					break;
			}

			switch (info.Secondary)
			{
				case StatCode.Str:
					secondaryLock = from.StrLock;
					break;
				case StatCode.Dex:
					secondaryLock = from.DexLock;
					break;
				case StatCode.Int:
					secondaryLock = from.IntLock;
					break;
			}

			// Gain
			// Decision block of both are selected to gain
			if (primaryLock == StatLockType.Up && secondaryLock == StatLockType.Up)
			{
				if (Utility.Random(4) == 0)
					GainStat(from, (Stat)info.Secondary);
				else
					GainStat(from, (Stat)info.Primary);
			}
			else // Will not do anything if neither are selected to gain
			{
				if (primaryLock == StatLockType.Up)
					GainStat(from, (Stat)info.Primary);
				else if (secondaryLock == StatLockType.Up)
					GainStat(from, (Stat)info.Secondary);
			}
		}

		public static bool CanLower(Mobile from, Stat stat)
		{
			switch (stat)
			{
				case Stat.Str:
					return (from.StrLock == StatLockType.Down && from.RawStr > 10);
				case Stat.Dex:
					return (from.DexLock == StatLockType.Down && from.RawDex > 10);
				case Stat.Int:
					return (from.IntLock == StatLockType.Down && from.RawInt > 10);
			}

			return false;
		}

		public static bool CanRaise(Mobile from, Stat stat, bool atTotalCap)
		{
			switch (stat)
			{
				case Stat.Str:
                    if (from.RawStr < from.StrCap)
                    {
                        if (atTotalCap && from is PlayerMobile)
                        {
                            return CanLower(from, Stat.Dex) || CanLower(from, Stat.Int); 
                        }
                        else
                        {
                            return true;
                        }
                    }
                    return false;
				case Stat.Dex:
					if (from.RawDex < from.DexCap)
                    {
                        if (atTotalCap && from is PlayerMobile)
                        {
                            return CanLower(from, Stat.Str) || CanLower(from, Stat.Int);
                        }
                        else
                        {
                            return true;
                        }
                    }
                    return false;
				case Stat.Int:
					if (from.RawInt < from.IntCap)
                    {
                        if (atTotalCap && from is PlayerMobile)
                        {
                            return CanLower(from, Stat.Str) || CanLower(from, Stat.Dex);
                        }
                        else
                        {
                            return true;
                        }
                    }
                    return false;
			}

			return false;
		}

        public static void IncreaseStat(Mobile from, Stat stat)
        {
            bool atTotalCap = from.RawStatTotal >= from.StatCap;

            switch (stat)
            {
                case Stat.Str:
				{
                    if (CanRaise(from, Stat.Str, atTotalCap))
                    {
                        if (atTotalCap)
                        {
                            if (CanLower(from, Stat.Dex) && (from.RawDex < from.RawInt || !CanLower(from, Stat.Int)))
                                --from.RawDex;
                            else if (CanLower(from, Stat.Int))
                                --from.RawInt;
                        }

                        ++from.RawStr;

                        if (from is BaseCreature && ((BaseCreature)from).HitsMaxSeed > -1 && ((BaseCreature)from).HitsMaxSeed < from.StrCap)
                        {
                            ((BaseCreature)from).HitsMaxSeed++;
                        }

                        if (Siege.SiegeShard && from is PlayerMobile)
                        {
                            Siege.IncreaseStat((PlayerMobile)from);
                        }
                    }

                    break;
				}
                case Stat.Dex:
				{
                    if (CanRaise(from, Stat.Dex, atTotalCap))
                    {
                        if (atTotalCap)
                        {
                            if (CanLower(from, Stat.Str) && (from.RawStr < from.RawInt || !CanLower(from, Stat.Int)))
                                --from.RawStr;
                            else if (CanLower(from, Stat.Int))
                                --from.RawInt;
                        }

                        ++from.RawDex;

                        if (from is BaseCreature && ((BaseCreature)from).StamMaxSeed > -1 && ((BaseCreature)from).StamMaxSeed < from.DexCap)
                        {
                            ((BaseCreature)from).StamMaxSeed++;
                        }

                        if (Siege.SiegeShard && from is PlayerMobile)
                        {
                            Siege.IncreaseStat((PlayerMobile)from);
                        }
                    }

                    break;
				}
                case Stat.Int:
				{
                    if (CanRaise(from, Stat.Int, atTotalCap))
                    {
                        if (atTotalCap)
                        {
                            if (CanLower(from, Stat.Str) && (from.RawStr < from.RawDex || !CanLower(from, Stat.Dex)))
                                --from.RawStr;
                            else if (CanLower(from, Stat.Dex))
                                --from.RawDex;
                        }

                        ++from.RawInt;

                        if (from is BaseCreature && ((BaseCreature)from).ManaMaxSeed > -1 && ((BaseCreature)from).ManaMaxSeed < from.IntCap)
                        {
                            ((BaseCreature)from).ManaMaxSeed++;
                        }

                        if (Siege.SiegeShard && from is PlayerMobile)
                        {
                            Siege.IncreaseStat((PlayerMobile)from);
                        }
                    }

                    break;
	            }
	        }
		}

		public static void GainStat(Mobile from, Stat stat)
		{
			if (!CheckStatTimer(from, stat))
				return;

			IncreaseStat(from, stat);
		}

		public static bool CheckStatTimer(Mobile from, Stat stat)
		{
			switch (stat)
			{
				case Stat.Str:
				{
					if (from is BaseCreature && ((BaseCreature)from).Controlled)
					{
						if ((from.LastStrGain + _PetStatGainDelay) >= DateTime.UtcNow)
							return false;
					}
					else if ((from.LastStrGain + _StatGainDelay) >= DateTime.UtcNow)
						return false;

					from.LastStrGain = DateTime.UtcNow;
					break;
				}
				case Stat.Dex:
				{
					if (from is BaseCreature && ((BaseCreature)from).Controlled)
					{
						if ((from.LastDexGain + _PetStatGainDelay) >= DateTime.UtcNow)
							return false;
					}
					else if ((from.LastDexGain + _StatGainDelay) >= DateTime.UtcNow)
						return false;

					from.LastDexGain = DateTime.UtcNow;
					break;
				}
				case Stat.Int:
				{
					if (from is BaseCreature && ((BaseCreature)from).Controlled)
					{
						if ((from.LastIntGain + _PetStatGainDelay) >= DateTime.UtcNow)
							return false;
					}
					else if ((from.LastIntGain + _StatGainDelay) >= DateTime.UtcNow)
						return false;

					from.LastIntGain = DateTime.UtcNow;
					break;
				}
			}
			return true;
		}

		private static bool CheckGGS(Mobile from, Skill skill)
		{
			if (!GGSActive)
				return false;

			if (from is PlayerMobile && skill.NextGGSGain < DateTime.UtcNow)
			{
				return true;
			}

			return false;
		}

		public static void UpdateGGS(Mobile from, Skill skill)
		{
			if (!GGSActive)
				return;

			var list = (int)Math.Min(GGSTable.Length - 1, skill.Base / 5);
			var column = from.Skills.Total >= 7000 ? 2 : from.Skills.Total >= 3500 ? 1 : 0;

			skill.NextGGSGain = DateTime.UtcNow + TimeSpan.FromMinutes(GGSTable[list][column]);
		}

		private static readonly int[][] GGSTable =
		{
			new[] {1, 3, 5}, // 0.0 - 4.9
			new[] {4, 10, 18}, new[] {7, 17, 30}, new[] {9, 24, 44}, new[] {12, 31, 57}, new[] {14, 38, 90}, new[] {17, 45, 84},
			new[] {20, 52, 96}, new[] {23, 60, 106}, new[] {25, 66, 120}, new[] {27, 72, 138}, new[] {33, 90, 162},
			new[] {55, 150, 264}, new[] {78, 216, 390}, new[] {114, 294, 540}, new[] {144, 384, 708}, new[] {180, 492, 900},
			new[] {228, 606, 1116}, new[] {276, 744, 1356}, new[] {336, 894, 1620}, new[] {396, 1056, 1920},
			new[] {468, 1242, 2280}, new[] {540, 1440, 2580}, new[] {618, 1662, 3060}
		};
	}
}
