using UnityEngine;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Net;
using System.Collections.Specialized;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityToolbag;
using System.IO;
using System.ComponentModel;
using System.Linq;

public class QuanLyDangNhap : Singleton<QuanLyDangNhap>
{

	// Use this for initialization
	string tmp_folder_path = "";
	bool isComplete;

	public GameObject panelDialog;
	public GameObject popup;
	public GameObject connecting;
	public GoogleAnalyticsV4 googleananytic;
	public Text txmessage;

	void Start ()
	{
//		googleananytic = GoogleAnalyticsV4.instance;
		googleananytic.StartSession ();
		googleananytic.LogScreen ("Login");
//		googleananytic.Log
		PlayerPrefs.SetInt ("Replay", 0);
		PlayerPrefs.Save ();
		#if UNITY_EDITOR
		tmp_folder_path = Application.persistentDataPath + "/";
		#elif UNITY_ANDROID
		tmp_folder_path = Application.persistentDataPath;
		#elif UNITY_IPHONE
		tmp_folder_path = Application.persistentDataPath+"/";
		#else
		tmp_folder_path = Application.persistentDataPath;
		#endif


//		if (!CapNhatThongTin.Instance.TestMang ()) {
//
//			return;
//		}
		InvokeRepeating ("VaoGame", 1, 1);
		if (!isDangNhap) { //tự động đăng nhập khi vào game
			string tentaikhoan = PlayerPrefs.GetString (Constants.kFacebookUser);
			if (tentaikhoan != null && tentaikhoan.Length > 0) {
				StartCoroutine (UpdateData ());
			} else {
				tentaikhoan = PlayerPrefs.GetString (Constants.kTenTaiKhoan);
				if (tentaikhoan != null && tentaikhoan.Length > 0) {
					StartCoroutine (UpdateData ());
				}
			}
			Reset ();

		}
	}

	IEnumerator UpdateData ()
	{
		string localFileName = tmp_folder_path + "Database.db";
		string uploadURL = Constants.url_base + Constants.url_upload_sqlite;

		WWW localFile = new WWW ("file:" + localFileName);
		yield return localFile;
		if (localFile.error == null)
			Debug.Log ("Loaded file successfully");
		else {
			Debug.Log ("Open file error: " + localFile.error);
			Reset ();
			yield break; // stop the coroutine here
		}
		WWWForm postForm = new WWWForm ();
		postForm.AddField ("id_nhanvat", PlayerPrefs.GetInt (Constants.kIdAccount) + ".db");
		postForm.AddBinaryData ("theFile", localFile.bytes, localFileName, "db");
		WWW upload = new WWW (uploadURL, postForm);
		yield return upload;
		if (upload.error == null) {
			//PlayerPrefs.SetInt ("ConnectionInternet", 1);
			//PlayerPrefs.Save ();
			string tentaikhoan = PlayerPrefs.GetString (Constants.kFacebookUser);
			if (tentaikhoan != null && tentaikhoan.Length > 0) {
				DangNhapBangFacebook ();
			} else {
				tentaikhoan = PlayerPrefs.GetString (Constants.kTenTaiKhoan);
				if (tentaikhoan != null && tentaikhoan.Length > 0) {
					DangNhapBangTaiKhoan ();
				}
			}
		} else {
			InternetNotification.Instance.ginternet.SetActive (true);
//			Reset ();
//			Debug.Log ("Error during upload: " + upload.error);
		}
	}


	//variable
	//UI
	public InputField input_username;
	public InputField input_password;
	//parameter
	public bool isDangNhap = false;

	//function - UI
	public void DangKy ()
	{ //đăng ký tài khoản
		if (!CapNhatThongTin.Instance.TestMang ()) {
			InternetNotification.Instance.ginternet.SetActive (true);
			return;
		}
		SceneManager.LoadScene ("ManHinhDangKy");
	}

	public void DangNhap ()
	{ //đăng nhập
//		if (PlayerPrefs.HasKey ("DownloadResourceComplete")) {
//			return;
//		}
//		string tentaikhoan1 = PlayerPrefs.GetString (Constants.kFacebookUser);
//		if (tentaikhoan1 != null && tentaikhoan1.Length > 0) {
//			return;
//		} else {
//			tentaikhoan1 = PlayerPrefs.GetString (Constants.kTenTaiKhoan);
//			if (tentaikhoan1 != null && tentaikhoan1.Length > 0) {
//				return;
//			}
//		}
		if (!CapNhatThongTin.Instance.TestMang ()) {
			InternetNotification.Instance.ginternet.SetActive (true);
			return;
		}
		string tentaikhoan = input_username.text;
		string matkhau = input_password.text;
		//kiểm tra username
		int check_user = Constants.isValid_Username (tentaikhoan);
		if (check_user == 0) {
			//kiểm tra password
			int check_pass = Constants.isValid_Password (matkhau);
			if (check_pass > 0) {
				Input_Password_On_End_Edit ();
				return;
			}
		} else {
			Input_Username_On_End_Edit ();
			return;
		}
		//bắt đầu đăng nhập
		string types = "0";
		BatDauDangNhap (tentaikhoan, matkhau, types);
	}


	public void QuenMatKhau ()
	{
		if (!CapNhatThongTin.Instance.TestMang ()) {
			InternetNotification.Instance.ginternet.SetActive (true);
			return;
		}
		SceneManager.LoadScene ("ManHinhQuenMatKhau");
	}
	//void - Selector
	public void DangNhapBangTaiKhoan ()
	{
		string tentaikhoan = PlayerPrefs.GetString (Constants.kTenTaiKhoan);
		string matkhau = PlayerPrefs.GetString (Constants.kMatKhau);
		string types = "0";
		BatDauDangNhap (tentaikhoan, matkhau, types);
	}

	public void DangNhapBangFacebook ()
	{
		string tentaikhoan = PlayerPrefs.GetString (Constants.kFacebookUser);
		string matkhau = "";
		string types = "1";
		BatDauDangNhap (tentaikhoan, matkhau, types);
	}

	void BatDauDangNhap (string tentaikhoan, string matkhau, string types)
	{
		//bắt đầu đăng nhập
		var data = new NameValueCollection ();
		data ["u"] = Constants.Base64Encode (tentaikhoan);
		data ["p"] = Constants.Base64Encode (matkhau);
		data ["d"] = Constants.Base64Encode (Constants.device_id);
		data ["t"] = types;
		data ["r"] = Constants.MD5 (tentaikhoan + "bhhunter");
		var wb = new WebClient ();
		wb.UploadValuesAsync (new Uri (Constants.url_base + Constants.url_dangnhap), data);
		wb.UploadValuesCompleted += (sender, e) => {
			Dispatcher.Invoke (() => {
				// this code is executed in main thread
				if (e.Error != null) {
					InternetNotification.Instance.ginternet.SetActive (true);
				} else {

//					Debug.Log ("Done ");
					popup.SetActive (false);
					connecting.SetActive (true);
					string responseString = Encoding.Default.GetString (e.Result);
//					Debug.Log ("responseString: " + responseString);
					var dict = JSON.Parse (responseString);
					int status_ketqua = dict ["success"].AsInt;
					if (status_ketqua == 1) {

//						Debug.Log ("Đăng nhập thành công");
						int id_acc = dict ["id_acc"].AsInt;
						PlayerPrefs.SetInt (Constants.kIdAccount, id_acc);
						PlayerPrefs.Save ();
						isDangNhap = true;
//						VaoGame ();
						//lưu tài khoản
						PlayerPrefs.SetString (Constants.kTenTaiKhoan, tentaikhoan);
						PlayerPrefs.SetString (Constants.kMatKhau, matkhau);
						PlayerPrefs.Save ();
//						Debug.Log ("//////" + PlayerPrefs.GetInt (Constants.kIdAccount));
						int taomoi = dict ["taomoi"].AsInt;
						if (taomoi == 1) {
//							Debug.Log ("tao moi");
							DatabaseDownLoadDataBase ("http://210.211.97.114:84/apphunter/database/hunter_base.db");
							//tải file database base ở đây (http://210.211.97.114:84/apphunter/database/hunter_base.db)
						} else {
//							Debug.Log ("Da co roi");
							DatabaseDownLoadDataBase ("http://210.211.97.114:84/apphunter/database/" + PlayerPrefs.GetInt (Constants.kIdAccount) + ".db");
//							đã tồn tại rồi, down file database tương ứng của nhân vật (http://210.211.97.114:84/apphunter/database/"++".db)
						}
					} else {
//						Debug.Log ("Đăng nhập thất bại");
						Reset ();
						connecting.SetActive (false);
						Dialog ("Incorrect Username/Password");
					}
				}
			});
		};
	}

	void DatabaseDownLoadDataBase (string url)
	{
		var wb = new WebClient ();
		wb.DownloadFileAsync (new Uri (url), tmp_folder_path + "Database.db");
		wb.DownloadFileCompleted += new AsyncCompletedEventHandler (client_DownloadFileCompleted);
	}

	void client_DownloadFileCompleted (object sender, AsyncCompletedEventArgs e)
	{
		isComplete = true;
	}

	void VaoGame ()
	{
		if (!isComplete) {
			return;
		}
		//down resource ở đây
//		Debug.Log ("Dang Vao game");
		if (PlayerPrefs.GetInt ("DownloadResourceComplete") == 0 && !PlayerPrefs.HasKey ("ResourceComplete")) {
			SceneManager.LoadScene ("DownloadResoures");
		} else {
			SceneManager.LoadScene ("Menu");
		}
	}
	//
	//	public void DangXuat() {
	//		//bool isFacebookUser = true;
	//		string tentaikhoan = PlayerPrefs.GetString(Constants.kFacebookUser);
	//		if (tentaikhoan != null && tentaikhoan.Length > 0) {
	//		} else {
	//			tentaikhoan = PlayerPrefs.GetString(Constants.kTenTaiKhoan);
	//			//isFacebookUser = false;
	//		}
	//		//bắt đầu đăng xuất
	//		var data = new NameValueCollection();
	//		data["u"] = Constants.Base64Encode(tentaikhoan);
	//		data["r"] = Constants.MD5(tentaikhoan + "bhhunter");
	//		var wb = new WebClient ();
	//		wb.UploadValuesAsync (new Uri(Constants.url_base+Constants.url_dangxuat), data);
	//		wb.UploadValuesCompleted += (sender, e) => {
	//			Dispatcher.Invoke(() => {
	//				// this code is executed in main thread
	//				if (e.Error != null) {
	//					Debug.Log ("Error ");
	//				} else {
	//					Debug.Log ("Done ");
	//					string responseString = Encoding.Default.GetString (e.Result);
	//					Debug.Log ("responseString: " + responseString);
	//					var dict = JSON.Parse (responseString);
	//					int status_ketqua = dict ["status"].AsInt;
	//					if (status_ketqua == 1) {
	//						Debug.Log ("Đăng xuất thành công");
	//						isDangNhap = false;
	//						PlayerPrefs.SetString (Constants.kFacebookUser, null);
	//						PlayerPrefs.SetString (Constants.kTenTaiKhoan, null);
	//						PlayerPrefs.SetString (Constants.kMatKhau, null);
	//						PlayerPrefs.Save ();
	//						SceneManager.LoadScene ("ManHinhDangNhap");
	//					} else {
	//						Debug.Log ("Đăng xuất thất bại");
	//					}
	//				}
	//			});
	//		};
	//	}
	//check input username + password
	public void Input_Username_On_End_Edit ()
	{
		string tentaikhoan = input_username.text;
		int check = Constants.isValid_Username (tentaikhoan);
		switch (check) {
		case 0:
			{
				//thành công
			}
			break;
		case 1:
			{
				//show error độ dài username
//				input_username.ActivateInputField ();
				Dialog ("Your username is not long enough ");
			}
			break;
		case 2:
			{
				//show error chứa ký tự đặc biệt
//				input_username.ActivateInputField ();
				Dialog ("Username must not contain special characters");
			}
			break;
		default:
			{
			}
			break;
		}
	}

	public void Input_Password_On_End_Edit ()
	{
		string matkhau = input_password.text;
		int check = Constants.isValid_Password (matkhau);
		switch (check) {
		case 0:
			{
				//thành công
			}
			break;
		case 1:
			{
				//show error độ dài password
//				input_password.ActivateInputField ();
				Dialog ("Your password is not long enough");
			}
			break;
		case 2:
			{
				//show error chứa ký tự đặc biệt
//				input_password.ActivateInputField ();
				Dialog ("Password must not contain special characters");
			}
			break;
		default:
			{
			}
			break;
		}
	}

	void Dialog (string message)
	{
		panelDialog.SetActive (true);
		txmessage.text = message;
	}

	public void Reset ()
	{
		popup.SetActive (true);
//		if (PlayerPrefs.HasKey ("ResourceComplete")) {
//			PlayerPrefs.DeleteAll ();
//			PlayerPrefs.SetInt ("ResourceComplete", 1);
//			PlayerPrefs.Save ();
//		} else {
//			PlayerPrefs.DeleteAll ();
//			PlayerPrefs.Save ();
//		}

	}
}
