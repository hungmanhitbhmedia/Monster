using UnityEngine;
using System.Collections;

public class ThayDanSungTIaThayTungVien :Singleton<ThayDanSungTIaThayTungVien>
{

	public void LenDan ()
	{
		ShotGun.Instance.isthaydan = true;
		Invoke ("LenXong", 1);
		this.GetComponent<GunAnimation> ().Lendan ();
	}

	void LenXong ()
	{
		if (GameEnd.Instance.IsGameOver) {
			CancelInvoke ();
			return;
		}
		SoundManager.Instance.LenDanSungTiaTungVien ();
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
//		SoundManager.Instance.LenDanSungTiaTungVien ();
//		ShotGun.Instance.Thaydan = false;
		ShotGun.Instance.isReLoad = false;
		ShotGun.Instance.Settam ();
//		ShotGun.Instance.tamnho.SetActive (false);
	}
}
