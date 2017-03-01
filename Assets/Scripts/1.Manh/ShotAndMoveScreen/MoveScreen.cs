using UnityEngine;
using System.Collections;

public class MoveScreen : Singleton<MoveScreen>
{
	float[] arrmami = { -0.007f, -0.004f, 0.001f, 0.007f, 0.004f };
	Vector3 postouchbegin;
	Vector3 postouchmove;
	//Vector3 posmaincameracurrent;
	Vector3 move;
	public GameObject mainCamera;
	bool ismove;

	float speed;
	float slowspeed;

	float x;
	float y;
	float a = 0;
	float b = 0.01f;

	public bool checkZoom;



	Vector3 ros;

	void Start ()
	{
		mainCamera = Camera.main.gameObject;
		InvokeRepeating ("Move", 0, 5);
	}

	void Update ()
	{
		if (GameEnd.Instance.isEnd) {
			return;
		}
		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown (0)) {
			postouchbegin = Camera.main.ScreenToViewportPoint (Input.mousePosition);
			//posmaincameracurrent = mainCamera.transform.position;
		}
		if (Input.GetMouseButton (0)) {
			postouchmove = Camera.main.ScreenToViewportPoint (Input.mousePosition);
			move = postouchmove - postouchbegin;
			Vector3 ro = mainCamera.transform.eulerAngles;
			a = ro.y + move.x * 50;
			a = Mathf.Clamp (a, 1, 200);
			mainCamera.transform.eulerAngles = new Vector3 (ro.x - move.y * 50, a, 0);
			postouchbegin = Camera.main.ScreenToViewportPoint (Input.mousePosition);
			iTween.Stop (Camera.main.gameObject);
		}
		#else
	        if (Input.touchCount == 1 && !checkZoom)
	        {
	
	        if(!ismove)
	        {
//		iTween.Stop (Camera.main.gameObject);
	        postouchbegin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
	        ismove = true;
	        }
	            if (Input.GetMouseButton(0))
	            {
	                postouchmove = Camera.main.ScreenToViewportPoint(Input.mousePosition);
	                move = postouchmove - postouchbegin;
	                Vector3 ro = mainCamera.transform.eulerAngles;
					a = ro.y + move.x * 150;
					a = Mathf.Clamp(a,1,200);
	                mainCamera.transform.eulerAngles = new Vector3(ro.x - move.y * 150, a, 0);
	                postouchbegin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
	            }
	        }
	        else{
	        ismove = false;
	        }
	
		#endif
	
		ros = mainCamera.transform.eulerAngles;
	
		mainCamera.transform.eulerAngles = new Vector3 (ros.x - y, ros.y + x, 0);
	
		//move slow motion
		slowspeed = mainCamera.transform.position.z + speed;
		slowspeed = Mathf.Clamp (slowspeed, -6, 4);
		mainCamera.transform.position = new Vector3 (0, 0, slowspeed);
	}

	void Move ()
	{
		if (ros.y >= 1 && ros.y <= 200) {
			x = arrmami [UnityEngine.Random.Range (0, 5)];
			y = arrmami [UnityEngine.Random.Range (0, 5)];
		}
	}

	public void SetZoom (bool iszoom)
	{
		checkZoom = iszoom;
	}

	public void Motion (string motion)
	{
		switch (motion) {
		case "Right":
			speed = 0.05f;
			break;
		case  "Left":
			speed = -0.05f;
			break;
		}
	}

	public void StopMotion ()
	{
		speed = 0;
	}
}