using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite4Unity3d;

public class RewardBoss
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }

	public int Region { get; set; }

	public int Quest { get; set; }

	public int Kill { get; set; }

	public string Monster { get; set; }

	public int Active { get; set; }

	public int Ep { get; set; }

	public int Silver { get; set; }

	public string Type{ get; set; }

	//	private SQLiteConnection connection;
	//
	//	public RewardBoss ()
	//	{
	//		DataManager dt = new DataManager ("Database.db");
	//		connection = dt.connection;
	//	}

	public RewardBoss GetRewardBoss (int region, int quest)
	{
		RewardBoss reward = DataManager.Instance.connection.Table<RewardBoss> ().Where (x => x.Region == region && x.Quest == quest).FirstOrDefault ();
		return reward;
	}

	public int CountQuest (int region)
	{
		int reward = DataManager.Instance.connection.Table<RewardBoss> ().Where (x => x.Region == region).Count ();
		return reward;
	}
}

