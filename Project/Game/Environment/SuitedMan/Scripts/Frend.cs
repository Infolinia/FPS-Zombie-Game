using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frend : MonoBehaviour {
	public Text missionInfo;
	public Transform target;
	private Animator animator;
	public float healthPoints = 50.0f;
	public SwitchScens switchScens;
	public AudioClip hitSound;
	private AudioSource audio;

	void Start(){
		animator = GetComponent<Animator> ();
		audio = GameObject.FindGameObjectWithTag ("AudioSource").GetComponent<AudioSource> ();
	}

	void takeHitSound(){
		audio.clip = hitSound;
		audio.Play ();
	}

	private void GameOverScene(){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		switchScens.EnableGameOverScene ();
	}

	IEnumerator Over()
	{
		yield return new WaitForSeconds(1f);
		GameOverScene ();
	}

	void takeHit(float damage) 
	{
		healthPoints -= damage;
		takeHitSound ();
		if (healthPoints <= 0) {
			GetComponent<CharacterController> ().enabled = false;
			animator.SetBool ("isDead", true);
			StartCoroutine(Over());
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.Equals ("Player")) {
			missionInfo.text = "Opuść wyspę";
		}
	}

	private void UpdateRotation(Collider other){
		Quaternion targetRotation = Quaternion.LookRotation(other.transform.position - transform.position);
		Quaternion finalRotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
		finalRotation.x =transform.rotation.x;
		finalRotation.z = transform.rotation.z;
		transform.rotation = finalRotation;
	}

	void OnTriggerStay(Collider other){
		if (other.tag.Equals ("Player") && healthPoints > 0.0f) {
			UpdateRotation (other);
			float distance = Vector3.Distance (transform.position, target.transform.position);
			if (distance > 2) {
				animator.SetBool ("IsWalk", true);
				transform.Translate(Vector3.forward * 0.5f * Time.deltaTime);
			} else {
				animator.SetBool ("IsWalk", false);
			}
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.tag.Equals ("Player")) {
			animator.SetBool ("IsWalk", false);
		}
	}
}

