using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeWeapons : MonoBehaviour {

	#region GameObject variables
		public GameObject[] weapons;
	#endregion

	#region String variables
		private string[] cheatCode;
	#endregion

	#region Int variables
		private int index;
	#endregion

	void Start() {
		cheatCode = new string[] { "/", "w", "e", "a", "p", "o", "n", "s" };
		index = 0;    
	}

	void Update() {
		if (Input.anyKeyDown) {
			if (Input.GetKeyDown(cheatCode[index])) {
				index++;
			}
			else {
				index = 0;    
			}
		}
		if (index == cheatCode.Length) {
			foreach (GameObject op in weapons)
			{
				op.GetComponent<Weapon> ().isAvailable = true;
			}
			index = 0;    
		}
	}
}
