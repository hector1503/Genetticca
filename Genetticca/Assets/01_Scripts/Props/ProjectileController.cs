using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {


    public Bullet bulletPC;
    Rigidbody rb;
   

    void Awake()
    {
       // target = GameObject.FindGameObjectsWithTag("Player")[0];
        
        rb = GetComponent<Rigidbody>();
        rb.useGravity = bulletPC.hasGravity;
        //transform.root.GetComponentInChildren<DestroyMe>().aliveTime = bulletPC.durationBullet;
        Destroy(gameObject,bulletPC.durationBullet );
        //if (bulletPC.isDefaultDirection && bulletPC.defaultDirection != Vector3.zero)
        //{
        //    bulletPC.setDirection(bulletPC.defaultDirection.normalized);
        //}
        //else if(bulletPC.isInput)
        //{

        //}

    }

    // Use this for initialization
    void Start () {
        //if (bulletPC.isAutotargeted)
        //{
        //    bulletPC.setDirection(bulletPC.target.transform.position - transform.position);
        //}
        //else if(bulletPC.isDefaultDirection)
        //{

        //}else if (bulletPC.isInput)
        //{

        //}
        //else
        //{
        //    throw new System.Exception("ERROR: No se ha seleccionado ningun modo de bala.");
        //}

        //else if (bulletPC.getDirection() != null && bulletPC.getDirection() != Vector3.zero)
        //{
        //    if (!bulletPC.alwaysSameDirection)
        //    {
        //        //BALA CON DIRECCIÓN DE JOYSTICK
        //    }
        //}
        
        rb.velocity = bulletPC.getDirection() * bulletPC.speedBullet;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col)
	{

        Multitag multiTagObject = col.gameObject.GetComponent<Multitag>();
        if (multiTagObject != null)
        {
            bool isTagCollieder = col.gameObject.GetComponent<Multitag>().containsTagInList(bulletPC.tagsTrigger);

            if (isTagCollieder && col.gameObject.GetComponent<LifeController>()!=null)
		    {
                //TODO SISTEMA DE DAÑO
                // HealthController
                LifeController targetLifeC = col.gameObject.GetComponent<LifeController> ();
                targetLifeC.TakeDamage(bulletPC.attack);
                Destroy(gameObject);
		    }
            if (gameObject != null)
                Destroy(gameObject);
        }
    }

    public void InitializeBullet(Bullet bullet)
    {
        bulletPC = bullet;
        
    }

    
}
