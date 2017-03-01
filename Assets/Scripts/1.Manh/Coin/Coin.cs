using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
	public Text txpath;
	public string path;

	void Start ()
	{
		txpath.text = path;
	}

	void Update ()
	{
		this.transform.Translate (Vector2.up * 0.5f * Time.deltaTime);
	}
}
