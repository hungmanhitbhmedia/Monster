using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : Singleton<MapManager>
{
	public Material sky_trua;
	public Material sky_chieu;
	public Material sky_toi;

	public enum Sky_Runtime
	{
		Trua,
		Chieu,
		Toi}

	;

	public Sky_Runtime sky_runtime;

	//DOCUMENT AI QUÁI
	//1. quái thuộc có type là A: Khi nghe thấy tiếng súng thì chạy đến tấn công
	//=> cái này thì do thằng simpleAI nó điểm nhận ko phải viết thêm gì,
	//2. quái có type là B: Khi nghe thấy tiếng súng thì nó chạy đến một điểm được định sẵn theo một quỹ đạo vẽ sẵn cho từng map
	//=>Cái này sẽ do thằng simpleAIB nó xử lí
	//3. quái được sinh ra tại một điểm rồi chạy đến 1 điểm theo một quỹ đạo nhất định trong map
	//=> Dev sẽ chủ động viết tối đa  4 dạng được đi trên map, theo các quỹ đạo khác nhau




	#region properties  AIC   của những con quái không tấn công

	// Ai 1: Ap dung với những thằng chạy mà ko tấn công,
	// AIC; bắt đầu tại một điểm và chạy đến 1 điểm
	//điểm bắt đầu sinh ra.
	// danh sách quỹ đạo di chuyển
	public List<List<Vector3>> wayC;


	#endregion


	#region properties AIB  của những con quái tấn công

	public	List<List<Vector3>> wayB;

	#endregion

	void Awake ()
	{
		wayC = new List<List<Vector3>> ();
		wayB = new List<List<Vector3>> ();
		iTweenPath[] w;
		w = this.GetComponents<iTweenPath> ();
		for (int i = 0; i < w.Length; i++) {
			if (w [i].pathName == "wayB" || w [i].pathName == "wayB1" || w [i].pathName == "wayB2" || w [i].pathName == "wayB3" || w [i].pathName == "wayB4") {
				wayB.Add (w [i].nodes);
			} else {
				wayC.Add (w [i].nodes);
			}
		}
//		RenderSettings.s
		switch (sky_runtime) {
		case Sky_Runtime.Trua:
			RenderSettings.skybox = sky_trua;
			break;
		case Sky_Runtime.Chieu:
			RenderSettings.skybox = sky_chieu;
			break;
		case Sky_Runtime.Toi:
			RenderSettings.skybox = sky_toi;
			break;
		default:
			return;
		}
//		Debug.Log (way [1] [1].x);
	}

	void Start ()
	{
		if (wayB.Count > 0) {
			for (int i = 0; i < wayB.Count; i++) {
				for (int j = 1; j < wayB [i].Count; j++) {
					GameObject pointWay = Instantiate (Resources.Load ("PointWay"))as GameObject;
					pointWay.transform.position = wayB [i] [j];
					pointWay.transform.name = "WayB" + i;
				}
			}
		}
		if (wayC.Count > 0) {
			for (int i = 0; i < wayC.Count; i++) {
				for (int j = 1; j < wayC [i].Count; j++) {
					GameObject pointWay = Instantiate (Resources.Load ("PointWay"))as GameObject;
					pointWay.transform.position = wayC [i] [j];
					pointWay.transform.name = "WayC" + i;
				}
			}
		}
	}
}
