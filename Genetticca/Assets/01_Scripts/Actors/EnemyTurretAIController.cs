using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretAIController : MonoBehaviour {


    #region Vars

    public Transform player;
	public float range = 20.0f;

	public float minTimeShoot = 1.0f;
	public float maxTimeShoot = 3.0f;



    private ShooterController[] shooterCList;
    private bool onRange= false;

	
    #endregion

    void Start(){
		float rand = Random.Range (minTimeShoot, maxTimeShoot);
		//InvokeRepeating("Shoot", startTimeShoot, rand);
        shooterCList = GetComponents<ShooterController>();
    }


	void Update() {

		onRange = Vector3.Distance(transform.position, player.position)<range;

		if (onRange)
        {
            //transform.LookAt(player);

            Vector3 direction = player.position-transform.position;
            foreach(ShooterController shooterControll in shooterCList)
            {
                if (shooterControll.bulletPrefab.GetComponent<ProjectileController>().bulletPC.isAutotargeted)
                {
                    shooterControll.ShootWithDirection(player.position - transform.position);
                }else if (shooterControll.bulletPrefab.GetComponent<ProjectileController>().bulletPC.isDefaultDirection)
                {
                    shooterControll.Shoot();
                }
            }
        }
	}
}
