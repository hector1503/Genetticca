using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{


	public float fullHealth;
	float currentHealth;
	public GameObject deathFx;
	GameObject thePlayer;
	playerHealth thePlayerHealth;
	public CheckPoint[] CheckPointsList;




	// Use this for initialization
	void Start ()
	{
		currentHealth = fullHealth;	
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		thePlayerHealth = thePlayer.GetComponentInChildren<playerHealth> ();
		//CheckPointsList = GameObject.FindGameObjectsWithTag ("CheckPoint");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public float addDamage (float damage)
	{
		currentHealth -= damage;

		Vector3 position = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
		Instantiate (deathFx,position, Quaternion.Euler(new Vector3(-90,0,0)));
		if (currentHealth <= 0) {
			makeDead ();
		}

		return currentHealth;
	}

	public float addCuracion (float numCuracion)
	{
		currentHealth += numCuracion;
		if (currentHealth > fullHealth)
			currentHealth = fullHealth;

		return currentHealth;
	}

	public void makeDead ()
	{
		Transform result;
		result = thePlayer.transform;

		if (gameObject.tag == "Player" || gameObject.tag == "PlayerChilds" || gameObject.tag == "targetPlayer") {
			if (CheckPointsList != null) {
				foreach (CheckPoint cp in CheckPointsList) {
					// We search the activated checkpoint to get its position
					if (cp.getIsActive()) {
						result = cp.transform;
						currentHealth = cp.getSaveHealth ();
					}
				}
			}
			
			thePlayer.transform.position = result.position;
			thePlayer.GetComponent<CharacterController> ().Move(Vector3.zero);

		
	
		} else {
			Vector3 position = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
			Instantiate (deathFx, position, Quaternion.Euler (new Vector3 (-90, 0, 0)));
			Destroy (transform.root.gameObject);
		}
	}

	public float getPlayerHealth(){
		return currentHealth;
	}
}