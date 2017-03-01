using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite4Unity3d;

public class Rifles
{
	[PrimaryKey, AutoIncrement]
	public string Name { get; set; }

	public string Types { get; set; }

	public int RegionStart { get; set; }

	public int RegionEnd { get; set; }

	public int Crystal { get; set; }

	public float Silver { get; set; }

	public int Gold { get; set; }

	public int Power { get; set; }

	public float PesentPower { get; set; }

	public float Reload { get; set; }

	public float Fire { get; set; }

	public float Stability { get; set; }

	public float Capacity { get; set; }

	public float Maxzoom { get; set; }

	public int TamSung{ get; set; }

	public int MaxCapacity{ get; set; }

	public int MaxMaxzoom{ get; set; }


	// 0. Súng trường
	// 1. Súng tỉa lúc thay đạn từng viên một
	// 2. Shot gun
	// 3. Súng đặc biệt
	// 4. Cung
	// 5. Sung tỉa thay đạn cả băng 2
	public int KieuBan{ get; set; }

	//	private SQLiteConnection connnection;
	//
	//	public Rifles ()
	//	{
	//		DataManager dt = new DataManager ("Database.db");
	//		connnection = dt.connection;
	//	}

	public Rifles GetRifles (string name)
	{
		Rifles rifles = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == name).FirstOrDefault ();
		return rifles;
	}

	public IEnumerable<Rifles> GetReflesOnRegion (int regionend)
	{
		
		return DataManager.Instance.connection.Table<Rifles> ().Where (x => x.RegionEnd == regionend);
	}
}
