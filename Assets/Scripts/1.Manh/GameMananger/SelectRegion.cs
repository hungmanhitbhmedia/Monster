using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectRegion : Singleton<SelectRegion>
{
	public GameObject back;
	public GameObject next;



	public GameObject pRegion1;
	public GameObject pRegion2;
	public GameObject pRegion3;
	public GameObject pRegion4;
	public GameObject pRegion5;


	public GameObject[] gRegion1;
	public GameObject[] gRegion2;
	public GameObject[] gRegion3;
	public GameObject[] gRegion4;

	public Text txNameRegion;

	public	int regionCurrent;
	int regionCurrentMax;

	void Start ()
	{
		pRegion1.SetActive (false);
		pRegion2.SetActive (false);
		pRegion3.SetActive (false);
		pRegion4.SetActive (false);
		pRegion5.SetActive (false);
		RequestRegion ();
		//ShowRegionHien ();

	}

	void ShowRegionHien ()
	{
		if (regionCurrent == 1 && regionCurrentMax == 1) {
			back.SetActive (false);
			next.SetActive (false);
		}
		next.SetActive (false);
	}

	public void ShowRegionMax ()
	{
//		RequestRegion ();
		if (PlayerPrefs.HasKey ("RegionCurrent")) {
			regionCurrent = PlayerPrefs.GetInt ("RegionCurrent");
		} else {
			regionCurrent = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Region;
		}
		if (regionCurrent == 1 && regionCurrentMax == 1) {
			back.SetActive (false);
			next.SetActive (false);
		}
		if (regionCurrent == 1 && regionCurrentMax > 1) {
			back.SetActive (false);
			next.SetActive (true);
		}
		if (regionCurrent > 1 && regionCurrentMax == regionCurrent) {
			back.SetActive (true);
			next.SetActive (false);
		}
		if (regionCurrent > 1 && regionCurrentMax > regionCurrent) {
			back.SetActive (true);
			next.SetActive (true);
		}
		ConfirmRegion ();
//		if(regionCurrent==1)
//		back.SetActive (true);
//		ShowRegionHien ();
	}

	void RequestRegion ()
	{
		regionCurrentMax = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Region;
//		if (PlayerPrefs.HasKey ("RegionCurrent")) {
//			regionCurrent = PlayerPrefs.GetInt ("RegionCurrent");
//		} else {
//			regionCurrent = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Region;
//		}
//		regionCurrent = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Region;
		ShowRegionMax ();
		ConfirmRegion ();
	}

	public void Back ()
	{
		LoadQuestInGame.Instance.statehunt = LoadQuestInGame.StateHunt.None;
		LoadQuestInGame.Instance.txmode.text = "";
		LoadQuestInGame.Instance.txquest.text = "";
		LoadQuestInGame.Instance.txEx.text = "";
		LoadQuestInGame.Instance.txsilver.text = "";
		LoadQuestInGame.Instance.txgold.text = "";
		LoadQuestInGame.Instance.txcrystal.text = "";
		LoadQuestInGame.Instance.RessetBthunt ();
		regionCurrent--;
		if (regionCurrent < 1) {
			regionCurrent = 1;
			return;
		}
		PlayerPrefs.SetInt ("RegionCurrent", regionCurrent);
		PlayerPrefs.Save ();
		pRegion1.SetActive (false);
		pRegion2.SetActive (false);
		pRegion3.SetActive (false);
		pRegion4.SetActive (false);
		pRegion5.SetActive (false);
		ConfirmRegion ();
		if (regionCurrent == 1 && regionCurrentMax == 1) {
			back.SetActive (false);
		} else {
			if (regionCurrent == 1) {
				back.SetActive (false);
			} else {
				back.SetActive (true);
			}
		}
		if (regionCurrent < regionCurrentMax) {
			next.SetActive (true);
		}
	}

	public void Next ()
	{
		LoadQuestInGame.Instance.statehunt = LoadQuestInGame.StateHunt.None;
		LoadQuestInGame.Instance.txmode.text = "";
		LoadQuestInGame.Instance.txquest.text = "";
		LoadQuestInGame.Instance.txEx.text = "";
		LoadQuestInGame.Instance.txsilver.text = "";
		LoadQuestInGame.Instance.txgold.text = "";
		LoadQuestInGame.Instance.txcrystal.text = "";
		LoadQuestInGame.Instance.RessetBthunt ();

		regionCurrent++;
		if (regionCurrent > regionCurrentMax) {
			regionCurrent = regionCurrentMax;
			return;
		}
		PlayerPrefs.SetInt ("RegionCurrent", regionCurrent);
		PlayerPrefs.Save ();
		pRegion1.SetActive (false);
		pRegion2.SetActive (false);
		pRegion3.SetActive (false);
		pRegion4.SetActive (false);
		pRegion5.SetActive (false);
		ConfirmRegion ();
		if (regionCurrent >= regionCurrentMax) {
			next.SetActive (false);
		} else {
			next.SetActive (true);
		}
		if (regionCurrent > 1) {
			back.SetActive (true);
		}
	}

	public void ConfirmRegion ()
	{
		pRegion1.SetActive (false);
		pRegion2.SetActive (false);
		pRegion3.SetActive (false);
		pRegion4.SetActive (false);
		pRegion5.SetActive (false);
		txNameRegion.text = "Region " + regionCurrent + ": " + DataManager.Instance.connection.Table<NameRegion> ().Where (x => x.Region == regionCurrent).FirstOrDefault ().Name;
		if (regionCurrent >= regionCurrentMax) {
			if (regionCurrent == 1) {
				pRegion1.SetActive (true);
			}
			if (regionCurrent == 2) {
				pRegion2.SetActive (true);
			}
			if (regionCurrent == 3) {
				pRegion3.SetActive (true);
			}
			if (regionCurrent == 4) {
				pRegion4.SetActive (true);
			}
			if (regionCurrent == 5) {
				pRegion5.SetActive (true);
			}
			return;
		}
		switch (regionCurrent) {
		case 1:
			Region1 ();
//			pRegion1.SetActive (true);
			break;
		case 2:
			Region2 ();
//			pRegion2.SetActive (true);

			break;
		case 3:
			Region3 ();
//			pRegion3.SetActive (true);

			break;
		case 4:
			Region4 ();
//			pRegion4.SetActive (true);
			break;
		case 5:
			pRegion5.SetActive (true);
			break;
		}
	}

	public void Region1 ()
	{

		pRegion1.SetActive (true);
		for (int i = 0; i < gRegion1.Length; i++) {
			gRegion1 [i].gameObject.SetActive (true);
			gRegion1 [i].GetComponent<Button> ().enabled = false;
			gRegion1 [i].transform.GetComponent<QuestManager> ().x.gameObject.SetActive (true);
		}
	}

	public  void Region2 ()
	{
		pRegion2.SetActive (true);
		for (int i = 0; i < gRegion2.Length; i++) {
			gRegion2 [i].gameObject.SetActive (true);
			gRegion2 [i].GetComponent<Button> ().enabled = false;
			gRegion2 [i].transform.GetComponent<QuestManager> ().x.gameObject.SetActive (true);
		}
	}

	public void Region3 ()
	{
		pRegion3.SetActive (true);
		for (int i = 0; i < gRegion3.Length; i++) {
			gRegion3 [i].gameObject.SetActive (true);
			gRegion3 [i].GetComponent<Button> ().enabled = false;
			gRegion3 [i].transform.GetComponent<QuestManager> ().x.gameObject.SetActive (true);
		}
	}

	public void Region4 ()
	{
		pRegion4.SetActive (true);
//		Debug.Log ("da toi day");
		for (int i = 0; i < gRegion4.Length; i++) {
			gRegion4 [i].gameObject.SetActive (true);
			gRegion4 [i].GetComponent<Button> ().enabled = false;
			gRegion4 [i].transform.GetComponent<QuestManager> ().x.gameObject.SetActive (true);
		}
	}
}
