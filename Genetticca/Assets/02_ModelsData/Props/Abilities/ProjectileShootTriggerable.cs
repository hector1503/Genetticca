using UnityEngine;
using System.Collections;

public class ProjectileShootTriggerable : MonoBehaviour
{

    public BurstOfBullets burstOfBullets;
    public GameObject bulletPrefab;
    public float castTime = 0F;

    float nextShootTime;
    float nextBulletTime;
    int contBulletsBurst;
    bool isBursting;                  // Float variable to hold the amount of force which we will apply to launch our projectiles
    Bullet bullet;

    // Use this for initialization
    void Start()
    {
        contBulletsBurst = burstOfBullets.numberBullets;
        bullet = bulletPrefab.GetComponent<ProjectileController>().bulletPC;

    }

    private void Update()
    {
        if (isBursting)
        {
            if (nextBulletTime < Time.time && contBulletsBurst > 0)
            {
                nextBulletTime = Time.time + burstOfBullets.timeBetweenBullets;
                contBulletsBurst--;
                Instantiate(bulletPrefab, transform.position, transform.rotation, transform);
            }
            else if (contBulletsBurst == 0)
            {
                isBursting = false;
                contBulletsBurst = burstOfBullets.numberBullets;
            }
        }
    }


    public void Shoot()
    {
        if (nextShootTime < Time.time)
        {
            nextShootTime = Time.time + burstOfBullets.timeBetweenShoots;
           // bullet.projectilePrefab.GetComponent<ProjectileController>().bulletPC = bullet;


            if (bullet.isAutotargeted)
            {
                bullet.setDirection(bullet.target.transform.position - transform.position);
            }
            else if (bullet.isDefaultDirection)
            {
                bullet.setDirection(bullet.defaultDirection);
            }
            else if (bullet.isInput)
            {
                bullet.setDirection(bullet.defaultDirection);
            }
            else
            {
                throw new System.Exception("ERROR: No se ha seleccionado ningun modo de bala.");
            }


            //Es una rafaga (burst)
            if (burstOfBullets.numberBullets > 1)
            {
                isBursting = true;

            }
            else//No es rafaga, solo una bala
            {
               // bullet.projectilePrefab.GetComponent<ProjectileController>().bulletPC = bullet;
          
          
                Instantiate(bulletPrefab, transform.position, transform.rotation);

            }
            //projectileC.direction = target.transform.position - transform.position;
        }


    }
    public void setDriection(Vector3 directionValue)
    {
        if (directionValue != null && directionValue != Vector3.zero)
        {
         //   bullet.projectilePrefab.GetComponent<ProjectileController>().bulletPC.defaultDirection = directionValue;
        }
    }

    public bool getIsBursting()
    {
        return this.isBursting;
    }
    
}