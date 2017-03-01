using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
	public Text txmessage;

	public void ShowNickName ()
	{
		txmessage.text = "Nick name: " + PlayerPrefs.GetString (Constants.kTenTaiKhoan);
	}
}
