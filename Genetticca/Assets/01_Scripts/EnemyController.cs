using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public Transform Player;
	public GameObject DivisionSpawn;
	public float MoveSpeed = 1.0f;
	public float DivisionDist = 2.0f;
	public float StartDist = 4.0f;
	public float StopDist = 0.5f;
	public int MaxEnemies = 3;
	private PlayerController playerController;




	void Start () 
	{

	}

	void Update ()
	{
		transform.LookAt (Player);

		if ((Vector3.Distance (transform.position, Player.position) < StartDist) && (Vector3.Distance (transform.position, Player.position) >= StopDist)) {

			transform.position += transform.forward * MoveSpeed * Time.deltaTime;



			if (Vector3.Distance (transform.position, Player.position) <= DivisionDist) {
				Divide ();
			} 

		}
	}

	void Divide()
	{
		if (GameObject.FindGameObjectsWithTag("Enemy").Length < MaxEnemies)
		{
			Instantiate(DivisionSpawn);
		}
	}	

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Prota")
		{
			//Destroy(col.gameObject);
			col.gameObject.GetComponent<PlayerController> ().getDamage();
		}
	}
}
