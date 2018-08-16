using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolAIController : MonoBehaviour {


    #region Vars
    public PatrolAbility patrolAbility;
    public Transform[] Waypoints;


    #endregion
    void Start(){
        GetComponent<SwordTriggerable>();
        patrolAbility.Waypoints = Waypoints;
        patrolAbility.Initialize(gameObject);
        patrolAbility.TriggerAbility();
    }


	void Update() {

		
	}
}
