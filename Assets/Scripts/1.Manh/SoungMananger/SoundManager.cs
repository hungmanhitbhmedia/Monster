using UnityEngine;
using System.Collections;

public class SoundManager : Singleton<SoundManager>
{
	public AudioSource au_menu;
	public AudioSource[] au_background;
	public AudioSource[] button;

	public AudioSource au_bansungtiatungvien;
	public AudioSource au_lendansungtiatungvien;


	public AudioSource au_thaydanshotgun;
	public AudioSource au_bansungshotgun;

	public AudioSource au_thaydansungtruong;

	public AudioSource au_bansung6long;
	public AudioSource au_thaydansung6long;

	public AudioSource au_banRPG;
	public AudioSource au_effectnoRPG;

	public AudioSource au_bear;
	public AudioSource au_camel;
	public AudioSource au_fox;
	public AudioSource au_husky;
	public AudioSource au_leopard;
	public AudioSource au_ostrish;
	public AudioSource au_tiger;
	public AudioSource au_willboar;
	public AudioSource au_special1;
	public AudioSource au_special2;
	public AudioSource au_special3;
	public AudioSource au_special4;
	public AudioSource au_special5;




	float fsoundbackground;
	float fsoundeffect;

	void Start ()
	{
		if (!PlayerPrefs.HasKey ("SoundBackground")) {
			PlayerPrefs.SetFloat ("SoundBackground", 1);
			PlayerPrefs.SetFloat ("SoundEffect", 1);
			PlayerPrefs.Save ();
		}
		fsoundbackground = PlayerPrefs.GetFloat ("SoundBackground");
		fsoundeffect = PlayerPrefs.GetFloat ("SoundEffect");
		SetUpSoundBackground (fsoundbackground);
		SetUpSoundEffect (fsoundeffect);
		if (Application.loadedLevelName == "GamePlay") {
			Randomsoundbackground ();
		}
	}
	// random sound background;
	public void Randomsoundbackground ()
	{
		int index = UnityEngine.Random.Range (0, 3);
		au_background [index].Play ();
	}
	// Khi click vao button trong game;
	public void PlayButton (int index)
	{
		button [index].Play ();
	}

	public void BackgrounInGame (int index)
	{
		au_background [index].Play ();
	}

	public void SetUpSoundBackground (float value)
	{
		if (Application.loadedLevelName == "Menu") {
			au_menu.volume = value;
		}
		for (int i = 0; i < au_background.Length; i++) {
			au_background [i].volume = value;
		}
		for (int i = 0; i < this.transform.GetChild (3).childCount; i++) {
			this.transform.GetChild (3).GetChild (i).GetComponent<AudioSource> ().volume = value;
		}
		for (int i = 0; i < this.transform.GetChild (4).childCount; i++) {
			this.transform.GetChild (4).GetChild (i).GetComponent<AudioSource> ().volume = value;
		}
	}

	public void SetUpSoundEffect (float value)
	{
		for (int i = 0; i < button.Length; i++) {
			button [i].volume = value;
		}
	}

	public void BanSungTiaTungVien ()
	{
		au_bansungtiatungvien.Play ();
	}

	public void LenDanSungTiaTungVien ()
	{
		if (GameEnd.Instance.isEnd) {
			return;
		}
		au_lendansungtiatungvien.Play ();
	}

	public void ThayDanShotGun ()
	{
		au_thaydanshotgun.Play ();
	}

	public void BanSungShotGun ()
	{
		au_bansungshotgun.Play ();
	}

	public void ThayDanSungTruong ()
	{
		au_thaydansungtruong.Play ();
	}

	public void BanSung6long ()
	{
		au_bansung6long.Play ();
	}

	public void StopSung6long ()
	{
		au_bansung6long.Stop ();
	}

	public void ThayDanSung6long ()
	{
		au_thaydansung6long.Play ();
	}

	public void BanRPG ()
	{
		au_banRPG.Play ();
	}

	public void EffectNoDanRPG ()
	{
		au_effectnoRPG.Play ();
	}


	public void Bear ()
	{
		au_bear.Play ();
	}

	public void Camel ()
	{
		au_camel.Play ();
	}

	public void Fox ()
	{
		au_fox.Play ();
	}

	public void Husky ()
	{
		au_husky.Play ();
	}

	public void Leopard ()
	{
		au_leopard.Play ();
	}

	public void Ostrich ()
	{
		au_ostrish.Play ();
	}

	public void Tiger ()
	{
		au_tiger.Play ();
	}

	public void Willboar ()
	{
		au_willboar.Play ();
	}

	public void Special1 ()
	{
//		au_s
		au_special1.Play ();
	}

	public void Special2 ()
	{
		au_special2.Play ();
	}

	public void Special3 ()
	{
		au_special3.Play ();
	}

	public void Special4 ()
	{
		au_special4.Play ();
	}

	public void Special5 ()
	{
		au_special5.Play ();
	}

}
