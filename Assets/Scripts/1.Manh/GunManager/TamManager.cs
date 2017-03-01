using UnityEngine;
using System.Collections;

public class TamManager : MonoBehaviour
{
	public GameObject tamrifle;
	public GameObject tamshotgun;
	public GameObject tamassualfile;

	void OnEnable ()
	{
		string guncurrent = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Gun;
		string guntype = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == guncurrent).FirstOrDefault ().Types;
		tamrifle.SetActive (false);
		tamshotgun.SetActive (false);
		tamassualfile.SetActive (false);
		if (guntype == "Rifles") {
			tamrifle.SetActive (true);
		}
		if (guntype == "AssaultRifles") {
			tamassualfile.SetActive (true);
		}
		if (guntype == "Shotgun") {
			tamshotgun.SetActive (true);
		}
		if (guntype == "Specialweapon") {
			tamassualfile.SetActive (true);
		}
	}

}
