using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
	public float speed;
	public float speedani;
	// Use this for initialization
	void Start ()
	{
		//Move1 ();
		this.transform.GetChild (0).GetComponent<Animation> ().Play ("Run");
		Animation ani =	this.transform.GetChild (0).GetComponent<Animation> ();
		ani ["Run"].speed = speedani;
//		this.GetComponent<Animation> ().GetClip ("Run").sp;
		//Invoke ("Move2", 15);
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.Translate (new Vector3 (0, 0, 1) * speed * Time.deltaTime);
	
	}

	void Move1 ()
	{
		this.GetComponent<Animation> ().Play ("Run");
		Vector3 tmp = new Vector3 (0.33f, 0.59f, -8.734f);
		iTween.MoveTo (this.transform.gameObject, tmp, 4);
	}

	void Move2 ()
	{
		speed = 0;
		this.GetComponent<Animation> ().Play ("Idle");
	}
}
