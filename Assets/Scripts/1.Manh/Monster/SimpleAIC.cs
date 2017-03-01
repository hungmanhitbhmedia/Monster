using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleAIC : MonoBehaviour
{
	public List<Vector3> positionWay = new List<Vector3> ();
	public string nameWay;
	List<Collider> objecttmp = new List<Collider> ();

	Vector3 positionEnd;
	int count = 1;
	Animation ani;
	float speed;
	SpeedMonster speedmonster;
	List<Collider> arrayCollider = new List<Collider> ();

	void Start ()
	{
		ani = this.transform.GetChild (0).GetComponent<Animation> ();
		speedmonster = this.transform.GetComponent<SpeedMonster> ();
		if (this.GetComponent<MonsterManager> ().typeMonster != "C")
			return;
		positionEnd = positionWay [1];
		StateChay ();
	}

	public void Init (List<Vector3> tmp, string nameway)
	{
		positionWay = tmp;
		nameWay = nameway;
	}

	void Update ()
	{
		if (this.GetComponent<MonsterManager> ().die) {
			speed = 0;
		}
		if (this.GetComponent<MonsterManager> ().typeMonster != "C")
			return;
		Vector2 vec1 = new Vector2 (this.transform.position.x - positionEnd.x, this.transform.position.z - positionEnd.z);
		Vector2 vec2 = new Vector3 (0, 1);// trucj z
		//Get the dot product
		float dot = Vector2.Dot (vec1, vec2);
		// Divide the dot by the product of the magnitudes of the vectors
		dot = dot / (vec1.magnitude * vec2.magnitude);
		//Get the arc cosin of the angle, you now have your angle in radians 
		var acos = Mathf.Acos (dot);
		//Multiply by 180/Mathf.PI to convert to degrees
		float angle = acos * 180 / Mathf.PI;
		//Congrats, you made it really hard on yourself.
		angle = angle - 90;
		//		Debug.Log ("Angle:" + angle);
		if (positionEnd.x > this.transform.position.x) {
			angle = 90 + (90 - angle);
		}
		this.transform.eulerAngles = new Vector3 (0, angle, 0);
		if (!this.GetComponent<MonsterManager> ().die) {
			this.transform.Translate (Vector2.left * speed * Time.deltaTime);
		}
		if (GameManager.Instance.skill == 0) {
			speed = 0;
			this.transform.Translate (Vector2.left * speed * Time.deltaTime);
		}
	}

	void OnTriggerEnter (Collider collider)
	{
		if (this.GetComponent<MonsterManager> ().typeMonster != "C")
			return;
		if (collider.tag == "PointWay" && collider.name == nameWay) {
			if (objecttmp.Count > 0) {
				for (int i = 0; i < objecttmp.Count; i++) {
					if (objecttmp [i] == collider) {
						return;
					}
				}
			}
			count++;
			if (count < positionWay.Count) {
				positionEnd = positionWay [count];
				objecttmp.Add (collider);
//				Debug.Log ("Count:" + count);
				return;
			}
			Destroy (this.gameObject);
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
//		Debug.Log ("Trung dan");
		speed = 0;
		if (ani.GetClip ("Trungdan") == null) {
			StateBithuong ();
			return;
		}
		ani.Play ("Trungdan");
		Invoke ("StateBithuong", ani ["Trungdan"].length);
	}

	public void StateDie ()
	{
		CancelInvoke ();
		speed = 0;
	}
}
