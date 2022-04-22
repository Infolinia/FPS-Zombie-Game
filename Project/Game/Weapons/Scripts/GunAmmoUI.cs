using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunAmmoUI : MonoBehaviour {

	private Text ammoCountUI;

	void Start(){
		ammoCountUI = GetComponent<Text>();
	}

	public void UpdateAmmoUI(int currentAmmo, int carriedAmmo){
		if (ammoCountUI != null) {
			ammoCountUI.text = currentAmmo.ToString () + " / " + carriedAmmo.ToString();
		}
	}
}
