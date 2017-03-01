using UnityEngine;
using System.Collections;

public class ToggleCustom : MonoBehaviour
{
	private GameObject btbefore;

	void OnEnable ()
	{
		for (int i = 0; i < this.transform.childCount; i++) {
			this.transform.GetChild (i).GetChild (0).gameObject.SetActive (false);
		}
	}

	public void SetButtonOn (GameObject btenable)
	{
		btenable.transform.GetChild (0).gameObject.SetActive (true);
		if (btbefore != null) {
			if (btenable == btbefore) {
				return;
			}
			btbefore.transform.GetChild (0).gameObject.SetActive (false);
		}

		btbefore = btenable.gameObject;
	}

}
