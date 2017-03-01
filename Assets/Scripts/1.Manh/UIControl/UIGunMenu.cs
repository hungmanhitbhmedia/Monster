using UnityEngine;
using System.Collections;

public class UIGunMenu : MonoBehaviour
{
	public GameObject UImodelGun;
	public GameObject panlMenu;
	public GameObject panelWeapon;
	public GameObject panelWeaponDetail;

	void OnEnable ()
	{
		if (this.transform.name != "PanelMenu") {
			return;
		}
		if (UImodelGun.transform.childCount > 0) {
			Destroy (UImodelGun.transform.GetChild (0).gameObject);
		}
		string _gun = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Gun;
		GameObject gun = Instantiate (Resources.Load ("GunUI/" + _gun + ""))as GameObject;
//		/gun.transform.SetParent (UImodelGun.transform);
		gun.transform.parent = UImodelGun.transform;
		gun.transform.localScale = new Vector3 (4, 4, 4);
		gun.transform.localPosition = new Vector3 (gun.transform.position.x, gun.transform.position.y, gun.transform.position.z);
		gun.GetComponent<HandleRotation> ().isGun = false;
		gun.GetComponent<Collider> ().enabled = false;
	}

	void Update ()
	{
		if (this.transform.name == "UIGunMenu" && !panlMenu.activeSelf && !panelWeapon.activeSelf && !panelWeaponDetail.activeSelf) {
			if (this.transform.childCount == 0) {
				return;
			}
			Destroy (this.transform.GetChild (0).gameObject);
		}
	}
	//	public void
}
