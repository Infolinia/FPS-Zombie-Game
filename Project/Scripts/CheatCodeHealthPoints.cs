using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeHealthPoints : MonoBehaviour {

	#region GameObject variables
	public Player player;
	#endregion

	#region String variables
	private string[] cheatCode;
	#endregion

	#region Int variables
	private int index;
	#endregion

	void Start() {
		cheatCode = new string[] { "/", "h", "p" };
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
			player.SetCurrentHealth (100);
			index = 0;    
		}
	}
}
