using UnityEngine;
using System.Collections;

public class UIHealth : MonoBehaviour
{


	void Update ()
	{
		var vec1 = new Vector2 (this.transform.position.x, this.transform.position.z + 6);
		var vec2 = new Vector2 (0, 1);
		//Get the dot product
		float dot = Vector3.Dot (vec1, vec2);
		// Divide the dot by the product of the magnitudes of the vectors
		dot = dot / (vec1.magnitude * vec2.magnitude);
		//Get the arc cosin of the angle, you now have your angle in radians 
		var acos = Mathf.Acos (dot);
		//Multiply by 180/Mathf.PI to convert to degrees
		var angle = acos * 180 / Mathf.PI;
		//Congrats, you made it really hard on yourself.
		this.transform.eulerAngles = new Vector3 (0, angle, 0);
	}
}
