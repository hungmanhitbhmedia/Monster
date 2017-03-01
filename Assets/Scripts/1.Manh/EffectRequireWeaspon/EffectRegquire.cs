using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EffectRegquire : MonoBehaviour
{

	public int power;
	public int stability;
	public int capacity;
	public int maxzoom;

	private float powerUpdate;
	private float stabilityUpdate;
	private float capacityUpdate;
	private float maxzoomUpdate;


	private bool isColor;
	private bool checkeffect;

	private string gunNew;

	Rifles rifles;
	RegionInGame regioningame;
	UpdateGun updategun;
	GunInGame guningame;

	public  void Confirm (bool _effect)
	{
		
		CancelInvoke ();
		rifles = new Rifles ();
		regioningame = new RegionInGame ();
		updategun = new UpdateGun ();
		guningame = new GunInGame ();
		gunNew = regioningame.GetRegionInGame ().Gun;
		this.transform.GetChild (1).gameObject.SetActive (false);
		checkeffect = _effect;
		if (!_effect) {
			CancelInvoke ();
			this.gameObject.GetComponent<Image> ().color = new Color (1, 1, 1);
		} else {
			InvokeRepeating ("Ef", 0.2f, 0.2f);
		}
		float powerdefault = rifles.GetRifles (gunNew).Power;
		powerUpdate = (powerdefault + rifles.GetRifles (gunNew).PesentPower * power);
		float stabilitydefault = rifles.GetRifles (gunNew).Stability;
		stabilityUpdate = stabilitydefault + (stabilitydefault * updategun.GetDetail (Const.Stability, rifles.GetRifles (gunNew).Types) * stability) / 100;
		float capacitydefault = rifles.GetRifles (gunNew).Capacity;
		capacityUpdate = (capacitydefault + updategun.GetDetail (Const.Capacity, rifles.GetRifles (gunNew).Types) * capacity); 
		float maxzomdefault = rifles.GetRifles (gunNew).Maxzoom;
		maxzoomUpdate = (maxzomdefault + updategun.GetDetail (Const.Maxzoom, rifles.GetRifles (gunNew).Types) * maxzoom);
	}

	void Ef ()
	{
		isColor = !isColor;
		if (isColor) {
			this.gameObject.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		} else {
			this.gameObject.GetComponent<Image> ().color = new Color (0.84f, 0.62f, 0.62f);
			this.gameObject.GetComponent<Button> ().enabled = true;
		}
	}

	public void ConfirmPower ()
	{
		if (LoadQuestInGame.Instance.statehunt == LoadQuestInGame.StateHunt.Endless || LoadQuestInGame.Instance.statehunt == LoadQuestInGame.StateHunt.Special)
			return;


		if (checkeffect) {
			this.transform.GetChild (1).GetChild (0).GetComponent<Text> ().text = powerUpdate.ToString ();
			this.transform.parent.GetComponent<ToggleEffectRequire> ().Confirm (this.transform.GetChild (1).gameObject);
		}
	}

	public void ConfirmStability ()
	{
		if (LoadQuestInGame.Instance.statehunt == LoadQuestInGame.StateHunt.Endless || LoadQuestInGame.Instance.statehunt == LoadQuestInGame.StateHunt.Special)
			return;
		if (checkeffect) {
			this.transform.GetChild (1).GetChild (0).GetComponent<Text> ().text = stabilityUpdate.ToString ();
			this.transform.parent.GetComponent<ToggleEffectRequire> ().Confirm (this.transform.GetChild (1).gameObject);
		}
	}

	public void ConfirmCapacity ()
	{
		if (LoadQuestInGame.Instance.statehunt == LoadQuestInGame.StateHunt.Endless || LoadQuestInGame.Instance.statehunt == LoadQuestInGame.StateHunt.Special)
			return;
		if (checkeffect) {
			this.transform.GetChild (1).GetChild (0).GetComponent<Text> ().text = capacityUpdate.ToString ();
			this.transform.parent.GetComponent<ToggleEffectRequire> ().Confirm (this.transform.GetChild (1).gameObject);
		}
	}

	public void ConfirmMaxzoom ()
	{
		if (LoadQuestInGame.Instance.statehunt == LoadQuestInGame.StateHunt.Endless || LoadQuestInGame.Instance.statehunt == LoadQuestInGame.StateHunt.Special)
			return;
		if (checkeffect) {
			this.transform.GetChild (1).GetChild (0).GetComponent<Text> ().text = maxzoomUpdate.ToString ();
			this.transform.parent.GetComponent<ToggleEffectRequire> ().Confirm (this.transform.GetChild (1).gameObject);
		}
	}


	public void ButtonChangedUpgrade (GameObject panelUgrade)
	{
		panelUgrade.SetActive (true);
		UpgradeManager.Instance.Init (regioningame.GetRegionInGame ().Gun);
	}

	public void Reset ()
	{
		CancelInvoke ();
		this.gameObject.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
	}
}
