using UnityEngine;
using System.Collections;

public class GunAnimation : Singleton<GunAnimation>
{
	public GameObject flash;
	public string thaydan;
	public string lendan;
	public string rutsung;
	public string dualenngam;
	public string bankhongngam;
	public string haongngam;
	public string tho;
	public string thadan1;
	public string animationDuasunglen;

	public Animation ani;


	public Vector3 pos;
	public Vector3 posend;
	public Vector3 ros;


	void Start ()
	{
		ani = this.transform.GetChild (0).GetComponent<Animation> ();
		Invoke ("Tho", 1.2f);

	}

	public void Thaydan ()
	{
		ani = this.transform.GetChild (0).GetComponent<Animation> ();
		ani.Play (thaydan);
	}

	public void Lendan ()
	{
		ani = this.transform.GetChild (0).GetComponent<Animation> ();
		ani.Play (lendan);
	}

	public void Rutsung ()
	{
		ani = this.transform.GetChild (0).GetComponent<Animation> ();
		ani.Play (rutsung);

	}

	public void Hasungxuong ()
	{
		ani = this.transform.GetChild (0).GetComponent<Animation> ();
//		ShotGun.Instance.btreload.SetActive (true);
		Debug.Log ("Ha sung xuong");
		ani.Play (tho);
		this.GetComponent<Animation> ().Play ("Ha");
	}

	public void Dualenngam ()
	{
		//ShotGun.Instance.tamnho.SetActive (false);
		this.GetComponent<Animation> ().Play (animationDuasunglen);

	}

	public void Bankhongngam ()
	{
		ani = this.transform.GetChild (0).GetComponent<Animation> ();
		ani.Play (bankhongngam);
		Invoke ("Tho1", (ani [bankhongngam].length + 2.5f));
	}

	void Tho1 ()
	{
		ani = this.transform.GetChild (0).GetComponent<Animation> ();
		ani.Play (tho);
	}

	public void Banngam ()
	{
		
	}

	public void Haongngam ()
	{
		ani = this.transform.GetChild (0).GetComponent<Animation> ();
		ani.Play (haongngam);
	}

	public void Tho ()
	{
		ani = this.transform.GetChild (0).GetComponent<Animation> ();
		ani.Play (tho);
		int tmpr = DataManager.Instance.connection.Table<Rifles> ().Where (x => x.Name == this.name).FirstOrDefault ().TamSung;
		if (tmpr == 0) {
			return;
		} 
		Dualenngam ();
		Invoke ("Chuyen", .5f);
	}

	void Chuyen ()
	{
		Hasungxuong ();
		this.transform.localPosition = pos;
	}

	public void Chuyensangtam ()
	{
		ShotGun.Instance.Chuyensangtam ();
		
	}

	public void StopAnition ()
	{
		ani = this.transform.GetChild (0).GetComponent<Animation> ();
		ani.Stop ();
	}
}
