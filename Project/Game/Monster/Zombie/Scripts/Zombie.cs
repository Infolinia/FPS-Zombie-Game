using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

	#region Audio variables
	private AudioSource audio;
	public AudioClip attackSound;
	public AudioClip hitSound;
	#endregion

	#region Float variables
	private float timer = 0.0f;
	public float attackDistance = 0.5f;
	public float healthPoints = 20.0f;
	#endregion

	#region Bool variables
	private bool isDead = false;
	private bool isCollision = false;
	private bool isActive = false;
	#endregion

	#region GameObject variables
	public GameObject obrazeniaUI;
	#endregion

	#region NavMeshAgent variables
	private NavMeshAgent navMeshAgent;
	#endregion

	#region Animator variables
	private Animator animator;
	#endregion

	#region Transform variables
	public Transform target;
	#endregion

	#region Collider variables
	private Collider player;
	#endregion

	void OnTriggerExit(Collider other) {
		if (other.tag.Equals ("Player")) {
			animator.SetInteger ("Value", 0);
			isCollision = false;
			animator.enabled = false;
		}
	}

	void OnEnable(){
		if (healthPoints <= 0) {
			Destroy (gameObject);
		}
	}

	void Awake(){
		obrazeniaUI =  GameObject.FindGameObjectWithTag ("DamageManager");
		target =  GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		animator = GetComponent<Animator> ();
		audio = GameObject.FindGameObjectWithTag ("AudioSource").GetComponent<AudioSource> ();
		this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
		animator.enabled = false;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals ("Player")) {
			animator.enabled = true;
			player = other;
			isCollision = true;
			if (!isActive) {
				this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
				isActive = true;
			}
		}
	}


	private void UpdateRotation(Collider other){
		Quaternion targetRotation = Quaternion.LookRotation(other.transform.position - transform.position);
		Quaternion finalRotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
		finalRotation.x =transform.rotation.x;
		finalRotation.z = transform.rotation.z;
		transform.rotation = finalRotation;
	}

	void TakeHitSound(){
		audio.clip = hitSound;
		audio.Play ();
	}

	void TakeHit(float damage) 
	{
		if (!isDead) {
			healthPoints -= damage;
			TakeHitSound ();
			if (healthPoints <= 0) {
				animator.enabled = true;
				GetComponent<CharacterController> ().enabled = false;
				GetComponent<BoxCollider> ().enabled = false;
				animator.SetInteger ("Value", 4);
				isDead = true;
				audio.Pause ();
			}
		}
	}

	void Update(){
		if (!isDead && isCollision && isActive) {
			UpdateRotation (player);
			float distance = Vector3.Distance (transform.position, target.transform.position);
			if (distance > attackDistance) {
				animator.SetInteger ("Value", 2);
			} else {
				if (timer > 1.5f) {
					if (player.enabled == true && !isDead) {
						player.SendMessage ("TakeHit", Random.Range (4, 20), SendMessageOptions.DontRequireReceiver);
						animator.SetInteger ("Value", 3);
						obrazeniaUI.SendMessage ("aktualizujHUD", transform);
						audio.clip = attackSound;
						if(!audio.isPlaying )
							audio.Play ();
						timer = 0;
					}
				}
			}
		}
		timer += Time.deltaTime;
	}
}
