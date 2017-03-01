using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadQuestInGame : Singleton<LoadQuestInGame>
{
	public GameObject panelMap;
	public GameObject panelEnergy;
	public GameObject[] gregion;

	public Text txnameregion;
	public Text txmode;
	public Text txquest;
	public Text txEx;
	public Text txsilver;
	public Text txgold;
	public Text txcrystal;

	public Image ipower;
	public Image istability;
	public Image icapacity;
	public Image imaxzoom;

	public GameObject uimonster;
	public GameObject dialoghuntnone;
	public GameObject paneleconfirmdamage;
	public GameObject panelChonSaiSung;
	public GameObject panelHetEnergy;

	public Button bthunt;
	RewardNormal rewardnormal;
	RewardBoss rewardboss;
	RewardSpecial rewardspecial;
	RewardEndless rewardendless;

	RegionInGame regioningame;

	RequireWeaspon requireweaspon;
	GunInGame guningame;
	NameRegion nameregion;
	Map map;


	private int quest;
	private int region;
	private string monster;
	private string type;
	private string nameGunCurrent;


	public enum StateHunt
	{
		None,
		Normal,
		Endless,
		Boss,
		Special}
	;

	public  StateHunt statehunt;

	void Start ()
	{
		rewardnormal = new RewardNormal ();
		regioningame = new RegionInGame ();
		requireweaspon = new RequireWeaspon ();
		rewardboss = new RewardBoss ();
		rewardspecial = new RewardSpecial ();
		rewardendless = new RewardEndless ();
		guningame = new GunInGame ();
		nameregion = new NameRegion ();
		map = new Map ();
		txnameregion.text = "Region " + regioningame.GetRegionInGame ().Region + ": " + nameregion.GetName (regioningame.GetRegionInGame ().Region).Name;
	}

	public void DefaultState ()
	{
		statehunt = StateHunt.None;
		int region = 0;
		if (SelectRegion.Instance != null) {
//			region = SelectRegion.Instance.regionCurrent;
			region = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Region;
			SelectRegion.Instance.ShowRegionMax ();
		} else {
			region = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Region;
		}
		if (EffectClick.Instance != null) {
			EffectClick.Instance.Init ();
		}
//		gregion [region].SetActive (true);
		txmode.text = "";
		txquest.text = "";
		txEx.text = "";
		txsilver.text = "";
		txgold.text = "";
		txcrystal.text = "";
	}

	/**
     * @gun name gun
     * @q
     */
	void LoadTextRecommed (string gun, int quest, int totalquest, int ex, int silver, int gold, int crystal)
	{
		if (gun == "AssaultRifles") {
			txmode.text = "Assault Rifles";
		
		} else {
		
			txmode.text = gun;
		}
		txquest.text = quest + "/" + totalquest;
		txEx.text = ex.ToString ();
		txsilver.text = silver.ToString ();
		txgold.text = gold.ToString ();
		txcrystal.text = crystal.ToString ();
		txnameregion.text = "Region " + regioningame.GetRegionInGame ().Region + ": " + nameregion.GetName (regioningame.GetRegionInGame ().Region).Name;
		PlayerPrefs.SetString ("TypeGun", gun);
		PlayerPrefs.Save ();

	}

	void LoadTextRecommed1 (string gun, int quest, int totalquest, int ex, int silver, int gold, int crystal)
	{
		txmode.text = gun;
		txquest.text = "∞";
		txEx.text = ex.ToString ();
		txsilver.text = silver.ToString ();
		txgold.text = gold.ToString ();
		txcrystal.text = crystal.ToString ();
//		txnameregion.text = "Region " + regioningame.GetRegionInGame ().Region + ": " + nameregion.GetName (regioningame.GetRegionInGame ().Region).Name;
		txnameregion.text = "Region " + SelectRegion.Instance.regionCurrent + ": " + nameregion.GetName (SelectRegion.Instance.regionCurrent).Name;

		PlayerPrefs.SetString ("TypeGun", gun);
		PlayerPrefs.Save ();

	}

	//Khi click vao xxx seri(các button quest)
	public void ConfirmGrunNormal (string namegun)
	{
		RessetBthunt ();
		nameGunCurrent = namegun;
		statehunt = StateHunt.Normal;

		string nameGun = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Gun; 
		string typeGun = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().Types;
		int regionGun = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().RegionEnd;
		int regionStart = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().RegionStart;
		int energy = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Energy;
//		region = regioningame.GetRegionInGame ().Region;
		region = PlayerPrefs.GetInt ("RegionCurrent");
		quest = regioningame.GetRegionInGame ().QuestNormal;
//		Debug.Log (region + "/" + quest);
		monster = rewardnormal.GetRewardNomal (region, quest).Monster;
		type = rewardnormal.GetRewardNomal (region, quest).Type;
		LoadTextRecommed (
			namegun,
			quest,
			rewardnormal.CountQuest (region),
			rewardnormal.GetRewardNomal (region, quest).Ep,
			rewardnormal.GetRewardNomal (region, quest).Silver,
			rewardnormal.GetRewardNomal (region, quest).Gold,
			rewardnormal.GetRewardNomal (region, quest).Crystal
		);
		if (regionGun == regionStart) {
			if (PlayerPrefs.GetInt ("RegionCurrent") != regionGun) {
				//ShowDiaLogChonSiaSung();
				return;
			}
		} else {
			if (PlayerPrefs.GetInt ("RegionCurrent") <= regionGun && PlayerPrefs.GetInt ("RegionCurrent") >= regionStart) {
			} else {
				//ShowDiaLogChonSiaSung();
				RessetBthunt ();

				return;
			}
		}
		
		if (statehunt == StateHunt.Normal) {
//			Debug.Log (nameGunCurrent + "//" + typeGun);
			if (nameGunCurrent != typeGun) {
				//ShowDiaLogChonSiaSung();
				return;
			}
			
		}
		if (statehunt == StateHunt.Endless) {
			if (typeGun == "Specialweapon") {
				//ShowDiaLogChonSiaSung();
				RessetBthunt ();

				return;
			}
		}
		if (statehunt == StateHunt.Special) {
			if (typeGun != "Specialweapon") {
				//ShowDiaLogChonSiaSung();
				return;
			}
		}
		if (statehunt == StateHunt.Boss) {
			if (typeGun == "Specialweapon") {
//				ShowDiaLogChonSiaSung();
				return;
			}
		}

	}

	public void ConfirmGunSpecial (string namegun)
	{
		nameGunCurrent = namegun;

		statehunt = StateHunt.Special;
//		region = regioningame.GetRegionInGame ().Region;
		region = PlayerPrefs.GetInt ("RegionCurrent");
		quest = UnityEngine.Random.Range (1, 13);
		//		monster = rewardspecial.GetRewardNomal (region, quest).Monster;
		monster = DataManager.Instance.connection.Table<RewardSpecial> ().Where (x => x.Region == region && x.Quest == quest).FirstOrDefault ().Monster;
		type = rewardspecial.GetRewardNomal (region, quest).Type;
		LoadTextRecommed1 (
			namegun,
			quest,
			rewardspecial.CountQuest (region),
			rewardspecial.GetRewardNomal (region, quest).Ep,
			rewardspecial.GetRewardNomal (region, quest).Silver,
			rewardspecial.GetRewardNomal (region, quest).Gold,
			rewardspecial.GetRewardNomal (region, quest).Crystal
		);
	}

	public void ConfirmGunEndless (string namegun)
	{
		nameGunCurrent = namegun;
		statehunt = StateHunt.Endless;
//		region = regioningame.GetRegionInGame ().Region;
		region = PlayerPrefs.GetInt ("RegionCurrent");
		int questendless = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ().Rifles;
		if (questendless > 20) {
			questendless = 20;
		}
		quest = UnityEngine.Random.Range (1, 20);
		monster = rewardnormal.GetRewardNomal (region, quest).Monster;
		type = rewardnormal.GetRewardNomal (region, quest).Type;
		LoadTextRecommed1 (
			namegun,
			0,
			0,
			rewardendless.GetEndless (region).Ep,
			rewardendless.GetEndless (region).Silver,
			rewardendless.GetEndless (region).Gold,
			rewardendless.GetEndless (region).Crystal
		);
	}

	public void ConfirmGunBoss (string namegun)
	{
		RessetBthunt ();
		nameGunCurrent = namegun;

		statehunt = StateHunt.Boss;
		string nameGun = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Gun; 
		string typeGun = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().Types;
		int regionGun = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().RegionEnd;
		int regionStart = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().RegionStart;
		int energy = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Energy;



		quest = regioningame.GetRegionInGame ().QuestBoss;
		region = regioningame.GetRegionInGame ().Region;
		monster = rewardboss.GetRewardBoss (region, regioningame.GetRegionInGame ().QuestBoss).Monster;
		type = rewardboss.GetRewardBoss (region, regioningame.GetRegionInGame ().QuestBoss).Monster;
		LoadTextRecommed (
			namegun,
			quest,
			rewardboss.CountQuest (region),
			rewardboss.GetRewardBoss (region, quest).Ep,
			rewardboss.GetRewardBoss (region, quest).Silver,
			0,
			0
		);
		if (regionGun == regionStart) {
			if (PlayerPrefs.GetInt ("RegionCurrent") != regionGun) {
//				ShowDiaLogChonSiaSung();
				return;
			}
		} else {
			if (PlayerPrefs.GetInt ("RegionCurrent") <= regionGun && PlayerPrefs.GetInt ("RegionCurrent") >= regionStart) {
			} else {
//				ShowDiaLogChonSiaSung();
				return;
			}
		}
		
		if (statehunt == StateHunt.Normal) {
//			Debug.Log (nameGunCurrent + "//" + typeGun);
			if (nameGunCurrent != typeGun) {
//				ShowDiaLogChonSiaSung();
				return;
			}
			
		}
		if (statehunt == StateHunt.Endless) {
			if (typeGun == "Specialweapon") {
//				ShowDiaLogChonSiaSung();
				return;
			}
		}
		if (statehunt == StateHunt.Special) {
			if (typeGun != "Specialweapon") {
//				ShowDiaLogChonSiaSung();
				return;
			}
		}
		if (statehunt == StateHunt.Boss) {
			if (typeGun == "Specialweapon") {
//				ShowDiaLogChonSiaSung();
				return;
			}
		}
	}

	public void Hunt ()
	{
		
		//Check điều kiện về súng
		// Súng của region nào bắn súng region đó 
		// Súng của kiểu nào bắn map của phần đó
		string nameGun = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Gun; 
		string typeGun = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().Types;
		int regionGun = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().RegionEnd;
		int regionStart = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().RegionStart;
		int energy = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Energy;

		Debug.Log (PlayerPrefs.GetString ("TypeGun"));
		if (regionGun == regionStart) {
			if (PlayerPrefs.GetInt ("RegionCurrent") != regionGun) {
				ShowDiaLogChonSiaSung ();
				return;
			}
		} else {
			if (PlayerPrefs.GetInt ("RegionCurrent") <= regionGun && PlayerPrefs.GetInt ("RegionCurrent") >= regionStart) {
			} else {
				ShowDiaLogChonSiaSung ();
				return;
			}
		}

		if (statehunt == StateHunt.Normal) {
//			Debug.Log (nameGunCurrent + "//" + typeGun);
			if (nameGunCurrent != typeGun) {
				ShowDiaLogChonSiaSung ();
				return;
			}

		}
		if (statehunt == StateHunt.Endless) {
			if (typeGun == "Specialweapon") {
				ShowDiaLogChonSiaSung ();

				return;
			}
		}
		if (statehunt == StateHunt.Special) {
			if (typeGun != "Specialweapon") {
				ShowDiaLogChonSiaSung ();

				return;
			}
		}
		if (statehunt == StateHunt.Boss) {
//			Debug.Log ("tmptype:" + typeGun);
			if (_tmptype == "BossR") {
				if (typeGun != Const.Rifles) {
					ShowDiaLogChonSiaSung ();
					RessetBthunt ();

					return;
				}
			}
			if (_tmptype == "BossA") {
				if (typeGun != Const.AssaultRifles) {
					ShowDiaLogChonSiaSung ();

					return;
				}

			}
			if (_tmptype == "BossS") {
				if (typeGun != "Shotgun") {
					ShowDiaLogChonSiaSung ();

					return;
				}
			}
		}

		if (statehunt != StateHunt.None) {
			if (energy == 0) {
//				panelEnergy.SetActive (true);
//				panelMap.SetActive (false);
				panelHetEnergy.SetActive (true);
				return;
			}
			paneleconfirmdamage.SetActive (true);

//			Debug.Log ("Region" + region + "   Monster:" + monster);
			GameObject _monster = Instantiate (Resources.Load ("MonsterMap/Region" + region + "/" + monster + "")) as GameObject;
			_monster.transform.SetParent (uimonster.transform);
			_monster.name = monster;
			_monster.transform.localPosition = new Vector3 (0, _monster.transform.position.y, 0);
			string s = "MonsterMap/Region" + region + "/" + monster + "";
			PlayerPrefs.SetString ("PathMonster", s);
			PlayerPrefs.SetInt ("Region", region);
			PlayerPrefs.SetString ("Monster", monster);
			PlayerPrefs.SetInt ("Quest", quest);
			PlayerPrefs.SetString ("Type", statehunt.ToString ());
			PlayerPrefs.SetString ("TypeMonster", type.ToString ());
			PlayerPrefs.Save ();
			LoadHeadMonster.Instance.LoadHead (monster);
//			} else {
//				dialoghuntnone.SetActive (true);
//			}
		} else {
			dialoghuntnone.SetActive (true);
		}
	}

	public void BackConfrimDamage ()
	{
		Destroy (uimonster.transform.GetChild (0).gameObject);
	}

	// kiểm tra vũ khí có đạt để băn tiếp ko
	public void CheckWeaspon (string typegun)
	{
		if (type == null) {
			return;
		}
		RessetBthunt ();
		string nameGun = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Gun; 
		string typeGun = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().Types;
		int regionGun = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().RegionEnd;
		int regionStart = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().RegionStart;
		int energy = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Energy;

		string tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Gun;
		string rifles = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().Types;
		if (rifles == "Specialweapon") {
//			ShowDiaLogChonSiaSung();
			return;
		}
		
		
		if (regionGun == regionStart) {
			if (PlayerPrefs.GetInt ("RegionCurrent") != regionGun) {
//				ShowDiaLogChonSiaSung();
				return;
			}
		} else {
			if (PlayerPrefs.GetInt ("RegionCurrent") <= regionGun && PlayerPrefs.GetInt ("RegionCurrent") >= regionStart) {
			} else {
//				ShowDiaLogChonSiaSung();
				
				return;
			}
		}
		
		if (statehunt == StateHunt.Normal) {
			//			Debug.Log (nameGunCurrent + "//" + typeGun);
			if (nameGunCurrent != typeGun) {
//				ShowDiaLogChonSiaSung();
				return;
			}
			
		}
		if (statehunt == StateHunt.Endless) {
			if (typeGun == "Specialweapon") {
//				ShowDiaLogChonSiaSung();
				
				return;
			}
		}
		if (statehunt == StateHunt.Special) {
			if (typeGun != "Specialweapon") {
//				ShowDiaLogChonSiaSung();
				RessetBthunt ();
				
				return;
			}
		}
		if (statehunt == StateHunt.Boss) {
			if (typeGun == "Specialweapon") {
//				ShowDiaLogChonSiaSung();
				return;
			}
		}

//		Debug.Log (typegun);
		UpgradeManager.Instance.TypeGun = typegun;
		bthunt.enabled = true;
		region = regioningame.GetRegionInGame ().Region;
		float _quest = map.GetQuest (region, typegun);
		string namegun = regioningame.GetRegionInGame ().Gun;

		//Debug.Log (quest);


		if (guningame.GetGunInGame (namegun).Power >= requireweaspon.GetDetailPath ("Nomarl", region, quest).Power) {
			ipower.GetComponent<EffectRegquire> ().power = requireweaspon.GetDetailPath ("Nomarl", region, quest).Power;
			ipower.GetComponent<EffectRegquire> ().Confirm (false);
		} else {
			ipower.GetComponent<EffectRegquire> ().power = requireweaspon.GetDetailPath ("Nomarl", region, quest).Power;
			ipower.GetComponent<EffectRegquire> ().Confirm (true);
			PlayerPrefs.SetString ("EffectRegquire", "Power");
			bthunt.enabled = false;
			Tutorial.Instance.ConfirmUpgrade ();
		}

		if (guningame.GetGunInGame (namegun).Stability >= requireweaspon.GetDetailPath ("Nomarl", region, quest).Stability) {
			istability.GetComponent<EffectRegquire> ().stability = requireweaspon.GetDetailPath ("Nomarl", region, quest).Stability;
			istability.GetComponent<EffectRegquire> ().Confirm (false);
		} else {
			istability.GetComponent<EffectRegquire> ().stability = requireweaspon.GetDetailPath ("Nomarl", region, quest).Stability;
			istability.GetComponent<EffectRegquire> ().Confirm (true);
			PlayerPrefs.SetString ("EffectRegquire", "Stability");
			bthunt.enabled = false;

		}
		int maxcapacity = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == namegun).FirstOrDefault ().MaxCapacity;
		if (maxcapacity == 0) {
			if (guningame.GetGunInGame (namegun).Capacity >= requireweaspon.GetDetailPath ("Nomarl", region, quest).Capacity) {
				icapacity.GetComponent<EffectRegquire> ().capacity = requireweaspon.GetDetailPath ("Nomarl", region, quest).Capacity;
				icapacity.GetComponent<EffectRegquire> ().Confirm (false);
			} else {
				icapacity.GetComponent<EffectRegquire> ().capacity = requireweaspon.GetDetailPath ("Nomarl", region, quest).Capacity;
				icapacity.GetComponent<EffectRegquire> ().Confirm (true);
				PlayerPrefs.SetString ("EffectRegquire", "Capacity");
				bthunt.enabled = false;

			}
		}

		int maxmaxzoom = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == namegun).FirstOrDefault ().MaxMaxzoom;
		if (maxmaxzoom == 0) {
			if (guningame.GetGunInGame (namegun).Maxzoom >= requireweaspon.GetDetailPath ("Nomarl", region, quest).Maxzoom) {
				imaxzoom.GetComponent<EffectRegquire> ().maxzoom = requireweaspon.GetDetailPath ("Nomarl", region, quest).Maxzoom;
				imaxzoom.GetComponent<EffectRegquire> ().Confirm (false);
			} else {
				imaxzoom.GetComponent<EffectRegquire> ().maxzoom = requireweaspon.GetDetailPath ("Nomarl", region, quest).Maxzoom;
				imaxzoom.GetComponent<EffectRegquire> ().Confirm (true);
				PlayerPrefs.SetString ("EffectRegquire", "Maxzoom");
				bthunt.enabled = false;

			}
		}
		PlayerPrefs.Save ();
	}

	private string _tmptype;

	public void CheckBoss (string tmptype)
	{
		_tmptype = tmptype;
		UpgradeManager.Instance.TypeGun = "Boss";
		bthunt.enabled = true;
		string namegun = regioningame.GetRegionInGame ().Gun;


		string nameGun = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Gun; 
		string typeGun = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().Types;
		int regionGun = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().RegionEnd;
		int regionStart = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == nameGun).FirstOrDefault ().RegionStart;
		int energy = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Energy;

		if (regionGun == regionStart) {
			if (PlayerPrefs.GetInt ("RegionCurrent") != regionGun) {
				return;
			}
		} else {
			if (PlayerPrefs.GetInt ("RegionCurrent") <= regionGun && PlayerPrefs.GetInt ("RegionCurrent") >= regionStart) {
			} else {
//				ShowDiaLogChonSiaSung();
				return;
			}
		}
		if (tmptype == "BossR") {
			if (typeGun != Const.Rifles) {
//				ShowDiaLogChonSiaSung();

				return;
			}
		}
		if (tmptype == "BossA") {
			if (typeGun != Const.AssaultRifles) {
//				ShowDiaLogChonSiaSung();

				return;
			}
		
		}
		if (tmptype == "BossS") {
			if (typeGun != "Shotgun") {
//				ShowDiaLogChonSiaSung();

				return;
			}
		}

		PlayerPrefs.SetString ("EffectRegquire", "Boss");
		PlayerPrefs.Save ();
//        Debug.Log(guningame.GetGunInGame(namegun).Capacity);
//        Debug.Log(requireweaspon.GetDetailPath("Boss", region, 1).Capacity);

		if (guningame.GetGunInGame (namegun).Power >= requireweaspon.GetDetailPath ("Boss", region, 1).Power) {
			ipower.GetComponent<EffectRegquire> ().power = requireweaspon.GetDetailPath ("Boss", region, 1).Power;
			ipower.GetComponent<EffectRegquire> ().Confirm (false);
		} else {
			ipower.GetComponent<EffectRegquire> ().power = requireweaspon.GetDetailPath ("Boss", region, 1).Power;
			ipower.GetComponent<EffectRegquire> ().Confirm (true);
			bthunt.enabled = false;

		}

		if (guningame.GetGunInGame (namegun).Stability >= requireweaspon.GetDetailPath ("Boss", region, 1).Stability) {
			istability.GetComponent<EffectRegquire> ().stability = requireweaspon.GetDetailPath ("Boss", region, 1).Stability;
			istability.GetComponent<EffectRegquire> ().Confirm (false);
		} else {
			istability.GetComponent<EffectRegquire> ().stability = requireweaspon.GetDetailPath ("Boss", region, 1).Stability;
			istability.GetComponent<EffectRegquire> ().Confirm (true);
			bthunt.enabled = false;

		}

		if (guningame.GetGunInGame (namegun).Capacity >= requireweaspon.GetDetailPath ("Boss", region, 1).Capacity) {
			icapacity.GetComponent<EffectRegquire> ().capacity = requireweaspon.GetDetailPath ("Boss", region, 1).Capacity;
			icapacity.GetComponent<EffectRegquire> ().Confirm (false);
		} else {
			icapacity.GetComponent<EffectRegquire> ().capacity = requireweaspon.GetDetailPath ("Boss", region, 1).Capacity;
			icapacity.GetComponent<EffectRegquire> ().Confirm (true);
			bthunt.enabled = false;

		}

		if (guningame.GetGunInGame (namegun).Maxzoom >= requireweaspon.GetDetailPath ("Boss", region, 1).Maxzoom) {
			imaxzoom.GetComponent<EffectRegquire> ().maxzoom = requireweaspon.GetDetailPath ("Boss", region, 1).Maxzoom;
			imaxzoom.GetComponent<EffectRegquire> ().Confirm (false);
		} else {
			imaxzoom.GetComponent<EffectRegquire> ().maxzoom = requireweaspon.GetDetailPath ("Boss", region, 1).Maxzoom;
			imaxzoom.GetComponent<EffectRegquire> ().Confirm (true);
			bthunt.enabled = false;

		}
	}

	public void RessetBthunt ()
	{
		bthunt.enabled = true;
		ipower.transform.GetChild (1).gameObject.SetActive (false);
		istability.transform.GetChild (1).gameObject.SetActive (false);
		icapacity.transform.GetChild (1).gameObject.SetActive (false);
		imaxzoom.transform.GetChild (1).gameObject.SetActive (false);

		ipower.GetComponent<Button> ().enabled = false;
		istability.GetComponent<Button> ().enabled = false;
		icapacity.GetComponent<Button> ().enabled = false;
		imaxzoom.GetComponent<Button> ().enabled = false;

		ipower.GetComponent<EffectRegquire> ().Reset ();
		istability.GetComponent<EffectRegquire> ().Reset ();
		icapacity.GetComponent<EffectRegquire> ().Reset ();
		imaxzoom.GetComponent<EffectRegquire> ().Reset ();
	}

	public GameObject btgotoRifles;
	public GameObject btgotoAssaultRifles;
	public GameObject btgotoShotGun;
	public GameObject btgotoSpecial;

	public void ShowDiaLogChonSiaSung ()
	{
		btgotoRifles.SetActive (false);
		btgotoAssaultRifles.SetActive (false);
		btgotoShotGun.SetActive (false);
		btgotoSpecial.SetActive (false);
		panelChonSaiSung.SetActive (true);
		string tmp = PlayerPrefs.GetString ("TypeGun");
		int region = PlayerPrefs.GetInt ("RegionCurrent");
		string message = "";
		if (tmp == "Endless") {
			switch (region) {
			case 1:
				message = "Selected gun is not valid for this game mode. Please choose  Rifles for region " + 1;
				btgotoRifles.SetActive (true);
				break;
			case 2:
				message = "Selected gun is not valid for this game mode. Please choose  Rifles or AssaultRifles for region " + 2;
				btgotoRifles.SetActive (true);
				btgotoAssaultRifles.SetActive (true);
				break;
			case 3:
				message = "Selected gun is not valid for this game mode. Please choose  Rifles or Shotgun for region " + 3;
				btgotoRifles.SetActive (true);
				btgotoShotGun.SetActive (true);
				break;
			case 4:
				message = "Selected gun is not valid for this game mode. Please choose  Rifles,AssaultRifles or Shotgun for region " + 4;
				btgotoRifles.SetActive (true);
				btgotoAssaultRifles.SetActive (true);
				btgotoShotGun.SetActive (true);
				break;
			}
		}
		if (tmp == "Special") {
			message = "Selected gun is not valid for this game mode. Plase choose gun Special for region " + region;
			btgotoSpecial.SetActive (true);
		}
		if (tmp != "Endless" && tmp != "Special") {
			message = "Selected gun is not valid for this game mode. Plase choose gun " + tmp + " for region " + region;
			if (tmp == "Endles") {
				btgotoRifles.SetActive (true);	
			}
			if (tmp == "AssaultRifles") {
				btgotoAssaultRifles.SetActive (true);
			}
			if (tmp == "Shotgun") {
				btgotoShotGun.SetActive (true);
			}
		}
		panelChonSaiSung.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = message;
	}
}
