using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterMap : MonoBehaviour
{
    
	private string _monster;
	public Text txhead;
	public Text txbody;
	public Text txleg;
	public Text txtail;
	public Text txcrystal;
	public Text txstomach;
	public Text txwing;
	public Text txhand;
	public Text txmounth;



	void Start ()
	{
		Animation ani = this.transform.GetChild (0).GetComponent<Animation> ();
		if (Application.loadedLevelName == "GamePlay") {
			if (ani.GetClip ("Dicham") != null) {
				ani.Play ("Dicham");
				return;
			}
			if (ani.GetClip ("Tho") != null) {
				ani.Play ("Tho");
				return;
			}
			ani.Play ("Chay");
			return;
		}
		Monsters monster = DataManager.Instance.connection.Table<Monsters> ().Where (x => x.Name == this.gameObject.name).FirstOrDefault ();
		if (monster == null) {
			return;
		}
		if (!monster.Head.Equals (0) && txhead != null)
			txhead.text = monster.Head.ToString () + "%";
		if (!monster.Body.Equals (0) && txbody != null)
			txbody.text = monster.Body.ToString () + "%";
		if (!monster.Leg.Equals (0) && txleg != null)
			txleg.text = monster.Leg.ToString () + "%";
		if (!monster.Tail.Equals (0) && txtail != null)
			txtail.text = monster.Tail.ToString () + "%";
		if (!monster.Crystal.Equals (0) && txcrystal != null)
			txcrystal.text = monster.Crystal.ToString () + "%";
		if (!monster.Stomach.Equals (0) && txstomach != null)
			txstomach.text = monster.Stomach.ToString () + "%";
		if (!monster.Wing.Equals (0) && txwing != null)
			txwing.text = monster.Wing.ToString () + "%";
		if (!monster.Hand.Equals (0) && txhand != null)
			txhand.text = monster.Hand.ToString () + "%";
		if (!monster.Mounth.Equals (0) && txmounth != null)
			txmounth.text = monster.Mounth.ToString () + "%";
		if (ani.GetClip ("Dicham") != null) {
			ani.Play ("Dicham");
			return;
		}
		if (ani.GetClip ("Tho") != null) {
			ani.Play ("Tho");
			return;
		}
		ani.Play ("Chay");

	
//		Debug.Log ("............" + this.gameObject.name);
	}


}
