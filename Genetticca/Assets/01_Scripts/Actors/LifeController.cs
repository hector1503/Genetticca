using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {


    public float fullHealth;
    public float currentHealth;
    public GameObject damageFx;

    DamageController damageController;
  
    //public CheckPoint[] CheckPointsList;


    // Use this for initialization
    void Start () {
        currentHealth = fullHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public float TakeDamage(Attack attack)
    {
        currentHealth -= attack.damage;
        damageController =  GetComponent<DamageController>();
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        if(damageFx!=null)
            Instantiate(damageFx, position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        if (damageController != null)
            damageController.enabled = true;

        if (currentHealth <= 0)
        {
            Dead();
        }

        return currentHealth;
    }

    private void Dead()
    {
        if (damageFx != null)
            Instantiate(damageFx, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        Destroy(gameObject);

    }


}
