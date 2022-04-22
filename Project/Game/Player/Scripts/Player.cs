using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour {

	#region Text variables
	public Text healthPoints;
	#endregion

	#region Float variables
	private float currentHealth;
	#endregion

	#region Audio variables
	private AudioSource audio;
	public AudioClip deadSound;
	#endregion

	#region Bool variables
	private bool isKilled = false;
	#endregion

	#region SwitchScens variables
	public SwitchScens switchScens;
	#endregion

	#region WeaponsManager variables
	public WeaponsManager weaponManager;
	#endregion

	public float GetCurrentHealth(){
		return currentHealth;
	}

	public void SetCurrentHealth(float hp){
		currentHealth = hp;
		healthPoints.text = currentHealth.ToString();
	}


	void Start(){
		healthPoints = healthPoints.GetComponent<Text> ();
		currentHealth = float.Parse(healthPoints.text);
		audio = GameObject.FindGameObjectWithTag ("Weapons").GetComponent<AudioSource> ();
	}

	private void GameOverScene(){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		switchScens.EnableGameOverScene ();
	}

	IEnumerator RotateObject(float angle, Vector3 axis, float inTime)
	{
		float rotationSpeed = angle / inTime;

		while (true)
		{
			Quaternion startRotation = transform.rotation;

			float deltaAngle = 0;
			while (deltaAngle < angle)
			{
				deltaAngle += rotationSpeed * Time.deltaTime;
				deltaAngle = Mathf.Min(deltaAngle, angle);

				transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

				yield return null;
			}
			Time.timeScale = 0.0f; 
			GameOverScene ();
			yield return new WaitForSeconds(1);
		}
	}

	private void IsDead(){
		if (!isKilled) {
			audio.clip = deadSound;
			audio.Play ();
			weaponManager.HideAllWeapons ();
			StartCoroutine (RotateObject (90, Vector3.right, 1.0f));
			isKilled = true;
		}
	}

	public void TakeHit(float damage) 
	{
		if(currentHealth - damage > 0) {
			currentHealth -= damage;
		}else {
			currentHealth = 0;
			IsDead ();
		}
		healthPoints.text = currentHealth.ToString();
	}
}
