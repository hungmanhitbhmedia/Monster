using UnityEngine;
using System.Collections;

public class SoundMonter : Singleton<SoundMonter>
{
	public enum EMonster
	{
		Bear,
		Camel,
		Fox,
		Husky,
		Leopar,
		Ostrick,
		Tiger,
		Wilboar,
		Special1,
		Special2,
		Special3,
		Special4,
		Special5}
	;

	public EMonster eMonter;

	public void SetSound ()
	{
		switch (eMonter) {
		case EMonster.Bear:
			SoundManager.Instance.Bear ();
			break;
		case EMonster.Camel:
			SoundManager.Instance.Camel ();
			break;
		case EMonster.Fox:
			SoundManager.Instance.Fox ();
			break;
		case EMonster.Husky:
			SoundManager.Instance.Husky ();
			break;
		case EMonster.Leopar:
			SoundManager.Instance.Leopard ();
			break;
		case EMonster.Tiger:
			SoundManager.Instance.Tiger ();
			break;
		case EMonster.Ostrick:
			SoundManager.Instance.Ostrich ();
			break;
		case EMonster.Wilboar:
			SoundManager.Instance.Willboar ();
			break;
		case EMonster.Special1:
			SoundManager.Instance.Special1 ();
			break;
		case EMonster.Special2:
			SoundManager.Instance.Special2 ();
			break;
		case EMonster.Special3:
			SoundManager.Instance.Special3 ();
			break;
		case EMonster.Special4:
			SoundManager.Instance.Special4 ();
			break;
		case EMonster.Special5:
			SoundManager.Instance.Special5 ();
			break;
		}
	}
}
