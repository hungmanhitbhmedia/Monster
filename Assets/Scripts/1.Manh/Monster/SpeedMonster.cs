using UnityEngine;
using System.Collections;

public class SpeedMonster : MonoBehaviour
{
	public float AnimationChay;
	public float AnimationBithuong;
	public float AnimaitonCham;
	public float speedChay;
	public float speedBithuong;
	public float speedDicham;

	SimpleAIA simple;

	public void Start ()
	{
		simple = this.GetComponent<SimpleAIA> ();
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.A)) {
			simple.StateDicham ();
		}
		if (Input.GetKey (KeyCode.S)) {
			simple.StateChay ();
		}
		if (Input.GetKey (KeyCode.D)) {
			simple.StateBithuong ();
		}
		if (Input.GetKey (KeyCode.F)) {
			simple.StateTrungDan ();
		}
	}
}
