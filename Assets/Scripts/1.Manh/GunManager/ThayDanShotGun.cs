using UnityEngine;
using System.Collections;

public class ThayDanShotGun : Singleton<ThayDanShotGun>
{
	public void Lendan ()
	{
		if (GameEnd.Instance.IsGameOver) {
			CancelInvoke ();
			return;
		}
		ShotGun.Instance.isthaydan = true;
		this.GetComponent<GunAnimation> ().Bankhongngam ();
		Invoke ("Lendantiep", .5f);
	}

	void Lendantiep ()
	{
		//SoundManager.Instance.ThayDanShotGun ();
		this.GetComponent<GunAnimation> ().Lendan ();
		Invoke ("ThayDanXong", 2f);
	}

	void ThayDanXong ()
	{
//		ShotGun.Instance.tamnho.SetActive (false);
		ShotGun.Instance.isthaydan = false;
	}


	void Lendantiep1 ()
	{
		this.GetComponent<GunAnimation> ().Lendan ();
	}



	public void LenDanNgamBan ()
	{
		ShotGun.Instance.isReLoad = false;
		ShotGun.Instance.Settam ();
//		Lendan ();
		ShotGun.Instance.isthaydan = true;
		this.GetComponent<GunAnimation> ().Bankhongngam ();
		Invoke ("Lendantiep1", .5f);
		Invoke ("LenDanNgamBanXong", 2.5f);
	}

	void  LenDanNgamBanXong ()
	{
		if (this.gameObject.name == "Desolation") {
			ShotGun.Instance.iszoom = false;
			ShotGun.Instance.isthaydan = false;
			ShotGun.Instance.gunani.Dualenngam ();
			ShotGun.Instance.istam = true;
			ShotGun.Instance.tamnho.SetActive (false);
			this.gameObject.SetActive (false);
			ShotGun.Instance.tam.SetActive (true);
			return;
		}
		Settam ();
	}

	public void Settam ()
	{
		if (!ShotGun.Instance.iszoom) {
			ShotGun.Instance.iszoom = true;
			if (ShotGun.Instance.istam) {
				this.transform.gameObject.SetActive (true);
				ShotGun.Instance.tam.SetActive (false);
				ShotGun.Instance.gunani.Hasungxuong ();
				this.transform.localPosition = ShotGun.Instance.gunani.pos;
				ShotGun.Instance.istam = false;
			} else {
				ShotGun.Instance.gunani.Dualenngam ();
				ShotGun.Instance.istam = true;
				ShotGun.Instance.tamnho.SetActive (false);

			}
			Invoke ("Chuyensangtam", 1f);
		}
	}

	public void Chuyensangtam ()
	{
		ShotGun.Instance.iszoom = false;
		ShotGun.Instance.isthaydan = false;
	}
}
