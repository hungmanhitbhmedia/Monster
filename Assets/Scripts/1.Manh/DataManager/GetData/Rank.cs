using System.Collections;
using SQLite4Unity3d;

public class Rank
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }

	public int Capdo{ get; set; }

	public string Name{ get; set; }

	public int Level{ get; set; }

	public int Exp{ get; set; }

	public int Gold{ get; set; }
}
