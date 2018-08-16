using UnityEngine;
using System.Collections;

public abstract class Ability : ScriptableObject
{

 

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();
}
