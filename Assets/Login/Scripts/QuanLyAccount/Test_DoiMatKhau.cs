using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Specialized;
using System.Net;
using UnityToolbag;
using System.Text;
using SimpleJSON;
using System;

public class Test_DoiMatKhau : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	}
	// Update is called once per frame
	void Update ()
	{
	}

	//constant

	//variable
	//UI
	public InputField input_matkhau_cu;
	public InputField input_matkhau_moi;
	public InputField input_xacnhan_matkhau;


	//function UI
	public void TestDoiMatKhau ()
	{
		string ten_taikhoan = "hungnh";
		string matkhau_cu = input_matkhau_cu.text;
		string matkhau_moi = input_matkhau_moi.text;
		string confirm_matkhau = input_xacnhan_matkhau.text;
		//check điều kiện
		if (matkhau_cu.Length == 0) {
//			Debug.LogError ("Vui lòng nhập mật khẩu hiện tại");
			return;
		} else if (matkhau_cu.Length > 16) {
//			Debug.LogError ("Mật khẩu hiện tại quá dài");
			return;
		}
		if (matkhau_moi.Length == 0) {
//			Debug.LogError ("Vui lòng nhập mật khẩu mới");
			return;
		} else if (matkhau_moi.Length > 16) {
//			Debug.LogError ("Mật khẩu mới quá dài");
			return;
		}
		if (!matkhau_moi.Equals (confirm_matkhau, StringComparison.Ordinal)) { //kiểm tra khớp mật khẩu hay không
//			Debug.LogError("Mật khẩu không khớp");
			return;
		}
		//bắt đầu đổi mật khẩu 
		var data = new NameValueCollection ();
		data ["user"] = Constants.Base64Encode (ten_taikhoan);
		data ["matkhau_cu"] = Constants.Base64Encode (matkhau_cu);
		data ["matkhau_moi"] = Constants.Base64Encode (matkhau_moi);
		data ["r"] = Constants.MD5 (ten_taikhoan + "bhhunter");
		var wb = new WebClient ();
		wb.UploadValuesAsync (new Uri (Constants.url_base + Constants.url_doi_matkhau), data);
		wb.UploadValuesCompleted += (sender, e) => {
			Dispatcher.Invoke (() => {
				// this code is executed in main thread
				if (e.Error != null) {
//					Debug.Log ("Error ");
				} else {
//					Debug.Log ("Done ");
					string responseString = Encoding.Default.GetString (e.Result);
//					Debug.Log ("responseString: " + responseString);
					var dict = JSON.Parse (responseString);
					int status_ketqua = dict ["success"].AsInt;
					if (status_ketqua == 1) {
//						Debug.Log ("Đổi mật khẩu thành công");
					} else {
//						Debug.Log ("Đổi mật khẩu thất bại, mật khẩu cũ không đúng");
					}
				}
			});
		};
	}
}
