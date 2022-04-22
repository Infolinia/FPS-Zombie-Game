using UnityEngine;
using System.Collections;

public class ZadajCube : MonoBehaviour {

	public GameObject obrazeniaUI;

	void OnTriggerEnter(Collider col){
		obrazeniaUI.SendMessage ("aktualizujHUD", transform.forward);
	}
}
