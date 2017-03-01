using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite4Unity3d;


public class RewardSpecial
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }

	public int Region { get; set; }

	public int Quest { get; set; }

	public int Kill { get; set; }

	public string Monster { get; set; }

	public int Ep { get; set; }

	public int Silver { get; set; }

	public int Gold { get; set; }

	public int Crystal { get; set; }

	public int Maxmonster { get; set; }

	public string Type{ get; set; }

	//	private SQLiteConnection connection;
	//
	//	public RewardSpecial ()
	//	{
	//		DataManager dt = new DataManager ("Database.db");
	//		connection = dt.connection;
	//	}

	public RewardSpecial GetRewardNomal (int region, int quest)
	{
		RewardSpecial reward = DataManager.Instance.connection.Table<RewardSpecial> ().Where (x => x.Region == region && x.Quest == quest).FirstOrDefault ();
		return reward;
	}

	public int CountQuest (int region)
	{
		int reward = DataManager.Instance.connection.Table<RewardSpecial> ().Where (x => x.Region == region).Count ();
		return reward;
	}
}
