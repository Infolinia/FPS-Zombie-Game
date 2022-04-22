using UnityEngine;
using System.Collections;

public class PozycjaGracza : MonoBehaviour {

	public static Vector3 wektorPrzod;
	public static Vector3 pozycja;

	void FixedUpdate(){
		wektorPrzod = transform.forward;
		pozycja = transform.position;
	}
}
