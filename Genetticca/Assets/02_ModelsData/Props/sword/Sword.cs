using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum ModeShooting
//{
//    None, autoTarget, defaultDirection, stick
//}

[CreateAssetMenu(menuName = "Weapon/Sword")]
public class Sword : ScriptableObject
{
    [SerializeField]
    public Attack attack;

    [SerializeField]
    public float durationSword;
    [SerializeField]
    public float timeBetwenAttacks;
    [SerializeField]
    public float castTime;

    [TagSelector]
    public string tagParent;

    [TagSelector]
    public string[] tagsTrigger;




}
