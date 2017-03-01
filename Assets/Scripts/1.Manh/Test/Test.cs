using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
	public InputField inputRegion;
	public InputField inputNormal;
	public InputField inputRifles;
	public InputField inputShotgun;
	public InputField inputAssaultRifles;
	public InputField boosR;
	public InputField bossS;
	public InputField bossA;

	public Text dialog;

	RegionInGame regioningame;
	RequireMap requireMap;

	void Start ()
	{
		regioningame = new RegionInGame ();
		requireMap = new RequireMap ();
	}

	public void ChangedRegion ()
	{
		if (inputRegion.text == null) {
			return;
		}
		int region = int.Parse (inputRegion.text);
		regioningame = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		requireMap = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ();
		if (inputNormal.text != null) {
			int normal = int.Parse (inputNormal.text);
			regioningame.QuestNormal = normal;
		}
		if (inputRifles.text != null) {
			int rifles = int.Parse (inputRifles.text);
			requireMap.Rifles = rifles;
		}
		if (inputShotgun.text != null) {
			int shotgun = int.Parse (inputShotgun.text);
			requireMap.Shotgun = shotgun;
		}
		if (inputAssaultRifles.text != null) {
			int assualt = int.Parse (inputAssaultRifles.text);
			requireMap.AssaultRifles = assualt;
		}
		requireMap.BossA = int.Parse (bossA.text);
		requireMap.BossS = int.Parse (bossS.text);
		requireMap.BossR = int.Parse (boosR.text);
		regioningame.Region = region;

		dialog.text = "Thay doi thanh cong";
		DataManager.Instance.connection.Update (regioningame);
		DataManager.Instance.connection.Update (requireMap);
		CapNhatThongTin.Instance.Uploadfile ();
		SceneManager.LoadScene ("Menu");

	}


	void Update ()
	{
		this.transform.eulerAngles = new Vector3 (0, 0, 0);
		this.transform.Translate (Vector2.left * 2 * Time.deltaTime);
	}
}
