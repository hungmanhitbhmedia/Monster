using UnityEngine;
using System.Collections;

public class MonsterTest : MonoBehaviour
{
	public GameObject _tranform;
	float angle;
	// Use this for initialization
	void Start ()
	{
	}

	void Update ()
	{
		Vector2 vec1 = new Vector2 (this.transform.position.x - _tranform.transform.position.x, this.transform.position.z - _tranform.transform.position.z);
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
//		Debug.Log ("Angle:" + angle);
		if (_tranform.transform.position.x > this.transform.position.x) {
			angle = 90 + (90 - angle);
		}
		this.transform.eulerAngles = new Vector3 (0, angle, 0);
		this.transform.Translate (Vector2.left * 3 * Time.deltaTime);
	}
}
