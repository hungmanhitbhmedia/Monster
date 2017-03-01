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

public class QuanLyDangKy : MonoBehaviour
{
	private static QuanLyDangKy instance;

	public static QuanLyDangKy Instance {
		get {
			if (instance == null)
				instance = new QuanLyDangKy ();
			return instance;
		}
	}

	public GoogleAnalyticsV4 googleananytic;

	public GameObject panelDialog;
	public Text txmessage;
	// Use this for initialization
	void Start ()
	{
//		googleananytic = GoogleAnalyticsV4.instance;
//		googleananytic.LogScreen ("Register");
		QuanLyFacebook.Instance.KhoiTaoFacebook ();
	}
	// Update is called once per frame
	void Update ()
	{
		
	}

	//constant
	public GameObject internet;

	//variable
	//UI
	public InputField input_username;
	public InputField input_email;
	public InputField input_password;
	public InputField input_confirm_pass;
	public GameObject confirm;
	//parameter

	//function - UI
	public void DoClose ()
	{
		SceneManager.LoadScene ("ManHinhDangNhap");
	}

	public void DangKy ()
	{ //đăng ký tài khoản
		if (!CapNhatThongTin.Instance.TestMang ()) {
			InternetNotification.Instance.ginternet.SetActive (true);
			return;
		}
		googleananytic.LogScreen ("Đăng kí bằng tài khoản mail");
		string tentaikhoan = input_username.text;
		string email = input_email.text;
		string matkhau = input_password.text;
		string confirm_matkhau = input_confirm_pass.text;
		//kiểm tra username
		int check_user = Constants.isValid_Username (tentaikhoan);
		if (check_user == 0) {
			//kiểm tra mail
			if (Constants.isValid_Mail (email)) {
				//kiểm tra mật khẩu 
				int check_pass = Constants.isValid_Password (matkhau);
				if (check_pass == 0) {
					//kiểm tra khớp mật khẩu hay không
					int value = matkhau.CompareTo (confirm_matkhau);
					if (value != 0 && matkhau != "") {
						DiaLog ("Incorrect password");
						return;
					}
//					if (!matkhau.Equals (confirm_matkhau, StringComparison.Ordinal)) { 
//						//Debug.LogError("Mật khẩu không khớp");
//						//Thông báo lỗi mật khẩu không khớp ở đây
//						//input_confirm_pass.ActivateInputField ();
//						return;
//					}
				} else {
					Input_Password_On_End_Edit (); 
					return;
				}

			} else {
				Input_Mail_On_End_Edit ();
				return;
			}
		} else {
			Input_Username_On_End_Edit ();
			return;
		}
		//bắt đầu đăng ký
		confirm.SetActive (true);
		var data = new NameValueCollection ();
		data ["m"] = Constants.Base64Encode (email);
		data ["u"] = Constants.Base64Encode (tentaikhoan);
		data ["p"] = Constants.Base64Encode (matkhau);
		data ["d"] = Constants.Base64Encode (Constants.device_id);
		data ["r"] = Constants.MD5 (email + "bhhunter");
		data ["t"] = "0";
		var wb = new WebClient ();
		wb.UploadValuesAsync (new Uri (Constants.url_base + Constants.url_dangky), data);
		wb.UploadValuesCompleted += (sender, e) => { 
			Dispatcher.Invoke (() => {
				confirm.SetActive (false);
				// this code is executed in main thread
				if (e.Error != null) {
					InternetNotification.Instance.ginternet.SetActive (true);

				} else {
//					Debug.Log ("Done ");
					string responseString = Encoding.Default.GetString (e.Result);
//					Debug.Log ("responseString: " + responseString);
					var dict = JSON.Parse (responseString);
					int status_email = dict ["status"] ["Email"].AsInt;
					int status_tentaikhoan = dict ["status"] ["User"].AsInt;
					int status_ketqua = dict ["success"].AsInt;
					if (status_ketqua == 1) {
//						Debug.Log ("Đăng ký thành công");
						PlayerPrefs.SetString (Constants.kTenTaiKhoan, tentaikhoan);
						PlayerPrefs.SetString (Constants.kMatKhau, matkhau);
						PlayerPrefs.Save ();
						//vào game luôn, bỏ qua phần đăng nhập
						DoClose ();
					} else {
						if (status_email == 0 && status_tentaikhoan == 0) {
//							Debug.Log ("Email và tên tài khoản đã tồn tại");
							DiaLog ("Invalid email address and username");
						} else if (status_email == 0) {
//							Debug.Log ("Email đã tồn tại");
							DiaLog ("Invalid email address");
						} else if (status_tentaikhoan == 0) {
//							Debug.Log ("Tên tài khoản đã tồn tại");
							DiaLog ("Invalid username");
						}
					}
				}
			});
		};
	}

	public void DangKyBangFacebook ()
	{
		if (!CapNhatThongTin.Instance.TestMang ()) {
			InternetNotification.Instance.ginternet.SetActive (true);

			return;
		}
		googleananytic.LogScreen ("Đăng kí bằng facebook");
		confirm.SetActive (true);
		QuanLyFacebook.Instance.LoginFacebook ();
	}
	//check input username + mail + password
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
				DiaLog ("Your username is not long enough ");
			}
			break;
		case 2:
			{
				//show error chứa ký tự đặc biệt 
//				input_username.ActivateInputField ();
				DiaLog ("Username must not contain special characters");
			}
			break;
		default:
			{
			}
			break;
		}
	}

	public void Input_Mail_On_End_Edit ()
	{
		string email = input_email.text;
		if (!Constants.isValid_Mail (email)) {
			//Debug.LogError("Email không hợp lệ");
			//input_email.ActivateInputField ();
			DiaLog ("Invalid email address");
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
				//input_password.ActivateInputField ();
				DiaLog ("Your password is not long enough");
			}
			break;
		case 2:
			{
				//show error chứa ký tự đặc biệt 
				//input_password.ActivateInputField ();
				DiaLog ("Password must not contain special characters");
			}
			break;
		default:
			{
			}
			break;
		}
	}

	public void DiaLog (string message)
	{
		txmessage.text = message;
		panelDialog.SetActive (true);
	}
}
