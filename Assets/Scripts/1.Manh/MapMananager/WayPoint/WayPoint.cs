using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour
{
	//	void Start ()
	//	{
	//
	//	}

	//	void OnTriggerEnter (Collider collider)
	//	{
	//		if (collider.tag == "Monster") {
	//			Debug.Log ("Way Point");
	//		}
	//	}
	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Ground") {
			Rigidbody rigibody = this.GetComponent<Rigidbody> ();
			rigibody.velocity = Vector3.zero;
			rigibody.useGravity = false;
			this.GetComponent<SphereCollider> ().isTrigger = true;
		}
	}
}
