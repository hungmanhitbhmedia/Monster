using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShotGun : Singleton<ShotGun>
{
	public Text txbullet;

	public int capacitymax;
	public int capacity;
	public bool iszoom;
	public bool isthaydan;
	public bool isReLoad;
	public bool isBan;

	//Animation ani;
	public GameObject tam;
	public GameObject tamnho;
	public GameObject btreload;
	public bool istam;
	private string gun;
	public float fieldView;
	Shot shot;

	public GunAnimation gunani;

	RegionInGame regioningame;
	Rifles rifles;
	GunInGame guningame;
	UpdateGun updategun;

	void Start ()
	{
		// ani = gun.GetComponent<Animation>();
		regioningame = new RegionInGame ();
		rifles = new Rifles ();
		guningame = new GunInGame ();
		updategun = new UpdateGun ();
		gun = regioningame.GetRegionInGame ().Gun;
		if (rifles.GetRifles (gun).MaxCapacity == 0) {
			capacitymax = int.Parse (
				rifles.GetRifles (gun).Capacity +
				updategun.GetDetail (Const.Capacity, rifles.GetRifles (gun).Types) *
				guningame.GetGunInGame (gun).Capacity + "");
		} else {
			capacitymax = int.Parse (rifles.GetRifles (gun).Capacity + "");
		}
		capacity = capacitymax;
		GameEnd.Instance.googleananytic.LogScreen ("Súng sử dụng:" + gun);
//		Invoke ("SetPositionStart");
		InvokeRepeating ("CheckReLoad", 0, 3);
	}

	//	public void SetPositionStart ()
	//	{
	//		gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
	//		gunani.Tho ();
	//		this.transform.GetChild (0).GetChild (0).transform.localPosition = gunani.pos;
	//	}

	void Update ()
	{
		txbullet.text = capacity + "/" + capacitymax;
		if (tam.activeSelf) {
			btreload.SetActive (false);
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			Settam ();
			CheckZoom ();
		}
	}

	void CheckReLoad ()
	{
		if (capacity <= capacitymax && !btreload.activeSelf && !tam.activeSelf && !isthaydan) {
			isReLoad = false;
			btreload.SetActive (true);
		}
	}

	public void Ban ()
	{
		if (GameEnd.Instance.IsGameOver) {
			return;
		}
		if (isthaydan) {
			return;
		}
		if (isReLoad) {
			return;
		}

		isBan = true;
		string tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().Where (x => x.Id == 1).FirstOrDefault ().Gun;
		int kieuban = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().KieuBan;
//		Debug.Log ("Kieu ban" + kieuban);
		switch (kieuban) {
		case 0:

			SoundManager.Instance.BanSungTiaTungVien ();
			// súng trường bắn ko hồng tâm  
			this.transform.GetChild (0).GetChild (0).GetChild (0).gameObject.SetActive (false);
			this.transform.GetChild (0).GetChild (0).GetChild (3).gameObject.SetActive (true);

			gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
//			GameManager.Instance.ConfirmMonster ();
			if (!GameEnd.Instance.isEnd) {
				capacity--;
				if (!istam) {
					gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
					gunani.Bankhongngam ();
					istg = false;

					if (capacity == 0) {
						btreload.SetActive (false);
						isthaydan = true;
						ThayDanSungTruong.Instance.Thaydan ();
						Shot.Instance.CancelInvoke ();
					}			
				} else {
					if (capacity == 0) {
						btreload.SetActive (false);
						isthaydan = true;
						tam.SetActive (false);
						//Invoke ("Chuyensangtam", 0.2f);
						this.transform.GetChild (0).GetChild (0).gameObject.SetActive (istam);
						gunani.Hasungxuong ();
						fieldView = Camera.main.fieldOfView;
						Camera.main.fieldOfView = 60;
						istg = false;

						ThayDanSungTruong.Instance.Thaydanngamban ();
						Shot.Instance.CancelInvoke ();
						//				Settam1 ();
					}
				}
			}


			break;
		case 1:
			// súng tỉa 
			gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
//			GameManager.Instance.ConfirmMonster ();
			if (!GameEnd.Instance.isEnd) {
				capacity--;
				if (!istam) {
					tamnho.SetActive (true);
					gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
					gunani.Bankhongngam ();
					Camera.main.fieldOfView = 60;
					istg = false;

					if (capacity == 0) {
						btreload.SetActive (false);

						isthaydan = true;
						Invoke ("Thaydan", 1);
						Shot.Instance.CancelInvoke ();
					} else {
						ThayDanSungTIaThayTungVien.Instance.LenDan ();
					}
				} else {
					if (capacity == 0) {
						btreload.SetActive (false);

						isthaydan = true;
						tam.SetActive (false);
						Invoke ("Chuyensangtam", 0.2f);
						this.transform.GetChild (0).GetChild (0).gameObject.SetActive (istam);
						gunani.Hasungxuong ();
						//Settam ();
						fieldView = Camera.main.fieldOfView;
						Camera.main.fieldOfView = 60;
						istg = false;


						Invoke ("ThaydanNgamBan", 1);
						Shot.Instance.CancelInvoke ();
//						Settam1 ();
					} else {
						ThayDanSungTIaThayTungVien.Instance.LenDanNgamBan ();
					}
				}
			}
			break;
		case 2:
			// shot gun 
			gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
//			GameManager.Instance.ConfirmMonster ();
			if (!GameEnd.Instance.isEnd) {
				capacity--;
				if (!istam) {
					tamnho.SetActive (true);
					gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
					gunani.Bankhongngam ();
					istg = false;

					if (capacity == 0) {
						btreload.SetActive (false);

						isthaydan = true;
						Invoke ("Thaydan", 1);
						Shot.Instance.CancelInvoke ();
					} else {
						ThayDanShotGun.Instance.Lendan ();
					}
				} else {
					if (capacity == 0) {
						btreload.SetActive (false);

						isthaydan = true;
						tam.SetActive (false);
						Invoke ("Chuyensangtam", 0.2f);
						this.transform.GetChild (0).GetChild (0).gameObject.SetActive (istam);
						gunani.Hasungxuong ();
						fieldView = Camera.main.fieldOfView;
						Camera.main.fieldOfView = 60;
						istg = false;
						Invoke ("ThaydanNgamBan", 1);
						Shot.Instance.CancelInvoke ();
					} else {
						ThayDanShotGun.Instance.LenDanNgamBan ();
					}
				}
			}
			break;
		case 3:
			// súng đặc biệt 
			gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
//			GameManager.Instance.ConfirmMonster ();
			if (!GameEnd.Instance.isEnd) {
				capacity--;
				if (!istam) {
					istg = false;

					//gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
//					gunani.Bankhongngam ();
					if (capacity == 0) {
						btreload.SetActive (false);
						isthaydan = true;
						ThayDanSungDacBiet.Instance.ThayDan ();
						Shot.Instance.CancelInvoke ();
					} else {
						//ThayDanSungTiaThayCaBang.Instance.LenDan ();
					}
//					Invoke ("Thaydan", 1);
				} else {
					if (capacity == 0) {
						btreload.SetActive (false);

						isthaydan = true;
						tam.SetActive (false);
						Invoke ("Chuyensangtam", 0.2f);
						this.transform.GetChild (0).GetChild (0).gameObject.SetActive (istam);
						gunani.Hasungxuong ();
						fieldView = Camera.main.fieldOfView;
						Camera.main.fieldOfView = 60;
						istg = false;

						Shot.Instance.CancelInvoke ();
						//Settam1 ();
					} else {
						//ThayDanSungTiaThayCaBang.Instance.LenDanNgamBan ();
					}
				}
			}
			if (tmpGun == "Gatlinggun") {
				SoundManager.Instance.BanSung6long ();
			}
			if (tmpGun == "Plasma RPG") {
				SoundManager.Instance.BanRPG ();
			}
			break;
		case 4:
			break;
		case 5:
			SoundManager.Instance.BanSungTiaTungVien ();
			gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
//			GameManager.Instance.ConfirmMonster ();
			if (!GameEnd.Instance.isEnd) {
				capacity--;
				if (!istam) {
					istg = false;

					gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
					gunani.Bankhongngam ();
					if (capacity == 0) {
						btreload.SetActive (false);

						isthaydan = true;
						ThayDanSungTiaThayCaBang.Instance.ThayDan ();
						Shot.Instance.CancelInvoke ();
					} else {
						//ThayDanSungTiaThayCaBang.Instance.LenDan ();
					}
				} else {
					if (capacity == 0) {
						btreload.SetActive (false);

						this.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
						isthaydan = true;
						tam.SetActive (false);
//						Invoke ("Chuyensangtam", 0.2f);
//						this.transform.GetChild (0).GetChild (0).gameObject.SetActive (istam);
						gunani.Hasungxuong ();
						fieldView = Camera.main.fieldOfView;
						Camera.main.fieldOfView = 60;
						istg = false;

						ThayDanSungTiaThayCaBang.Instance.ThayDanNgamBan ();
						Shot.Instance.CancelInvoke ();
					} else {
					}
				}
			}
			break;
		}
		
	}
	// Nhả cò
	public void NhaBan ()
	{
		isBan = false;
		string tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().Where (x => x.Id == 1).FirstOrDefault ().Gun;
		int kieuban = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().KieuBan;
		//Debug.Log ("Kieu ban" + kieuban);
		switch (kieuban) {
		case 0:
			// súng trường bắn ko hồng tâm  
			if (!isthaydan) {
				this.transform.GetChild (0).GetChild (0).GetChild (0).gameObject.SetActive (true);
				this.transform.GetChild (0).GetChild (0).GetChild (3).gameObject.SetActive (false);
				GunAnimation.Instance.Bankhongngam ();
				Shot.Instance.CancelInvoke ();
			}
			break;
		case 1:
			// súng tỉa 
			Shot.Instance.CancelInvoke ();
			break;
		case 2:
			// shot gun 
			break;
		case 3:
			// súng đặc biệt 
			Shot.Instance.CancelInvoke ();
			if (!isthaydan) {
				if (tmpGun == "Gatlinggun") {
					this.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Animation> ().Play ("Trangthaikhongdichuyen");
				}
			}
			break;
		case 4:
			break;
		case 5:
			Shot.Instance.CancelInvoke ();
			break;
		}
	}


	public void Thaydanxong ()
	{
		
		isthaydan = false;
		gunani.Tho ();
	}

	public void Thaydan ()
	{
		if (GameEnd.Instance.IsGameOver) {
			return;
		}
		
		isthaydan = true;
		gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
		gunani.Thaydan ();
		Invoke ("Thaydan1", gunani.ani.clip.length);
	}

	void Thaydan1 ()
	{
		EventGunAnimation.Instance.Thaydan1 ();
	}

	public void ThaydanNgamBan ()
	{
		if (GameEnd.Instance.IsGameOver) {
			return;
		}
		
		isthaydan = true;
		gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
		gunani.Thaydan ();
		Invoke ("ThaydanNgamban1", gunani.ani.clip.length);
	}

	public void ThaydanNgamban1 ()
	{
		EventGunAnimation.Instance.ThandanBanngam1 ();
	}



	public void ThayDanNgamBanSungTiaZoom ()
	{
		
	}

	public void Idle ()
	{
//        ani.Play("Idle");
		gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
		gunani.Tho ();
	}

	public void Settam ()
	{
		string tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().Where (x => x.Id == 1).FirstOrDefault ().Gun;
		int kieuban = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().KieuBan;
		//Debug.Log ("Kieu ban" + kieuban);
		if (kieuban == 3) {
			return;
		}
		if (isthaydan) {
			return;
		}
		if (isReLoad) {
			return;
		}
		gunani = this.transform.GetChild (0).GetChild (0).GetComponent<GunAnimation> ();
		if (!iszoom) {
			iszoom = true;
			if (istam) {
				this.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
				tam.SetActive (false);
				gunani.Hasungxuong ();
				this.transform.GetChild (0).GetChild (0).transform.localPosition = gunani.pos;
			} else {
				gunani.Dualenngam ();
			}
			Invoke ("Chuyensangtam", 0.2f);
		}
	}

	public void Chuyensangtam ()
	{
		iszoom = false;
		//this.transform.GetChild (0).GetChild (0).gameObject.SetActive (istam);
		int tmpr = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == gunani.name).FirstOrDefault ().TamSung;
		if (tmpr == 0) {
			this.transform.GetChild (0).GetChild (0).gameObject.SetActive (istam);
			tam.SetActive (!istam);
		} 
		tamnho.SetActive (istam);
		istam = !istam;
	}

	public  bool istg;

	public void CheckZoom ()
	{
		string tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().Where (x => x.Id == 1).FirstOrDefault ().Gun;
		int kieuban = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().KieuBan;
		//Debug.Log ("Kieu ban" + kieuban);
		if (kieuban == 3) {
			return;
		}
		istg = !istg;
		//Debug.Log (istg);
		InvokeRepeating ("InvokeZoom", 0.05f, 0.05f);
		//Invoke("InvokeZoom",0.5f);
	}

	void InvokeZoom ()
	{
		if (tam.activeSelf) {
			if (istg) {
				tam.transform.GetChild (4).GetChild (0).GetChild (0).GetComponent<Zoom> ().NoZoom ();
				//Zoom.Instance.NoZoom ();	
			}
			CancelInvoke ("InvokeZoom");
		}
		if (!istg) {
			Camera.main.fieldOfView = 60;
		}
	}

	public void ReLoad ()
	{
		if (capacity >= capacitymax) {
			return;
		}
		if (isBan) {
			return;
		}
		if (isthaydan) {
			return;
		}
		if (GameEnd.Instance.IsGameOver) {
			return;
		}
		string tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().Where (x => x.Id == 1).FirstOrDefault ().Gun;
		int kieuban = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().KieuBan;
		isthaydan = true;
		isReLoad = true;
		btreload.SetActive (false);
		GameObject modelGun = this.transform.GetChild (0).GetChild (0).gameObject;
//		if (modelGun.GetComponent<ThayDanShotGun> () != null) {
//			Debug.Log ("1");
//			Invoke ("Thaydan", 1);
//		}
		if (kieuban == 2) {
			if (modelGun.name == "Desolation") {
				Invoke ("Thaydan", 1);
				Shot.Instance.CancelInvoke ();
				return;
			}
			tam.SetActive (false);
			Invoke ("Chuyensangtam", 0.2f);
			gunani.Hasungxuong ();
			fieldView = Camera.main.fieldOfView;
			Camera.main.fieldOfView = 60;
			Invoke ("ThaydanNgamBan", 1);
			Shot.Instance.CancelInvoke ();
		}
		if (modelGun.GetComponent<ThayDanSungDacBiet> () != null) {
//			Debug.Log ("2");
			ThayDanSungDacBiet.Instance.ThayDan ();
		}
		if (modelGun.GetComponent<ThayDanSungTiaThayCaBang> () != null) {
//			Debug.Log ("3");
			ThayDanSungTiaThayCaBang.Instance.ThayDan ();
		}
		if (modelGun.GetComponent<ThayDanSungTIaThayTungVien> () != null) {
//			Debug.Log ("4");
			Invoke ("Thaydan", 2);
			//ThayDanSungTIaThayTungVien.Instance.LenDanNgamBan ();
		}
//		if (modelGun.GetComponent<ThayDanSungTruong> () != null) {
//			Debug.Log ("5");
//			ThayDanSungTruong.Instance.Thaydan ();
//		}
		if (kieuban == 0) {
			this.transform.GetChild (0).GetChild (0).gameObject.SetActive (istam);
			gunani.Hasungxuong ();
			fieldView = Camera.main.fieldOfView;
			Camera.main.fieldOfView = 60;
			ThayDanSungTruong.Instance.Thaydanngamban ();
			Shot.Instance.CancelInvoke ();
		}
	}

}

