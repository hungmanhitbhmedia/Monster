using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite4Unity3d;

public class RequireMap
{
	//	SQLiteConnection connection;

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }

	public int Region{ get; set; }

	public int Rifles{ get; set; }

	public int BossR{ get; set; }

	public int Shotgun{ get; set; }

	public int BossS{ get; set; }

	public int Crossbow{ get; set; }

	public int BossC{ get; set; }

	public int AssaultRifles{ get; set; }

	public int BossA{ get; set; }

	//	public RequireMap ()
	//	{
	//		DataManager dt = new DataManager ("Database.db");
	//		connection = dt.connection;
	//	}

	public RequireMap GetRequireMap (int region)
	{
		RequireMap requiremap = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ();
		return requiremap;
	}

	public void UpdateRifles (int region)
	{
		RequireMap requiremap = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ();
		requiremap.Rifles = requiremap.Rifles + 1;
		DataManager.Instance.connection.Update (requiremap);
	}

	public void UpdateBossR (int region)
	{
		RequireMap requiremap = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ();
		requiremap.BossR = requiremap.BossR + 1;
		DataManager.Instance.connection.Update (requiremap);
	}

	public void UpdateShotgun (int region)
	{
		RequireMap requiremap = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ();
		requiremap.Shotgun = requiremap.Shotgun + 1;
		DataManager.Instance.connection.Update (requiremap);
	}

	public void UpdateBossS (int region)
	{
		RequireMap requiremap = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ();
		requiremap.BossS = requiremap.BossS + 1;
		DataManager.Instance.connection.Update (requiremap);
	}

	public void UpdateCrossbow (int region)
	{
		RequireMap requiremap = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ();
		requiremap.Crossbow = requiremap.Crossbow + 1;
		DataManager.Instance.connection.Update (requiremap);
	}

	public void UpdateBossC (int region)
	{
		RequireMap requiremap = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ();
		requiremap.BossC = requiremap.BossC + 1;
		DataManager.Instance.connection.Update (requiremap);
	}

	public void UpdateAssaultRifles (int region)
	{
		RequireMap requiremap = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ();
		requiremap.AssaultRifles = requiremap.AssaultRifles + 1;
		DataManager.Instance.connection.Update (requiremap);
	}

	public void UpdateBossA (int region)
	{
		RequireMap requiremap = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ();
		requiremap.BossA = requiremap.BossA + 1;
		DataManager.Instance.connection.Update (requiremap);
	}

	public void Test (int region)
	{
		RequireMap requiremap = DataManager.Instance.connection.Table<RequireMap> ().Where (x => x.Region == region).FirstOrDefault ();
		requiremap.Rifles = 1;
		DataManager.Instance.connection.Update (requiremap);
	}

}

