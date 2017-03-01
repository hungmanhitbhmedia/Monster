using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
	void Start ()
	{
		float x = Screen.width;
		float y = Screen.height;
		float with = (1536 * x) / y;
		GetComponent<RectTransform> ().sizeDelta = new Vector2 (with, 1536);
	}
}
