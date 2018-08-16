using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/SwordAbility")]
public class SwordAbility : Ability
{

    //public GameObject SwordPrefab;
    [TagSelector]
    public string playerTag;


    GameObject espadaGO;
    Sword Sword;
    private SwordTriggerable swordTriggerable;
    private SwordController swordController;

    
    public override void Initialize(GameObject obj)
    {
        if (swordTriggerable == null)
        {
            //GameObject player = GameObject.FindGameObjectWithTag(playerTag).transform.root.gameObject;
            swordTriggerable = obj.GetComponent<SwordTriggerable>();
            //No funciona pq esta disable la espada
            espadaGO = obj.transform.Find("Espada").gameObject;
            //espadaGO = GameObject.FindGameObjectsWithTag(espadaGameObjectTag)[0];
            //posiblie solucion al disable
            swordTriggerable.swordGO = espadaGO;

            swordController = espadaGO.GetComponentInChildren<SwordController>();
            swordTriggerable.swordController = swordController;

            Sword = swordController.SwordSc;
            swordTriggerable.sword = Sword;
        }
    }

    public override void TriggerAbility()
    {
        swordTriggerable.Attack();
    }

   
}