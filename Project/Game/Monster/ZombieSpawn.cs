using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour {

	#region GameObject variables
	public GameObject respawnPrefab;
	private GameObject[] respawns;
	#endregion

	void Awake()
	{
		if (respawns == null)
			respawns = GameObject.FindGameObjectsWithTag("Spawn");

		foreach (GameObject respawn in respawns)
		{
			GameObject childObject = Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation)  as GameObject;
			childObject.transform.parent = gameObject.transform;

		}
	}
}
