using System.Collections;
using UnityEngine;
using SplineMesh;

/// <summary>
/// Attacker used to have a projectile created to follow a spline
/// </summary>
public class SplineAttacker : Attacker {
    [SerializeField] private Boomerang boomerang;
    public Transform spawnParent;
    private SplineNode _firstNode;

    void Start() {
        _firstNode = spawnParent.GetComponent<Spline>().nodes[0];
    }

    protected override void SendProjectile() {
        if(_currentEnemy == null)
            return;

        Boomerang p = Instantiate(boomerang, transform.TransformPoint(_firstNode.Position), transform.rotation, spawnParent);
        p.SetEnemy(_currentEnemy);
        p.damage = Mathf.FloorToInt(damage * modifier.damageModifier);
        p.speed = projectileSpeed;
    }

    /// <summary>
    /// Projectile creation based on speed of attacker
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator ProjectileCoroutine() {
        _projectileCoroutineStarted = true;
        SendProjectile();
        while(true) {
            float speed = attackSpeed * modifier.speedModifier;
            if(Mathf.Approximately(speed, 0.0f))
                yield return new WaitForSeconds(1);
            else
                yield return new WaitForSeconds(1 / speed);

            SendProjectile();
        }
    }
}
