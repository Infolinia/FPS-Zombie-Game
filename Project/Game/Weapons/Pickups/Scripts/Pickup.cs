using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType{
	ammo, pistol
}
	
public class Pickup : MonoBehaviour {

	#region Audio variables
		private AudioSource audio;
		public AudioClip pickup;
		public AudioClip nopickup;
	#endregion

	#region PickupType variables
		public PickupType pickupType;
	#endregion

	#region GameObject variables
	public GameObject weapon;
	#endregion

	void Start () {
		audio = GameObject.FindGameObjectWithTag("Weapons").GetComponent<AudioSource> ();
	}

	private void PlaySound(AudioClip sound){
		audio.clip = sound;
		audio.Play ();
	}

	private void PickupItem(){
		Weapon w = weapon.GetComponent<Weapon> ();
		if (pickupType == PickupType.ammo) {
			if (w.isAvailable) {
				w.AddWeaponMagazine ();
				w.UpdateUIMagazine ();
				Destroy (this.gameObject);
				PlaySound (pickup);
			} else {
				PlaySound (nopickup);
			}
		} else if (pickupType == PickupType.pistol) {
			if (!w.isAvailable) {
				w.isAvailable = true;
				Destroy (this.gameObject);
				PlaySound (pickup);
			}else {
				PlaySound (nopickup);
			}
		}
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			PickupItem ();
		}
	}
}
