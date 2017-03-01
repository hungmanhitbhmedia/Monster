using UnityEngine;
using System.Collections;

public class ThayDanSungTruong : Singleton<ThayDanSungTruong>
{

	public void Thaydan ()
	{
		if (GameEnd.Instance.IsGameOver) {
			CancelInvoke ();
			return;
		}
		this.transform.GetChild (0).gameObject.SetActive (false);
		this.transform.GetChild (2).gameObject.SetActive (true);
		this.transform.GetChild (3).gameObject.SetActive (false);
		SoundManager.Instance.ThayDanSungTruong ();
		Invoke ("Thaydanxong", 2.5f);
	}

	void Thaydanxong ()
	{
		this.transform.GetChild (0).gameObject.SetActive (true);
		this.transform.GetChild (2).gameObject.SetActive (false);
		ShotGun.Instance.capacity = ShotGun.Instance.capacitymax;
		ShotGun.Instance.isthaydan = false;
	}

	public void Thaydanngamban ()
	{
		Thaydan ();
		Invoke ("ChuyenLenTam", 2.5f);
	}

	void ChuyenLenTam ()
	{
		this.GetComponent<GunAnimation> ().Dualenngam ();
		ShotGun.Instance.isthaydan = false;
	}
}
