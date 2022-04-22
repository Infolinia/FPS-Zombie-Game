using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScens : MonoBehaviour {

	#region GameObject variables
	public GameObject[] scens;
	private GameObject frend;
	#endregion

	void Start(){
		frend = GameObject.FindGameObjectWithTag("Frend");
	}

	public void HideActiveScen(){
		foreach (GameObject go in scens) {
			if (go.activeInHierarchy == true) {
				go.SetActive (false);
			}
		}
	}


	public void EnablePanuseScene(){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Time.timeScale = 0;
		HideActiveScen ();

		scens[0].SetActive (true);
	}

	public void EnableGameScene(){
		Time.timeScale = 1;
		HideActiveScen ();
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
		scens[1].SetActive (true);
	}

	public void EnableGameOverScene(){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Time.timeScale = 0;
		HideActiveScen ();
		scens[2].SetActive (true);
	}

	public void EnableGameWinScene(){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Time.timeScale = 0;
		HideActiveScen ();
		scens[3].SetActive (true);
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape) && frend.GetComponent<Frend>().healthPoints > 0) {
			if (scens [0].activeInHierarchy == false)
				EnablePanuseScene ();
			else {
				EnableGameScene ();	
			}
		}
	}
}
