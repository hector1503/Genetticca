using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{

    public Bullet bullet;
    public BurstOfBullets burstOfBullets;
    public float castTime = 0F;

    private ProjectileShootTriggerable launcher;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<ProjectileShootTriggerable>();
        //launcher.bulletPrefab = bullet;
        launcher.burstOfBullets = burstOfBullets;
        launcher.castTime = castTime;
        //launcher.projectileForce = projectileForce;
       // launcher.projectile = projectile;
    }

    public override void TriggerAbility()
    {
        launcher.Shoot();
    }

}