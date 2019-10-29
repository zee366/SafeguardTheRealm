using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {

    [SerializeField] private Projectile projectile;
    [Range(0.1f, 4f)] public float attackSpeed = 1;
    private GameObject _currentEnemy;
    private Coroutine _currentCoroutine;
    private bool _projectileCoroutineStarted = false;
    public void Attack(GameObject enemy) {
        if (_projectileCoroutineStarted && enemy != _currentEnemy) StopCoroutine(_currentCoroutine);
        _currentEnemy = enemy;
        _currentCoroutine = StartCoroutine(ProjectileCoroutine());
    }


    public void Stop(GameObject enemy) {
        Debug.Log("STOP");
        if ( enemy == _currentEnemy ) {
            StopCoroutine(_currentCoroutine);
        }
    }
    
    private void SendProjectile() {
        Projectile p = Instantiate(projectile, transform.position, transform.rotation);
        p.setEnemy(_currentEnemy);
    }


    private IEnumerator ProjectileCoroutine() {
        _projectileCoroutineStarted = true;
        SendProjectile();
        while ( true ) {
            yield return new WaitForSeconds(attackSpeed);
            SendProjectile();
        }
    }
    
}
