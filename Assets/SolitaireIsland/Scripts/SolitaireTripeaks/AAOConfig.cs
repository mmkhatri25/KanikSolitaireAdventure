using com.F4A.MobileThird;
using Nightingale.Localization;
using Nightingale.Utilitys;
using System.Diagnostics;
using UnityEngine;

namespace SolitaireTripeaks
{
	public class AAOConfig : SingletonClass<AAOConfig>
	{
		private ScheduleData Schedule;

		public int GetLevelInWorld(ScheduleData schedule)
		{
			if (schedule.world == -1)
			{
				ExpertWorldConfig worldConfig = SingletonClass<ExpertLevelConfigGroup>.Get().GetWorldConfig();
				return worldConfig.GetLevel(schedule);
			}
			return UniverseConfig.Get().GetLevelInWorld(schedule);
		}

		public int GetLevelInWorld()
		{
			return GetLevelInWorld(Schedule);
		}

		public bool IsLowBuyStepCoins()
		{
			if (Schedule.world == 0)
			{
				return GetLevelInWorld() < 10;
			}
			return false;
		}

		public int GetLevel(ScheduleData schedule)
		{
			if (schedule.world == -1)
			{
				ExpertWorldConfig worldConfig = SingletonClass<ExpertLevelConfigGroup>.Get().GetWorldConfig();
				return worldConfig.GetLevel(schedule) + int.MinValue;
			}
			return UniverseConfig.Get().GetLevels(schedule);
		}

		public int GetLevel()
		{
			return GetLevel(Schedule);
		}

		public ScheduleData GetNextSchedule(ScheduleData schedule)
		{
			if (schedule.world == -1)
			{
				ExpertWorldConfig worldConfig = SingletonClass<ExpertLevelConfigGroup>.Get().GetWorldConfig();
				return worldConfig.GetNext(schedule);
			}
			return UniverseConfig.Get().GetNextScheduleData(schedule);
		}

		public ScheduleData GetPreSchedule(ScheduleData schedule)
		{
			if (schedule.world == -1)
			{
				ExpertWorldConfig worldConfig = SingletonClass<ExpertLevelConfigGroup>.Get().GetWorldConfig();
				return worldConfig.GetPre(schedule);
			}
			return UniverseConfig.Get().GetPreScheduleData(schedule);
		}

		public bool HasNext(ScheduleData schedule)
		{
			schedule = GetNextSchedule(schedule);
			return schedule.IsEmpty();
		}

		public LevelConfig GetLevelConfig(ScheduleData schedule)
		{
			if (schedule.world == -1)
			{
				ExpertWorldConfig worldConfig = SingletonClass<ExpertLevelConfigGroup>.Get().GetWorldConfig();
				return worldConfig.GetLevelConfig(schedule);
			}
            if (PlayerPrefs.HasKey($"GetLevelConfig world:{schedule.world}, chapter:{schedule.chapter}, level:{schedule.level}"))
            {
                UnityEngine.Debug.Log("not exists.....");
            }
            else if (schedule.level == 0)
            {
                PlayerPrefs.SetString($"GetLevelConfig world:{schedule.world}, chapter:{schedule.chapter}, level:{schedule.level}",$"GetLevelConfig world:{schedule.world}, chapter:{schedule.chapter}, level:{schedule.level}");
                UnityEngine.Debug.Log("existss....."+$"GetLevelConfig world:{schedule.world}, chapter:{schedule.chapter}, level:{schedule.level}");
            
               //PurchasSuccessPopup.ShowPurchasSuccessPopup(new PurchasingCommodity[1]
                    //{
                    //    new PurchasingCommodity
                    //    {
                    //        boosterType = BoosterType.RandomBooster,
                    //        count = 1
                    //    }
                    //});
                    SessionData.Get().PutCommodity(BoosterType.RandomBooster, CommoditySource.Buy, 1L);
               }
            //if (schedule.chapter == 5 && schedule.level == 2)
            if (schedule.chapter == 4 && schedule.level == 19)
            {
                 PlayerPrefs.SetInt("EndGame", 1);
                 UnityEngine.Debug.Log("EndGame I am correct data .... - "+ PlayerPrefs.GetInt("EndGame"));
            }
            UnityEngine.Debug.Log($"@LOG GetLevelConfig world:{schedule.world}, chapter:{schedule.chapter}, level:{schedule.level}".Color(Color.blue));
			return UniverseConfig.Get().GetChapterConfig(schedule.world, schedule.chapter).GetLevelConfig(schedule.level);
		}

		public LevelRetrunCoinConfig GetLevelRetrunCoinConfig(ScheduleData schedule)
		{
			if (schedule.world == -1)
			{
				ExpertWorldConfig worldConfig = SingletonClass<ExpertLevelConfigGroup>.Get().GetWorldConfig();
				return worldConfig.GetLevelRetrunCoinConfig(schedule);
			}
			return UniverseConfig.Get().GetWorldConfig(schedule.world).GetLevelRetrunCoinConfig(schedule);
		}

		public LevelConfig GetLevelConfig()
		{
			return GetLevelConfig(Schedule);
		}

		public LevelRetrunCoinConfig GetLevelRetrunCoinConfig()
		{
			return GetLevelRetrunCoinConfig(Schedule);
		}

		public int GetAllLevelInWorld(int world)
		{
			if (world == -1)
			{
				ExpertWorldConfig worldConfig = SingletonClass<ExpertLevelConfigGroup>.Get().GetWorldConfig();
				return worldConfig.GetPoints().Count;
			}
			return UniverseConfig.Get().GetAllLevelInWorld(world);
		}

		public string GetLevelString(ScheduleData playSchedule)
		{
			if (playSchedule.world == -1)
			{
				return string.Format(LocalizationUtility.Get("Localization_level_confirm.json").GetString("expert_label_level_num"), GetLevelInWorld(playSchedule) + 1);
			}
			return string.Format(LocalizationUtility.Get("Localization_level_confirm.json").GetString("label_level_num"), GetLevelInWorld(playSchedule) + 1);
		}

		public string GetLevelString()
		{
			return GetLevelString(Schedule);
		}

		public WorldConfig GetWorldConfig(int world)
		{
			if (world == -1)
			{
				return SingletonClass<ExpertLevelConfigGroup>.Get().GetWorldConfig();
			}
			return UniverseConfig.Get().GetWorldConfig(world);
		}

		public ChapterConfig GetChapterConfig(int world, int chapter)
		{
			WorldConfig worldConfig = GetWorldConfig(world);
			if (worldConfig == null)
			{
				return null;
			}
			if (worldConfig.chapters.Count > chapter)
			{
				return worldConfig.chapters[chapter];
			}
			return null;
		}

		public void SetPlaySchedule(ScheduleData schedule)
		{
			Schedule = schedule;
			if (schedule.world != -1)
			{
				PlayData.Get().SetPlayScheduleData(schedule);
			}
		}

		public ScheduleData GetPlaySchedule()
		{
			return Schedule;
		}
	}
}
