using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	#region Audio variables
	public AudioClip[] pistolSound;
	private AudioSource audio;
	#endregion

	#region GameObject variables
	public GameObject[] bulletHole;
	public GameObject bloodParticles;
	public GameObject fireParticles;
	public GameObject shellPosition;
	public GameObject shellObject;
	#endregion

	#region Float variables
	public float range = 10.0f;
	private float timer = 0;
	public float dispersionWeapon = 0.2f;
	public float damage = 5.0f;
	public float height = 10f;
	public float width = 2f;
	public float defaultSpread = 10f;
	public float resizedSpread = 20f;
	public float resizeSpeed = 3f;
	private float spread;
	#endregion

	#region Int variables
	public int gunMagazine = 0;
	public int maxAmmo = 20;
	public int currentAmo = 20;
	#endregion

	#region Bool variables
	public bool isSniper = false;
	public bool holdFireButton = false;
	public bool isAvailable = false;
	private bool resizing = false;
	private bool isReloading = false;
	#endregion

	#region Double variables
	[Range(0f, 2f)]
	public double defaultSpeedShot = 0.4f;
	[Range(0f, 1f)]
	public double holdSpeedShot = 0.2f;
	#endregion

	#region Animation variables
	private Animation animation;
	#endregion

	#region RaycastHit variables
	private RaycastHit hit;
	#endregion

	#region GunAmmoUI variables
	public GunAmmoUI gunAmmoUI;
	#endregion

	#region GunAmmoUI variables
	public Color color = Color.grey;
	#endregion

	void Awake(){
		spread = defaultSpread;
	}


	void CrossHair(){
		if (Input.GetButtonDown ("Fire1") && timer > defaultSpeedShot ) 
			resizing = true;
		if (!Input.GetButton ("Fire1"))
			resizing = false;

		if (resizing){
			spread = Mathf.Lerp(spread, resizedSpread, resizeSpeed * Time.deltaTime);
		}
		else{
			spread = Mathf.Lerp(spread, defaultSpread, resizeSpeed * Time.deltaTime);
		}
		spread = Mathf.Clamp(spread, defaultSpread, resizedSpread);
	}


	void OnGUI(){
		if (!isSniper || Camera.main.fieldOfView == 10) {
			Texture2D texture = new Texture2D (1, 1);
			texture.SetPixel (0, 0, color);
			texture.wrapMode = TextureWrapMode.Repeat;
			texture.Apply ();
			GUI.DrawTexture (new Rect (Screen.width / 2 - width / 2, (Screen.height / 2 - height / 2) + spread / 2, width, height), texture);
			GUI.DrawTexture (new Rect (Screen.width / 2 - width / 2, (Screen.height / 2 - height / 2) - spread / 2, width, height), texture);
			GUI.DrawTexture (new Rect ((Screen.width / 2 - height / 2) + spread / 2, Screen.height / 2 - width / 2, height, width), texture);
			GUI.DrawTexture (new Rect ((Screen.width / 2 - height / 2) - spread / 2, Screen.height / 2 - width / 2, height, width), texture);
		}
	}

	public int GetGunMagazine(){
		return gunMagazine;
	}

	public int GetCurrentAmo(){
		return currentAmo;
	}

	public int GetMaxAmo(){
		return maxAmmo;
	}

	public void UpdateUIMagazine(){
		if(gameObject.activeInHierarchy == true)
			gunAmmoUI.UpdateAmmoUI (GetCurrentAmo (), GetGunMagazine () * GetMaxAmo());
	}

	void OnEnable() {
		animation = GetComponent<Animation> ();
		audio = GameObject.FindGameObjectWithTag ("Weapons").GetComponent<AudioSource> ();
	}

	public void ReadyWeaponPlay(){
		audio.clip = pistolSound[0];
		audio.Play ();
		animation.Play("Ready");
	}

	public void ReloadWeaponPlay(){
		audio.clip = pistolSound[1];
		audio.Play ();
		animation.Play("Reload");
	}

	public void FireWeaponPlay(){
		audio.clip = pistolSound[2];
		audio.Play ();
		animation.Stop ();
		animation.Play ("Fire");
	}

	public void NoAmmoWeaponPlay(){
		audio.clip = pistolSound[3];
		audio.Play ();
	}

	public void AddWeaponMagazine(){
		gunMagazine++;
	}

	IEnumerator FireFlash()
	{
		fireParticles.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		fireParticles.SetActive(false);
	}

	IEnumerator PlaySoundAfterTenSeconds(AudioSource audioShell)
	{
		yield return new WaitForSeconds(0.5f);
		audioShell.Play();
	}

	private void Shot(){
		Vector3 t = Camera.main.transform.position;
		Vector2 bulletOffset = Random.insideUnitCircle * dispersionWeapon;
		Vector3 pos = new Vector3 (t.x + bulletOffset.x,  t.y + bulletOffset.y,t.z );
		if (Physics.Raycast (pos, (Camera.main.transform.TransformDirection(Vector3.forward)), out hit)) {
			if (hit.transform.tag == "Zombie" && hit.distance < range || hit.transform.tag == "Frend") {
				Vector3 v3 = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
				GameObject goo = Instantiate (bloodParticles, v3, Quaternion.FromToRotation (Vector3.up, hit.normal)) as GameObject; 
				Destroy (goo, 0.2f);
				hit.transform.gameObject.SendMessage("TakeHit", damage);	
			} else if (hit.distance < range) {
				if (hit.transform.tag != "Helicopter" && hit.transform.tag != "Water" && hit.transform.tag != "Frend" && hit.transform.tag != "Player") {
					if (hit.transform.tag != "WaterCollider") {
						GameObject go = Instantiate (bulletHole [Random.Range (0, bulletHole.Length - 1)], hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal)) as GameObject; 
						Destroy (go, 5);
					}
				} 
			}
		}

		GameObject shell = Instantiate (shellObject, shellPosition.transform.position, transform.rotation) as GameObject; 
		StartCoroutine(PlaySoundAfterTenSeconds(shell.GetComponent<AudioSource> ()));
		Destroy (shell, 10);
		StartCoroutine(FireFlash());
		FireWeaponPlay ();
		currentAmo--;
		UpdateUIMagazine ();
		timer = 0;
	}

	private void PressFire(){
		if (!isReloading) {
			if (currentAmo > 0) {
				if (Input.GetButtonDown ("Fire1")) {
					if (timer > defaultSpeedShot) {
						Shot ();
					}
				}else if (Input.GetButton ("Fire1")) {
					if (holdFireButton) { 
						if (timer == 0) {
							Shot ();
						} else if (timer > holdSpeedShot) {
							Shot ();
							timer = 0;
						}
					}
				}
			} else {
				if (Input.GetButtonDown ("Fire1") && gunMagazine == 0) {
					NoAmmoWeaponPlay ();
				}
			}
		}
	}

	private void ZoomSniper(){
		if (isSniper && !isReloading) {
			if (Input.GetButtonDown ("Fire2"))
				Camera.main.fieldOfView = 10;
			else if (Input.GetButtonUp ("Fire2"))
				Camera.main.fieldOfView = 60;
		}
	}

	private void Reload(){
		if(Input.GetButtonDown("Fire1") && currentAmo == 0 && timer > 0.2f && gunMagazine > 0) {
			ReloadWeaponPlay ();
			isReloading = true;
			timer = 0;
		}

		if(Input.GetKeyDown(KeyCode.R) && timer > 0.2f && gunMagazine > 0) {
			if (currentAmo < maxAmmo) {
				ReloadWeaponPlay ();
				isReloading = true;
				timer = 0;
			}
		}

		if(isReloading && timer > 2.0f) {
			currentAmo = maxAmmo;
			if (gunMagazine > 0)
				gunMagazine--;
			isReloading = false;
			UpdateUIMagazine ();
		}
	}


	void Update () {
		if(Time.timeScale == 0) return; 
		timer += Time.deltaTime;
		CrossHair ();
		PressFire ();
		Reload ();
		ZoomSniper ();

		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward) * 10;
		Debug.DrawRay(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z), forward, Color.green);
	}
}
