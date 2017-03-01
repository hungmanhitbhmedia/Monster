using UnityEngine;
using System.Collections;

public class SwicthPlatform : MonoBehaviour
{
	public static string file_path;

	void Awake ()
	{
		SwichPlasform ();
	}

	public void SwichPlasform ()
	{
		#if UNITY_EDITOR
		file_path = Application.persistentDataPath + "/";
		#elif UNITY_ANDROID
		file_path = Application.persistentDataPath;
		#elif UNITY_IPHONE
		file_path = Application.persistentDataPath+"/";
		#else
		file_path = Application.persistentDataPath;
		#endif
	}
}
