using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Net;

public class EnergyManager : Singleton<EnergyManager>
{

	//	public Text[] txenergy;
	public int energycurrent;
	private int energyMax = 10;

	RegionInGame regioningame;

	void Start ()
	{
//		PlayerPrefs.DeleteKey ("Energy");
//		PlayerPrefs.SetString ("Energy", DateTime.Now.AddSeconds (500).ToString ());
//		PlayerPrefs.Save ();
//		Debug.Log (".............." + this.gameObject.name);
		regioningame = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		energycurrent = regioningame.Energy;
		if (PlayerPrefs.HasKey ("Energy")) {
			InvokeRepeating ("CheckEnergy", 1, 1);
		} else {
			if (regioningame.Energy < 10) {
				PlayerPrefs.SetString ("Energy", DateTime.Now.ToString ());
				PlayerPrefs.Save ();
				InvokeRepeating ("CheckEnergy", 1, 1);
			}
		}
	}

	public void SubtractEnergy ()
	{
		RegionInGame re = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		re.Energy = re.Energy - 1;
		PlayerPrefs.SetString ("Energy", System.DateTime.Now.ToString ());
		PlayerPrefs.Save ();
		DataManager.Instance.connection.Update (re);
//		CapNhatThongTin.Instance.TestUploadFile ();
	}

	DateTime timecurrent;
	DateTime timebefor;

	void CheckEnergy ()
	{
		if (regioningame.Energy >= 10) {
			return;
		}
		timebefor = System.DateTime.Parse (PlayerPrefs.GetString ("Energy"));
		timecurrent = DateTime.Now;
		TimeSpan tmptspan = timecurrent.Subtract (timebefor);
		int tmp = tmptspan.Days * 86400 + tmptspan.Hours * 3600 + tmptspan.Minutes * 60 + tmptspan.Seconds;
//		Debug.Log ("tmp" + tmp);
		int addenergy = tmp / 600;
		if (addenergy <= 0) {
			return;
		}
	
		int tmpspan = tmp - (addenergy * 300);
		DateTime timebeforcaculator = timecurrent.AddSeconds (-tmpspan);
		PlayerPrefs.SetString ("Energy", timebeforcaculator.ToString ());
		PlayerPrefs.Save ();
		if (addenergy + energycurrent >= 10) {
			regioningame.Energy = 10;
			DataManager.Instance.connection.Update (regioningame);
			CapNhatThongTin.Instance.Uploadfile ();
		} else {
			regioningame.Energy = regioningame.Energy + addenergy;
			DataManager.Instance.connection.Update (regioningame);
			CapNhatThongTin.Instance.Uploadfile ();
		}
		regioningame = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
	}

	public void  BuyEnergy (int number)
	{
		regioningame.Energy = regioningame.Energy + number;
		regioningame = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		DataManager.Instance.connection.Update (regioningame);
		CapNhatThongTin.Instance.Uploadfile ();
	}


}
