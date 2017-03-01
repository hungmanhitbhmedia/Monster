using UnityEngine;
using System.Collections;

public class ThayDanSungTiaThayCaBang : Singleton<ThayDanSungTiaThayCaBang>
{
	public void LenDan ()
	{
		ShotGun.Instance.isthaydan = true;
		Invoke ("LenXong", 1);
		this.transform.GetChild (0).GetComponent<Animation> ().Play ("Thadan");
	}

	void LenXong ()
	{
		ShotGun.Instance.isthaydan = false;
	}

	public void LenDanNgamBan ()
	{
		ShotGun.Instance.isReLoad = false;
		ShotGun.Instance.Settam ();
		LenDan ();
		Invoke ("LenDanNgamBanXong", 1.5f);
	}

	void LenDanNgamBanXong ()
	{
		ShotGun.Instance.isReLoad = false;

		ShotGun.Instance.Settam ();
	}

	public void ThayDan ()
	{
		if (GameEnd.Instance.IsGameOver) {
			CancelInvoke ();
			return;
		}
		SoundManager.Instance.ThayDanSungTruong ();
		this.transform.GetChild (0).gameObject.SetActive (false);
		this.transform.GetChild (2).gameObject.SetActive (true);
		ShotGun.Instance.tamnho.SetActive (false);
		Invoke ("ThayDanXong", 2.5f);
	}

	void ThayDanXong ()
	{
		ShotGun.Instance.isthaydan = false;
		this.transform.GetChild (0).gameObject.SetActive (true);
		this.transform.GetChild (2).gameObject.SetActive (false);
		ShotGun.Instance.capacity = ShotGun.Instance.capacitymax;
		if (this.gameObject.name == "End Of Dream") {
			ShotGun.Instance.tamnho.SetActive (true);
		}
	}

	public void ThayDanNgamBan ()
	{
		ThayDan ();
		Invoke ("ChuyenLenTam", 2.5f);
	}

	void ChuyenLenTam ()
	{
		this.GetComponent<GunAnimation> ().Dualenngam ();
		Invoke ("DuSungLen", 0.3f);
	}

	void DuSungLen ()
	{
		this.transform.GetChild (2).gameObject.SetActive (false);
		this.transform.GetChild (0).gameObject.SetActive (true);
		ShotGun.Instance.tam.SetActive (true);
		ShotGun.Instance.isthaydan = false;
		this.gameObject.SetActive (false);
	}
}
