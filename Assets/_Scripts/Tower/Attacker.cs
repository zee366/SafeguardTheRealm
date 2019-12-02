using System.Collections;
using UnityEngine;

/// <summary>
/// Base class for tower's attackers.
/// Attacker are weapon's behaviors triggered to target enemies.
/// </summary>
public class Attacker : MonoBehaviour {

    [SerializeField] private Projectile projectile;
    public int damage = 1;
    [Range(0.1f, 4f)] public float projectileSpeed = 2;
    public float attackSpeed = 1;
    protected Enemy _currentEnemy;
    protected Coroutine _currentCoroutine;
    protected bool _projectileCoroutineStarted = false;
    protected TowerModifier modifier = new TowerModifier(1, 1);

    /// <summary>
    /// Set enemy target and start Coroutine for attacking
    /// </summary>
    /// <param name="enemy"></param>
    public void Attack(Enemy enemy) {
        if(_projectileCoroutineStarted && enemy != _currentEnemy) StopCoroutine(_currentCoroutine);
        _currentEnemy = enemy;
        _currentCoroutine = StartCoroutine(ProjectileCoroutine());
    }

    /// <summary>
    /// Stop attacking current enemy
    /// </summary>
    /// <param name="enemy"></param>
    public void Stop(Enemy enemy) {
        if(enemy == _currentEnemy) {
            StopCoroutine(_currentCoroutine);
        }
    }

    /// <summary>
    /// Sending a projectile instance and assign target to it.
    /// </summary>
    protected virtual void SendProjectile() {
        if(_currentEnemy == null) {
            return;
        }

        Projectile p = Instantiate(projectile, transform.position, transform.rotation);
        p.SetEnemy(_currentEnemy);
        p.damage = Mathf.FloorToInt(damage * modifier.damageModifier);
        p.speed = projectileSpeed;
    }

    /// <summary>
    /// Routine for creating projectile based on attack speed.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator ProjectileCoroutine() {
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


    /// <summary>
    /// Will be called by the supports towers event.
    /// Modifiers changes the speed/damage of attackers
    /// </summary>
    /// <param name="modifier"></param>
    public void ApplyAttackerModifier(TowerModifier modifier) {
        this.modifier.damageModifier += modifier.damageModifier - 1;
        this.modifier.speedModifier += modifier.speedModifier - 1;
    }


    public TowerModifier GetModifier() { return modifier; }

}