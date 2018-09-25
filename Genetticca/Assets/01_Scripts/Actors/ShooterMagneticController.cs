using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMagneticController : MonoBehaviour
{

    public BurstOfBullets burstOfBullets;
    public GameObject bulletPrefab;
    public GameObject parentOfBullets;

    float m_nextShootTime;
    //float m_nextBulletTime;
    //int m_contBulletsBurst;
    bool m_isShooting;
    Bullet bullet;





    void Awake()
    {
        m_nextShootTime = 0f;
    }

    // Use this for initialization
    void Start()
    {
        bullet = bulletPrefab.GetComponent<ProjectileMagneticController>().bulletPC;
    }

    private void Update()
    {

    }

    // Disparo
    public void ShootWithDirection(Vector3 direction)
    {
        bulletPrefab.GetComponent<ProjectileMagneticController>().bulletPC.setDirection(direction.normalized);
        MasterShoot();
    }

    // Disparo
    public void Shoot()
    {

        //bulletPrefab.GetComponent<ProjectileController>().bulletPC = bullet;

        if (bullet.isDefaultDirection)
            bulletPrefab.GetComponent<ProjectileMagneticController>().bulletPC.setDirection(bullet.defaultDirection);


        MasterShoot();

    }

    //public void setDriection(Vector3 directionValue)
    //{
    //    if (directionValue != null && directionValue != Vector3.zero)
    //    {
    //        //bullet.projectilePrefab.GetComponent<ProjectileController>().bulletPC.setDirection(directionValue);
    //    }
    //}

    public bool getIsBursting()
    {
        return this.m_isShooting;
    }

    private void MasterShoot()
    {
        if (m_nextShootTime < Time.time && parentOfBullets.transform.childCount == 0)
        {
            m_nextShootTime = Time.time + burstOfBullets.timeBetweenBullets;
            m_isShooting = true;
            Instantiate(bulletPrefab, transform.position, transform.rotation, parentOfBullets.transform);
        }

    }


}


