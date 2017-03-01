using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyManager : Singleton<BuyManager>
{
	public GameObject panelWeapon;
	public GameObject panelWeaponDetail;
	public GameObject panelConfirmMuaban;
	public GameObject dialog;
	public Text txmessage;




	//enum để xác định người chơi mua gói nào
	enum TypeItem
	{
		Silver,
		Gold,
		Energy}

	;

	TypeItem typeItem;

	// property khi click vào một vật phẩm cần mua
	// value là giá trị của vật phẩm khi đk mua
	//price là giá tiền, có thể bằng tiền thật orr là bằng gold. Giá này liên quan đến phần của a Hưng, mình không phải
	int value;
	int price;
	string message;
	//	string identifier;



	public void BuyGold (int _value)
	{
		UIController.Instance.googleanaytic.LogEvent ("Mua", "Mua gold:" + _value, "", 1);
		message = "gold";
		typeItem = TypeItem.Gold;
		value = _value;
	}

	public void BuySilver (int _value)
	{
		UIController.Instance.googleanaytic.LogEvent ("Mua", "Mua silver:" + _value, "", 1);

		message = "silver";

		typeItem = TypeItem.Silver;
		value = _value;
	}

	public void BuyEnergy (int _value)
	{
		UIController.Instance.googleanaytic.LogEvent ("Mua", "Mua energy:" + _value, "", 1);

		message = "silver";

		typeItem = TypeItem.Energy;
		value = _value;
	}

	public void PriceItem (int _price)
	{
		
		price = _price;
		if (typeItem != TypeItem.Energy) {
			ShowDialog ("Do you want to buy " + value + " " + typeItem.ToString () + " with " + price + "$");
		} else {
			ShowDialog ("Do you want to buy " + value + " " + typeItem.ToString () + " with " + price + " gold");
		}
	}

	public void IdetifieStore (string idetifi)
	{
		Purchaser.Instance.kProductIDConsumable = idetifi;
	}

	// Button bắt đầu purchase
	public void Buy ()
	{
		UIController.Instance.googleanaytic.LogEvent ("Mua", "Mua gold:" + value + " (YES)", "", 1);
		if (typeItem != TypeItem.Energy) {
//			Purchaser.Instance.BuyProductID (idetifi);
			Purchaser.Instance.BuyConsumable ();
		} else {
			int gold = (int)DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ().Gold;
			if (gold < price) {
				ShowDialog ("Your gold is not enough");
				return;
			} else {
				BuyCallBack ();
			}
		}
	}

	public void NOBuy ()
	{
		UIController.Instance.googleanaytic.LogEvent ("Mua", "Mua gold:" + value + " (NO)", "", 1);
	}

	//	xử lí call back
	public void BuyCallBack ()
	{
		RegionInGame regioningame = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		switch (typeItem) {
		case TypeItem.Gold:
			regioningame.Gold = regioningame.Gold + value; 
			PlayerPrefs.SetInt ("BuyItem", 1);
			PlayerPrefs.Save ();
			break;
		case TypeItem.Silver:
			regioningame.Silver = regioningame.Silver + value; 
			PlayerPrefs.SetInt ("BuyItem", 1);
			PlayerPrefs.Save ();
			break;
		case TypeItem.Energy:
			regioningame.Energy = regioningame.Energy + value;
			regioningame.Gold = regioningame.Gold - price;
			panelConfirmMuaban.SetActive (false);
			break;
		}
		DataManager.Instance.connection.Update (regioningame);
//		Debug.Log ("Da Mua thanh cong roi ma??????????????");
		CapNhatThongTin.Instance.Uploadfile ();
	}

	// thông báo lên xem người chơi mua gói nào
	public void ShowDialog (string message)
	{
		dialog.SetActive (true);
		txmessage.text = message;
	}

	public void ConfirmShow3D ()
	{
		if (panelWeapon.activeSelf || panelWeaponDetail.activeSelf) {
			ShopManager.Instance.ShowGunUI (ShopManager.Instance.gun);
		}
	}
}
