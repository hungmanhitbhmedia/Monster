using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIController : Singleton<UIController>
{

	public GameObject panelMap;
	public GameObject panelMenu;
	public GameObject internet;
	public GoogleAnalyticsV4 googleanaytic;

	void Start ()
	{
//		googleanaytic = GoogleAnalyticsV4.instance;
		if (googleanaytic != null) {
			googleanaytic.LogScreen ("Menu");
		}

		if (PlayerPrefs.GetInt ("Replay") == 1) {
			Map ();
		}
	}
	//ShowPanel
	public void ShowPanel (GameObject sPanel)
	{
		sPanel.SetActive (true);
	}
	//HidePanel
	public void HidePanel (GameObject hPanel)
	{
		hPanel.SetActive (false);
	}

	public void BackMenu ()
	{
		SceneManager.LoadScene ("Menu");
	}

	public void StartGame ()
	{
		EnergyManager.Instance.SubtractEnergy ();
		SceneManager.LoadScene ("GamePlay");

//		StartCoroutine (GetTimeBegin ());
//		StartCoroutine (Lo÷adGame ());
	}

	//	IEnumerator LoadGame ()
	//	{
	//		EnergyManager.Instance.SubtractEnergy ();
	//		yield return new WaitForEndOfFrame ();
	//	}

	//	IEnumerator  GetTimeBegin ()
	//	{
	////		string url = Constants.url_base + Constants.url_time_global;
	////		WWW www = new WWW (url);
	////		yield return www;
	////		int timecurrent = int.Parse (www.text);
	////		PlayerPrefs.SetInt ("Energy", timecurrent);
	////		PlayerPrefs.Save ();
	//		SceneManager.LoadScene ("GamePlay");
	//	}

	public void Menu ()
	{
		SceneManager.LoadScene ("Menu");
	}

	public void Map ()
	{
		UIController.Instance.ShowPanel (panelMap);
		UIController.Instance.HidePanel (panelMenu);
		LoadQuestInGame.Instance.DefaultState ();
		PlayerPrefs.SetInt ("Replay", 0);
		PlayerPrefs.Save ();
	}
	//	public void EffecShot (int index)
	//	{
	//		effectShot.SetActive (false);
	//		effectShot.SetActive (true);
	//		for (int i = 0; i < effectShot.transform.childCount; i++) {
	//			effectShot.transform.GetChild (i).gameObject.SetActive (false);
	//		}
	//		effectShot.transform.GetChild (index).gameObject.SetActive (true);
	//	}
	public void LogOut ()
	{
		if (!CapNhatThongTin.Instance.TestMang ()) {
			internet.SetActive (true);
			return;
		} else {
			CapNhatThongTin.Instance.Uploadfile ();
		}
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();
		PlayerPrefs.SetInt ("ResourceComplete", 1);
		PlayerPrefs.Save ();
		StartCoroutine (Loads ());
	}

	IEnumerator Loads ()
	{
		yield return new WaitForEndOfFrame ();
		SceneManager.LoadScene ("ManHinhDangNhap");
	}

	public void Restart ()
	{
		SceneManager.LoadScene ("ManHinhDangNhap");
	}
}
