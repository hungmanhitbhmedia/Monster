using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite4Unity3d;

public class Monsters
{
	[PrimaryKey, AutoIncrement]
	public string Name { get; set; }

	public float Rifles { get; set; }

	public float AssaultRifles { get; set; }

	public float Shotgun { get; set; }

	public float CrossBow { get; set; }

	public float Specialweapon { get; set; }

	public float Head { get; set; }

	public float Body { get; set; }

	public float Leg { get; set; }

	public float Tail { get; set; }

	public float Crystal { get; set; }

	public float Stomach { get; set; }

	public float Wing { get; set; }

	public float Hand { get; set; }

	public float Mounth { get; set; }

	public float Region { get; set; }

	//	private SQLiteConnection connection;
	private float _head;
	private float _subtracthead;

	//	public Monsters ()
	//	{
	//		DataManager dt = new DataManager ("Database.db");
	//		connection = dt.connection;
	//	}

	public Monsters GetMonster (string name)
	{
		Monsters monster = DataManager.Instance.connection.Table<Monsters> ().Where (x => x.Name == name).FirstOrDefault ();
		return monster;
	}
	//@monster: ten monster
	//@type: kieu sung gi
	public float GetHeadMonster (string monster, string type)
	{
		Monsters _monster = DataManager.Instance.connection.Table<Monsters> ().Where (x => x.Name == monster).FirstOrDefault ();
		switch (type) {
		case Const.Rifles:
			_head = _monster.Rifles;
			break;
		case Const.AssaultRifles:
			_head = _monster.AssaultRifles;
			break;
		case Const.ShotGun:
			_head = _monster.Shotgun;
			break;
		case Const.CrossBows:
			_head = _monster.CrossBow;
			break;
		case Const.Specialweapon:
			_head = _monster.Specialweapon;
			break;
		}
		return _head;
	}

	public float GetHeadSubtract (string monster, string pos)
	{
		Monsters _monster = DataManager.Instance.connection.Table<Monsters> ().Where (x => x.Name == monster).FirstOrDefault ();
		switch (pos) {
		case Const.Head:
			_subtracthead = _monster.Head;
			break;
		case Const.Body:
			_subtracthead = _monster.Body;
			break;
		case Const.Leg:
			_subtracthead = _monster.Leg;
			break;
		case Const.Tail:
			_subtracthead = _monster.Tail;
			break;
		case Const.Crytal:
			_subtracthead = _monster.Crystal;
			break;
		case Const.Hand:
			_subtracthead = _monster.Hand;
			break;
		case Const.Stomach:
			_subtracthead = _monster.Stomach;
			break;
		case Const.Wing:
			_subtracthead = _monster.Wing;
			break;
		case Const.Mounth:
			_subtracthead = _monster.Mounth;
			break;
		}
		return _subtracthead;
	}
}

