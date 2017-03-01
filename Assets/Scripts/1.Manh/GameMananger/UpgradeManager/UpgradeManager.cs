using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UpgradeManager : Singleton<UpgradeManager>
{
	public GoogleAnalyticsV4 googleananytic;
	public GameObject panelWeaponDetail;
	public GameObject panelMap;
	public GameObject ui3dmodelgun;

	public Text txpower;
	public Text txpowerUpgrade;
	public Text txcapacity;
	public Text txcapacityUpgrade;
	public Text txreload;
	public Text txstability;
	public Text txstabilityUgrade;
	public Text txfirefate;
	public Text txmazoom;
	public Text txmazoomUpgrade;
	public Text txgun;

	public Text txpricesilver;
	public Text txpricegold;

	public GameObject or;
	public GameObject dialogUpgrade;
	public Text txdialog;
	public GameObject dialogPurchareFail;


	public GameObject gEffect;
	float fEffect;

	RegionInGame regioningame;
	GunInGame guningame;
	Rifles rifles;
	UpdateGun updategun;
	Silver silver;

	private string gunUpgrade;
	// kieeur sung
	private string type;
	// bộ phận nâng cấp
	private string path;

	private string tmpD;

	enum Money
	{
		Silver,
		Gold}
	;

	Money money;

	void Start ()
	{
//		googleananytic = GoogleAnalyticsV4.instance;
//		googleananytic.LogScreen ("Upgrad");
		regioningame = new RegionInGame ();		
		guningame = new GunInGame ();
		rifles = new Rifles ();
		updategun = new UpdateGun ();
		silver = new Silver ();
		if (PlayerPrefs.HasKey ("CownDownUgrade")) {
			InvokeRepeating ("CoundDownUpgrade", 1, 1);
		}
	}

	public void Init (string name)
	{
		UIController.Instance.googleanaytic.LogScreen ("Upgrade");
		ShopManager.Instance.ShowGunUI (name);
		path = "None";
		type = rifles.GetRifles (name).Types;
		gunUpgrade = name;
		txgun.text = name;
		txreload.text = rifles.GetRifles (name).Reload.ToString ();
		txfirefate.text = rifles.GetRifles (name).Fire.ToString ();

		float powerdefault = rifles.GetRifles (name).Power;
		float capacitydefault = rifles.GetRifles (name).Capacity;
		float stabilitydefault = rifles.GetRifles (name).Stability;
		float maxzomdefault = rifles.GetRifles (name).Maxzoom;
		txcapacityUpgrade.text = "";
		txstabilityUgrade.text = "";
		txpowerUpgrade.text = "";
		txmazoomUpgrade.text = "";

		txpricegold.transform.parent.gameObject.SetActive (false);


		txpower.text = (powerdefault + rifles.GetRifles (name).PesentPower * guningame.GetGunInGame (name).Power) + "";
		txstability.text = Math.Round (stabilitydefault + (stabilitydefault * updategun.GetDetail (Const.Stability, rifles.GetRifles (name).Types) * guningame.GetGunInGame (name).Stability) / 100, 1) + "";
		if (rifles.GetRifles (name).MaxCapacity == 0) {
//			txcapacity.text = (capacitydefault + updategun.GetDetail (Const.Capacity, rifles.GetRifles (name).Types) * guningame.GetGunInGame (name).Capacity) + ""; 
			txcapacity.text = Math.Round ((capacitydefault + updategun.GetDetail (Const.Capacity, rifles.GetRifles (name).Types) * guningame.GetGunInGame (name).Capacity), 1) + ""; 
		} else {
//			txcapacity.text = capacitydefault.ToString ();
			txcapacity.text = Math.Round (capacitydefault, 1).ToString ();
		}
		if (rifles.GetRifles (name).MaxMaxzoom == 0) {
			txmazoom.text = Math.Round ((maxzomdefault + updategun.GetDetail (Const.Maxzoom, rifles.GetRifles (name).Types) * guningame.GetGunInGame (name).Maxzoom), 1) + "";
		} else {
			txmazoom.text = Math.Round (maxzomdefault, 1).ToString ();
		}
	}

	public void CheckMax ()
	{
		
		if (guningame.GetPathGun (gunUpgrade, Const.Power) < 5) {
			txpower.color = new Color (0.32f, 0.9f, 0.9f);
		} else {
			txpower.color = new Color (1, 0, 0);
		}
		if (guningame.GetPathGun (gunUpgrade, Const.Capacity) < 5) {
			txcapacity.color = new Color (0.32f, 0.9f, 0.9f);
			
		} else {
			txcapacity.color = new Color (1, 0, 0);
		}
		
		if (guningame.GetPathGun (gunUpgrade, Const.Stability) < 5) {
			txstability.color = new Color (0.32f, 0.9f, 0.9f);
		} else {
			txstability.color = new Color (1, 0, 0);
		}
		if (guningame.GetPathGun (gunUpgrade, Const.Maxzoom) < 5) {
			txmazoom.color = new Color (0.32f, 0.9f, 0.9f);
		} else {
			txmazoom.color = new Color (1, 0, 0);
		}
		
		
	}

	public void ResetTextUpgrade ()
	{
		txpowerUpgrade.text = "";
		txcapacityUpgrade.text = "";
		txstabilityUgrade.text = "";
		txmazoomUpgrade.text = "";
//		txpricegold.transform.parent.gameObject.SetActive (false);
//		txpricesilver.text = "";
//		txpricegold.text = "";
		txpricegold.transform.parent.gameObject.SetActive (false);
//		txpricesilver.transform.parent.gameObject.SetActive (false);
	}

	public void Power ()
	{

		tmpD = Const.Power;
		ResetTextUpgrade ();
		LoadSilverAndGold (Const.Power);
		if (guningame.GetPathGun (gunUpgrade, Const.Power) < 5) {
			txpowerUpgrade.text = "+" + Math.Round (rifles.GetRifles (gunUpgrade).PesentPower, 2);
			fEffect = rifles.GetRifles (gunUpgrade).PesentPower;
			path = Const.Power;
		} else {
			txpowerUpgrade.text = "MAX";
			txpower.color = Color.red;
			or.SetActive (false);
			path = "None";
		}
		gEffect = txpower.gameObject.transform.GetChild (0).gameObject;
	}

	public void Capacity ()
	{

		tmpD = Const.Capacity;
		ResetTextUpgrade ();
		LoadSilverAndGold (Const.Capacity);
		if (guningame.GetPathGun (gunUpgrade, Const.Capacity) < 5) {
			txcapacityUpgrade.text = "+" + Math.Round (updategun.GetDetail (Const.Capacity, type), 2);
			fEffect = updategun.GetDetail (Const.Capacity, type);
			path = Const.Capacity;
		} else {
			txcapacityUpgrade.text = "MAX";
			txcapacity.color = Color.red;
			or.SetActive (false);
			path = "None";
		}
		gEffect = txcapacity.gameObject.transform.GetChild (0).gameObject;
	}

	public void Stability ()
	{

		tmpD = Const.Stability;
		ResetTextUpgrade ();
		LoadSilverAndGold (Const.Stability);
		if (guningame.GetPathGun (gunUpgrade, Const.Stability) < 5) {
			txstabilityUgrade.text = "+" + Math.Round (updategun.GetDetail (Const.Stability, type) * rifles.GetRifles (gunUpgrade).Stability / 100, 2);
			//Debug.Log(updategun.GetDetail(Const.Stability, type) * rifles.GetRifles(gunUpgrade).Stability / 100);
			fEffect = updategun.GetDetail (Const.Stability, type) * rifles.GetRifles (gunUpgrade).Stability / 100;
			path = Const.Stability;
		} else {
			txstabilityUgrade.text = "MAX";
			txstability.color = Color.red;
			or.SetActive (false);
			path = "None";
		}
		gEffect = txstability.gameObject.transform.GetChild (0).gameObject;
	}

	public void Maxzoom ()
	{

		tmpD = Const.Maxzoom;
		ResetTextUpgrade ();
		LoadSilverAndGold (Const.Maxzoom);
		if (guningame.GetPathGun (gunUpgrade, Const.Maxzoom) < 5) {
			txmazoomUpgrade.text = "+" + Math.Round (updategun.GetDetail (Const.Maxzoom, type), 2);
			fEffect = updategun.GetDetail (Const.Maxzoom, type);
			path = Const.Maxzoom;
		} else {
			txmazoomUpgrade.text = "MAX";
			txmazoom.color = Color.red;
			or.SetActive (false);
			path = "None";
		}
		gEffect = txmazoom.gameObject.transform.GetChild (0).gameObject;
	}

	void LoadSilverAndGold (string _path)
	{
		or.SetActive (true);
		int region = regioningame.GetRegionInGame ().Region;
		int level = guningame.GetPathGun (gunUpgrade, _path) + 1;
		if (level <= 5) {
			txpricesilver.text = "" + silver.GetSilver (_path, region, level, "Silver");
			txpricegold.text = "" + silver.GetSilver (_path, region, level, "Gold");
		} else {
			txpricegold.text = "";
			txpricesilver.text = "";
		}
	}

	public void SubtractSilver ()
	{
		if (isUpgrade) {
			return;
		}
		if (iscounddown) {
			return;
		}
		ui3dmodelgun.SetActive (false);
		if (path != "None") {
			dialogUpgrade.SetActive (true);
			txdialog.text = "Do you want to upgrade " + path + " with silver?";
			money = Money.Silver;
		}
		SoundManager.Instance.PlayButton (0);
	}

	public void SubtractGold ()
	{
		if (isUpgrade) {
			return;
		}
		ui3dmodelgun.SetActive (false);
		if (path != "None") {
			dialogUpgrade.SetActive (true);
			txdialog.text = "Do you want to upgrade " + path + " with gold?";
			money = Money.Gold;
		}
		SoundManager.Instance.PlayButton (0);
	}

	bool isUpgrade;

	public void Yes ()
	{
		
		if (isUpgrade) {
			return;
		}
		isUpgrade = true;
		int region = regioningame.GetRegionInGame ().Region;
		int level = guningame.GetPathGun (gunUpgrade, path) + 1;
		switch (money) {
		case Money.Gold:
			if (regioningame.GetRegionInGame ().Gold >= silver.GetSilver (path, region, level, "Gold")) {
				UIController.Instance.googleanaytic.LogEvent ("Upgrade", "Nâng cấp súng:" + gunUpgrade + " thành công bằng gold ", "", 1);
//				regioningame.ChangeGoldSilveCrystal (silver.GetSilver (path, region, level, "Gold"), 0, 0);
				ShopManager.Instance.UIModelGun3D.SetActive (true);
				float tmpgold = silver.GetSilver (path, region, level, "Gold");
				RegionInGame region1 = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
				region1.Gold = region1.Gold - tmpgold;
				DataManager.Instance.connection.Update (region1);

//				Debug.Log (path);
				guningame.ChangedPath (gunUpgrade, path);
				Init (gunUpgrade);
//				HieuUngThieuTrangBi.Instance.ResetHieuUng ();
				HieuUngThieuTrangBi.Instance.CheckHieuUng ();
				ResetTextUpgrade ();
				gEffect.transform.GetComponentInChildren<Text> ().text = "" + Math.Round (fEffect, 3).ToString ();
				gEffect.SetActive (true);
				Invoke ("EffectDisable", 1.5f);
			} else {
				isUpgrade = false;
				dialogPurchareFail.SetActive (true);
				return;
			}
			break;
		case Money.Silver:
//			Debug.Log ("1");
			if (regioningame.GetRegionInGame ().Silver >= silver.GetSilver (path, region, level, "Silver")) {
				UIController.Instance.googleanaytic.LogEvent ("Upgrade", "Nâng cấp súng:" + gunUpgrade + " thành công silver", "", 1); 

				StartCounDown ();
//				regioningame.ChangeGoldSilveCrystal (0, silver.GetSilver (path, region, level, "Silver"), 0);
//				Debug.Log ("2");
				ShopManager.Instance.UIModelGun3D.SetActive (true);

				float tmpsilver = silver.GetSilver (path, region, level, "Silver");
				RegionInGame region1 = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
				region1.Silver = region1.Silver - tmpsilver;
				DataManager.Instance.connection.Update (region1);

//				Debug.Log (gunUpgrade + "" + path);
				guningame.ChangedPath (gunUpgrade, path);
//				HieuUngThieuTrangBi.Instance.ResetHieuUng ();
				HieuUngThieuTrangBi.Instance.CheckHieuUng ();

				Init (gunUpgrade);
				ResetTextUpgrade ();
				gEffect.transform.GetComponentInChildren<Text> ().text = "" + Math.Round (fEffect, 3).ToString ();
				gEffect.SetActive (true);
				Invoke ("EffectDisable", 1.5f);
			} else {
//				Debug.Log ("3");
				isUpgrade = false;
				dialogPurchareFail.SetActive (true);
				return;
			}
			break;
		}
		dialogUpgrade.SetActive (false);
		ProcessAffterClickYes ();
		CapNhatThongTin.Instance.Uploadfile ();


	}

	//	IEnumerator CapNhat ()
	//	{
	//		yield  return new WaitForEndOfFrame ();
	////		LoadRegionInGame.Instance.Init ();
	//
	//	}

	public void No ()
	{
		UIController.Instance.googleanaytic.LogEvent ("Upgrade", "Huỷ nâng cấp súng: " + gunUpgrade, "", 1);
		dialogUpgrade.SetActive (false);
		isUpgrade = false;
	}

	public string TypeGun;

	public void  UpgradedEnd ()
	{
		if (type != "Boss") {
//			Debug.Log ("TypeGun" + TypeGun);
			LoadQuestInGame.Instance.CheckWeaspon (TypeGun);
//			EffectClick.Instance.Init ();
		} else {
			LoadQuestInGame.Instance.CheckBoss (TypeGun);
//			EffectClick.Instance.Init ();

		}
		if (!panelWeaponDetail.activeSelf) {
			panelMap.SetActive (true);
			ShopManager.Instance.HideModelUIGun ();
			//LoadQuestInGame.Instance.DefaultState ();
		}
	}

	void EffectDisable ()
	{
		gEffect.SetActive (false);
//		isUpgrade = false;
		Invoke ("No", 0.5f);
//		LoadRegionInGame.Instance.Init ();
	}


	void ProcessAffterClickYes ()
	{
		switch (tmpD) {
		case Const.Power:
			Power ();
			break;
		case Const.Capacity:
			Capacity ();
			break;
		case Const.Stability:
			Stability ();
			break;
		case Const.Maxzoom:
			Maxzoom ();
			break;
		}
	}

	bool iscounddown;

	public void CoundDownUpgrade ()
	{
		DateTime timecurrent = DateTime.Now;
		DateTime timebefor = DateTime.Parse (PlayerPrefs.GetString ("CownDownUgrade"));
		TimeSpan timespan = timecurrent.Subtract (timebefor);
		int tmp = timespan.Days * 86400 + timespan.Hours * 3600 + timespan.Minutes * 60 + timespan.Seconds;
		if (tmp >= 300) {
			iscounddown = false;
			txpricesilver.color = new Color (1, 1, 1, 1);
			CancelInvoke ("CoundDownUpgrade");
			return;
		} else {
			iscounddown = true;
			txpricesilver.color = Color.red;

		}
		int tg = 300 - tmp;
		int mi = tg / 60;
		int seco = tg - mi * 60;
		string stm = "";
		if (seco < 10) {
			stm = "0" + seco;
		} else {
			stm = seco.ToString ();
		}
		txpricesilver.text = "0" + mi + ":" + stm;
//		txpricesilver.text = tmp.ToString ();
	}

	public void StopCounDown ()
	{
		CancelInvoke ("CoundDownUpgrade");
	}

	public void StartCounDown ()
	{
		PlayerPrefs.SetString ("CownDownUgrade", DateTime.Now.ToString ());
		PlayerPrefs.Save ();
		InvokeRepeating ("CoundDownUpgrade", 1, 1);
	}
}
