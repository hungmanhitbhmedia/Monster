using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadHeadMonster : Singleton<LoadHeadMonster>
{

	public Text txnamemonster;
	public Text txhead;
	public Text txbody;
	public Text txleg;
	public Text txtail;
	public Text txcrystal;
	public Text txstomach;
	public Text txwing;
	public Text txhand;
	public Text txmounth;
	public Text txskill;

	public GameObject parent;
	public string namemonster;
	public float ro_y;
	Monsters monster;
	Vector3 posmouse;
	Vector3 posbegin;

	RewardBoss rewardboss;
	RewardEndless rewardendless;
	RewardNormal rewardnormal;
	RewardSpecial rewardspecial;

	RegionInGame regioningame;

	void Start ()
	{
		monster = new Monsters ();
		rewardboss = new RewardBoss ();
		rewardendless = new RewardEndless ();
		rewardnormal = new RewardNormal ();
		rewardspecial = new RewardSpecial ();
		regioningame = new RegionInGame ();
	}

	public void LoadHead (string _monster)
	{
		namemonster = _monster;
		txnamemonster.text = _monster;
		txhead.text = monster.GetMonster (_monster).Head.ToString ();
		txbody.text = monster.GetMonster (_monster).Body.ToString ();
		txleg.text = monster.GetMonster (_monster).Leg.ToString ();
		txtail.text = monster.GetMonster (_monster).Tail.ToString ();
		txcrystal.text = monster.GetMonster (_monster).Crystal.ToString ();
		txstomach.text = monster.GetMonster (_monster).Stomach.ToString ();
		txwing.text = monster.GetMonster (_monster).Wing.ToString ();
		txhand.text = monster.GetMonster (_monster).Hand.ToString ();
		txmounth.text = monster.GetMonster (_monster).Mounth.ToString ();

		int region = regioningame.GetRegionInGame ().Region;
		int quest = PlayerPrefs.GetInt ("Quest");
		switch (PlayerPrefs.GetString ("Type")) {
		case "Normal":
			//txskill.text = "Skill " + rewardnormal.GetRewardNomal (region, regioningame.GetRegionInGame ().QuestNormal).Kill + " " + rewardnormal.GetRewardNomal (region, quest).Monster;
			txskill.text = "Kill " + rewardnormal.GetRewardNomal (region, regioningame.GetRegionInGame ().QuestNormal).Kill + " " + PlayerPrefs.GetString ("Monster");
			break;
		case "Endless":
			int skillendless = rewardnormal.GetRewardNomal (region, UnityEngine.Random.Range (1, regioningame.GetRegionInGame ().QuestNormal)).Kill;
			PlayerPrefs.SetInt ("SkillEndless", skillendless);
			PlayerPrefs.Save ();
		//	txskill.text = "Skill " + skillendless + " " + rewardnormal.GetRewardNomal (region, skillendless).Monster;
			//txskill.text = "";
			txskill.text = "Kill " + skillendless + " " + PlayerPrefs.GetString ("Monster");
			break;
		case "Boss":
			//txskill.text = "Skill " + rewardboss.GetRewardBoss (region, regioningame.GetRegionInGame ().QuestBoss).Kill + " " + rewardboss.GetRewardBoss (region, quest).Monster;
			txskill.text = "Kill " + rewardboss.GetRewardBoss (region, regioningame.GetRegionInGame ().QuestBoss).Kill + " " + PlayerPrefs.GetString ("Monster");

			break;
		case "Special":
			//txskill.text = "Skill " + rewardspecial.GetRewardNomal (region, regioningame.GetRegionInGame ().QuestSpecial).Kill + " " + rewardspecial.GetRewardNomal (region, quest).Monster;
			txskill.text = "Kill " + rewardspecial.GetRewardNomal (region, quest).Kill + " " + PlayerPrefs.GetString ("Monster");

			break;
		}
//		Debug.Log ("Kill:" + txskill.text);
	}

	//	void Update ()
	//	{
	//		if (Input.GetMouseButtonDown (0)) {
	//			posbegin = Camera.main.ScreenToViewportPoint (Input.mousePosition);
	//		}
	//		if (Input.GetMouseButton (0)) {
	//			posmouse = Camera.main.ScreenToViewportPoint (Input.mousePosition);
	//			ro_y = -400 * (posmouse.x - posbegin.x);
	//			parent.transform.Rotate (new Vector3 (0, -400 * (posmouse.x - posbegin.x), 0));
	//
	//			posbegin = posmouse;
	//		}
	//	}
}
