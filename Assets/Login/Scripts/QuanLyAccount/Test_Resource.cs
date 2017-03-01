using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System;
using System.ComponentModel;
using UnityEngine.UI;
using UnityToolbag;
using System.Linq;
using UnityEngine.SceneManagement;

public class Test_Resource : MonoBehaviour
{
	public Image gress;
	public Text txgress;
	float bytesIn;
	float totalBytes;
	bool isComplete;
	//	bool isComplete1;
	// Use this for initialization
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
//		Debug.Log ("Vao Man hinh download");
		StartCoroutine (TestLoadFileNhaChinh ());
		InvokeRepeating ("ProgressBar", 0.5f, 0.5f);
		InvokeRepeating ("ProcessZipFile", 0.6f, 0.6f);
	}

	//function UI
	public void TestUploadFile ()
	{
		string localFileName = Application.dataPath + "/StreamingAssets/abc.db";
		string uploadURL = Constants.url_base + Constants.url_upload_sqlite;
		StartCoroutine (UploadFileSqlite (localFileName, uploadURL));
	}

	IEnumerator UploadFileSqlite (string localFileName, string uploadURL)
	{
		WWW localFile = new WWW ("file:" + localFileName);
		yield return localFile;
		if (localFile.error == null)
			Debug.Log ("Loaded file successfully");
		else {
//			Debug.Log ("Open file error: " + localFile.error);
			yield break; // stop the coroutine here
		}
		WWWForm postForm = new WWWForm ();
		postForm.AddField ("id_nhanvat", "14");
		postForm.AddBinaryData ("theFile", localFile.bytes, localFileName, "db");
		WWW upload = new WWW (uploadURL, postForm);        
		yield return upload;
		if (upload.error == null)
			Debug.Log ("upload done :" + upload.text);
		else
			Debug.Log ("Error during upload: " + upload.error);
	}


	//

	IEnumerator TestLoadFileNhaChinh ()
	{ 
		yield return new WaitForEndOfFrame ();
		var wb = new WebClient ();
		wb.DownloadProgressChanged += new DownloadProgressChangedEventHandler (client_DownloadProgressChanged);
		wb.DownloadFileAsync (new Uri (Constants.url_download_resource + Constants.url_dl_all), tmp_folder_path + "Deer_Hunter.zip");
//		wb.DownloadFileCompleted += new AsyncCompletedEventHandler (client_DownloadFileCompleted);

		wb.DownloadFileCompleted += (sender, e) => {
			Dispatcher.Invoke (() => {
				// this code is executed in main thread
				if (e.Error != null) {
					//					Debug.Log ("Error: Re-download");
					//					Directory.Delete (duongDanFileAnh);
					//					DownloadManHinh (tenManHinhDown);
				} else {
					string filePath = tmp_folder_path + "Deer_Hunter.zip";
					string exportLocation = tmp_folder_path + "Deer_Hunter";
//					Debug.Log ("Download success. Unzip file");
					ZipUtil.Unzip (filePath, exportLocation);
					File.Delete (filePath);
					isComplete = true;
				}
			});
		};
	}

	void client_DownloadProgressChanged (object sender, DownloadProgressChangedEventArgs e)
	{
		bytesIn = float.Parse (e.BytesReceived.ToString ());
		totalBytes = float.Parse (e.TotalBytesToReceive.ToString ());
	}

	void client_DownloadFileCompleted (object sender, AsyncCompletedEventArgs e)
	{
//		Debug.Log ("Done ");
//		Debug.Log ("tải file thành công. bắt đầu giải nén");
		string filePath = tmp_folder_path + "Deer_Hunter.zip";
		string exportLocation = tmp_folder_path + "Deer_Hunter";
		ZipUtil.Unzip (filePath, exportLocation);
		File.Delete (filePath);
		isComplete = true;

	}


	void ProgressBar ()
	{
		if (isComplete) {
			PlayerPrefs.SetInt ("DownloadResourceComplete", 1);
			PlayerPrefs.Save ();
		}

		if (PlayerPrefs.GetInt ("DownloadResourceComplete") == 1) {

			PlayerPrefs.SetInt ("ResourceComplete", 1);
			PlayerPrefs.Save ();
			SceneManager.LoadScene ("Menu");
			CancelInvoke ();
			return;
		}
		gress.fillAmount = bytesIn / totalBytes;
		float tmo = bytesIn / totalBytes * 100;
		int tmp = (int)tmo;
		txgress.text = tmp + "%";
	}

	//	IEnumerator DownLoadDataBase ()
	//	{
	//		yield return new WaitForEndOfFrame ();
	//		var wb = new WebClient ();
	//		wb.DownloadFileAsync (new Uri (Constants.url_base), tmp_folder_path + "Database.db");
	//	}
}
