using UnityEngine;
using System.Collections;

public class BulletHitGround : MonoBehaviour
{

	void Start ()
	{
		Invoke ("DisableCollider", 1);
		Invoke ("DisableCollider1", 1.05f);
	}

	void DisableCollider ()
	{
		this.GetComponent<Collider> ().enabled = true;
		SoundManager.Instance.EffectNoDanRPG ();
	}

	void DisableCollider1 ()
	{
		this.GetComponent<Collider> ().enabled = false;
	}
}
