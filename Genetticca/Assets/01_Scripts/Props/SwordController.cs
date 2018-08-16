using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{


    public Sword SwordSc;

    Animator myAnim;
    GameObject parent;
    SwordTriggerable swordTr;


    List<GameObject> collisionObjects;

    void Awake()
    {
       // Destroy(gameObject, SwordSc.durationSword);

    }

    // Use this for initialization
    void Start()
    {
        collisionObjects = new List<GameObject>();
        parent = GameObject.FindGameObjectsWithTag(SwordSc.tagParent)[0];
        swordTr = parent.GetComponent<SwordTriggerable>();//TODO ON ES FA SERVIR EL SWORD TRIGGERABLE AQUI?????
        //myAnim = GetComponent<Animator>();
        //myAnim.SetBool("isAttacking", true);
    }

    void OnDisable()
    {
        collisionObjects.Clear();

    }


    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {

        Multitag multiTagObject = col.gameObject.GetComponent<Multitag>();
        if (multiTagObject != null && !collisionObjects.Contains(col.gameObject))
        {
            collisionObjects.Add(col.gameObject);
            bool isTagCollieder = col.gameObject.GetComponent<Multitag>().containsTagInList(SwordSc.tagsTrigger);

            if (isTagCollieder && col.gameObject.GetComponentInChildren<LifeController>() != null)
            {
                LifeController targetLifeC = col.gameObject.GetComponentInChildren<LifeController>();
                targetLifeC.TakeDamage(SwordSc.attack);
                
            }
            
        }
    }




}
