using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuanLyFacebook : MonoBehaviour
{
	private static QuanLyFacebook instance;

	public static QuanLyFacebook Instance {
		get {
			if (instance == null)
				instance = new QuanLyFacebook ();
			return instance;
		}
	}
	//	 Use this for initialization
	void Start ()
	{
		KhoiTaoFacebook ();
	}
	// Update is called once per frame
	//	void Update ()
	//	{
	//	}

	//	variable
	public Image img_profile;
	public Text txt_username;
	string appLinkUrl;

	//	function - UI

	//	Void - Selector
	public void KhoiTaoFacebook ()
	{
		if (!FB.IsInitialized) { //khởi tạo facebook
			FB.Init (InitCallback, OnHideUnity);
		} else { // đã init, active app
			FB.ActivateApp ();
//			Debug.Log ("đã khởi tạo facebook");
		}
	}

	private void InitCallback ()
	{ //callback init facebook
		if (FB.IsInitialized) {
			FB.ActivateApp ();
		} else {
//			Debug.Log ("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) { // Pause the game - we will need to hide
			Time.timeScale = 0;
		} else { // Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}
	//login
	public void LoginFacebook ()
	{
		var perms = new List<string> (){ "public_profile", "email", "user_friends" };
		FB.LogInWithReadPermissions (perms, AuthCallback);
	}

	private void AuthCallback (ILoginResult result)
	{
//		Debug.Log ("result: " + result);
		if (result.Cancelled) {
			SceneManager.LoadScene ("ManHinhDangKy");
//			Debug.Log ("Cancel call back");
			return;
		}

		if (FB.IsLoggedIn) {
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			if (aToken == null) {
				SceneManager.LoadScene ("ManHinhDangKy");
				return;
			}
			string tentaikhoan = "fb" + aToken.UserId;
			PlayerPrefs.SetString (Constants.kFacebookUser, tentaikhoan);
			PlayerPrefs.Save ();
			SceneManager.LoadScene ("ManHinhDangNhap");
		} else {
//			Debug.Log ("User cancelled login");
			QuanLyDangKy.Instance.confirm.SetActive (false);
			return;
		}
	}
}
