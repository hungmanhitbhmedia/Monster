using UnityEngine;
using System.Collections;

public class Tutorial : Singleton<Tutorial>
{
	public GameObject map;
	public GameObject hunt;
	public GameObject start;
	public GameObject confirmupgrade;
	public GameObject power;
	public GameObject silver;

	void Start ()
	{
		RegionInGame regioningame = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		int region = regioningame.Region;
		int quest = regioningame.QuestNormal;
		if (region > 1) {
			return;
		}
		if (quest > 1) {
			return;
		}
		if (PlayerPrefs.GetInt ("Tutorial") == 0) {
			map.SetActive (true);
		}
	}

	public void BtMap ()
	{
		PlayerPrefs.SetInt ("Tutorial", 1);
		PlayerPrefs.Save ();
		if (map.activeSelf) {
			hunt.SetActive (true);
		}
		map.SetActive (false);
	}

	public void BtHunt ()
	{
		if (hunt.activeSelf) {
			start.SetActive (true);
		}
		hunt.SetActive (false);
	}

	public void ConfirmUpgrade ()
	{
		RegionInGame regioningame = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		int region = regioningame.Region;
		int quest = regioningame.QuestNormal;
		if (region != 1 && quest != 3) {
			return;
		}
		if (PlayerPrefs.HasKey ("ConfirmUpgrade")) {
			return;
		}
		confirmupgrade.SetActive (true);
		PlayerPrefs.SetInt ("ConfirmUpgrade", 1);
		PlayerPrefs.Save ();
	}

	public void BtPower ()
	{
		if (confirmupgrade.activeSelf) {
			power.SetActive (true);
		}
		confirmupgrade.SetActive (false);
	}

	public void BtSilver ()
	{
		if (power.activeSelf) {
			silver.SetActive (true);
		}
		power.SetActive (false);
	}
		
}
