using UnityEngine;
using System.Collections;
using System.IO;

public class LoadMaterialMap : MonoBehaviour
{
	#region -moi truong

	#region -region1

	public Material c8;
	public Material c9;
	public Material c10;
	public Material c11;
	public Material laudai1;
	public Material oilcan_d1;
	public Material oilcan_d2;
	public Material oilcan_d3;
	public Material oilcan_d4;
	public Material thung_phuy2;
	public Material xe_day_go;

	#endregion

	#region -region2

	public Material banh_xe_d;
	public Material box;
	public Material cay_do;
	public Material cay_do1;
	public Material cay6;
	public Material dau_xe_tai;


	#endregion

	#region -region3

	public Material binh_nhien_lieu;
	public Material c16;
	public Material nha_may;
	public Material tree3_rg3;

	#endregion

	#region-region4

	public Material c9_rg3;
	public Material house;
	public Material snowpine_detail;
	public Material snowpine_leaf;

	#endregion

	#endregion

	#region -Cay

	#endregion

	public int region;
	string file_path;

	void Awake ()
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
		Caching.CleanCache ();
		MaterialCay ();
		MaterialMoitruong ();

	}

	void Start ()
	{
//		Debug.Log ("material dhslgdhlgdslglkkkkkkk");
		Caching.CleanCache (); 
	}

	void MaterialCay ()
	{
//		string path = file_path + "Deer_Hunter/Cay/";
//		LoadLocalMaterial (autumn_tree1, path + "autumn_tree_2_by_plmnpeev-d7aa9n7.png");
//		LoadLocalMaterial (autumn_tree2, path + "autumn_tree_2_by_plmnpeev-d7aa9n7.png");
//		LoadLocalMaterial (autumn_tree_by1, path + "autumn_tree_by_cimarron29-d5rmehp.png");
//		LoadLocalMaterial (autumn_tree_by2, path + "autumn_tree_by_cimarron29-d5rmehp.png");
//		LoadLocalMaterial (cay2_c, path + "cay2.png");
//		LoadLocalMaterial (cay3_c, path + "cay3.png");
//		LoadLocalMaterial (cay5_c, path + "cay5.png");
//		LoadLocalMaterial (cay6_c, path + "cay2.png");
//		LoadLocalMaterial (cay7_c, path + "cay2.png");
//		LoadLocalMaterial (cay22, path + "cay2.png");
//		LoadLocalMaterial (creeby_tree, path + "creepy_tree_18_by_wolverine041269-d621vcx.png");
//		LoadLocalMaterial (tree, path + "tree.png");
//		LoadLocalMaterial (tree_3_png1, path + "tree-3-png-with-transparency-by-bupaje-on-deviantart-31.png");
//		LoadLocalMaterial (tree_3_png2, path + "tree-3-png-with-transparency-by-bupaje-on-deviantart-31.png");
//		LoadLocalMaterial (tree_1582, path + "tree-1585208_1280.png");
//		LoadLocalMaterial (tree_tree_png_i, path + "Tree-Free-PNG-Image.png");
//		LoadLocalMaterial (tree_tree_png_p, path + "Tree-PNG-Picture.png");
//		LoadLocalMaterial (tree_png230, path + "tree_PNG230.png");
//		LoadLocalMaterial (tree_png3472, path + "tree_PNG3472.png");
//		LoadLocalMaterial (tree_png_stock1, path + "tree_png_stock_by_lubman-d70r60o.png");
//		LoadLocalMaterial (tree_png_stock2, path + "tree_png_stock_by_lubman-d70r60o.png");
//		LoadLocalMaterial (tree_08, path + "trees_08_png_stock_by_jumpfer_stock-d6yaw6v.png");
//		LoadLocalMaterial (tree_15, path + "trees_15_png_stock_by_jumpfer_stock-d6yawbz.png");
//		LoadLocalMaterial (tree_16, path + "trees_16_png_stock_by_jumpfer_stock-d6yawcs.png");
//		LoadLocalMaterial (wicked_halloween, path + "wicked-halloween-tree-clipart-click-the-image-to-view-and-download-ooZ6fP-clipart.png");
//		LoadLocalMaterial (wicked_halloween2, path + "wicked-halloween-tree-clipart-click-the-image-to-view-and-download-ooZ6fP-clipart.png");
	}

	void MaterialMoitruong ()
	{
		string path = file_path + "Deer_Hunter/Moitruong/Region";
		switch (region) {
		case 1:
			LoadLocalMaterial (c8, path + "1/" + "c8.png");
			LoadLocalMaterial (c9, path + "1/" + "c9.png");
			LoadLocalMaterial (c10, path + "1/" + "c10.png");
			LoadLocalMaterial (c11, path + "1/" + "c9.png");
			LoadLocalMaterial (laudai1, path + "1/" + "lau dai1.png");
			LoadLocalMaterial (oilcan_d1, path + "1/" + "Oilcan_D1.png");
			LoadLocalMaterial (oilcan_d2, path + "1/" + "Oilcan_D2.png");
			LoadLocalMaterial (oilcan_d3, path + "1/" + "Oilcan_D3.png");
			LoadLocalMaterial (oilcan_d4, path + "1/" + "Oilcan_D4.png");
			LoadLocalMaterial (thung_phuy2, path + "1/" + "thung phuy2.png");
			LoadLocalMaterial (xe_day_go, path + "1/" + "xe day go.png");
			break;
		case 2:
			LoadLocalMaterial (banh_xe_d, path + "2/" + "banh xe_D.png");
			LoadLocalMaterial (box, path + "2/" + "BOX.jpg");
			LoadLocalMaterial (cay_do, path + "2/" + "cay do.png");
			LoadLocalMaterial (cay_do1, path + "2/" + "cay do.png");
			LoadLocalMaterial (cay6, path + "2/" + "cay6.png");
			LoadLocalMaterial (dau_xe_tai, path + "2/" + "dau xe tai_D.png");
			break;
		case 3:
			LoadLocalMaterial (binh_nhien_lieu, path + "3/" + "binh nhien lieu.jpg");
			LoadLocalMaterial (c16, path + "3/" + "c16.png");
			LoadLocalMaterial (nha_may, path + "3/" + "nha may.jpg");
			LoadLocalMaterial (tree3_rg3, path + "3/" + "tree_3__png_with_transparency__by_bupaje-d65gvf3.png");
			break;
		case 4:
			LoadLocalMaterial (c9_rg3, path + "4/" + "c9.png");
			LoadLocalMaterial (house, path + "4/" + "house.jpg");
			LoadLocalMaterial (snowpine_detail, path + "4/" + "snowpine_leaf.png");
			LoadLocalMaterial (snowpine_leaf, path + "4/" + "snowpine_leaf.png");
			break;
		}
	}

	public void LoadLocalMaterial (Material material, string filepath)
	{
		if (File.Exists (filepath)) {
			var bytes = File.ReadAllBytes (filepath);
			Texture2D texture = new Texture2D (1, 1);
			texture.LoadImage (bytes);
			material.mainTexture = texture;
		} else {
			PlayerPrefs.SetInt ("DownloadResourceComplete", 0);
			PlayerPrefs.Save ();
//			Debug.Log (filepath);
			return;
		}
	}
}
