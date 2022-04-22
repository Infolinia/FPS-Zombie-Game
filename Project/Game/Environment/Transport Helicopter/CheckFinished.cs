using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinished : MonoBehaviour {
	public SwitchScens switchScens;
	private bool isEntered = false;

	void OnTriggerEnter(Collider other){
		if (other.tag.Equals ("Frend") ) {
			isEntered = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag.Equals ("Frend")) {
			isEntered = false;
		}
	}

	void Update(){
		if (isEntered) {
			switchScens.EnableGameWinScene ();
		}
	}
}
