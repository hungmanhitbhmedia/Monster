using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LoadRegionInGame : Singleton<LoadRegionInGame>
{
	
	public Text txgold;
	public Text txSilver;
	public Text txCraytal;
	public Text txEnery;


	float gold;
	float silver;
	float crystal;
	float energy;

	void Start ()
	{
		Init ();
		InvokeRepeating ("CheckIn", .5f, .5f);
	}

	public void Init ()
	{
		RegionInGame regioningame = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		gold = regioningame.Gold;
		crystal = regioningame.Crystal;
		energy = regioningame.Energy;
		silver = regioningame.Silver;
	}

	void CheckIn ()
	{
		if (txgold != null) {
			txgold.text = gold.ToString ();
		}
		if (txSilver != null) {
			txSilver.text = silver.ToString ();
		}
		if (txCraytal != null) {
			txCraytal.text = crystal.ToString ();
		}
		if (txEnery != null) {
			txEnery.text = energy + "/10";
		}
	}
}
