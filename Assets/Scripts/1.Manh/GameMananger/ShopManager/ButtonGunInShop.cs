using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonGunInShop : MonoBehaviour
{
	private int regionstart;
	private int regionend;
	private int region;
	private int active;
	GunInGame guningame;
	RegionInGame regioningame;
	Rifles rifles;


	void OnEnable ()
	{
		guningame = new GunInGame ();
		rifles = new Rifles ();
		regioningame = new RegionInGame ();
		region = regioningame.GetRegionInGame ().Region;
		active = guningame.GetGunInGame (this.gameObject.name).Active;
		this.transform.GetChild (2).GetComponent<Text> ().text = this.gameObject.name;
		this.transform.GetChild (2).GetComponent<Text> ().fontSize = 40;

		if (this.gameObject.GetComponent<Toggle> () != null) {
			this.gameObject.GetComponent<Toggle> ().isOn = false;
		}
		if (regioningame.GetRegionInGame ().Gun == this.gameObject.name) {
			// gun đang được sử dụng để bắn
			this.transform.GetChild (1).gameObject.SetActive (true);
		} else {
			// gun ko đk sử dụng
			this.transform.GetChild (1).gameObject.SetActive (false);
		}
		if (guningame.GetGunInGame (this.gameObject.name).Active == 1) {
			// gun đã được mua
			this.transform.GetChild (3).gameObject.SetActive (false);// ẩn pice của súng đi
		} else {// gun chưa được mua
			
			this.transform.GetChild (3).gameObject.SetActive (true);

			//load gold lên 
			this.transform.GetChild (3).GetChild (0).GetChild (0).GetComponent<Text> ().text = rifles.GetRifles (this.gameObject.name).Gold + "";
			// load silver
			this.transform.GetChild (3).GetChild (1).GetChild (0).GetComponent<Text> ().text = rifles.GetRifles (this.gameObject.name).Silver + "";
			// load crystal
			this.transform.GetChild (3).GetChild (2).GetChild (0).GetComponent<Text> ().text = rifles.GetRifles (this.gameObject.name).Crystal + "";

			// check gold
			if (!rifles.GetRifles (this.gameObject.name).Gold.Equals (0)) {
				this.transform.GetChild (3).GetChild (0).gameObject.SetActive (true);
			} else {
				this.transform.GetChild (3).GetChild (0).gameObject.SetActive (false);
			}
			// nếu mua bằng silve thì hiện silve lên
			if (!rifles.GetRifles (this.gameObject.name).Silver.Equals (0)) {
				this.transform.GetChild (3).GetChild (1).gameObject.SetActive (true);
			} else {
				this.transform.GetChild (3).GetChild (1).gameObject.SetActive (false);
			}
			// nếu mua bằng crystal thì hiện crystal lên
			if (!rifles.GetRifles (this.gameObject.name).Crystal.Equals (0)) {
				this.transform.GetChild (3).GetChild (2).gameObject.SetActive (true);
			} else {
				this.transform.GetChild (3).GetChild (2).gameObject.SetActive (false);
			}
		}

		// Load region
		regionstart = rifles.GetRifles (this.gameObject.name).RegionStart;
		regionend = rifles.GetRifles (this.gameObject.name).RegionEnd;
		if (regionstart == regionend) {
			this.transform.GetChild (4).GetComponent<Text> ().text = "Region: " + regionend;
		} else {
			this.transform.GetChild (4).GetComponent<Text> ().text = "Region: " + regionstart + " - " + regionend;
		}
	}

	public void Reload ()
	{
		OnEnable ();
		ShopManager.Instance.DetailGun (this.gameObject.name, this.gameObject);
	}

	public void ClickIonGun ()
	{
		//ShopManager.Instance.DetailGun (this.gameObject.name, this.gameObject);

		ShopManager.Instance.DetailGun (this.gameObject.name, this.gameObject);

		this.transform.parent.GetComponent<ToggleCustom> ().SetButtonOn (this.gameObject);
		ShopManager.Instance.ShowGunUI (this.gameObject.name);
	}


}
