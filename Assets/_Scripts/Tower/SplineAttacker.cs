using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineAttacker : MonoBehaviour
{
    [SerializeField] private Boomerang projectile;
    public int damage = 1;
    [Range(0.1f, 4f)] public float projectileSpeed = 2;
    public float attackSpeed = 1;
    private Enemy _currentEnemy;
    private Coroutine _currentCoroutine;
    private bool _projectileCoroutineStarted = false;
    private TowerModifier modifier = new TowerModifier(1, 1);

    public void Attack(Enemy enemy) {
        if(_projectileCoroutineStarted && enemy != _currentEnemy) StopCoroutine(_currentCoroutine);
        _currentEnemy = enemy;
        _currentCoroutine = StartCoroutine(ProjectileCoroutine());
    }


    public void Stop(Enemy enemy) {
        if(enemy == _currentEnemy) {
            StopCoroutine(_currentCoroutine);
        }
    }

    private void SendProjectile() {
        if(_currentEnemy == null) {
            return;
        }

        Boomerang p = Instantiate(projectile, transform.position, transform.rotation);
        p.SetEnemy(_currentEnemy);
        p.damage = Mathf.FloorToInt(damage * modifier.damageModifier);
        p.speed = projectileSpeed;
    }

    private IEnumerator ProjectileCoroutine() {
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
