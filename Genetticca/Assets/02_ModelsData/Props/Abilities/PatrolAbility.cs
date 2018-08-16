using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/PatrolAbility")]
public class PatrolAbility : Ability
{

    //public GameObject SwordPrefab;
    public Patrol patrolData;
    [HideInInspector]
    public Transform[] Waypoints;



    private PatrolTriggerable patrolTriggerable;
    

    
    public override void Initialize(GameObject obj)
    {
        patrolTriggerable = obj.GetComponent<PatrolTriggerable>();
        patrolTriggerable.Waypoints = Waypoints;
        patrolTriggerable.patrol = patrolData;
    }

    public override void TriggerAbility()
    {
       patrolTriggerable.SetActivePatrol(patrolData.doPatrol);
    }

    public void setDoPatrol(bool value)
    {
        patrolTriggerable.patrol.doPatrol = value;
        patrolData.doPatrol = value;
    }

    public bool getDoPatrol()
    {
        return patrolData.doPatrol;
    }
   

   
   
}