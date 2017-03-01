using UnityEngine;
using System.Collections;

public class Shot : Singleton<Shot>
{
	public GameObject Monster;
	public GameObject tamnho;
	public Vector3 postionend;
	public RaycastHit hit;
	string tmpGun;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.S)) {
			Ban ();
		}
		if (Input.GetKeyUp (KeyCode.S)) {
			NhaBan ();
		}
	}

	public void Ban ()
	{
		if (!ShotGun.Instance.isthaydan && !GameEnd.Instance.isEnd) {
			if (ShotGun.Instance.isReLoad) {
				return;
			}
			GameManager.Instance.ConfirmMonster ();
			tamnho.GetComponent<Animator> ().Play ("Run");
			tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().Where (x => x.Id == 1).FirstOrDefault ().Gun;
			int kieuban = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().KieuBan;
			switch (kieuban) {
			case 0:
				// súng trường bắn ko hồng tâm  
				Debug.Log ("Ban");
				InvokeRepeating ("SetRayCastHit", 0, 0.2f);
				break;
			case 1:
				// súng tỉa 
				SetRayCastHit ();
				GiatCamera (-30);
				SoundManager.Instance.BanSungTiaTungVien ();
				break;
			case 2:
				// shot gun 
				SetRayCastHit ();
				GiatCamera (-30);
				SoundManager.Instance.BanSungShotGun ();
				break;
			case 3:
				// súng đặc biệt 
				InvokeRepeating ("SetRayCastHit", 0, 0.1f);
				break;
			case 4:
				break;
			case 5:
				// thay dan ca bang, nhung luc ban co hong tam vaf ban lien tuc
				InvokeRepeating ("SetRayCastHit", 0, 0.2f);
				break;
			}
		}
	}




	void GiatCamera (int dogiat)
	{
		Vector3 p1 = this.transform.eulerAngles;
		Vector3 p2 = new Vector3 (this.transform.eulerAngles.x + UnityEngine.Random.Range (0, dogiat), this.transform.eulerAngles.y + UnityEngine.Random.Range (0, 0), this.transform.eulerAngles.z);
		this.transform.eulerAngles = p2;
		iTween.RotateTo (this.gameObject, p1, 1);
		//this.transform.eulerAngles = Vector3.MoveTowards (p2, p1, 2);
	}

	public void SetRayCastHit ()
	{
		string tmpGun = DataManager.Instance.connection.Table<RegionInGame> ().Where (x => x.Id == 1).FirstOrDefault ().Gun;
		int kieuban = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == tmpGun).FirstOrDefault ().KieuBan;
		if (kieuban == 0 && kieuban == 5) {
			if (!ShotGun.Instance.istam) {
				GameObject _flash = Instantiate (Resources.Load ("Effect/MuzzleFlash"))as GameObject;
				_flash.transform.position = GunAnimation.Instance.flash.transform.position;
			}
		} else {
			if (kieuban == 3) {
				if (!ShotGun.Instance.istam) {
					GameObject _flash = Instantiate (Resources.Load ("Effect/MuzzleFlash3"))as GameObject;
					_flash.transform.position = GunAnimation.Instance.flash.transform.position;
				}
			} else {
				if (!ShotGun.Instance.istam) {
					GameObject _flash = Instantiate (Resources.Load ("Effect/MuzzleFlash"))as GameObject;
					_flash.transform.position = GunAnimation.Instance.flash.transform.position;
				}
			}
		}
//		RaycastHit hit;
		hit = new RaycastHit ();
		if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit)) {

			if (tmpGun != "Plasma RPG") {
				if (hit.collider.gameObject.tag != "Untagged" && hit.collider.gameObject.tag != "Ground" && hit.collider.gameObject.tag != "Boom") {
					if (hit.collider.gameObject.GetComponent<SetCollider> () != null) {
						hit.collider.gameObject.GetComponent<SetCollider> ().ConfirmHead ();
					}
					postionend = hit.point;
					if (!GameEnd.Instance.isEnd) {
						GameObject effect = Instantiate (Resources.Load ("Effect/BloodFX"))as GameObject;
						effect.transform.position = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
					}
				} else {
					
					if (Monster.transform.childCount > 0) {
						for (int i = 0; i < Monster.transform.childCount; i++) {
							Monster.transform.GetChild (i).GetComponent<MonsterManager> ().MoveAttack ();

						}
					}
				}
			} else {
//				GameObject bullet = Instantiate (Resources.Load ("Effect/BulletHitGround"))as GameObject;
//				bullet.transform.position = hit.point;
				postionend = hit.point;
				StartCoroutine (EffectPlasmaRPGHitGround (hit.point));
			}
			if (hit.collider.gameObject.tag == "Ground") {
				if (tmpGun == "Plasma RPG") {
//					GameObject bullet = Instantiate (Resources.Load ("Effect/BulletHitGround"))as GameObject;
//					bullet.transform.position = hit.point;
				} else {
					GameObject bullet = Instantiate (Resources.Load ("Effect/BulletHitGround"))as GameObject;
					bullet.transform.position = hit.point;
				}
			}
		} else {
			if (hit.collider == null) {
//				Debug.Log ("Hit bang null");
			}
		}
		ShotGun.Instance.Ban ();
	}
	// Nhả súng
	public void NhaBan ()
	{
		ShotGun.Instance.NhaBan ();
		tamnho.GetComponent<Animator> ().Play ("Idle");
	}

	public void SlowMotion ()
	{
		if (tmpGun == "Plasma RPG") {
			
		} else {
			GameObject bullet = Instantiate (Resources.Load ("Bullet")) as GameObject;
			bullet.transform.eulerAngles = this.transform.GetChild (0).GetChild (0).GetComponent<SlowMotion> ().roObject.transform.eulerAngles;
			bullet.transform.position = this.transform.GetChild (0).GetChild (0).GetComponent<SlowMotion> ().slowmotion.transform.position;
		}
	}

	public	void EffectPlasmaRPG ()
	{
		GameObject bullet = Instantiate (Resources.Load ("BulletPlasmaRPG")) as GameObject;
		bullet.transform.eulerAngles = this.transform.GetChild (0).GetChild (0).GetComponent<SlowMotion> ().roObject.transform.eulerAngles;
		bullet.transform.position = this.transform.GetChild (0).GetChild (0).GetComponent<SlowMotion> ().slowmotion.transform.position;
	}

	IEnumerator EffectPlasmaRPGHitGround (Vector3 pos)
	{
//		Debug.Log ("Position:" + pos);
		yield return new WaitForSeconds (1);
		GameObject bullet = Instantiate (Resources.Load ("Effect/BulletHitGroundRPG"))as GameObject;
		bullet.transform.position = pos;
//		postionend = pos;
	}
}
