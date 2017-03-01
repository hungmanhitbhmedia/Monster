using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Zoom : Singleton<Zoom>
{
	public GameObject gZoom;
	public GameObject gZoommin;
	public Text txZoom;
	public float MouseYMax;
	public float MouseYMin;
	float ZoomStart;
	float zoomMax;
	float zoomMin;
	Vector3 mouse;
	string gun;
	//fdgdg
	RegionInGame regioningame;
	GunInGame guningame;
	//GunInGame guningame;
	UpdateGun updategun;
	Rifles rifles;

	void Awake ()
	{
		regioningame = new RegionInGame ();
		guningame = new GunInGame ();
		// guningame = new GunInGame();
		updategun = new UpdateGun ();
		rifles = new Rifles ();
		gun = regioningame.GetRegionInGame ().Gun;

		MouseYMax = this.GetComponent<RectTransform> ().position.y;
		MouseYMin = gZoommin.GetComponent<RectTransform> ().position.y;
		float maxzomdefault = rifles.GetRifles (gun).Maxzoom;
		zoomMin = rifles.GetRifles (gun).Maxzoom;
		int indexMax = guningame.GetGunInGame (regioningame.GetRegionInGame ().Gun).Maxzoom;
		zoomMax = (maxzomdefault + updategun.GetDetail (Const.Maxzoom, rifles.GetRifles (gun).Types) * indexMax);
		ZoomStart = 60 - zoomMin;
	}

	public void Buttom ()
	{
		MoveScreen.Instance.SetZoom (true);
	}

	void Update ()
	{
		//        if (Input.touchCount == 1)
		//        {
		if (MoveScreen.Instance.checkZoom) {
			mouse = Input.mousePosition;
			if (mouse.y <= MouseYMax && mouse.y >= MouseYMin) {
				float alpha = (MouseYMax - MouseYMin) / 90;
				float z = 45 - (MouseYMax - mouse.y) / alpha;
				gZoom.transform.eulerAngles = new Vector3 (0, 0, z);

				float bta = (MouseYMax - MouseYMin) / (zoomMax - zoomMin);
				float _zoom = ZoomStart - (MouseYMax - mouse.y) / bta;
				Camera.main.fieldOfView = _zoom;

				float tzoom = zoomMin + (MouseYMax - mouse.y) / bta;
				txZoom.text = tzoom + "x";
				//                Debug.Log(_zoom);
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			MoveScreen.Instance.SetZoom (false);
		}
		//        }
	}

	float fieldView;

	public void NoZoom ()
	{
		Camera.main.fieldOfView = 60 - zoomMin;
		gZoom.transform.eulerAngles = new Vector3 (0, 0, 45);
		txZoom.text = zoomMin + "x";
	}

	public void ResetZoom ()
	{
		Camera.main.fieldOfView = 60;
	}
}
