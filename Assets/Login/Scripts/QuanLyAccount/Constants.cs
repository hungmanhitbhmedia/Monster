using UnityEngine;
using System.Collections;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Text.RegularExpressions;

public static class Constants
{
	public const string kTenTaiKhoan = "kTenTaiKhoan";
	public const string kMatKhau = "kMatKhau";
	public const string kFacebookUser = "kFacebookUser";
	public const string kIdAccount = "kIdAccount";


	public static string device_id = SystemInfo.deviceUniqueIdentifier;
	//
	public const string url_base = "http://210.211.97.114:84/apphunter/index.php";
	public const string url_dangky = "?task=register";
	//QuanLyDangKy->DangKy()
	public const string url_dangnhap = "?task=login";
	//QuanLyDangNhap->BatDauDangNhap()
	public const string url_dangxuat = "?task=logout";
	//QuanLyDangNhap->DangXuat()
	public const string url_quenmatkhau = "?task=password_forgot";
	//QuanLyQuenMatKhau->HienMatKhau()
	public const string url_doi_matkhau = "?task=password_change";
	//Test_DoiMatKhau->TestDoiMatKhau()
	public const string url_upload_sqlite = "?task=upload_sqlite";
	public const string url_time_global = "?task=get_time_stamp";
	//Test_Resource->TestUploadFile()
	//
	public const string url_download_resource = "http://210.211.97.114:84/apphunter/resources/";
	public const string url_dl_all = "Deer_Hunter.zip";

	public const string url_database = "";


	//check internet connection
	public static bool isInternetConnection ()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			//Debug.Log ("khong co mang");
			return false;
		}
		try {
			using (var client = new WebClient ())
			using (var stream = client.OpenRead ("http://www.google.com")) {
				return true;
			}
		} catch {
			return false;
		}
	}


	//encode base64
	public static string Base64Encode (string plainText)
	{
		var plainTextBytes = System.Text.Encoding.UTF8.GetBytes (plainText);
		return System.Convert.ToBase64String (plainTextBytes);
	}
	//decode base64
	public static string Base64Decode (string base64EncodedData)
	{ 
		var base64EncodedBytes = System.Convert.FromBase64String (base64EncodedData);
		return System.Text.Encoding.UTF8.GetString (base64EncodedBytes);
	}


	//md5 string
	public static string MD5 (string plainText)
	{ 
		byte[] encodedPassword = new UTF8Encoding ().GetBytes (plainText); // byte array representation of that string
		byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName ("MD5")).ComputeHash (encodedPassword); // need MD5 to calculate the hash
		string encoded = BitConverter.ToString (hash) // string representation (similar to UNIX format)
			.Replace ("-", string.Empty) // without dashes
			.ToLower (); // make lowercase
		return encoded;
	}


	//check mail valid
	public static bool isValid_Mail (string email)
	{
		bool isEmail = Regex.IsMatch (email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
		return isEmail;
	}
	//check user valid
	public static int isValid_Username (string tentaikhoan)
	{
		//kiểm tra độ dài tài khoản
		if (tentaikhoan.Length < 4 || tentaikhoan.Length > 12) { 
			//Debug.LogError("Tên tài khoản chỉ trong khoảng 4-12 ký tự");
			return 1;
		}
		//kiểm tra ký tự đặc biệt
		if (!Regex.IsMatch (tentaikhoan, @"^[a-zA-Z0-9_]+$")) { 
			//Debug.LogError("Tên tài khoản có chứa ký tự đặc biệt");
			return 2;
		}
		return 0;
	}
	//check password valid
	public static int isValid_Password (string matkhau)
	{
		//kiểm tra độ dài mật khẩu 
		if (matkhau.Length < 4 || matkhau.Length > 16) { 
			//Debug.LogError("Mật khẩu chỉ trong khoảng 4-16 ký tự");
			return 1;
		}
		//kiểm tra ký tự đặc biệt
		if (!Regex.IsMatch (matkhau, @"^[a-zA-Z0-9_]+$")) { //kiểm tra ký tự đặc biệt
			//Debug.LogError("Mật khẩu có chứa ký tự đặc biệt");
			return 2;
		}
		return 0;
	}


}
