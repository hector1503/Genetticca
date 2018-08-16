using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum ModeShooting
//{
//    None, autoTarget, defaultDirection, stick
//}

[CreateAssetMenu(menuName = "Bullet/Projectile")]
public class Bullet : ScriptableObject
{
    [SerializeField]
    public Attack attack;
    [SerializeField]
    public float speedBullet;
    [SerializeField]
    public float durationBullet;

    //public ModeShooting modeShooting ;
    public bool isAutotargeted = true;
    [SerializeField]
    [ConditionalHide ("isAutotargeted", true)]//TODO UTILITZAR ENUM PER FER UN SELECTOR
    public GameObject target= null;
    
    public bool isDefaultDirection = false;
    [SerializeField]
    [ConditionalHide("isDefaultDirection", true)]//TODO UTILITZAR ENUM PER FER UN SELECTOR
    public Vector3 defaultDirection ;

    public bool isInput = false;
    

    [SerializeField]
    public bool hasGravity=false;

    [TagSelector]
    public string[] tagsTrigger;
    private Vector3 direction;

    public Vector3 getDirection()
    {
        
        return this.direction;
    }

    public void setDirection(Vector3 value)
    {
        this.direction = value;
    }

   

    
}
