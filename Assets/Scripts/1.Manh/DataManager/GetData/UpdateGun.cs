using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite4Unity3d;

public class UpdateGun
{
	[PrimaryKey, AutoIncrement]
	public string Name { get; set; }

	public float Rifles { get; set; }

	public float AssaultRifles { get; set; }

	public float ShotGun { get; set; }

	public float CrossBows { get; set; }

	public float Specialweapon { get; set; }

	//	private SQLiteConnection connecttion;
	private float index;

	//	public UpdateGun ()
	//	{
	//		DataManager dt = new DataManager ("Database.db");
	//		connecttion = dt.connection;
	//	}

	public float GetDetail (string path, string type)
	{
		UpdateGun updategun = DataManager.Instance.connection.Table<UpdateGun> ().Where (x => x.Name == path).FirstOrDefault ();
		switch (type) {
		case Const.Rifles:
			index = updategun.Rifles;
			break;
		case Const.AssaultRifles:
			index = updategun.AssaultRifles;
			break;
		case Const.ShotGun:
			index = updategun.ShotGun;
			break;
		case Const.CrossBows:
			index = updategun.CrossBows;
			break;
		case Const.Specialweapon:
			index = updategun.Specialweapon;
			break;
		case "Shotgun":
			index = updategun.ShotGun;
			break;
		}
		return index;
	}
}

