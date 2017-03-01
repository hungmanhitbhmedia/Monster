using UnityEngine;
using System.Collections;

public class SimpleAIA : Singleton<SimpleAIA>
{

	// tốc độ di chuyển của từng trạng thái
	// Do aniamtion của từng con quái khác nhau nên phải khởi tao cái này cho phù hợp với từng con

	public float speed;




	private float angle;
	private float angleMax;
	private float angleMin;
	float fix = -0.5f;
	private float count;
	private bool isbiban;

	Animation ani;
	SpeedMonster speedmonster;



	public enum State
	{
		Idle,
		Transfer,
		MoveAttack,
		Attack,
		Die
	}

	public State state;

	void Start ()
	{
		ani = this.transform.GetChild (0).GetComponent<Animation> ();
		speedmonster = this.GetComponent<SpeedMonster> ();
		if (this.GetComponent<MonsterManager> ().typeMonster != "A")
			return;	
		angle = UnityEngine.Random.Range (0, 360);
		Init ();
	}

	void Init ()
	{
//		Debug.Log ("Init");
		ComfirmAIRelax ();
//		float tmpX = (xMax + xMin) / 2;
//		float tmpZ = (zMax + zMin) / 2;
//		 Kiểm tra quái được sinh ra, nếu nó thuộc trong vùng camera nhìn thấy thì nó đứng yên 
//		if ((position.x > xMin + tmpX / 2 && position.x < xMax - tmpX / 2) && (position.z > zMin + tmpZ / 2 && position.z < zMax - tmpZ / 2)) {
//			state = State.Idle;
//			speed = 1;
//			//set animation;
//			Animation ani = this.transform.GetChild (0).GetComponent <Animation> ();
//			angle = UnityEngine.Random.Range (-110, 90);
//			if (ani.GetClip ("Idle") != null) {
//				ani.Play ("Idle");
//			}
//		} else {
//			speed = 1f;
//			if (position.z < 0) {
//				angle = UnityEngine.Random.Range (45, 90);
//			} else {
//				angle = UnityEngine.Random.Range (-110, -45); 
//			}
//		}
//		if (ani.GetClip ("Tho") != null) {
//			ani.Play ("Tho");
//
//		} else if (ani.GetClip ("Idle") != null) {
//			ani.Play ("Idle");
//		} else {
//			ani.Play ("Chay");
//			ani ["Chay"].speed = ani ["Chay"].speed / 5;
//			speed = 1;
//		}
		//InvokeRepeating ("ComfirmAIRelax", timeLoop, timeLoop);

	}

	// Hàm này để tính góc quay lại. Khi mà quái  nó ko quay về phía mình.
	void AngleDistion ()
	{
		Vector2 vec1 = new Vector2 (this.transform.position.x, this.transform.position.z + 6);
		Vector2 vec2 = new Vector3 (0, 1);// trucj z
		//Get the dot product
		float dot = Vector2.Dot (vec1, vec2);
		// Divide the dot by the product of the magnitudes of the vectors
		dot = dot / (vec1.magnitude * vec2.magnitude);
		//Get the arc cosin of the angle, you now have your angle in radians 
		var acos = Mathf.Acos (dot);
		//Multiply by 180/Mathf.PI to convert to degrees
		angle = acos * 180 / Mathf.PI;
		//Congrats, you made it really hard on yourself.
		angle = angle - 90;
		angleMax = angle + 100;
		angleMin = angle - 100;
//		if (count < 4) {
//			InvokeRepeating ("ProcessRotation", 0, 4);
//		}
	}

	void Update ()
	{
		if (this.GetComponent<MonsterManager> ().die) {
			speed = 0;
		}
		if (this.GetComponent<MonsterManager> ().typeMonster != "A")
			return;	
		if (GameEnd.Instance.isEnd) {
			return;
		}
		this.transform.eulerAngles = new Vector3 (0, angle, 0);
		if (!this.GetComponent<MonsterManager> ().die) {
			this.transform.Translate (Vector2.left * speed * Time.deltaTime);
		}
		if (GameManager.Instance.skill == 0) {
			speed = 0;
			this.transform.Translate (Vector2.left * speed * Time.deltaTime);
		}
		if (state == State.MoveAttack) {
			SetAttack ();
		}

	}

	public void SetAttack ()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		float distance = Vector3.Distance (player.transform.position, this.gameObject.transform.position);
		if (distance < 35) {
			AngleDistion ();
			state = State.Attack;
		} else {
			if (angle < angleMin) {
				fix = 0.5f;
			}
			if (angle > angleMax) {
				fix = -0.5f;
			}
			angle = angle + fix;
		}
	}

	void ProcessRotation ()
	{
		count++;
//		Debug.Log (count);
		if (count == 1) {
			angle = angle - 50;
		} else if (count == 2) {
			angle = angle + 100;
		} else {
			AngleDistion ();
		}
	}

	public void ConfirmMoveBiThuong ()
	{
		if (this.GetComponent<MonsterManager> ().typeMonster != "A")
			return;
		if (isbiban)
			return;
		CancelInvoke ();
//		Debug.Log ("Phat hien bi ban");
		speed = 0;
		Invoke ("ChayChon", UnityEngine.Random.Range (1, 5));
		//		//ani [nameanimation].speed = 2.5f;
		//		if (ani.GetClip ("Idle") != null) {
		//			ani.Play ("Idle");
		//			Invoke ("ChayChon", UnityEngine.Random.Range (1, 5));
		//		} else if (ani.GetClip ("Tho") != null) {
		//			ani.Play ("Tho");
		//			Invoke ("ChayChon", UnityEngine.Random.Range (1, 5));
		//		} else {
		//			Invoke ("ChayChon", UnityEngine.Random.Range (1, 5));
		//		}
	}

	void ChayChon ()
	{
//		int ran = UnityEngine.Random.Range (0, 2);
//		if (!isbiban && !this.GetComponent<MonsterManager> ().die) {
//			isbiban = true;
//			switch (ran) {
//			case 0:
//				AngleDistion ();
//				StateChay ();
//				Debug.Log ("Attack");
//				state = State.MoveAttack;
//				//iTween.RotateTo (this.gameObject, iTween.Hash ("rotation", angle, "time", 1.5f, "oncomplete", "StateAttack"));
//				break;
//			case 1:
//				int tmp = UnityEngine.Random.Range (0, 2);
//				switch (tmp) {
//				case 0:
//					angle = UnityEngine.Random.Range (45, 90);
//					break;
//				case 1:
//					angle = UnityEngine.Random.Range (-110, -45); 
//					break;
//				}
//				StateChay ();
//				//iTween.RotateTo (this.gameObject, iTween.Hash ("rotation", angle, "time", 1.5f, "oncomplete", "StateChay"));
//				break;
//			}
//		}
		if (!isbiban && !this.GetComponent<MonsterManager> ().die) {
			isbiban = true;
			AngleDistion ();
			StateChay ();
//			Debug.Log ("Attack");
			state = State.MoveAttack;
		}
	}




	public void StateIdle ()
	{
		if (this.GetComponent<MonsterManager> ().die)
			return;
		if (ani.GetClip ("Idle") == null) {
			StateDicham ();
			return;
		}
		ani.Play ("Idle");
		speed = 0;

	}

	public void StateTho ()
	{
		if (this.GetComponent<MonsterManager> ().die)
			return;
		if (ani.GetClip ("Tho") == null) {
			StateDicham ();
			return;				
		}
		ani.Play ("Tho");
		speed = 0;
	}

	public void StateAttack ()
	{
		if (this.GetComponent<MonsterManager> ().typeMonster != "A")
			return;	
//		Debug.Log ("Tancong");
		ani.Play ("Attack");
		speed = 0;
	}

	public void StateBithuong ()
	{
		if (ani.GetClip ("Bithuong") == null) {
			StateDicham ();
			return;
		}
		ani.Play ("Bithuong");
		ani ["Bithuong"].speed = speedmonster.AnimationBithuong;
		speed = speedmonster.speedBithuong;
	}

	public void StateChay ()
	{
//		Debug.Log ("Chay");
		ani.Play ("Chay");
		speed = speedmonster.speedChay;
		ani ["Chay"].speed = ani ["Chay"].speed * speedmonster.AnimationChay;
	}

	public void StateDicham ()
	{
		if (this.GetComponent<MonsterManager> ().die)
			return;
		if (ani.GetClip ("Dicham") == null) {
//			Debug.Log ("DichamChay");
			ani.Play ("Chay");
			ani ["Chay"].speed = ani ["Chay"].speed * speedmonster.AnimaitonCham;
		} else {
//			Debug.Log ("Dicham");
			ani.Play ("Dicham");
		}

		speed = speedmonster.speedDicham;
	}

	public void StateTrungDan ()
	{
		if (ani.GetClip ("Trungdan") == null) {
			StateBithuong ();
			return;
		}
		ani.Play ("Trungdan");
		speed = 0;
		Invoke ("StateBithuong", ani ["Trungdan"].length);
	}

	public void StateDie ()
	{
		CancelInvoke ();
		speed = 0;
	}



	// DOCUMENT AI MONSTER;
	// Hàm này xác định hướng tiếp theo phải làm của quái. Khi mà quái chưa bị bắn
	//

	public void ComfirmAIRelax ()
	{
		CancelInvoke ();
		int tmp = UnityEngine.Random.Range (0, 3);
		if (ani.GetClip ("Idle") != null) {
			StateIdle ();
		} else if (ani.GetClip ("Tho") != null) {
			StateTho ();
		} else {
			StateDicham ();
		}

		switch (tmp) {
		case 0:
			AI1 ();
			break;
		case 1:
			AI2 ();
			break;
		case 2:
			AI3 ();
			break;
		}
	}

	private void AI1 ()
	{
		Invoke ("StateIdle", 3);
		Invoke ("StateTho", 6);
		Invoke ("StateDicham", 10);
		Invoke ("ComfirmAIRelax", 15);
	}

	private void AI2 ()
	{
		Invoke ("StateDicham", 4);
		Invoke ("StateTho", 7);
		Invoke ("StateDicham", 11);
		Invoke ("ComfirmAIRelax", 15);
	}

	private void AI3 ()
	{
		Invoke ("StateIdle", 3);
		Invoke ("StateDicham", 6);
		Invoke ("StateTho", 10);
		Invoke ("ComfirmAIRelax", 15);
	}

}
