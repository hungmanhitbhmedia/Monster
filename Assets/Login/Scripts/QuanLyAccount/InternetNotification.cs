using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InternetNotification : Singleton<InternetNotification>
{
	public GameObject ginternet;
	float waitingTime = 2.0f;
	float waitingTimeCurrent = 5.0f;
	float pingStartTime;
	float pingTimeCurrent;
	bool isInternet;
	Ping ping;
	bool connectI;

	void Start ()
	{
		//
		//CreatePing ();
	}

	//	ConnectionTesterStatus connectionTestResult = ConnectionTesterStatus.Undetermined;

	//	public bool CheckInternet ()
	//	{
	////		connectionTestResult = Network.TestConnection ();
	////		switch (connectionTestResult) {
	////		case ConnectionTesterStatus.Error:
	////			connectI = false;
	////			break;
	////		case ConnectionTesterStatus.Undetermined:
	////			connectI = true;
	////			break;
	////		case ConnectionTesterStatus.PublicIPIsConnectable:
	////			connectI = true;
	////			break;
	////		}
	////		return connectI;
	//	}


	public void CreatePing ()
	{
		ping = new Ping ("8.8.8.8");
		pingStartTime = Time.time;
	}

	private void InternetIsNotAvailable ()
	{
		if (isInternet) {
			isInternet = false;
			StartCoroutine (NoInternet ());
		}
//		Debug.Log ("No Internet " + Time.time);
	}

	IEnumerator NoInternet ()
	{
		yield return new WaitForSeconds (5);
		if (!isInternet)
			ShowReload ();
	}

	private void InternetAvailable ()
	{
		if (!isInternet) {
			isInternet = true;
			HideReload ();
		}
//		Debug.Log ("Internet isvailabled" + Time.time);
	}

	public void ShowReload ()
	{
		ginternet.SetActive (true);
		Time.timeScale = 0;
	}

	public void HideReload ()
	{
		ginternet.SetActive (false);
		Time.timeScale = 1;
	}

	public void ReStart ()
	{
		SceneManager.LoadScene ("ManHinhDangNhap");
	}
}
