using UnityEngine;
using System.Collections;

public class EffectClick :Singleton<EffectClick>
{
	GameObject tgObject;

	void Start ()
	{
		if (tgObject != null) {
			tgObject.SetActive (false);
			tgObject = null;
		}
	}

	public void Init ()
	{
		if (tgObject != null) {
			tgObject.SetActive (false);
			tgObject = null;
		}
	}

	public void OnClick (GameObject tg)
	{
		tg.SetActive (true);
		if (tg == tgObject) {
			return;
		}
		if (tgObject != null) {
			tgObject.SetActive (false);
		}
		tgObject = tg;
	}
}
