using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour {

	#region Scrollbar variables
	public Scrollbar scrollSound;
	#endregion

	void Start(){
		scrollSound.value = AudioListener.volume;
	}

	public void ChangeValueSound(){
		AudioListener.volume = scrollSound.value;
	}
}
