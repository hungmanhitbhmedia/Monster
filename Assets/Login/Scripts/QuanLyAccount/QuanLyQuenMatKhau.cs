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
using System.Text.RegularExpressions;
using UnityToolbag;

public class QuanLyQuenMatKhau : MonoBehaviour
{
	private static QuanLyQuenMatKhau instance;

	public static QuanLyQuenMatKhau Instance {
		get {
			if (instance == null)
				instance = new QuanLyQuenMatKhau ();
			return instance;
		}
	}
	// Use this for initialization
	void Start ()
	{
//		googleananytic = GoogleAnalyticsV4.instance;
		googleananytic.LogScreen ("Quên mật khẩu ");
	}
	// Update is called once per frame
	void Update ()
	{
	}

	//constant
	public GoogleAnalyticsV4 googleananytic;
	//variable
	//UI
	public InputField input_username;
	public InputField input_email;
	public Text txdialog;
	public GameObject internet;
	public GameObject confirm;

	//parameter
	string tentaikhoan;
	string email;

	//function - UI
	public void DoClose ()
	{
		SceneManager.LoadScene ("ManHinhDangNhap");

//		Invoke (Loads, "1");
	}




	public void HienMatKhau ()
	{ //gửi email chứa mật khẩu và tài khoản về email đăng ký 
		if (!CapNhatThongTin.Instance.TestMang ()) {
			InternetNotification.Instance.ginternet.SetActive (true);
			googleananytic.LogEvent ("Quên mật khẩu", "Gửi mật khẩu thất bại ", "", 1);

			return;
		}
		if (input_username.text == null || input_email.text == null) {
			googleananytic.LogEvent ("Quên mật khẩu", "Gửi mật khẩu thất bại ", "", 1);

			return;
		}
		googleananytic.LogEvent ("Quên mật khẩu", "Gửi mật khẩu thành công", "", 1);
		confirm.SetActive (true);
		tentaikhoan = input_username.text;
		email = input_email.text;
		//bắt đầu xử lý tìm mật khẩu 
		var data = new NameValueCollection ();
		data ["m"] = Constants.Base64Encode (email);
		data ["u"] = Constants.Base64Encode (tentaikhoan);
		data ["r"] = Constants.MD5 (email + "bhhunter");
		var wb = new WebClient ();
		wb.UploadValuesAsync (new Uri (Constants.url_base + Constants.url_quenmatkhau), data);
		wb.UploadValuesCompleted += (object sender, UploadValuesCompletedEventArgs e) => {
			Dispatcher.Invoke (() => {
				// this code is executed in main thread
				if (e.Error != null) {
					InternetNotification.Instance.ginternet.SetActive (true);

				} else {
//					Debug.Log ("Done ");
					string responseString = Encoding.Default.GetString (e.Result);
//					Debug.Log ("responseString: " + responseString);
					var dict = JSON.Parse (responseString);
					int status_ketqua = dict ["success"].AsInt;
					if (status_ketqua == 1) {
//						Debug.Log ("Email password đã được gửi về mail của bạn, nếu trong hộp thư đến không có, vui lòng check trong hộp Spam");
						txdialog.text = "Your password has been sent to your email address. If you don't receive the e-mail, prlease check your spam folde";
						txdialog.color = Color.green;
					} else {
//						Debug.Log ("username/email không đúng");
						txdialog.text = "Incorrect Username/Email Address";
						txdialog.color = Color.red;
					}
				}
				confirm.SetActive (false);
			});
		};
	}
}
