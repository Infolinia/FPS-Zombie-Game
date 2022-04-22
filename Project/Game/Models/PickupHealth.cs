using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : MonoBehaviour {

	#region Player variables
		public Player player;
	#endregion

	#region Audio variables
		private AudioSource audio;
		public AudioClip pickup;
		public AudioClip nopickup;
	#endregion

	void Start () {
		audio = GameObject.FindGameObjectWithTag("Weapons").GetComponent<AudioSource> ();
	}

	private void PlaySound(AudioClip sound){
		audio.clip = sound;
		audio.Play ();
	}


	private void Pickup(){
		float hp = player.GetCurrentHealth ();
		if (hp < 100) {
			player.SetCurrentHealth (hp + 30);
			if(player.GetCurrentHealth () > 100)
				player.SetCurrentHealth(100);
			PlaySound (pickup);
			Destroy (this.gameObject);
		}else
			PlaySound (nopickup);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Pickup ();
		}
	}

	void Update(){

	}
}
