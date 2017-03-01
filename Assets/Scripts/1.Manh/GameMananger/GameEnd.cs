using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEnd : Singleton<GameEnd>
{
	public GoogleAnalyticsV4 googleananytic;
	public GameObject UI3D;
	public GameObject FPSGun;
	public GameObject panelGameOver;
	public GameObject panelGameComplete;
	public GameObject panelPlay;
	public GameObject gtam;
	public GameObject tam1;
	public GameObject effectdie;
	public Text txmonster;
	public Text txmonsterfailed;
	public Text txreward;
	public Text txsilver;
	public Text txEp;
	public Text txCrystal;

	public GameObject parrentMonster;


	RewardNormal rewardnormal;
	RewardBoss rewardboss;
	RewardSpecial rewardspecial;
	RewardEndless rewardendless;
	RegionInGame regioningame;

	RequireMap requiremap;
	int region;
	int quest;

	int gold;
	int silver;
	int crystal;

	public bool isEnd;
	public bool IsGameOver;

	void Start ()
	{
//		googleananytic = GoogleAnalyticsV4.instance;
//		googleananytic.LogScreen ("Game Play");
		rewardnormal = new RewardNormal ();
		rewardboss = new RewardBoss ();
		rewardspecial = new RewardSpecial ();
		rewardendless = new RewardEndless ();
		regioningame = new RegionInGame ();
		requiremap = new RequireMap ();
		region = PlayerPrefs.GetInt ("Region");
		quest = PlayerPrefs.GetInt ("Quest");
	}

	public void EffectDie ()
	{
		effectdie.SetActive (true);
	}

	public void GameOver ()
	{
		if (!isEnd) {
			string type = PlayerPrefs.GetString ("Type");
			int quest = PlayerPrefs.GetInt ("Quest");
			Instance.googleananytic.LogScreen ("Game Play - " + " Region:" + region + " - Chế độ chơi:" + type + " - Quest:" + quest + " - Fail ");
//			Debug.Log ("Type Gun End " + PlayerPrefs.GetString ("CheckButtonTypeGun"));
			Shot.Instance.CancelInvoke ();
			panelPlay.SetActive (false);
			panelGameOver.SetActive (true);
			tam1.SetActive (false);
			FPSGun.SetActive (false);
			isEnd = true;
			Camera.main.fieldOfView = 60;
			parrentMonster.SetActive (false);
			Invoke ("CloneMonster", 6f);
			txmonsterfailed.gameObject.SetActive (true);
			txmonsterfailed.text = PlayerPrefs.GetString ("Monster");
			CapNhatThongTin.Instance.Uploadfile ();
		}
	}

	public void GameComplete ()
	{
		if (!isEnd) {
			string type = PlayerPrefs.GetString ("Type");
			int quest = PlayerPrefs.GetInt ("Quest");
			Instance.googleananytic.LogScreen ("Game Play - " + " Region:" + region + " - Chế độ chơi:" + type + " - Quest:" + quest + " - Complete ");

			Shot.Instance.CancelInvoke ();
			panelPlay.SetActive (false);
			isEnd = true;
			FPSGun.SetActive (false);
			gtam.SetActive (false);
			tam1.SetActive (false);
			GunAnimation.Instance.StopAnition ();
			Shot.Instance.SlowMotion ();
			ConfirmReward ();
//			UpdateReward ();
			Camera.main.fieldOfView = 60;
			Invoke ("ShowPanelGameComplete", 5);
		}
	}

	void ShowPanelGameComplete ()
	{
		panelGameComplete.SetActive (true);
		Invoke ("CloneMonster", 6f);
	}

	void CloneMonster ()
	{
		GoogleMobileAdsDemoScript.Instance.ShowFull ();
		GameObject monster = Instantiate (Resources.Load (PlayerPrefs.GetString ("PathMonster")))as GameObject;
		monster.transform.SetParent (UI3D.transform);
		monster.transform.position = UI3D.transform.position;
		monster.transform.eulerAngles = UI3D.transform.eulerAngles;
		UI3D.transform.localScale = new Vector3 (0.7f, 0.7f, 0.7f);
		for (int i = 1; i < monster.transform.childCount; i++) {
			monster.transform.GetChild (i).gameObject.SetActive (false);
		}
		Camera.main.fieldOfView = 60;
		this.transform.GetComponent<GameManager> ().gmonster.SetActive (false);
	}

	public void Hunt ()
	{
		isEnd = true;
		PlayerPrefs.SetInt ("Replay", 1);
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("Menu");
	}

	public void ConfirmReward ()
	{
		txmonster.text = PlayerPrefs.GetString ("Monster");
		int exp = 0;
		switch (PlayerPrefs.GetString ("Type")) {
		case "Normal":
//			txreward.text = rewardnormal.GetRewardNomal (region, quest).Silver;
			txsilver.text = "" + rewardnormal.GetRewardNomal (region, quest).Silver;
			txEp.text = "" + rewardnormal.GetRewardNomal (region, quest).Ep;
			silver = int.Parse (rewardnormal.GetRewardNomal (region, quest).Silver.ToString ());
			gold = int.Parse (rewardnormal.GetRewardNomal (region, quest).Gold.ToString ());
			crystal = int.Parse (rewardnormal.GetRewardNomal (region, quest).Crystal.ToString ());
			exp = rewardnormal.GetRewardNomal (region, quest).Ep;
			txCrystal.text = crystal.ToString ();
			UpdateReward ();

			break;
		case "Endless":
	
			txsilver.text = "" + rewardendless.GetEndless (region).Silver;
			txEp.text = "" + rewardendless.GetEndless (region).Ep;
			silver = rewardendless.GetEndless (region).Silver;
			gold = rewardendless.GetEndless (region).Gold;
			crystal = rewardendless.GetEndless (region).Crystal;
			exp = rewardendless.GetEndless (region).Ep;
			txCrystal.text = crystal.ToString ();

			break;
		case "Boss":
			txsilver.text = "" + rewardboss.GetRewardBoss (region, quest).Silver;
			txEp.text = "" + rewardboss.GetRewardBoss (region, quest).Ep;
			silver = int.Parse (rewardboss.GetRewardBoss (region, quest).Silver.ToString ());
			gold = 0;
			crystal = 0;
			exp = rewardboss.GetRewardBoss (region, quest).Ep;
			txCrystal.text = crystal.ToString ();
			UpdateReward ();

			break;
		case"Special":
			txsilver.text = "" + rewardspecial.GetRewardNomal (region, quest).Silver;
			txEp.text = "" + rewardspecial.GetRewardNomal (region, quest).Ep;
			silver = int.Parse (rewardspecial.GetRewardNomal (region, quest).Silver.ToString ());
			gold = int.Parse (rewardspecial.GetRewardNomal (region, quest).Gold.ToString ());
			crystal = UnityEngine.Random.Range (5, 11);
			exp = rewardspecial.GetRewardNomal (region, quest).Ep;
			txCrystal.text = crystal.ToString ();

			break;
		}
		RegionInGame regioningame = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		regioningame.Exp += exp;
		regioningame.Silver += silver;
		regioningame.Gold += gold;
		regioningame.Crystal += crystal;
		DataManager.Instance.connection.Update (regioningame);
	}

	public void UpdateReward ()
	{
//		Debug.Log ("update");

		bool checkOverRigion = bool.Parse (PlayerPrefs.GetString ("BoolCheckButtonTypeGun"));
		string typegun = PlayerPrefs.GetString ("CheckButtonTypeGun");
		regioningame.UpdateReward (gold, silver, crystal, PlayerPrefs.GetString ("Type"));
//		Debug.Log ("Type Gun End " + PlayerPrefs.GetString ("CheckButtonTypeGun"));
		if (!checkOverRigion) {
//			if (regioningame.QuestBoss >= 2) {
//				Debug.Log ("Len Region");
//				regioningame.QuestNormal = 1;
//				regioningame.QuestBoss = 1;
//				DataManager.Instance.connection.Update (regioningame);
//			}
			switch (typegun) {
			case Const.Rifles:
				requiremap.UpdateRifles (region);
				break;
			case Const.ShotGun:
				requiremap.UpdateShotgun (region);
				break;
			case Const.CrossBows:
				requiremap.UpdateCrossbow (region);
				break;
			case Const.AssaultRifles:
				requiremap.UpdateAssaultRifles (region);
				break;
			case "BossR":
				requiremap.UpdateBossR (region);
				break;
			case "BossS":
				requiremap.UpdateBossS (region);
				break;
			case "BossC":
				requiremap.UpdateBossC (region);
				break;
			case "BossA":
				requiremap.UpdateBossA (region);
				break;
			case "Endless":
				break;

			}
		} else {
			switch (typegun) {
			case "BossR":
				requiremap.UpdateBossR (region);
				if (requiremap.GetRequireMap (region).BossR > 2) {
					regioningame.UpdateRegion ();
				}
				break;
			case "BossS":
				requiremap.UpdateBossS (region);
				if (requiremap.GetRequireMap (region).BossS > 2) {
					regioningame.UpdateRegion ();
				}
				break;
			case "BossC":
				requiremap.UpdateBossC (region);
				if (requiremap.GetRequireMap (region).BossC > 2) {
					regioningame.UpdateRegion ();
				}
				break;
			case "BossA":
				requiremap.UpdateBossA (region);
				if (requiremap.GetRequireMap (region).BossA > 2) {
					regioningame.UpdateRegion ();
				}
				break;
			}
		}
		CapNhatThongTin.Instance.Uploadfile ();
	}
}
