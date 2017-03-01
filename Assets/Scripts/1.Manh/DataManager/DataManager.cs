using SQLite4Unity3d;
using UnityEngine;

#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataManager
{
	private static DataManager instance;

	private DataManager ()
	{
	}

	public static DataManager Instance {
		get {
			if (instance == null) {
				instance = new DataManager ("Database.db");
			}
			return instance;
		}
	}

	public SQLiteConnection connection;
	private string db_Base_Path;

	public DataManager (string DatabaseName)
	{
		string tmp_folder_path = "";
		#if UNITY_EDITOR
		tmp_folder_path = Application.persistentDataPath + "/";
		#elif UNITY_ANDROID
		tmp_folder_path = Application.persistentDataPath;
		#elif UNITY_IPHONE
		tmp_folder_path = Application.persistentDataPath+"/";
		#else
		tmp_folder_path = Application.persistentDataPath;
		#endif
		tmp_folder_path = tmp_folder_path + "Database.db";
//#if UNITY_EDITOR
//		var dbPath = string.Format (@"Assets/StreamingAssets/{0}", "Database.db");
//#else
//        // check if file exists in Application.persistentDataPath
//        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
//
//        if (!File.Exists(filepath))
//        {
//            Debug.Log("Database not in Persistent path");
//            // if it doesn't ->
//            // open StreamingAssets directory and load the db ->
//
//#if UNITY_ANDROID 
//            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
//            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
//            // then save to Application.persistentDataPath
//            File.WriteAllBytes(filepath, loadDb.bytes);
//#elif UNITY_IOS
//                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
//                // then save to Application.persistentDataPath
//                File.Copy(loadDb, filepath);
//#elif UNITY_WP8
//                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
//                // then save to Application.persistentDataPath
//                File.Copy(loadDb, filepath);
//
//#elif UNITY_WINRT
//		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
//		// then save to Application.persistentDataPath
//		File.Copy(loadDb, filepath);
//#else
//	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
//	// then save to Application.persistentDataPath
//	File.Copy(loadDb, filepath);
//
//#endif
//
//            Debug.Log("Database written");
//        }
//
//        var dbPath = filepath;
//#endif
////		Debug.Log (dbPath.ToString ());
//		connection = new SQLiteConnection (dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

		connection = new SQLiteConnection (tmp_folder_path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

	}
}
