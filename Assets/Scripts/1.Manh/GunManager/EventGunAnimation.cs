using UnityEngine;
using System.Collections;

public class EventGunAnimation : Singleton<EventGunAnimation>
{
	public GameObject dan;

	void Start ()
	{
//		Debug.Log (this.gameObject.name);
	}

	public void  Thaydan1 ()
	{
		if (GameEnd.Instance.IsGameOver) {
			CancelInvoke ();
			return;
		}
		ShotGun.Instance.capacity++;
		if (ShotGun.Instance.capacity >= ShotGun.Instance.capacitymax) {
			//ShotGun.Instance.Thaydanxong ();
			Thadan ();
		} else {
			this.GetComponent<Animation> ().Play ("Thadan1");
			InvokeRepeating ("Thaydan2", GunAnimation.Instance.ani.clip.length, GunAnimation.Instance.ani.clip.length);
		}
		string tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().Where (x => x.Id == 1).FirstOrDefault ().Gun;
		int kieuban = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().KieuBan;
		switch (kieuban) {
		case 1:
			SoundManager.Instance.LenDanSungTiaTungVien ();
			break;
		case 2:
			SoundManager.Instance.ThayDanShotGun ();
			break;
		}
	}

	public void Thaydan2 ()
	{
		if (GameEnd.Instance.IsGameOver) {
			CancelInvoke ();
			return;
		}
//		Debug.Log (1);
		this.GetComponent<Animation> ().Play ("Thadan1");
		ShotGun.Instance.capacity++;
		if (ShotGun.Instance.capacity >= ShotGun.Instance.capacitymax) {
			CancelInvoke ();
			Thadan ();
			if (dan != null) {
				dan.SetActive (false);
			}
		} 
		string tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().Where (x => x.Id == 1).FirstOrDefault ().Gun;
		int kieuban = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().KieuBan;
		switch (kieuban) {
		case 1:
			SoundManager.Instance.LenDanSungTiaTungVien ();
			break;
		case 2:
			SoundManager.Instance.ThayDanShotGun ();
			break;
		}

	}

	void Thadan ()
	{
		this.GetComponent<Animation> ().Play ("Thadan");
		Invoke ("Thaydanxong", GunAnimation.Instance.ani.clip.length);
	}

	public void Thaydanxong ()
	{
//		ShotGun.Instance.btreload.SetActive (true);
		ShotGun.Instance.Thaydanxong ();
	}

	public void Tho ()
	{
		GunAnimation.Instance.Tho ();
	}

	public
	void ThandanBanngam1 ()
	{
		if (GameEnd.Instance.IsGameOver) {
			CancelInvoke ();
			return;
		}
		ShotGun.Instance.capacity++;
		if (ShotGun.Instance.capacity >= ShotGun.Instance.capacitymax) {
			//ShotGun.Instance.Thaydanxong ();
			ThayDanNgamban ();
		} else {
			this.GetComponent<Animation> ().Play ("Thadan1");
			InvokeRepeating ("ThaydanNgamban2", GunAnimation.Instance.ani.clip.length, GunAnimation.Instance.ani.clip.length);
		}
		string tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().Where (x => x.Id == 1).FirstOrDefault ().Gun;
		int kieuban = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().KieuBan;
		switch (kieuban) {
		case 1:
			SoundManager.Instance.LenDanSungTiaTungVien ();
			break;
		case 2:
			SoundManager.Instance.ThayDanShotGun ();
			break;
		}
	}

	public void ThaydanNgamban2 ()
	{
		if (GameEnd.Instance.IsGameOver) {
			CancelInvoke ();
			return;
		}
		ShotGun.Instance.capacity++;
		if (ShotGun.Instance.capacity >= ShotGun.Instance.capacitymax) {
			CancelInvoke ();
			ThayDanNgamban ();
		}
		string tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().Where (x => x.Id == 1).FirstOrDefault ().Gun;
		int kieuban = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().KieuBan;
		switch (kieuban) {
		case 1:
			SoundManager.Instance.LenDanSungTiaTungVien ();
			break;
		case 2:
			SoundManager.Instance.ThayDanShotGun ();
			break;
		}
	}

	public void ThayDanNgamban ()
	{
		this.GetComponent<Animation> ().Play ("Thadan");
		Invoke ("ThaydanNgambanxong", GunAnimation.Instance.ani.clip.length);
	}

	public void ThaydanNgambanxong ()
	{
		Camera.main.fieldOfView = ShotGun.Instance.fieldView;
		string tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().Where (x => x.Id == 1).FirstOrDefault ().Gun;
		int kieuban = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().KieuBan;
		if (kieuban == 2) {
			ShotGun.Instance.isthaydan = false;
			ShotGun.Instance.Settam ();
			Animation	ani = this.transform.GetComponent<Animation> ();
			ani.Play ("Trangthaikhongdichuyen");
		
		} else {
			ShotGun.Instance.Thaydanxong ();
			ShotGun.Instance.Chuyensangtam ();
		
		}


	}

}
