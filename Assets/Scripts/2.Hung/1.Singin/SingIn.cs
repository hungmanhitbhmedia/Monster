using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SingIn : MonoBehaviour
{
	void Start ()
	{
//		PlayerPrefs.DeleteAll ();
	}

	public void SignInGame ()
	{
		SceneManager.LoadScene ("Menu");
	}
}
