using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
	public Text txtimer;
	public Text txskill;

	public GameObject map;
	public GameObject gmonster;
	public GameObject fps;


	RegionInGame regioningame;
	RewardNormal rewardnormal;
	RewardBoss rewardboss;
	RewardSpecial rewardspecial;
	RewardEndless rewardendless;


	GameObject gunShot;

	int region;
	string monster;
	int quest;
	string type;
	string typeMonster;

	public Vector3 poslocalgun;
	// vị trí lúc đầu tạo ra gun
	public Vector3 rotationgun;
	// hướng quay lúc đầu tạo ra

	public int skill;
	// số lượng quái cần giết trong

	private int timecount = 80;
	// thời gian bắn trong map, tính bằng
	private int totalskill;


	// chỉ sinh ra quái trong vùng
	private float xMax = 69.2f;
	private float xMin = 29.9f;
	private float zMax = 23.67f;
	private float zMin = -32.9f;


	void Start ()
	{

		rewardnormal = new RewardNormal ();
		rewardboss = new RewardBoss ();
		rewardspecial = new RewardSpecial ();
		rewardendless = new RewardEndless ();
		regioningame = new RegionInGame ();

		region = PlayerPrefs.GetInt ("RegionCurrent");
//		Debug.Log ("Region:" + region);
		monster = PlayerPrefs.GetString ("Monster");
		quest = PlayerPrefs.GetInt ("Quest");
		type = PlayerPrefs.GetString ("Type");
		typeMonster = PlayerPrefs.GetString ("TypeMonster");

		GameEnd.Instance.googleananytic.LogScreen ("Game Play - " + " Region:" + region + " - Quest:" + quest);
//		Debug.Log ("Type:" + type);
		Init ();
		StartCoroutine (InstanceMap ());
		StartCoroutine (InstanceMonster ());
		StartCoroutine (InstanceGun ());
		totalskill = skill;
	}

	void Init ()
	{
		txtimer.text = timecount.ToString ();
		switch (type) {
		case "Normal":
			skill = rewardnormal.GetRewardNomal (region, quest).Kill;
			InvokeRepeating ("CountDowTimer", 5, 1);

			break;
		case "Endless":
			skill = PlayerPrefs.GetInt ("SkillEndless");
			InvokeRepeating ("CountDowTimer", 5, 1);

			break;
		case "Boss":
			skill = rewardboss.GetRewardBoss (region, quest).Kill;
			InvokeRepeating ("CountDowTimer", 5, 1);
			InstanceBoss ();
			break;
		case "Special":
			skill = rewardspecial.GetRewardNomal (region, quest).Kill;
			InvokeRepeating ("CountDowTimer", 5, 1);
			txtimer.text = "";
			break;
		}
		totalskill = skill;
		int tg = totalskill - skill;
		txskill.text = "Kill: " + tg + "/" + totalskill; 
	}

	public int CountDowTimer ()
	{   
		timecount--;
		txtimer.text = timecount.ToString ();
		if (timecount == 0) {
			CancelInvoke ();
			GameEnd.Instance.GameOver ();
		}
		if (skill == 0) {
			CancelInvoke ();
		}
		return timecount;
	}

	public IEnumerator InstanceMonster ()
	{
		yield return new WaitForEndOfFrame ();
		switch (typeMonster) {
		case "A":
			InstanceMonsterA ();
			break;
		case "B":
			InstanceMonsterB ();
			break;
		case "C":
			InvokeRepeating ("InstanceMonsterC", 0, 5);
//			CancelInvoke ("CountDowTimer");
			txtimer.text = "";
//			InstanceMonsterC ();
			break;
		}


	}



	//Quái sinh ra ngẫu nhiên trong map, khi phát hiện bắt 1 là bỏ chạy, 1 là tấn công
	void InstanceMonsterA ()
	{
		for (int i = 0; i < skill + 2; i++) {
			GameObject _monster = Instantiate (Resources.Load ("Monster/Region" + region + "/" + monster + "")) as GameObject;
			_monster.name = monster;
			_monster.transform.position = new Vector3 (UnityEngine.Random.Range (xMin, xMax)
				, -5
				, UnityEngine.Random.Range (zMin, zMax)
			);
			_monster.transform.parent = gmonster.transform;
			_monster.GetComponent<MonsterManager> ().typeMonster = typeMonster;
		}
	}

	// quái sinh ra ngẫu nhiên trong map, khi phát hiện tiếng súng thì bỏ
	void InstanceMonsterB ()
	{
		for (int i = 0; i < skill + 2; i++) {
			GameObject _monster = Instantiate (Resources.Load ("Monster/Region" + region + "/" + monster + "")) as GameObject;
			_monster.name = monster;
			_monster.transform.position = new Vector3 (UnityEngine.Random.Range (xMin, xMax)
				, -5
				, UnityEngine.Random.Range (zMin, zMax)
			);
			_monster.transform.parent = gmonster.transform;
			_monster.GetComponent<MonsterManager> ().typeMonster = typeMonster;
		}
	}


	// quái được sinh ra tại một vị trí, và di chuyển đến một địa điểm khác
	int countMonsterC;

	void InstanceMonsterC ()
	{
		countMonsterC++;
		if (countMonsterC >= 12) {
			CancelInvoke ("InstanceMonsterC");
		}
		if (_map.GetComponent<MapManager> () != null) {
			int totalPoint = _map.GetComponent<MapManager> ().wayC.Count;
			int random = UnityEngine.Random.Range (0, totalPoint);
			Vector3 position = _map.GetComponent<MapManager> ().wayC [random] [0];
			GameObject _monster = Instantiate (Resources.Load ("Monster/Region" + region + "/" + monster + ""))as GameObject;
			_monster.name = monster;
			_monster.transform.position = new Vector3 (position.x, -6, position.z);
			_monster.transform.parent = gmonster.transform;
			_monster.GetComponent<MonsterManager> ().typeMonster = typeMonster;
			_monster.GetComponent<SimpleAIC> ().Init (_map.GetComponent<MapManager> ().wayC [random], "WayC" + random);
		}
	}

	void InstanceBoss ()
	{
		for (int i = 0; i < skill + 2; i++) {
			GameObject _monster = Instantiate (Resources.Load ("Monster/Region" + region + "/" + monster + "")) as GameObject;
			_monster.name = monster;
			_monster.transform.position = new Vector3 (UnityEngine.Random.Range (xMin, xMax)
				, -5
				, UnityEngine.Random.Range (zMin, zMax)
			);
			_monster.transform.parent = gmonster.transform;
			_monster.GetComponent<MonsterManager> ().typeMonster = "A";
		}
	}

	GameObject _map;

	public IEnumerator InstanceMap ()
	{
		yield return new WaitForEndOfFrame ();
		_map = Instantiate (Resources.Load ("Map/Region" + region + "/Map" + UnityEngine.Random.Range (1, 5) + "")) as GameObject;
//		_map = Instantiate (Resources.Load ("Map/Region" + region + "/Map1")) as GameObject;
		_map.transform.parent = map.transform;
	}

	public IEnumerator InstanceGun ()
	{
		
		yield return  new WaitForEndOfFrame ();
		string gun = regioningame.GetRegionInGame ().Gun;

		gunShot = Instantiate (Resources.Load ("Gun/" + gun + ""))as GameObject;
		gunShot.transform.SetParent (fps.transform);
		gunShot.transform.localPosition = poslocalgun;
		gunShot.transform.eulerAngles = rotationgun;
		gunShot.transform.name = gun;
	}

	public void ConfirmSkill (GameObject monster)
	{
		skill--;
		if (skill == 0) {
			GameEnd.Instance.GameComplete ();
			ConfirmMonsterDie (monster);
			gunShot.SetActive (false);
			CancelInvoke ();
		}
		int tg = totalskill - skill;
		txskill.text = "Kill: " + tg + "/" + totalskill; 
	}


	public void ConfirmMonster ()
	{
		for (int i = 0; i < gmonster.transform.childCount; i++) {
			gmonster.transform.GetChild (i).GetComponent<SimpleAIA> ().ConfirmMoveBiThuong ();
			gmonster.transform.GetChild (i).GetComponent<SimpleAIB> ().ConfirmMoveBiThuong ();
		}
	}

	public void ConfirmMonsterDie (GameObject monster)
	{
		
		for (int i = 0; i < gmonster.transform.childCount; i++) {
			if (monster != gmonster.transform.GetChild (i)) {
				gmonster.transform.GetChild (i).GetComponent<MonsterManager> ().stopAnimation ();
			}
		}
	}
}
