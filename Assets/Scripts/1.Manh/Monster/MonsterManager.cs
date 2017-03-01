using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterManager : Singleton<MonsterManager>
{
	public GameObject monsterparent;
	public GameObject effectShot;
	GameObject canvas;
	public Vector3 destination = new Vector3 (-16.64f, 1.92f, -1.21f);
	public Vector3 move;
	public float speed;
	public Image slider;
	public string typeMonster;
	// mau cua quai
	public float head;
	// teen sung ban
	private string gun;
	// sung kieu gi


	private string type;
	// strong sung
	private float powergun;

	private bool isattack;
	public bool die;

	private string _path;


	RegionInGame regioningame;
	Rifles rifles;
	Monsters monster;
	GunInGame guningame;




	void Start ()
	{
		Physics.IgnoreLayerCollision (7, 9);
		canvas = this.transform.GetChild (1).gameObject;
		canvas.SetActive (false);
		regioningame = new RegionInGame ();
		rifles = new Rifles ();
		monster = new Monsters ();
		guningame = new GunInGame ();
		gun = regioningame.GetRegionInGame ().Gun;
		type = rifles.GetRifles (gun).Types;
		head = monster.GetHeadMonster (this.gameObject.name, type);
		powergun = rifles.GetRifles (gun).Power +
		rifles.GetRifles (gun).PesentPower * guningame.GetGunInGame (gun).Power;
	}

	public void SubtractHead (string path)
	{
		_path = path;
		if (!die) {
			canvas.SetActive (true);
			powergun = rifles.GetRifles (gun).Power +
			rifles.GetRifles (gun).PesentPower * guningame.GetGunInGame (gun).Power;
//			Debug.Log ("Head:" + this.gameObject.name + "???" + head);
//			Debug.Log ("Hit damage" + powergun * monster.GetHeadSubtract (this.gameObject.name, path) / 100);
			head = head - powergun * monster.GetHeadSubtract (this.gameObject.name, path) / 100;
			if (head > 0) {
				slider.fillAmount = head / monster.GetHeadMonster (this.gameObject.name, type);
				//this.transform.GetChild (0).GetComponent<Animation> ().Play ("Bithuong");
				this.GetComponent<SimpleAIA> ().StateBithuong ();
				this.GetComponent<SimpleAIB> ().StateBithuong ();
				CoinEffect ();
//				Animation ani = this.transform.GetChild (0).GetComponent<Animation> ();
//				ani ["Bithuong"].speed = 5;
				//GameManager.Instance.ConfirmMonster ();
			} else {
				if (GameEnd.Instance.IsGameOver) {
					return;
				}
				//Monster die;
				CancelInvoke ();
				die = true;
				slider.fillAmount = 0;
				slider.enabled = false;
				speed = 0;
				this.transform.parent = null;
				this.GetComponent<SimpleAIA> ().StateDie ();
				this.GetComponent<SimpleAIC> ().StateDie ();
				this.GetComponent<SimpleAIB> ().StateDie ();
				this.GetComponent<SoundMonter> ().SetSound ();
				CoinEffect ();
				this.transform.GetChild (1).gameObject.SetActive (false);
//				GameManager.Instance.ConfirmMonsterDie (this.gameObject);
				this.transform.GetChild (0).GetComponent<Animation> ().Stop ();
				StartCoroutine (AnimationDie (0.1f));
				Invoke ("IsKinematic", 2);
				//GameManager.Instance.ConfirmMonster ();
				GameManager.Instance.ConfirmSkill (this.gameObject);
			}
		}
	}

	public void stopAnimation ()
	{
		this.transform.GetComponent<SimpleAIA> ().StateDie ();
		this.transform.GetChild (0).GetComponent<Animation> ().enabled = false;
	}

	public	IEnumerator AnimationDie (float time)
	{
		yield return new	WaitForSeconds (time);
		if (!GameEnd.Instance.isEnd) {
			this.transform.eulerAngles = Vector3.Lerp (this.transform.eulerAngles, Vector3.zero, 0.2f);
			this.transform.GetChild (0).GetComponent<Animation> ().Play ("Chet");
			Animation ani = this.transform.GetChild (0).GetComponent<Animation> ();
			ani ["Chet"].speed = 1;
			this.GetComponent<SimpleAIA> ().StateDie ();
			this.GetComponent<SimpleAIC> ().StateDie ();
			this.GetComponent<SimpleAIB> ().StateDie ();
			CancelInvoke ();
			slider.gameObject.SetActive (false);
			IsKinematic ();
			Destroy (this.gameObject, 5);

		} else {
//			Invoke ("Die", 2.3f);
			StartCoroutine (Die ());
		}
	}

	IEnumerator Die ()
	{
		yield return new WaitForSeconds (2.3f);
		this.GetComponent<SimpleAIA> ().StateDie ();
		this.GetComponent<SimpleAIC> ().StateDie ();
		this.GetComponent<SimpleAIB> ().StateDie ();
		CancelInvoke ();
		this.transform.eulerAngles = Vector3.Lerp (this.transform.eulerAngles, Vector3.zero, 0.2f);
		this.transform.GetChild (0).GetComponent<Animation> ().Play ("Chet");
		Destroy (this.gameObject, 10);
	}

	void IsKinematic ()
	{
		this.GetComponent<Rigidbody> ().mass = 1;
		//this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		//this.GetComponent<Rigidbody> ().Sleep ();

	}



	public void MoveAttack ()
	{
		if (!isattack) {
			isattack = true;
			speed = 1;
		}
	}

	//	void OnCollisionEnter (Collision collision)
	//	{
	//		if (collision.gameObject.tag == "Player") {
	//			this.transform.GetChild (0).GetComponent<Animation> ().Play ("Attack");
	//			this.GetComponent<SimpleAI> ().speed = 0;
	//			Animation ani = this.transform.GetChild (0).GetComponent<Animation> ();
	//			Invoke ("GameOVer", ani ["Attack"].length);
	//		}
	//	}

	bool isdie = false;

	void OnTriggerEnter (Collider collider)
	{
		if (collider.gameObject.tag == "Player") {
			if (!isdie) {
				this.GetComponent<SimpleAIA> ().StateAttack ();
				Animation ani = this.transform.GetChild (0).GetComponent<Animation> ();
				GameEnd.Instance.IsGameOver = true;
				StartCoroutine (GameOVer (5));
				GameEnd.Instance.EffectDie ();
			}
		}
		if (collider.gameObject.tag == "Boom") {
			if (!isdie) {
				this.GetComponent<SoundMonter> ().SetSound ();
				isdie = true;
//				Debug.Log ("Chung dan boom");
				head = 0;
				CoinEffect ();
				this.transform.GetChild (1).gameObject.SetActive (false);
				this.transform.GetChild (0).GetComponent<Animation> ().Stop ();
				StartCoroutine (AnimationDie (0.5f));
				slider.fillAmount = 0;
				die = true;
				slider.enabled = false;
				speed = 0;
				Invoke ("IsKinematic", 2);
				this.transform.parent = null;
				GameManager.Instance.ConfirmSkill (this.gameObject);
			}
		}
	}


	IEnumerator GameOVer (float time)
	{
		yield return new WaitForSeconds (time);
		if (!die) {
			GameEnd.Instance.GameOver ();
		} else {
			
		}
	}

	void CoinEffect ()
	{
		//		GameObject coin = Instantiate (Resources.Load ("Coin"))as GameObject;
		//		coin.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 5, this.transform.position.z);
		//		coin.GetComponent<Coin> ().path = _path;
		//		Destroy (coin, 2);
		switch (_path) {
		case "Head":
			EffecShot (0);
			break;
		case "Body":
			EffecShot (1);
			break;
		case"Leg":
			EffecShot (2);
			break;
		case "Tail":
			EffecShot (3);
			break;
		case "Crystal":
			EffecShot (4);
			break;
		case "Stomach":
			EffecShot (5);
			break;
		case "Wing":
			EffecShot (6);
			break;
		case "Hand":
			EffecShot (7);
			break;
		case "Mounth":
			EffecShot (8);
			break;
		}
	}

	public void EffecShot (int index)
	{
		effectShot.SetActive (false);
		effectShot.SetActive (true);
		for (int i = 0; i < effectShot.transform.childCount; i++) {
			effectShot.transform.GetChild (i).gameObject.SetActive (false);
		}
		effectShot.transform.GetChild (index).gameObject.SetActive (true);
	}

}
