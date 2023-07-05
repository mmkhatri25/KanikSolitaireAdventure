using DG.Tweening;
using Nightingale.ScenesManager;
using Nightingale.Socials;
using Nightingale.Utilitys;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SolitaireTripeaks
{
	public class AchievementScene : SoundScene
	{
		public Button ClosedButton;

		public Button RestButton;

		public Transform ContentTransform;

		public RectTransform AvaterTransform;

		public FriendAvaterUI FriendAvaterUI;

		public SelectAchievementItemUI selectAchievementItemUI;

		protected override void OnDestroy()
		{
			base.OnDestroy();
			SingletonBehaviour<LoaderUtility>.Get().UnLoadScene(typeof(AchievementScene).Name);
		}

		public override void OnSceneStateChanged(SceneState state)
		{
			base.OnSceneStateChanged(state);
			if (state == SceneState.Closing)
			{
				AvaterTransform.DOAnchorPosY(500f, 0.3f);
			}
		}

		private void Start()
		{
			base.IsStay = true;
			Sequence s = DOTween.Sequence();
			s.AppendInterval(0.5f);
			s.Append(AvaterTransform.DOAnchorPosY(-100f, 0.3f));
			s.Append(AvaterTransform.DOAnchorPosY(-85f, 0.1f));
			CreateView(AchievementData.Get().AchievementDatas);
			ClosedButton.onClick.AddListener(delegate
			{
				SingletonClass<MySceneManager>.Get().Close();
			});
			RestButton.gameObject.SetActive(SingletonBehaviour<FacebookMananger>.Get().IsLogin());
			RestButton.onClick.AddListener(delegate
			{
				SingletonBehaviour<TripeaksPlayerHelper>.Get().GetSelf().SetAvatar(string.Empty);
				SingletonBehaviour<ClubSystemHelper>.Get().Profile(AuxiliaryData.Get().GetNickName(), AuxiliaryData.Get().AvaterFileName);
				UpdateRestButton();
			});
			UpdateRestButton();
			FriendAvaterUI.SetUser(SingletonBehaviour<TripeaksPlayerHelper>.Get().GetSelf());
			AchievementCompletedScene.TryShow();
		}

		private void CreateView(List<AchievementInfo> achievementDatas)
		{
			bool flag = true;
			GameObject asset = SingletonBehaviour<LoaderUtility>.Get().GetAsset<GameObject>(typeof(AchievementScene).Name, "UI/AchievementItemUI");
			achievementDatas = (from e in achievementDatas
				orderby e.GetConfig().OrderIndex
				select e).ToList();
			foreach (AchievementInfo achievementData in achievementDatas)
			{
              //  print("tiutle--- " +achievementData.GetTitle());
               
				AchievementConfig config = achievementData.GetConfig();
               
                if ((config.achievementType != AchievementType.CompeletedChapter && config.achievementType != AchievementType.CollectedAllStarsInChapter) || UniverseConfig.Get().GetChapterConfig(config.scheduleData.world, config.scheduleData.chapter) != null)
                {
                    if (achievementData.GetTitle() =="Be a fans"|| achievementData.GetTitle() =="Tortoise Island Owner" ||achievementData.GetTitle() =="Tigridia Island Owner"||achievementData.GetTitle() =="Kung Fu Island Owner"||achievementData.GetTitle() =="Volcano Island Owner"||achievementData.GetTitle() =="Complete Tortoise Island"||achievementData.GetTitle() =="Complete Tigridia Island"|| achievementData.GetTitle() =="Complete Candy Island"||achievementData.GetTitle() =="Complete Kung Fu Island"||achievementData.GetTitle() =="Complete Volcano Island"|| achievementData.GetTitle() =="Complete DeadTree Island"||achievementData.GetTitle() =="Complete Halloween Island"||achievementData.GetTitle().Contains("Easter Islands")||achievementData.GetTitle().Contains("LongGong Island")||achievementData.GetTitle().Contains("Crab Island")||achievementData.GetTitle().Contains("Complete Lost Island")||achievementData.GetTitle().Contains("Labyrinth")||achievementData.GetTitle().Contains("Dinosaur")||achievementData.GetTitle().Contains("Pirate")||achievementData.GetTitle().Contains("GiantTree")||achievementData.GetTitle().Contains("Steam")||achievementData.GetTitle().Contains("Clover")||achievementData.GetTitle().Contains("Valentine")||achievementData.GetTitle().Contains("Octopus")||achievementData.GetTitle().Contains("Mermaid")||achievementData.GetTitle().Contains("Christmas")||achievementData.GetTitle().Contains("Penguin")||achievementData.GetTitle().Contains("Toy")||achievementData.GetTitle().Contains("Clover")||achievementData.GetTitle().Contains("Valentine")||achievementData.GetTitle().Contains("Lushan")||achievementData.GetTitle()=="LongGong Island Owner"||achievementData.GetTitle()=="Halloween Island Owner"||achievementData.GetTitle()=="DeadTree Island Owner"||achievementData.GetTitle()=="Crab Island Owner"||achievementData.GetTitle()=="Lost Island Owner"||achievementData.GetTitle()=="Pirate Island" ||achievementData.GetTitle()=="Dinosaur Island"||achievementData.GetTitle()=="Labyrinth Island"||achievementData.GetTitle()=="Do Everything"||achievementData.GetTitle()=="Solitaire Fans"||achievementData.GetTitle() == "SENIOR MASTER"||achievementData.GetTitle() == "INTERMEDIATE MASTER"||achievementData.GetTitle() == "JUNIOR MASTER" || achievementData.GetTitle() == "Social Master" || achievementData.GetTitle() == "Socialer" || achievementData.GetTitle() =="Inviter" || achievementData.GetTitle() =="Helper" || achievementData.GetTitle() =="Help Me"|| achievementData.GetTitle() =="Invite the Earth"|| achievementData.GetTitle() =="Lei Feng"|| achievementData.GetTitle() =="Friendly" || achievementData.GetTitle() =="Christmas Day" || achievementData.GetTitle() =="Halloween Day" || achievementData.GetTitle() =="Thanksgiving Day"|| achievementData.GetTitle() =="Valentine's Day")
                    { 
                        print("Remove --- " + achievementData.GetTitle());
                       // return;
                    }
                    else
                    {
                        print("Added--- " + achievementData.GetTitle());
                    
                        AchievementItemUI component = Object.Instantiate(asset).GetComponent<AchievementItemUI>();
                        component.SetAchievementData(achievementData, selectAchievementItemUI.SetSelectAchievementInfo);
                        component.transform.SetParent(ContentTransform, worldPositionStays: false);
                        if (flag)
                        {
                            selectAchievementItemUI.SetSelectAchievementInfo(component, achievementData);
                            flag = false;
                        }
                    }
                }
			}
		}

		public void UpdateRestButton()
		{
			RestButton.interactable = !string.IsNullOrEmpty(AuxiliaryData.Get().AvaterFileName);
		}
	}
}
