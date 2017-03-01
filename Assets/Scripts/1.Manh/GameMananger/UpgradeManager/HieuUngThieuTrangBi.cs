using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HieuUngThieuTrangBi : Singleton<HieuUngThieuTrangBi>
{
	public Text power;
	public Text stabiliti;
	public Text cabacity;
	public Text maxzoom;

	Text txtmp;
	public bool isE;

	void OnEnable ()
	{
		CancelInvoke ();
		string tmHieuUng = PlayerPrefs.GetString ("EffectRegquire");
		Debug.Log ("HIeu Ung" + tmHieuUng);
		if (!PlayerPrefs.HasKey ("EffectRegquire") || PlayerPrefs.GetString ("EffectRegquire") == "") {
			return;
		}
		CheckHieuUng ();
	}

	void EffectHU ()
	{
		isE = !isE;
		if (isE) {
			txtmp.color = new Color (.28f, .79f, .79f, 1);
		} else {
			txtmp.color = Color.red;
		}
	}

	bool isP;

	void EffectPower ()
	{
		isP = !isP;
		if (isP) {
			power.color = new Color (.28f, .79f, .79f, 1);
		} else {
			power.color = Color.red;
		}
	}

	bool isC;

	void EffectCapacity ()
	{
		isC = !isC;
		if (isC) {
			cabacity.color = new Color (.28f, .79f, .79f, 1);
		} else {
			cabacity.color = Color.red;
		}
	}

	bool isS;

	void EffectSabacity ()
	{
		isS = !isS;
		if (isS) {
			stabiliti.color = new Color (.28f, .79f, .79f, 1);
		} else {
			stabiliti.color = Color.red;
		}
	}

	bool isM;

	void EffectMaxzoom ()
	{
		isM = !isM;
		if (isM) {
			maxzoom.color = new Color (.28f, .79f, .79f, 1);
		} else {
			maxzoom.color = Color.red;
		}
	}
	//
	//	public void ResetHieuUng ()
	//	{
	//		CancelInvoke ();
	//	}

	GunInGame guningame;
	RequireWeaspon requireweaspon;

	public void CheckHieuUng ()
	{
		if (!PlayerPrefs.HasKey ("EffectRegquire") || PlayerPrefs.GetString ("EffectRegquire") == "") {
			return;
		}
		CancelInvoke ();
		RegionInGame guncurrent = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
//		requireweaspon = 
		requireweaspon = new RequireWeaspon ();
		string namegun = guncurrent.Gun;
		int region = guncurrent.Region;
		float quest = guncurrent.QuestNormal;
		string s = PlayerPrefs.GetString ("CheckButtonTypeGun");
//		Debug.Log ("======" + s);
		guningame = DataManager.Instance.connection.Table<GunInGame> ().Where (x => x.Name == namegun).FirstOrDefault ();
		if (s == "AssaultRifles" || s == "Rifles" || s == "ShotGun") {

			if (guningame.Power >= requireweaspon.GetDetailPath ("Nomarl", region, quest).Power) {
				CancelInvoke ("EffectPower");
			} else {
				InvokeRepeating ("EffectPower", 0.5f, 0.5f);
			}

			if (guningame.Stability >= requireweaspon.GetDetailPath ("Nomarl", region, quest).Stability) {
				CancelInvoke ("EffectSabacity");
			} else {
				InvokeRepeating ("EffectSabacity", 0.5f, 0.5f);

			}

			int maxcapacity = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == namegun).FirstOrDefault ().MaxCapacity;
			if (maxcapacity == 0) {
				if (guningame.Capacity >= requireweaspon.GetDetailPath ("Nomarl", region, quest).Capacity) {
					CancelInvoke ("EffectCapacity");
				} else {
					InvokeRepeating ("EffectCapacity", 0.5f, 0.5f);

				}
			}

			int maxmaxzoom = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == namegun).FirstOrDefault ().MaxMaxzoom;
			if (maxmaxzoom == 0) {
				if (guningame.Maxzoom >= requireweaspon.GetDetailPath ("Nomarl", region, quest).Maxzoom) {
					CancelInvoke ("EffectMaxzoom");
				} else {
					InvokeRepeating ("EffectMaxzoom", 0.5f, 0.5f);

				}
			}
		}

		if (s == "BossS" || s == "BossA" || s == "BossR" || s == "BossC") {
			if (guningame.Power >= requireweaspon.GetDetailPath ("Boss", region, 1).Power) {
				CancelInvoke ("EffectPower");
			} else {
				InvokeRepeating ("EffectPower", 0.5f, 0.5f);
			}

			if (guningame.Stability >= requireweaspon.GetDetailPath ("Boss", region, 1).Stability) {
				CancelInvoke ("EffectSabacity");
			} else {
				InvokeRepeating ("EffectSabacity", 0.5f, 0.5f);

			}

			if (guningame.Capacity >= requireweaspon.GetDetailPath ("Boss", region, 1).Capacity) {
				CancelInvoke ("EffectCapacity");
			} else {
				InvokeRepeating ("EffectCapacity", 0.5f, 0.5f);

			}

			if (guningame.Maxzoom >= requireweaspon.GetDetailPath ("Boss", region, 1).Maxzoom) {
				CancelInvoke ("EffectMaxzoom");
			} else {
				InvokeRepeating ("EffectMaxzoom", 0.5f, 0.5f);

			}
		}
	}
}
