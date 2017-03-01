using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite4Unity3d;

public class RewardEndless
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }

	public int Region { get; set; }

	public int Ep { get; set; }

	public int Silver { get; set; }

	public int Crystal { get; set; }

	public int Gold { get; set; }

	//	private SQLiteConnection connection;
	//
	//	public RewardEndless ()
	//	{
	//		DataManager dt = new DataManager ("Database.db");
	//		connection = dt.connection;
	//	}

	public RewardEndless GetEndless (int region)
	{
		RewardEndless reward = DataManager.Instance.connection.Table<RewardEndless> ().Where (x => x.Region == region).FirstOrDefault ();
		return reward;
	}
}

