using UnityEngine;
using System.Collections;

public class RegionCurrent : MonoBehaviour
{

	public int region;

	public void SetRegion ()
	{
		PlayerPrefs.SetInt ("RegionCurrent", region);
		PlayerPrefs.Save ();
	}
}
