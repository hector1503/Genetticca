using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMagneticController : MonoBehaviour
{


    public Bullet bulletPC;

    public float startMagneticEffect = 1.5f;

    Transform player;
    Rigidbody rb;
    float startTime;
    Vector3 nextPosition;
    Vector3 direction;
    Quaternion rotationProjectile;
    float forceProjectile = 0f;

    void Awake()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.transform;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = bulletPC.hasGravity;

        Destroy(gameObject, bulletPC.durationBullet);


    }

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        direction = bulletPC.getDirection();
        forceProjectile = 1;
    }
    private void Update()
    {

        if (Time.time > startTime + startMagneticEffect)
        {

            if (Input.GetAxis("FireMagnetic") >= 0.1f && Vector3.Distance(rb.position, player.position) < 15f)
            {
                forceProjectile = Input.GetAxis("FireMagnetic");
                direction = rb.position - player.position;
            }
            else
            {
                forceProjectile = 1;
                direction = player.position - rb.position;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Time.time > startTime + startMagneticEffect)
        //{
        //    if (Input.GetAxis("FireMagnetic") >= 0.1f && Vector3.Distance(rb.position, player.position) < 9f)


        //    else


        //}
        direction.z = 0;
        direction = direction.normalized;
        rb.transform.Translate(direction * forceProjectile * bulletPC.speedBullet * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {

        Multitag multiTagObject = col.gameObject.GetComponentInChildren<Multitag>();
        if (multiTagObject != null)
        {
            bool isTagCollieder = col.gameObject.GetComponentInChildren<Multitag>().containsTagInList(bulletPC.tagsTrigger);


            if (gameObject != null)
                Destroy(gameObject);
            if (Time.time > startTime + startMagneticEffect && col.gameObject.GetComponent<Multitag>().ContainsTag("Player"))
            {
                Destroy(gameObject);
            }
            if (isTagCollieder && col.gameObject.GetComponentInChildren<LifeController>() != null)
            {
                LifeController targetLifeC = col.gameObject.GetComponentInChildren<LifeController>();
                targetLifeC.TakeDamage(bulletPC.attack);
                Destroy(gameObject);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool isTagCollieder = collision.gameObject.GetComponentInChildren<Multitag>().containsTagInList(bulletPC.tagsTrigger);

    }

    public void InitializeBullet(Bullet bullet)
    {
        bulletPC = bullet;

    }


}
