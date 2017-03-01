using UnityEngine;
using System.Collections;

public class HandleRotation : MonoBehaviour
{
	
	public float ro_y;
	public bool isGun;
	bool ro;
	Monsters monster;
	Vector3 posmouse;
	Vector3 posbegin;

	void Update ()
	{
		if (!isGun) {
			if (Input.GetMouseButtonDown (0)) {
				posbegin = Camera.main.ScreenToViewportPoint (Input.mousePosition);
			}
			if (Input.GetMouseButton (0)) {
				posmouse = Camera.main.ScreenToViewportPoint (Input.mousePosition);
				ro_y = -400 * (posmouse.x - posbegin.x);
				this.transform.Rotate (new Vector3 (0, -400 * (posmouse.x - posbegin.x), 0));
				posbegin = posmouse;
			}
		} else {
			if (ro) {
				posmouse = Camera.main.ScreenToViewportPoint (Input.mousePosition);
				ro_y = -400 * (posmouse.x - posbegin.x);
				this.transform.Rotate (new Vector3 (0, -400 * (posmouse.x - posbegin.x), 0));
				posbegin = posmouse;
			}
		}
	}

	void OnMouseDown ()
	{
		ro = true;
		posbegin = Camera.main.ScreenToViewportPoint (Input.mousePosition);
	}

	void OnMouseUp ()
	{
		ro = false;
	}
}
