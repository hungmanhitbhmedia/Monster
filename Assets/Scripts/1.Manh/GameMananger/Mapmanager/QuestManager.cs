using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
	public GameObject boss;
	public GameObject x;
	public GameObject effect;
	//	public GameObject effectClick;
	public string typegun;
	public int region;
	public int totarequest;

	public bool checkOverRegion;
	// Check qua region

	RequireMap requireMap;
	Map map;
	int request;

	void OnEnable ()
	{
		int tmpregion = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Region;
		if (region != tmpregion) {
//			Khoa ();
			return;
		}
		Init ();
	}

	public void Init ()
	{
		requireMap = new RequireMap ();
		Invoke ("CheckEnd", .1f);
	}

	void CheckEnd ()
	{
//		PlayerPrefs.SetInt ("SetRegion", region);
//		PlayerPrefs.Save ();
		switch (typegun) {
		case Const.Rifles:
//			request = requireMap.GetRequireMap (region).Rifles;
			request = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ().Rifles;
			break;
		case Const.ShotGun:
//			request = requireMap.GetRequireMap (region).Shotgun;
			request = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ().Shotgun;

			break;
		case Const.CrossBows:
//			request = requireMap.GetRequireMap (region).Crossbow;
			request = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ().Crossbow;

			break;
		case Const.AssaultRifles:
//			request = requireMap.GetRequireMap (region).AssaultRifles;
			request = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ().AssaultRifles;

			break;
		case "BossR":
//			request = requireMap.GetRequireMap (region).BossR;
			request = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ().BossR;

			break;
		case "BossS":
//			request = requireMap.GetRequireMap (region).BossS;
			request = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ().BossS;

			break;
		case "BossC":
//			request = requireMap.GetRequireMap (region).BossC;
			request = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ().BossC;

			break;
		case "BossA":
//			request = requireMap.GetRequireMap (region).BossA;
			request = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ().BossA;

			break;
		}
		if (request > this.totarequest) {
			this.GetComponent<Button> ().enabled = false;
			this.x.gameObject.SetActive (true);
			if (boss.gameObject != null) {
				boss.gameObject.SetActive (true);

			}
		} else {
			CheckButtonTypeGun ();
			this.GetComponent<RegionCurrent> ().SetRegion ();
			StartCoroutine (ClickEffect ());
			if (this.gameObject.name == "Boss") {
				LoadQuestInGame.Instance.ConfirmGunBoss ("Boss");
				LoadQuestInGame.Instance.CheckBoss (typegun);
			} else {
				
				if (this.typegun == "ShotGun") {
					LoadQuestInGame.Instance.ConfirmGrunNormal ("Shotgun");
					LoadQuestInGame.Instance.CheckWeaspon ("Shotgun");
				} else {
					LoadQuestInGame.Instance.ConfirmGrunNormal (typegun);
					LoadQuestInGame.Instance.CheckWeaspon (typegun);

				}

			}
		}
	}

	IEnumerator ClickEffect ()
	{
		yield return new WaitForSeconds (.3f);
		EffectClick.Instance.OnClick (effect);
	}

	public void Khoa ()
	{
		this.GetComponent<Button> ().enabled = false;
		this.x.gameObject.SetActive (true);
		if (boss.gameObject != null) {
			boss.gameObject.SetActive (true);
		}
	}

	public void CheckButtonTypeGun ()
	{
		PlayerPrefs.SetString ("CheckButtonTypeGun", this.typegun);
		PlayerPrefs.SetString ("BoolCheckButtonTypeGun", this.checkOverRegion.ToString ());
		PlayerPrefs.Save ();
	}
}
