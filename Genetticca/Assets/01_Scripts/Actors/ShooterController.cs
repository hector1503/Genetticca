using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{

    // public GameObject bulletprojectilePrefab;
    //  public float timeBetweenShoots = 2F;

    public BurstOfBullets burstOfBullets;
    public GameObject bulletPrefab;
    public GameObject parentOfBullets;
    //public Bullet bullet;
    //public float castTime = 0F;

    float m_nextShootTime;
    float m_nextBulletTime;
    int m_contBulletsBurst;
    bool m_isBursting;
    Bullet bullet;





    void Awake()
    {
        m_nextShootTime = 0f;
        m_nextBulletTime = 0f;
    }

    // Use this for initialization
    void Start()
    {
        bullet = bulletPrefab.GetComponent<ProjectileController>().bulletPC;
        m_contBulletsBurst = burstOfBullets.numberBullets;

    }

    private void Update()
    {
        if (m_isBursting)
        {
            if (m_nextBulletTime < Time.time && m_contBulletsBurst > 0)
            {
                m_nextBulletTime = Time.time + burstOfBullets.timeBetweenBullets;
                m_contBulletsBurst--;
                Instantiate(bulletPrefab, transform.position, transform.rotation, parentOfBullets.transform);
            }
            else if (m_contBulletsBurst == 0)
            {
                m_isBursting = false;
                m_contBulletsBurst = burstOfBullets.numberBullets;
            }
        }
    }

    // Disparo
    public void ShootWithDirection(Vector3 direction)
    {
        bulletPrefab.GetComponent<ProjectileController>().bulletPC.setDirection(direction.normalized);
        MasterShoot();
    }

    // Disparo
    public void Shoot()
    {

        //bulletPrefab.GetComponent<ProjectileController>().bulletPC = bullet;

        if (bullet.isDefaultDirection)
            bulletPrefab.GetComponent<ProjectileController>().bulletPC.setDirection(bullet.defaultDirection);
        

        MasterShoot();

    }

    public void setDriection(Vector3 directionValue)
    {
        if (directionValue != null && directionValue != Vector3.zero)
        {
            //bullet.projectilePrefab.GetComponent<ProjectileController>().bulletPC.setDirection(directionValue);
        }
    }

    public bool getIsBursting()
    {
        return this.m_isBursting;
    }

    private void MasterShoot()
    {
        if (m_nextShootTime < Time.time)
        {
            m_nextShootTime = Time.time + burstOfBullets.timeBetweenShoots;
            //Es una rafaga (burst)
            if (burstOfBullets.numberBullets > 1)
            {
                m_isBursting = true;

            }
            else//No es rafaga, solo una bala
            {
                //bullet.projectilePrefab.GetComponent<ProjectileController>().bulletPC = bullet;
                //bullet.projectilePrefab.GetComponent<ProjectileController>().bulletPC.setDirection(direction);
                Instantiate(bulletPrefab, transform.position, transform.rotation, parentOfBullets.transform);

            }
        }
        //projectileC.direction = target.transform.position - transform.position;
    }


}


