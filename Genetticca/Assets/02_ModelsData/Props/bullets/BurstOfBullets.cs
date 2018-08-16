using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(menuName = "Bullet/BurstOfBullets")]
public class BurstOfBullets : ScriptableObject
{
    [SerializeField]
    public int numberBullets;
    [SerializeField]
    public float timeBetweenBullets;
    [SerializeField]
    public float timeBetweenShoots;
    //[SerializeField]
    //public Bullet bullet;
    [SerializeField]
    public float castTime=0;
}
