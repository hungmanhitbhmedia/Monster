using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite4Unity3d;

public class GunInGame
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }

	public string Name { get; set; }

	public int Active { get; set; }

	public int Stability { get; set; }

	public int Capacity { get; set; }

	public int Maxzoom { get; set; }

	public int Power { get; set; }



	//	private SQLiteConnection connection;
	private int index;

	public GunInGame ()
	{
		//DataManager dt = new DataManager ("Database.db");
		//connection = dt.connection;
	}

	public GunInGame GetGunInGame (string name)
	{
//		GunInGame gun = connection.Table<GunInGame> ().Where (x => x.Name == name).FirstOrDefault ();
		GunInGame gun = DataManager.Instance.connection.Table<GunInGame> ().Where (x => x.Name == name).FirstOrDefault ();
		return gun;
	}

	public void ChangeActive (string name)
	{
//		GunInGame gun = connection.Table<GunInGame> ().Where (x => x.Name == name).FirstOrDefault ();
		GunInGame gun = DataManager.Instance.connection.Table<GunInGame> ().Where (x => x.Name == name).FirstOrDefault ();

		gun.Active = 1;
		DataManager.Instance.connection.Update (gun);
	}

	//@ path: bộ phận nâng câos
	public void ChangedPath (string name, string path)
	{
		GunInGame gun = DataManager.Instance.connection.Table<GunInGame> ().Where (x => x.Name == name).FirstOrDefault ();
		switch (path) {
		case Const.Power:
			gun.Power = gun.Power + 1;
			break;
		case Const.Stability:
			gun.Stability = gun.Stability + 1;
			break;
		case Const.Capacity:
			gun.Capacity = gun.Capacity + 1;
			break;
		case Const.Maxzoom:
			gun.Maxzoom = gun.Maxzoom + 1;
			break;
		}
		DataManager.Instance.connection.Update (gun);
	}

	// path: bộ phận nâng cấp
	public int GetPathGun (string name, string path)
	{
		GunInGame gun = DataManager.Instance.connection.Table<GunInGame> ().Where (x => x.Name == name).FirstOrDefault ();
		switch (path) {
		case Const.Power:
			index = gun.Power;
			break;
		case Const.Stability:
			index = gun.Stability;
			break;
		case Const.Capacity:
			index = gun.Capacity;
			break;
		case Const.Maxzoom:
			index = gun.Maxzoom;
			break;
		}
		return index;
	}
}