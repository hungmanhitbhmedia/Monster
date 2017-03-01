using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite4Unity3d;


public class Map
{
	[PrimaryKey, AutoIncrement]
	public int Id{ get; set; }

	public int Region{ get; set; }

	public int Rifles{ get; set; }

	public int AssaultRifles{ get; set; }

	public int ShotGun{ get; set; }

	public int CrossBows{ get; set; }


	//    private SQLiteConnection connection;
	private float typegun;

	//    public Map()
	//    {
	//        DataManager dt = new DataManager("Database.db");
	//        connection = dt.connection;
	//    }

	public float  GetQuest (int region, string _typegun)
	{
		Map map = DataManager.Instance.connection.Table<Map> ().Where (x => x.Region == region).FirstOrDefault ();
		switch (_typegun) {
		case Const.Rifles:
			typegun = map.Rifles;
			break;
		case Const.AssaultRifles:
			typegun = map.AssaultRifles;
			break;
		case Const.ShotGun:
			typegun = map.ShotGun;
			break;
		case Const.CrossBows:
			typegun = map.CrossBows;
			break;
		}
		return typegun;
	}
}
