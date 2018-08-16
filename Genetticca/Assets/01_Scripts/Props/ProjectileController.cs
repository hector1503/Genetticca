using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {


    public Bullet bulletPC;
    Rigidbody rb;
   

    void Awake()
    {
       
        rb = GetComponent<Rigidbody>();
        rb.useGravity = bulletPC.hasGravity;
       
        Destroy(gameObject,bulletPC.durationBullet );
       
    }

    // Use this for initialization
    void Start () {
          
        rb.velocity = bulletPC.getDirection() * bulletPC.speedBullet;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col)
	{

        Multitag multiTagObject = col.gameObject.GetComponentInChildren<Multitag>();
        if (multiTagObject != null)
        {
            bool isTagCollieder = col.gameObject.GetComponentInChildren<Multitag>().containsTagInList(bulletPC.tagsTrigger);

            if (isTagCollieder && col.gameObject.GetComponentInChildren<LifeController>()!=null)
		    {
               
                LifeController targetLifeC = col.gameObject.GetComponentInChildren<LifeController> ();
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
