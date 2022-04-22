using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsManager : MonoBehaviour {

	#region Int variables
		private int weaponNumber;
	#endregion

	#region GunAmmoUI variables
		private GunAmmoUI gunAmmoUI;
	#endregion

	#region Float variables
		private float timer;
	#endregion

	#region Double variables
		[Range(0f, 1f)]
		public double speedReload;
	#endregion

	#region GameObject variables
		public GameObject[] weapons;
	#endregion

	void Start () {
		gunAmmoUI = GameObject.FindGameObjectWithTag("AmmoUI").GetComponent<GunAmmoUI> ();
		weaponNumber = 0;
		speedReload = 1.1;
		timer = 0;
	}

	public void HideAllWeapons(){
		foreach (GameObject op in weapons)
		{
			op.SetActive(false);
		}
	}

	private void ChangeWeapon(){
		if (Input.GetAxis ("Mouse ScrollWheel") != 0 && timer > speedReload && !Input.GetButton ("Fire2")) {
			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				if (weaponNumber > 0)
					weaponNumber -= 1;
				else
					weaponNumber = weapons.Length - 1;

				SkipMissingFinal();

			} else if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				if (weaponNumber < weapons.Length - 1)
					weaponNumber += 1;
				else
					weaponNumber = 0;

				SkipMissingStarter();
			} 

		}
	}

	void SkipMissingStarter(){
		while(weaponNumber <= (weapons.Length - 1) && weapons [weaponNumber].GetComponent<Weapon> ().isAvailable == false)
		{
			weaponNumber++;
			if(weaponNumber > weapons.Length - 1)			
				weaponNumber = 0;														
		}
		Weapon weapon = weapons [weaponNumber].GetComponent<Weapon> ();
		HideAllWeapons ();
		weapons [weaponNumber].SetActive (true);
		weapon.ReadyWeaponPlay ();
		gunAmmoUI.UpdateAmmoUI (weapon.GetCurrentAmo (), weapon.GetGunMagazine () * weapon.GetMaxAmo ());
		timer = 0;
	}

	void SkipMissingFinal(){
		while(weaponNumber >= 0 && weapons [weaponNumber].GetComponent<Weapon> ().isAvailable == false)
		{
			weaponNumber--;
			if(weaponNumber < 0)			
				weaponNumber = weapons.Length - 1;														
		}
		Weapon weapon = weapons [weaponNumber].GetComponent<Weapon> ();
		HideAllWeapons ();
		weapons [weaponNumber].SetActive (true);
		weapon.ReadyWeaponPlay ();
		gunAmmoUI.UpdateAmmoUI (weapon.GetCurrentAmo (), weapon.GetGunMagazine () * weapon.GetMaxAmo ());
		timer = 0;
	}

	void Update () {
		if(Time.timeScale == 0) return;
		timer += Time.deltaTime;
		ChangeWeapon ();
	}
}
