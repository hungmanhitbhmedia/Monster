//
// Money.cs
//
// Author:
//       HungManh <>
//
// Copyright (c) 2016 HungManh
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite4Unity3d;


public class Silver
{
	[PrimaryKey, AutoIncrement]
	public int Id{ get; set; }

	public string Name{ get; set; }

	public int Region{ get; set; }

	public string Type{ get; set; }

	public int Level1{ get; set; }

	public int Level2{ get; set; }

	public int Level3{ get; set; }

	public int Level4{ get; set; }

	public int Level5{ get; set; }

	//	private SQLiteConnection connection;
	private float silver;
	//
	//	public Silver ()
	//	{
	//		DataManager dt = new DataManager ("Database.db");
	//		connection = dt.connection;
	//	}


	// path: bộ phận cần nâng cấp
	// level: level nâng cấp (level hiện tại +1)
	// type: nâng cấp bằng silver hay là nâng cấp bằng gold
	public float GetSilver (string path, int region, int level, string type)
	{
		int _region;
		if (region <= 3) {
			_region = 1;
		} else {
			if (region <= 6) {
				_region = 2;
			} else {
				_region = 3;
			}
		}
		Silver sil = DataManager.Instance.connection.Table<Silver> ().Where (x => x.Region == _region && x.Name == path && x.Type == type).FirstOrDefault ();

		switch (level) {
		case 1:
			silver = sil.Level1;
			break;
		case 2:
			silver = sil.Level2;
			break;
		case 3:
			silver = sil.Level3;
			break;
		case 4:
			silver = sil.Level4;
			break;
		case 5:
			silver = sil.Level5;
			break;
		}
		return silver;
	}
}


