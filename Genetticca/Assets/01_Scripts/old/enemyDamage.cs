using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour
{
	public float damage;
	public float damageRate;
	public float pushBackForce;



	float currentTime;
	//bool playerInRange;
	GameObject thePlayer;
	playerHealth thePlayerHealth;


	// Use this for initialization
	void Start ()
	{

		currentTime = damageRate;
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		thePlayerHealth = thePlayer.GetComponentInChildren<playerHealth> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if (playerInRange && thePlayer != null) {
			//Attack ();

		//}

		
	}

	//	void OnControllerColliderHit(ControllerColliderHit hit){
	//		Rigidbody body = hit.collider.attachedRigidbody;
	//		if (body == null || body.isKinematic)
	//			return;
	//		if (hit.gameObject.tag == "Player")
	//			playerInRange = true;
	//
	//	}

//	void OnTriggerEnter (Collider other)
//	{
//		if (other.tag == "Player") {
//			playerInRange = true;
//		}
//	}
//
//	void OnTriggerExit (Collider other)
//	{
//		if (other.tag == "Player") {
//			playerInRange = false;
//		}
//	}

	void OnTriggerStay(Collider other)
	{
		currentTime += Time.deltaTime;
		if (currentTime >= damageRate) {
			
			if (other.tag == "Player") {
				Attack ();
				currentTime = 0;
				pushBack (thePlayer.transform);
			}
		}
	}
	void Attack ()
	{
		HealthBar.health=thePlayerHealth.addDamage (damage);

	}

	void pushBack (Transform pushedObject)
	{
		if (pushedObject != null && pushedObject.root != null) {
			Vector3 pushDirection = new Vector3 ((pushedObject.position.x - transform.position.x), (transform.position.y - pushedObject.position.y), 0).normalized;
			//pushDirection *= pushBackForce;
			GameObject root = pushedObject.root.gameObject;
			pushDirection.x *= 4;
			pushDirection.y *= 0.5f;
//			Rigidbody pushedRB = root.GetComponent<Rigidbody> ();
//			pushedRB.velocity = Vector3.zero;
//			pushedRB.AddForce (pushDirection, ForceMode.Impulse);


			playerController myPlayerController = root.GetComponent<playerController> ();
			myPlayerController.AddImpact (pushDirection,pushBackForce);
		}

	}
}
