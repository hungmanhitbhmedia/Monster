using UnityEngine;
using System.Collections;
using System.IO;

public class MaterialHelper : MonoBehaviour
{
	public enum StateMaterial
	{
		Sung,
		Cay,
		MT1,
		MT2,
		MT3,
		MT4,
		Q1,
		Q2,
		Q3,
		Q4}

	;

	public StateMaterial statematerial;
	public Material material;
	public string filename;

	void Awake ()
	{
		SwichPlasform ();
	}

	string file_path;

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

	void Start ()
	{
		string stm = "";
		string stt = "";
		switch (statematerial) {
		case StateMaterial.Sung:
			stm = "Sung";
			break;
		case StateMaterial.Cay:
			stm = "Cay";
			break;
		case StateMaterial.MT1:
			stm = "Moitruong/Region1";
			break;
		case StateMaterial.MT2:
			stm = "Moitruong/Region2";
			break;
		case StateMaterial.MT3:
			stm = "Moitruong/Region3";
			break;
		case StateMaterial.MT4:
			stm = "Moitruong/Region4";
			break;
		case StateMaterial.Q1:
			stm = "Quai/Region1";
			break;
		case StateMaterial.Q2:
			stm = "Quai/Region2";
			break;
		case StateMaterial.Q3:
			stm = "Quai/Region3";
			break;
		case StateMaterial.Q4:
			stm = "Quai/Region4";
			break;
		}
		string str = file_path + "Deer_Hunter/" + stm + "/" + filename + ".jpg";
		string str1 = file_path + "Deer_Hunter/" + stm + "/" + filename + ".png";
		string str2 = file_path + "Deer_Hunter/" + stm + "/" + filename + ".tga";
		if (File.Exists (str)) {
			LoadLocalMaterial (material, str);
		}
		if (File.Exists (str1)) {
			LoadLocalMaterial (material, str1);
		}
		if (File.Exists (str2)) {
			LoadLocalMaterial (material, str2);
		}
	}

	public void LoadLocalMaterial (Material _material, string filepath)
	{
		if (File.Exists (filepath)) {
			var bytes = File.ReadAllBytes (filepath);
			Texture2D texture = new Texture2D (1, 1);
			texture.LoadImage (bytes);
			_material.mainTexture = texture;
		} else {
//			Debug.Log (filepath);
			PlayerPrefs.SetInt ("DownloadResourceComplete", 0);
			PlayerPrefs.Save ();
			return;
		}
	}
}
