using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
	public Image bar;
	public GameObject starWhile;
	public GameObject starYello;
	public Image icon;
	public Text txRank;
	public Text txchiso;


	private int expInGame;
	private List<Rank> rank;
	public Sprite[] spriteIcon;


	DataManager datamaganer;

	void Start ()
	{
		datamaganer = DataManager.Instance;
		expInGame = datamaganer.connection.Table<RegionInGame> ().FirstOrDefault ().Exp;
		rank = datamaganer.connection.Table<Rank> ().ToList ();
		rank = rank.OrderBy (x => x.Exp).ToList ();
		for (int i = 0; i < rank.Count; i++) {
			if (rank [i].Exp > expInGame) {
				txRank.text = rank [i - 1].Name + ": " + rank [i - 1].Level;
				icon.sprite = spriteIcon [rank [i - 1].Capdo];
				if (i < 7) {
					starWhile.transform.GetChild (rank [i - 1].Level - 1).gameObject.SetActive (true);
				} else {
					starYello.transform.GetChild (rank [i - 1].Level - 1).gameObject.SetActive (true);
				}
				float f1 = expInGame;
				float f2 = rank [i - 1].Exp;
				float f3 = rank [i].Exp;
				bar.fillAmount = (f1 - f2) / (f3 - f2);
				txchiso.text = f1 + "/" + f3;
				return;
			}
		}
	}
}
