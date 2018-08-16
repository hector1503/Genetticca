using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum ModeShooting
//{
//    None, autoTarget, defaultDirection, stick
//}

[CreateAssetMenu(menuName = "Patrol/PatrolEnemy")]
public class Patrol : ScriptableObject
{
   
    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    public int curWayPoint = 0;
    [SerializeField]
    public bool doPatrol = true;




}
