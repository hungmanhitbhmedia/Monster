using UnityEngine;
using UnityEngine.UI;
using UnityToolbag;
using UnityEngine.SceneManagement;

using System;
using System.IO;
using System.Net;

using System.Linq;
using System.Collections;

//using System.Collections.Specialized;
//using System.Collections.Generic;
//using System.Text;
//using System.Globalization;
//using System.Security;
using System.Net.Mail;
using System.Net.Security;

//using System.Security.Cryptography.X509Certificates;

using Facebook.Unity;

public class Social : MonoBehaviour
{
	public InputField feedBackContent;
	public InputField emailAddress;
	public GameObject feedback;
	public GameObject confirm;
	public GameObject internet;
	public Text txdialog;

	void Start ()
	{
		FB.Init ();
	}


	public void ResetText ()
	{
		txdialog.text = "";
		feedBackContent.text = "";
		emailAddress.text = "";
	}

	public void Feedback ()
	{
		confirm.SetActive (true);
		if (!CapNhatThongTin.Instance.TestMang ()) {
			internet.SetActive (true);
			confirm.SetActive (false);

			return;
		}
		if (feedBackContent.text == "" || emailAddress.text == "") {
			txdialog.text = "Send mail Error ";
			txdialog.color = Color.red;
			confirm.SetActive (false);

			return;
		}
		string email = emailAddress.text;
		if (!Constants.isValid_Mail (email)) {
			Input_Mail_On_End_Edit ();
			confirm.SetActive (false);

			return;
		}
		StartCoroutine (SendMail ());
//		txdialog.text = "";
//		txdialog.color = new Color (1, 1, 1, 1);
//		Debug.Log ("feed back");
//		string supportMail = "feedback@binhhoang.com";
//		string playerEmailAddress = String.IsNullOrEmpty (emailAddress.text) ? supportMail : emailAddress.text;
//		string content = feedBackContent.text;
//		string senderMail = "feedback@binhhoang.com";
//		string senderpw = "bhmediafeedback2016$";
//
//		Debug.Log ("feed back  1");
//
//		//send mail:
//		int charID = PlayerPrefs.GetInt (Constants.kIdAccount);
//		string mailSubject = "Mounter Hunter FeedBack from character ID: ";
//		MailMessage mail = new MailMessage ();
//		mail.From = new MailAddress (playerEmailAddress);
//		mail.To.Add (supportMail);
//		mail.Subject = (charID > -1) ? mailSubject + charID : mailSubject;
//		mail.Subject += " " + playerEmailAddress;
//		mail.Body = content;
//		Debug.Log ("feed back  2");
//		SmtpClient smtpServer = new SmtpClient ("smtp.gmail.com");
//		smtpServer.Port = 587;
//		smtpServer.UseDefaultCredentials = false;
//		smtpServer.Credentials = new System.Net.NetworkCredential (senderMail, senderpw) as ICredentialsByHost;
//		smtpServer.EnableSsl = true;
//
//		smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
//		smtpServer.Timeout = 10000;
//		Debug.Log ("Truocs delegate");
//		ServicePointManager.ServerCertificateValidationCallback = delegate {
//			return true;
//		};
//
//		Debug.Log ("sau  delegate");
//
//		try {
//			Debug.Log ("bat dau send mail");
//			smtpServer.Send (mail);
//			txdialog.color = new Color (0, 1, 0.1f, 1);
//			txdialog.text = "Thank you for your opinion, we'll improve game experiment as soon as possible.";
//			confirm.SetActive (false);
//			Invoke ("DisableFeedback", .5f);
//			Debug.Log ("Send mail thanh cong");
//		} catch (Exception ex) {
//			Debug.Log ("Send mail that bai");
////			Debug.Log (mylog.text);
//			txdialog.text = "Send mail Error: " + ex.ToString ();
//			txdialog.color = Color.red;
//			confirm.SetActive (false);
//		}
	}

	IEnumerator SendMail ()
	{
		yield return new WaitForSeconds (0.5f);
		txdialog.text = "";
		txdialog.color = new Color (1, 1, 1, 1);
//		Debug.Log ("feed back");
		string supportMail = "feedback@binhhoang.com";
		string playerEmailAddress = String.IsNullOrEmpty (emailAddress.text) ? supportMail : emailAddress.text;
		string content = feedBackContent.text;
		string senderMail = "feedback@binhhoang.com";
		string senderpw = "bhmediafeedback2016$";

//		Debug.Log ("feed back  1");

		//send mail:
		int charID = PlayerPrefs.GetInt (Constants.kIdAccount);
		string mailSubject = "Mounter Hunter FeedBack from character ID: ";
		MailMessage mail = new MailMessage ();
		mail.From = new MailAddress (playerEmailAddress);
		mail.To.Add (supportMail);
		mail.Subject = (charID > -1) ? mailSubject + charID : mailSubject;
		mail.Subject += " " + playerEmailAddress;
		mail.Body = content;
//		Debug.Log ("feed back  2");
		SmtpClient smtpServer = new SmtpClient ("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.UseDefaultCredentials = false;
		smtpServer.Credentials = new System.Net.NetworkCredential (senderMail, senderpw) as ICredentialsByHost;
		smtpServer.EnableSsl = true;

		smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
		smtpServer.Timeout = 10000;
//		Debug.Log ("Truocs delegate");
		ServicePointManager.ServerCertificateValidationCallback = delegate {
			return true;
		};

//		Debug.Log ("sau  delegate");

		try {
//			Debug.Log ("bat dau send mail");
			smtpServer.Send (mail);
			txdialog.color = new Color (0, 1, 0.1f, 1);
			txdialog.text = "Thank you for your opinion, we'll improve game experiment as soon as possible.";
			confirm.SetActive (false);
			Invoke ("DisableFeedback", 1f);
//			Debug.Log ("Send mail thanh cong");
		} catch (Exception ex) {
//			Debug.Log ("Send mail that bai");
			//			Debug.Log (mylog.text);
			txdialog.text = "Send mail Error: " + ex.ToString ();
			txdialog.color = Color.red;
			confirm.SetActive (false);
		}
	}

	void DisableFeedback ()
	{

		feedback.SetActive (false);
	}

	public void ShareFacebook ()
	{
		if (!CapNhatThongTin.Instance.TestMang ()) {
			internet.SetActive (true);
			return;
		}
		FB.ShareLink (
			new Uri ("http://bhmedia.vn"), "We're going on a monster hunt.", "Join us for the hunting experience of a life-time.",
			new Uri ("http://app.bhmedia.vn/idata/icon/IconMonsterHunt.png"),
			callback: ShareCallBack
		);
	}

	private void ShareCallBack (IShareResult result)
	{
		if (result.Cancelled || !String.IsNullOrEmpty (result.Error)) {
//			Debug.Log ("FB Share Error: " + result.Error);

		} else {
//			Debug.Log ("FB Share Success!");
//			timeRemainToShareFB = cooldown;
//			NhanVatInfo.Instance.gold += 1;
//			onCooldownFB = true;
//			CapNhatThongTin.Instance.updateThongTinTaiKhoan ();
//			lastShareFBTime ();

		}

	}

	public void Input_Mail_On_End_Edit ()
	{
		string email = emailAddress.text;
		if (!Constants.isValid_Mail (email)) {
			//Debug.LogError("Email không hợp lệ");
			//input_email.ActivateInputField ();
//			DiaLog ("Invalid email address");
			txdialog.text = "Invalid email address";
			txdialog.color = Color.red;
		}
	}
}
