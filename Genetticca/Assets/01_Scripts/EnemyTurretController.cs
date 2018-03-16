using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretController : MonoBehaviour {

	public Transform player;
	public float range = 20.0f;
	public float bulletImpulse = 10.0f;
	public float startTimeShoot = 2.0f;
	public float minTimeShoot = 1.0f;
	public float maxTimeShoot = 3.0f;

	private bool onRange= false;

	public Rigidbody projectile;

	void Start(){
		float rand = Random.Range (minTimeShoot, maxTimeShoot);
		InvokeRepeating("Shoot", startTimeShoot, rand);
	}

	void Shoot(){

		if (onRange){

			Rigidbody bullet = (Rigidbody)Instantiate(projectile, transform.position + transform.forward, transform.rotation);
			bullet.AddForce(transform.forward*bulletImpulse, ForceMode.Impulse);

			Destroy (bullet.gameObject, 1);
		}


	}

	void Update() {

		onRange = Vector3.Distance(transform.position, player.position)<range;

		if (onRange)
			transform.LookAt(player);
	}
}
