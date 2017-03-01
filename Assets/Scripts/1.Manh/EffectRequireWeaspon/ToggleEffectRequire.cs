using UnityEngine;
using System.Collections;

public class ToggleEffectRequire : MonoBehaviour {

	private GameObject btbegin;
	public void Confirm(GameObject g)
	{
		g.gameObject.SetActive (true);
		if (btbegin != null) {
			btbegin.SetActive (false);
		}
		if (btbegin = g) {
			btbegin.SetActive (true);
		}
		btbegin = g.gameObject;
	}
}
