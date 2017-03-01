using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite4Unity3d;


public class RegionInGame
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }

	public int Region { get; set; }



	public int QuestNormal { get; set; }

	public int QuestSpecial { get; set; }

	public int QuestBoss { get; set; }

	public float Gold { get; set; }

	public float Silver { get; set; }

	public float Crystal { get; set; }

	public int Energy { get; set; }

	public string Gun{ get; set; }

	public int Exp{ get; set; }

	//
	//	private SQLiteConnection connection;
	//
	//	public RegionInGame ()
	//	{
	//		DataManager dt = new DataManager ("Database.db");
	//		connection = dt.connection;
	//	}

	public RegionInGame GetRegionInGame ()
	{
		RegionInGame region = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		return region;
	}

	public void ChangeGunUse (string gunNew)
	{
		RegionInGame region = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		region.Gun = gunNew;
		DataManager.Instance.connection.Update (region);
	}

	public void ChangeGoldSilveCrystal (float gold, float silve, float crystal)
	{
		RegionInGame region = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		region.Gold = region.Gold - gold;
		region.Silver = region.Silver - silve;
		region.Crystal = region.Crystal - crystal;
		DataManager.Instance.connection.Update (region);
	}

	public void UpdateReward (float gold, float silve, float crystal, string name)
	{
		RegionInGame region = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
//		region.Gold = region.Gold + gold;
//		region.Silver = region.Silver + silve;
//		region.Crystal = region.Crystal + crystal;
		switch (name) {
		case "Normal":
			if (region.Region == 1) {
				if (region.QuestNormal > 30) {
					region.QuestNormal = 1;
				} else {
					region.QuestNormal = region.QuestNormal + 1;
				}
			
			} else {
				if (region.QuestNormal > 20) {
					region.QuestNormal = 1;
				} else {
					region.QuestNormal = region.QuestNormal + 1;
				}
			}
			break;
		case "Endless":
			return;

			break;
		case "Boss":
			if (region.QuestBoss >= 2) {
				region.QuestBoss = 1;
				region.QuestNormal = 1;
			} else {
				region.QuestBoss = region.QuestBoss + 1;
			}
			break;
		case "Special":
			//region.QuestSpecial = region.QuestSpecial + 1;
			break;
		}
		DataManager.Instance.connection.Update (region);
	}

	public void UpdateRegion ()
	{
		RegionInGame region = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();

		region.Region = region.Region + 1;
		region.QuestNormal = 1;
		region.QuestBoss = 1;
		DataManager.Instance.connection.Update (region);

	}


	public void TestRegion (int _region, int _quest)
	{
		RegionInGame region = DataManager.Instance.connection.Table<RegionInGame> ().FirstOrDefault ();
		region.Region = _region;
		region.QuestNormal = _quest;

		DataManager.Instance.connection.Update (region);
	}
}