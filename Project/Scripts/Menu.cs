using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {

	#region GameObject variables
	public GameObject[] panels;
	private GameObject buttonsUI;
	#endregion

	void setActiveButtonUI(bool value){
		if(buttonsUI != null)
			buttonsUI.SetActive (value);
	}

	void Start(){
		buttonsUI = GameObject.FindGameObjectWithTag("ButtonsUI");
		if (gameObject.name == "FirstScene" || gameObject.name == "PauseScene") {
			setActiveButtonUI (true);
		}else
			setActiveButtonUI (false);
	}

	private bool CheckPanelIsEnabled(){
		foreach (GameObject go in panels) {
			if (go.activeInHierarchy == true) {
				return true;
			}
		}
		return false;
	}

	public void HideActivePanel(){
		foreach (GameObject go in panels) {
			if (go.activeInHierarchy == true) {
				go.SetActive (false);
				setActiveButtonUI (true);
			}
		}
	}

	public void NewGame(){
		if (CheckPanelIsEnabled () == false) {
			panels [0].SetActive (true);
			setActiveButtonUI (false);
		}
	}

	public void NewGameYes(){
		HideActivePanel ();
		panels [4].SetActive (true);
		setActiveButtonUI (false);
		Time.timeScale = 1;
	}

	public void OptionsGame(){
		if (CheckPanelIsEnabled () == false) {
			panels [1].SetActive (true);
			setActiveButtonUI (false);
		}
	}

	public void OptionsGameYes(){
		Debug.Log ("Ładuję zapisaną grę...");
		HideActivePanel ();
	}

	public void AuthorsGame(){
		if (CheckPanelIsEnabled () == false) {
			panels [2].SetActive (true);
			setActiveButtonUI (false);
		}
	}

	public void ExitGame(){
		if (CheckPanelIsEnabled () == false) {
			panels [3].SetActive (true);
			setActiveButtonUI (false);
		}
	}

	public void ExitGameYes(){
		Debug.Log ("Wychodzę z gry...");
		HideActivePanel ();
		Application.Quit ();
	}
}
