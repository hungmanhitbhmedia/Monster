using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ShopManager : Singleton<ShopManager>
{
	public GameObject panelMenu;
	public GameObject paneDetailWeaspon;
	public GameObject panelUpgrade;
	public GameObject paneWeaspon;
	public GameObject UIModelGun3D;
	public GameObject comingsoon;
	public Text txpower;
	public Text txcapacity;
	public Text txreload;
	public Text txfirerate;
	public Text txstability;
	public Text txmaxzoom;


	public Text txbuy;

	public Text txGun;

	public GameObject btpurchare;
	public GameObject btpurchareCrystal;
	public GameObject btuse;
	public GameObject btupgrade;

	public GameObject popupgun;

	public GameObject dialogbuyfail;
	public GameObject dialogstategun;

	public GameObject trifles;
	public GameObject tshotgun;
	public GameObject tcrossbow;
	public GameObject tspecial;
	public GameObject tassaultrifles;

	//@1 rifles
	//@2 assultrifles
	//@3 crosshow
	//@4 shotgun
	//@5 special
	public GameObject[] buttontypegun;
	// popup gun detail weaspon

	RegionInGame regioningame;
	GunInGame guningame;
	Rifles rifles;
	UpdateGun updategun;
	private int region;
	public string gun;
	private string gunNew;
	private GameObject btgunclick;

	void Start ()
	{
		regioningame = new RegionInGame ();
		guningame = new GunInGame ();
		rifles = new Rifles ();
		updategun = new UpdateGun ();
		region = regioningame.GetRegionInGame ().Region;
		gun = regioningame.GetRegionInGame ().Gun;
	}

	// khi click vào gun trong shop để xcas định các chỉ số cuar súng
	public void DetailGun (string _name, GameObject _btgunclick)
	{
		btuse.SetActive (false);
		btpurchare.SetActive (false);
		btupgrade.SetActive (false);
		txpower.text = "";
		txcapacity.text = "";
		txstability.text = "";
		txmaxzoom.text = "";
		txGun.text = "";
		txreload.text = "";
		txfirerate.text = "";
		comingsoon.SetActive (false);

		if (rifles.GetRifles (_name).RegionEnd > 4) {
			comingsoon.SetActive (true);
			return;
		}

		btgunclick = _btgunclick.gameObject;
		gunNew = _name;
		txreload.text = rifles.GetRifles (gunNew).Reload.ToString ();
		txfirerate.text = rifles.GetRifles (gunNew).Fire.ToString ();

		float powerdefault = rifles.GetRifles (gunNew).Power;
		float capacitydefault = rifles.GetRifles (gunNew).Capacity;
		float stabilitydefault = rifles.GetRifles (gunNew).Stability;
		float maxzomdefault = rifles.GetRifles (gunNew).Maxzoom;
		if (rifles.GetRifles (_name).MaxCapacity == 0) {
//			txmaxzoom.text = (maxzomdefault + updategun.GetDetail (Const.Maxzoom, rifles.GetRifles (gunNew).Types) * guningame.GetGunInGame (_name).Maxzoom) + "";
			txmaxzoom.text = Math.Round ((maxzomdefault + updategun.GetDetail (Const.Maxzoom, rifles.GetRifles (gunNew).Types) * guningame.GetGunInGame (_name).Maxzoom), 1) + "";
		} else {
//			txmaxzoom.text = maxzomdefault.ToString ();
			txmaxzoom.text = Math.Round (maxzomdefault, 1) + "";

		}
		if (rifles.GetRifles (_name).MaxCapacity == 0) {
//			txcapacity.text = (capacitydefault + updategun.GetDetail (Const.Capacity, rifles.GetRifles (gunNew).Types) * guningame.GetGunInGame (_name).Capacity) + ""; 
			txcapacity.text = Math.Round ((capacitydefault + updategun.GetDetail (Const.Capacity, rifles.GetRifles (gunNew).Types) * guningame.GetGunInGame (_name).Capacity), 1) + ""; 

		} else {
//			txcapacity.text = capacitydefault.ToString ();
			txcapacity.text = Math.Round (capacitydefault, 1) + "";
		}


//		txpower.text = (powerdefault + rifles.GetRifles (_name).PesentPower * guningame.GetGunInGame (_name).Power) + "";
		txpower.text = Math.Round ((powerdefault + rifles.GetRifles (_name).PesentPower * guningame.GetGunInGame (_name).Power), 1) + "";
//		txstability.text = stabilitydefault + (stabilitydefault * updategun.GetDetail (Const.Stability, rifles.GetRifles (gunNew).Types) * guningame.GetGunInGame (_name).Stability) / 100 + "";
		txstability.text = Math.Round (stabilitydefault + (stabilitydefault * updategun.GetDetail (Const.Stability, rifles.GetRifles (gunNew).Types) * guningame.GetGunInGame (_name).Stability) / 100, 1) + "";
		txGun.text = _name;



		// ẩn hiện button use or button puchare và btupgrade

		Rifles rGun = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == _name).FirstOrDefault ();
		if (rGun.Types != "Specialweapon") {
			btpurchareCrystal.SetActive (false);
			if (guningame.GetGunInGame (_name).Active == 1) {
				btuse.SetActive (true);
				btpurchare.SetActive (false);
				btupgrade.SetActive (true);
			} else {
				btuse.SetActive (false);
				btpurchare.SetActive (true);
				btupgrade.SetActive (false);
			}
		} else {
			if (guningame.GetGunInGame (_name).Active == 1) {
				btuse.SetActive (true);
				btpurchare.SetActive (false);
				btupgrade.SetActive (true);
				btpurchareCrystal.SetActive (false);

			} else {
				btuse.SetActive (false);
				btpurchare.SetActive (false);
				btupgrade.SetActive (false);
				btpurchareCrystal.SetActive (true);
			}
		}



	}

	void RessetTextWeasponDetail ()
	{
		txreload.text = "";
		txfirerate.text = "";
		txpower.text = "";
		txcapacity.text = "";
		txstability.text = "";
		txmaxzoom.text = "";
		txGun.text = "";
		btuse.SetActive (false);
		btpurchare.SetActive (false);
		btupgrade.SetActive (false);

		tshotgun.SetActive (false);
		tcrossbow.SetActive (false);
		tassaultrifles.SetActive (false);
		tspecial.SetActive (false);
		trifles.SetActive (false);
		gun = null;
	}

	public void Weaspon ()
	{
		// hàm này để check theo concept lúc đầu
		// Concept lúc đầu chỉ hiện lên những kiểu súng trong region đó có còn region k có thì sẽ ko 
//        var _rifles = rifles.GetReflesOnRegion(region);
//        foreach (var ri in _rifles)
//        {
//            switch (ri.Types)
//            {
//                case Const.Rifles:
//                    buttontypegun[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
//                    buttontypegun[0].GetComponent<Button>().enabled = true;
//                    break;
//
//                case Const.AssaultRifles:
//                    buttontypegun[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
//                    buttontypegun[1].GetComponent<Button>().enabled = true;
//                    break;
//                case Const.CrossBows:
//                    buttontypegun[2].GetComponent<Image>().color = new Color(1, 1, 1, 1);
//                    buttontypegun[2].GetComponent<Button>().enabled = true;
//                    break;
//                case Const.ShotGun:
//                    buttontypegun[3].GetComponent<Image>().color = new Color(1, 1, 1, 1);
//                    buttontypegun[3].GetComponent<Button>().enabled = true;
//                    break;
//                case Const.Specialweapon:
//                    buttontypegun[4].GetComponent<Image>().color = new Color(1, 1, 1, 1);
//                    buttontypegun[4].GetComponent<Button>().enabled = true;
//                    break;
//            }
//        }
	}

	// thay đổi súng sử dụng trong game
	public void ChangeUseGunInGame ()
	{
		int regionstart = rifles.GetRifles (gunNew).RegionStart;
		int regionend = rifles.GetRifles (gunNew).RegionEnd;
		region = regioningame.GetRegionInGame ().Region;
		regioningame.ChangeGunUse (gunNew);
		panelMenu.SetActive (true);
		paneDetailWeaspon.SetActive (false);
		paneWeaspon.SetActive (false);
		UIController.Instance.googleanaytic.LogEvent ("Used Gun", "Thay đổi súng bắn là:" + gunNew, "", 1);
//		if (region >= regionstart && region <= regionend) {
//		} else {
//			DialogStateGun ("" + gunNew + " không được sử dụng  ở region này ");
//		}
		CapNhatThongTin.Instance.Uploadfile ();
	}

	public void AssaultRifles ()
	{
//        ConfirmShowRegion(1)
		RessetTextWeasponDetail ();
		tassaultrifles.SetActive (true);
	}

	public void CrossBows ()
	{
//        ConfirmShowRegion(3);
		RessetTextWeasponDetail ();
		tcrossbow.SetActive (true);
	}

	public void Rifles ()
	{
//        ConfirmShowRegion(0);
		RessetTextWeasponDetail ();
		trifles.SetActive (true);

	}

	public void Shotgun ()
	{
//        ConfirmShowRegion(2);
		RessetTextWeasponDetail ();
		tshotgun.SetActive (true);
	}

	public void Specialweapon ()
	{
//        ConfirmShowRegion(4);
		RessetTextWeasponDetail ();
		tspecial.SetActive (true);
	}

	void ConfirmShowRegion (int pos)
	{
//        for (int i = 0; i < 9; i++)
//        {
//            if (region - 1 == i)
//            {
//                popupgun.transform.GetChild(i).gameObject.SetActive(true);
//            }
//            else
//            {
//                popupgun.transform.GetChild(i).gameObject.SetActive(false);
//            }
//        }
//        for (int i = 0; i < 5; i++)
//        {
//            if (pos == i)
//            {
//                popupgun.transform.GetChild(region - 1).GetChild(i).gameObject.SetActive(true);
//            }
//            else
//            {
//                popupgun.transform.GetChild(region - 1).GetChild(i).gameObject.SetActive(false);
//            }
//        }
	}

	public void Purchare ()
	{
		
//		Debug.Log ("Da mua roi");
		UIModelGun3D.SetActive (false);
		int regionend = rifles.GetRifles (gunNew).RegionEnd;
		int active = guningame.GetGunInGame (gunNew).Active;
		if (active.Equals (0) && regionend <= region) {
			if (rifles.GetRifles (gunNew).Gold > regioningame.GetRegionInGame ().Gold) {
				dialogbuyfail.SetActive (true);
				return;
			}
//			if (rifles.GetRifles (gunNew).Crystal > regioningame.GetRegionInGame ().Crystal) {
//				dialogbuyfail.SetActive (true);
//				return;
//			}
			if (rifles.GetRifles (gunNew).Silver > regioningame.GetRegionInGame ().Silver) {
				dialogbuyfail.SetActive (true);
				return;
			}
			// trừ tiền trong game
			regioningame.ChangeGoldSilveCrystal (rifles.GetRifles (gunNew).Gold, rifles.GetRifles (gunNew).Silver, 0);
			guningame.ChangeActive (gunNew);
			btgunclick.GetComponent<ButtonGunInShop> ().Reload ();
		} else {
			DialogStateGun ("" + gunNew + " unlock region " + rifles.GetRifles (gunNew).RegionEnd + " ");
		}
		CapNhatThongTin.Instance.Uploadfile ();
	}

	public void ConfirmUIGun3D ()
	{
		if (dialogbuyfail.activeSelf || dialogstategun.activeSelf) {
			UIModelGun3D.SetActive (false);
		}
	}

	public void PurchareCrystal ()
	{
		
		//		Debug.Log ("Da mua roi");
		UIModelGun3D.SetActive (false);
		int regionend = rifles.GetRifles (gunNew).RegionEnd;
		int active = guningame.GetGunInGame (gunNew).Active;
		if (active.Equals (0) && regionend <= region) {
			if (rifles.GetRifles (gunNew).Crystal > regioningame.GetRegionInGame ().Crystal) {
				dialogbuyfail.SetActive (true);
				return;
			}
			regioningame.ChangeGoldSilveCrystal (0, 0, rifles.GetRifles (gunNew).Crystal);
			// active gun
			guningame.ChangeActive (gunNew);
			btgunclick.GetComponent<ButtonGunInShop> ().Reload ();
		} else {
			DialogStateGun ("" + gunNew + " unlock region " + rifles.GetRifles (gunNew).RegionEnd + " ");
		}
		CapNhatThongTin.Instance.Uploadfile ();
	}

	public void DialogStateGun (string state)
	{
		dialogstategun.SetActive (true);
		dialogstategun.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = state;
	}

	public void Upgrade ()
	{
		
		PlayerPrefs.SetString ("EffectRegquire", "");
		PlayerPrefs.Save ();
		UpgradeManager.Instance.Init (gunNew);

	}

	public void ShowGunUI (string nameGun)
	{
		gun = nameGun;
		if (UIModelGun3D.transform.childCount > 0) {
			for (int i = 0; i < UIModelGun3D.transform.childCount; i++) {
				Destroy (UIModelGun3D.transform.GetChild (i).gameObject);
			}
		}
		if (Resources.Load ("GunUI/" + nameGun) == null) {
			return;
		}
		GameObject gunUI = Instantiate (Resources.Load ("GunUI/" + nameGun))as GameObject;
		gunUI.transform.SetParent (UIModelGun3D.transform);
//		StartCoroutine (ShowGunUI1 ());
	}

	IEnumerator ShowGunUI1 ()
	{
		yield return new WaitForEndOfFrame ();
		if (UIModelGun3D.transform.childCount > 0) {
			for (int i = 0; i < UIModelGun3D.transform.childCount; i++) {
				Destroy (UIModelGun3D.transform.GetChild (i).gameObject);
			}
		}
		if (Resources.Load ("GunUI/" + gun) != null) {
			GameObject gunUI = Instantiate (Resources.Load ("GunUI/" + gun))as GameObject;
			gunUI.transform.SetParent (UIModelGun3D.transform);
		}
	}

	public void HideModelUIGun ()
	{
		comingsoon.SetActive (false);
		if (UIModelGun3D.transform.childCount > 0) {
			for (int i = 0; i < UIModelGun3D.transform.childCount; i++) {
				Destroy (UIModelGun3D.transform.GetChild (i).gameObject);
			}
		}
	}

	public void ShowGunEnergy ()
	{
		if (panelUpgrade.activeSelf || paneDetailWeaspon.activeSelf) {
			ShowGunUI (gun);
		}
	}
}
