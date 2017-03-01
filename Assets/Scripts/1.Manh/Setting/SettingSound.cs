using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingSound : MonoBehaviour
{
	public Slider sliderSoundBackground;
	public Slider sliderSoundEffect;
	float fsoundbackground;
	float fsoundeffect;


	void OnEnable ()
	{
		if (!PlayerPrefs.HasKey ("SoundBackground")) {
			sliderSoundBackground.value = 1;
			PlayerPrefs.SetFloat ("SoundBackground", 1);
			PlayerPrefs.Save ();
		} else {
			fsoundbackground = PlayerPrefs.GetFloat ("SoundBackground");
			sliderSoundBackground.value = fsoundbackground;
		}
		if (!PlayerPrefs.HasKey ("SoundEffect")) {
			sliderSoundEffect.value = 1;
			PlayerPrefs.SetFloat ("SoundEffect", 1);
			PlayerPrefs.Save ();
		} else {
			fsoundeffect = PlayerPrefs.GetFloat ("SoundEffect");
			sliderSoundEffect.value = fsoundeffect;
		}

	}

	public void SoundBackground ()
	{
		fsoundbackground = sliderSoundBackground.value;
		SoundManager.Instance.SetUpSoundBackground (fsoundbackground);
		PlayerPrefs.SetFloat ("SoundBackground", fsoundbackground);
		PlayerPrefs.Save ();
		//Debug.Log ("SoundBackground:" + fsoundbackground);
	}

	public void SoundEffect ()
	{
		fsoundeffect = sliderSoundEffect.value;
		SoundManager.Instance.SetUpSoundEffect (fsoundeffect);
		PlayerPrefs.SetFloat ("SoundEffect", fsoundeffect);
		PlayerPrefs.Save ();
//		Debug.Log ("SoundEffect:" + fsoundeffect);
	}
}
