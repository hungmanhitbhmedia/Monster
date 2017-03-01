using UnityEngine;
using System.Collections;

public class SetCollider : MonoBehaviour
{
	public MonsterManager monter;

	public void ConfirmHead ()
	{
		//this.gameobject.tag la vi tri cua lan ban
		if (monter == null) {
			return;
		}
		monter.SubtractHead (this.gameObject.tag);
	}
}
