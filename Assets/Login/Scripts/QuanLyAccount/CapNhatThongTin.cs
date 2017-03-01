using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;

//using BHMGameContent.Common;
using UnityToolbag;
using System.IO;
using System.ComponentModel;

public class CapNhatThongTin : Singleton<CapNhatThongTin>
{
	string tmp_folder_path = "";

	void Start ()
	{
		#if UNITY_EDITOR
		tmp_folder_path = Application.persistentDataPath + "/";
		#elif UNITY_ANDROID
			tmp_folder_path = Application.persistentDataPath;
		#elif UNITY_IPHONE
			tmp_folder_path = Application.persistentDataPath+"/";
		#else
			tmp_folder_path = Application.persistentDataPath;
		#endif

		if (Time.time == 0) {
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy (this.gameObject);
		}
		CreatePing ();
		//InvokeRepeating ("Timer", 1, 1);
	}

	//	void Timer ()
	//	{
	//		StartCoroutine (getTime ());
	//	}

	
	// Update is called once per frame
	bool isconnect;

	public	bool TestMang ()
	{
//		if (ping != null) {
//			bool stopCheck = true;
//			if (ping.isDone) {
//				if (ping.time >= 0) {
//					isconnect = true;
//				} else {
//					isconnect = false;
//				}
//			} else if (Time.time - pingStartTime < waitingTime) {
//				stopCheck = false;
//				isconnect = false;
//			} else {
//				isconnect = false;
//			}
//
//			if (stopCheck) {
//				ping = null;
//				CreatePing ();
//			}
//		}
		try {
			using (var client = new WebClient ())
			using (var stream = new WebClient ().OpenRead ("http://www.google.com")) {
				isconnect = true;
			}
		} catch {
			isconnect = false;
		}
		return isconnect;
	}

	float waitingTime = 2.0f;
	float pingStartTime;
	Ping ping;

	public void CreatePing ()
	{
//		ping = new Ping ("8.8.8.8");
//		pingStartTime = Time.time;
	}

	//	public void TestUploadFile ()
	//	{
	//		StartCoroutine (Confirm ());
	//		string localFileName = tmp_folder_path + "Database.db";
	//		string uploadURL = Constants.url_base + Constants.url_upload_sqlite;
	//		if (TestMang ()) {
	//			StartCoroutine (UploadFileSqlite (localFileName, uploadURL));
	//			PlayerPrefs.SetInt ("ConnectionInternet", 1);
	//			PlayerPrefs.Save ();
	//		} else {
	//			PlayerPrefs.SetInt ("ConnectionInternet", 2);
	//			PlayerPrefs.Save ();
	//		}
	//	}

	public void Uploadfile ()
	{
		StartCoroutine (Confirm ());
		string localFileName = tmp_folder_path + "Database.db";
		string uploadURL = Constants.url_base + Constants.url_upload_sqlite;
		PlayerPrefs.SetInt ("ConnectionInternet", 2);
		PlayerPrefs.Save ();
		StartCoroutine (UploadFileSqlite (localFileName, uploadURL));
	}

	public  bool HasConnection ()
	{
		try {
			using (var client = new WebClient ())
			using (var stream = new WebClient ().OpenRead ("http://www.google.com")) {
				return true;
			}
		} catch {
			return false;
		} 
	}

	public void CapNhat ()
	{
		string localFileName = tmp_folder_path + "Database.db";
		string uploadURL = Constants.url_base + Constants.url_upload_sqlite;
		StartCoroutine (UploadFileSqlite (localFileName, uploadURL));

	}

	IEnumerator UploadFileSqlite (string localFileName, string uploadURL)
	{
		Debug.Log ("Da toi day");
		WWW localFile = new WWW ("file:" + localFileName);
		yield return localFile;
		if (localFile.error == null) {
			
		} else {
			yield break; // stop the coroutine here
		}
		WWWForm postForm = new WWWForm ();
		postForm.AddField ("id_nhanvat", PlayerPrefs.GetInt (Constants.kIdAccount) + ".db");
		postForm.AddBinaryData ("theFile", localFile.bytes, localFileName, "db");
		WWW upload = new WWW (uploadURL, postForm);
		yield return upload;
		if (upload.error == null) {
			if (LoadRegionInGame.Instance != null) {
				Debug.Log ("upload success: " + upload.error);
			}
		} else {
			Debug.Log ("Error during upload: " + upload.error);
		}
	}

	IEnumerator Confirm ()
	{
		yield return new WaitForEndOfFrame ();
		LoadRegionInGame.Instance.Init ();
	}

}
