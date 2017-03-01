using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public enum Duong
	{
		DanThuong,
		DanPlasma
	}

	public Duong duong;

	Vector3 positionend;
	float distance;

	void Start ()
	{
		switch (duong) {
		case Duong.DanPlasma:
			MoveDanPlasma ();
			break;
		case Duong.DanThuong:
			MoveDanThuong ();
			break;
		}
	}

	IEnumerator OnComPlete ()
	{
		yield return new WaitForSeconds (2.5f);
		this.transform.GetChild (0).gameObject.SetActive (false);
		GameObject effect = Instantiate (Resources.Load ("Effect/BloodFX"))as GameObject;
		effect.transform.position = new Vector3 (Shot.Instance.postionend.x, Shot.Instance.postionend.y, Shot.Instance.postionend.z + 0.5f);
	}

	void MoveDanThuong ()
	{
		//		Time.timeScale = 0;
		positionend = Shot.Instance.postionend;	
		iTween.MoveTo (this.gameObject, iTween.Hash ("position", positionend, "time", 2.5, "easetype", iTween.EaseType.linear));
		StartCoroutine (OnComPlete ());
	}

	void MoveDanPlasma ()
	{
//		Time.timeScale = 0;
		positionend = Shot.Instance.postionend;	
		iTween.MoveTo (this.gameObject, iTween.Hash ("position", positionend, "time", 0.5f, "easetype", iTween.EaseType.linear));
		StartCoroutine (OnComPlete1 ());
	}

	IEnumerator OnComPlete1 ()
	{
		yield return new WaitForSeconds (0.5f);
		this.transform.gameObject.SetActive (false);
	}
}
