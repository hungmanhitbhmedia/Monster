using UnityEngine;
using System.Collections;

public class ThayDanSungDacBiet :Singleton<ThayDanSungDacBiet>
{
	public GameObject daudan;

	public void ThayDan ()
	{

		if (GameEnd.Instance.IsGameOver) {
			CancelInvoke ();
			return;
		}
		if (this.gameObject.name == "Plasma RPG") {
			if (Shot.Instance.hit.collider != null) {
				Shot.Instance.EffectPlasmaRPG ();
				if (daudan != null) {
					daudan.SetActive (false);
				}
			}
		}
		if (this.gameObject.name == "Gatlinggun") {
			if (GameEnd.Instance.isEnd) {
				SoundManager.Instance.ThayDanSung6long ();
			}
		}
		Invoke ("ThayDan1", 1.14f);
	}

	public void ThayDan1 ()
	{
		this.transform.GetChild (0).GetComponent<Animation> ().Play ("Rutsung");
		if (daudan != null) {
			daudan.SetActive (true);
		}
		Invoke ("ThayDanXong", 2.13f);
	}

	void ThayDanXong ()
	{
		ShotGun.Instance.capacity = ShotGun.Instance.capacitymax;
		ShotGun.Instance.isthaydan = false;
	}
	 
}
